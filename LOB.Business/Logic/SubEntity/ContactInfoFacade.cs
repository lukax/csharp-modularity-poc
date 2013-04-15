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
    public class ContactInfoFacade : IContactInfoFacade {
        private ContactInfo _entity;
        public ContactInfo Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public ContactInfo GenerateEntity() {
            return new ContactInfo {
                Code = 0,
                Description = "",
                Error = null,
                Status = default(ContactStatus),
                PS = "",
                Emails = new List<Email>(),
                PhoneNumbers = new List<PhoneNumber>(),
                SpeakWith = "",
                WebSite = "http://",
            };
        }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.Description) ? new ValidationResult("Description", Strings.Notification_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    !Regex.IsMatch(_entity.WebSite,
                                   @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$")
                        ? new ValidationResult("WebSite", Strings.Notification_Field_WrongFormat)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.SpeakWith.Length > 300
                        ? new ValidationResult("SpeakWith", string.Format(Strings.Notification_Field_X_MaxLength, 300))
                        : null);
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
            fields.AddRange(_entity.GetValidations("WebSite"));
            fields.AddRange(_entity.GetValidations("Description"));
            fields.AddRange(_entity.GetValidations("SpeakWith"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }
    }
}