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
    public sealed class AlterPhoneNumberViewModel : AlterBaseEntityViewModel<PhoneNumber>, IAlterPhoneNumberViewModel {
        private readonly IPhoneNumberFacade _phoneNumberFacade;

        public AlterPhoneNumberViewModel(PhoneNumber entity, IPhoneNumberFacade phoneNumberFacade, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) { _phoneNumberFacade = phoneNumberFacade; }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            if(ViewID.State == ViewState.Add) return _phoneNumberFacade.CanAdd(out results);
            if(ViewID.State == ViewState.Update) return _phoneNumberFacade.CanUpdate(out results);
            return false;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        //protected override void QuickSearch(object arg) { _eventAggregator.GetEvent<QuickSearchEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) {
            Entity = _phoneNumberFacade.GenerateEntity();
            _phoneNumberFacade.Entity = (Entity);
        }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.PhoneNumber, State = ViewState.Add};
    }
}