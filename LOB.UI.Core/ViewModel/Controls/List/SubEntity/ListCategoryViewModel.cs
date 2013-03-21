#region Usings

using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity
{
    public sealed class ListCategoryViewModel : ListServiceViewModel<Category>, IListCategoryViewModel
    {
        public ListCategoryViewModel(Category entity, IRepository repository)
            : base(entity, repository)
        {
        }

        public override OperationType OperationType
        {
            get { return OperationType.NewCategory; }
        }
    }
}