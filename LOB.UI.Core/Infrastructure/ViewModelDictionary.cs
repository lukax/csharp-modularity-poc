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
    public static class ViewModelDictionary {
        private static readonly Lazy<IDictionary<ViewID, Type>> LazyViewModel =
            new Lazy<IDictionary<ViewID, Type>>(() => new Dictionary<ViewID, Type> {
                #region Internal
                {new ViewID {Type = ViewType.MessageTool, State = ViewState.Internal}, typeof(MessageToolViewModel)},
                {new ViewID {Type = ViewType.ColumnTool, State = ViewState.Internal}, typeof(ColumnToolViewModel)},
                {new ViewID {Type = ViewType.HeaderTool, State = ViewState.Internal}, typeof(HeaderToolViewModel)},
                {new ViewID {Type = ViewType.NotificationTool, State = ViewState.Internal}, typeof(NotificationToolViewModel)},

                #endregion
                #region Add
                {new ViewID {Type = ViewType.Address, State = ViewState.Add}, typeof(AlterAddressViewModel)},
                {new ViewID {Type = ViewType.BaseEntity, State = ViewState.Add}, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                {new ViewID {Type = ViewType.Category, State = ViewState.Add}, typeof(AlterCategoryViewModel)},
                {new ViewID {Type = ViewType.ContactInfo, State = ViewState.Add}, typeof(AlterContactInfoViewModel)},
                {new ViewID {Type = ViewType.Customer, State = ViewState.Add}, typeof(AlterCustomerViewModel)},
                {new ViewID {Type = ViewType.Email, State = ViewState.Add}, typeof(AlterEmailViewModel)},
                {new ViewID {Type = ViewType.Employee, State = ViewState.Add}, typeof(AlterEmployeeViewModel)},
                {new ViewID {Type = ViewType.LegalPerson, State = ViewState.Add}, typeof(AlterLegalPersonViewModel)},
                {new ViewID {Type = ViewType.NaturalPerson, State = ViewState.Add}, typeof(AlterNaturalPersonViewModel)},
                {new ViewID {Type = ViewType.PayCheck, State = ViewState.Add}, typeof(AlterPayCheckViewModel)},
                {new ViewID {Type = ViewType.Person, State = ViewState.Add}, typeof(AlterPersonViewModel)},
                {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.Add}, typeof(AlterPhoneNumberViewModel)},
                {new ViewID {Type = ViewType.Product, State = ViewState.Add}, typeof(AlterProductViewModel)},
                {new ViewID {Type = ViewType.Sale, State = ViewState.Add}, typeof(AlterSaleViewModel)},

                #endregion
                #region Update
                {new ViewID {Type = ViewType.Address, State = ViewState.Update}, typeof(AlterAddressViewModel)},
                {new ViewID {Type = ViewType.BaseEntity, State = ViewState.Update}, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                {new ViewID {Type = ViewType.Category, State = ViewState.Update}, typeof(AlterCategoryViewModel)},
                {new ViewID {Type = ViewType.ContactInfo, State = ViewState.Update}, typeof(AlterContactInfoViewModel)},
                {new ViewID {Type = ViewType.Customer, State = ViewState.Update}, typeof(AlterCustomerViewModel)},
                {new ViewID {Type = ViewType.Email, State = ViewState.Update}, typeof(AlterEmailViewModel)},
                {new ViewID {Type = ViewType.Employee, State = ViewState.Update}, typeof(AlterEmployeeViewModel)},
                {new ViewID {Type = ViewType.LegalPerson, State = ViewState.Update}, typeof(AlterLegalPersonViewModel)},
                {new ViewID {Type = ViewType.NaturalPerson, State = ViewState.Update}, typeof(AlterNaturalPersonViewModel)},
                {new ViewID {Type = ViewType.PayCheck, State = ViewState.Update}, typeof(AlterPayCheckViewModel)},
                {new ViewID {Type = ViewType.Person, State = ViewState.Update}, typeof(AlterPersonViewModel)},
                {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.Update}, typeof(AlterPhoneNumberViewModel)},
                {new ViewID {Type = ViewType.Product, State = ViewState.Update}, typeof(AlterProductViewModel)},
                {new ViewID {Type = ViewType.Sale, State = ViewState.Update}, typeof(AlterSaleViewModel)},

                #endregion
                #region List
                {new ViewID {Type = ViewType.Address, State = ViewState.List}, typeof(ListAddressViewModel)},
                {new ViewID {Type = ViewType.BaseEntity, State = ViewState.List}, typeof(ListBaseEntityViewModel<BaseEntity>)},
                {new ViewID {Type = ViewType.Category, State = ViewState.List}, typeof(ListCategoryViewModel)},
                {new ViewID {Type = ViewType.Customer, State = ViewState.List}, typeof(ListCustomerViewModel)},
                {new ViewID {Type = ViewType.ContactInfo, State = ViewState.List}, typeof(ListContactInfoViewModel)},
                {new ViewID {Type = ViewType.Employee, State = ViewState.List}, typeof(ListEmployeeViewModel)},
                {new ViewID {Type = ViewType.LegalPerson, State = ViewState.List}, typeof(ListLegalPersonViewModel)},
                {new ViewID {Type = ViewType.NaturalPerson, State = ViewState.List}, typeof(ListNaturalPersonViewModel)},
                {new ViewID {Type = ViewType.Op, State = ViewState.List}, typeof(ListOpViewModel)},
                {new ViewID {Type = ViewType.PayCheck, State = ViewState.List}, typeof(ListPayCheckViewModel)},
                {new ViewID {Type = ViewType.Person, State = ViewState.List}, typeof(ListPersonViewModel)},
                {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.List}, typeof(ListPhoneNumberViewModel)},
                {new ViewID {Type = ViewType.Product, State = ViewState.List}, typeof(ListProductViewModel)},

                #endregion
                #region QuickSearch
                {new ViewID {Type = ViewType.Address, State = ViewState.QuickSearch}, typeof(ListAddressViewModel)},
                {new ViewID {Type = ViewType.BaseEntity, State = ViewState.QuickSearch}, typeof(ListBaseEntityViewModel<BaseEntity>)},
                {new ViewID {Type = ViewType.Category, State = ViewState.QuickSearch}, typeof(ListCategoryViewModel)},
                {new ViewID {Type = ViewType.Customer, State = ViewState.QuickSearch}, typeof(ListCustomerViewModel)},
                {new ViewID {Type = ViewType.ContactInfo, State = ViewState.QuickSearch}, typeof(ListContactInfoViewModel)},
                {new ViewID {Type = ViewType.Employee, State = ViewState.QuickSearch}, typeof(ListEmployeeViewModel)},
                {new ViewID {Type = ViewType.LegalPerson, State = ViewState.QuickSearch}, typeof(ListLegalPersonViewModel)},
                {new ViewID {Type = ViewType.NaturalPerson, State = ViewState.QuickSearch}, typeof(ListNaturalPersonViewModel)},
                {new ViewID {Type = ViewType.Op, State = ViewState.QuickSearch}, typeof(ListOpViewModel)},
                {new ViewID {Type = ViewType.PayCheck, State = ViewState.QuickSearch}, typeof(ListPayCheckViewModel)},
                {new ViewID {Type = ViewType.Person, State = ViewState.QuickSearch}, typeof(ListPersonViewModel)},
                {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.QuickSearch}, typeof(ListPhoneNumberViewModel)},
                {new ViewID {Type = ViewType.Product, State = ViewState.QuickSearch}, typeof(ListProductViewModel)},

                #endregion
            });

        public static IDictionary<ViewID, Type> ViewModels {
            get { return LazyViewModel.Value; }
        }
    }
}