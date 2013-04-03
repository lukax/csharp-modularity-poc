#region Usings

using System;
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

        public override void Refresh() { throw new NotImplementedException(); }

        private UIOperation _operation = new UIOperation {
            Type = UIOperationType.Service,
            State = UIOperationState.List
        };
        public override UIOperation Operation {
            get { return _operation; }
            set { _operation = value; }
        }

    }
}