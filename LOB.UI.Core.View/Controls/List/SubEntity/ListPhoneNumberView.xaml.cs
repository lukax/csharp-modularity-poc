#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
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

        public string Header { get { return Strings.Header_List_PhoneNumber; } }

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