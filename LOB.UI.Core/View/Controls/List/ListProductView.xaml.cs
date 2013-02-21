#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    [Export]
    public partial class ListProductView : UserControl, ITabProp, IView
    {
        [ImportingConstructor]
        public ListProductView(ListBaseEntityViewModel<Product> dataContext)
        {
            InitializeComponent();
            DataContext = dataContext;
        }

        public string Header
        {
            get { return "Produtos"; }
            set { }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
            throw new System.NotImplementedException();
        }

        public void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}