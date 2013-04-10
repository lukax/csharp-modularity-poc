#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class EmailFacade : IEmailFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private Email _entity;

        public EmailFacade(IBaseEntityFacade baseEntityFacade) { _baseEntityFacade = baseEntityFacade; }

        public void SetEntity<T>(T entity) where T : Email { _entity = entity; }

        public Email GenerateEntity() { return new Email {Code = 0, Error = null, Value = "",}; }

        public void ConfigureValidations() {
            _baseEntityFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.Value)) return new ValidationResult("Value", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.Value, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")) return new ValidationResult("Value", Strings.Notification_Field_WrongFormat);
                                          return null;
                                      });
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) { _baseEntityFacade.SetEntity(entity); }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("Value"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}