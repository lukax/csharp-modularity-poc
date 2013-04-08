#region Usings

using System;
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

        [InjectionConstructor]
        public MainWindowViewModel(IFluentNavigator navigator) {
            Navigator = navigator;

            OpenTabCommand = new DelegateCommand(OpenTab);
        }

        public string LicenseInformation {
            get { return ProductLicense.LicenseInformation(); }
        }

        public ICommand OpenTabCommand { get; set; }
        private IFluentNavigator Navigator { get; set; }

        private UIOperation _operation = new UIOperation {Type = UIOperationType.Main};
        public override UIOperation Operation {
            get { return _operation; }
            set { _operation = value; }
        }

        private void OpenTab(object arg) {
            UIOperationType operationType = arg.ToString().ToUIOperationType();
            var op = new UIOperation {Type = operationType};
            Navigator.ResolveView(op).ResolveViewModel(op).AddToRegion(RegionName.TabRegion);
        }

        public override void InitializeServices() { }

        public override void Refresh() { }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}