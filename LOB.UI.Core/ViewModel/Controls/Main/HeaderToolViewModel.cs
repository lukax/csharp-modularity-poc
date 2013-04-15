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

        private ViewID _viewID = new ViewID {Type = ViewType.HeaderTool, State = ViewState.Internal};
        public override ViewID ViewID {
            get { return _viewID; }
            set { _viewID = value; }
        }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}