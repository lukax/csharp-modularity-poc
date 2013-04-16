#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class EmployeeFacade : IEmployeeFacade {
        private Employee _entity;
        public Employee Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public static Employee GenerateEntity() {
            return new Employee {
                Code = 0,
                Error = null,
                Address = AddressFacade.GenerateEntity(),
                ContactInfo = ContactInfoFacade.GenerateEntity(),
                Notes = "",
                BirthDate = DateTime.Now,
                CPF = "",
                FirstName = "",
                LastName = "",
                HireDate = DateTime.Now,
                NickName = "",
                Password = "",
                PayCheck = PayCheckFacade.GenerateEntity(),
                RG = "",
                RGUF = "",
                Title = "",
                WorksIn = new Store(), //TODO
            };
        }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.Title) ? new ValidationResult("Title", Strings.Notification_Field_Empty) : null);
                _entity.AddValidation(delegate {
                                          if(_entity.HireDate.CompareTo(new DateTime(1990, 1, 1)) < 0) return new ValidationResult("HireDate", Strings.Notification_Field_DateTooEarly);
                                          if(_entity.HireDate.CompareTo(new DateTime(2015, 1, 1)) > 0) return new ValidationResult("HireDate", Strings.Notification_Field_DateTooLate);
                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(_entity.BirthDate.CompareTo(new DateTime(1900, 1, 1)) < 0) return new ValidationResult("BirthDate", Strings.Notification_Field_DateTooEarly);
                                          if(_entity.BirthDate.CompareTo(new DateTime(2013, 1, 1)) > 0) return new ValidationResult("BirthDate", Strings.Notification_Field_DateTooLate);
                                          return null;
                                      });
            }
        }

        Employee IBaseEntityFacade<Employee>.GenerateEntity() { return GenerateEntity(); }
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
            fields.AddRange(_entity.GetValidations("Title"));
            fields.AddRange(_entity.GetValidations("HireDate"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }
    }
}