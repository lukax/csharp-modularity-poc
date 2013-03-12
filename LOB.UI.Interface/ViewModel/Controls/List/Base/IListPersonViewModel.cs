#region Usings

using LOB.Domain.Base;

#endregion

namespace LOB.UI.Interface.ViewModel.Controls.List.Base
{
    public interface IListPersonViewModel<T> : IListBaseEntityViewModel<T> where T : Person
    {
    }
}