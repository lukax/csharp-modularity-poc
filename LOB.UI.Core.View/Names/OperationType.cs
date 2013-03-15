using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LOB.Domain.Base;
using LOB.UI.Core.Events;
using LOB.UI.Core.View.Controls.Alter;
using LOB.UI.Core.View.Controls.Alter.Base;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Controls.List;
using LOB.UI.Core.View.Controls.List.Base;
using LOB.UI.Core.View.Controls.List.SubEntity;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Main;
using LOB.UI.Interface.Names;

namespace LOB.UI.Core.View.Names
{
    public static class OperationType
    {
        #region View
        private static readonly Lazy<IDictionary<OperationName, Type>> lazyView =
            new Lazy<IDictionary<OperationName, Type>>(() => new Dictionary<OperationName, Type>
                {
                    {OperationName.MessageHideEvent, typeof(MessageHideEvent)},
                    {OperationName.MessageShowEvent, typeof(MessageShowEvent)},
                    
                    {OperationName.ColumnTools, typeof(ColumnToolsView)},
                    {OperationName.HeaderTools, typeof(HeaderToolsView)},

                    {OperationName.AlterAddress, typeof(AlterAddressView)},
                    {OperationName.AlterBaseEntity, typeof(AlterBaseEntityView)},
                    {OperationName.AlterCategory, typeof(AlterCategoryView)},
                    {OperationName.AlterContactInfo, typeof(AlterContactInfoView)},
                    {OperationName.AlterCustomer, typeof(AlterCustomerView)},
                    {OperationName.AlterEmail, typeof(AlterEmailView)},
                    {OperationName.AlterEmployee, typeof(AlterEmployeeView)},
                    {OperationName.AlterLegalPerson, typeof(AlterLegalPersonView)},
                    {OperationName.AlterNaturalPerson, typeof(AlterNaturalPersonView)},
                    {OperationName.AlterPayCheck, typeof(AlterPayCheckView)},
                    {OperationName.AlterPerson, typeof(AlterPersonView)},
                    {OperationName.AlterPhoneNumber, typeof(AlterPhoneNumberView)},
                    {OperationName.AlterProduct, typeof(AlterProductView)},
                    {OperationName.AlterSale, typeof(AlterSaleView)},
                    {OperationName.AlterService, typeof(AlterServiceView)},

                    //{OperationName.ListAddress, typeof(ListAddressView)},
                    {OperationName.ListBaseEntity, typeof(ListBaseEntityView)},
                    {OperationName.ListCategory, typeof(ListCategoryView)},
                    {OperationName.ListCustomer, typeof(ListCategoryView)},
                    //{OperationName.ListEmail, typeof(ListContactInfoView)},
                    {OperationName.ListEmployee, typeof(ListEmployeeView)},
                    //{OperationName.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{OperationName.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {OperationName.ListOp, typeof(ListOpView)},
                    //{OperationName.ListPayCheck, typeof(ListPayCheckView)},
                    //{OperationName.ListPerson, typeof(ListPersonView)},
                    {OperationName.ListPhoneNumber, typeof(ListPhoneNumberView)},
                    {OperationName.ListProduct, typeof(ListProductView)},
                    {OperationName.ListService, typeof(ListServiceView)},
                });
        #endregion

        #region ViewModel
        private static readonly Lazy<IDictionary<OperationName, Type>> lazyViewModel =
    new Lazy<IDictionary<OperationName, Type>>(() => new Dictionary<OperationName, Type>
                {
                    {OperationName.MessageHideEvent, typeof(MessageHideEvent)},
                    {OperationName.MessageShowEvent, typeof(MessageShowEvent)},
                    
                    {OperationName.ColumnTools, typeof(ColumnToolsViewModel)},
                    {OperationName.HeaderTools, typeof(HeaderToolsViewModel)},

                    {OperationName.AlterAddress, typeof(AlterAddressViewModel)},
                    {OperationName.AlterBaseEntity, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                    {OperationName.AlterCategory, typeof(AlterCategoryViewModel)},
                    {OperationName.AlterContactInfo, typeof(AlterContactInfoViewModel)},
                    {OperationName.AlterCustomer, typeof(AlterCustomerViewModel)},
                    {OperationName.AlterEmail, typeof(AlterEmailViewModel)},
                    {OperationName.AlterEmployee, typeof(AlterEmployeeViewModel)},
                    {OperationName.AlterLegalPerson, typeof(AlterLegalPersonViewModel)},
                    {OperationName.AlterNaturalPerson, typeof(AlterNaturalPersonViewModel)},
                    {OperationName.AlterPayCheck, typeof(AlterPayCheckViewModel)},
                    {OperationName.AlterPerson, typeof(AlterPersonViewModel<Person>)},
                    {OperationName.AlterPhoneNumber, typeof(AlterPhoneNumberViewModel)},
                    {OperationName.AlterProduct, typeof(AlterProductViewModel)},
                    {OperationName.AlterSale, typeof(AlterSaleViewModel)},
                    {OperationName.AlterService, typeof(AlterServiceViewModel<>)},

                    {OperationName.ListAddress, typeof(ListAddressViewModel)},
                    {OperationName.ListBaseEntity, typeof(ListBaseEntityViewModel<BaseEntity>)},
                    {OperationName.ListCategory, typeof(ListCategoryViewModel)},
                    {OperationName.ListCustomer, typeof(ListCustomerViewModel)},
                    {OperationName.ListEmail, typeof(ListEmailViewModel)},
                    {OperationName.ListEmployee, typeof(ListEmployeeViewModel)},
                    {OperationName.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    {OperationName.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {OperationName.ListOp, typeof(ListOpViewModel)},
                    {OperationName.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {OperationName.ListPayCheck, typeof(ListPayCheckViewModel)},
                    {OperationName.ListPerson, typeof(ListPersonViewModel<Person>)},
                    {OperationName.ListPhoneNumber, typeof(ListPhoneNumberViewModel)},
                    {OperationName.ListProduct, typeof(ListProductViewModel)},
                    {OperationName.ListService, typeof(ListServiceViewModel<Service>)},
                });
        #endregion

        public static IDictionary<OperationName, Type> Views { get { return lazyView.Value; } }
        public static IDictionary<OperationName, Type> ViewModels { get { return lazyViewModel.Value; } }

    }
}
