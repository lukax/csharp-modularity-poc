#region Usings

using System;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using IRegionAdapter = LOB.UI.Interface.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    public class RegionAdapter : IRegionAdapter {

        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        [InjectionConstructor]
        public RegionAdapter(IUnityContainer container, IRegionManager regionManager) {
            _container = container;
            _regionManager = regionManager;
        }

        public void AddView<TView>(TView view, string regionName) where TView : IBaseView {
            var region = _regionManager.Regions[regionName];
            var previousView = region.GetView(view.Operation.ToString());
            if(previousView != null) if(region.Views.Contains(previousView)) region.Remove(previousView);
            region.Add(view, view.Operation.ToString());
        }

        public IBaseView GetView(UIOperation param, string regionName) {
            var region = _regionManager.Regions[regionName];
            return region.GetView(param.ToString()) as IBaseView;
        }

        public void RemoveView(UIOperation param, string regionName) {
            if(param.Type == default(UIOperationType)) throw new ArgumentNullException("param");
            var region = _regionManager.Regions[regionName];
            var view = region.GetView(param.ToString());
            if(region.Views.Contains(view)) region.Remove(view);
        }

    }
}