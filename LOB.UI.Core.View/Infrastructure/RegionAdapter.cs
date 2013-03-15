#region Usings

using System.Windows.Controls;
using LOB.UI.Interface;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using IRegionAdapter = LOB.UI.Interface.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Infrastructure
{
    public class RegionAdapter : IRegionAdapter
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        [InjectionConstructor]
        public RegionAdapter(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public IRegionAdapter RegisterRegion(string name, object region)
        {
            _regionManager.AddToRegion(name, region);
            return this;
        }

        public IRegionAdapter AddView<TView>(TView view, string regionName, string title = "IsDefault")
            where TView : class
        {
            object region = _regionManager.Regions[regionName];
            if (region is IBaseView)
            {
                ((IBaseView) region).Header = title;
            }
            if (region is ContentControl)
            {
                ((ContentControl) region).Content = view;
            }

            return this;
        }
    }
}