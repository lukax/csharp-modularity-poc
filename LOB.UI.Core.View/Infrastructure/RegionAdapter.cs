#region Usings

using System;
using System.Windows.Controls;
using LOB.UI.Interface;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using IRegionAdapter = LOB.UI.Interface.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Infrastructure
{
    public class RegionAdapter : Interface.Infrastructure.IRegionAdapter
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        [InjectionConstructor]
        public RegionAdapter(IUnityContainer container, IRegionManager regionManager)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            _container = container;
            _regionManager = regionManager;
        }

        public void AddView<TView>(TView view, string regionName)
            where TView : IBaseView
        {
            if (regionName == null) throw new ArgumentNullException("regionName");
            var region = _regionManager.Regions[regionName];
            var previousView = region.GetView(typeof (TView).Name);
            if(previousView != null)
                if(region.Views.Contains(previousView))
                    region.Remove(previousView);
            region.Add(view, typeof (TView).Name);
        }

        public IBaseView GetView(string param, string regionName)
        {
            if (param == null) throw new ArgumentNullException("param");
            if (regionName == null) throw new ArgumentNullException("regionName");
            var region = _regionManager.Regions[regionName];
            return region.GetView(param) as IBaseView;
        }

        public void RemoveView(string param, string regionName)
        {
            if (param == null) throw new ArgumentNullException("param");
            if (regionName == null) throw new ArgumentNullException("regionName");
            var region = _regionManager.Regions[regionName];
            region.Remove(region.GetView(param));
        }
    }
}