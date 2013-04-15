#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
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

        public AlterCategoryViewModel(IRepository repository, ICategoryFacade categoryFacade, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(repository, eventAggregator, logger) { _categoryFacade = categoryFacade; }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultID;
            base.InitializeServices();
            EventAggregator.GetEvent<IncludeEntityEvent>().Subscribe(Include);
        }

        private void Include(BaseEntity baseEntity) {
            var entity = baseEntity as Category;
            if(entity == null) return;
            Entity = entity;
            ViewID.State(ViewState.Update);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            if(ViewID.State == ViewState.Add) return _categoryFacade.CanAdd(out results);
            if(ViewID.State == ViewState.Update) return _categoryFacade.CanUpdate(out results);
            return false;
        }

        protected override bool CanCancel(object arg) {
            if(ViewID.State == ViewState.Add) return true;
            if(ViewID.State == ViewState.Update) return true;
            return false;
        }

        protected override void ClearEntity(object arg) { Entity = _categoryFacade.GenerateEntity(); }

        private readonly ViewID _defaultID = new ViewID {Type = ViewType.Category, State = ViewState.Add};

        protected override void EntityChanged() {
            base.EntityChanged();
            _categoryFacade.Entity = Entity;
        }
    }
}