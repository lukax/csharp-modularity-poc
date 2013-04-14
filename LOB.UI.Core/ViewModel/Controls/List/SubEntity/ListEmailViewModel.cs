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
    public class ListEmailViewModel : ListBaseEntityViewModel<Email>, IListEmailViewModel {

        public ListEmailViewModel(Email entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        public new Expression<Func<Email, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Value.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void InitializeServices() { if (Equals(Operation, default(ViewID))) Operation = _operation; }

        public override void Refresh() { Search = ""; }

        private readonly ViewID _operation = new ViewID {Type = ViewType.Email, State = ViewState.List};

    }
}