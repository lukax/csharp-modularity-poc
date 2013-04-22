#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using NullGuard;

#endregion

namespace LOB.UI.Core.View.Controllers {
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class MainRegionController : IDisposable {
        [Import] private IEventAggregator EventAggregator {
            set {
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
            if(openViewPayload.ViewInfo.ViewStates[0] == ViewState.QuickSearch) QuickSearch(openViewPayload);
            else Navigator.Value.Init.ResolveView(openViewPayload.ViewInfo).AddToRegion(RegionName.TabRegion);
            if(openViewPayload.GetIdFunc != null) openViewPayload.GetIdFunc(Navigator.Value.GetViewId);
        }

        private void QuickSearch(OpenViewPayload openViewPayload) {
            //if(param.Type == default(ViewType)) throw new ArgumentException("param");
            //var view = _navigator.Init.ResolveView(param).ResolveViewModel(param).Get();
            //var baseViewModel = view.ViewModel as BaseViewModel;
            //if(baseViewModel != null) baseViewModel.ViewModelInfo = new ViewModelInfo {ViewState = ViewState.QuickSearch, Type = view.ViewID.Type};
            //// Let the IuiComponentModel know that it's in QuickSearch ViewState
            //_regionAdapter.Add(view, RegionName.ModalRegion);
        }

        private void CloseView(Guid param) {
            try {
                RegionAdapter.Value.Remove(RegionAdapter.Value.Get(param));
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

        public void MessageHide([AllowNull] string param) {
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