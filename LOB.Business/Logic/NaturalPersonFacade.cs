#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LOB.Business.Interface.Logic;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic {
    public class NaturalPersonFacade : INaturalPersonFacade {
        private NaturalPerson _entity;
        public NaturalPerson Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }
        public NaturalPerson GenerateEntity() {
            return new NaturalPerson {
                FirstName = "",
                LastName = "",
                NickName = "",
                BirthDate = DateTime.Now,
                CPF = "",
                RG = "",
                RGUF = "",
                Code = 0,
                Error = null,
                Address =
                    new Address {
                        Code = 0,
                        County = "",
                        Country = "Brasil",
                        District = "",
                        Error = null,
                        IsDefault = false,
                        State = "Rio de Janeiro",
                        Status = default(AddressStatus),
                        Street = "",
                        StreetComplement = "",
                        StreetNumber = "",
                        ZipCode = "",
                    },
                ContactInfo =
                    new ContactInfo {
                        Code = 0,
                        Description = "",
                        Error = null,
                        Status = default(ContactStatus),
                        PS = "",
                        Emails = new List<Email>(),
                        PhoneNumbers = new List<PhoneNumber>(),
                        SpeakWith = "",
                        WebSite = "http://",
                    },
                Notes = "",
            };
        }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.FirstName)) return new ValidationResult("FirstName", Strings.Notification_Field_Empty);
                                          if(
                                              !Regex.IsMatch(_entity.FirstName,
                                                             @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))
                                              return new ValidationResult("FirstName",
                                                                          string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_FirstName));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.LastName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                                          if(
                                              !Regex.IsMatch(_entity.LastName,
                                                             @"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))
                                              return new ValidationResult("LastName",
                                                                          string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_LastName));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.CPF)) return new ValidationResult("CPF", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.CPF, @"^\d{3}.\d{3}.\d{3}-\d{2}$")) return new ValidationResult("CPF", string.Format(Strings.Notification_Field_X_Invalid, "CPF"));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.RG)) return new ValidationResult("RG", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.RG, @"^\d{2}.\d{3}.\d{3}-\d{1}$")) return new ValidationResult("RG", string.Format(Strings.Notification_Field_X_Invalid, "RG"));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(_entity.BirthDate.CompareTo(new DateTime(1910, 1, 1)) < 0) return new ValidationResult("DeliverDate", Strings.Notification_Field_DateTooEarly);
                                          if(_entity.BirthDate.CompareTo(new DateTime(2014, 1, 1)) > 0) return new ValidationResult("DeliverDate", Strings.Notification_Field_DateTooEarly);
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
            fields.AddRange(_entity.GetValidations("Number"));
            fields.AddRange(_entity.GetValidations("Description"));
            fields.AddRange(_entity.GetValidations("CPF"));
            fields.AddRange(_entity.GetValidations("RG"));
            fields.AddRange(_entity.GetValidations("BirthDate"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }
    }
}