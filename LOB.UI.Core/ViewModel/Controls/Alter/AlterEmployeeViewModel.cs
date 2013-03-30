#region Usings
using System;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterEmployeeViewModel : AlterBaseEntityViewModel<Employee>, IAlterEmployeeViewModel {

        private ICommandService _commandService;
        private IUnityContainer _container;

        [InjectionConstructor] public AlterEmployeeViewModel(Employee entity, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {}

        public new Employee Entity { get; set; }

        public override void InitializeServices() {
            throw new NotImplementedException();
        }

        public override void Refresh() {
            throw new NotImplementedException();
        }

        public override OperationType OperationType {
            get { throw new NotImplementedException(); }
        }

        public IAlterAddressViewModel AlterAddressViewModel { get; set; }
        public IAlterContactInfoViewModel AlterContactInfoViewModel { get; set; }

        protected override bool CanSaveChanges(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void QuickSearch(object arg) {
            //_commandService.Execute("QuickSearch", OperationName.ListEmployee);
        }

        protected override void ClearEntity(object arg) {
            throw new NotImplementedException();
        }

    }
}