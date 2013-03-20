﻿#region Usings

using System;
using System.Windows.Controls;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
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
            var previousView = region.GetView(view.OperationType.ToString());
            if(previousView != null)
                if(region.Views.Contains(previousView))
                    region.Remove(previousView);
            region.Add(view, view.OperationType.ToString());
        }

        public IBaseView GetView(OperationType param, string regionName)
        {
            if (param == default(OperationType)) throw new ArgumentNullException("param");
            if (regionName == null) throw new ArgumentNullException("regionName");
            var region = _regionManager.Regions[regionName];
            return region.GetView(param.ToString()) as IBaseView;
        }

        public void RemoveView(OperationType param, string regionName)
        {
            if (param == default(OperationType)) throw new ArgumentNullException("param");
            if (regionName == null) throw new ArgumentNullException("regionName");
            var region = _regionManager.Regions[regionName];
            var view = region.GetView(param.ToString());
            if(region.Views.Contains(view))
                region.Remove(view);
        }
    }
}