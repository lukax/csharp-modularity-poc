#region Usings

using System.Collections.Generic;
using LOB.Domain.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.List.Base {
    public interface IListBaseEntityViewModel : IBaseViewModel {}

    public interface IListBaseEntityViewModel<out TEntity> : IListBaseEntityViewModel where TEntity : BaseEntity {
        TEntity Entity { get; }
        IEnumerable<TEntity> Entities { get; }
    }
}