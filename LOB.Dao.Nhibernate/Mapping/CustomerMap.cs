#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class CustomerMap : BaseEntityMap<Customer> {
        public CustomerMap() {
            References(x => x.Person).Cascade.All();
            HasManyToMany(x => x.AssociatedCompanies);
            Map(x => x.Status);
            HasMany(x => x.Orders);
            Map(x => x.PersonType);
        }
    }
}