#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
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
            get { return Strings.Header_Alter_Sale; }
        }

        public int Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }

        public OperationType OperationType
        {
            get { return OperationType.AlterSale; }
        }
    }
}