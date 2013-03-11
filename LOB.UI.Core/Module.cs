using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.ViewModel.Controls.List;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace LOB.UI.Core
{
    public class Module : IModule
    {
        private IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            #region Alter
            _container.RegisterType<IAlterBaseEntityViewModel<BaseEntity>, AlterBaseEntityViewModel<BaseEntity>>();
            _container.RegisterType<IAlterPersonViewModel<Person>, AlterPersonViewModel<Person>>();
            _container.RegisterType<IAlterServiceViewModel<Service>, AlterServiceViewModel<Service>>();

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
            _container.RegisterType<IListBaseEntityViewModel<BaseEntity>, ListBaseEntityViewModel<BaseEntity>>();
            _container.RegisterType<IListPersonViewModel<Person>, ListPersonViewModel<Person>>();
            _container.RegisterType<IListServiceViewModel<Service>, ListServiceViewModel<Service>>();

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
            //_container.RegisterType<IListSaleViewModel, ListSaleViewModel>();
            #endregion
        }
    }
}
