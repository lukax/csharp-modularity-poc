using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Dao.Interface;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace LOB.Dao.Nhibernate
{
    public class Module : IModule 
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IRepository, DomainRepository>();
            _container.RegisterType<IUnityOfWork, UnityOfWork>();
            _container.RegisterType<ISessionCreator, SessionCreator>();
        }
    }
}
