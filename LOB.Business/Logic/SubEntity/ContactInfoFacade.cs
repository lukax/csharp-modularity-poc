#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class ContactInfoFacade : IContactInfoFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private ContactInfo _entity;

        public ContactInfoFacade(IBaseEntityFacade baseEntityFacade) { _baseEntityFacade = baseEntityFacade; }

        public void SetEntity<T>(T entity) where T : ContactInfo {
            _baseEntityFacade.SetEntity(entity);
            _entity = entity;
        }

        public ContactInfo GenerateEntity() {
            return new ContactInfo {
                Code = 0,
                Error = null,
                Status = default(ContactStatus),
                PS = "",
                Emails = new List<Email>(),
                PhoneNumbers = new List<PhoneNumber>(),
                SpeakWith = "",
                WebSite = "",
            };
        }

        public void ConfigureValidations() {
            _baseEntityFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) => _entity.WebSite.Length > 300 ? new ValidationResult("WebSite", Strings.Error_Field_TooLong) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.Description) ? new ValidationResult("Description", Strings.Error_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.SpeakWith.Length > 300 ? new ValidationResult("SpeakWith", Strings.Error_Field_TooLong) : null);
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

        void IBaseEntityFacade.SetEntity<T>(T entity) { _baseEntityFacade.SetEntity(entity); }

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