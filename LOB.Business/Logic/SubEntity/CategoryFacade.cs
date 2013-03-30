#region Usings
using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class CategoryFacade : ICategoryFacade {

        private readonly IServiceFacade _serviceFacade;

        private Category _entity;

        public CategoryFacade(IServiceFacade serviceFacade) {
            this._serviceFacade = serviceFacade;
        }

        public void SetEntity<T>(T entity) where T : Category {
            this._serviceFacade.SetEntity(entity);
            this._entity = entity;
        }

        public void ConfigureValidations() {
            this._serviceFacade.ConfigureValidations();
            if(this._entity != null) {
                //Category validations...
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            //TODO: custom validations for Category

            IEnumerable<ValidationResult> validationResults;
            bool result = this._serviceFacade.CanAdd(out validationResults);
            invalidFields = fields;
            return result;
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

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

    }
}