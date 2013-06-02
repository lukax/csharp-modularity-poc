#region Usings

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

#endregion

namespace LOB.Business.Modularity {
    [ModuleExport("BusinessModule", typeof(Module), DependsOnModuleNames = new[] {"LogModule", "DaoModule"})]
    public class Module : IModule {
#if DEBUG
        [Import]
        public ILoggerFacade LoggerFacade { get; set; }
#endif

        public void Initialize() {
            //_container.RegisterType<IBaseEntityFacade<BaseEntity>, BaseEntityFacade<BaseEntity>>();
            //_container.RegisterType<IPersonFacade, PersonFacade>();

            //_container.RegisterType<IAddressFacade, AddressFacade>();
            //_container.RegisterType<ICategoryFacade, CategoryFacade>();
            //_container.RegisterType<IPayCheckFacade, PayCheckFacade>();
            //_container.RegisterType<IContactInfoFacade, ContactInfoFacade>();
            //_container.RegisterType<IEmailFacade, EmailFacade>();
            //_container.RegisterType<IContactInfoFacade, ContactInfoFacade>();
            //_container.RegisterType<IPhoneNumberFacade, PhoneNumberFacade>();
            //_container.RegisterType<IShipmentInfoFacade, ShipmentInfoFacade>();

            //_container.RegisterType<ICustomerFacade, CustomerFacade>();
            //_container.RegisterType<IEmployeeFacade, EmployeeFacade>();
            //_container.RegisterType<ILegalPersonFacade, LegalPersonFacade>();
            //_container.RegisterType<INaturalPersonFacade, NaturalPersonFacade>();
            //_container.RegisterType<IProductFacade, ProductFacade>();
            ////_container.RegisterType<ISaleFacade, SaleFacade>();

#if DEBUG
            LoggerFacade.Log("BusinessModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}