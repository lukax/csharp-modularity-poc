#region Usings



#endregion

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.Domain.Logic;

namespace LOB.Business.Logic.Base
{
    public class ServiceFacade : IServiceFacade
    {
        private readonly IBaseEntityFacade _baseEntityFacade;
        private Service _entity;

        public ServiceFacade(IBaseEntityFacade baseEntityFacade)
        {
            _baseEntityFacade = baseEntityFacade;
        }

        public void SetEntity<T>(T entity) where T : Service
        {
            _baseEntityFacade.SetEntity(entity);
            _entity = entity;
        }

        public void ConfigureValidations()
        {
            _baseEntityFacade.ConfigureValidations();
            if (_entity != null)
            {
                _entity.AddValidation((sender, name) => _entity.Name.Length < 1 ? new ValidationResult("Name", Strings.Error_Field_Empty) : null);
                _entity.AddValidation((sender, name) => _entity.Description.Length < 1 ? new ValidationResult("Description", Strings.Error_Field_Empty) : null);
            }
        }

        private bool BasicValidations(out IEnumerable<ValidationResult> invalidFields)
        {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("Name"));
            fields.AddRange(_entity.GetValidations("Description"));
            invalidFields = fields;
            int i = 0;
            foreach (var validationResult in fields)
            {
                if (validationResult != null)
                    if (!string.IsNullOrEmpty(validationResult.ErrorDescription))
                        i++;
            }
            if (i > 0)
                return false;
            return true;
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields)
        {
            bool result = BasicValidations(out invalidFields);
            //TODO: Repository validations here
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new System.NotImplementedException();
        }

        void IBaseEntityFacade.SetEntity<T>(T entity)
        {
            _baseEntityFacade.SetEntity(entity);
        }
    }
}