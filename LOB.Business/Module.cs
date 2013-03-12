#region Usings

using LOB.Business.Interface;
using LOB.Domain.Base;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Business
{
    [Module(ModuleName = "BusinessModule")]
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