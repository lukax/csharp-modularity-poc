#region Usings

using System;
using System.ComponentModel.Composition;
using System.Linq;
using LOB.Core.Localization;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Core.Infrastructure;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    [Export(typeof(IFluentNavigator))]
    public class FluentNavigator : IFluentNavigator {
        protected IBaseView<IBaseViewModel> ResolvedView;
        [Import] protected Lazy<IRegionAdapter> RegionAdapter { get; set; }
        [Import] protected Lazy<IServiceLocator> ServiceLocator { get; set; }
        [ImportMany] protected Lazy<IBaseView<IBaseViewModel>, IViewInfo>[] Views { get; set; }
        /// <summary>
        ///     Initialize with clean Fields
        /// </summary>
        public IFluentNavigator Init {
            get {
                ResolvedView = null;
                return this;
            }
        }

        public IFluentNavigator ResolveView(IViewInfo param) {
            var firstOrDefault = Views.FirstOrDefault(x => ViewInfoExtension.Equals(x.Metadata, param));
            if(firstOrDefault != null) {
                ResolvedView = firstOrDefault.Value;
                RegionAdapter.Value.Add(firstOrDefault.Value, RegionName.TabRegion);
            }
            return this;
        }

        public IFluentNavigator ResolveView<TView>() where TView : IBaseViewModel {
            if(ResolvedView != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var s = ServiceLocator.Value.GetInstance<IBaseView<TView>>();
            ResolvedView = s as IBaseView<IBaseViewModel>;
            //var resolved = _container.GetInstance<TView>();
            //SetView(resolved);
            return this;
        }

        public IFluentNavigator AddToRegion(string regionName) {
            var view = GetView();
            RegionAdapter.Value.Add(view, regionName);
            view.ViewModel.InitializeServices();
            return this;
        }

        public Guid GetViewId {
            get {
                if(ResolvedView != null) return ResolvedView.ViewModel.Id;
                throw new InvalidOperationException("View was not resolved");
            }
        }

        protected IBaseView<IBaseViewModel> GetView() {
            if(ResolvedView == null) throw new ArgumentException(Strings.Notification_Navigator_View_ResolveFirst);
            if(ResolvedView.ViewModel == null) throw new ArgumentException(Strings.Notification_Navigator_ViewModel_ResolveFirst);
            return ResolvedView;
        }

        //public IFluentNavigator SetView(Func<IBaseView<IBaseViewModel>> view) {
        //    _resolvedView = view();
        //    return this;
        //}
    }
}