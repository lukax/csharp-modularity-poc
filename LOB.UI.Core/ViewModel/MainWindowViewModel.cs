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

        [InjectionConstructor] public MainWindowViewModel(IUnityContainer container, IFluentNavigator navigator,
            ICommandService commandService) {
            this._container = container;
            this._navigator = navigator;
            this._commandService = commandService;

            this.OpenTabCommand = new DelegateCommand(this.OpenTab);
        }

        public string LicenseInformation {
            get { return ProductLicense.LicenseInformation(); }
        }

        public ICommand OpenTabCommand { get; set; }
        private IUnityContainer _container { get; set; }
        private IFluentNavigator _navigator { get; set; }

        public override OperationType OperationType {
            get { return OperationType.Main; }
        }

        private void OpenTab(object arg) {
            OperationType oP = arg.ToString().ToOperationType();
            this._navigator.ResolveView(oP).ResolveViewModel(oP).AddToRegion(RegionName.TabRegion);
        }

        public override void InitializeServices() {}

        public override void Refresh() {}

    }
}