#region Usings
using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class SaleMap : BaseEntityMap<Sale> {

        public SaleMap() {
            this.Map(x => x.State);
            this.Map(x => x.SaleDate);
            References(x => x.Buyer);
            HasManyToMany(x => x.Products);
            this.Map(x => x.TotalValue);
            this.Map(x => x.UnitValue);
            this.Map(x => x.Quantity);
            this.Map(x => x.Ps);
        }

    }
}