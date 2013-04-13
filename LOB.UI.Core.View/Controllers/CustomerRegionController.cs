#region Usings

using System;
using System.ComponentModel;
using LOB.UI.Core.Events.Operation;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using IRegionAdapter = LOB.UI.Interface.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Controllers {
    public class CustomerRegionController : IDisposable {

        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly IRegionAdapter _regionAdapter;
        private readonly IFluentNavigator _fluentNavigator;
        private readonly BackgroundWorker _worker = new BackgroundWorker();

        public CustomerRegionController(IEventAggregator eventAggregator, IRegionManager regionManager, IRegionAdapter regionAdapter,
            IFluentNavigator fluentNavigator) {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _regionAdapter = regionAdapter;
            _fluentNavigator = fluentNavigator;

            OnLoad();
        }

        private SubscriptionToken _personTypeChangedSubscription;
        private void OnLoad() { _personTypeChangedSubscription = _eventAggregator.GetEvent<PersonTypeChangedEvent>().Subscribe(PersonTypeChangedExecute, false); }

        private void PersonTypeChangedExecute(UIOperation obj) {
            var view = _fluentNavigator.Init.ResolveView(obj).ResolveViewModel(obj).GetView();
            //Remove everything first
            _regionAdapter.RemoveView(obj.Type(UIOperationType.LegalPerson), "Customer_PersonRegion");
            _regionAdapter.RemoveView(obj.Type(UIOperationType.NaturalPerson), "Customer_PersonRegion");
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