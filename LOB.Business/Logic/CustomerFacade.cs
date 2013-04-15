#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic;
using LOB.Domain;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic {
    public class CustomerFacade : ICustomerFacade {
        private Customer _entity;
        public Customer Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public Customer GenerateEntity() {
            return new Customer {
                Code = 0,
                Error = null,
                BoughtHistory = new List<Sale>(),
                Status = default(CustomerStatus),
                CustomerOf = new List<Store>(),
                Person = null,
                PersonType = default(PersonType) //default(PersonType),
            };
        }

        public void ConfigureValidations() {
            //if(_entity != null)
            //    _entity.AddValidation(
            //        (sender, name) => _entity.CustomerOf.Count < 1 ? new ValidationResult("CustomerOf", Strings.Notification_Field_Empty) : null);
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) {
            bool result = ProcessBasicValidations(out invalidFields);
            return result;
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