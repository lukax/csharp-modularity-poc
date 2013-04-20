#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    [Export(typeof(IColumnToolViewModel))]
    public sealed class ColumnToolViewModel : BaseViewModel, IColumnToolViewModel {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private readonly IEventAggregator _eventAggregator;
        private readonly IFluentNavigator _navigator;
        private readonly IRegionAdapter _regionAdapter;
        private readonly INotificationToolViewModel _notificationToolViewModel;
        public string NotificationStatus { get; set; }
        public ICommand OperationCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        [ImportingConstructor]
        public ColumnToolViewModel(IEventAggregator eventAggregator, IFluentNavigator navigator, IRegionAdapter regionAdapter,
            INotificationToolViewModel notificationToolViewModel) {
            _eventAggregator = eventAggregator;
            _navigator = navigator;
            _regionAdapter = regionAdapter;
            _notificationToolViewModel = notificationToolViewModel;
            OperationCommand = new DelegateCommand(ShowOperations);
            ShopCommand = new DelegateCommand(ShowShop);
            NotificationCommand = new DelegateCommand(ShowNotification);
            LogoutCommand = new DelegateCommand(Logout);
            NotificationStatus = Strings.UI_ToolTip_Notifications;
            InitWorker();
        }
        public override void InitializeServices() { }

        public override void Refresh() { }

        private ViewModelState _viewModelState = new ViewModelState {ViewState = ViewState.Other};
        public override ViewModelState ViewModelState {
            get { return _viewModelState; }
            set { _viewModelState = value; }
        }

        private void ShowOperations(object arg) {
            //var op = new ViewModelState {Type = ViewType.Op, ViewState = ViewState.List};
            //if(_regionAdapter.ContainsView(op, RegionName.TabRegion)) _regionAdapter.RemoveView(op, RegionName.TabRegion);
            //else _navigator.Init.ResolveView(op).ResolveViewModel(op).AddToRegion(RegionName.TabRegion);
        }

        private void ShowShop(object obj) {
            //_eventAggregator.GetEvent<OpenViewEvent>().Publish("");
        }

        private void ShowNotification(object o) {
            _notificationToolViewModel.IsVisible = !_notificationToolViewModel.IsVisible;
            //var op = new ViewModelState {
            //    Type = ViewType.NotificationTool,
            //    ViewState = ViewState.Tool
            //};
            //if(_regionAdapter.ContainsView(op, RegionName.BottomRegion)) _regionAdapter.RemoveView(op, RegionName.BottomRegion);
            //else
            //    _navigator.Init.ResolveView(op)
            //              .ResolveViewModel(op)
            //              .AddToRegion(RegionName.BottomRegion);
        }

        private void Logout(object o) { //_eventAggregator.GetEvent<CloseViewEvent>().Publish(new ViewModelState {Type = ViewType.Main}); 
        }

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
                NotificationStatus = _notificationToolViewModel.Status;
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