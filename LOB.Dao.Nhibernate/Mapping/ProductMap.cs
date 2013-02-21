using FluentNHibernate.Mapping;
using LOB.Domain;

namespace LOB.Dao.Nhibernate.Mapping
{
    public class ProductMap : SubclassMap<Product>
    {
        public ProductMap()
        {
            References(x => x.Category);
            Map(x => x.Status);
            Map(x => x.UnitsInStock);
            Map(x => x.UnitCostPrice);
            Map(x => x.UnitSalePrice);
            Map(x => x.ProfitMargin);
            Map(x => x.QuantityPerUnity);
            HasManyToMany(x => x.StockedStores)
                .Cascade.All()
                .Inverse()
                .Table("ProductStore");
            HasManyToMany(x => x.Sales)
                .Inverse()
                .Cascade.All()
                .Table("ProductSale");
            References(x => x.Suppliers);
            References(x => x.ShipmentInfo);
        }
    }
}