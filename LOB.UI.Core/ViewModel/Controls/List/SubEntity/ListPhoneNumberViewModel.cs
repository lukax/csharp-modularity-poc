﻿#region Usings
using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    public class ListPhoneNumberViewModel : ListBaseEntityViewModel<PhoneNumber>, IListPhoneNumberViewModel {

        public ListPhoneNumberViewModel(PhoneNumber entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) {}

        public new Expression<Func<PhoneNumber, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Number.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.PhoneNumberType.ToString().ToUpper().Contains(this.Search.ToUpper()) ||
                         arg.Description.ToString().ToUpper().Contains(this.Search.ToUpper()));
                }
                catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void Refresh() {
            throw new NotImplementedException();
        }

        public override OperationType OperationType {
            get { return OperationType.ListPhoneNumber; }
        }

    }
}