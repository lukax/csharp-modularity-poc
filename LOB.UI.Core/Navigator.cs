#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using LOB.UI.Core.View;
using LOB.UI.Core.View.Controls.Alter;
using LOB.UI.Core.View.Controls.List;
using LOB.UI.Core.View.Controls.List.SubEntity;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core
{
    public class Navigator : INavigator
    {
        private readonly IUnityContainer _container;
        private readonly IRegionAdapter _regionAdapter;
        private dynamic _resolvedView;

        [ImportingConstructor]
        public Navigator(IUnityContainer container, IRegionAdapter regionAdapter)
        {
            _container = container;
            _regionAdapter = regionAdapter;
        }

        public void Startup<TView>(object viewModel = null) where TView : class
        {
            var view = _container.Resolve<TView>() as Window;
            if (view == null) return;
            if (viewModel != null) view.DataContext = viewModel;
            if (view is IView) ((IView) view).InitializeServices();
            view.Show();
        }

        public void Startup(string viewName, object viewModel = null)
        {
            var view = ResolveView(viewName) as Window;
            if (view == null) return;
            if (view is IView) ((IView)view).InitializeServices();
            view.Show();
        }

        public void OpenView<TView>(string regionName, object viewModel = null) where TView : class
        {
            dynamic vModel;
            var view = _container.Resolve<TView>() as UserControl;
            if (view == null) return;
            if (viewModel != null) vModel = viewModel;

            if (view is IView) ((IView) view).InitializeServices();
            _regionAdapter.AddView(view, regionName);
        }

        public object GetView
        {
            get { return _resolvedView; }
        }

        public INavigator ResolveView(string param, object viewModel = null)
        {
            switch (param) {
                case "AlterProduct":
                    _resolvedView = _container.Resolve<AlterProductView>();
                    break;
                case "ListProduct":
                    _resolvedView = _container.Resolve<ListProductView>();
                    break;
                case "AlterEmployee":
                    _resolvedView = _container.Resolve<AlterEmployeeView>();
                    break;
                case "ListEmployee":
                    _resolvedView = _container.Resolve<ListEmployeeView>();
                    break;
                case "AlterClient":
                    _resolvedView = _container.Resolve<AlterClientView>();
                    break;
                case "ListClient":
                    _resolvedView = _container.Resolve<ListClientView>();
                    break;
                case "AlterSale":
                    _resolvedView = _container.Resolve<AlterSaleView>();
                    break;
                case "QuickSearch":
                    _resolvedView = _container.Resolve<ListBaseEntityView>();
                    break;
                default:
                    throw new ArgumentException("Parameter not implemented yet, ", "param");
            }
            if (viewModel != null) {
                _resolvedView.DataContext = viewModel;
            }
            return this;
        }

        public void StartView(bool asDialog = false)
        {
            if (_resolvedView == null) return;
            if (_resolvedView is UserControl) {
                var window = new FrameWindow()
                    {
                        Content = _resolvedView
                    };
                if (asDialog) window.ShowDialog();
                else window.Show();
                return;
            }
            if (_resolvedView is Window) {
                ((Window) _resolvedView).Show();
                return;
            }
        }
        
        //public Window AsWindow(object resolvedView)
        //{
        //    if (_resolvedView is Window) return ((Window)_resolvedView);
        //    return new FrameWindow() { Content = resolvedView };
        //}
    }
}