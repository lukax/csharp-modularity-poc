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
    public static class UIOperationMapping {
        #region View

        private static readonly Lazy<IDictionary<UIOperation, Type>> LazyView =
            new Lazy<IDictionary<UIOperation, Type>>(
                () => new Dictionary<UIOperation, Type> { //Alter
                    {
                        new UIOperation {
                            Type = UIOperationType.MessageTool,
                            State = UIOperationState.Alter
                        },
                        typeof(MessageToolView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.ColumnTool,
                            State = UIOperationState.Alter
                        },
                        typeof(ColumnToolView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.HeaderTool,
                            State = UIOperationState.Alter
                        },
                        typeof(HeaderToolView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Address,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterAddressView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.BaseEntity,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterBaseEntityView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Category,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterCategoryView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.ContactInfo,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterContactInfoView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Customer,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterCustomerView)
                    }, {
                        new UIOperation {Type = UIOperationType.Email, State = UIOperationState.Alter},
                        typeof(AlterEmailView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Employee,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterEmployeeView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.LegalPerson,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterLegalPersonView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.NaturalPerson,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterNaturalPersonView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.PayCheck,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterPayCheckView)
                    }, {
                        new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Alter}
                        , typeof(AlterPersonView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.PhoneNumber,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterPhoneNumberView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Product,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterProductView)
                    }, {
                        new UIOperation {Type = UIOperationType.Sale, State = UIOperationState.Alter},
                        typeof(AlterSaleView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Service,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterServiceView)
                    },

                    //List
                    //{new UIOperation{ Type = UIOperationType.Address}, typeof(ListAddressView)},
                    {
                        new UIOperation {
                            Type = UIOperationType.BaseEntity,
                            State = UIOperationState.List
                        },
                        typeof(ListBaseEntityView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Category,
                            State = UIOperationState.List
                        },
                        typeof(ListCategoryView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Customer,
                            State = UIOperationState.List
                        },
                        typeof(ListCustomerView)
                    },
                    //{UIOperation.ListContactInfo, typeof(ListContactInfoView)},
                    {
                        new UIOperation {
                            Type = UIOperationType.Employee,
                            State = UIOperationState.List
                        },
                        typeof(ListEmployeeView)
                    },
                    //{Command.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {
                        new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List},
                        typeof(ListOpView)
                    },
                    //{Command.ListPayCheck, typeof(ListPayCheckView)},
                    //{Command.ListPerson, typeof(ListPersonView)},
                    {
                        new UIOperation {
                            Type = UIOperationType.PhoneNumber,
                            State = UIOperationState.List
                        },
                        typeof(ListPhoneNumberView)
                    }, {
                        new UIOperation {Type = UIOperationType.Product, State = UIOperationState.List}
                        , typeof(ListProductView)
                    }, {
                        new UIOperation {Type = UIOperationType.Service, State = UIOperationState.List}
                        , typeof(ListServiceView)
                    },

                    //QuickSearch
                    {
                        new UIOperation {
                            Type = UIOperationType.Address,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListAddressView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.BaseEntity,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListBaseEntityView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Category,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListCategoryView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Customer,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListCustomerView)
                    },
                    //{UIOperation.ListContactInfo, typeof(ListContactInfoView)},
                    {
                        new UIOperation {
                            Type = UIOperationType.Employee,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListEmployeeView)
                    },
                    //{Command.ListLegalPerson, typeof(ListLegalPersonView)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonView)},
                    {
                        new UIOperation {
                            Type = UIOperationType.Op,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListOpView)
                    },
                    //{Command.ListPayCheck, typeof(ListPayCheckView)},
                    //{Command.ListPerson, typeof(ListPersonView)},
                    {
                        new UIOperation {
                            Type = UIOperationType.PhoneNumber,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListPhoneNumberView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Product,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListProductView)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Service,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListServiceView)
                    },
                });

        #endregion
        #region ViewModel

        private static readonly Lazy<IDictionary<UIOperation, Type>> LazyViewModel =
            new Lazy<IDictionary<UIOperation, Type>>(
                () => new Dictionary<UIOperation, Type> { //Alter
                    {
                        new UIOperation {
                            Type = UIOperationType.MessageTool,
                            State = UIOperationState.Alter
                        },
                        typeof(MessageToolViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.ColumnTool,
                            State = UIOperationState.Alter
                        },
                        typeof(ColumnToolViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.HeaderTool,
                            State = UIOperationState.Alter
                        },
                        typeof(HeaderToolViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Address,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterAddressViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.BaseEntity,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterBaseEntityViewModel<BaseEntity>)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Category,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterCategoryViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.ContactInfo,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterContactInfoViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Customer,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterCustomerViewModel)
                    }, {
                        new UIOperation {Type = UIOperationType.Email, State = UIOperationState.Alter},
                        typeof(AlterEmailViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Employee,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterEmployeeViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.LegalPerson,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterLegalPersonViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.NaturalPerson,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterNaturalPersonViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.PayCheck,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterPayCheckViewModel)
                    }, {
                        new UIOperation {Type = UIOperationType.Person, State = UIOperationState.Alter}
                        , typeof(AlterPersonViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.PhoneNumber,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterPhoneNumberViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Product,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterProductViewModel)
                    }, {
                        new UIOperation {Type = UIOperationType.Sale, State = UIOperationState.Alter},
                        typeof(AlterSaleViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Service,
                            State = UIOperationState.Alter
                        },
                        typeof(AlterServiceViewModel)
                    },

                    //List
                    {
                        new UIOperation {Type = UIOperationType.Address, State = UIOperationState.List}
                        , typeof(ListAddressViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.BaseEntity,
                            State = UIOperationState.List
                        },
                        typeof(ListBaseEntityViewModel<BaseEntity>)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Category,
                            State = UIOperationState.List
                        },
                        typeof(ListCategoryViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Customer,
                            State = UIOperationState.List
                        },
                        typeof(ListCustomerViewModel)
                    },
                    //{Command.ListEmail, typeof(ListContactInfoViewModel)},
                    {
                        new UIOperation {
                            Type = UIOperationType.Employee,
                            State = UIOperationState.List
                        },
                        typeof(ListEmployeeViewModel)
                    },
                    //{Command.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {
                        new UIOperation {Type = UIOperationType.Op, State = UIOperationState.List},
                        typeof(ListOpViewModel)
                    },
                    //{Command.ListPayCheck, typeof(ListPayCheckViewModel)},
                    //{Command.ListPerson, typeof(ListPersonViewModel)},
                    {
                        new UIOperation {
                            Type = UIOperationType.PhoneNumber,
                            State = UIOperationState.List
                        },
                        typeof(ListPhoneNumberViewModel)
                    }, {
                        new UIOperation {Type = UIOperationType.Product, State = UIOperationState.List}
                        , typeof(ListProductViewModel)
                    }, {
                        new UIOperation {Type = UIOperationType.Service, State = UIOperationState.List}
                        , typeof(ListServiceViewModel)
                    },

                    //QuickSearch
                    {
                        new UIOperation {
                            Type = UIOperationType.Address,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListAddressViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.BaseEntity,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListBaseEntityViewModel<BaseEntity>)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Category,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListCategoryViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Customer,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListCustomerViewModel)
                    },
                    //{Command.ListEmail, typeof(ListContactInfoViewModel)},
                    {
                        new UIOperation {
                            Type = UIOperationType.Employee,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListEmployeeViewModel)
                    },
                    //{Command.ListLegalPerson, typeof(ListLegalPersonViewModel)},
                    //{Command.ListNaturalPerson, typeof(ListNaturalPersonViewModel)},
                    {
                        new UIOperation {
                            Type = UIOperationType.Op,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListOpViewModel)
                    },
                    //{Command.ListPayCheck, typeof(ListPayCheckViewModel)},
                    //{Command.ListPerson, typeof(ListPersonViewModel)},
                    {
                        new UIOperation {
                            Type = UIOperationType.PhoneNumber,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListPhoneNumberViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Product,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListProductViewModel)
                    }, {
                        new UIOperation {
                            Type = UIOperationType.Service,
                            State = UIOperationState.QuickSearch
                        },
                        typeof(ListServiceViewModel)
                    },
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