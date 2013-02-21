#region Usings

using LOB.Dao.Interface;
using LOB.Domain.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public class ListClientViewModel : ListPersonViewModel
    {
        public ListClientViewModel(Person entity, IRepository repository)
            : base(entity, repository)
        {
        }
    }
}