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

        [ImportingConstructor]
        public ListEmployeeView(ListEmployeeViewModel dataContext) {
            InitializeComponent();
            DataContext = dataContext;
        }

        public string Header {
            get { return (string.IsNullOrEmpty(_header)) ? "Funcionarios" : _header; }
            set { _header = value; }
        }

        public int? Index { get; set; }

        public void InitializeServices() {
        }

        public void Refresh() {
        }
    }
}