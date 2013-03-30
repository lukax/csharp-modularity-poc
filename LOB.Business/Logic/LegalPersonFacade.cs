#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class LegalPersonFacade : ILegalPersonFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private readonly IPersonFacade _personFacade;
        private LegalPerson _entity;

        public LegalPersonFacade(IBaseEntityFacade baseEntityFacade, IPersonFacade personFacade) {
            this._baseEntityFacade = baseEntityFacade;
            this._personFacade = personFacade;
        }

        public void SetEntity<T>(T entity) where T : LegalPerson {
            this._baseEntityFacade.SetEntity(entity);
            this._entity = entity;
        }

        public void ConfigureValidations() {
            this._baseEntityFacade.ConfigureValidations();
            this._personFacade.ConfigureValidations();
            if(this._entity != null) {
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.CorporateName.Length < 1
                                               ? new ValidationResult("CorporateName", Strings.Error_Field_Empty)
                                               : null);
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.TradingName.Length < 1
                                               ? new ValidationResult("TradingName", Strings.Error_Field_Empty)
                                               : null);
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.Cnpj.ToString().Length < 1
                                               ? new ValidationResult("Cnpj", Strings.Error_Field_Empty)
                                               : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = this.ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            throw new NotImplementedException();
        }

        void IBaseEntityFacade.SetEntity<T>(T entity) {
            this._baseEntityFacade.SetEntity(entity);
        }

        void IPersonFacade.SetEntity<T>(T entity) {
            this._personFacade.SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(this._entity.GetValidations("CorporateName"));
            fields.AddRange(this._entity.GetValidations("TradingName"));
            fields.AddRange(this._entity.GetValidations("Cnpj"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}