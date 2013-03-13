#region Usings

using LOB.UI.Core.View.Controls.Alter;
using LOB.UI.Core.View.Controls.Alter.Base;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.View.Controls.Sell;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
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
            _container.RegisterInstance<ICommandService>(CommandService.Default);

            var regionManager = _container.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(RegionNames.HeaderRegion, typeof (HeaderToolsView));
            regionManager.RegisterViewWithRegion(RegionNames.ColumnRegion, typeof (ColumnToolsView));
            regionManager.RegisterViewWithRegion(RegionNames.BodyRegion, typeof (AlterCustomerBaseView));
            regionManager.RegisterViewWithRegion(RegionNames.BodyRegion, typeof (AlterCategoryBaseView));
        }
    }
}