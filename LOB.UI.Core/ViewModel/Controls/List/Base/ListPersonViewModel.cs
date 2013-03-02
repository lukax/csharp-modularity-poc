#region Usings

using LOB.Dao.Interface;
using LOB.Domain.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base
{
    public abstract class ListPersonViewModel<T> : ListBaseEntityViewModel<T> where T:Person
    {

        public ListPersonViewModel(T entity, IRepository repository)
            : base(entity, repository)
        {
        }

        protected override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        protected override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }
    }
}