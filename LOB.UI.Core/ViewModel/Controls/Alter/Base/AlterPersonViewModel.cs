#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
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
        private readonly IEventAggregator _eventAggregator;

        [InjectionConstructor]
        protected AlterPersonViewModel(Person entity, IPersonFacade personFacade,
            AlterAddressViewModel alterAddressViewModel, AlterContactInfoViewModel alterContactInfoViewModel,
            IRepository repository, IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _personFacade = personFacade;
            _alterAddressViewModel = alterAddressViewModel;
            _alterContactInfoViewModel = alterContactInfoViewModel;
            _eventAggregator = eventAggregator;

            //TODO: Use business logic to set default params
            if(Entity.Address.State == null && Entity.Address.Country == null) {
                Entity.Address.Country = "Brasil";
                Entity.Address.State = UfBrDictionary.Ufs[UF.RJ];
            }
        }

        public IAlterAddressViewModel AlterAddressViewModel {
            get { return _alterAddressViewModel; }
            set { _alterAddressViewModel = value as AlterAddressViewModel; }
        }
        public IAlterContactInfoViewModel AlterContactInfoViewModel {
            get { return _alterContactInfoViewModel; }
            set { _alterContactInfoViewModel = value as AlterContactInfoViewModel; }
        }

        public override void InitializeServices() { ClearEntity(null); }

        public override void Refresh() { ClearEntity(null); }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Person,
            State = UIOperationState.Add
        };
        public override UIOperation UIOperation {
            get { return _operation; }
        }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow) {
                Repository.Uow.BeginTransaction();
                Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(UIOperation); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            return _personFacade.CanAdd(out results);
        }

        protected override bool CanCancel(object arg) { return true; }

        protected override void QuickSearch(object arg) { _eventAggregator.GetEvent<QuickSearchEvent>().Publish(UIOperation); }

        protected override void ClearEntity(object arg) {
            _alterAddressViewModel.ClearEntityCommand.Execute(null);
            _alterContactInfoViewModel.ClearEntityCommand.Execute(null);
            Entity = new LocalPerson {
                Address = _alterAddressViewModel.Entity,
                Code = 0,
                ContactInfo = _alterContactInfoViewModel.Entity,
                Notes = "",
            };
            _personFacade.SetEntity(Entity);
            _personFacade.ConfigureValidations();
        }

        private class LocalPerson : Person {

        }

    }
}