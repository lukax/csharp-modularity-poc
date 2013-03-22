#region Usings

using System;
using System.Threading.Tasks;
using LOB.Dao.Interface;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Main;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View.Controllers
{
    public class MainRegionController
    {
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private readonly IFluentNavigator _navigator;
        private readonly IRegionAdapter _regionAdapter;
        private readonly ISessionCreator _sessionCreator;

        public MainRegionController(IUnityContainer container, IRegionAdapter regionAdapter,
                                    IEventAggregator eventAggregator, ILoggerFacade logger, IFluentNavigator navigator,
                                    ISessionCreator sessionCreator)
        {
            _container = container;
            _regionAdapter = regionAdapter;
            _eventAggregator = eventAggregator;
            _logger = logger;
            _sessionCreator = sessionCreator;
            _navigator = navigator;

            OnLoad();
        }

        private void OnLoad()
        {
            _eventAggregator.GetEvent<OpenViewEvent>().Subscribe(OpenView, true);
            _eventAggregator.GetEvent<CloseViewEvent>().Subscribe(CloseView, true);
            _eventAggregator.GetEvent<MessageShowEvent>().Subscribe(s => MessageShow(s), true);
            _eventAggregator.GetEvent<MessageHideEvent>().Subscribe(MessageHide, true);
            _sessionCreator.OnCreatingSession +=
                (sender, args) => _eventAggregator.GetEvent<MessageShowEvent>().Publish(args.Message);
            _sessionCreator.OnSessionCreated +=
                (sender, args) => _eventAggregator.GetEvent<MessageHideEvent>().Publish(args.Message);
        }

        private void OpenView(OperationType param)
        {
            if (param == default(OperationType)) throw new ArgumentNullException("param");
            _navigator.Init.ResolveView(param).ResolveViewModel(param).AddToRegion(RegionName.TabRegion);
        }

        private void CloseView(OperationType param)
        {
            try
            {
                _regionAdapter.RemoveView(param, RegionName.TabRegion);
            }
            catch (Exception ex)
            {
                _logger.Log(ex.Message, Category.Exception, Priority.High);
                MessageHide(ex.Message);
            }
        }

        public void MessageShow(string param, bool isRestrictive = true)
        {
            MessageHide(null);
            var viewModel = _container.Resolve<MessageToolsViewModel>();
            viewModel.Initialize(param, !isRestrictive, isRestrictive);
            _navigator.Init.ResolveView(OperationType.MessageTools)
                      .SetViewModel(viewModel)
                      .AddToRegion(RegionName.ModalRegion);
        }

        public async void MessageHide(string param)
        {
            _regionAdapter.RemoveView(OperationType.MessageTools, RegionName.ModalRegion);

            if (param != null)
            {
                MessageShow(param, false);
                await Task.Delay(4000);
                MessageHide(null);
            }
        }
    }
}