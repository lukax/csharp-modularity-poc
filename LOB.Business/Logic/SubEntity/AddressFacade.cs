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
    public class AddressFacade : IAddressFacade {
        private Address _entity;

        public static Address GenerateEntity() {
            return new Address {
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
            };
        }

        public Address Entity {
            set {
                _entity = value;
                if(!ReferenceEquals(value, null)) ConfigureValidations();
            }
        }

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.Street) ? new ValidationResult("Street", Strings.Notification_Field_Empty) : null);
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.StreetNumber)) return new ValidationResult("StreetNumber", Strings.Notification_Field_Empty);
                                          if(!Regex.IsMatch(_entity.StreetNumber, @"\d")) return new ValidationResult("StreetNumber", Strings.Notification_Field_IntOnly);

                                          return null;
                                      });
                _entity.AddValidation(delegate {
                                          if(string.IsNullOrWhiteSpace(_entity.ZipCode)) return new ValidationResult("ZipCode", Strings.Notification_Field_Empty);
                                          if(Regex.IsMatch(_entity.ZipCode, @"^\d{5}-\d{3}$"))
                                              return new ValidationResult("ZipCode",
                                                                          string.Format(Strings.Notification_Field_X_Invalid, Strings.Common_ZipCode));
                                          return null;
                                      });
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.County) ? new ValidationResult("County", Strings.Notification_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.District) ? new ValidationResult("District", Strings.Notification_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.State) ? new ValidationResult("State", Strings.Notification_Field_Empty) : null);
            }
        }

        Address IBaseEntityFacade<Address>.GenerateEntity() { return GenerateEntity(); }
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
            fields.AddRange(_entity.GetValidations("Street"));
            fields.AddRange(_entity.GetValidations("StreetNumber"));
            fields.AddRange(_entity.GetValidations("ZipCode"));
            fields.AddRange(_entity.GetValidations("County"));
            fields.AddRange(_entity.GetValidations("District"));
            fields.AddRange(_entity.GetValidations("State"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }
    }
}