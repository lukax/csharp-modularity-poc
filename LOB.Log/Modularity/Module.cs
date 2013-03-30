#region Usings
using LOB.Log.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Log.Modularity {
    [Module(ModuleName = "LogModule")] public class Module : IModule {

        private readonly IUnityContainer _container;

        public Module(IUnityContainer container) {
            this._container = container;
        }

        public void Initialize() {
            this._container.RegisterType<ILogger, Logger>();
            this._container.RegisterType<ILoggerFacade, Logger>();
        }

    }
}