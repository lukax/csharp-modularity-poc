#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Contract.Controller;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.View.Controllers {
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class MainRegionController : IBaseController {
        [Import] private IEventAggregator EventAggregator {
            set {
                _openViewEventSubscription = value.GetEvent<OpenViewInfoEvent>().Subscribe(OpenViewInfo, true);
                _openViewEventSubscription = value.GetEvent<OpenViewEvent>().Subscribe(OpenView, true);
                _closeViewEventSubscription = value.GetEvent<CloseViewEvent>().Subscribe(CloseView, true);
            }
        }
        [Import] private Lazy<ILoggerFacade> Logger { get; set; }
        [Import] private Lazy<IFluentNavigator> Navigator { get; set; }
        [Import] private Lazy<IRegionAdapter> RegionAdapter { get; set; }

        private SubscriptionToken _openViewEventSubscription;
        private SubscriptionToken _closeViewEventSubscription;

        private void OpenView(OpenViewPayload openViewPayload) {
            if(openViewPayload.ViewState == ViewState.QuickSearch) Navigator.Value.Init.ResolveView(openViewPayload.ViewType).AddToRegion(RegionName.ModalRegion);
            else Navigator.Value.Init.ResolveView(openViewPayload.ViewType).AddToRegion(RegionName.TabRegion);
            if(openViewPayload.GetIdFunc != null) openViewPayload.GetIdFunc(Navigator.Value.GetViewId);
        }

        private void OpenViewInfo(OpenViewInfoPayload openViewInfoPayload) {
            if(openViewInfoPayload.ViewInfo.ViewStates != null && openViewInfoPayload.ViewInfo.ViewStates[0] == ViewState.QuickSearch) Navigator.Value.Init.ResolveView(openViewInfoPayload.ViewInfo).AddToRegion(RegionName.ModalRegion);
            else Navigator.Value.Init.ResolveView(openViewInfoPayload.ViewInfo).AddToRegion(RegionName.TabRegion);
            if(openViewInfoPayload.GetIdFunc != null) openViewInfoPayload.GetIdFunc(Navigator.Value.GetViewId);
        }

        private void CloseView(CloseViewPayload closeViewPayload) {
            try {
                RegionAdapter.Value.Remove(RegionAdapter.Value.Get(closeViewPayload.ViewId));
            } catch(Exception ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                MessageHide(ex.Message);
            }
        }

        public void MessageShow(string param, bool isRestrictive = true) {
            //MessageHide(null);
            //var viewModel = _container.GetInstance<MessageToolViewModel>();
            //viewModel.Initialize(param, !isRestrictive, isRestrictive);
            //_navigator.Init.ResolveView(new ViewModelInfo {Type = ViewType.MessageTool}).SetViewModel(() => viewModel).AddToRegion(RegionName.ModalRegion);
        }

        public void MessageHide(string param) {
            //_regionAdapter.Remove(new ViewModelInfo {Type = ViewType.MessageTool}, RegionName.ModalRegion);

            //if(param != null) MessageShow(param, false);
            ////await Task.Delay(4000);
            ////MessageHide(null);
        }
        #region Implementation of IDisposable

        ~MainRegionController() { Dispose(false); }
        private void Dispose(bool disposing) {
            if(!disposing) return;
            _openViewEventSubscription.Dispose();
            _closeViewEventSubscription.Dispose();
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}