#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.View.Controls.List.Base
{
    public partial class ListServiceView : UserControl, IBaseView
    {
        private string _header;

        public ListServiceView()
        {
            InitializeComponent();
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IListServiceViewModel; }
            set { DataContext = value; }
        }

        public string Header
        {
            get { return Strings.Header_ListService; }
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
            get { return OperationType.ListService; }
        }
    }
}