#region Usings

using System.Collections.Generic;
using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Business.Logic.Base;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    [Export(typeof(IProductFacade))]
    public sealed class ProductFacade : BaseEntityFacade<Product>, IProductFacade {
        private readonly ICategoryFacade _categoryFacade;
        private readonly IShipmentInfoFacade _shipmentInfoFacade;

        [ImportingConstructor]
        public ProductFacade(ICategoryFacade categoryFacade, IShipmentInfoFacade shipmentInfoFacade, IRepository repository)
            : base(repository) {
            _categoryFacade = categoryFacade;
            _shipmentInfoFacade = shipmentInfoFacade;
            ConfigureValidations();
        }

        public override Product GenerateEntity() {
            var result = base.GenerateEntity();
            result.Status = default(ProductStatus);
            result.Category = _categoryFacade.GenerateEntity();
            result.Description = "";
            result.Name = "";
            result.ShipmentInfo = _shipmentInfoFacade.GenerateEntity();
            result.CodBarras = 0;
            result.Image = new byte[8];
            result.MaxUnitsOfStock = 0;
            result.MinUnitsOfStock = 0;
            result.ProfitMargin = 0;
            result.QuantityPerUnit = "";
            result.Sales = new List<Sale>();
            result.StockedStores = new List<Store>();
            result.Suppliers = new List<Supplier>();
            result.UnitCostPrice = 0;
            result.UnitSalePrice = 0;
            result.UnitsInStock = 0;
            return result;
        }

        public void ConfigureValidations() {
            AddValidation(
                (sender, name) => string.IsNullOrWhiteSpace(Entity.Name) ? new ValidationResult("Name", Strings.Notification_Field_Empty) : null);
            AddValidation(
                (sender, name) =>
                Entity.Description.Length > 300
                    ? new ValidationResult("Description", string.Format(Strings.Notification_Field_X_MaxLength, 300))
                    : null);
            AddValidation(
                (sender, name) => Entity.UnitSalePrice < 0 ? new ValidationResult("UnitSalePrice", Strings.Notification_Field_Negative) : null);
            AddValidation((sender, name) => Entity.UnitsInStock < 0 ? new ValidationResult("UnitsInStock", Strings.Notification_Field_Negative) : null);
            AddValidation(
                (sender, name) =>
                string.IsNullOrWhiteSpace(Entity.Category.ToString()) ? new ValidationResult("Category", Strings.Notification_Field_Empty) : null);
        }
    }
}