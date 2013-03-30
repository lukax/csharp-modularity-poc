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
    [Module(ModuleName = "UICoreModule")] public class Module : IModule {

        private readonly IUnityContainer _container;

        public Module(IUnityContainer container) {
            this._container = container;
        }

        public void Initialize() {
            #region Events
            #endregion
            #region Main
            this._container.RegisterType<IColumnToolsViewModel, ColumnToolsViewModel>();
            this._container.RegisterType<IHeaderToolsViewModel, HeaderToolsViewModel>();
            #endregion
            #region Alter
            this._container.RegisterType<IAlterBaseEntityViewModel, AlterBaseEntityViewModel<BaseEntity>>();
            this._container.RegisterType<IAlterPersonViewModel, AlterPersonViewModel>();
            this._container.RegisterType<IAlterServiceViewModel, AlterServiceViewModel>();

            this._container.RegisterType<IAlterAddressViewModel, AlterAddressViewModel>();
            this._container.RegisterType<IAlterCategoryViewModel, AlterCategoryViewModel>();
            this._container.RegisterType<IAlterEmailViewModel, AlterEmailViewModel>();
            this._container.RegisterType<IAlterPayCheckViewModel, AlterPayCheckViewModel>();
            this._container.RegisterType<IAlterPhoneNumberViewModel, AlterPhoneNumberViewModel>();

            this._container.RegisterType<IAlterCustomerViewModel, AlterCustomerViewModel>();
            this._container.RegisterType<IAlterEmployeeViewModel, AlterEmployeeViewModel>();
            this._container.RegisterType<IAlterLegalPersonViewModel, AlterLegalPersonViewModel>();
            this._container.RegisterType<IAlterNaturalPersonViewModel, AlterNaturalPersonViewModel>();
            this._container.RegisterType<IAlterProductViewModel, AlterProductViewModel>();
            this._container.RegisterType<IAlterSaleViewModel, AlterSaleViewModel>();
            #endregion
            #region List
            this._container.RegisterType<IListBaseEntityViewModel, ListBaseEntityViewModel<BaseEntity>>();
            this._container.RegisterType<IListPersonViewModel, ListPersonViewModel>();
            this._container.RegisterType<IListServiceViewModel, ListServiceViewModel>();

            this._container.RegisterType<IListAddressViewModel, ListAddressViewModel>();
            this._container.RegisterType<IListCategoryViewModel, ListCategoryViewModel>();
            this._container.RegisterType<IListEmailViewModel, ListEmailViewModel>();
            this._container.RegisterType<IListPayCheckViewModel, ListPayCheckViewModel>();
            this._container.RegisterType<IListPhoneNumberViewModel, ListPhoneNumberViewModel>();

            this._container.RegisterType<IListCustomerViewModel, ListCustomerViewModel>();
            this._container.RegisterType<IListEmployeeViewModel, ListEmployeeViewModel>();
            this._container.RegisterType<IListLegalPersonViewModel, ListLegalPersonViewModel>();
            this._container.RegisterType<IListNaturalPersonViewModel, ListNaturalPersonViewModel>();
            this._container.RegisterType<IListProductViewModel, ListProductViewModel>();
            this._container.RegisterType<IListOpViewModel, ListOpViewModel>();
            //_container.RegisterType<IListSaleViewModel, ListSaleViewModel>();
            #endregion
            this._container.RegisterInstance(this._container.Resolve<MessageToolsViewModel>());

#if DEBUG
            var log = this._container.Resolve<ILogger>();
            log.Log("UICoreModule Initialized", Category.Debug, Priority.Medium);
#endif
        }

    }
}