#region Usings

using System.Collections.Generic;
using System.Linq;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;

#endregion

namespace LOB.Business.Logic.SubEntity {
    public class CategoryFacade : ICategoryFacade {
        private Category _entity;

        public void ConfigureValidations() {
            if(_entity != null) {
                _entity.AddValidation(
                    (sender, name) => string.IsNullOrWhiteSpace(_entity.Name) ? new ValidationResult("Name", Strings.Common_Name) : null);
                _entity.AddValidation(
                    (sender, name) =>
                    string.IsNullOrWhiteSpace(_entity.Description) ? new ValidationResult("Description", Strings.Common_Description) : null);
            }
        }

        public Category Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public static Category GenerateEntity() { return new Category {Code = 0, Description = "", Error = null, Name = "",}; }

        Category IBaseEntityFacade<Category>.GenerateEntity() { return GenerateEntity(); }
        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) { return ProcessBasicValidations(out invalidFields); }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) { return ProcessBasicValidations(out invalidFields); }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) { return ProcessBasicValidations(out invalidFields); }

        private bool ProcessBasicValidations(out IEnumerable<ValidationResult> invalidFields) {
            var fields = new List<ValidationResult>();
            fields.AddRange(_entity.GetValidations("Name"));
            fields.AddRange(_entity.GetValidations("Description"));
            invalidFields = fields;
            if(
                fields.Where(validationResult => validationResult != null)
                      .Count(validationResult => !string.IsNullOrEmpty(validationResult.ErrorDescription)) > 0) return false;
            return true;
        }
    }
}