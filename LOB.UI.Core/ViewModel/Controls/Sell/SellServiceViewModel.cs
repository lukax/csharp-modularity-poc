﻿#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    [Export(typeof(ISellServiceViewModel))]
    public class SellServiceViewModel : ISellServiceViewModel {
        private readonly ViewID _viewID = new ViewID {Type = ViewType.Service, State = ViewState.Sell};
        public ViewID ViewID {
            get { return _viewID; }
        }

        public string Header { get; set; }

        public void InitializeServices() { throw new NotImplementedException(); }

        public void Refresh() { throw new NotImplementedException(); }
        #region Implementation of IDisposable

        public void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}