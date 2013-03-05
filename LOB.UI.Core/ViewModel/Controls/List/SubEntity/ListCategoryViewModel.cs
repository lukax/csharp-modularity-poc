using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity
{
    public class ListCategoryViewModel :ListBaseEntityViewModel<Category>
    {
        public ListCategoryViewModel(Category entity, IRepository repository) : base(entity, repository)
        {
        }
    }
}