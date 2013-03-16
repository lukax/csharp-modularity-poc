using System;
using LOB.Dao.Interface;
using LOB.UI.Core.Event;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Core.ViewModel.Main;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace LOB.UI.Core.View.Controller
{
    public class MainRegionController
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private readonly IFluentNavigator _navigator;
        private readonly ISessionCreator _sessionCreator;

        public MainRegionController(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator, ILoggerFacade logger, IFluentNavigator navigator, ISessionCreator sessionCreator)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (logger == null) throw new ArgumentNullException("logger");
            if (navigator == null) throw new ArgumentNullException("navigator");
            _container = container;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _logger = logger;
            _navigator = navigator;
            _sessionCreator = sessionCreator;

            OnLoad();
        }

        private void OnLoad()
        {
            _eventAggregator.GetEvent<OpenViewEvent>().Subscribe(OpenView, true);
            _eventAggregator.GetEvent<OpenTabEvent>().Subscribe(OpenTab, true);
            _eventAggregator.GetEvent<MessageShowEvent>().Subscribe(MessageShow, true);
            _eventAggregator.GetEvent<MessageHideEvent>().Subscribe(MessageHide, true);
            _sessionCreator.OnCreatingSession += (sender, args) => _eventAggregator.GetEvent<MessageShowEvent>().Publish("Conectando no banco de dados...");
            _sessionCreator.OnSessionCreated += (sender, args) => _eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }

        private void OpenView(string param)
        {
            if (string.IsNullOrEmpty(param)) throw new ArgumentNullException("param");
            var region = this._regionManager.Regions[RegionName.TabRegion];
            var view = _navigator.ResolveView(param).GetView();
            if (region.Views.Contains(view.GetType().Name))
                return;
            region.Add(view, view.GetType().Name);
        }

        public void MessageShow(string param)
        {
            MessageHide(param);
            var region = this._regionManager.Regions[RegionName.ModalRegion];
            var view = _container.Resolve<MessageToolsView>();
            var viewModel = _container.Resolve<MessageToolsViewModel>(); viewModel.Initialize(param, false);
            region.Add(_navigator.SetView(view).SetViewModel(viewModel).GetView(), OperationName.MessageTools);
        }

        public void MessageHide(string param)
        {
            var region = this._regionManager.Regions[RegionName.ModalRegion];
            
            var view = region.GetView(OperationName.MessageTools);
            if (region.Views.Contains(view))
                region.Remove(view);
        }

        private void OpenTab(string param)
        {
            if (string.IsNullOrEmpty(param)) throw new ArgumentNullException("param");
            _regionManager.RegisterViewWithRegion(RegionName.TabRegion, _navigator.ResolveView(param).ResolveViewModel(param).GetView);
        }
    }
}
