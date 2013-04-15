#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic {
    public class ProductFacade : IProductFacade {
        private Product _entity;
        public Product Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public Product GenerateEntity() {
            return new Product {
                Code = 0,
                Error = null,
                Status = default(ProductStatus),
                Category = null,
                Description = null,
                Name = null,
                ShipmentInfo = new ShipmentInfo(),
                CodBarras = 0,
                Image = new byte[8],
                MaxUnitsOfStock = 0,
                MinUnitsOfStock = 0,
                ProfitMargin = 0,
                QuantityPerUnit = "",
                Sales = new List<Sale>(),
                StockedStores = new List<Store>(),
                Suppliers = new List<Supplier>(),
                UnitCostPrice = 0,
                UnitSalePrice = 0,
                UnitsInStock = 0,
            };
        }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) => string.IsNullOrWhiteSpace(_entity.Name) ? new ValidationResult("Name", Strings.Notification_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.Description.Length > 300
                        ? new ValidationResult("Description", string.Format(Strings.Notification_Field_X_MaxLength, 300))
                        : null);
                _entity.AddValidation(
                    (sender, name) => _entity.UnitSalePrice < 0 ? new ValidationResult("UnitSalePrice", Strings.Notification_Field_Negative) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.UnitsInStock < 0 ? new ValidationResult("UnitsInStock", Strings.Notification_Field_Negative) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.Category.ToString()) ? new ValidationResult("Category", Strings.Notification_Field_Empty) : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) { return ProcessBasicValidations(out invalidFields); }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("Name"));
            fields.AddRange(_entity.GetValidations("Description"));
            fields.AddRange(_entity.GetValidations("UnitSalePrice"));
            fields.AddRange(_entity.GetValidations("UnitsInStock"));
            fields.AddRange(_entity.GetValidations("Category"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }
    }
}