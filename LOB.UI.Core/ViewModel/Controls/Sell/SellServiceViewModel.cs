#region Usings

using System;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    public class SellServiceViewModel : ISellServiceViewModel {

        private readonly ViewID _operation = new ViewID {Type = ViewType.Service, State = ViewState.Sell};
        public ViewID Operation {
            get { return _operation; }
        }

        public string Header { get; set; }

        public void InitializeServices() { throw new NotImplementedException(); }

        public void Refresh() { throw new NotImplementedException(); }
        #region Implementation of IDisposable

        public void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}