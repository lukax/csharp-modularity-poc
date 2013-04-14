#region Usings

using System;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class HeaderToolViewModel : BaseViewModel, IHeaderToolsViewModel {

        public override void InitializeServices() { }

        public override void Refresh() { }

        private ViewID _operation = new ViewID {Type = ViewType.HeaderTool, State = ViewState.Internal};
        public override ViewID Operation {
            get { return _operation; }
            set { _operation = value; }
        }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}