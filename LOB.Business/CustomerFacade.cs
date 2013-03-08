#region Usings

using LOB.Domain;

#endregion

namespace LOB.Business
{
    public class CustomerFacade : EntityFacade<Customer>
    {
        public CustomerFacade(Customer entity)
            : base(entity)
        {
        }
    }
}