#region Usings

using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List
{
    public class ListProductViewModel : ListBaseEntityViewModel<Product>
    {
        public ListProductViewModel() : base(new Product())
        {
            Entity = new Product();
        }

        public override void InitializeServices()
        {
            throw new System.NotImplementedException();
        }

        public override void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}