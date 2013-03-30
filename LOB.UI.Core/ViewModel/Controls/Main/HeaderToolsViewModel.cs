#region Usings

using System;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class HeaderToolsViewModel : IHeaderToolsViewModel {

        public string Header { get; set; }

        public void InitializeServices() {
            throw new NotImplementedException();
        }

        public void Refresh() {
            throw new NotImplementedException();
        }

        public OperationType OperationType { get; set; }

    }
}