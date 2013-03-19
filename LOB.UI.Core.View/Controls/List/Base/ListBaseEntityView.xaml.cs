#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.List.Base
{
    public partial class ListBaseEntityView : UserControl, IBaseViewModel
    {
        private string _header;

        public ListBaseEntityView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public int? Index { get; set; }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Códigos" : _header; }
            set { _header = value; }
        }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }

        public Interface.Infrastructure.OperationType OperationType
        {
            get { return OperationType.ListBaseEntity; }
        }
    }
}