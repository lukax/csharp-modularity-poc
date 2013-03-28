#region Usings

using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Interface.Logic.Base
{
    public interface IBaseEntityFacade
    {
        void SetEntity<T>(T entity) where T : BaseEntity;
        void ConfigureValidations();
        bool CanAdd(out IEnumerable<ValidationResult> invalidFields);
        bool CanUpdate(out IEnumerable<ValidationResult> invalidFields);
        bool CanDelete(out IEnumerable<ValidationResult> invalidFields);
    }

    internal interface IBaseEntityFacade<TEntity> where TEntity : BaseEntity
    {
    }
}