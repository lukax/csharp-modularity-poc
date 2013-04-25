#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.Core.Licensing;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Core.ViewModel.Base;

#endregion

namespace LOB.UI.Core.ViewModel {
    [Export]
    public class ShellViewModel : BaseViewModel {
        public string LicenseInformation {
            get { return ProductLicense.LicenseInformation(); }
        }
        public ICommand OpenTabCommand { get; set; }
        [Import] private Lazy<IFluentNavigator> Navigator { get; set; }

        public ShellViewModel() { OpenTabCommand = new DelegateCommand(OpenTab); }

        private void OpenTab(object arg) {
            //ViewType operationType = arg.ToString().ToUIOperationType();
            //Navigator.ResolveView(op).ResolveViewModel(op).AddToRegion(RegionName.TabRegion);
        }

        public override void InitializeServices() { }

        public override void Refresh() { }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}