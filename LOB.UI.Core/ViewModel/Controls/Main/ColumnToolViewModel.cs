#region Usings

using System.Windows.Input;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public sealed class ColumnToolViewModel : IColumnToolsViewModel {

        private readonly IEventAggregator _eventAggregator;
        private readonly IFluentNavigator _navigator;
        private readonly IRegionAdapter _regionAdapter;

        public ColumnToolViewModel(IEventAggregator eventAggregator, IFluentNavigator navigator,
            IRegionAdapter regionAdapter) {
            _eventAggregator = eventAggregator;
            _navigator = navigator;
            _regionAdapter = regionAdapter;
            OperationCommand = new DelegateCommand(ShowOperations);
            ShopCommand = new DelegateCommand(ShowShop);
            NotificationCommand = new DelegateCommand(ShowNotification);
            LogoutCommand = new DelegateCommand(Logout);
        }

        public ICommand OperationCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public ICommand NotificationCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        public string Header { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.ColumnTool,
            State = UIOperationState.Tool
        };
        public UIOperation Operation {
            get { return _operation; }
        }

        private void ShowOperations(object arg) {
            var op = new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List};
            if(_regionAdapter.ContainsView(op, RegionName.TabRegion)) _regionAdapter.RemoveView(op, RegionName.TabRegion);
            else
                _navigator.Init.ResolveView(op)
                          .ResolveViewModel(op)
                          .AddToRegion(RegionName.TabRegion);
        }

        private void ShowShop(object obj) {
            //_eventAggregator.GetEvent<OpenViewEvent>().Publish("");
        }

        private void ShowNotification(object o) {
            var op = new UIOperation {
                Type = UIOperationType.NotificationTool,
                State = UIOperationState.Tool
            };
            if(_regionAdapter.ContainsView(op, RegionName.BottomRegion)) _regionAdapter.RemoveView(op, RegionName.BottomRegion);
            else
                _navigator.Init.ResolveView(op)
                          .ResolveViewModel(op)
                          .AddToRegion(RegionName.BottomRegion);
        }

        private void Logout(object o) {
            _eventAggregator.GetEvent<CloseViewEvent>()
                            .Publish(new UIOperation {Type = UIOperationType.Main});
        }

    }
}