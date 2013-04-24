#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.Domain.Logic;

#endregion

namespace LOB.Business.Interface.Logic.Base {
    public interface IBaseEntityFacade :IDisposable {
        Tuple<bool, IEnumerable<ValidationResult>> CanAdd();
        Tuple<bool, IEnumerable<ValidationResult>> CanUpdate();
        Tuple<bool, IEnumerable<ValidationResult>> CanDelete();
    }

    public interface IBaseEntityFacade<TEntity> : IBaseEntityFacade where TEntity : BaseEntity {
        TEntity Entity { set; }
        TEntity GenerateEntity();
    }
}