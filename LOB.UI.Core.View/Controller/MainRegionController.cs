#region Usings

using System;
using System.Threading.Tasks;
using LOB.Dao.Interface;
using LOB.UI.Core.Event;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.ViewModel.Main;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using IRegionAdapter = LOB.UI.Interface.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Controller
{
    public class MainRegionController
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private readonly IRegionManager _regionManager;
        private readonly ISessionCreator _sessionCreator;
        private readonly IFluentNavigator _navigator;

        public MainRegionController(IUnityContainer container, IRegionManager regionManager,
                                    IEventAggregator eventAggregator, ILoggerFacade logger, IFluentNavigator navigator,
                                    ISessionCreator sessionCreator)
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
            _sessionCreator = sessionCreator;
            _navigator = navigator;
            
            OnLoad();
        }

        private void OnLoad()
        {
            _eventAggregator.GetEvent<OpenViewEvent>().Subscribe(OpenView, true);
            _eventAggregator.GetEvent<OpenTabEvent>().Subscribe(OpenTab, true);
            _eventAggregator.GetEvent<MessageShowEvent>().Subscribe(s => MessageShow(s), true);
            _eventAggregator.GetEvent<MessageHideEvent>().Subscribe(MessageHide, true);
            _sessionCreator.OnCreatingSession +=
                (sender, args) => _eventAggregator.GetEvent<MessageShowEvent>().Publish(args.Message);
            _sessionCreator.OnSessionCreated +=
                (sender, args) => _eventAggregator.GetEvent<MessageHideEvent>().Publish(args.Message);
        }

        private void OpenView(string param)
        {
            if (string.IsNullOrEmpty(param)) throw new ArgumentNullException("param");
            var region = this._regionManager.Regions[RegionName.TabRegion];
            var view = _navigator.Init.ResolveView(param).GetView();
            if (region.Views.Contains(region.GetView(param)))
                region.Remove(region.GetView(param));
            region.Add(view, param);
        }

        public void MessageShow(string param, bool isRestrictive = true)
        {
            MessageHide(null);
            var region = this._regionManager.Regions[RegionName.ModalRegion];
            var view = _container.Resolve<MessageToolsView>();
            var viewModel = _container.Resolve<MessageToolsViewModel>();
            viewModel.Initialize(param, !isRestrictive, isRestrictive);
            region.Add(_navigator.Init.SetView(view).SetViewModel(viewModel).GetView(), OperationType.MessageTools);
        }

        public async void MessageHide(string param)
        {
            var region = this._regionManager.Regions[RegionName.ModalRegion];
            var view = region.GetView(OperationType.MessageTools);
            if (region.Views.Contains(view))
                region.Remove(view);

            if (param != null)
            {
                MessageShow(param, false);
                await Task.Delay(4000);
                MessageHide(null);
            }
        }

        private void OpenTab(string param)
        {
            if (string.IsNullOrEmpty(param)) throw new ArgumentNullException("param");
            var region = this._regionManager.Regions[RegionName.TabRegion];
            var view = _navigator.Init.ResolveView(param).ResolveViewModel(param).GetView();
            region.Add(view, param);
        }
    }
}