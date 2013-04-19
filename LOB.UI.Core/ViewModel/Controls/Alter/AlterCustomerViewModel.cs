#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.Events.Operation;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterCustomerViewModel))]
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>, IAlterCustomerViewModel {
        private readonly ICustomerFacade _customerFacade;
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Customer, State = ViewState.Add};
        private readonly AlterNaturalPersonViewModel _alterNaturalPersonViewModel;
        private readonly AlterLegalPersonViewModel _alterLegalPersonViewModel;
        private string _personType;
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
            get { return _personType ?? ""; }
            set {
                _personType = value;
                PersonTypeChanged();
            }
        }
        public string Status {
            get { return Entity.Status.ToLocalizedString(); }
        }

        [ImportingConstructor]
        public AlterCustomerViewModel(ICustomerFacade customerFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger,
            IAlterNaturalPersonViewModel alterNaturalPersonViewModel, IAlterLegalPersonViewModel alterLegalPersonViewModel)
            : base(customerFacade, repository, eventAggregator, logger) {
            _customerFacade = customerFacade;
            _alterNaturalPersonViewModel = alterNaturalPersonViewModel as AlterNaturalPersonViewModel;
            _alterLegalPersonViewModel = alterLegalPersonViewModel as AlterLegalPersonViewModel;
            PersonOperation = new ViewID {Type = ViewType.Person, State = ViewState.Add};
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }

        private void PersonTypeChanged() {
            if(string.IsNullOrWhiteSpace(PersonType)) return;
            if(PersonType.ToPersonType() == Domain.Base.PersonType.Natural) NaturalPersonCfg();
            if(PersonType.ToPersonType() == Domain.Base.PersonType.Legal) LegalPersonCfg();
        }

        private void LegalPersonCfg() {
            PersonOperation.Type(ViewType.LegalPerson);
            PersonOperation.ViewModel = _alterLegalPersonViewModel;
            ClearEntity(null);
            (_alterLegalPersonViewModel).Entity = Entity.Person as LegalPerson;
            EventAggregator.GetEvent<PersonTypeChangedEvent>().Publish(PersonOperation);
        }

        private void NaturalPersonCfg() {
            PersonOperation.Type(ViewType.NaturalPerson);
            PersonOperation.ViewModel = _alterNaturalPersonViewModel;
            ClearEntity(null);
            (_alterNaturalPersonViewModel).Entity = Entity.Person as NaturalPerson;
            EventAggregator.GetEvent<PersonTypeChangedEvent>().Publish(PersonOperation);
        }

        protected override bool CanSaveChanges(object arg) {
            if(ReferenceEquals(Entity, null)) return false;
            if(ViewID.State == ViewState.Add) return base.CanSaveChanges(arg) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            if(ViewID.State == ViewState.Update) return base.CanSaveChanges(arg) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            return false;
        }

        protected override void ClearEntity(object arg) { Entity = _customerFacade.GenerateEntity(PersonType.ToPersonType()); }

        public override void Dispose() {
            _alterLegalPersonViewModel.Dispose();
            _alterNaturalPersonViewModel.Dispose();
            base.Dispose();
        }
    }
}