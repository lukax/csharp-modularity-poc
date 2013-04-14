#region Usings

using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class NaturalPersonFacade : INaturalPersonFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private readonly IPersonFacade _personFacade;
        private NaturalPerson _entity;
        private CultureInfo Culture { get { return Thread.CurrentThread.CurrentCulture; } }

        public NaturalPersonFacade(IBaseEntityFacade baseEntityFacade, IPersonFacade personFacade) {
            _baseEntityFacade = baseEntityFacade;
            _personFacade = personFacade;
        }

        public void SetEntity<T>(T entity) where T : NaturalPerson {
            _baseEntityFacade.SetEntity(entity);
            _entity = entity;
        }

        public NaturalPerson GenerateEntity() {
            var localPerson = _personFacade.GenerateEntity();
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
                Address = localPerson.Address,
                ContactInfo = localPerson.ContactInfo,
                Notes = "",
            };
        }

        Person IPersonFacade.GenerateEntity() { return GenerateEntity(); }

        public void ConfigureValidations() {
            var culture = Thread.CurrentThread.CurrentCulture;
            _baseEntityFacade.ConfigureValidations();
            _personFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.FirstName)) return new ValidationResult("FirstName", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.FirstName,@"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))return new ValidationResult("FirstName",string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_FirstName));return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.LastName)) return new ValidationResult("LastName", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.LastName,@"^([\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+((\s[\'\.\^\~\´\`\\áÁ\\àÀ\\ãÃ\\âÂ\\éÉ\\èÈ\\êÊ\\íÍ\\ìÌ\\óÓ\\òÒ\\õÕ\\ôÔ\\úÚ\\ùÙ\\çÇaA-zZ]+)+)?$"))return new ValidationResult("LastName",string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_LastName));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.CPF)) return new ValidationResult("CPF", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.CPF, @"^\d{3}.\d{3}.\d{3}-\d{2}$")) return new ValidationResult("CPF", string.Format(Strings.Notification_Field_X_Invalid, "CPF"));
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.RG)) return new ValidationResult("RG", Strings.Notification_Field_Empty);
                                          if (!Regex.IsMatch(_entity.RG, @"^\d{2}.\d{3}.\d{3}-\d{1}$")) return new ValidationResult("RG", string.Format(Strings.Notification_Field_X_Invalid, "RG"));
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