#region Usings

using System;
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
    public sealed class AlterNaturalPersonViewModel : AlterBaseEntityViewModel<NaturalPerson>, IAlterNaturalPersonViewModel {

        private readonly INaturalPersonFacade _naturalPersonFacade;

        private readonly ViewID _operation = new ViewID {Type = ViewType.NaturalPerson, State = ViewState.Add};
        public IAlterPersonViewModel AlterPersonViewModel { get; set; }

        [InjectionConstructor]
        public AlterNaturalPersonViewModel(NaturalPerson entity, INaturalPersonFacade naturalPersonFacade, IAlterPersonViewModel alterPersonViewModel,
            IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) {
            _naturalPersonFacade = naturalPersonFacade;
            AlterPersonViewModel = alterPersonViewModel;
        }

        public string BirthDate {
            get { return Entity.BirthDate.ToShortDateString(); }
            set {
                if(Entity.BirthDate.ToShortDateString() == value) return;

                DateTime parsed;
                if(DateTime.TryParse(value, out parsed)) Entity.BirthDate = parsed;
            }
        }

        public override void InitializeServices() {
            if(Equals(Operation, default(ViewID))) Operation = _operation;
            AlterPersonViewModel.InitializeServices();
            ClearEntity(null);
        }

        protected override bool CanSaveChanges(object arg) {
            if(Operation.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                //var s= AlterPersonIuiComponentModel.SaveChangesCommand.CanExecute(null);
                return _naturalPersonFacade.CanAdd(out results) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            }
            if(Operation.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _naturalPersonFacade.CanUpdate(out results) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            }
            return false;
        }
        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        protected override void ClearEntity(object arg) {
            Entity = _naturalPersonFacade.GenerateEntity();
            _naturalPersonFacade.SetEntity(Entity);
            _naturalPersonFacade.ConfigureValidations();
        }

        public override void Dispose() {
            AlterPersonViewModel.Dispose();
            base.Dispose();
        }

    }
}