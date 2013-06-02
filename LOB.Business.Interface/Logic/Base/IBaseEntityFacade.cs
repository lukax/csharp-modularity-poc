#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LOB.Domain.Base;

#endregion

namespace LOB.Business.Contract.Logic.Base {
    public interface IBaseEntityFacade : IDisposable {}

    public interface IBaseEntityFacade<out TEntity> : IBaseEntityFacade where TEntity : BaseEntity {
        TEntity Generate();
    }
}