﻿#region Usings

using System;
using System.ComponentModel.Composition;
using LOB.Business.Interface.Logic;
using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    [Export(typeof(IAlterNaturalPersonViewModel))]
    public sealed class AlterNaturalPersonViewModel : AlterBaseEntityViewModel<NaturalPerson>, IAlterNaturalPersonViewModel {
        private AlterPersonViewModel _alterPersonViewModel;
        public IAlterPersonViewModel AlterPersonViewModel {
            get { return _alterPersonViewModel; }
            set { _alterPersonViewModel = value as AlterPersonViewModel; }
        }

        [ImportingConstructor]
        public AlterNaturalPersonViewModel(INaturalPersonFacade naturalPersonFacade, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade logger, IAlterPersonViewModel alterPersonViewModel)
            : base(naturalPersonFacade, repository, eventAggregator, logger) { AlterPersonViewModel = alterPersonViewModel; }

        public string BirthDate {
            get { return Entity.BirthDate.ToShortDateString(); }
            set {
                if(Entity.BirthDate.ToShortDateString() == value) return;

                DateTime parsed;
                if(DateTime.TryParse(value, out parsed)) Entity.BirthDate = parsed;
            }
        }

        public override void InitializeServices() {
            base.InitializeServices();
            AlterPersonViewModel.InitializeServices();
        }

        protected override bool CanSaveChanges(object arg) {
            if(ReferenceEquals(Entity, null)) return false;
            if(ViewModelState.ViewState == ViewState.Add) return base.CanSaveChanges(arg) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            if(ViewModelState.ViewState == ViewState.Update) return base.CanSaveChanges(arg) & AlterPersonViewModel.SaveChangesCommand.CanExecute(null);
            return false;
        }

        protected override void EntityChanged() {
            base.EntityChanged();
            _alterPersonViewModel.Entity = Entity;
        }

        public override void Dispose() {
            AlterPersonViewModel.Dispose();
            base.Dispose();
        }
    }
}