#region Usings

using System;
using System.ComponentModel;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Core.ViewModel.Controls.Main;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.ServiceLocation;
using NullGuard;

#endregion

namespace LOB.UI.Core.View.Controllers {
    public class MainRegionController : IDisposable {

        private readonly IServiceLocator _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private readonly IFluentNavigator _navigator;
        private readonly IRegionAdapter _regionAdapter;
        //private readonly BackgroundWorker _worker = new BackgroundWorker();

        public MainRegionController(IServiceLocator container, IRegionAdapter regionAdapter, IEventAggregator eventAggregator, ILoggerFacade logger,
            IFluentNavigator navigator) {
            _container = container;
            _regionAdapter = regionAdapter;
            _eventAggregator = eventAggregator;
            _logger = logger;
            _navigator = navigator;

            OnLoad();
        }

        private SubscriptionToken _openViewEventSubscription;
        private SubscriptionToken _closeViewEventSubscription;
        private void OnLoad() {
            _openViewEventSubscription = _eventAggregator.GetEvent<OpenViewEvent>().Subscribe(OpenView, true);
            _closeViewEventSubscription = _eventAggregator.GetEvent<CloseViewEvent>().Subscribe(CloseView, true);
            //_eventAggregator.GetEvent<MessageShowEvent>().Subscribe(s => MessageShow(s), true);
            //_eventAggregator.GetEvent<MessageHideEvent>().Subscribe(MessageHide, true);
        }

        private void OpenView(UIOperation param) {
            if(param.Type == default(UIOperationType)) throw new ArgumentNullException("param");
            param.IsChild = false;
            if(param.State == UIOperationState.QuickSearch) QuickSearch(param);
            else _navigator.Init.ResolveView(param).ResolveViewModel(param).AddToRegion(RegionName.TabRegion);
        }

        private void QuickSearch(UIOperation param) {
            if(param.Type == default(UIOperationType)) throw new ArgumentException("param");
            var view = _navigator.Init.ResolveView(param).ResolveViewModel(param).GetView();
            var baseViewModel = view.ViewModel as BaseViewModel;
            if(baseViewModel != null) baseViewModel.Operation = new UIOperation {State = UIOperationState.QuickSearch, Type = view.Operation.Type};
            // Let the viewModel know that it's in QuickSearch State
            _regionAdapter.AddView(view, RegionName.ModalRegion);
        }

        private void CloseView(UIOperation param) {
            try {
                if(param.State != UIOperationState.QuickSearch) _regionAdapter.RemoveView(param, RegionName.TabRegion);
                else if(param.State == UIOperationState.QuickSearch) _regionAdapter.RemoveView(param, RegionName.ModalRegion);
            } catch(Exception ex) {
                _logger.Log(ex.Message, Category.Exception, Priority.High);
                MessageHide(ex.Message);
            }
        }

        public void MessageShow(string param, bool isRestrictive = true) {
            MessageHide(null);
            var viewModel = _container.GetInstance<MessageToolViewModel>();
            viewModel.Initialize(param, !isRestrictive, isRestrictive);
            _navigator.Init.ResolveView(new UIOperation {Type = UIOperationType.MessageTool})
                      .SetViewModel(viewModel)
                      .AddToRegion(RegionName.ModalRegion);
        }

        public void MessageHide([AllowNull] string param) {
            _regionAdapter.RemoveView(new UIOperation {Type = UIOperationType.MessageTool}, RegionName.ModalRegion);

            if(param != null) MessageShow(param, false);
                //await Task.Delay(4000);
                //MessageHide(null);
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