#region Usings

using LOB.Domain.Base;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.List.Base
{
    public interface IListBaseEntityViewModel<T> : IBaseViewModel where T:BaseEntity
    {
    }
}