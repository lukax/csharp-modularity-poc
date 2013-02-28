#region Usings

using System.ComponentModel.Composition;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    [Export]
    public sealed class ListProductViewModel : ListBaseEntityViewModel<Product>
    {
        [ImportingConstructor]
        public ListProductViewModel(Product product, IRepository repository)
            : base(product, repository)
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