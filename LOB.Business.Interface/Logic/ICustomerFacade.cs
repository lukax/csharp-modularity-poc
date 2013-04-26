#region Usings

using LOB.Business.Contract.Logic.Base;
using LOB.Domain;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Contract.Logic {
    public interface ICustomerFacade : IBaseEntityFacade<Customer> {
        Customer GenerateEntity(PersonType personType);
    }
}