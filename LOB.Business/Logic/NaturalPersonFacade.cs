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
            _baseEntityFacade = baseEntityFacade;
            _personFacade = personFacade;
        }

        public void SetEntity<T>(T entity) where T : NaturalPerson {
            _baseEntityFacade.SetEntity(entity);
            _entity = entity;
        }

        public void ConfigureValidations() {
            _baseEntityFacade.ConfigureValidations();
            _personFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                                      (sender, name) =>
                                      _entity.FirstName.Length < 1
                                          ? new ValidationResult("Name", Strings.Error_Field_Empty)
                                          : null);
                _entity.AddValidation(
                                      (sender, name) =>
                                      _entity.LastName.Length < 1
                                          ? new ValidationResult("Description", Strings.Error_Field_Empty)
                                          : null);
                _entity.AddValidation(
                                      (sender, name) =>
                                      _entity.Cpf.ToString().Length < 1
                                          ? new ValidationResult("Cpf", Strings.Error_Field_Empty)
                                          : null);
                _entity.AddValidation(
                                      (sender, name) =>
                                      _entity.Rg.ToString().Length < 1
                                          ? new ValidationResult("Rg", Strings.Error_Field_Empty)
                                          : null);
                _entity.AddValidation(
                                      (sender, name) =>
                                      _entity.BirthDate.ToShortDateString().ToString().Length < 1
                                          ? new ValidationResult("BirthDate", Strings.Error_Field_Empty)
                                          : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
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
            _baseEntityFacade.SetEntity(entity);
        }

        void IPersonFacade.SetEntity<T>(T entity) {
            _personFacade.SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("Number"));
            fields.AddRange(_entity.GetValidations("Description"));
            fields.AddRange(_entity.GetValidations("Cpf"));
            fields.AddRange(_entity.GetValidations("Rg"));
            fields.AddRange(_entity.GetValidations("BirthDate"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}