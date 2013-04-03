#region Usings

using System;
using System.Threading.Tasks;
using LOB.Dao.Interface;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Controls.Main;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;
using NullGuard;

#endregion

namespace LOB.UI.Core.View.Controllers {
    public class MainRegionController {

        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private readonly IFluentNavigator _navigator;
        private readonly IRegionAdapter _regionAdapter;
        private readonly ISessionCreator _sessionCreator;
        private readonly IUnityOfWork _unityOfWork;

        public MainRegionController(IUnityContainer container, IRegionAdapter regionAdapter,
            IEventAggregator eventAggregator, ILoggerFacade logger, IFluentNavigator navigator,
            ISessionCreator sessionCreator, IUnityOfWork unityOfWork) {
            _container = container;
            _regionAdapter = regionAdapter;
            _eventAggregator = eventAggregator;
            _logger = logger;
            _sessionCreator = sessionCreator;
            _unityOfWork = unityOfWork;
            _navigator = navigator;

            OnLoad();
        }

        private void OnLoad() {
            _eventAggregator.GetEvent<OpenViewEvent>().Subscribe(OpenView, true);
            _eventAggregator.GetEvent<CloseViewEvent>().Subscribe(CloseView, true);
            _eventAggregator.GetEvent<MessageShowEvent>().Subscribe(s => MessageShow(s), true);
            _eventAggregator.GetEvent<MessageHideEvent>().Subscribe(MessageHide, true);
            //_eventAggregator.GetEvent<QuickSearchEvent>().Subscribe(QuickSearch, true);
        }

        private void QuickSearch(UIOperation param) {
            if(param.Type == default(UIOperationType)) throw new ArgumentException("param");
            var view = _navigator.Init.ResolveView(param).ResolveViewModel(param).GetView();
            _regionAdapter.AddView(view, RegionName.ModalRegion);
        }

        private void OpenView(UIOperation param) {
            if(param.Type == default(UIOperationType)) throw new ArgumentNullException("param");
            if(param.State == UIOperationState.QuickSearch) QuickSearch(param);
            _navigator.Init.ResolveView(param).ResolveViewModel(param).AddToRegion(RegionName.TabRegion);
        }

        private void CloseView(UIOperation param) {
            try {
                _regionAdapter.RemoveView(param, RegionName.TabRegion);
            } catch(Exception ex) {
                _logger.Log(ex.Message, Category.Exception, Priority.High);
                MessageHide(ex.Message);
            }
        }

        public void MessageShow(string param, bool isRestrictive = true) {
            MessageHide(null);
            var viewModel = _container.Resolve<MessageToolViewModel>();
            viewModel.Initialize(param, !isRestrictive, isRestrictive);
            _navigator.Init.ResolveView(new UIOperation {Type = UIOperationType.MessageTool})
                      .SetViewModel(viewModel)
                      .AddToRegion(RegionName.ModalRegion);
        }

        public async void MessageHide([AllowNull] string param) {
            _regionAdapter.RemoveView(new UIOperation {Type = UIOperationType.MessageTool}, RegionName.ModalRegion);

            if(param != null) {
                MessageShow(param, false);
                await Task.Delay(4000);
                MessageHide(null);
            }
        }

    }
}