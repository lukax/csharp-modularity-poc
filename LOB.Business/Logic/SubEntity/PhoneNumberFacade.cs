#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class PhoneNumberFacade : IPhoneNumberFacade {
        private PhoneNumber _entity;
        public PhoneNumber Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public PhoneNumber GenerateEntity() { return new PhoneNumber {Code = 0, Error = null, Description = "", Number = "", Type = default(PhoneNumberType),}; }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.Number)) return new ValidationResult("Number", Strings.Notification_Field_Empty);
                                          if(_entity.Number.Length < 8) return new ValidationResult("Number", string.Format(Strings.Notification_Field_X_MinLength, 8));
                                          if(!Regex.IsMatch(_entity.Number, @"\d")) return new ValidationResult("Number", Strings.Notification_Field_IntOnly);
                                          return null;
                                      });
                _entity.AddValidation(
                    delegate {
                        return string.IsNullOrWhiteSpace(_entity.Type.ToString())
                                   ? new ValidationResult("Type", Strings.Notification_Field_Empty)
                                   : null;
                    });
                _entity.AddValidation(
                    delegate {
                        return string.IsNullOrWhiteSpace(_entity.Description)
                                   ? new ValidationResult("Description", Strings.Notification_Field_Empty)
                                   : null;
                    });
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
            fields.AddRange(_entity.GetValidations("Number"));
            fields.AddRange(_entity.GetValidations("Description"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }
    }
}