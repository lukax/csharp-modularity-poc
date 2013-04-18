#region Usings

using System;
using System.ComponentModel.Composition;
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
    [Export]
    public partial class ColumnToolView : IBaseView<IColumnToolViewModel> {
        public ColumnToolView() { InitializeComponent(); }

        [Import] public IColumnToolViewModel ViewModel {
            get { return DataContext as IColumnToolViewModel; }
            set { DataContext = value; }
        }

        public string Header {
            get { return Strings.UI_Header_Main_Column; }
        }

        public int Index { get; set; }

        public void Refresh() { }

        public ViewID ViewID {
            get { return ViewModel.ViewID; }
        }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}