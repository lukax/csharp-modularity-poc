#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Contract.Logic.Base {
    public interface IBaseEntityFacade : IDisposable {
        Tuple<bool, IEnumerable<ValidationResult>> CanAdd();
        Tuple<bool, IEnumerable<ValidationResult>> CanUpdate();
        Tuple<bool, IEnumerable<ValidationResult>> CanDelete();
    }

    public interface IBaseEntityFacade<TEntity> : IBaseEntityFacade where TEntity : BaseEntity {
        TEntity GenerateEntity();
    }
}