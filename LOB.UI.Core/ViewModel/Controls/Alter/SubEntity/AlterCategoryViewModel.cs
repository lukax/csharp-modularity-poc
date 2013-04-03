#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
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
            Operation = _operation;
            ClearEntity(null);
            _eventAggregator.GetEvent<IncludeEvent>().Subscribe(Include);
        }

        private void Include(BaseEntity baseEntity) {
            var entity = baseEntity as Category;
            if(entity == null) return;
            Entity = entity;
            Operation = new UIOperation {State = UIOperationState.Update, Type = Operation.Type};
        }

        public override void Refresh() { ClearEntity(null); }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Category,
            State = UIOperationState.Add
        };

        protected override void SaveChanges(object arg) {
            using(Repository.Uow.BeginTransaction()) {
                Entity = Repository.SaveOrUpdate(Entity);
                Repository.Uow.CommitTransaction();
            }
        }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override bool CanSaveChanges(object arg) {
            //TODO: If viewState == Add : ..., If viewState == Update : ....
            IEnumerable<ValidationResult> results;
            return _facade.CanAdd(out results);
        }

        protected override void ClearEntity(object arg) {
            Entity = new Category {Name = "", Description = "", Code = 0};
            _facade.SetEntity(Entity);
            _facade.ConfigureValidations();
        }

    }
}