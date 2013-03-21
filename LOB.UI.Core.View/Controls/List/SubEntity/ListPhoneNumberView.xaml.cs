#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;

#endregion

namespace LOB.UI.Core.View.Controls.List.SubEntity
{
    public partial class ListPhoneNumberView : UserControl, IBaseView
    {
        private string _header;

        public ListPhoneNumberView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IListPhoneNumberViewModel; }
            set { DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Códigos" : _header; }
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
            get { return OperationType.ListPhoneNumber; }
        }
    }
}