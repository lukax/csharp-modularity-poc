#region Usings

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;

#endregion

namespace LOB.UI.Core.Modularity {
    [ModuleExport("UICoreModule", typeof(Module), DependsOnModuleNames = new[] {"LogModule", "DaoModule", "BusinessModule"})]
    public class Module : IModule {
#if DEBUG
        [Import] public ILoggerFacade LoggerFacade { get; set; }
#endif

        public void Initialize() {
            //#region Events

            //#endregion
            //#region Main

            //_container.RegisterType<IColumnToolsViewModel, ColumnToolViewModel>();
            //_container.RegisterType<IHeaderToolsViewModel, HeaderToolViewModel>();
            ////_container.RegisterType<INotificationToolIuiComponentModel, NotificationToolIuiComponentModel>();
            //_container.RegisterType<IMessageToolViewModel, MessageToolViewModel>();

            ////_container.RegisterInstance<IMessageToolIuiComponentModel>(_container.Resolve<MessageToolIuiComponentModel>());
            //_container.RegisterInstance<INotificationToolViewModel>(_container.Resolve<NotificationToolViewModel>());
            ////_container.RegisterInstance<IColumnToolsIuiComponentModel>(_container.Resolve<ColumnToolIuiComponentModel>());
            ////_container.RegisterInstance<IHeaderToolsIuiComponentModel>(_container.Resolve<HeaderToolIuiComponentModel>());

            //#endregion
            //#region Alter

            //_container.RegisterType<IAlterBaseEntityViewModel, AlterBaseEntityViewModel<BaseEntity>>();
            //_container.RegisterType<IAlterPersonViewModel, AlterPersonViewModel>();

            //_container.RegisterType<IAlterAddressViewModel, AlterAddressViewModel>();
            //_container.RegisterType<IAlterCategoryViewModel, AlterCategoryViewModel>();
            //_container.RegisterType<IAlterContactInfoViewModel, AlterContactInfoViewModel>();
            //_container.RegisterType<IAlterEmailViewModel, AlterEmailViewModel>();
            //_container.RegisterType<IAlterPayCheckViewModel, AlterPayCheckViewModel>();
            //_container.RegisterType<IAlterPhoneNumberViewModel, AlterPhoneNumberViewModel>();

            //_container.RegisterType<IAlterCustomerViewModel, AlterCustomerViewModel>();
            //_container.RegisterType<IAlterEmployeeViewModel, AlterEmployeeViewModel>();
            //_container.RegisterType<IAlterLegalPersonViewModel, AlterLegalPersonViewModel>();
            //_container.RegisterType<IAlterNaturalPersonViewModel, AlterNaturalPersonViewModel>();
            //_container.RegisterType<IAlterProductViewModel, AlterProductViewModel>();
            //_container.RegisterType<IAlterSaleViewModel, AlterSaleViewModel>();

            //#endregion
            //#region List

            //_container.RegisterType<IListBaseEntityViewModel, ListBaseEntityViewModel<BaseEntity>>();
            //_container.RegisterType<IListPersonViewModel, ListPersonViewModel>();

            //_container.RegisterType<IListAddressViewModel, ListAddressViewModel>();
            //_container.RegisterType<IListCategoryViewModel, ListCategoryViewModel>();
            //_container.RegisterType<IListEmailViewModel, ListEmailViewModel>();
            //_container.RegisterType<IListPayCheckViewModel, ListPayCheckViewModel>();
            //_container.RegisterType<IListPhoneNumberViewModel, ListPhoneNumberViewModel>();

            //_container.RegisterType<IListCustomerViewModel, ListCustomerViewModel>();
            //_container.RegisterType<IListEmployeeViewModel, ListEmployeeViewModel>();
            //_container.RegisterType<IListLegalPersonViewModel, ListLegalPersonViewModel>();
            //_container.RegisterType<IListNaturalPersonViewModel, ListNaturalPersonViewModel>();
            //_container.RegisterType<IListProductViewModel, ListProductViewModel>();
            //_container.RegisterType<IListOpViewModel, ListOpViewModel>();
            ////_container.RegisterType<IListSaleViewModel, ListSaleViewModel>();

            //#endregion
#if DEBUG
            LoggerFacade.Log("UICoreModule Initialized", Category.Debug, Priority.Medium);
#endif
        }
    }
}