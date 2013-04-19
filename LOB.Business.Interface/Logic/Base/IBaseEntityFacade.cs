#region Usings

using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Interface.Logic.Base {
    public interface IBaseEntityFacade {
        bool CanAdd(out IEnumerable<ValidationResult> invalidFields);
        bool CanUpdate(out IEnumerable<ValidationResult> invalidFields);
        bool CanDelete(out IEnumerable<ValidationResult> invalidFields);
    }

    public interface IBaseEntityFacade<TEntity> : IBaseEntityFacade where TEntity : BaseEntity {
        TEntity Entity { set; }
        TEntity GenerateEntity();
    }
}