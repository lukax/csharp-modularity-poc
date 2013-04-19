#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.View.Controls.Main {
    /// <summary>
    ///     Interaction logic for ColumnToolView.xaml
    /// </summary>
    [Export]
    public partial class HeaderToolView : IBaseView<IHeaderToolViewModel> {
        public HeaderToolView() { InitializeComponent(); }

        [Import] public IHeaderToolViewModel ViewModel {
            get { return DataContext as IHeaderToolViewModel; }
            set {
                DataContext = value;
                value.InitializeServices();
            }
        }

        public string Header {
            get { return Strings.UI_Header_Main_Header; }
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