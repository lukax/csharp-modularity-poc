#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Category = LOB.Domain.SubEntity.Category;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterCategoryViewModel : AlterBaseEntityViewModel<Category>, IAlterCategoryViewModel {

        private readonly ICategoryFacade _facade;
        private readonly IEventAggregator _eventAggregator;

        public AlterCategoryViewModel(Category entity, IRepository repository, ICategoryFacade facade,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _facade = facade;
            _eventAggregator = eventAggregator;
            Refresh();
        }

        public override void InitializeServices() {
            ClearEntity(null);
        }

        public override void Refresh() {
            ClearEntity(null);
        }

        private UIOperation _uiOperation = new UIOperation {Type = UIOperationType.Category, State = UIOperationState.Add};
        public override UIOperation UIOperation {
            get { return _uiOperation; }
        }

        protected override void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Entity = Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) {
            _eventAggregator.GetEvent<CloseViewEvent>().Publish(UIOperation);
        }

        protected override bool CanSaveChanges(object arg) {
            //TODO: If viewState == Add : ..., If viewState == Update : ....
            IEnumerable<ValidationResult> results;
            return _facade.CanAdd(out results);
        }

        protected override void QuickSearch(object arg) {
            _uiOperation.State = UIOperationState.QuickSearch;
            _eventAggregator.GetEvent<QuickSearchEvent>().Publish(UIOperation);
        }

        protected override void ClearEntity(object arg) {
            Entity = new Category {Name = "", Description = "", Code = 0};
            _facade.SetEntity(Entity);
            _facade.ConfigureValidations();
        }

    }
}