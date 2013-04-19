#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    [Export(typeof(IListPayCheckViewModel))]
    public class ListPayCheckViewModel : ListBaseEntityViewModel<PayCheck>, IListPayCheckViewModel {
        [ImportingConstructor]
        public ListPayCheckViewModel(IRepository repository, IEventAggregator eventAggregator)
            : base(repository, eventAggregator) { }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }

        public new Expression<Func<PayCheck, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.PS.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Bonus.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CurrentSalary.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.PayCheck, State = ViewState.List};
    }
}