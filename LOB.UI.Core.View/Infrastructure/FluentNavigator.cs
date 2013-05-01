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
using LOB.UI.Contract.Exception;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Core.ViewModel.Controls.Alter.SubEntity;
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
            throw_if_view_wasnt_resolved();
            return this;
        }

        public IFluentNavigator ResolveView(Type param) {
            if(param.GetInterfaces().Any(x=> x == typeof(IBaseViewModel))) {
                ResolvedView = ServiceLocator.Value.GetInstance(typeof(IBaseView<>).MakeGenericType(param)) as IBaseView<IBaseViewModel>;
            }
            else throw new NotSupportedException("param should be derived from IBaseViewModel");
            throw_if_view_wasnt_resolved();
            return this;
        }

        public IFluentNavigator ResolveView<TView>() where TView : IBaseViewModel {
            var s = ServiceLocator.Value.GetInstance<IBaseView<TView>>();
            ResolvedView = s as IBaseView<IBaseViewModel>;
            throw_if_view_wasnt_resolved();
            return this;
        }

        public IFluentNavigator AddToRegion(string regionName) {
            throw_if_view_wasnt_resolved();
            RegionAdapter.Value.Add(ResolvedView, regionName);
            ResolvedView.ViewModel.InitializeServices();
            return this;
        }

        public Guid GetViewId {
            get {
                throw_if_view_wasnt_resolved();
                return ResolvedView.ViewModel.Id;
            }
        }

        protected void throw_if_view_wasnt_resolved() { if(ResolvedView == null) throw new ViewLoadException(Strings.Notification_Navigator_View_CouldNotBeLoaded); }
        #region Helper methods

        //TODO: Move this to somewhere else
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

        #endregion
    }
}