#region Usings

using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public class AlterPersonViewModel : AlterBaseEntityViewModel<Person>, IAlterPersonViewModel {
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Person, State = ViewState.Add};
        private AlterAddressViewModel _alterAddressViewModel;
        private AlterContactInfoViewModel _alterContactInfoViewModel;

        public AlterPersonViewModel(IPersonFacade personFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(personFacade, repository, eventAggregator, logger) { }

        [Import] public IAlterAddressViewModel AlterAddressViewModel {
            get { return _alterAddressViewModel; }
            set { _alterAddressViewModel = value as AlterAddressViewModel; }
        }
        [Import] public IAlterContactInfoViewModel AlterContactInfoViewModel {
            get { return _alterContactInfoViewModel; }
            set { _alterContactInfoViewModel = value as AlterContactInfoViewModel; }
        }

        protected override bool CanSaveChanges(object arg) {
            if(ReferenceEquals(Entity, null)) return false;
            if(ViewID.State == ViewState.Add)
                return base.CanSaveChanges(arg) & _alterAddressViewModel.SaveChangesCommand.CanExecute(arg) &&
                       _alterContactInfoViewModel.SaveChangesCommand.CanExecute(arg);
            if(ViewID.State == ViewState.Update)
                return base.CanSaveChanges(arg) & _alterContactInfoViewModel.SaveChangesCommand.CanExecute(arg) &&
                       _alterContactInfoViewModel.SaveChangesCommand.CanExecute(arg);
            return false;
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
            AlterAddressViewModel.InitializeServices();
            AlterContactInfoViewModel.InitializeServices();
        }

        protected override void EntityChanged() {
            base.EntityChanged();
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