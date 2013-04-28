#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;
using LOB.Core.Localization;
using LOB.UI.Contract;
using LOB.UI.Contract.Infrastructure;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    [Export(typeof(IFluentNavigator))]
    public class FluentNavigator : IFluentNavigator {
        protected IBaseView<IBaseViewModel> ResolvedView;
        [Import] protected Lazy<IRegionAdapter> RegionAdapter { get; set; }
        [Import] protected Lazy<IServiceLocator> ServiceLocator { get; set; }
        //[ImportMany] protected Lazy<Type, IViewInfo>[] Views { get; set; }
        [Import] protected Lazy<AggregateCatalog> Catalog { get; set; }
        /// <summary>
        ///     Initialize with clean Fields
        /// </summary>
        public IFluentNavigator Init {
            get {
                ResolvedView = null;
                return this;
            }
        }

        public IEnumerable<Type> GetExportedTypes<T>() { return Catalog.Value.Parts.Select(part => ComposablePartExportType<T>(part)).Where(t => t != null); }

        private Type ComposablePartExportType<T>(ComposablePartDefinition part) {
            return
                part.ExportDefinitions.Any(
                    def => def.Metadata.ContainsKey("ExportTypeIdentity") && def.Metadata["ExportTypeIdentity"].Equals(typeof(T).FullName))
                    ? ReflectionModelServices.GetPartType(part).Value
                    : null;
        }

        private Type ComposablePartExportType(ComposablePartDefinition part) {
            return part.ExportDefinitions.Any(def => def.Metadata.ContainsKey("ExportTypeIdentity"))
                       ? ReflectionModelServices.GetPartType(part).Value
                       : null;
        }

        public IFluentNavigator ResolveView(IViewInfo param) {
            var t =
                Catalog.Value.FirstOrDefault(
                    x =>
                    x.ExportDefinitions.Any(
                        y =>
                        y.Metadata.Any(z => param.ViewType.Equals(z.Value)) &&
                        x.ExportDefinitions.Any(
                            a =>
                            a.Metadata.Any(
                                b => b.Value as IEnumerable<ViewState> != null && param.ViewStates.SequenceEqual(b.Value as IEnumerable<ViewState>)))));
            if(t != null) ResolvedView = ServiceLocator.Value.GetInstance(ComposablePartExportType(t)) as IBaseView<IBaseViewModel>;

            // var firstOrDefault = Views.FirstOrDefault(x => ViewInfoExtension.Equals(x.Metadata, param));
            //if(firstOrDefault != null) ResolvedView = firstOrDefault.Value;
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