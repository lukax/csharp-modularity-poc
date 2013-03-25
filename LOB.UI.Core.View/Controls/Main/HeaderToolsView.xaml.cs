#region Usings

using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.View.Controls.Main
{
    /// <summary>
    ///     Interaction logic for ColumnToolsView.xaml
    /// </summary>
    public partial class HeaderToolsView : UserControl, IBaseView
    {
        public HeaderToolsView(IHeaderToolsViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel
        {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header
        {
            get { return Strings.Header_Main_Header; }
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
            get { return OperationType.HeaderTools; }
        }
    }
}