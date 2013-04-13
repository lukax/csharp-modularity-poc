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
using LOB.UI.Core.View.Controls.Util;
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
    public static class UIOperationMapping {
        #region View

        private static readonly Lazy<IDictionary<UIOperation, Type>> LazyView =
            new Lazy<IDictionary<UIOperation, Type>>(() => new Dictionary<UIOperation, Type> {
                #region Internal
                {new UIOperation {Type = UIOperationType.MessageTool, State = UIOperationState.Internal}, typeof(MessageShowToolView)},
                {new UIOperation {Type = UIOperationType.ColumnTool, State = UIOperationState.Internal}, typeof(ColumnToolView)},
                {new UIOperation {Type = UIOperationType.HeaderTool, State = UIOperationState.Internal}, typeof(HeaderToolView)},
                {new UIOperation {Type = UIOperationType.NotificationTool, State = UIOperationState.Internal}, typeof(NotificationToolView)},

                #endregion   
                #region Add
                {new UIOperation {Type = UIOperationType.Address, State = UIOperationState.Add}, typeof(AlterAddressView)},
                {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.Add}, typeof(CodeView)},
                {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.Add}, typeof(AlterCategoryView)},
                {new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.Add}, typeof(AlterContactInfoView)},
                {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.Add}, typeof(AlterCustomerView)},
                {new UIOperation {Type = UIOperationType.Email, State = UIOperationState.Add}, typeof(AlterEmailView)},
                {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.Add}, typeof(AlterEmployeeView)},
                {new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.Add}, typeof(AlterLegalPersonView)},
                {new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.Add}, typeof(AlterNaturalPersonView)},
                {new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.Add}, typeof(AlterPayCheckView)},
                {new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Add}, typeof(AlterPersonView)},
                {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.Add}, typeof(AlterPhoneNumberView)},
                {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.Add}, typeof(AlterProductView)},
                {new UIOperation {Type = UIOperationType.Sale, State = UIOperationState.Add}, typeof(AlterSaleView)},
                {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Add}, typeof(AlterServiceView)},

                #endregion Add
                #region State
                {new UIOperation {Type = UIOperationType.Address, State = UIOperationState.Update}, typeof(AlterAddressView)},
                {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.Update}, typeof(CodeView)},
                {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.Update}, typeof(AlterCategoryView)},
                {new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.Update}, typeof(AlterContactInfoView)},
                {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.Update}, typeof(AlterCustomerView)},
                {new UIOperation {Type = UIOperationType.Email, State = UIOperationState.Update}, typeof(AlterEmailView)},
                {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.Update}, typeof(AlterEmployeeView)},
                {new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.Update}, typeof(AlterLegalPersonView)},
                {new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.Update}, typeof(AlterNaturalPersonView)},
                {new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.Update}, typeof(AlterPayCheckView)},
                {new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Update}, typeof(AlterPersonView)},
                {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.Update}, typeof(AlterPhoneNumberView)},
                {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.Update}, typeof(AlterProductView)},
                {new UIOperation {Type = UIOperationType.Sale, State = UIOperationState.Update}, typeof(AlterSaleView)},
                {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Update}, typeof(AlterServiceView)},

                #endregion
                #region List
                {new UIOperation {Type = UIOperationType.Address, State = UIOperationState.List}, typeof(ListAddressView)},
                {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.List}, typeof(ListBaseEntityView)},
                {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.List}, typeof(ListCategoryView)},
                {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.List}, typeof(ListCustomerView)},
                {new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.List}, typeof(ListContactInfoView)},
                {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.List}, typeof(ListEmployeeView)},
                {new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.List}, typeof(ListLegalPersonView)},
                {new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.List}, typeof(ListNaturalPersonView)},
                {new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List}, typeof(ListOpView)},
                {new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.List}, typeof(ListPayCheckView)},
                {new UIOperation {Type = UIOperationType.Person, State = UIOperationState.List}, typeof(ListPersonView)},
                {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.List}, typeof(ListPhoneNumberView)},
                {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.List}, typeof(ListProductView)},
                {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.List}, typeof(ListServiceView)},

                #endregion
                #region QuickSearch
                {new UIOperation {Type = UIOperationType.Address, State = UIOperationState.QuickSearch}, typeof(ListAddressView)},
                {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.QuickSearch}, typeof(ListBaseEntityView)},
                {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.QuickSearch}, typeof(ListCategoryView)},
                {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.QuickSearch}, typeof(ListCustomerView)},
                {new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.QuickSearch}, typeof(ListContactInfoView)},
                {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.QuickSearch}, typeof(ListEmployeeView)},
                {new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.QuickSearch}, typeof(ListLegalPersonView)},
                {new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.QuickSearch}, typeof(ListNaturalPersonView)},
                {new UIOperation {Type = UIOperationType.Op, State = UIOperationState.QuickSearch}, typeof(ListOpView)},
                {new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.QuickSearch}, typeof(ListPayCheckView)},
                {new UIOperation {Type = UIOperationType.Person, State = UIOperationState.QuickSearch}, typeof(ListPersonView)},
                {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.QuickSearch}, typeof(ListPhoneNumberView)},
                {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.QuickSearch}, typeof(ListProductView)},
                {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.QuickSearch}, typeof(ListServiceView)},

                #endregion
            });

        #endregion
        #region ViewModel

        private static readonly Lazy<IDictionary<UIOperation, Type>> LazyViewModel =
            new Lazy<IDictionary<UIOperation, Type>>(() => new Dictionary<UIOperation, Type> {
                #region Internal
                {new UIOperation {Type = UIOperationType.MessageTool, State = UIOperationState.Internal}, typeof(MessageToolViewModel)},
                {new UIOperation {Type = UIOperationType.ColumnTool, State = UIOperationState.Internal}, typeof(ColumnToolViewModel)},
                {new UIOperation {Type = UIOperationType.HeaderTool, State = UIOperationState.Internal}, typeof(HeaderToolViewModel)},
                {new UIOperation {Type = UIOperationType.NotificationTool, State = UIOperationState.Internal}, typeof(NotificationToolViewModel)},

                #endregion
                #region Add
                {new UIOperation {Type = UIOperationType.Address, State = UIOperationState.Add}, typeof(AlterAddressViewModel)},
                {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.Add}, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.Add}, typeof(AlterCategoryViewModel)},
                {new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.Add}, typeof(AlterContactInfoViewModel)},
                {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.Add}, typeof(AlterCustomerViewModel)},
                {new UIOperation {Type = UIOperationType.Email, State = UIOperationState.Add}, typeof(AlterEmailViewModel)},
                {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.Add}, typeof(AlterEmployeeViewModel)},
                {new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.Add}, typeof(AlterLegalPersonViewModel)},
                {new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.Add}, typeof(AlterNaturalPersonViewModel)},
                {new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.Add}, typeof(AlterPayCheckViewModel)},
                {new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Add}, typeof(AlterPersonViewModel)},
                {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.Add}, typeof(AlterPhoneNumberViewModel)},
                {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.Add}, typeof(AlterProductViewModel)},
                {new UIOperation {Type = UIOperationType.Sale, State = UIOperationState.Add}, typeof(AlterSaleViewModel)},
                {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Add}, typeof(AlterServiceViewModel)},

                #endregion
                #region Update
                {new UIOperation {Type = UIOperationType.Address, State = UIOperationState.Update}, typeof(AlterAddressViewModel)},
                {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.Update}, typeof(AlterBaseEntityViewModel<BaseEntity>)},
                {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.Update}, typeof(AlterCategoryViewModel)},
                {new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.Update}, typeof(AlterContactInfoViewModel)},
                {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.Update}, typeof(AlterCustomerViewModel)},
                {new UIOperation {Type = UIOperationType.Email, State = UIOperationState.Update}, typeof(AlterEmailViewModel)},
                {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.Update}, typeof(AlterEmployeeViewModel)},
                {new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.Update}, typeof(AlterLegalPersonViewModel)},
                {new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.Update}, typeof(AlterNaturalPersonViewModel)},
                {new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.Update}, typeof(AlterPayCheckViewModel)},
                {new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Update}, typeof(AlterPersonViewModel)},
                {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.Update}, typeof(AlterPhoneNumberViewModel)},
                {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.Update}, typeof(AlterProductViewModel)},
                {new UIOperation {Type = UIOperationType.Sale, State = UIOperationState.Update}, typeof(AlterSaleViewModel)},
                {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Update}, typeof(AlterServiceViewModel)},

                #endregion
                #region List
                {new UIOperation {Type = UIOperationType.Address, State = UIOperationState.List}, typeof(ListAddressViewModel)},
                {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.List}, typeof(ListBaseEntityViewModel<BaseEntity>)},
                {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.List}, typeof(ListCategoryViewModel)},
                {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.List}, typeof(ListCustomerViewModel)},
                {new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.List}, typeof(ListContactInfoViewModel)},
                {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.List}, typeof(ListEmployeeViewModel)},
                {new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.List}, typeof(ListLegalPersonViewModel)},
                {new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.List}, typeof(ListNaturalPersonViewModel)},
                {new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List}, typeof(ListOpViewModel)},
                {new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.List}, typeof(ListPayCheckViewModel)},
                {new UIOperation {Type = UIOperationType.Person, State = UIOperationState.List}, typeof(ListPersonViewModel)},
                {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.List}, typeof(ListPhoneNumberViewModel)},
                {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.List}, typeof(ListProductViewModel)},
                {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.List}, typeof(ListServiceViewModel)},

                #endregion
                #region QuickSearch
                {new UIOperation {Type = UIOperationType.Address, State = UIOperationState.QuickSearch}, typeof(ListAddressViewModel)}, 
                {new UIOperation {Type = UIOperationType.BaseEntity, State = UIOperationState.QuickSearch},typeof(ListBaseEntityViewModel<BaseEntity>)},
                {new UIOperation {Type = UIOperationType.Category, State = UIOperationState.QuickSearch}, typeof(ListCategoryViewModel)},
                {new UIOperation {Type = UIOperationType.Customer, State = UIOperationState.QuickSearch}, typeof(ListCustomerViewModel)},
                {new UIOperation {Type = UIOperationType.ContactInfo, State = UIOperationState.QuickSearch}, typeof(ListContactInfoViewModel)},
                {new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.QuickSearch}, typeof(ListEmployeeViewModel)},
                {new UIOperation {Type = UIOperationType.LegalPerson, State = UIOperationState.QuickSearch}, typeof(ListLegalPersonViewModel)},
                {new UIOperation {Type = UIOperationType.NaturalPerson, State = UIOperationState.QuickSearch}, typeof(ListNaturalPersonViewModel)},
                {new UIOperation {Type = UIOperationType.Op, State = UIOperationState.QuickSearch}, typeof(ListOpViewModel)},
                {new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.QuickSearch}, typeof(ListPayCheckViewModel)},
                {new UIOperation {Type = UIOperationType.Person, State = UIOperationState.QuickSearch}, typeof(ListPersonViewModel)},
                {new UIOperation {Type = UIOperationType.PhoneNumber, State = UIOperationState.QuickSearch}, typeof(ListPhoneNumberViewModel)},
                {new UIOperation {Type = UIOperationType.Product, State = UIOperationState.QuickSearch}, typeof(ListProductViewModel)},
                {new UIOperation {Type = UIOperationType.Service, State = UIOperationState.QuickSearch}, typeof(ListServiceViewModel)},

                #endregion
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