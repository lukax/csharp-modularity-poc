#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic.Base
{
    public class PersonFacade : IPersonFacade
    {
        private readonly IAddressFacade _addressFacade;
        private readonly IBaseEntityFacade _baseEntityFacade;
        private readonly IContactInfoFacade _contactInfoFacade;
        private Person _entity;

        public PersonFacade(IBaseEntityFacade baseEntityFacade, IAddressFacade addressFacade,
                            IContactInfoFacade contactInfoFacade)
        {
            _baseEntityFacade = baseEntityFacade;
            _addressFacade = addressFacade;
            _contactInfoFacade = contactInfoFacade;
        }

        public void SetEntity<T>(T entity) where T : Person
        {
            _baseEntityFacade.SetEntity(entity);
            _addressFacade.SetEntity(entity.Address);
            _contactInfoFacade.SetEntity(entity.ContactInfo);
            _entity = entity;
        }

        public void ConfigureValidations()
        {
            _baseEntityFacade.ConfigureValidations();
            _addressFacade.ConfigureValidations();
            _contactInfoFacade.ConfigureValidations();
            if (_entity != null)
            {
                _entity.AddValidation((sender, name) => _entity.Notes.Length > 300
                                                            ? new ValidationResult("Notes", Strings.Error_Field_TooLong)
                                                            : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields)
        {
            bool result = ProcessBasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new NotImplementedException();
        }

        void IBaseEntityFacade.SetEntity<T>(T entity)
        {
            _baseEntityFacade.SetEntity(entity);
        }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields)
        {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("Notes"));
            invalidFields = fields;
            if (
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0)
                return false;
            return true;
        }
    }
}