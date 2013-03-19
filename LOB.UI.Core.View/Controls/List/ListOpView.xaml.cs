#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;

#endregion

namespace LOB.UI.Core.View.Controls.List
{
    /// <summary>
    ///     Interaction logic for ListCommandView.xaml
    /// </summary>
    public partial class ListOpView : UserControl, IBaseView
    {
        public ListOpView(IListOpViewModel viewModel)
        {
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
            get { return "Commands"; }
            set { }
        }

        public int? Index { get; set; }

        public void InitializeServices()
        {
        }

        public void Refresh()
        {
        }

        public Interface.Infrastructure.OperationType OperationType
        {
            get { return OperationType.ListOp; }
        }
    }
}