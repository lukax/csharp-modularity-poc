#region Usings

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
    public sealed class ListCategoryViewModel : ListBaseEntityViewModel<Category>, IListCategoryViewModel {

        public ListCategoryViewModel(Category entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) { }

        public override void InitializeServices() {
            base.InitializeServices();
            if (Equals(Operation, default(ViewID))) Operation = _operation;
        }

        public override void Refresh() { Search = ""; }

        public new Expression<Func<Category, bool>> SearchCriteria {
            get {
                try {
                    return
                        (arg =>
                         arg.Code.ToString(Culture).ToUpper().Contains(Search.ToUpper()) || arg.Description.ToUpper().Contains(Search.ToUpper()) ||
                         arg.Name.ToUpper().Contains(Search.ToUpper()));
                } catch(FormatException) {
                    return arg => false;
                }
            }
        }

        private readonly ViewID _operation = new ViewID {Type = ViewType.Category, State = ViewState.List};

    }
}