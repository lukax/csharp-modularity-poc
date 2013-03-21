#region Usings

using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain
{
    public class Store : BaseEntity
    {
        public LegalPerson LegalPerson { get; set; }
        public string Name { get; set; }
        public IList<Employee> Employees { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Customer> Clients { get; set; }
        public IList<Sale> Sales { get; set; }
        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}