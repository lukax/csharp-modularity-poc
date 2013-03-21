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
    public static class OperationTypeMapping
    {
        #region View

        private static readonly Lazy<IDictionary<OperationType, Type>> LazyView =
            new Lazy<IDictionary<OperationType, Type>>(() => new Dictionary<OperationType, Type>
                {
                    {OperationType.MessageTools, typeof (MessageToolsView)},
                    {OperationType.ColumnTools, typeof (ColumnToolsView)},
                    {OperationType.HeaderTools, typeof (HeaderToolsView)},

                    {OperationType.NewAddress, typeof (AlterAddressView)},
                    {OperationType.NewBaseEntity, typeof (AlterBaseEntityView)},
                    {OperationType.NewCategory, typeof (AlterCategoryView)},
                    {OperationType.NewContactInfo, typeof (AlterContactInfoView)},
                    {OperationType.NewCustomer, typeof (AlterCustomerView)},
                    {OperationType.NewEmail, typeof (AlterEmailView)},
                    {OperationType.NewEmployee, typeof (AlterEmployeeView)},
                    {OperationType.NewLegalPerson, typeof (AlterLegalPersonView)},
                    {OperationType.NewNaturalPerson, typeof (AlterNaturalPersonView)},
                    {OperationType.NewPayCheck, typeof (AlterPayCheckView)},
                    {OperationType.NewPerson, typeof (AlterPersonView)},
                    {OperationType.NewPhoneNumber, typeof (AlterPhoneNumberView)},
                    {OperationType.NewProduct, typeof (AlterProductView)},
                    {OperationType.NewSale, typeof (AlterSaleView)},
                    {OperationType.NewService, typeof (AlterServiceView)},

                    //{Command.ListAddress, typeof(ListAddressView)},
                    {OperationType.ListBaseEntity, typeof (ListBaseEntityView)},
                    {OperationType.ListCategory, typeof (ListCategoryView)},
                    {OperationType.ListCustomer, typeof (ListCategoryView)},
                    //{OperationType.ListContactInfo, typeof(ListContactInfoView)},
                    {OperationType.ListEmployee, typeof (ListEmployeeView)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {OperationType.ListOp, typeof (ListOpView)},
                    //{Command.ListPayCheck, typeof(ListPayCheckView)},
                    //{Command.ListPerson, typeof(ListPersonView)},
                    {OperationType.ListPhoneNumber, typeof (ListPhoneNumberView)},
                    {OperationType.ListProduct, typeof (ListProductView)},
                    {OperationType.ListService, typeof (ListServiceView)},
                });

        #endregion

        #region ViewModel

        private static readonly Lazy<IDictionary<OperationType, Type>> LazyViewModel =
            new Lazy<IDictionary<OperationType, Type>>(() => new Dictionary<OperationType, Type>
                {
                    {OperationType.MessageTools, typeof (MessageToolsViewModel)},
                    {OperationType.ColumnTools, typeof (ColumnToolsViewModel)},
                    {OperationType.HeaderTools, typeof (HeaderToolsViewModel)},

                    {OperationType.NewAddress, typeof (AlterAddressViewModel)},
                    {OperationType.NewBaseEntity, typeof (AlterBaseEntityViewModel<BaseEntity>)},
                    {OperationType.NewCategory, typeof (AlterCategoryViewModel)},
                    {OperationType.NewContactInfo, typeof (AlterContactInfoViewModel)},
                    {OperationType.NewCustomer, typeof (AlterCustomerViewModel)},
                    {OperationType.NewEmail, typeof (AlterEmailViewModel)},
                    {OperationType.NewEmployee, typeof (AlterEmployeeViewModel)},
                    {OperationType.NewLegalPerson, typeof (AlterLegalPersonViewModel)},
                    {OperationType.NewNaturalPerson, typeof (AlterNaturalPersonViewModel)},
                    {OperationType.NewPayCheck, typeof (AlterPayCheckViewModel)},
                    {OperationType.NewPerson, typeof (AlterPersonViewModel<Person>)},
                    {OperationType.NewPhoneNumber, typeof (AlterPhoneNumberViewModel)},
                    {OperationType.NewProduct, typeof (AlterProductViewModel)},
                    {OperationType.NewSale, typeof (AlterSaleViewModel)},
                    {OperationType.NewService, typeof (AlterServiceViewModel<Service>)},

                    //{Command.ListAddress, typeof(ListAddressViewModel)},
                    {OperationType.ListBaseEntity, typeof (ListBaseEntityViewModel<BaseEntity>)},
                    {OperationType.ListCategory, typeof (ListCategoryViewModel)},
                    {OperationType.ListCustomer, typeof (ListCategoryViewModel)},
                    //{Command.ListEmail, typeof(ListContactInfoViewModel)},
                    {OperationType.ListEmployee, typeof (ListEmployeeViewModel)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {OperationType.ListOp, typeof (ListOpViewModel)},
                    //{Command.ListPayCheck, typeof(ListPayCheckViewModel)},
                    //{Command.ListPerson, typeof(ListPersonViewModel)},
                    {OperationType.ListPhoneNumber, typeof (ListPhoneNumberViewModel)},
                    {OperationType.ListProduct, typeof (ListProductViewModel)},
                    {OperationType.ListService, typeof (ListServiceViewModel<Service>)},
                });

        #endregion

        public static IDictionary<OperationType, Type> Views
        {
            get { return LazyView.Value; }
        }

        public static IDictionary<OperationType, Type> ViewModels
        {
            get { return LazyViewModel.Value; }
        }
    }
}