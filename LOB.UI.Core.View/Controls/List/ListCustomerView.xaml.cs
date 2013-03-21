#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    public partial class ListCustomerView : UserControl, IBaseView
    {
        private string _header;

        public ListCustomerView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IListCustomerViewModel; }
            set { DataContext = value; }
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

        public OperationType OperationType
        {
            get { return OperationType.ListCustomer; }
        }
    }
}