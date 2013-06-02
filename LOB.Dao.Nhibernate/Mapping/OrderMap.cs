#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class OrderMap : BaseEntityMap<Order> {
        public OrderMap() {
            Map(x => x.Status);
            References(x => x.Buyer);
            HasManyToMany(x => x.Products);
            Map(x => x.TotalValue);
            Map(x => x.Description);
        }
    }
}