#region Usings
using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class ProductFacade : IProductFacade {

        private readonly IServiceFacade _serviceFacade;

        private Product _entity;

        public ProductFacade(IServiceFacade serviceFacade) {
            this._serviceFacade = serviceFacade;
        }

        public void SetEntity<T>(T entity) where T : Product {
            this._entity = entity;
        }

        public void ConfigureValidations() {
            this._serviceFacade.ConfigureValidations();
            if(this._entity != null) {
                //Validations for product later..        
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) {
            ((IBaseEntityFacade) this._serviceFacade).SetEntity(entity);
        }

        void IServiceFacade.SetEntity<T>(T entity) {
            (this._serviceFacade).SetEntity(entity);
        }

    }
}