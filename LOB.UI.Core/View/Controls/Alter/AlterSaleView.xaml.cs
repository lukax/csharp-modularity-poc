#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterSaleView : UserControl, ITabProp, IView
    {
        private string _header;

        [ImportingConstructor]
        public AlterSaleView(AlterSaleViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Alterar Venda" : _header; }
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