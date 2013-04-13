#region Usings

using System;
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
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>, IAlterCustomerViewModel {

        private readonly AlterLegalPersonViewModel _alterLegalPersonViewModel;
        private readonly AlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        private readonly IFluentNavigator _navigator;
        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.Add};
        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }
        public IAlterPersonViewModel AlterPersonViewModel {
            get {
                if(Entity.PersonType == Domain.Base.PersonType.Natural) return _alterNaturalPersonViewModel;
                if(Entity.PersonType == Domain.Base.PersonType.Legal) return _alterLegalPersonViewModel;
                throw new NotImplementedException("PersonType");
            }
        }
        public IList<string> PersonTypes { get { return PersonExtensions.PersonTypesLocalizationsDict.Values.ToList(); } }
        public string PersonType { set { Entity.PersonType = value.ToPersonType(); } }

        [InjectionConstructor]
        public AlterCustomerViewModel(Customer entity, IRepository repository, IFluentNavigator navigator, IEventAggregator eventAggregator,
            ILoggerFacade logger, AlterLegalPersonViewModel alterLegalPersonViewModel, AlterNaturalPersonViewModel alterNaturalPersonViewModel)
            : base(entity, repository, eventAggregator, logger) {
            _navigator = navigator;
            _alterLegalPersonViewModel = alterLegalPersonViewModel;
            _alterNaturalPersonViewModel = alterNaturalPersonViewModel;
            Operation = _operation;
            //default init customer as natural person
            NaturalPersonCfg();
            PersonTypeChanged();
        }

        public override void InitializeServices() { }

        public override void Refresh() { Entity = new Customer(); }

        private void PersonTypeChanged() {
            Entity.PropertyChanged += (s, e) => {
                                          if(e.PropertyName == "PersonType") {
                                              switch(Entity.PersonType) {
                                                  case Domain.Base.PersonType.Natural:
                                                      NaturalPersonCfg();
                                                      break;
                                                  case Domain.Base.PersonType.Legal:
                                                      LegalPersonCfg();
                                                      break;
                                              }
                                              OnPropertyChanged("AlterPersonViewModel");
                                          }
                                      };
        }

        private void LegalPersonCfg() { Entity.Person = _alterLegalPersonViewModel.Entity; }

        private void NaturalPersonCfg() { Entity.Person = _alterNaturalPersonViewModel.Entity; }

        protected override bool CanSaveChanges(object arg) { return true; }

        protected override bool CanCancel(object arg) { return true; }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) { Entity = new Customer {}; }

    }
}