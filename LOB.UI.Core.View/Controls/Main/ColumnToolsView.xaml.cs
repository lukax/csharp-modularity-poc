#region Usings
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.View.Controls.Main {
    /// <summary>
    ///     Interaction logic for ColumnToolsView.xaml
    /// </summary>
    public partial class ColumnToolsView : UserControl, IBaseView {

        public ColumnToolsView(IColumnToolsViewModel viewModel) {
            this.InitializeComponent();
            this.ViewModel = viewModel;
        }

        public IBaseViewModel ViewModel {
            get { return this.DataContext as IBaseViewModel; }
            set { this.DataContext = value; }
        }

        public string Header {
            get { return Strings.Header_Main_Column; }
        }

        public int Index { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType {
            get { return OperationType.ColumnTools; }
        }

    }
}