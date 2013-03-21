#region Usings

using System.Windows.Input;
using LOB.Core;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ICommandService _commandService;

        [InjectionConstructor]
        public MainWindowViewModel(IUnityContainer container, IFluentNavigator navigator, ICommandService commandService)
        {
            _container = container;
            _navigator = navigator;
            _commandService = commandService;

            OpenTabCommand = new DelegateCommand(OpenTab);
        }

        public string LicenseInformation
        {
            get { return ProductLicense.LicenseInformation(); }
        }

        public ICommand OpenTabCommand { get; set; }
        private IUnityContainer _container { get; set; }
        private IFluentNavigator _navigator { get; set; }

        public override OperationType OperationType
        {
            get { return OperationType.Main; }
        }

        private void OpenTab(object arg)
        {
            OperationType oP = arg.ToString().ToOperationType();
            _navigator.ResolveView(oP).ResolveViewModel(oP).AddToRegion(RegionName.TabRegion);
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}