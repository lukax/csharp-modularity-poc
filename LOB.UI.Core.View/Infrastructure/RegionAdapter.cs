#region Usings

using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Regions;
using IRegionAdapter = LOB.UI.Interface.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    public class RegionAdapter : IRegionAdapter {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public RegionAdapter(IRegionManager regionManager) { _regionManager = regionManager; }

        public void AddView<TView>(TView view, string regionName) where TView : IBaseView {
            try {
                var region = _regionManager.Regions[regionName];
                var previousView = region.GetView(ApplyNamingConvention(view.ViewID)) as IBaseView;
                if(previousView != null) if(region.Views.Contains(previousView)) RemoveView(previousView.ViewID, regionName);
                region.Add(view, ApplyNamingConvention(view.ViewID));
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                AddView(view, regionName);
            }
        }

        public IBaseView GetView(ViewID param, string regionName) {
            try {
                var region = _regionManager.Regions[regionName];
                return region.GetView(ApplyNamingConvention(param)) as IBaseView;
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return GetView(param, regionName);
            }
        }

        public void RemoveView(ViewID param, string regionName) {
            try {
                if(param.Type == default(ViewType)) throw new ArgumentNullException("param");
                var region = _regionManager.Regions[regionName];
                var view = region.GetView(ApplyNamingConvention(param)) as IBaseView;
                if(ContainsView(param, regionName)) {
                    region.Remove(view);
                    if(view != null) view.Dispose();
                }
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                RemoveView(param, regionName);
            }
        }

        public bool ContainsView(ViewID param, string regionName) {
            try {
                if(param.Type == default(ViewType)) throw new ArgumentNullException("param");
                var region = _regionManager.Regions[regionName];
                var view = region.GetView(ApplyNamingConvention(param));
                return region.Views.Contains(view);
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return ContainsView(param, regionName);
            }
        }

        private string ApplyNamingConvention(ViewID op) {
            string s = op.Type.ToString();
            //s = s.Split('_').First();
            //s = s + "_" + op.GetHashCode();
            return s; //INFO: Only one Type can be opened at the same time like this  
        }
    }
}