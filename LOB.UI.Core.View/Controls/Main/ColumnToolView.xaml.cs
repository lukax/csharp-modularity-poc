#region Usings

using System;
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
    public partial class ColumnToolView : UserControl, IBaseView {
        public ColumnToolView(IColumnToolsViewModel viewModel) {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public ViewID ViewID {
            get { return ViewModel.ViewID; }
        }
        public IBaseViewModel ViewModel {
            get { return DataContext as IBaseViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.UI_Header_Main_Column; }
        }

        public int Index { get; set; }

        public void Refresh() { }

        public ViewType ViewType {
            get { return ViewType.ColumnTool; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}