#region Usings

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class AddressFacade : IAddressFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private Address _entity;
        private CultureInfo Culture {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

        public AddressFacade(IBaseEntityFacade baseEntityFacade) { _baseEntityFacade = baseEntityFacade; }
       
        public void SetEntity<T>(T entity) where T : Address {
            _baseEntityFacade.SetEntity(entity);
            _entity = entity;
        }

        public Address GenerateEntity() {
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

        public void ConfigureValidations() {
            _baseEntityFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.Street)
                        ? new ValidationResult("Street", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.StreetNumber)
                        ? new ValidationResult("StreetNumber", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.ZipCode)
                        ? new ValidationResult("ZipCode", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.County)
                        ? new ValidationResult("County", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.District)
                        ? new ValidationResult("District", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.State)
                        ? new ValidationResult("State", Strings.Error_Field_Empty)
                        : null);
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
            fields.AddRange(_entity.GetValidations("Street"));
            fields.AddRange(_entity.GetValidations("StreetNumber"));
            fields.AddRange(_entity.GetValidations("ZipCode"));
            fields.AddRange(_entity.GetValidations("County"));
            fields.AddRange(_entity.GetValidations("District"));
            fields.AddRange(_entity.GetValidations("State"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(
                          validationResult =>
                          !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}