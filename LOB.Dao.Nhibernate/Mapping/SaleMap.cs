#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class SaleMap : BaseEntityMap<Sale> {
        public SaleMap() {
            Map(x => x.State);
            Map(x => x.SaleDate);
            References(x => x.Buyer);
            HasManyToMany(x => x.Products);
            Map(x => x.TotalValue);
            Map(x => x.UnitValue);
            Map(x => x.Quantity);
            Map(x => x.Ps);
        }
    }
}