#region Usings
using System;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.SubEntity {
    public sealed class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>, IAlterPayCheckViewModel {

        public AlterPayCheckViewModel(PayCheck entity, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) {}

        public override void InitializeServices() {}

        public override void Refresh() {
            Entity = new PayCheck();
        }

        public override OperationType OperationType {
            get { return OperationType.AlterPayCheck; }
        }

        protected override bool CanSaveChanges(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void QuickSearch(object arg) {
            //Messenger.Default.Send<object>(_container.Resolve<ListPayCheckViewModel>(), "QuickSearchCommand");
        }

        protected override void ClearEntity(object arg) {
            throw new NotImplementedException();
        }

    }
}