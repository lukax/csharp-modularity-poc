#region Usings

using System;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.View.Controls.Sell {
    public partial class SellServiceView : IBaseView<ISellServiceViewModel> {
        public SellServiceView() { InitializeComponent(); }

        public ISellServiceViewModel ViewModel { get; set; }

        public int Index { get; set; }

        public void InitializeServices() { throw new NotImplementedException(); }

        public void Refresh() { throw new NotImplementedException(); }
        #region Implementation of IDisposable

        public void Dispose() {
            if(ViewModel != null) ViewModel.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}