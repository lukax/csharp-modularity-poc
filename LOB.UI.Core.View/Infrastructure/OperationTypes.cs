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
using LOB.UI.Core.ViewModel.Main;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Infrastructure
{
    public static class OperationTypes
    {
        #region View

        private static readonly Lazy<IDictionary<string, Type>> LazyView =
            new Lazy<IDictionary<string, Type>>(() => new Dictionary<string, Type>
                {
                    {OperationName.MessageTools, typeof (MessageToolsView)},
                    {OperationName.ColumnTools, typeof (ColumnToolsView)},
                    {OperationName.HeaderTools, typeof (HeaderToolsView)},
                    {OperationName.AlterAddress, typeof (AlterAddressView)},
                    {OperationName.AlterBaseEntity, typeof (AlterBaseEntityView)},
                    {OperationName.AlterCategory, typeof (AlterCategoryView)},
                    {OperationName.AlterContactInfo, typeof (AlterContactInfoView)},
                    {OperationName.AlterCustomer, typeof (AlterCustomerView)},
                    {OperationName.AlterEmail, typeof (AlterEmailView)},
                    {OperationName.AlterEmployee, typeof (AlterEmployeeView)},
                    {OperationName.AlterLegalPerson, typeof (AlterLegalPersonView)},
                    {OperationName.AlterNaturalPerson, typeof (AlterNaturalPersonView)},
                    {OperationName.AlterPayCheck, typeof (AlterPayCheckView)},
                    {OperationName.AlterPerson, typeof (AlterPersonView)},
                    {OperationName.AlterPhoneNumber, typeof (AlterPhoneNumberView)},
                    {OperationName.AlterProduct, typeof (AlterProductView)},
                    {OperationName.AlterSale, typeof (AlterSaleView)},
                    {OperationName.AlterService, typeof (AlterServiceView)},

                    //{Command.ListAddress, typeof(ListAddressView)},
                    {OperationName.ListBaseEntity, typeof (ListBaseEntityView)},
                    {OperationName.ListCategory, typeof (ListCategoryView)},
                    {OperationName.ListCustomer, typeof (ListCategoryView)},
                    //{Command.ListEmail, typeof(ListContactInfoView)},
                    {OperationName.ListEmployee, typeof (ListEmployeeView)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {OperationName.ListOp, typeof (ListOpView)},
                    //{Command.ListPayCheck, typeof(ListPayCheckView)},
                    //{Command.ListPerson, typeof(ListPersonView)},
                    {OperationName.ListPhoneNumber, typeof (ListPhoneNumberView)},
                    {OperationName.ListProduct, typeof (ListProductView)},
                    {OperationName.ListService, typeof (ListServiceView)},
                });

        #endregion

        #region ViewModel

        private static readonly Lazy<IDictionary<string, Type>> LazyViewModel =
            new Lazy<IDictionary<string, Type>>(() => new Dictionary<string, Type>
                {
                    {OperationName.MessageTools, typeof (MessageToolsViewModel)},
                    {OperationName.ColumnTools, typeof (ColumnToolsViewModel)},
                    {OperationName.HeaderTools, typeof (HeaderToolsViewModel)},
                    {OperationName.AlterAddress, typeof (AlterAddressViewModel)},
                    {OperationName.AlterBaseEntity, typeof (AlterBaseEntityViewModel<BaseEntity>)},
                    {OperationName.AlterCategory, typeof (AlterCategoryViewModel)},
                    {OperationName.AlterContactInfo, typeof (AlterContactInfoViewModel)},
                    {OperationName.AlterCustomer, typeof (AlterCustomerViewModel)},
                    {OperationName.AlterEmail, typeof (AlterEmailViewModel)},
                    {OperationName.AlterEmployee, typeof (AlterEmployeeViewModel)},
                    {OperationName.AlterLegalPerson, typeof (AlterLegalPersonViewModel)},
                    {OperationName.AlterNaturalPerson, typeof (AlterNaturalPersonViewModel)},
                    {OperationName.AlterPayCheck, typeof (AlterPayCheckViewModel)},
                    {OperationName.AlterPerson, typeof (AlterPersonViewModel<Person>)},
                    {OperationName.AlterPhoneNumber, typeof (AlterPhoneNumberViewModel)},
                    {OperationName.AlterProduct, typeof (AlterProductViewModel)},
                    {OperationName.AlterSale, typeof (AlterSaleViewModel)},
                    {OperationName.AlterService, typeof (AlterServiceViewModel<Service>)},

                    //{Command.ListAddress, typeof(ListAddressViewModel)},
                    {OperationName.ListBaseEntity, typeof (ListBaseEntityViewModel<BaseEntity>)},
                    {OperationName.ListCategory, typeof (ListCategoryViewModel)},
                    {OperationName.ListCustomer, typeof (ListCategoryViewModel)},
                    //{Command.ListEmail, typeof(ListContactInfoViewModel)},
                    {OperationName.ListEmployee, typeof (ListEmployeeViewModel)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {OperationName.ListOp, typeof (ListOpViewModel)},
                    //{Command.ListPayCheck, typeof(ListPayCheckViewModel)},
                    //{Command.ListPerson, typeof(ListPersonViewModel)},
                    {OperationName.ListPhoneNumber, typeof (ListPhoneNumberViewModel)},
                    {OperationName.ListProduct, typeof (ListProductViewModel)},
                    {OperationName.ListService, typeof (ListServiceViewModel<Service>)},
                });

        #endregion

        public static IDictionary<string, Type> Views
        {
            get { return LazyView.Value; }
        }

        public static IDictionary<string, Type> ViewModels
        {
            get { return LazyViewModel.Value; }
        }
    }
}