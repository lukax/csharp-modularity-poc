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
            if(Equals(Operation, default(UIOperation))) Operation = _operation;
            AlterPersonViewModel.InitializeServices();
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.Employee, State = UIOperationState.Add};

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        protected override bool CanSaveChanges(object arg) {
            if(Operation.State == UIOperationState.Add) {
                IEnumerable<ValidationResult> results;
                return _employeeFacade.CanAdd(out results);
            }
            if(Operation.State == UIOperationState.Update) {
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

    }
}