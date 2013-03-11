using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Business.Interface;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace LOB.Business
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
            _container.RegisterType<IEntityFacade<BaseEntity>, EntityFacade<BaseEntity>>();
            _container.RegisterType<IProductFacade, ProductFacade>();
            _container.RegisterType<IEmployeeFacade, EmployeeFacade>();
        }
    }
}
