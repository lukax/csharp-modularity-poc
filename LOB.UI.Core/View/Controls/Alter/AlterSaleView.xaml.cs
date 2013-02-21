using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Interface;

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export]
    public partial class AlterSaleView : UserControl, ITabProp
    {
        [ImportingConstructor]
        public AlterSaleView(AlterSaleViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        public string Header { get { return "Alterar Venda"; } set{} }
        public int? Index { get; set; }
    }
}
