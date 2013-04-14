#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class LegalPersonFacade : ILegalPersonFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private readonly IPersonFacade _personFacade;
        private LegalPerson _entity;

        public LegalPersonFacade(IBaseEntityFacade baseEntityFacade, IPersonFacade personFacade) {
            _baseEntityFacade = baseEntityFacade;
            _personFacade = personFacade;
        }

        public void SetEntity<T>(T entity) where T : LegalPerson {
            _baseEntityFacade.SetEntity(entity);
            _entity = entity;
        }

        public LegalPerson GenerateEntity() {
            var localPerson = _personFacade.GenerateEntity();
            return new LegalPerson {
                Code = 0,
                Error = null,
                Address = localPerson.Address,
                ContactInfo = localPerson.ContactInfo,
                Notes = "",
                CNAEFiscal = "",
                CNPJ = "",
                CorporateName = "",
                InscEstadual = "",
                InscMunicipal = "",
                TradingName = "",
            };
        }

        Person IPersonFacade.GenerateEntity() { return GenerateEntity(); }

        public void ConfigureValidations() {
            var culture = Thread.CurrentThread.CurrentCulture;
            _baseEntityFacade.ConfigureValidations();
            _personFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.CorporateName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.CorporateName, @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$")) return new ValidationResult("CorporateName", string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_CorporateName));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.TradingName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.TradingName, @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$")) return new ValidationResult("TradingName", string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_TradingName));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.CNPJ)) return new ValidationResult("CNPJ", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.CNPJ, @"^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$")) return new ValidationResult("CNPJ",string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_Cnpj));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.CNAEFiscal)) return new ValidationResult("CNAEFiscal", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.CNAEFiscal, @"^\d{4}-\d{1}/\d{2}$"))return new ValidationResult("CNAEFiscal",string.Format(Strings.Notification_Field_X_Invalid,Strings.Common_CnaeFiscal));
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

        void IPersonFacade.SetEntity<T>(T entity) { _personFacade.SetEntity(entity); }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("CorporateName"));
            fields.AddRange(_entity.GetValidations("TradingName"));
            fields.AddRange(_entity.GetValidations("CNPJ"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}