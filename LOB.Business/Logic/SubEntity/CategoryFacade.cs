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
            _serviceFacade = serviceFacade;
        }

        public void SetEntity<T>(T entity) where T : Category {
            _serviceFacade.SetEntity(entity);
            _entity = entity;
        }

        public void ConfigureValidations() {
            _serviceFacade.ConfigureValidations();
            if (_entity != null) {
                //Category validations...
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            //TODO: custom validations for Category

            IEnumerable<ValidationResult> validationResults;
            bool result = _serviceFacade.CanAdd(out validationResults);
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
            ((IBaseEntityFacade) _serviceFacade).SetEntity(entity);
        }

        void IServiceFacade.SetEntity<T>(T entity) {
            ((IServiceFacade) _serviceFacade).SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }
    }
}