#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public class AlterPersonViewModel : AlterBaseEntityViewModel<Person>, IAlterPersonViewModel {
        private readonly IPersonFacade _personFacade;
        private AlterAddressViewModel _alterAddressViewModel;
        private AlterContactInfoViewModel _alterContactInfoViewModel;

        [InjectionConstructor]
        public AlterPersonViewModel(IPersonFacade personFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(repository, eventAggregator, logger) { _personFacade = personFacade; }

        [Dependency]
        public IAlterAddressViewModel AlterAddressViewModel {
            get { return _alterAddressViewModel; }
            set { _alterAddressViewModel = value as AlterAddressViewModel; }
        }
        [Dependency]
        public IAlterContactInfoViewModel AlterContactInfoViewModel {
            get { return _alterContactInfoViewModel; }
            set { _alterContactInfoViewModel = value as AlterContactInfoViewModel; }
        }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override bool CanSaveChanges(object arg) {
            if(ViewID.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                return _personFacade.CanAdd(out results) & _alterAddressViewModel.SaveChangesCommand.CanExecute(null) &&
                       _alterContactInfoViewModel.SaveChangesCommand.CanExecute(null);
            }
            if(ViewID.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _personFacade.CanUpdate(out results) & _alterContactInfoViewModel.SaveChangesCommand.CanExecute(null) &&
                       _alterContactInfoViewModel.SaveChangesCommand.CanExecute(null);
            }
            return false;
        }

        protected override void ClearEntity(object arg) { Entity = _personFacade.GenerateEntity(); }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Person, State = ViewState.Add};

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
            AlterAddressViewModel.InitializeServices();
            AlterContactInfoViewModel.InitializeServices();
        }
        public override void Refresh() { ClearEntity(null); }

        protected override void EntityChanged() {
            base.EntityChanged();
            _personFacade.Entity = Entity;
            _alterAddressViewModel.Entity = Entity.Address;
            _alterContactInfoViewModel.Entity = Entity.ContactInfo;
        }

        public override void Dispose() {
            _alterAddressViewModel.Dispose();
            _alterContactInfoViewModel.Dispose();
            base.Dispose();
        }
    }
}