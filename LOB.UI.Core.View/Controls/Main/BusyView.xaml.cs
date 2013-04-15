#region Usings

using System;
using System.Windows.Controls;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Controls.Main {
    /// <summary>
    ///     Interaction logic for BusyIuiComponent.xaml
    /// </summary>
    public partial class BusyView : UserControl, IBaseView {
        public BusyView() { InitializeComponent(); }

        public ViewID ViewID {
            get { return ViewModel.ViewID; }
        }
        public IBaseViewModel ViewModel { get; set; }

        public string Header {
            get { return Strings.UI_Header_Main_Busy; }
        }

        public int Index { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}