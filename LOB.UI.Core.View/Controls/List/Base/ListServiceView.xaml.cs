#region Usings

using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
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
            get { return DataContext as IListServiceViewModel<Service>; }
            set { DataContext = value; }
        }

        public string Header
        {
            get { return (string.IsNullOrEmpty(_header)) ? "Services" : _header; }
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