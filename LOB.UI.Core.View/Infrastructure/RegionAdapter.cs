#region Usings

using System;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using LOB.Core.Localization;
using LOB.UI.Interface;
using Microsoft.Practices.Prism.Regions;
using IRegionAdapter = LOB.UI.Interface.Infrastructure.IRegionAdapter;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    [Export(typeof(IRegionAdapter))]
    public class RegionAdapter : IRegionAdapter {
        [Import] protected Lazy<IRegionManager> LazyRegionManager { get; set; }

        public void Add<TView>(TView view, string regionName) where TView : IBaseView<IBaseViewModel> {
            try {
                var region = LazyRegionManager.Value.Regions[regionName];
                var previousView = region.GetView(ApplyConvention(view.ViewModel.Id)) as IBaseView<IBaseViewModel>;
                if(previousView != null) if(region.Views.Contains(previousView)) Remove(Get(previousView.ViewModel.Id));
                region.Add(view, ApplyConvention(view.ViewModel.Id));
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                Add(view, regionName);
            }
        }

        public IBaseView<IBaseViewModel> Get(Guid viewId) {
            if(viewId == default(Guid)) throw new ArgumentNullException("viewId");
            try {
                IBaseView<IBaseViewModel> view = null;
                foreach(var region in LazyRegionManager.Value.Regions) foreach(var source in region.Views.Cast<IBaseView<IBaseViewModel>>().Where(x => x.ViewModel.Id.Equals(viewId))) view = source;

                return view;
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                return Get(viewId);
            }
        }

        public void Remove(IBaseView<IBaseViewModel> param) {
            try {
                IBaseView<IBaseViewModel> view = null;
                foreach(var region in LazyRegionManager.Value.Regions) foreach(var vieww in region.Views) if(vieww.Equals(param)) region.Remove(param);

                return; //region.Get(ApplyConvention(viewId)) as IBaseView<IBaseViewModel>;
            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
#if DEBUG
                Debug.WriteLine(ex.Message);
#endif
                Remove(param);
            }
        }

        public bool Contains(IBaseView<IBaseViewModel> param, string regionName) {
//            try {
//                if(param.Type == default(ViewType)) throw new ArgumentNullException("param");
//                var region = LazyRegionManager.Regions[regionName];
//                var view = region.Get(ApplyConvention(param));
//                return region.Views.Contains(view);
//            } catch(UpdateRegionsException ex) { //BUG: Known bug to RegionManager, fix this later
//#if DEBUG
//                Debug.WriteLine(ex.Message);
//#endif
//                return Contains(param, regionName);
//            }
            throw new NotImplementedException();
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