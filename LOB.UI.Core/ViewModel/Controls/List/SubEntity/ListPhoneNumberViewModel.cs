﻿#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListPhoneNumberViewModel))]
    public class ListPhoneNumberViewModel : ListBaseEntityViewModel<PhoneNumber>, IListPhoneNumberViewModel {
        [ImportingConstructor]
        public ListPhoneNumberViewModel(IRepository repository, IEventAggregator eventAggregator)
            : base(repository, eventAggregator) { }

        public new Expression<Func<PhoneNumber, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Number.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.Type.ToString().ToUpper().Contains(Search.ToUpper()) ||
                         arg.Description.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }
    }
}