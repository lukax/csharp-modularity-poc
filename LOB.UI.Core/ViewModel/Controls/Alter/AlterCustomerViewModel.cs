#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.Events.Operation;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>, IAlterCustomerViewModel {

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.Add};
        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }
        //public IAlterPersonViewModel AlterPersonViewModel {
        //    get {
        //        if(Entity.PersonType == Domain.Base.PersonType.Natural) return _alterNaturalPersonViewModel;
        //        if(Entity.PersonType == Domain.Base.PersonType.Legal) return _alterLegalPersonViewModel;
        //        throw new NotImplementedException("PersonType");
        //    }
        //}
        public UIOperation PersonOperation { get; set; }
        public IList<string> PersonTypes { get { return PersonExtensions.PersonTypesLocalizationsDict.Values.ToList(); } }
        public string PersonType { set { Entity.PersonType = value.ToPersonType(); } }
        public string Status { get { return Entity.Status.ToLocalizedString(); } }
        [InjectionConstructor]
        public AlterCustomerViewModel(Customer entity, IRepository repository, IFluentNavigator navigator, IEventAggregator eventAggregator,
            ILoggerFacade logger, AlterLegalPersonViewModel alterLegalPersonViewModel, AlterNaturalPersonViewModel alterNaturalPersonViewModel)
            : base(entity, repository, eventAggregator, logger) {
            //_alterLegalPersonViewModel = alterLegalPersonViewModel;
            //_alterNaturalPersonViewModel = alterNaturalPersonViewModel;
            PersonOperation = new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Add};
            //default init customer as natural person
            //NaturalPersonCfg();
            PersonTypeChanged();
        }

        public override void InitializeServices() { if (Equals(Operation, default(UIOperation))) Operation = _operation; }

        public override void Refresh() { }

        private void PersonTypeChanged() {
            Entity.PropertyChanged += (s, e) => {
                                          if(e.PropertyName == "PersonType")
                                              switch(Entity.PersonType) {
                                                  case Domain.Base.PersonType.Natural:
                                                      NaturalPersonCfg();
                                                      break;
                                                  case Domain.Base.PersonType.Legal:
                                                      LegalPersonCfg();
                                                      break;
                                              }
                                      };
        }

        private void LegalPersonCfg() {
            PersonOperation.Type(UIOperationType.LegalPerson);
            PersonOperation.Entity = Entity.Person;
            EventAggregator.GetEvent<PersonTypeChangedEvent>().Publish(PersonOperation);
        }

        private void NaturalPersonCfg() {
            PersonOperation.Type(UIOperationType.NaturalPerson);
            PersonOperation.Entity = Entity.Person;
            EventAggregator.GetEvent<PersonTypeChangedEvent>().Publish(PersonOperation);
        }

        protected override bool CanSaveChanges(object arg) { return true; }

        protected override bool CanCancel(object arg) { return true; }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) { Entity = new Customer {}; }

    }
}