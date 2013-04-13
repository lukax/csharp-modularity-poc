#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.Operation;
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

        private readonly ICategoryFacade _categoryFacade;
        public AlterCategoryViewModel(Category entity, IRepository repository, ICategoryFacade categoryFacade, IEventAggregator eventAggregator,
            ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) {
            _categoryFacade = categoryFacade;
            Refresh();
        }

        public override void InitializeServices() {
            Operation = _operation;
            ClearEntity(null);
            EventAggregator.GetEvent<IncludeEntityEvent>().Subscribe(Include);
        }

        private void Include(BaseEntity baseEntity) {
            var entity = baseEntity as Category;
            if(entity == null) return;
            Entity = entity;
            Operation.State(UIOperationState.Update);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            if(Operation.State == UIOperationState.Add) return _categoryFacade.CanAdd(out results);
            if(Operation.State == UIOperationState.Update) return _categoryFacade.CanUpdate(out results);
            return false;
        }

        protected override bool CanCancel(object arg) {
            if(Operation.State == UIOperationState.Add) return true;
            if(Operation.State == UIOperationState.Update) return true;
            return false;
        }

        protected override void ClearEntity(object arg) {
            Entity = _categoryFacade.GenerateEntity();
            _categoryFacade.SetEntity(Entity);
            _categoryFacade.ConfigureValidations();
        }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Category, State = UIOperationState.Add};

    }
}