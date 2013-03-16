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
            _eventAggregator.GetEvent<MessageShowEvent>().Subscribe(s =>
                {
                    _regionManager.RegisterViewWithRegion(RegionNames.ModalRegion, () =>
                        {
                            return _navigator.ResolveView(Operation.MessageTools)
                                          .SetViewModel(_container.Resolve<MessageToolsViewModel>(new ParameterOverride("message", s)))
                                          .GetView();
                        });
                });
            _eventAggregator.GetEvent<MessageHideEvent>().Subscribe(s =>
                {
                    //_regionManager.Regions[RegionNames.ModalRegion].Remove(_container.Resolve<MessageToolsView>());
                });
            _sessionCreator.OnCreatingSession +=
                (sender, args) => _eventAggregator.GetEvent<MessageShowEvent>().Publish("Por favor aguarde...");
            _sessionCreator.OnSessionCreated +=
                (sender, args) => _eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }

        private void OpenView(string param)
        {
            if (string.IsNullOrEmpty(param)) throw new ArgumentNullException("param");
            _navigator.ResolveView(param).Show(true);
        }
    }
}
