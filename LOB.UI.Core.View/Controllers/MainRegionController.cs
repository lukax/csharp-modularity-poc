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
            this._container = container;
            this._regionAdapter = regionAdapter;
            this._eventAggregator = eventAggregator;
            this._logger = logger;
            this._sessionCreator = sessionCreator;
            this._unityOfWork = unityOfWork;
            this._navigator = navigator;

            this.OnLoad();
        }

        private void OnLoad() {
            this._eventAggregator.GetEvent<OpenViewEvent>().Subscribe(this.OpenView, true);
            this._eventAggregator.GetEvent<CloseViewEvent>().Subscribe(this.CloseView, true);
            this._eventAggregator.GetEvent<MessageShowEvent>().Subscribe(s => this.MessageShow(s), true);
            this._eventAggregator.GetEvent<MessageHideEvent>().Subscribe(this.MessageHide, true);
        }

        private void OpenView(OperationType param) {
            if(param == default(OperationType)) throw new ArgumentNullException("param");
            this._navigator.Init.ResolveView(param).ResolveViewModel(param).AddToRegion(RegionName.TabRegion);
        }

        private void CloseView(OperationType param) {
            try {
                this._regionAdapter.RemoveView(param, RegionName.TabRegion);
            }
            catch(Exception ex) {
                this._logger.Log(ex.Message, Category.Exception, Priority.High);
                this.MessageHide(ex.Message);
            }
        }

        public void MessageShow(string param, bool isRestrictive = true) {
            this.MessageHide(null);
            var viewModel = this._container.Resolve<MessageToolsViewModel>();
            viewModel.Initialize(param, !isRestrictive, isRestrictive);
            this._navigator.Init.ResolveView(OperationType.MessageTools)
                .SetViewModel(viewModel)
                .AddToRegion(RegionName.ModalRegion);
        }

        public async void MessageHide([AllowNull] string param) {
            this._regionAdapter.RemoveView(OperationType.MessageTools, RegionName.ModalRegion);

            if(param != null) {
                this.MessageShow(param, false);
                await Task.Delay(4000);
                this.MessageHide(null);
            }
        }

    }
}