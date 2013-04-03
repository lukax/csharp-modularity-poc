#region Usings

using LOB.Dao.Interface;
using LOB.Log.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Dao.Nhibernate.Modularity {
    [Module(ModuleName = "NHibernateModule")]
    public class Module : IModule {

        private readonly IUnityContainer _container;

        public Module(IUnityContainer container) { _container = container; }

        public void Initialize() {
            //_container.RegisterType<ISessionCreator, SessionCreator>();
            _container.RegisterInstance<ISessionCreator>(_container.Resolve<SessionCreator>());
            _container.RegisterType<IUnityOfWork, UnityOfWork>();
            //_container.RegisterInstance<IUnityOfWork>(_container.Resolve<UnityOfWork>());
            _container.RegisterType<IRepository, Repository>();

#if DEBUG
            var log = _container.Resolve<ILogger>();
            log.Log("NhibernateModule Initialized", Category.Debug, Priority.Medium);
#endif
        }

    }
}