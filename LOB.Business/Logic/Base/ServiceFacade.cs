#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic.Base {
    public class ServiceFacade : IServiceFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private Service _entity;

        public ServiceFacade(IBaseEntityFacade baseEntityFacade) {
            this._baseEntityFacade = baseEntityFacade;
        }

        public void SetEntity<T>(T entity) where T : Service {
            this._baseEntityFacade.SetEntity(entity);
            this._entity = entity;
        }

        public void ConfigureValidations() {
            this._baseEntityFacade.ConfigureValidations();
            if(this._entity != null) {
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.Name.Length < 1
                                               ? new ValidationResult("Name", Strings.Error_Field_Empty)
                                               : null);
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.Description.Length > 300
                                               ? new ValidationResult("Description", Strings.Error_Field_Empty)
                                               : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = this.ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) {
            this._baseEntityFacade.SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(this._entity.GetValidations("Name"));
            fields.AddRange(this._entity.GetValidations("Description"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}