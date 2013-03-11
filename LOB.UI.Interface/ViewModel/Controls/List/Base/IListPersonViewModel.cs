using LOB.Domain.Base;

namespace LOB.UI.Interface.ViewModel.Controls.List.Base
{
    public interface IListPersonViewModel<T> : IListBaseEntityViewModel<T> where T:Person
    {
    }
}