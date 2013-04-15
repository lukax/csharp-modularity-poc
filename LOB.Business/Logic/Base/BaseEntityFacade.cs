#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic.Base {
    public abstract class BaseEntityFacade : IBaseEntityFacade<BaseEntity> {
        private BaseEntity _entity;

        public void ConfigureValidations() { if(_entity != null) _entity.AddValidation((sender, name) => _entity.Code < 0 ? new ValidationResult("Code", Strings.Notification_Field_WrongFormat) : null); }

        public BaseEntity Entity {
            set {
                _entity = value;
                ConfigureValidations();
            }
        }

        public BaseEntity GenerateEntity() { return null; }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields) {
            bool result = true;
            IList<ValidationResult> results = new List<ValidationResult>();
            if(_entity.Code != default(int)) {
                result = false;
                results.Add(new ValidationResult("Code", Strings.Notification_Field_NotEmpty));
            }
            if(_entity.Id != default(Guid)) {
                result = false;
                results.Add(new ValidationResult("Id", Strings.Notification_Field_NotEmpty));
            }
            invalidFields = results;
            return result;
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields) {
            bool result = true;
            IList<ValidationResult> results = new List<ValidationResult>();
            if(_entity.Code == default(int)) {
                result = false;
                results.Add(new ValidationResult("Code", Strings.Notification_Field_Empty));
            }
            if(_entity.Id == default(Guid)) {
                result = false;
                results.Add(new ValidationResult("Id", Strings.Notification_Field_Empty));
            }
            invalidFields = results;
            return result;
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields) { return CanUpdate(out invalidFields); }
    }
}