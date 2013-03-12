#region Usings

using LOB.Dao.Interface;
using LOB.Log.Interface;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Dao.Nhibernate
{
    [Module(ModuleName = "NHibernateModule")]
    public class Module : IModule
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.Resolve<ILogger>();
            _container.RegisterType<IRepository, DomainRepository>();
            _container.RegisterType<IUnityOfWork, UnityOfWork>();
            _container.RegisterType<ISessionCreator, SessionCreator>();
        }
    }
}