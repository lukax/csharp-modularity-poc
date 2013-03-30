#region Usings
using System.Windows.Input;
using LOB.UI.Core.Events.View;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public sealed class ColumnToolsViewModel : IColumnToolsViewModel {

        private readonly ICommandService _commandService;
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public ColumnToolsViewModel(IUnityContainer container, ICommandService commandService,
            IEventAggregator eventAggregator) {
            this._container = container;
            this._commandService = commandService;
            this._eventAggregator = eventAggregator;
            this.OperationCommand = new DelegateCommand(this.ShowOperations);
            this.ShopCommand = new DelegateCommand(this.ShowShop);
        }

        public ICommand OperationCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public string Header { get; set; }

        public void InitializeServices() {}

        public void Refresh() {}

        public OperationType OperationType { get; set; }

        private void ShowShop(object obj) {
            //_eventAggregator.GetEvent<OpenViewEvent>().Publish("");
        }

        private void ShowOperations(object arg) {
            this._eventAggregator.GetEvent<OpenViewEvent>().Publish(OperationType.ListOp);
        }

    }
}