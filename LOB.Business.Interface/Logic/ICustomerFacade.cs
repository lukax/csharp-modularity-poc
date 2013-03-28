#region Usings

using LOB.Business.Interface.Logic.Base;
using LOB.Domain;

#endregion

namespace LOB.Business.Interface.Logic
{
    public interface ICustomerFacade : IPersonFacade
    {
        new void SetEntity<T>(T entity) where T : Customer;
    }
}