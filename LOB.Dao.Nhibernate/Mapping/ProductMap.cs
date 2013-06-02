#region Usings

using LOB.Dao.Nhibernate.Mapping.Base;
using LOB.Domain;

#endregion

namespace LOB.Dao.Nhibernate.Mapping {
    public class ProductMap : BaseEntityMap<Product> {
        public ProductMap() {
            References(x => x.Category);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Status);
            Map(x => x.CodeBarras);
            Map(x => x.CodeNCM);
            Map(x => x.CFOP);
            Map(x => x.Image);
            Map(x => x.UnitCostPrice);
            Map(x => x.UnitSalePrice);
            Map(x => x.ProfitMargin);
            HasManyToMany(x => x.AssociatedCompanies).Cascade.All().Inverse().Table("ProductStore");
            HasManyToMany(x => x.AssociatedOrders).Inverse().Cascade.All().Table("ProductSale");
            HasMany(x => x.Suppliers);
        }
    }
}