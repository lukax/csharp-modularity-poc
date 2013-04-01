#region Usings

using System;
using System.Collections.Generic;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Controls.Alter;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Core.ViewModel.Controls.List.SubEntity;
using LOB.UI.Core.ViewModel.Controls.Main;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.Infrastructure {
    public static class UIOperationCatalog {
        #region View

        private static readonly Lazy<IList<UIOperation>> LazyUIOperations =
            new Lazy<IList<UIOperation>>(
                () =>
                new List<UIOperation> {

                    //Alter
                    new UIOperation {Type = UIOperationType.MessageTool, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.ColumnTool, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.HeaderTool, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Address, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Category, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Email, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Product, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Sale, State = UIOperationState.Alter},
                    new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Alter},

                    //List
                    //new UIOperation{ Type = UIOperationType.Address},
                    new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.List},
                    new UIOperation {Type = UIOperationType.Category, State = UIOperationState.List},
                    new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.List},
                    //UIOperation.ListContactInfo, typeof(ListContactInfoView),
                    new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.List},
                    //Command.ListLegalPerson, typeof(ListLegalPersonView),
                    //Command.ListNaturalPerson, typeof(ListNaturalPersonView),
                    new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List},
                    //Command.ListPayCheck, typeof(ListPayCheckView),
                    //Command.ListPerson, typeof(ListPersonView),
                    new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.List},
                    new UIOperation {Type = UIOperationType.Product, State = UIOperationState.List},
                    new UIOperation {Type = UIOperationType.Service, State = UIOperationState.List},

                    //Sell
                    new UIOperation {Type = UIOperationType.Product, State = UIOperationState.Sell},
                    new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Sell},
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
                    {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.List},typeof(ListBaseEntityViewModel<BaseEntity>)}, 
                    {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.List},typeof(ListCategoryViewModel)}, 
                    {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.List},typeof(ListCustomerViewModel)},
                    //{Command.ListEmail, typeof(ListContactInfoViewModel)},
                    {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.List},typeof(ListEmployeeViewModel)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List}, typeof(ListOpViewModel)},
                    //{Command.ListPayCheck, typeof(ListPayCheckViewModel)},
                    //{Command.ListPerson, typeof(ListPersonViewModel)},
                    {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.List},typeof(ListPhoneNumberViewModel)}, 
                    {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.List},typeof(ListProductViewModel)}, 
                    {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.List},typeof(ListServiceViewModel)},
                });

        #endregion
        public static IList<UIOperation> UIOperations {
            get { return LazyUIOperations.Value; }
        }

        public static IDictionary<UIOperation, Type> ViewModels {
            get { return LazyViewModel.Value; }
        }

    }
}