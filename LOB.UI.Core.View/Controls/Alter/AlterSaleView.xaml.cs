#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export("")]
    public partial class AlterSaleView : UserControl, IView
    {
        private string _header;

        public AlterSaleView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterSaleView(AlterSaleViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public AlterSaleViewModel ViewModel
        {
            set { this.DataContext = value; }
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