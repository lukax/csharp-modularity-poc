#region Usings

using LOB.Business.Interface;
using LOB.Business.Logic;
using LOB.Domain.Base;
using LOB.Log.Interface;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.Business.Modularity
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

#if DEBUG
            var log = _container.Resolve<ILogger>();
            log.Log("BusinessModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}