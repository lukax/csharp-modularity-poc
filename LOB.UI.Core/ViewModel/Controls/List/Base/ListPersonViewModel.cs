﻿#region Usings

using System;
using System.Globalization;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base {
    public class ListPersonViewModel : ListBaseEntityViewModel<Person>, IListPersonViewModel {
        protected ListPersonViewModel(IRepository repository, IEventAggregator eventAggregator)
            : base(repository, eventAggregator) { }

        public new Expression<Func<Person, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Notes.ToString(CultureInfo.InvariantCulture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }

        public override void Refresh() { Search = ""; }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Person, State = ViewState.List};
    }
}