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
    public sealed class ColumnToolViewModel : IColumnToolsViewModel {

        private readonly ICommandService _commandService;
        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;

        public ColumnToolViewModel(IUnityContainer container, ICommandService commandService,
            IEventAggregator eventAggregator) {
            _container = container;
            _commandService = commandService;
            _eventAggregator = eventAggregator;
            OperationCommand = new DelegateCommand(ShowOperations);
            ShopCommand = new DelegateCommand(ShowShop);
        }

        public ICommand OperationCommand { get; set; }
        public ICommand ShopCommand { get; set; }
        public string Header { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.ColumnTool};
        public UIOperation Operation {
            get { return _operation; }
        }

        private void ShowShop(object obj) {
            //_eventAggregator.GetEvent<OpenViewEvent>().Publish("");
        }

        private void ShowOperations(object arg) {
            _eventAggregator.GetEvent<OpenViewEvent>()
                            .Publish(new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List});
        }

    }
}