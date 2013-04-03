#region Usings

using System.Windows.Input;
using LOB.Core.Licensing;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel {
    public class MainWindowViewModel : BaseViewModel {

        private ICommandService _commandService;

        [InjectionConstructor]
        public MainWindowViewModel(IUnityContainer container, IFluentNavigator navigator, ICommandService commandService) {
            _container = container;
            _navigator = navigator;
            _commandService = commandService;

            OpenTabCommand = new DelegateCommand(OpenTab);
        }

        public string LicenseInformation {
            get { return ProductLicense.LicenseInformation(); }
        }

        public ICommand OpenTabCommand { get; set; }
        private IUnityContainer _container { get; set; }
        private IFluentNavigator _navigator { get; set; }

        private UIOperation _operation = new UIOperation {Type = UIOperationType.Main};
        public override UIOperation Operation {
            get { return _operation; }
            set { _operation = value; }
        }

        private void OpenTab(object arg) {
            UIOperationType operationType = arg.ToString().ToUIOperationType();
            var op = new UIOperation {Type = operationType};
            _navigator.ResolveView(op).ResolveViewModel(op).AddToRegion(RegionName.TabRegion);
        }

        public override void InitializeServices() { }

        public override void Refresh() { }

    }
}