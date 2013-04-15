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
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>, IAlterEmployeeViewModel {
        private readonly IEmployeeFacade _employeeFacade;

        public IAlterPersonViewModel AlterPersonViewModel { get; set; }

        [InjectionConstructor]
        public AlterEmployeeViewModel(IEmployeeFacade employeeFacade, IAlterPersonViewModel alterPersonViewModel, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(repository, eventAggregator, logger) {
            _employeeFacade = employeeFacade;
            AlterPersonViewModel = alterPersonViewModel;
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            AlterPersonViewModel.InitializeServices();
            base.InitializeServices();
        }

        public override void Refresh() { ClearEntity(null); }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.Employee, State = ViewState.Add};

        protected override bool CanSaveChanges(object arg) {
            if(ViewID.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                return _employeeFacade.CanAdd(out results);
            }
            if(ViewID.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _employeeFacade.CanUpdate(out results);
            }
            return false;
        }

        protected override bool CanCancel(object arg) { return true; }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override void ClearEntity(object arg) { Entity = _employeeFacade.GenerateEntity(); }

        public override void Dispose() {
            AlterPersonViewModel.Dispose();
            base.Dispose();
        }

        protected override void EntityChanged() {
            base.EntityChanged();
            _employeeFacade.Entity = Entity;
        }
    }
}