#region Usings

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private CultureInfo Culture {
            get { return Thread.CurrentThread.CurrentCulture; }
        }

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
                CPF = 0,
                RG = 0,
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
                _entity.AddValidation(
                    (sender, name) => string.IsNullOrWhiteSpace(_entity.FirstName) ? new ValidationResult("Name", Strings.Error_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.LastName) ? new ValidationResult("Description", Strings.Error_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.CPF.ToString(culture).Length < 1 ? new ValidationResult("CPF", Strings.Error_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.CPF.ToString(culture).Length > 11 ? new ValidationResult("CPF", Strings.Error_Field_TooLong) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.RG.ToString(culture).Length < 1 ? new ValidationResult("RG", Strings.Error_Field_Empty) : null);
                _entity.AddValidation(
                    (sender, name) => _entity.RG.ToString(culture).Length > 9 ? new ValidationResult("RG", Strings.Error_Field_TooLong) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.BirthDate.CompareTo(new DateTime(1910, 1, 1)) < 0
                        ? new ValidationResult("DeliverDate", Strings.Error_Field_DateTooEarly)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.BirthDate.CompareTo(new DateTime(2014, 1, 1)) > 0
                        ? new ValidationResult("DeliverDate", Strings.Error_Field_DateTooEarly)
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