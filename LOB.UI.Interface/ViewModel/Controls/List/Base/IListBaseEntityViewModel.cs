#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.List.Base
{
    public interface IListBaseEntityViewModel<T> : IBaseViewModel where T : BaseEntity
    {
    }
}