#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    [Export(typeof(ISellServiceViewModel))]
    public class SellServiceViewModel : BaseViewModel, ISellServiceViewModel {
        #region Overrides of BaseViewModel

        public override ViewModelInfo Info { get; set; }
        public override void InitializeServices() { throw new NotImplementedException(); }
        public override void Refresh() { throw new NotImplementedException(); }
        public override void Dispose() { throw new NotImplementedException(); }

        #endregion
    }
}