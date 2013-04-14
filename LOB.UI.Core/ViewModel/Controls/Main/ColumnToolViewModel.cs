#region Usings

using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public sealed class ColumnToolViewModel : BaseViewModel, IColumnToolsViewModel {

        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly IEventAggregator _eventAggregator;
        private readonly IFluentNavigator _navigator;
        private readonly IRegionAdapter _regionAdapter;
        private readonly IUnityContainer _unityContainer;
        public string NotificationStatus { get; set; }
        public ICommand OperationCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public ColumnToolViewModel(IEventAggregator eventAggregator, IFluentNavigator navigator, IRegionAdapter regionAdapter,
            IUnityContainer unityContainer) {
            _eventAggregator = eventAggregator;
            _navigator = navigator;
            _regionAdapter = regionAdapter;
            _unityContainer = unityContainer;
            OperationCommand = new DelegateCommand(ShowOperations);
            ShopCommand = new DelegateCommand(ShowShop);
            NotificationCommand = new DelegateCommand(ShowNotification);
            LogoutCommand = new DelegateCommand(Logout);
            NotificationStatus = Strings.UI_ToolTip_Notifications;
            InitWorker();
        }
        public override void InitializeServices() { }

        public override void Refresh() { }

        private ViewID _operation = new ViewID {Type = ViewType.ColumnTool, State = ViewState.Internal};
        public override ViewID Operation {
            get { return _operation; }
            set { _operation = value; }
        }

        private void ShowOperations(object arg) {
            var op = new ViewID {Type = ViewType.Op, State = ViewState.List};
            if(_regionAdapter.ContainsView(op, RegionName.TabRegion)) _regionAdapter.RemoveView(op, RegionName.TabRegion);
            else _navigator.Init.ResolveView(op).ResolveViewModel(op).AddToRegion(RegionName.TabRegion);
        }

        private void ShowShop(object obj) {
            //_eventAggregator.GetEvent<OpenViewEvent>().Publish("");
        }

        private void ShowNotification(object o) {
            var vm = _unityContainer.Resolve<INotificationToolViewModel>();
            vm.IsVisible = !vm.IsVisible;
            //var op = new ViewID {
            //    Type = ViewType.NotificationTool,
            //    State = ViewState.Tool
            //};
            //if(_regionAdapter.ContainsView(op, RegionName.BottomRegion)) _regionAdapter.RemoveView(op, RegionName.BottomRegion);
            //else
            //    _navigator.Init.ResolveView(op)
            //              .ResolveViewModel(op)
            //              .AddToRegion(RegionName.BottomRegion);
        }

        private void Logout(object o) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(new ViewID {Type = ViewType.Main}); }

        private void InitWorker() {
            _worker.DoWork += UpdateStatus;
            _worker.RunWorkerAsync();
        }

        private async void UpdateStatus(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            do {
                await Task.Delay(1000);
                NotificationStatus = _unityContainer.Resolve<INotificationToolViewModel>().Status;
            } while(!_worker.CancellationPending);
        }
        #region Implementation of IDisposable

        //~ColumnToolIuiComponentModel() { Disposing(false); }

        public override void Dispose() {
            Disposing(true);
            GC.SuppressFinalize(this);
        }

        private void Disposing(bool disposing) {
            _worker.CancelAsync();
            if(disposing) _worker.Dispose();
        }

        #endregion
    }
}