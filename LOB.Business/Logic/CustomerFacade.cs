#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class CustomerFacade : ICustomerFacade {

        private readonly ILegalPersonFacade _legalPersonFacade;
        private readonly INaturalPersonFacade _naturalPersonFacade;
        private Customer _entity;
        private PersonType PersonType {
            get {
                if(_entity != null) return _entity.PersonType;
                return default(PersonType);
            }
        }

        public CustomerFacade(INaturalPersonFacade naturalPersonFacade, ILegalPersonFacade legalPersonFacade) {
            _naturalPersonFacade = naturalPersonFacade;
            _legalPersonFacade = legalPersonFacade;
        }

        public void SetEntity<T>(T entity) where T : Customer {
            if(entity.PersonType == PersonType.Legal) _legalPersonFacade.SetEntity(entity.Person as LegalPerson);
            if(entity.PersonType == PersonType.Natural) _naturalPersonFacade.SetEntity(entity.Person as NaturalPerson);
            _entity = entity;
        }

        public Customer GenerateEntity() {
            var localPerson = ((IPersonFacade)this).GenerateEntity();
            return new Customer {
                Code = 0,
                Error = null,
                BoughtHistory = new List<Sale>(),
                Status = default(CustomerStatus),
                CustomerOf = new List<Store>(),
                Person = localPerson,
                PersonType = default(PersonType),
            };
        }

        Person IPersonFacade.GenerateEntity() {
            if(PersonType == PersonType.Natural) return _naturalPersonFacade.GenerateEntity();
            if(PersonType == PersonType.Legal) return _legalPersonFacade.GenerateEntity();
            return null;
        }

        public void ConfigureValidations() {
            //if(_entity != null)
            //    _entity.AddValidation(
            //        (sender, name) => _entity.CustomerOf.Count < 1 ? new ValidationResult("CustomerOf", Strings.Notification_Field_Empty) : null);
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

        void IBaseEntityFacade.SetEntity<T>(T entity) {
            ((IBaseEntityFacade)_naturalPersonFacade).SetEntity(entity);
            ((IBaseEntityFacade)_legalPersonFacade).SetEntity(entity);
        }

        void IPersonFacade.SetEntity<T>(T entity) {
            ((IPersonFacade)_naturalPersonFacade).SetEntity(entity);
            ((IPersonFacade)_legalPersonFacade).SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("CustomerOf"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }

    }
}