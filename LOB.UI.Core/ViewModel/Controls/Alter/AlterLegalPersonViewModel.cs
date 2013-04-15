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
    public sealed class AlterLegalPersonViewModel : AlterBaseEntityViewModel<LegalPerson>, IAlterLegalPersonViewModel {
        private readonly ILegalPersonFacade _legalPersonFacade;
        private readonly IEventAggregator _eventAggregator;
        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.LegalPerson, State = ViewState.Add};

        public IAlterPersonViewModel AlterPersonViewModel { get; set; }

        [InjectionConstructor]
        public AlterLegalPersonViewModel(ILegalPersonFacade legalPersonFacade, IAlterPersonViewModel alterPersonViewModel, IRepository repository,
            IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(repository, eventAggregator, logger) {
            AlterPersonViewModel = alterPersonViewModel;
            _legalPersonFacade = legalPersonFacade;
            _eventAggregator = eventAggregator;
        }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
            AlterPersonViewModel.InitializeServices();
        }

        protected override bool CanSaveChanges(object arg) {
            if(ViewID.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                return _legalPersonFacade.CanAdd(out results);
            }
            if(ViewID.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _legalPersonFacade.CanUpdate(out results);
            }
            return false;
        }

        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override void ClearEntity(object arg) { Entity = _legalPersonFacade.GenerateEntity(); }

        public override void Dispose() {
            AlterPersonViewModel.Dispose();
            base.Dispose();
        }

        protected override void EntityChanged() {
            base.EntityChanged();
            _legalPersonFacade.Entity = Entity;
        }
    }
}