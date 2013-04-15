﻿#region Usings

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
    public sealed class AlterPayCheckViewModel : AlterBaseEntityViewModel<PayCheck>, IAlterPayCheckViewModel {
        private readonly IPayCheckFacade _payCheckFacade;
        public AlterPayCheckViewModel(IPayCheckFacade payCheckFacade, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(repository, eventAggregator, logger) { _payCheckFacade = payCheckFacade; }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _defaultViewID;
            base.InitializeServices();
        }

        public override void Refresh() { ClearEntity(null); }

        protected override bool CanSaveChanges(object arg) {
            IEnumerable<ValidationResult> results;
            if(ViewID.State == ViewState.Add) return _payCheckFacade.CanAdd(out results);
            if(ViewID.State == ViewState.Update) return _payCheckFacade.CanUpdate(out results);
            return false;
        }

        protected override bool CanCancel(object arg) {
            //TODO: Business logic
            return true;
        }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        protected override void ClearEntity(object arg) { Entity = _payCheckFacade.GenerateEntity(); }

        private readonly ViewID _defaultViewID = new ViewID {Type = ViewType.PayCheck, State = ViewState.Add};

        protected override void EntityChanged() {
            base.EntityChanged();
            _payCheckFacade.Entity = Entity;
        }
    }
}