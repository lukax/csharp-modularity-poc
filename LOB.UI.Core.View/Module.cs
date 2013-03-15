#region Usings

using LOB.Log.Interface;
using LOB.UI.Core.Infrastructure;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.View.Infrastructure;
using LOB.UI.Interface;
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

            regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof (HeaderToolsView));
            regionManager.RegisterViewWithRegion(RegionNames.ColumnRegion, typeof (ColumnToolsView));
            //regionManager.RegisterViewWithRegion(RegionNames.BodyRegion, typeof (AlterCustomerView));
            //regionManager.Regions.Add(RegionNames.BodyRegion, new Region());
            //regionManager.AddToRegion(RegionNames.BodyRegion, typeof (AlterCategoryView));

#if DEBUG
            var log = _container.Resolve<ILogger>();
            log.Log("UICoreViewModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}