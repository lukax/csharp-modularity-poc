#region Usings

using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Interface.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base
{
    public class ListServiceViewModel<T> : ListBaseEntityViewModel<T>, IListServiceViewModel where T : Service
    {
        public ListServiceViewModel(T entity, IRepository repository) : base(entity, repository)
        {
        }
    }
}