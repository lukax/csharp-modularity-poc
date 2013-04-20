#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.View.Controls.Main {
    /// <summary>
    ///     Interaction logic for BusyIuiComponent.xaml
    /// </summary>
    [Export]
    public partial class BusyView : IBaseView<IBaseViewModel> {
        public BusyView() { InitializeComponent(); }

        public IBaseViewModel ViewModel { get; set; }

        public int Index { get; set; }

        public void Refresh() { }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}