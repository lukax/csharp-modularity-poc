#region Usings

using System;
using System.Linq.Expressions;
using System.Threading;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    public class ListPayCheckViewModel : ListBaseEntityViewModel<PayCheck>, IListPayCheckViewModel {

        public ListPayCheckViewModel(PayCheck entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        public new Expression<Func<PayCheck, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Thread.CurrentThread.CurrentCulture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Ps.ToString(Thread.CurrentThread.CurrentCulture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Bonus.ToString(Thread.CurrentThread.CurrentCulture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.CurrentSalary.ToString(Thread.CurrentThread.CurrentCulture)
                            .ToUpper()
                            .Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void Refresh() { throw new NotImplementedException(); }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.PayCheck,
            State = UIOperationState.List
        };
        public override UIOperation UIOperation {
            get { return _operation; }
        }

    }
}