#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;

#endregion

namespace LOB.UI.Core.View.Controls.Alter
{
    public partial class AlterSaleView : UserControl, IBaseView
    {
        private string _header;

        public AlterSaleView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IAlterSaleViewModel; }
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