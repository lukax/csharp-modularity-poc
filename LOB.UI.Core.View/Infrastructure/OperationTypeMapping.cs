#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;
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
using LOB.UI.Core.ViewModel.Controls.Main;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    public static class OperationTypeMapping {
        #region View

        private static readonly Lazy<IDictionary<OperationType, Type>> LazyView =
            new Lazy<IDictionary<OperationType, Type>>(
                () =>
                new Dictionary<OperationType, Type> {
                    {OperationType.MessageTools, typeof(MessageToolsView)},
                    {OperationType.ColumnTools, typeof(ColumnToolsView)},
                    {OperationType.HeaderTools, typeof(HeaderToolsView)},
                    {OperationType.AlterAddress, typeof(AlterAddressView)},
                    {OperationType.AlterBaseEntity, typeof(AlterBaseEntityView)},
                    {OperationType.AlterCategory, typeof(AlterCategoryView)},
                    {OperationType.AlterContactInfo, typeof(AlterContactInfoView)},
                    {OperationType.AlterCustomer, typeof(AlterCustomerView)},
                    {OperationType.AlterEmail, typeof(AlterEmailView)},
                    {OperationType.AlterEmployee, typeof(AlterEmployeeView)},
                    {OperationType.AlterLegalPerson, typeof(AlterLegalPersonView)},
                    {OperationType.AlterNaturalPerson, typeof(AlterNaturalPersonView)},
                    {OperationType.AlterPayCheck, typeof(AlterPayCheckView)},
                    {OperationType.AlterPerson, typeof(AlterPersonView)},
                    {OperationType.AlterPhoneNumber, typeof(AlterPhoneNumberView)},
                    {OperationType.AlterProduct, typeof(AlterProductView)},
                    {OperationType.AlterSale, typeof(AlterSaleView)},
                    {OperationType.AlterService, typeof(AlterServiceView)},

                    //{Command.ListAddress, typeof(ListAddressView)},
                    {OperationType.ListBaseEntity, typeof(ListBaseEntityView)},
                    {OperationType.ListCategory, typeof(ListCategoryView)},
                    {OperationType.ListCustomer, typeof(ListCategoryView)},
                    //{OperationType.ListContactInfo, typeof(ListContactInfoView)},
                    {OperationType.ListEmployee, typeof(ListEmployeeView)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {OperationType.ListOp, typeof(ListOpView)},
                    //{Command.ListPayCheck, typeof(ListPayCheckView)},
                    //{Command.ListPerson, typeof(ListPersonView)},
                    {OperationType.ListPhoneNumber, typeof(ListPhoneNumberView)},
                    {OperationType.ListProduct, typeof(ListProductView)},
                    {OperationType.ListService, typeof(ListServiceView)},
                });

        #endregion
        #region ViewModel

        private static readonly Lazy<IDictionary<OperationType, Type>> LazyViewModel =
            new Lazy<IDictionary<OperationType, Type>>(
                () =>
                new Dictionary<OperationType, Type> {
                    {OperationType.MessageTools, typeof(MessageToolsViewModel)},
                    {OperationType.ColumnTools, typeof(ColumnToolsViewModel)},
                    {OperationType.HeaderTools, typeof(HeaderToolsViewModel)},
                    {OperationType.AlterAddress, typeof(AlterAddressViewModel)},
                    {OperationType.AlterBaseEntity, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                    {OperationType.AlterCategory, typeof(AlterCategoryViewModel)},
                    {OperationType.AlterContactInfo, typeof(AlterContactInfoViewModel)},
                    {OperationType.AlterCustomer, typeof(AlterCustomerViewModel)},
                    {OperationType.AlterEmail, typeof(AlterEmailViewModel)},
                    {OperationType.AlterEmployee, typeof(AlterEmployeeViewModel)},
                    {OperationType.AlterLegalPerson, typeof(AlterLegalPersonViewModel)},
                    {OperationType.AlterNaturalPerson, typeof(AlterNaturalPersonViewModel)},
                    {OperationType.AlterPayCheck, typeof(AlterPayCheckViewModel)},
                    {OperationType.AlterPerson, typeof(AlterPersonViewModel)},
                    {OperationType.AlterPhoneNumber, typeof(AlterPhoneNumberViewModel)},
                    {OperationType.AlterProduct, typeof(AlterProductViewModel)},
                    {OperationType.AlterSale, typeof(AlterSaleViewModel)},
                    {OperationType.AlterService, typeof(AlterServiceViewModel)},

                    //{Command.ListAddress, typeof(ListAddressViewModel)},
                    {OperationType.ListBaseEntity, typeof(ListBaseEntityViewModel<BaseEntity>)},
                    {OperationType.ListCategory, typeof(ListCategoryViewModel)},
                    {OperationType.ListCustomer, typeof(ListCategoryViewModel)},
                    //{Command.ListEmail, typeof(ListContactInfoViewModel)},
                    {OperationType.ListEmployee, typeof(ListEmployeeViewModel)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {OperationType.ListOp, typeof(ListOpViewModel)},
                    //{Command.ListPayCheck, typeof(ListPayCheckViewModel)},
                    //{Command.ListPerson, typeof(ListPersonViewModel)},
                    {OperationType.ListPhoneNumber, typeof(ListPhoneNumberViewModel)},
                    {OperationType.ListProduct, typeof(ListProductViewModel)},
                    {OperationType.ListService, typeof(ListServiceViewModel)},
                });

        #endregion
        public static IDictionary<OperationType, Type> Views {
            get { return LazyView.Value; }
        }

        public static IDictionary<OperationType, Type> ViewModels {
            get { return LazyViewModel.Value; }
        }

    }
}