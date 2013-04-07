#region Usings

using LOB.Domain.Base;
using LOB.Log.Interface;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Main;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.ViewModel.Controls.List;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.Modularity {
    [Module(ModuleName = "UICoreModule")]
    public class Module : IModule {

        private readonly IUnityContainer _container;

        public Module(IUnityContainer container) { _container = container; }

        public void Initialize() {
            #region Events

            #endregion
            #region Main

            _container.RegisterType<IColumnToolsViewModel, ColumnToolViewModel>();
            _container.RegisterType<IHeaderToolsViewModel, HeaderToolViewModel>();
            //_container.RegisterType<INotificationToolViewModel, NotificationToolViewModel>();
            _container.RegisterType<IMessageToolViewModel, MessageToolViewModel>();

            //_container.RegisterInstance<IMessageToolViewModel>(_container.Resolve<MessageToolViewModel>());
            _container.RegisterInstance<INotificationToolViewModel>(_container.Resolve<NotificationToolViewModel>());
            //_container.RegisterInstance<IColumnToolsViewModel>(_container.Resolve<ColumnToolViewModel>());
            //_container.RegisterInstance<IHeaderToolsViewModel>(_container.Resolve<HeaderToolViewModel>());

            #endregion
            #region Alter

            _container.RegisterType<IAlterBaseEntityViewModel, AlterBaseEntityViewModel<BaseEntity>>
                ();
            _container.RegisterType<IAlterPersonViewModel, AlterPersonViewModel>();
            _container.RegisterType<IAlterServiceViewModel, AlterServiceViewModel>();

            _container.RegisterType<IAlterAddressViewModel, AlterAddressViewModel>();
            _container.RegisterType<IAlterCategoryViewModel, AlterCategoryViewModel>();
            _container.RegisterType<IAlterEmailViewModel, AlterEmailViewModel>();
            _container.RegisterType<IAlterPayCheckViewModel, AlterPayCheckViewModel>();
            _container.RegisterType<IAlterPhoneNumberViewModel, AlterPhoneNumberViewModel>();

            _container.RegisterType<IAlterCustomerViewModel, AlterCustomerViewModel>();
            _container.RegisterType<IAlterEmployeeViewModel, AlterEmployeeViewModel>();
            _container.RegisterType<IAlterLegalPersonViewModel, AlterLegalPersonViewModel>();
            _container.RegisterType<IAlterNaturalPersonViewModel, AlterNaturalPersonViewModel>();
            _container.RegisterType<IAlterProductViewModel, AlterProductViewModel>();
            _container.RegisterType<IAlterSaleViewModel, AlterSaleViewModel>();

            #endregion
            #region List

            _container.RegisterType<IListBaseEntityViewModel, ListBaseEntityViewModel<BaseEntity>>();
            _container.RegisterType<IListPersonViewModel, ListPersonViewModel>();
            _container.RegisterType<IListServiceViewModel, ListServiceViewModel>();

            _container.RegisterType<IListAddressViewModel, ListAddressViewModel>();
            _container.RegisterType<IListCategoryViewModel, ListCategoryViewModel>();
            _container.RegisterType<IListEmailViewModel, ListEmailViewModel>();
            _container.RegisterType<IListPayCheckViewModel, ListPayCheckViewModel>();
            _container.RegisterType<IListPhoneNumberViewModel, ListPhoneNumberViewModel>();

            _container.RegisterType<IListCustomerViewModel, ListCustomerViewModel>();
            _container.RegisterType<IListEmployeeViewModel, ListEmployeeViewModel>();
            _container.RegisterType<IListLegalPersonViewModel, ListLegalPersonViewModel>();
            _container.RegisterType<IListNaturalPersonViewModel, ListNaturalPersonViewModel>();
            _container.RegisterType<IListProductViewModel, ListProductViewModel>();
            _container.RegisterType<IListOpViewModel, ListOpViewModel>();
            //_container.RegisterType<IListSaleViewModel, ListSaleViewModel>();

            #endregion
#if DEBUG
            var log = _container.Resolve<ILogger>();
            log.Log("UICoreModule Initialized", Category.Debug, Priority.Medium);
#endif
        }

    }
}