#region Usings

using System;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    public class SellProductViewModel : ISellProductViewModel {
        public string Header { get; set; }

        public void InitializeServices() { throw new NotImplementedException(); }

        public void Refresh() { throw new NotImplementedException(); }

        private readonly ViewID _viewID = new ViewID {Type = ViewType.Service, State = ViewState.Add};
        public ViewID ViewID {
            get { return _viewID; }
        }
        #region Implementation of IDisposable

        public void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}