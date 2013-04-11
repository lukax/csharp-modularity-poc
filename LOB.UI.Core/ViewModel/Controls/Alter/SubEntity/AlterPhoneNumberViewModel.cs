#region Usings

using System.Collections.Generic;
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
    public sealed class AlterPhoneNumberViewModel : AlterBaseEntityViewModel<PhoneNumber>, IAlterPhoneNumberViewModel {

        private readonly IPhoneNumberFacade _phoneNumberFacade;

        public AlterPhoneNumberViewModel(PhoneNumber entity, IPhoneNumberFacade phoneNumberFacade, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) { _phoneNumberFacade = phoneNumberFacade; }

        public override void InitializeServices() {
            Operation = _operation;
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            if(Operation.State == UIOperationState.Add) return _phoneNumberFacade.CanAdd(out results);
            if(Operation.State == UIOperationState.Update) return _phoneNumberFacade.CanUpdate(out results);
            return false;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        //protected override void QuickSearch(object arg) { _eventAggregator.GetEvent<QuickSearchEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) {
            Entity = _phoneNumberFacade.GenerateEntity();
            _phoneNumberFacade.SetEntity(Entity);
            _phoneNumberFacade.ConfigureValidations();
        }

        private readonly UIOperation _operation = new UIOperation { Type = UIOperationType.PhoneNumber, State = UIOperationState.Add };
    }
}