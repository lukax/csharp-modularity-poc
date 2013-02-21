#region Usings

using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public class ListPersonViewModel : ListBaseEntityViewModel<Person>
    {
        public ListPersonViewModel(Person entity, IRepository repository)
            : base(entity, repository)
        {
        }

        public override bool CanUpdate(object arg)
        {
            //TODO: Business logic
            return true;
        }

        public override bool CanDelete(object arg)
        {
            //TODO: Business logic
            return true;
        }
    }
}