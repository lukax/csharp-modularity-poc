#region Usings

using System;
using LOB.UI.Core.Events.Operation;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.View.Controllers {
    public class CustomerRegionController : IDisposable {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionAdapter _regionAdapter;
        private readonly IFluentNavigator _fluentNavigator;
        //private readonly BackgroundWorker _worker = new BackgroundWorker();

        public CustomerRegionController(IEventAggregator eventAggregator, IRegionAdapter regionAdapter, IFluentNavigator fluentNavigator) {
            _eventAggregator = eventAggregator;
            _regionAdapter = regionAdapter;
            _fluentNavigator = fluentNavigator;

            OnLoad();
        }

        private SubscriptionToken _personTypeChangedSubscription;
        private void OnLoad() { _personTypeChangedSubscription = _eventAggregator.GetEvent<PersonTypeChangedEvent>().Subscribe(PersonTypeChangedExecute, false); }

        private void PersonTypeChangedExecute(ViewID id) {
            var view = id.ViewModel != null
                           ? _fluentNavigator.Init.ResolveView(id).SetViewModel(id.ViewModel).GetView()
                           : _fluentNavigator.Init.ResolveView(id).ResolveViewModel(id).GetView();
            //Remove everything first
            _regionAdapter.RemoveView(id.Type(ViewType.LegalPerson), "Customer_PersonRegion");
            _regionAdapter.RemoveView(id.Type(ViewType.NaturalPerson), "Customer_PersonRegion");
            _regionAdapter.AddView(view, "Customer_PersonRegion");
        }
        #region Implementation of IDisposable

        ~CustomerRegionController() { Dispose(false); }
        private void Dispose(bool disposing) {
            if(!disposing) return;
            _personTypeChangedSubscription.Dispose();
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}