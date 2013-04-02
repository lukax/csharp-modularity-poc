#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.Domain.Logic;

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

        public void SetEntity<T>(T entity) where T : Product { _entity = entity; }

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
                Image = null,
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
                //Validations for product later..        
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) { throw new NotImplementedException(); }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) { throw new NotImplementedException(); }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) { throw new NotImplementedException(); }

        void IBaseEntityFacade.SetEntity<T>(T entity) { ((IBaseEntityFacade)_serviceFacade).SetEntity(entity); }

        void IServiceFacade.SetEntity<T>(T entity) { (_serviceFacade).SetEntity(entity); }

    }
}