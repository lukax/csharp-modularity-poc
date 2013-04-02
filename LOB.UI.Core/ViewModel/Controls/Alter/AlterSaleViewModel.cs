﻿#region Usings

using LOB.Dao.Interface;
using LOB.Domain;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter {
    public sealed class AlterSaleViewModel : AlterBaseEntityViewModel<Sale>, IAlterSaleViewModel {

        private readonly IEventAggregator _eventAggregator;
        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.Service,
            State = UIOperationState.Add
        };
        public override UIOperation UIOperation {
            get { return _operation; }
        }

        public AlterSaleViewModel(Sale entity, IRepository repository, IEventAggregator eventAggregator,
            ILoggerFacade loggerFacade)
            : base(entity, repository, eventAggregator, loggerFacade) { _eventAggregator = eventAggregator; }

        public override void InitializeServices() { ClearEntity(null); }

        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(UIOperation); }

        protected override void QuickSearch(object arg) { _eventAggregator.GetEvent<QuickSearchEvent>().Publish(UIOperation); }

        protected override void ClearEntity(object arg) { Entity = new Sale {}; }

    }
}