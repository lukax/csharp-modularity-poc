#region Usings

using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.Domain {
    public class Store : BaseEntity {

        public LegalPerson LegalPerson { get; set; }
        public string Name { get; set; }
        public IList<Employee> Employees { get; set; }
        public IList<Product> Products { get; set; }
        public IList<Customer> Clients { get; set; }
        public IList<Sale> Sales { get; set; }

    }
}