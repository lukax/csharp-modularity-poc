#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(ICustomerFacade)), Export(typeof(IBaseEntityFacade<Customer>))]
    public sealed class CustomerFacade : BaseEntityFacade<Customer>, ICustomerFacade {
        private readonly INaturalPersonFacade _naturalPersonFacade;
        private readonly ILegalPersonFacade _legalPersonFacade;

        [ImportingConstructor]
        public CustomerFacade(INaturalPersonFacade naturalPersonFacade, ILegalPersonFacade legalPersonFacade, IRepository repository)
            : base(repository) {
            _naturalPersonFacade = naturalPersonFacade;
            _legalPersonFacade = legalPersonFacade;
        }

        public Customer GenerateEntity(PersonType personType) {
            var result = GenerateEntity();
            if(personType == PersonType.Natural) {
                result.BoughtHistory = new List<Sale>();
                result.Status = default(CustomerStatus);
                result.CustomerOf = new List<Store>();
                result.Person = _naturalPersonFacade.GenerateEntity();
                result.PersonType = default(PersonType);
            }
            if(personType == PersonType.Legal) {
                result.BoughtHistory = new List<Sale>();
                result.Status = default(CustomerStatus);
                result.CustomerOf = new List<Store>();
                result.Person = _legalPersonFacade.GenerateEntity();
                result.PersonType = default(PersonType);
            }
            return result;
        }
    }
}