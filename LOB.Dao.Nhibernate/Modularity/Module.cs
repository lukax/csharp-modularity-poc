#region Usings
using LOB.Dao.Interface;
using LOB.Log.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Dao.Nhibernate.Modularity {
    [Module(ModuleName = "NHibernateModule")] public class Module : IModule {

        private readonly IUnityContainer _container;

        public Module(IUnityContainer container) {
            this._container = container;
        }

        public void Initialize() {
            //_container.RegisterType<ISessionCreator, SessionCreator>();
            this._container.RegisterInstance<ISessionCreator>(this._container.Resolve<SessionCreator>());
            //_container.RegisterType<IUnityOfWork, UnityOfWork>();
            this._container.RegisterInstance<IUnityOfWork>(this._container.Resolve<UnityOfWork>());
            this._container.RegisterType<IRepository, Repository>();

#if DEBUG
            var log = this._container.Resolve<ILogger>();
            log.Log("NhibernateModule Initialized", Category.Debug, Priority.Medium);
#endif
        }

    }
}