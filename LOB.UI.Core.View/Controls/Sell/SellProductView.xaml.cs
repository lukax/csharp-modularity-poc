#region Usings

using System;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.View.Controls.Sell {
    public partial class SellProductView : IBaseView<ISellProductViewModel> {
        public SellProductView() { InitializeComponent(); }

        public ISellProductViewModel ViewModel { get; set; }

        public string Header {
            get { return Strings.Command_Sell_Product; }
        }

        public int Index { get; set; }

        public void InitializeServices() { throw new NotImplementedException(); }

        public void Refresh() { throw new NotImplementedException(); }

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