#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LOB.Business.Interface.Logic;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class LegalPersonFacade : ILegalPersonFacade {
        private LegalPerson _entity;
        public LegalPerson Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public LegalPerson GenerateEntity() {
            return new LegalPerson {
                Code = 0,
                Error = null,
                //Address = localPerson.Address,
                //ContactInfo = localPerson.ContactInfo,
                Notes = "",
                CNAEFiscal = "",
                CNPJ = "",
                CorporateName = "",
                InscEstadual = "",
                InscMunicipal = "",
                TradingName = "",
            };
        }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.CorporateName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                                          if(
                                              !Regex.IsMatch(_entity.CorporateName,
                                                             @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))
                                              return new ValidationResult("CorporateName",
                                                                          string.Format(Strings.Notification_Field_X_Invalid,
                                                                                        Strings.Common_CorporateName));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.TradingName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                                          if(
                                              !Regex.IsMatch(_entity.TradingName,
                                                             @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))
                                              return new ValidationResult("TradingName",
                                                                          string.Format(Strings.Notification_Field_X_Invalid,
                                                                                        Strings.Common_TradingName));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.CNPJ)) return new ValidationResult("CNPJ", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.CNPJ, @"^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$"))
                                              return new ValidationResult("CNPJ",
                                                                          string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_Cnpj));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.CNAEFiscal)) return new ValidationResult("CNAEFiscal", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.CNAEFiscal, @"^\d{4}-\d{1}/\d{2}$"))
                                              return new ValidationResult("CNAEFiscal",
                                                                          string.Format(Strings.Notification_Field_X_Invalid,
                                                                                        Strings.Common_CnaeFiscal));
                                          return null;
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