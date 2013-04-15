#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.Domain.SubEntity;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterEmailViewModel : AlterBaseEntityViewModel<Email>, IAlterEmailViewModel {
        private readonly IEmailFacade _emailFacade;

        public AlterEmailViewModel(IRepository repository, IEmailFacade emailFacade, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(repository, eventAggregator, logger) { _emailFacade = emailFacade; }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }

        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override bool CanSaveChanges(object arg) {
            if(ViewID.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                return _emailFacade.CanAdd(out results);
            }
            if(ViewID.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _emailFacade.CanUpdate(out results);
            }
            return false;
        }

        protected override void ClearEntity(object arg) { Entity = _emailFacade.GenerateEntity(); }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Email, State = ViewState.Add};

        protected override void EntityChanged() {
            base.EntityChanged();
            _emailFacade.Entity = Entity;
        }
    }
}