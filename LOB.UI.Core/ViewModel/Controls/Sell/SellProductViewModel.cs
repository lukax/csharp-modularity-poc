#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    [Export(typeof(ISellProductViewModel))]
    public class SellProductViewModel : ISellProductViewModel {
        #region Implementation of IDisposable

        public void Dispose() { throw new NotImplementedException(); }

        #endregion
        #region Implementation of IBaseViewModel

        public Guid Id { get; private set; }
        public ViewModelInfo Info { get; set; }
        public void InitializeServices() { throw new NotImplementedException(); }
        public void Refresh() { throw new NotImplementedException(); }

        #endregion
    }
}