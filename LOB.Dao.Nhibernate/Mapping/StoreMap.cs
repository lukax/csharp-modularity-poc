#region Usings

using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class StoreMap : SubclassMap<Company> {
        public StoreMap() {
            Map(x => x.LocalName);
            HasMany(x => x.Employees).Cascade.All();
            HasManyToMany(x => x.Products).Table("ProductStore").Cascade.All();
            HasManyToMany(x => x.Customers).Cascade.All();
            HasMany(x => x.Orders);
            //References(x => x.Address).Cascade.All();
            //References(x => x.ContactInfo).Cascade.All();
        }
    }
}