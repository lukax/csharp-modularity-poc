#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Business.Contract.Logic;
using LOB.Business.Contract.Logic.Base;
using LOB.Business.Contract.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Dao.Contract;
using LOB.Domain;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(IProductFacade)), Export(typeof(IBaseEntityFacade<Product>)), PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class ProductFacade : BaseEntityFacade, IProductFacade {
        private readonly ICategoryFacade _categoryFacade;

        [ImportingConstructor]
        public ProductFacade(ICategoryFacade categoryFacade, IShipmentFacade shipmentFacade, IRepository repository)
                : base(repository) { _categoryFacade = categoryFacade; }

        public Product Generate() {
            var result = new Product {
                    Status = default(ProductStatus),
                    Category = _categoryFacade.Generate(),
                    Description = "",
                    Name = "",
                    CodeBarras = 0,
                    Image = new byte[8],
                    ProfitMargin = 0,
                    AssociatedOrders = new List<Order>(),
                    AssociatedCompanies = new List<Company>(),
                    Suppliers = new List<Supplier>(),
                    UnitCostPrice = 0,
                    UnitSalePrice = 0
            };
            return result;
        }
    }
}