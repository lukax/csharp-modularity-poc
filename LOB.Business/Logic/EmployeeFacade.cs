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
            this._baseEntityFacade = baseEntityFacade;
            this._naturalPersonFacade = naturalPersonFacade;
        }

        void INaturalPersonFacade.SetEntity<T>(T entity) {
            this._naturalPersonFacade.SetEntity(entity);
        }

        public void SetEntity<T>(T entity) where T : Employee {
            this._baseEntityFacade.SetEntity(entity);
            this._naturalPersonFacade.SetEntity(entity);
            this._entity = entity;
        }

        public void ConfigureValidations() {
            this._baseEntityFacade.ConfigureValidations();
            this._naturalPersonFacade.ConfigureValidations();
            if(this._entity != null) {
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.Title.Length < 1
                                               ? new ValidationResult("Title", Strings.Error_Field_Empty)
                                               : null);
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.HireDate.ToShortDateString().ToString().Length < 1
                                               ? new ValidationResult("HireDate", Strings.Error_Field_Empty)
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
            ((IPersonFacade) this._naturalPersonFacade).SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(this._entity.GetValidations("Title"));
            fields.AddRange(this._entity.GetValidations("HireDate"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}