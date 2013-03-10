#region Usings

using System.ComponentModel.Composition;
using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    [Export("")]
    public partial class AlterSaleBaseView : UserControl, IBaseView
    {
        private string _header;

        public AlterSaleBaseView()
        {
            InitializeComponent();
        }

        [ImportingConstructor]
        public AlterSaleBaseView(IAlterSaleViewModel viewModel)
            : this()
        {
            ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
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