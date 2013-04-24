#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    [Export(typeof(IColumnToolViewModel))]
    public sealed class ColumnToolViewModel : BaseViewModel, IColumnToolViewModel {
        public string NotificationStatus { get; set; }
        public ICommand OperationCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        [Import] public Lazy<IEventAggregator> LazyEventAggregator { get; set; }
        [Import] public Lazy<IFluentNavigator> LazyFluentNavigator { get; set; }
        [Import] public Lazy<IRegionAdapter> LazyRegionAdapter { get; set; }
        [Import] public Lazy<INotificationToolViewModel> LazyNotificationViewModel { get; set; }

        public ColumnToolViewModel() {
            OperationCommand = new DelegateCommand(ShowOperations);
            ShopCommand = new DelegateCommand(ShowShop);
            NotificationCommand = new DelegateCommand(ShowNotification);
            LogoutCommand = new DelegateCommand(Logout);
            NotificationStatus = Strings.UI_ToolTip_Notifications;
            InitWorker();
        }
        public override void InitializeServices() { }

        public override void Refresh() { }

        private ViewModelInfo _viewModelInfo = new ViewModelInfo {State = ViewState.Other};
        public override ViewModelInfo Info {
            get { return _viewModelInfo; }
            set { _viewModelInfo = value; }
        }

        private void ShowOperations(object arg) {
            //var op = new ViewModelInfo {Type = ViewType.Op, ViewState = ViewState.List};
            //if(_regionAdapter.Contains(op, RegionName.TabRegion)) _regionAdapter.Remove(op, RegionName.TabRegion);
            //else _navigator.Init.ResolveView(op).ResolveViewModel(op).AddToRegion(RegionName.TabRegion);
            LazyFluentNavigator.Value.Init.ResolveView<IListOpViewModel>().AddToRegion(RegionName.TabRegion);
        }

        private void ShowShop(object obj) {
            //_eventAggregator.GetEvent<OpenViewEvent>().Publish("");
        }

        private void ShowNotification(object o) {
            LazyNotificationViewModel.Value.IsVisible = !LazyNotificationViewModel.Value.IsVisible;
            //var op = new ViewModelInfo {
            //    Type = ViewType.NotificationTool,
            //    ViewState = ViewState.Tool
            //};
            //if(_regionAdapter.Contains(op, RegionName.BottomRegion)) _regionAdapter.Remove(op, RegionName.BottomRegion);
            //else
            //    _navigator.Init.ResolveView(op)
            //              .ResolveViewModel(op)
            //              .AddToRegion(RegionName.BottomRegion);
        }

        private void Logout(object o) { //_eventAggregator.GetEvent<CloseViewEvent>().Publish(new ViewModelInfo {Type = ViewType.Main}); 
        }

        private void InitWorker() {
            Worker.DoWork += UpdateStatus;
            Worker.RunWorkerAsync();
        }

        private async void UpdateStatus(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            do {
                await Task.Delay(1000);
                NotificationStatus = LazyNotificationViewModel.Value.Status;
            } while(!Worker.CancellationPending);
        }
        #region Implementation of IDisposable

        //~ColumnToolIuiComponentModel() { Disposing(false); }

        public override void Dispose() {
            Disposing(true);
            GC.SuppressFinalize(this);
        }

        private void Disposing(bool disposing) {
            Worker.CancelAsync();
            if(disposing) Worker.Dispose();
        }

        #endregion
    }
}