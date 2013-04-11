#region Usings

using System.Collections.Generic;
using System.ComponentModel;
using LOB.Business.Interface.Logic.SubEntity;
using LOB.Core.Localization;
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

        public AlterEmailViewModel(Email entity, IRepository repository, IEmailFacade emailFacade, IEventAggregator eventAggregator,
            ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) {
            _emailFacade = emailFacade;
        }

        public override void InitializeServices() {
            Operation = _operation;
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override bool CanSaveChanges(object arg) {
            if(Operation.State == UIOperationState.Add) {
                IEnumerable<ValidationResult> results;
                return _emailFacade.CanAdd(out results);
            }
            if(Operation.State == UIOperationState.Update) {
                IEnumerable<ValidationResult> results;
                return _emailFacade.CanUpdate(out results);
            }
            return false;
        }

        protected override void ClearEntity(object arg) {
            Entity = _emailFacade.GenerateEntity();
            _emailFacade.SetEntity(Entity);
            _emailFacade.ConfigureValidations();
        }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Email, State = UIOperationState.Add};

    }
}