using LOB.Dao.Interface;
using LOB.Domain.Base;

namespace LOB.UI.Core.ViewModel.Controls.List.Base
{
    public class ListServiceViewModel :ListBaseEntityViewModel<Service>
    {
        public ListServiceViewModel(Service entity, IRepository repository) : base(entity, repository)
        {
        }
    }
}