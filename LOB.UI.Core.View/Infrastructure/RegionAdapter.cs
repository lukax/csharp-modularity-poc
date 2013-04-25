#region Usings

using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using LOB.Core.Localization;
using LOB.UI.Contract;
using Microsoft.Practices.Prism.Regions;
using IRegionAdapter = LOB.UI.Contract.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    [Export(typeof(IRegionAdapter))]
    public class RegionAdapter : IRegionAdapter {
        [Import] protected Lazy<IRegionManager> RegionManager { get; set; }

        public void Add<TView>(TView view, string regionName) where TView : IBaseView<IBaseViewModel> {
            try {
                var region = RegionManager.Value.Regions[regionName];
                var previousView = region.GetView(ApplyConvention(view.ViewModel.Id)) as IBaseView<IBaseViewModel>;
                if(previousView != null) if(region.Views.Contains(previousView)) Remove(previousView);
                region.Add(view, ApplyConvention(view.ViewModel.Id));
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                Add(view, regionName);
            }
        }

        public IBaseView<IBaseViewModel> Get(Guid viewId, string regionName = null) {
            if(viewId == default(Guid)) throw new ArgumentNullException("viewId");
            try {
                IBaseView<IBaseViewModel> view = null;
                if(regionName != null)
                    view =
                        RegionManager.Value.Regions[regionName].Views.Cast<IBaseView<IBaseViewModel>>().FirstOrDefault(x => x.ViewModel.Id == viewId);
                else
                    foreach(var source in
                        RegionManager.Value.Regions.SelectMany(
                            region => region.Views.Cast<IBaseView<IBaseViewModel>>().Where(x => x.ViewModel.Id.Equals(viewId)))) view = source;
                return view;
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return Get(viewId);
            }
        }

        public void Remove(IBaseView<IBaseViewModel> param, string regionName = null) {
            try {
                if(regionName != null) RegionManager.Value.Regions[regionName].Remove(param);
                else foreach(var region in RegionManager.Value.Regions) foreach(var vieww in region.Views) if(vieww.Equals(param)) region.Remove(param);
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                Remove(param);
            }
        }

        public bool Contains(IBaseView<IBaseViewModel> param, string regionName) {
            try {
                var region = RegionManager.Value.Regions[regionName];
                return region.Views.Contains(param);
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return Contains(param, regionName);
            }
        }

        protected string ApplyConvention(Guid param) {
            if(param == default(Guid)) throw new ArgumentException(Strings.Notification_Param_NotUnique, "param");
            string s = param.ToString();
            ////s = s.Split('_').First();
            ////s = s + "_" + op.GetHashCode();
            //return s; //INFO: Only one Type can be opened at the same time like this  
            return s;
        }
    }
}