#region Usings

using LOB.Business.Interface.Logic.Base;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.Events.View;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base {
    public class AlterServiceViewModel : AlterBaseEntityViewModel<Service>, IAlterServiceViewModel {

        private readonly IServiceFacade _serviceFacade;
        private readonly IEventAggregator _eventAggregator;

        protected AlterServiceViewModel(Service entity, IRepository repository, IServiceFacade serviceFacade, IEventAggregator eventAggregator,
            ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {
            _serviceFacade = serviceFacade;
            _eventAggregator = eventAggregator;
        }
        #region Overrides of BaseViewModel

        public override void InitializeServices() {
            Operation = _operation;
            ClearEntity(null);
        }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Service, State = UIOperationState.Add};

        public override void Refresh() { ClearEntity(null); }

        #endregion
        #region Overrides of AlterBaseEntityViewModel<Service>

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }
        protected override void ClearEntity(object arg) {
            Entity = _serviceFacade.GenerateEntity();
            _serviceFacade.SetEntity(Entity);
            _serviceFacade.ConfigureValidations();
        }

        #endregion
    }
}