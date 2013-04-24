#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using LOB.Business.Interface.Logic;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterCustomerViewModel))]
    public sealed class AlterCustomerViewModel : AlterBaseEntityViewModel<Customer>, IAlterCustomerViewModel {
        private readonly ICustomerFacade _customerFacade;
        private string _personType;
        public IList<string> PersonTypes {
            get { return PersonExtension.PersonTypesLocalizationsDict.Values.ToList(); }
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
        public AlterCustomerViewModel(ICustomerFacade customerFacade)
            : base(customerFacade) { _customerFacade = customerFacade; }

        private void PersonTypeChanged() {
            if(string.IsNullOrWhiteSpace(PersonType)) return;
            if(PersonType.ToPersonType() == Domain.Base.PersonType.Natural) NaturalPersonCfg();
            if(PersonType.ToPersonType() == Domain.Base.PersonType.Legal) LegalPersonCfg();
        }

        private void LegalPersonCfg() {
            ClearEntityExecute(null);
            //EventAggregator.GetEvent<PersonTypeChangedEvent>().Publish(PersonOperation);
        }

        private void NaturalPersonCfg() {
            ClearEntityExecute(null);
            //EventAggregator.GetEvent<PersonTypeChangedEvent>().Publish(PersonOperation);
        }

        protected override void ClearEntityExecute(object arg) { Entity = _customerFacade.GenerateEntity(PersonType.ToPersonType()); }
    }
}