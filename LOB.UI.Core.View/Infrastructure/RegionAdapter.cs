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
    [Export(typeof(IRegionAdapter))]
    public class RegionAdapter : IRegionAdapter {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public RegionAdapter(IRegionManager regionManager) { _regionManager = regionManager; }

        public void AddView<TView>(TView view, string regionName) where TView : IBaseView<IBaseViewModel> {
//            try {
//                var region = _regionManager.Regions[regionName];
//                var previousView = region.GetView(ApplyNamingConvention(view.ViewID)) as IBaseView<IBaseViewModel>;
//                if(previousView != null) if(region.Views.Contains(previousView)) RemoveView(previousView.ViewID, regionName);
//                region.Add(view, ApplyNamingConvention(view.ViewID));
//            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
//#if DEBUG
//                Debug.WriteLine(ex.Message);
//#endif
//                AddView(view, regionName);
//            }
        }

        public IBaseView<IBaseViewModel> GetView(ViewModelState param, string regionName) {
            try {
                var region = _regionManager.Regions[regionName];
                return region.GetView(ApplyNamingConvention(param)) as IBaseView<IBaseViewModel>;
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return GetView(param, regionName);
            }
        }

        public void RemoveView(ViewModelState param, string regionName) {
//            try {
//                if(param.Type == default(ViewType)) throw new ArgumentNullException("param");
//                var region = _regionManager.Regions[regionName];
//                var view = region.GetView(ApplyNamingConvention(param)) as IBaseView<IBaseViewModel>;
//                if(ContainsView(param, regionName)) {
//                    region.Remove(view);
//                    if(view != null) view.Dispose();
//                }
//            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
//#if DEBUG
//                Debug.WriteLine(ex.Message);
//#endif
//                RemoveView(param, regionName);
//            }
        }

        public bool ContainsView(ViewModelState param, string regionName) {
//            try {
//                if(param.Type == default(ViewType)) throw new ArgumentNullException("param");
//                var region = _regionManager.Regions[regionName];
//                var view = region.GetView(ApplyNamingConvention(param));
//                return region.Views.Contains(view);
//            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
//#if DEBUG
//                Debug.WriteLine(ex.Message);
//#endif
//                return ContainsView(param, regionName);
//            }
            throw new NotImplementedException();
        }

        private string ApplyNamingConvention(ViewModelState op) {
            //string s = op.Type.ToString();
            ////s = s.Split('_').First();
            ////s = s + "_" + op.GetHashCode();
            //return s; //INFO: Only one Type can be opened at the same time like this  
            throw new NotImplementedException();
        }
    }
}