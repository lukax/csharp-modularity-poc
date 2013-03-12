using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.UI.Interface;
using LOB.UI.Interface.Command;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

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
        }
    }
}
