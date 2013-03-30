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
    public class EmployeeFacade : IEmployeeFacade {

        private readonly IBaseEntityFacade _baseEntityFacade;
        private readonly INaturalPersonFacade _naturalPersonFacade;
        private Employee _entity;

        public EmployeeFacade(IBaseEntityFacade baseEntityFacade, INaturalPersonFacade naturalPersonFacade) {
            _baseEntityFacade = baseEntityFacade;
            _naturalPersonFacade = naturalPersonFacade;
        }

        void INaturalPersonFacade.SetEntity<T>(T entity) {
            _naturalPersonFacade.SetEntity(entity);
        }

        public void SetEntity<T>(T entity) where T : Employee {
            _baseEntityFacade.SetEntity(entity);
            _naturalPersonFacade.SetEntity(entity);
            _entity = entity;
        }

        public void ConfigureValidations() {
            _baseEntityFacade.ConfigureValidations();
            _naturalPersonFacade.ConfigureValidations();
            if(_entity != null) {
                _entity.AddValidation(
                                      (sender, name) =>
                                      _entity.Title.Length < 1
                                          ? new ValidationResult("Title", Strings.Error_Field_Empty)
                                          : null);
                _entity.AddValidation(
                                      (sender, name) =>
                                      _entity.HireDate.ToShortDateString().ToString().Length < 1
                                          ? new ValidationResult("HireDate", Strings.Error_Field_Empty)
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
            ((IPersonFacade) _naturalPersonFacade).SetEntity(entity);
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