#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class PayCheckFacade : IPayCheckFacade {
        private PayCheck _entity;
        public PayCheck Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public PayCheck GenerateEntity() { return new PayCheck {Bonus = 0, Code = 0, CurrentSalary = 0, Error = null, PS = "",}; }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) => _entity.Bonus > 30000 ? new ValidationResult("Bonus", Strings.Notification_Field_TooLong) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.CurrentSalary < 0 ? new ValidationResult("CurrentSalary", Strings.Notification_Field_Negative) : null);
                _entity.AddValidation(
                    (sender, name) => string.IsNullOrWhiteSpace(_entity.PS) ? new ValidationResult("PS", Strings.Notification_Field_Empty) : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

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
            fields.AddRange(_entity.GetValidations("Bonus"));
            fields.AddRange(_entity.GetValidations("CurrentSalary"));
            fields.AddRange(_entity.GetValidations("PS"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }
    }
}