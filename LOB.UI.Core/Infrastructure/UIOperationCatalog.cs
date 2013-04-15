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

        private static readonly Lazy<IList<ViewID>> LazyUIOperations = new Lazy<IList<ViewID>>(() => new List<ViewID> { //
            //Alter
            new ViewID {Type = ViewType.MessageTool, State = ViewState.Add},
            new ViewID {Type = ViewType.ColumnTool, State = ViewState.Add},
            new ViewID {Type = ViewType.HeaderTool, State = ViewState.Add},
            new ViewID {Type = ViewType.Address, State = ViewState.Add},
            new ViewID {Type = ViewType.BaseEntity, State = ViewState.Add},
            new ViewID {Type = ViewType.Category, State = ViewState.Add},
            new ViewID {Type = ViewType.ContactInfo, State = ViewState.Add},
            new ViewID {Type = ViewType.Customer, State = ViewState.Add},
            new ViewID {Type = ViewType.Email, State = ViewState.Add},
            new ViewID {Type = ViewType.Employee, State = ViewState.Add},
            new ViewID {Type = ViewType.LegalPerson, State = ViewState.Add},
            new ViewID {Type = ViewType.NaturalPerson, State = ViewState.Add},
            new ViewID {Type = ViewType.PayCheck, State = ViewState.Add},
            new ViewID {Type = ViewType.Person, State = ViewState.Add},
            new ViewID {Type = ViewType.PhoneNumber, State = ViewState.Add},
            new ViewID {Type = ViewType.Product, State = ViewState.Add},
            new ViewID {Type = ViewType.Sale, State = ViewState.Add},
            new ViewID {Type = ViewType.Service, State = ViewState.Add},

            //List
            //new Operation{ Type = ViewType.Address},
            new ViewID {Type = ViewType.BaseEntity, State = ViewState.List},
            new ViewID {Type = ViewType.Category, State = ViewState.List},
            new ViewID {Type = ViewType.Customer, State = ViewState.List},
            //Operation.ListContactInfo, typeof(ListContactInfoView),
            new ViewID {Type = ViewType.Employee, State = ViewState.List},
            //Command.ListLegalPerson, typeof(ListLegalPersonView),
            //Command.ListNaturalPerson, typeof(ListNaturalPersonView),
            new ViewID {Type = ViewType.Op, State = ViewState.List},
            //Command.ListPayCheck, typeof(ListPayCheckView),
            //Command.ListPerson, typeof(ListPersonView),
            new ViewID {Type = ViewType.PhoneNumber, State = ViewState.List},
            new ViewID {Type = ViewType.Product, State = ViewState.List},
            new ViewID {Type = ViewType.Service, State = ViewState.List},

            //Sell
            new ViewID {Type = ViewType.Product, State = ViewState.Sell},
            new ViewID {Type = ViewType.Service, State = ViewState.Sell},
        });

        #endregion
        #region IuiComponentModel

        private static readonly Lazy<IDictionary<ViewID, Type>> LazyViewModel =
            new Lazy<IDictionary<ViewID, Type>>(
                () =>
                new Dictionary<ViewID, Type> {
                    {new ViewID {Type = ViewType.MessageTool}, typeof(MessageToolViewModel)},
                    {new ViewID {Type = ViewType.ColumnTool}, typeof(ColumnToolViewModel)},
                    {new ViewID {Type = ViewType.HeaderTool}, typeof(HeaderToolViewModel)},
                    {new ViewID {Type = ViewType.Address}, typeof(AlterAddressViewModel)},
                    {new ViewID {Type = ViewType.BaseEntity}, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                    {new ViewID {Type = ViewType.Category}, typeof(AlterCategoryViewModel)},
                    {new ViewID {Type = ViewType.ContactInfo}, typeof(AlterContactInfoViewModel)},
                    {new ViewID {Type = ViewType.Customer}, typeof(AlterCustomerViewModel)},
                    {new ViewID {Type = ViewType.Email}, typeof(AlterEmailViewModel)},
                    {new ViewID {Type = ViewType.Employee}, typeof(AlterEmployeeViewModel)},
                    {new ViewID {Type = ViewType.LegalPerson}, typeof(AlterLegalPersonViewModel)},
                    {new ViewID {Type = ViewType.NaturalPerson}, typeof(AlterNaturalPersonViewModel)},
                    {new ViewID {Type = ViewType.PayCheck}, typeof(AlterPayCheckViewModel)},
                    {new ViewID {Type = ViewType.Person}, typeof(AlterPersonViewModel)},
                    {new ViewID {Type = ViewType.PhoneNumber}, typeof(AlterPhoneNumberViewModel)},
                    {new ViewID {Type = ViewType.Product}, typeof(AlterProductViewModel)},
                    {new ViewID {Type = ViewType.Sale}, typeof(AlterSaleViewModel)},
                    {new ViewID {Type = ViewType.BaseEntity, State = ViewState.List}, typeof(ListBaseEntityViewModel<BaseEntity>)},
                    {new ViewID {Type = ViewType.Category, State = ViewState.List}, typeof(ListCategoryViewModel)},
                    {new ViewID {Type = ViewType.Customer, State = ViewState.List}, typeof(ListCustomerViewModel)},
                    //{Command.ListEmail, typeof(ListContactInfoIuiComponentModel)},
                    {new ViewID {Type = ViewType.Employee, State = ViewState.List}, typeof(ListEmployeeViewModel)},
                    //{Command.ListLegalPerson, typeof(ListLegalPersonIuiComponentModel)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonIuiComponentModel)},
                    {new ViewID {Type = ViewType.Op, State = ViewState.List}, typeof(ListOpViewModel)},
                    //{Command.ListPayCheck, typeof(ListPayCheckIuiComponentModel)},
                    //{Command.ListPerson, typeof(ListPersonIuiComponentModel)},
                    {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.List}, typeof(ListPhoneNumberViewModel)},
                    {new ViewID {Type = ViewType.Product, State = ViewState.List}, typeof(ListProductViewModel)},
                });

        #endregion
        public static IList<ViewID> UIOperations {
            get { return LazyUIOperations.Value; }
        }

        public static IDictionary<ViewID, Type> ViewModels {
            get { return LazyViewModel.Value; }
        }
    }
}