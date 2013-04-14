#region Usings

using System.Collections.Generic;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.Domain.Logic;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>, IAlterEmployeeViewModel {

        private readonly IEmployeeFacade _employeeFacade;

        public IAlterPersonViewModel AlterPersonViewModel { get; set; }

        [InjectionConstructor]
        public AlterEmployeeViewModel(Employee entity, IEmployeeFacade employeeFacade, IAlterPersonViewModel alterPersonViewModel,
            IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) {
            _employeeFacade = employeeFacade;
            AlterPersonViewModel = alterPersonViewModel;
        }

        public override void InitializeServices() {
            if(Equals(Operation, default(ViewID))) Operation = _operation;
            AlterPersonViewModel.InitializeServices();
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        private readonly ViewID _operation = new ViewID {Type = ViewType.Employee, State = ViewState.Add};

        protected override bool CanSaveChanges(object arg) {
            if(Operation.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                return _employeeFacade.CanAdd(out results);
            }
            if(Operation.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _employeeFacade.CanUpdate(out results);
            }
            return false;
        }

        protected override bool CanCancel(object arg) { return true; }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) {
            Entity = _employeeFacade.GenerateEntity();
            _employeeFacade.SetEntity(Entity);
            _employeeFacade.ConfigureValidations();
        }

        public override void Dispose() {
            AlterPersonViewModel.Dispose();
            base.Dispose();
        }

    }
}