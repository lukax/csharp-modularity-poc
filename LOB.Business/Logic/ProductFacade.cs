﻿#region Usings

using System.Collections.Generic;
using System.Linq;
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
        //private readonly IShipmentInfoFacade _shipmentInfoFacade;

        private Product _entity;

        public ProductFacade(IServiceFacade serviceFacade, ICategoryFacade categoryFacade) {
            _serviceFacade = serviceFacade;
            _categoryFacade = categoryFacade;
            //_shipmentInfoFacade = shipmentInfoFacade;
        }

        public void SetEntity<T>(T entity) where T : Product {
            _entity = entity;
            _serviceFacade.SetEntity(entity);
        }

        public Product GenerateEntity() {
            var localService = _serviceFacade.GenerateEntity();
            //var localShipmentInfo = _shipmentInfoFacade.GenerateEntity();
            var localCategory = _categoryFacade.GenerateEntity();
            return new Product {
                Code = 0,
                Error = null,
                Status = default(ProductStatus),
                Category = localCategory,
                Description = localService.Description,
                Name = localService.Name,
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

        Service IServiceFacade.GenerateEntity() { return GenerateEntity(); }

        public void ConfigureValidations() {
            _serviceFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) => string.IsNullOrWhiteSpace(_entity.Name) ? new ValidationResult("Name", Strings.Notification_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.Description.Length > 300 ? new ValidationResult("Description", string.Format(Strings.Notification_Field_X_MaxLength, 300)) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.UnitSalePrice < 0 ? new ValidationResult("UnitSalePrice", Strings.Notification_Field_Negative) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.UnitsInStock < 0 ? new ValidationResult("UnitsInStock", Strings.Notification_Field_Negative) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.Category.ToString()) ? new ValidationResult("Category", Strings.Notification_Field_Empty) : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            IEnumerable<ValidationResult> validationResults;
            bool result = _serviceFacade.CanAdd(out validationResults);
            if(result) result = ProcessBasicValidations(out validationResults);
            //if(result) result = _categoryFacade.CanAdd(out validationResults);
            //if(result) result = _shipmentInfoFacade.CanAdd(out validationResults);
            invalidFields = validationResults;
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) { ((IBaseEntityFacade)_serviceFacade).SetEntity(entity); }

        void IServiceFacade.SetEntity<T>(T entity) { (_serviceFacade).SetEntity(entity); }

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