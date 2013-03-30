#region Usings
using FluentNHibernate.Mapping;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class ProductMap : SubclassMap<Product> {

        public ProductMap() {
            References(x => x.Category);
            Map(x => x.Status);
            Map(x => x.CodBarras);
            Map(x => x.Image); //.CustomSqlType("BinaryBlob"); Nhibernate automap binary arrays
            Map(x => x.UnitsInStock);
            Map(x => x.MaxUnitsOfStock);
            Map(x => x.MinUnitsOfStock);
            Map(x => x.UnitCostPrice);
            Map(x => x.UnitSalePrice);
            Map(x => x.ProfitMargin);
            Map(x => x.QuantityPerUnit);
            HasManyToMany(x => x.StockedStores).Cascade.All().Inverse().Table("ProductStore");
            HasManyToMany(x => x.Sales).Inverse().Cascade.All().Table("ProductSale");
            HasMany(x => x.Suppliers);
            References(x => x.ShipmentInfo);
        }

    }
}