#region Usings
using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class ProductMap : SubclassMap<Product> {

        public ProductMap() {
            References(x => x.Category);
            this.Map(x => x.Status);
            this.Map(x => x.CodBarras);
            this.Map(x => x.Image); //.CustomSqlType("BinaryBlob"); Nhibernate automap binary arrays
            this.Map(x => x.UnitsInStock);
            this.Map(x => x.MaxUnitsOfStock);
            this.Map(x => x.MinUnitsOfStock);
            this.Map(x => x.UnitCostPrice);
            this.Map(x => x.UnitSalePrice);
            this.Map(x => x.ProfitMargin);
            this.Map(x => x.QuantityPerUnit);
            HasManyToMany(x => x.StockedStores).Cascade.All().Inverse().Table("ProductStore");
            HasManyToMany(x => x.Sales).Inverse().Cascade.All().Table("ProductSale");
            HasMany(x => x.Suppliers);
            References(x => x.ShipmentInfo);
        }

    }
}