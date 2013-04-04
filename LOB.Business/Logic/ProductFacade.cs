#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic {
    public class ProductFacade : IProductFacade {

        private readonly IServiceFacade _serviceFacade;
        private readonly ICategoryFacade _categoryFacade;
        private readonly IShipmentInfoFacade _shipmentInfoFacade;

        private Product _entity;

        public ProductFacade(IServiceFacade serviceFacade, ICategoryFacade categoryFacade,
            IShipmentInfoFacade shipmentInfoFacade) {
            _serviceFacade = serviceFacade;
            _categoryFacade = categoryFacade;
            _shipmentInfoFacade = shipmentInfoFacade;
        }

        public void SetEntity<T>(T entity) where T : Product {
            _entity = entity;
            _serviceFacade.SetEntity(entity);
        }

        public Product GenerateEntity() {
            var localService = _serviceFacade.GenerateEntity();
            var localShipmentInfo = _shipmentInfoFacade.GenerateEntity();
            var localCategory = _categoryFacade.GenerateEntity();
            return new Product {
                Code = 0,
                Error = null,
                Status = default(ProductStatus),
                Category = localCategory,
                Description = localService.Description,
                Name = localService.Name,
                ShipmentInfo = localShipmentInfo,
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

        Service IServiceFacade.GenerateEntity() { return GenerateEntity(); }

        public void ConfigureValidations() {
            _serviceFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.Name.Length < 1
                        ? new ValidationResult("Name", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.Description.Length > 300
                        ? new ValidationResult("Description", Strings.Error_Field_TooLong)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.UnitSalePrice < 0
                        ? new ValidationResult("UnitSalePrice", Strings.Error_Field_Negative)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.UnitsInStock < 0
                        ? new ValidationResult("UnitsInStock", Strings.Error_Field_Negative)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.Category == default(Category)
                        ? new ValidationResult("Category", Strings.Error_Field_Empty)
                        : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            //TODO: custom validations for Category

            IEnumerable<ValidationResult> validationResults;
            bool result = _serviceFacade.CanAdd(out validationResults);
            //if(result)
            //     result = _categoryFacade.CanAdd(out validationResults);
            //if(result) 
            //     result = _shipmentInfoFacade.CanAdd(out validationResults);
            invalidFields = fields;
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) { throw new NotImplementedException(); }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) { throw new NotImplementedException(); }

        void IBaseEntityFacade.SetEntity<T>(T entity) { ((IBaseEntityFacade)_serviceFacade).SetEntity(entity); }

        void IServiceFacade.SetEntity<T>(T entity) { (_serviceFacade).SetEntity(entity); }

    }
}