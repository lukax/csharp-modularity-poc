#region Usings

using System;
using System.Linq.Expressions;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base {
    public class ListServiceViewModel : ListBaseEntityViewModel<Service>, IListServiceViewModel {

        public ListServiceViewModel(Service entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        public override void InitializeServices() {
            base.InitializeServices();
            if (Equals(Operation, default(UIOperation))) Operation = _operation;
        }

        public new Expression<Func<Service, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) ||
                         arg.Description.ToString(Culture).ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        public override void Refresh() { Search = ""; }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Service, State = UIOperationState.List};

    }
}