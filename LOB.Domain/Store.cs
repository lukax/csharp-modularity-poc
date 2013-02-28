#region Usings

using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Domain
{
    public class Store : BaseEntity
    {
        public virtual LegalPerson LegalPerson { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Employee> Employees { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual IList<Client> Clients { get; set; }
        public virtual IList<Sale> Sales { get; set; }
        public virtual Address Address { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
    }
}