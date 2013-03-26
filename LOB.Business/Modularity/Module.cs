#region Usings

using LOB.Business.Interface.Logic;
using LOB.Business.Interface.Logic.Base;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Business.Logic;
using LOB.Business.Logic.Base;
using LOB.Business.Logic.SubEntity;
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
            _container.RegisterType<IBaseEntityFacade, BaseEntityFacade>();
            _container.RegisterType<IPersonFacade, PersonFacade>();
            _container.RegisterType<IServiceFacade, ServiceFacade>();

            _container.RegisterType<IAddressFacade, AddressFacade>();
            _container.RegisterType<ICategoryFacade, CategoryFacade>();
            _container.RegisterType<IContactInfoFacade, ContactInfoFacade>();
            _container.RegisterType<IEmailFacade, EmailFacade>();
            //_container.RegisterType<IShipmentInfoFacade, ShipmentInfoInfoFacade>();

            _container.RegisterType<ICustomerFacade, CustomerFacade>();
            _container.RegisterType<IEmployeeFacade, EmployeeFacade>();
            _container.RegisterType<ILegalPersonFacade, LegalPersonFacade>();
            _container.RegisterType<INaturalPersonFacade, NaturalPersonFacade>();
            _container.RegisterType<IProductFacade, ProductFacade>();
            //_container.RegisterType<ISaleFacade, SaleFacade>();

#if DEBUG
            var log = _container.Resolve<ILogger>();
            log.Log("BusinessModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}