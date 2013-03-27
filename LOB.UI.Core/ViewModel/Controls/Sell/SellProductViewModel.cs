#region Usings

using System;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    public class SellProductViewModel : ISellProductViewModel {
        public string Header { get; set; }

        public void InitializeServices() {
            throw new NotImplementedException();
        }

        public void Refresh() {
            throw new NotImplementedException();
        }

        public OperationType OperationType {
            get { return OperationType.SellProduct; }
        }
    }
}