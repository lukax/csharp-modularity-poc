#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class PayCheckFacade : IPayCheckFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private PayCheck _entity;

        public PayCheckFacade(IBaseEntityFacade baseEntityFacade) { _baseEntityFacade = baseEntityFacade; }

        public void SetEntity<T>(T entity) where T : PayCheck {
            _baseEntityFacade.SetEntity(entity);
            _entity = entity;
        }

        public PayCheck GenerateEntity() { return new PayCheck {Bonus = 0, Code = 0, CurrentSalary = 0, Error = null, PS = "",}; }

        public void ConfigureValidations() {
            _baseEntityFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.Bonus > 30000
                        ? new ValidationResult("Bonus", Strings.Error_Field_TooLong)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.CurrentSalary < 1
                        ? new ValidationResult("CurrentSalary", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.PS)
                        ? new ValidationResult("PS", Strings.Error_Field_Empty)
                        : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields)
        {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields)
        {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) { _baseEntityFacade.SetEntity(entity); }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("Bonus"));
            fields.AddRange(_entity.GetValidations("CurrentSalary"));
            fields.AddRange(_entity.GetValidations("PS"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(
                          validationResult =>
                          !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}