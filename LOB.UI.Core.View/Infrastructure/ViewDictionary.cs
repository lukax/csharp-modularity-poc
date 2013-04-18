#region Usings

using System;
using System.Collections.Generic;
using LOB.UI.Core.View.Controls.Alter;
using LOB.UI.Core.View.Controls.Alter.Base;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Controls.List;
using LOB.UI.Core.View.Controls.List.Base;
using LOB.UI.Core.View.Controls.List.SubEntity;
using LOB.UI.Core.View.Controls.Main;
using LOB.UI.Core.View.Controls.Util;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    public static class ViewDictionary {
        private static readonly Lazy<IDictionary<ViewID, Type>> LazyView = new Lazy<IDictionary<ViewID, Type>>(() => new Dictionary<ViewID, Type> {
            #region Internal
            {new ViewID {Type = ViewType.MessageTool, State = ViewState.Internal}, typeof(MessageShowToolView)},
            {new ViewID {Type = ViewType.ColumnTool, State = ViewState.Internal}, typeof(ColumnToolView)},
            {new ViewID {Type = ViewType.HeaderTool, State = ViewState.Internal}, typeof(HeaderToolView)},
            {new ViewID {Type = ViewType.NotificationTool, State = ViewState.Internal}, typeof(NotificationToolView)},

            #endregion   
            #region Add
            {new ViewID {Type = ViewType.Address, State = ViewState.Add}, typeof(AlterAddressView)},
            {new ViewID {Type = ViewType.BaseEntity, State = ViewState.Add}, typeof(CodeView)},
            {new ViewID {Type = ViewType.Category, State = ViewState.Add}, typeof(AlterCategoryView)},
            {new ViewID {Type = ViewType.ContactInfo, State = ViewState.Add}, typeof(AlterContactInfoView)},
            {new ViewID {Type = ViewType.Customer, State = ViewState.Add}, typeof(AlterCustomerView)},
            {new ViewID {Type = ViewType.Email, State = ViewState.Add}, typeof(AlterEmailView)},
            {new ViewID {Type = ViewType.Employee, State = ViewState.Add}, typeof(AlterEmployeeView)},
            {new ViewID {Type = ViewType.LegalPerson, State = ViewState.Add}, typeof(AlterLegalPersonView)},
            {new ViewID {Type = ViewType.NaturalPerson, State = ViewState.Add}, typeof(AlterNaturalPersonView)},
            {new ViewID {Type = ViewType.PayCheck, State = ViewState.Add}, typeof(AlterPayCheckView)},
            {new ViewID {Type = ViewType.Person, State = ViewState.Add}, typeof(AlterPersonView)},
            {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.Add}, typeof(AlterPhoneNumberView)},
            {new ViewID {Type = ViewType.Product, State = ViewState.Add}, typeof(AlterProductView)},
            {new ViewID {Type = ViewType.Sale, State = ViewState.Add}, typeof(AlterSaleView)},

            #endregion Add
            #region State
            {new ViewID {Type = ViewType.Address, State = ViewState.Update}, typeof(AlterAddressView)},
            {new ViewID {Type = ViewType.BaseEntity, State = ViewState.Update}, typeof(CodeView)},
            {new ViewID {Type = ViewType.Category, State = ViewState.Update}, typeof(AlterCategoryView)},
            {new ViewID {Type = ViewType.ContactInfo, State = ViewState.Update}, typeof(AlterContactInfoView)},
            {new ViewID {Type = ViewType.Customer, State = ViewState.Update}, typeof(AlterCustomerView)},
            {new ViewID {Type = ViewType.Email, State = ViewState.Update}, typeof(AlterEmailView)},
            {new ViewID {Type = ViewType.Employee, State = ViewState.Update}, typeof(AlterEmployeeView)},
            {new ViewID {Type = ViewType.LegalPerson, State = ViewState.Update}, typeof(AlterLegalPersonView)},
            {new ViewID {Type = ViewType.NaturalPerson, State = ViewState.Update}, typeof(AlterNaturalPersonView)},
            {new ViewID {Type = ViewType.PayCheck, State = ViewState.Update}, typeof(AlterPayCheckView)},
            {new ViewID {Type = ViewType.Person, State = ViewState.Update}, typeof(AlterPersonView)},
            {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.Update}, typeof(AlterPhoneNumberView)},
            {new ViewID {Type = ViewType.Product, State = ViewState.Update}, typeof(AlterProductView)},
            {new ViewID {Type = ViewType.Sale, State = ViewState.Update}, typeof(AlterSaleView)},

            #endregion
            #region List
            {new ViewID {Type = ViewType.Address, State = ViewState.List}, typeof(ListAddressView)},
            {new ViewID {Type = ViewType.BaseEntity, State = ViewState.List}, typeof(ListBaseEntityView)},
            {new ViewID {Type = ViewType.Category, State = ViewState.List}, typeof(ListCategoryView)},
            {new ViewID {Type = ViewType.Customer, State = ViewState.List}, typeof(ListCustomerView)},
            {new ViewID {Type = ViewType.ContactInfo, State = ViewState.List}, typeof(ListContactInfoView)},
            {new ViewID {Type = ViewType.Employee, State = ViewState.List}, typeof(ListEmployeeView)},
            {new ViewID {Type = ViewType.LegalPerson, State = ViewState.List}, typeof(ListLegalPersonView)},
            {new ViewID {Type = ViewType.NaturalPerson, State = ViewState.List}, typeof(ListNaturalPersonView)},
            {new ViewID {Type = ViewType.Op, State = ViewState.List}, typeof(ListOpView)},
            {new ViewID {Type = ViewType.PayCheck, State = ViewState.List}, typeof(ListPayCheckView)},
            {new ViewID {Type = ViewType.Person, State = ViewState.List}, typeof(ListPersonView)},
            {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.List}, typeof(ListPhoneNumberView)},
            {new ViewID {Type = ViewType.Product, State = ViewState.List}, typeof(ListProductView)},

            #endregion
            #region QuickSearch
            {new ViewID {Type = ViewType.Address, State = ViewState.QuickSearch}, typeof(ListAddressView)},
            {new ViewID {Type = ViewType.BaseEntity, State = ViewState.QuickSearch}, typeof(ListBaseEntityView)},
            {new ViewID {Type = ViewType.Category, State = ViewState.QuickSearch}, typeof(ListCategoryView)},
            {new ViewID {Type = ViewType.Customer, State = ViewState.QuickSearch}, typeof(ListCustomerView)},
            {new ViewID {Type = ViewType.ContactInfo, State = ViewState.QuickSearch}, typeof(ListContactInfoView)},
            {new ViewID {Type = ViewType.Employee, State = ViewState.QuickSearch}, typeof(ListEmployeeView)},
            {new ViewID {Type = ViewType.LegalPerson, State = ViewState.QuickSearch}, typeof(ListLegalPersonView)},
            {new ViewID {Type = ViewType.NaturalPerson, State = ViewState.QuickSearch}, typeof(ListNaturalPersonView)},
            {new ViewID {Type = ViewType.Op, State = ViewState.QuickSearch}, typeof(ListOpView)},
            {new ViewID {Type = ViewType.PayCheck, State = ViewState.QuickSearch}, typeof(ListPayCheckView)},
            {new ViewID {Type = ViewType.Person, State = ViewState.QuickSearch}, typeof(ListPersonView)},
            {new ViewID {Type = ViewType.PhoneNumber, State = ViewState.QuickSearch}, typeof(ListPhoneNumberView)},
            {new ViewID {Type = ViewType.Product, State = ViewState.QuickSearch}, typeof(ListProductView)},

            #endregion
        });

        public static IDictionary<ViewID, Type> Views {
            get { return LazyView.Value; }
        }
    }
}