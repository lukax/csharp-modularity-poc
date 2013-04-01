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
    public static class UIOperationTypeMap {
        #region View

        private static readonly Lazy<IDictionary<UIOperation, Type>> LazyView =
            new Lazy<IDictionary<UIOperation, Type>>(
                () =>
                new Dictionary<UIOperation, Type> {
                    {new UIOperation {Type = UIOperationType.Employee}, typeof(AlterEmployeeView)},
                    {new UIOperation {Type = UIOperationType.MessageTool}, typeof(MessageToolView)},
                    {new UIOperation {Type = UIOperationType.ColumnTool}, typeof(ColumnToolView)},
                    {new UIOperation {Type = UIOperationType.HeaderTool}, typeof(HeaderToolView)},
                    {new UIOperation {Type = UIOperationType.Address}, typeof(AlterAddressView)},
                    {new UIOperation {Type = UIOperationType.BaseEntity}, typeof(AlterBaseEntityView)},
                    {new UIOperation {Type = UIOperationType.Category}, typeof(AlterCategoryView)},
                    {new UIOperation {Type = UIOperationType.ContactInfo}, typeof(AlterContactInfoView)},
                    {new UIOperation {Type = UIOperationType.Customer}, typeof(AlterCustomerView)},
                    {new UIOperation {Type = UIOperationType.Email}, typeof(AlterEmailView)},
                    {new UIOperation {Type = UIOperationType.Employee}, typeof(AlterEmployeeView)},
                    {new UIOperation {Type = UIOperationType.LegalPerson}, typeof(AlterLegalPersonView)},
                    {new UIOperation {Type = UIOperationType.NaturalPerson}, typeof(AlterNaturalPersonView)},
                    {new UIOperation {Type = UIOperationType.PayCheck}, typeof(AlterPayCheckView)},
                    {new UIOperation {Type = UIOperationType.Person}, typeof(AlterPersonView)},
                    {new UIOperation {Type = UIOperationType.PhoneNumber}, typeof(AlterPhoneNumberView)},
                    {new UIOperation {Type = UIOperationType.Product}, typeof(AlterProductView)},
                    {new UIOperation {Type = UIOperationType.Sale}, typeof(AlterSaleView)},
                    {new UIOperation {Type = UIOperationType.Service}, typeof(AlterServiceView)},

                    //{new UIOperation{ Type = UIOperationType.Address}, typeof(ListAddressView)},
                    {new UIOperation{ Type = UIOperationType.BaseEntity, State = UIOperationState.List}, typeof(ListBaseEntityView)},
                    {new UIOperation{ Type = UIOperationType.Category, State = UIOperationState.List}, typeof(ListCategoryView)},
                    {new UIOperation{ Type = UIOperationType.Customer, State = UIOperationState.List}, typeof(ListCustomerView)},
                    //{UIOperation.ListContactInfo, typeof(ListContactInfoView)},
                    {new UIOperation{ Type = UIOperationType.Employee, State = UIOperationState.List}, typeof(ListEmployeeView)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {new UIOperation{ Type = UIOperationType.Op, State = UIOperationState.List}, typeof(ListOpView)},
                    //{Command.ListPayCheck, typeof(ListPayCheckView)},
                    //{Command.ListPerson, typeof(ListPersonView)},
                    {new UIOperation{ Type = UIOperationType.PhoneNumber, State = UIOperationState.List}, typeof(ListPhoneNumberView)},
                    {new UIOperation{ Type = UIOperationType.Product, State = UIOperationState.List}, typeof(ListProductView)},
                    {new UIOperation{ Type = UIOperationType.Service, State = UIOperationState.List}, typeof(ListServiceView)},
                });

        #endregion
        #region ViewModel

        private static readonly Lazy<IDictionary<UIOperation, Type>> LazyViewModel =
            new Lazy<IDictionary<UIOperation, Type>>(
                () =>
                new Dictionary<UIOperation, Type> {
                    {new UIOperation {Type = UIOperationType.MessageTool}, typeof(MessageToolViewModel)},
                    {new UIOperation {Type = UIOperationType.ColumnTool}, typeof(ColumnToolViewModel)},
                    {new UIOperation {Type = UIOperationType.HeaderTool}, typeof(HeaderToolViewModel)},
                    {new UIOperation {Type = UIOperationType.Address}, typeof(AlterAddressViewModel)},
                    {new UIOperation {Type = UIOperationType.BaseEntity}, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                    {new UIOperation {Type = UIOperationType.Category}, typeof(AlterCategoryViewModel)},
                    {new UIOperation {Type = UIOperationType.ContactInfo}, typeof(AlterContactInfoViewModel)},
                    {new UIOperation {Type = UIOperationType.Customer}, typeof(AlterCustomerViewModel)},
                    {new UIOperation {Type = UIOperationType.Email}, typeof(AlterEmailViewModel)},
                    {new UIOperation {Type = UIOperationType.Employee}, typeof(AlterEmployeeViewModel)},
                    {new UIOperation {Type = UIOperationType.LegalPerson}, typeof(AlterLegalPersonViewModel)},
                    {new UIOperation {Type = UIOperationType.NaturalPerson}, typeof(AlterNaturalPersonViewModel)},
                    {new UIOperation {Type = UIOperationType.PayCheck}, typeof(AlterPayCheckViewModel)},
                    {new UIOperation {Type = UIOperationType.Person}, typeof(AlterPersonViewModel)},
                    {new UIOperation {Type = UIOperationType.PhoneNumber}, typeof(AlterPhoneNumberViewModel)},
                    {new UIOperation {Type = UIOperationType.Product}, typeof(AlterProductViewModel)},
                    {new UIOperation {Type = UIOperationType.Sale}, typeof(AlterSaleViewModel)},
                    {new UIOperation {Type = UIOperationType.Service}, typeof(AlterServiceViewModel)},

                    //{Command.ListAddress, typeof(ListAddressViewModel)},
                    {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.List}, typeof(ListBaseEntityViewModel<BaseEntity>)},
                    {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.List}, typeof(ListCategoryViewModel)},
                    {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.List}, typeof(ListCustomerViewModel)},
                    //{Command.ListEmail, typeof(ListContactInfoViewModel)},
                    {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.List}, typeof(ListEmployeeViewModel)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List}, typeof(ListOpViewModel)},
                    //{Command.ListPayCheck, typeof(ListPayCheckViewModel)},
                    //{Command.ListPerson, typeof(ListPersonViewModel)},
                    {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.List}, typeof(ListPhoneNumberViewModel)},
                    {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.List}, typeof(ListProductViewModel)},
                    {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.List}, typeof(ListServiceViewModel)},
                });

        #endregion
        public static IDictionary<UIOperation, Type> Views {
            get { return LazyView.Value; }
        }

        public static IDictionary<UIOperation, Type> ViewModels {
            get { return LazyViewModel.Value; }
        }

    }
}