#region Usings

using LOB.Business.Interface.Logic.Base;
using LOB.Domain;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Interface.Logic {
    public interface ICustomerFacade : IBaseEntityFacade<Customer> {
        Customer GenerateEntity(PersonType personType);
    }
}