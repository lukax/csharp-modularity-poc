#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.Core.Licensing;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.ViewModel {
    [Export]
    public class ShellViewModel : BaseViewModel {
        public ShellViewModel() { OpenTabCommand = new DelegateCommand(OpenTab); }

        public string LicenseInformation {
            get { return ProductLicense.LicenseInformation(); }
        }

        public ICommand OpenTabCommand { get; set; }
        [Import] private IFluentNavigator Navigator { get; set; }

        private ViewID _viewID = new ViewID {Type = ViewType.Main};
        public override ViewID ViewID {
            get { return _viewID; }
            set { _viewID = value; }
        }

        private void OpenTab(object arg) {
            ViewType operationType = arg.ToString().ToUIOperationType();
            var op = new ViewID {Type = operationType};
            Navigator.ResolveView(op).ResolveViewModel(op).AddToRegion(RegionName.TabRegion);
        }

        public override void InitializeServices() { }

        public override void Refresh() { }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}