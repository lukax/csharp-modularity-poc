#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Events.Operation;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>, IAlterCustomerViewModel {
        private readonly ICustomerFacade _customerFacade;
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Customer, State = ViewState.Add};
        private readonly IAlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        private readonly IAlterLegalPersonViewModel _alterLegalPersonViewModel;
        public IAlterPersonViewModel AlterPersonViewModel {
            get {
                if(Entity.PersonType == Domain.Base.PersonType.Natural) return _alterNaturalPersonViewModel.AlterPersonViewModel;
                if(Entity.PersonType == Domain.Base.PersonType.Legal) return _alterLegalPersonViewModel.AlterPersonViewModel;
                return _alterNaturalPersonViewModel.AlterPersonViewModel;
            }
        }
        public ViewID PersonOperation { get; set; }
        public IList<string> PersonTypes {
            get { return PersonExtensions.PersonTypesLocalizationsDict.Values.ToList(); }
        }
        public string PersonType {
            set { Entity.PersonType = value.ToPersonType(); }
        }
        public string Status {
            get { return Entity.Status.ToLocalizedString(); }
        }

        [InjectionConstructor]
        public AlterCustomerViewModel(Customer entity, ICustomerFacade customerFacade, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade logger, IAlterNaturalPersonViewModel alterNaturalPersonViewModel, IAlterLegalPersonViewModel alterLegalPersonViewModel)
            : base(entity, repository, eventAggregator, logger) {
            _customerFacade = customerFacade;
            _alterNaturalPersonViewModel = alterNaturalPersonViewModel;
            _alterLegalPersonViewModel = alterLegalPersonViewModel;
            PersonOperation = new ViewID {Type = ViewType.Person, State = ViewState.Add};
        }

        public override void Refresh() { }

        private void PersonTypeChanged() {
            if(Entity != null)
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
            PersonOperation.Type(ViewType.LegalPerson);
            PersonOperation.ViewModel = _alterLegalPersonViewModel;
            ClearEntity(null);
            ((AlterLegalPersonViewModel)_alterLegalPersonViewModel).Entity = Entity.Person as LegalPerson;
            EventAggregator.GetEvent<PersonTypeChangedEvent>().Publish(PersonOperation);
        }

        private void NaturalPersonCfg() {
            PersonOperation.Type(ViewType.NaturalPerson);
            PersonOperation.ViewModel = _alterNaturalPersonViewModel;
            ClearEntity(null);
            ((AlterNaturalPersonViewModel)_alterNaturalPersonViewModel).Entity = Entity.Person as NaturalPerson;
            EventAggregator.GetEvent<PersonTypeChangedEvent>().Publish(PersonOperation);
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            ClearEntity(null);
        }

        protected override bool CanSaveChanges(object arg) {
            if(ViewID.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                return _customerFacade.CanAdd(out results) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            }
            if(ViewID.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _customerFacade.CanUpdate(out results) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            }
            return false;
        }

        protected override bool CanCancel(object arg) { return true; }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override void ClearEntity(object arg) {
            Entity = _customerFacade.GenerateEntity();
            PersonTypeChanged();
            _customerFacade.Entity = (Entity);
            //_customerFacade.EnableValidations();
        }

        public override void Dispose() {
            _alterLegalPersonViewModel.Dispose();
            _alterNaturalPersonViewModel.Dispose();
            base.Dispose();
        }
    }
}