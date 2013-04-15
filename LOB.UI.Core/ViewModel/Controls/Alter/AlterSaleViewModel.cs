#region Usings

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
        private ViewID _viewID = new ViewID {Type = ViewType.Service, State = ViewState.Add};
        public override ViewID ViewID {
            get { return _viewID; }
            set { _viewID = value; }
        }
        public AlterSaleViewModel(Sale entity, IRepository repository, IEventAggregator eventAggregator, ILoggerFacade logger)
            : base(entity, repository, eventAggregator, logger) { }

        public override void InitializeServices() {
            if(Equals(ViewID, default(ViewID))) ViewID = _viewID;
            ClearEntity(null);
        }

        public override void Refresh() { ClearEntity(null); }

        protected override void Cancel(object arg) { EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewID); }

        //protected override void QuickSearch(object arg) {
        //    _previousState = _defaultViewID.State;
        //    _defaultViewID.State = ViewState.QuickSearch;
        //   _eventAggregator.GetEvent<OpenViewEvent>().Publish(_defaultViewID);
        //    _currentSubscription = _eventAggregator.GetEvent<CloseViewEvent>().Subscribe(ChangeUIState);
        //}

        //private void ChangeUIState(Operation uiOperation) {
        //    if(uiOperation.State == ViewState.QuickSearch) {
        //        _defaultViewID.State = _previousState;
        //        _currentSubscription.Dispose();
        //    }
        //}

        protected override void ClearEntity(object arg) { Entity = new Sale {}; }
    }
}