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
    public class NaturalPersonFacade : INaturalPersonFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private readonly IPersonFacade _personFacade;
        private NaturalPerson _entity;

        public NaturalPersonFacade(IBaseEntityFacade baseEntityFacade, IPersonFacade personFacade) {
            this._baseEntityFacade = baseEntityFacade;
            this._personFacade = personFacade;
        }

        public void SetEntity<T>(T entity) where T : NaturalPerson {
            this._baseEntityFacade.SetEntity(entity);
            this._entity = entity;
        }

        public void ConfigureValidations() {
            this._baseEntityFacade.ConfigureValidations();
            this._personFacade.ConfigureValidations();
            if(this._entity != null) {
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.FirstName.Length < 1
                                               ? new ValidationResult("Name", Strings.Error_Field_Empty)
                                               : null);
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.LastName.Length < 1
                                               ? new ValidationResult("Description", Strings.Error_Field_Empty)
                                               : null);
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.Cpf.ToString().Length < 1
                                               ? new ValidationResult("Cpf", Strings.Error_Field_Empty)
                                               : null);
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.Rg.ToString().Length < 1
                                               ? new ValidationResult("Rg", Strings.Error_Field_Empty)
                                               : null);
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.BirthDate.ToShortDateString().ToString().Length < 1
                                               ? new ValidationResult("BirthDate", Strings.Error_Field_Empty)
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
            fields.AddRange(this._entity.GetValidations("Number"));
            fields.AddRange(this._entity.GetValidations("Description"));
            fields.AddRange(this._entity.GetValidations("Cpf"));
            fields.AddRange(this._entity.GetValidations("Rg"));
            fields.AddRange(this._entity.GetValidations("BirthDate"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}