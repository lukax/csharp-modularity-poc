#region Usings

using System;
using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Core.Localization;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Logic.Base
{
    public class BaseEntityFacade : IBaseEntityFacade
    {
        private BaseEntity _entity;

        public BaseEntityFacade()
        {
        }

        public void SetEntity<T>(T entity) where T : BaseEntity
        {
            _entity = entity;
        }

        public void ConfigureValidations()
        {
            if (_entity != null)
            {
                _entity.AddValidation((sender, name) => _entity.Code < 0 ? new ValidationResult("Code", Strings.Error_Field_WrongFormat) : null);
            }
        }

        public bool CanAdd(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new NotImplementedException();
        }

        public bool CanUpdate(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new NotImplementedException();
        }

        public bool CanDelete(out IEnumerable<ValidationResult> invalidFields)
        {
            throw new NotImplementedException();
        }
    }
}