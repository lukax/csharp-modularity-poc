#region Usings

using System.ComponentModel.Composition;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.View.Actions;
using LOB.UI.Core.View.Controllers;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using IRegionAdapter = LOB.UI.Interface.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Modularity {
    [ModuleExport("UICoreViewModule", typeof(Module), DependsOnModuleNames = new[] {"LogModule", "UICoreModule"},
        InitializationMode = InitializationMode.OnDemand)]
    public class Module : IModule {
        private readonly ILoggerFacade _loggerFacade;
        private readonly IRegionManager _regionManager;
        private readonly IRegionAdapter _regionAdapter;
        [Import] private MainRegionController _mainRegionController;

        [ImportingConstructor]
        public Module(ILoggerFacade loggerFacade, IRegionManager regionManager, IRegionAdapter regionAdapter) {
            _loggerFacade = loggerFacade;
            _regionManager = regionManager;
            _regionAdapter = regionAdapter;
        }

        public void Initialize() {
            _regionManager.RegisterViewWithRegion(RegionName.HeaderRegion, typeof(IBaseView<IHeaderToolViewModel>));
            _regionManager.RegisterViewWithRegion(RegionName.ColumnRegion, typeof(IBaseView<IColumnToolViewModel>));
            _regionManager.RegisterViewWithRegion(RegionName.BottomRegion, typeof(IBaseView<INotificationToolViewModel>));

            CloseTabItemAction.RegionAdapter = _regionAdapter;

#if DEBUG
            _loggerFacade.Log("UICoreViewModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}