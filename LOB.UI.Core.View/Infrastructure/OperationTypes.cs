#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.UI.Core.Event;
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
                    {Operation.MessageTools, typeof (MessageToolsView)},
                    {Operation.ColumnTools, typeof (ColumnToolsView)},
                    {Operation.HeaderTools, typeof (HeaderToolsView)},
                    {Operation.AlterAddress, typeof (AlterAddressView)},
                    {Operation.AlterBaseEntity, typeof (AlterBaseEntityView)},
                    {Operation.AlterCategory, typeof (AlterCategoryView)},
                    {Operation.AlterContactInfo, typeof (AlterContactInfoView)},
                    {Operation.AlterCustomer, typeof (AlterCustomerView)},
                    {Operation.AlterEmail, typeof (AlterEmailView)},
                    {Operation.AlterEmployee, typeof (AlterEmployeeView)},
                    {Operation.AlterLegalPerson, typeof (AlterLegalPersonView)},
                    {Operation.AlterNaturalPerson, typeof (AlterNaturalPersonView)},
                    {Operation.AlterPayCheck, typeof (AlterPayCheckView)},
                    {Operation.AlterPerson, typeof (AlterPersonView)},
                    {Operation.AlterPhoneNumber, typeof (AlterPhoneNumberView)},
                    {Operation.AlterProduct, typeof (AlterProductView)},
                    {Operation.AlterSale, typeof (AlterSaleView)},
                    {Operation.AlterService, typeof (AlterServiceView)},

                    //{Command.ListAddress, typeof(ListAddressView)},
                    {Operation.ListBaseEntity, typeof (ListBaseEntityView)},
                    {Operation.ListCategory, typeof (ListCategoryView)},
                    {Operation.ListCustomer, typeof (ListCategoryView)},
                    //{Command.ListEmail, typeof(ListContactInfoView)},
                    {Operation.ListEmployee, typeof (ListEmployeeView)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {Operation.ListOp, typeof (ListOpView)},
                    //{Command.ListPayCheck, typeof(ListPayCheckView)},
                    //{Command.ListPerson, typeof(ListPersonView)},
                    {Operation.ListPhoneNumber, typeof (ListPhoneNumberView)},
                    {Operation.ListProduct, typeof (ListProductView)},
                    {Operation.ListService, typeof (ListServiceView)},
                });

        #endregion

        #region ViewModel

        private static readonly Lazy<IDictionary<string, Type>> LazyViewModel =
            new Lazy<IDictionary<string, Type>>(() => new Dictionary<string, Type>
                {
                    {Operation.MessageTools, typeof (MessageToolsViewModel)},
                    {Operation.ColumnTools, typeof (ColumnToolsViewModel)},
                    {Operation.HeaderTools, typeof (HeaderToolsViewModel)},
                    {Operation.AlterAddress, typeof (AlterAddressViewModel)},
                    {Operation.AlterBaseEntity, typeof (AlterBaseEntityViewModel<BaseEntity>)},
                    {Operation.AlterCategory, typeof (AlterCategoryViewModel)},
                    {Operation.AlterContactInfo, typeof (AlterContactInfoViewModel)},
                    {Operation.AlterCustomer, typeof (AlterCustomerViewModel)},
                    {Operation.AlterEmail, typeof (AlterEmailViewModel)},
                    {Operation.AlterEmployee, typeof (AlterEmployeeViewModel)},
                    {Operation.AlterLegalPerson, typeof (AlterLegalPersonViewModel)},
                    {Operation.AlterNaturalPerson, typeof (AlterNaturalPersonViewModel)},
                    {Operation.AlterPayCheck, typeof (AlterPayCheckViewModel)},
                    {Operation.AlterPerson, typeof (AlterPersonViewModel<Person>)},
                    {Operation.AlterPhoneNumber, typeof (AlterPhoneNumberViewModel)},
                    {Operation.AlterProduct, typeof (AlterProductViewModel)},
                    {Operation.AlterSale, typeof (AlterSaleViewModel)},
                    {Operation.AlterService, typeof (AlterServiceViewModel<>)},
                    {Operation.ListAddress, typeof (ListAddressViewModel)},
                    {Operation.ListBaseEntity, typeof (ListBaseEntityViewModel<BaseEntity>)},
                    {Operation.ListCategory, typeof (ListCategoryViewModel)},
                    {Operation.ListCustomer, typeof (ListCustomerViewModel)},
                    {Operation.ListEmail, typeof (ListEmailViewModel)},
                    {Operation.ListEmployee, typeof (ListEmployeeViewModel)},
                    {Operation.ListLegalPerson, typeof (ListLegalPersonViewModel)},
                    {Operation.ListNaturalPerson, typeof (ListNaturalPersonViewModel)},
                    {Operation.ListOp, typeof (ListOpViewModel)},
                    {Operation.ListNaturalPerson, typeof (ListNaturalPersonViewModel)},
                    {Operation.ListPayCheck, typeof (ListPayCheckViewModel)},
                    {Operation.ListPerson, typeof (ListPersonViewModel<Person>)},
                    {Operation.ListPhoneNumber, typeof (ListPhoneNumberViewModel)},
                    {Operation.ListProduct, typeof (ListProductViewModel)},
                    {Operation.ListService, typeof (ListServiceViewModel<Service>)},
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