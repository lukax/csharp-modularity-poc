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
    public class CustomerFacade : ICustomerFacade {

        private readonly ILegalPersonFacade _legalPersonFacade;
        private readonly INaturalPersonFacade _naturalPersonFacade;
        private Customer _entity;
        private PersonType _personType;

        public CustomerFacade(INaturalPersonFacade naturalPersonFacade, ILegalPersonFacade legalPersonFacade) {
            this._naturalPersonFacade = naturalPersonFacade;
            this._legalPersonFacade = legalPersonFacade;
        }

        public void SetEntity<T>(T entity) where T : Customer {
            if(entity.PersonType == PersonType.Legal) this._legalPersonFacade.SetEntity(entity.Person as LegalPerson);
            if(entity.PersonType == PersonType.Natural) this._naturalPersonFacade.SetEntity(entity.Person as NaturalPerson);
            this._entity = entity;
        }

        public void ConfigureValidations() {
            this._legalPersonFacade.ConfigureValidations();
            this._naturalPersonFacade.ConfigureValidations();
            if(this._entity != null)
                this._entity.AddValidation(
                                           (sender, name) =>
                                           this._entity.CustomerOf.Count < 1
                                               ? new ValidationResult("CustomerOf", Strings.Error_Field_Empty)
                                               : null);
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
            ((IBaseEntityFacade) this._naturalPersonFacade).SetEntity(entity);
            ((IBaseEntityFacade) this._legalPersonFacade).SetEntity(entity);
        }

        void IPersonFacade.SetEntity<T>(T entity) {
            ((IPersonFacade) this._naturalPersonFacade).SetEntity(entity);
            ((IPersonFacade) this._legalPersonFacade).SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(this._entity.GetValidations("CustomerOf"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}