#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    [Export(typeof(ISellProductViewModel))]
    public class SellProductViewModel : ISellProductViewModel {
        public string Header { get; set; }

        public ViewModelState State { get; set; }
        public void InitializeServices() { throw new NotImplementedException(); }

        public void Refresh() { throw new NotImplementedException(); }
        #region Implementation of IDisposable

        public void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}