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
    public static class OperationTypes
    {
        #region View
        private static readonly Lazy<IDictionary<OperationNames, Type>> lazyView =
            new Lazy<IDictionary<OperationNames, Type>>(() => new Dictionary<OperationNames, Type>
                {
                    {OperationNames.MessageHideEvent, typeof(MessageHideEvent)},
                    {OperationNames.MessageShowEvent, typeof(MessageShowEvent)},
                    
                    {OperationNames.ColumnTools, typeof(ColumnToolsView)},
                    {OperationNames.HeaderTools, typeof(HeaderToolsView)},

                    {OperationNames.AlterAddress, typeof(AlterAddressView)},
                    {OperationNames.AlterBaseEntity, typeof(AlterBaseEntityView)},
                    {OperationNames.AlterCategory, typeof(AlterCategoryView)},
                    {OperationNames.AlterContactInfo, typeof(AlterContactInfoView)},
                    {OperationNames.AlterCustomer, typeof(AlterCustomerView)},
                    {OperationNames.AlterEmail, typeof(AlterEmailView)},
                    {OperationNames.AlterEmployee, typeof(AlterEmployeeView)},
                    {OperationNames.AlterLegalPerson, typeof(AlterLegalPersonView)},
                    {OperationNames.AlterNaturalPerson, typeof(AlterNaturalPersonView)},
                    {OperationNames.AlterPayCheck, typeof(AlterPayCheckView)},
                    {OperationNames.AlterPerson, typeof(AlterPersonView)},
                    {OperationNames.AlterPhoneNumber, typeof(AlterPhoneNumberView)},
                    {OperationNames.AlterProduct, typeof(AlterProductView)},
                    {OperationNames.AlterSale, typeof(AlterSaleView)},
                    {OperationNames.AlterService, typeof(AlterServiceView)},

                    //{OperationNames.ListAddress, typeof(ListAddressView)},
                    {OperationNames.ListBaseEntity, typeof(ListBaseEntityView)},
                    {OperationNames.ListCategory, typeof(ListCategoryView)},
                    {OperationNames.ListCustomer, typeof(ListCategoryView)},
                    //{OperationNames.ListEmail, typeof(ListContactInfoView)},
                    {OperationNames.ListEmployee, typeof(ListEmployeeView)},
                    //{OperationNames.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{OperationNames.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {OperationNames.ListOp, typeof(ListOpView)},
                    //{OperationNames.ListPayCheck, typeof(ListPayCheckView)},
                    //{OperationNames.ListPerson, typeof(ListPersonView)},
                    {OperationNames.ListPhoneNumber, typeof(ListPhoneNumberView)},
                    {OperationNames.ListProduct, typeof(ListProductView)},
                    {OperationNames.ListService, typeof(ListServiceView)},
                });
        #endregion

        #region ViewModel
        private static readonly Lazy<IDictionary<OperationNames, Type>> lazyViewModel =
    new Lazy<IDictionary<OperationNames, Type>>(() => new Dictionary<OperationNames, Type>
                {
                    {OperationNames.MessageHideEvent, typeof(MessageHideEvent)},
                    {OperationNames.MessageShowEvent, typeof(MessageShowEvent)},
                    
                    {OperationNames.ColumnTools, typeof(ColumnToolsViewModel)},
                    {OperationNames.HeaderTools, typeof(HeaderToolsViewModel)},

                    {OperationNames.AlterAddress, typeof(AlterAddressViewModel)},
                    {OperationNames.AlterBaseEntity, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                    {OperationNames.AlterCategory, typeof(AlterCategoryViewModel)},
                    {OperationNames.AlterContactInfo, typeof(AlterContactInfoViewModel)},
                    {OperationNames.AlterCustomer, typeof(AlterCustomerViewModel)},
                    {OperationNames.AlterEmail, typeof(AlterEmailViewModel)},
                    {OperationNames.AlterEmployee, typeof(AlterEmployeeViewModel)},
                    {OperationNames.AlterLegalPerson, typeof(AlterLegalPersonViewModel)},
                    {OperationNames.AlterNaturalPerson, typeof(AlterNaturalPersonViewModel)},
                    {OperationNames.AlterPayCheck, typeof(AlterPayCheckViewModel)},
                    {OperationNames.AlterPerson, typeof(AlterPersonViewModel<Person>)},
                    {OperationNames.AlterPhoneNumber, typeof(AlterPhoneNumberViewModel)},
                    {OperationNames.AlterProduct, typeof(AlterProductViewModel)},
                    {OperationNames.AlterSale, typeof(AlterSaleViewModel)},
                    {OperationNames.AlterService, typeof(AlterServiceViewModel<>)},

                    {OperationNames.ListAddress, typeof(ListAddressViewModel)},
                    {OperationNames.ListBaseEntity, typeof(ListBaseEntityViewModel<BaseEntity>)},
                    {OperationNames.ListCategory, typeof(ListCategoryViewModel)},
                    {OperationNames.ListCustomer, typeof(ListCustomerViewModel)},
                    {OperationNames.ListEmail, typeof(ListEmailViewModel)},
                    {OperationNames.ListEmployee, typeof(ListEmployeeViewModel)},
                    {OperationNames.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    {OperationNames.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {OperationNames.ListOp, typeof(ListOpViewModel)},
                    {OperationNames.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {OperationNames.ListPayCheck, typeof(ListPayCheckViewModel)},
                    {OperationNames.ListPerson, typeof(ListPersonViewModel<Person>)},
                    {OperationNames.ListPhoneNumber, typeof(ListPhoneNumberViewModel)},
                    {OperationNames.ListProduct, typeof(ListProductViewModel)},
                    {OperationNames.ListService, typeof(ListServiceViewModel<Service>)},
                });
        #endregion

        public static IDictionary<OperationNames, Type> Views { get { return lazyView.Value; } }
        public static IDictionary<OperationNames, Type> ViewModels { get { return lazyViewModel.Value; } }

    }
}
