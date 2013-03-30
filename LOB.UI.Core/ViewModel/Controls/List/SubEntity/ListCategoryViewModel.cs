#region Usings
using LOB.Dao.Interface;
using LOB.Domain.SubEntity;
using LOB.UI.Core.ViewModel.Controls.List.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.SubEntity {
    public sealed class ListCategoryViewModel : ListServiceViewModel, IListCategoryViewModel {

        public ListCategoryViewModel(Category entity, IRepository repository, IEventAggregator eventAggregator)
            : base(entity, repository, eventAggregator) {}

        public override OperationType OperationType {
            get { return OperationType.AlterCategory; }
        }

    }
}