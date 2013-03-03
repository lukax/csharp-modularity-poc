#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    [Export]
    public partial class ListCustomerView : UserControl, ITabProp, IView
    {
        private string _header;

        public ListCustomerView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public ListCustomerView(ListCustomerViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public ListCustomerViewModel ViewModel
        {
            set { this.DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Clientes" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }
    }
}