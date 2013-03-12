#region Usings

using LOB.Log.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Log
{
    [Module(ModuleName = "LogModule")]
    public class Module : IModule
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<ILogger, Logger>();
            _container.RegisterType<ILoggerFacade, Logger>();
        }
    }
}