#region Usings

using LOB.Log.Interface;
using LOB.UI.Core.View.Controls.Alter;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.View.Names;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Names;
using Microsoft.Expression.Interactivity.Core;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using IRegionAdapter = LOB.UI.Interface.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View
{
    [Module(ModuleName = "UICoreViewModule")]
    public class Module : IModule
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IFluentNavigator, FluentNavigator>();
            _container.RegisterType<IRegionAdapter, RegionAdapter>();
            _container.RegisterInstance(CommandService.Default);

            var regionManager = _container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(RegionName.HeaderRegion, typeof (HeaderToolsView));
            regionManager.RegisterViewWithRegion(RegionName.ColumnRegion, typeof (ColumnToolsView));
            regionManager.RegisterViewWithRegion(RegionName.BodyRegion, typeof (AlterCustomerView));
            regionManager.RegisterViewWithRegion(RegionName.BodyRegion, typeof (AlterCategoryView));

#if DEBUG
            var log = _container.Resolve<ILogger>();
            log.Log("UICoreViewModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}