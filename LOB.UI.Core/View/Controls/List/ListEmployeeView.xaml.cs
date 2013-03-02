#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    [Export]
    public partial class ListEmployeeView : UserControl, ITabProp, IView
    {
        private string _header;

        public ListEmployeeView()
        {
            InitializeComponent();
        }

        public ListEmployeeViewModel ViewModel { set { this.DataContext = value; } }
        [ImportingConstructor]
        public ListEmployeeView(ListEmployeeViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Funcionarios" : _header; }
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