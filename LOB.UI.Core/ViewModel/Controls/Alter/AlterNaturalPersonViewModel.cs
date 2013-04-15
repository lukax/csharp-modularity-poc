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

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.NaturalPerson, State = ViewState.Add};
        private AlterPersonViewModel _alterPersonViewModel;
        [Dependency]
        public IAlterPersonViewModel AlterPersonViewModel {
            get { return _alterPersonViewModel; }
            set { _alterPersonViewModel = value as AlterPersonViewModel; }
        }

        [InjectionConstructor]
        public AlterNaturalPersonViewModel(INaturalPersonFacade naturalPersonFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(repository, eventAggregator, logger) {
            _naturalPersonFacade = naturalPersonFacade;
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
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
            AlterPersonViewModel.InitializeServices();
        }

        protected override bool CanSaveChanges(object arg) {
            if(ViewID.State == ViewState.Add) {
                IEnumerable<ValidationResult> results;
                //var s= AlterPersonIuiComponentModel.SaveChangesCommand.CanExecute(null);
                return _naturalPersonFacade.CanAdd(out results) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            }
            if(ViewID.State == ViewState.Update) {
                IEnumerable<ValidationResult> results;
                return _naturalPersonFacade.CanUpdate(out results) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            }
            return false;
        }
        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override void ClearEntity(object arg) { Entity = _naturalPersonFacade.GenerateEntity(); }

        protected override void EntityChanged() {
            base.EntityChanged();
            _naturalPersonFacade.Entity = Entity;
            _alterPersonViewModel.Entity = Entity;
        }

        public override void Dispose()
        {
            AlterPersonViewModel.Dispose();
            base.Dispose();
        }
    }
}