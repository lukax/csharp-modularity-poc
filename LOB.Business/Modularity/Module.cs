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

namespace LOB.Business.Modularity {
    [Module(ModuleName = "BusinessModule")] public class Module : IModule {

        private readonly IUnityContainer _container;

        public Module(IUnityContainer container) {
            this._container = container;
        }

        public void Initialize() {
            this._container.RegisterType<IBaseEntityFacade, BaseEntityFacade>();
            this._container.RegisterType<IPersonFacade, PersonFacade>();
            this._container.RegisterType<IServiceFacade, ServiceFacade>();

            this._container.RegisterType<IAddressFacade, AddressFacade>();
            this._container.RegisterType<ICategoryFacade, CategoryFacade>();
            this._container.RegisterType<IContactInfoFacade, ContactInfoFacade>();
            this._container.RegisterType<IEmailFacade, EmailFacade>();
            this._container.RegisterType<IContactInfoFacade, ContactInfoFacade>();
            this._container.RegisterType<IPhoneNumberFacade, PhoneNumberFacade>();
            this._container.RegisterType<IShipmentInfoFacade, ShipmentInfoInfoFacade>();

            this._container.RegisterType<ICustomerFacade, CustomerFacade>();
            this._container.RegisterType<IEmployeeFacade, EmployeeFacade>();
            this._container.RegisterType<ILegalPersonFacade, LegalPersonFacade>();
            this._container.RegisterType<INaturalPersonFacade, NaturalPersonFacade>();
            this._container.RegisterType<IProductFacade, ProductFacade>();
            //_container.RegisterType<ISaleFacade, SaleFacade>();

#if DEBUG
            var log = this._container.Resolve<ILogger>();
            log.Log("BusinessModule Initialized", Category.Debug, Priority.Medium);
#endif
        }

    }
}