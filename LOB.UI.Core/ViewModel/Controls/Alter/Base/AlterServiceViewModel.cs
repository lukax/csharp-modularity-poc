#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Events.View;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public class AlterServiceViewModel : AlterBaseEntityViewModel<Service>, IAlterServiceViewModel {

        private readonly IServiceFacade _serviceFacade;
        protected AlterServiceViewModel(Service entity, IRepository repository, IServiceFacade serviceFacade, IEventAggregator eventAggregator,
            ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) { _serviceFacade = serviceFacade; }

        public override void InitializeServices() {
            if(Equals(Operation, default(UIOperation))) Operation = _operation;
            ClearEntity(null);
        }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Add};

        public override void Refresh() { ClearEntity(null); }

        protected override bool CanSaveChanges(object arg) {
            if(Operation.State == UIOperationState.Add) {
                IEnumerable<ValidationResult> results;
                return _serviceFacade.CanAdd(out results);
            }
            if(Operation.State == UIOperationState.Update) {
                IEnumerable<ValidationResult> results;
                return _serviceFacade.CanUpdate(out results);
            }
            return false;
        }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }
        protected override void ClearEntity(object arg) {
            Entity = _serviceFacade.GenerateEntity();
            _serviceFacade.SetEntity(Entity);
            _serviceFacade.ConfigureValidations();
        }

    }
}