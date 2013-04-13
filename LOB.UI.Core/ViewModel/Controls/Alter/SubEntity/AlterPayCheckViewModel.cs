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
    public sealed class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>, IAlterPayCheckViewModel {

        private readonly IPayCheckFacade _payCheckFacade;
        public AlterPayCheckViewModel(PayCheck entity, IPayCheckFacade payCheckFacade, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) { _payCheckFacade = payCheckFacade; }

        public override void InitializeServices() {
            if (Equals(Operation, default(UIOperation))) Operation = _operation;
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            if(Operation.State == UIOperationState.Add) return _payCheckFacade.CanAdd(out results);
            if(Operation.State == UIOperationState.Update) return _payCheckFacade.CanUpdate(out results);
            return false;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) {
            Entity = _payCheckFacade.GenerateEntity();
            _payCheckFacade.SetEntity(Entity);
            _payCheckFacade.ConfigureValidations();
        }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.PayCheck, State = UIOperationState.Add};

    }
}