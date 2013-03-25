#region Usings

using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base
{
    public class ListServiceViewModel : ListBaseEntityViewModel<Service>, IListServiceViewModel
    {
        public ListServiceViewModel(Service entity, IRepository repository) : base(entity, repository)
        {
        }

        public override OperationType OperationType
        {
            get { return OperationType.ListService; }
        }
    }
}