#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic.Base {
    public class PersonFacade : IPersonFacade {
        private Person _entity;
        public Person Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public Person GenerateEntity() { return new LocalPerson {Code = 0, Error = null, Address = null, ContactInfo = null, Notes = "",}; }

        public void ConfigureValidations() {
            if(_entity != null)
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.Notes.Length > 300 ? new ValidationResult("Notes", string.Format(Strings.Notification_Field_X_MaxLength, 300)) : null);
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
            fields.AddRange(_entity.GetValidations("Notes"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

        public class LocalPerson : Person {}
    }
}