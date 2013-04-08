﻿#region Usings

using System;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class HeaderToolViewModel : BaseViewModel, IHeaderToolsViewModel {

        public override void InitializeServices() { }

        public override void Refresh() { }

        private UIOperation _operation = new UIOperation {Type = UIOperationType.HeaderTool, State = UIOperationState.Internal};
        public override UIOperation Operation {
            get { return _operation; }
            set { _operation = value; }
        }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}