#region Usings

using System;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Sell;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Sell {
    public class SellServiceViewModel : ISellServiceViewModel {

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Sell};
        public UIOperation UIOperation {
            get { return _operation; }
        }

        public string Header { get; set; }

        public void InitializeServices() {
            throw new NotImplementedException();
        }

        public void Refresh() {
            throw new NotImplementedException();
        }

    }
}