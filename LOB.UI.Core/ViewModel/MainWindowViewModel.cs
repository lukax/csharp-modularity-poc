#region Usings

using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Core;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel
{
    [Export]
    public class MainWindowViewModel : BaseViewModel
    {
        public string LicenseInformation { get { return ProductLicense.LicenseInformation(); } }

        [ImportingConstructor]
        public MainWindowViewModel(IUnityContainer container, IFluentNavigator navigator)
        {
            _container = container;
            _navigator = navigator;

            OpenTabCommand = new DelegateCommand(OpenTab);
        }

        public ICommand OpenTabCommand { get; set; }
        private IUnityContainer _container { get; set; }
        private IFluentNavigator _navigator { get; set; }

        private void OpenTab(object arg)
        {
            Messenger.Default.Send(_navigator.Resolve(arg.ToString()).Get(), "OpenTab");
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}