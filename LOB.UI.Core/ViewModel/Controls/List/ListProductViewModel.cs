#region Usings

using System.ComponentModel.Composition;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls
{
    public class ListProductViewModel : ListEntityViewModel<Product>
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