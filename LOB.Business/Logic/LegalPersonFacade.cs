#region Usings

using System.Collections.Generic;
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
                CNAEFiscal = 0,
                CNPJ = 0,
                CorporateName = "",
                InscEstadual = 0,
                InscMunicipal = 0,
                TradingName = "",
            };
        }

        Person IPersonFacade.GenerateEntity() { return GenerateEntity(); }

        public void ConfigureValidations() {
            var culture = Thread.CurrentThread.CurrentCulture;
            _baseEntityFacade.ConfigureValidations();
            _personFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.CorporateName.Length < 1
                        ? new ValidationResult("CorporateName", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.TradingName.Length < 1
                        ? new ValidationResult("TradingName", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.CNPJ.ToString(culture).Length < 1
                        ? new ValidationResult("CNPJ", Strings.Error_Field_Empty)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.CNPJ.ToString(culture).Length > 14
                        ? new ValidationResult("CNPJ", Strings.Error_Field_TooLong)
                        : null);
                _entity.AddValidation(
                    (sender, name) =>
                    _entity.CNAEFiscal.ToString(culture).Length > 7
                        ? new ValidationResult("CNAEFiscal", Strings.Error_Field_TooLong)
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
            fields.AddRange(_entity.GetValidations("CorporateName"));
            fields.AddRange(_entity.GetValidations("TradingName"));
            fields.AddRange(_entity.GetValidations("CNPJ"));
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