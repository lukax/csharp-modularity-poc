#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Core.Events;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    /// <summary>
    ///     Interaction logic for ListCommandView.xaml
    /// </summary>
    public partial class ListOpView : UserControl, IBaseView
    {
        private readonly IEventAggregator _eventAggregator;

        public ListOpView(IEventAggregator eventAggregator, IListOpViewModel viewModel)
        {
            _eventAggregator = eventAggregator;
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IListOpViewModel; }
            set { DataContext = value; }
        }

        public string Header
        {
            get { return Strings.Header_List_Op; }
        }

        public int Index { get; set; }

        public void InitializeServices()
        {
            _eventAggregator.GetEvent<RefreshEvent>().Subscribe(o => { if (o == OperationType.ListOp) Refresh(); });
        }

        public void Refresh()
        {
            ListViewEntitys.SelectedIndex = -1;
        }

        public OperationType OperationType
        {
            get { return OperationType.ListOp; }
        }
    }
}