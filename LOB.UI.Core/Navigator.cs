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
            if (viewModel == null)
                switch (param)
                {
                    case "AlterProduct":
                        _resolvedView = _container.Resolve<AlterProductView>();
                        break;
                    case "AlterLegalPerson":
                        _resolvedView = _container.Resolve<AlterLegalPersonView>();
                        break;
                    case "AlterNaturalPerson":
                        _resolvedView = _container.Resolve<AlterNaturalPersonView>();
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
                        _resolvedView = _container.Resolve<AlterCustomerView>();
                        break;
                    case "ListClient":
                        _resolvedView = _container.Resolve<ListCustomerView>();
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

            else if (viewModel != null)
                switch (param)
                {
                    case "AlterProduct":
                        _resolvedView =
                            _container.Resolve<AlterProductView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterLegalPerson":
                        _resolvedView =
                            _container.Resolve<AlterLegalPersonView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterNaturalPerson":
                        _resolvedView =
                            _container.Resolve<AlterNaturalPersonView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "ListProduct":
                        _resolvedView =
                            _container.Resolve<ListProductView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterEmployee":
                        _resolvedView =
                            _container.Resolve<AlterEmployeeView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "ListEmployee":
                        _resolvedView =
                            _container.Resolve<ListEmployeeView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterClient":
                        _resolvedView =
                            _container.Resolve<AlterCustomerView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "ListClient":
                        _resolvedView =
                            _container.Resolve<ListCustomerView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterSale":
                        _resolvedView = _container.Resolve<AlterSaleView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "QuickSearch":
                        _resolvedView =
                            _container.Resolve<ListBaseEntityView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    default:
                        throw new ArgumentException("Parameter not implemented yet, ", "param");
                }
            return this;
        }

        public void StartView(bool asDialog = false)
        {
            if (_resolvedView == null) return;
            if (_resolvedView is UserControl)
            {
                var window = new FrameWindow()
                    {
                        Content = _resolvedView
                    };
                if (asDialog) window.ShowDialog();
                else window.Show();
                return;
            }
            if (_resolvedView is Window)
            {
                ((Window) _resolvedView).Show();
                return;
            }
        }

        public bool PromptUser(string message)
        {
            return MessageBox.Show(message, "Prompt", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        public void Startup(string viewName, object viewModel = null)
        {
            var view = ResolveView(viewName) as Window;
            if (view == null) return;
            if (view is IView) ((IView) view).InitializeServices();
            view.Show();
        }

        //public Window AsWindow(object resolvedView)
        //{
        //    if (_resolvedView is Window) return ((Window)_resolvedView);
        //    return new FrameWindow() { Content = resolvedView };
        //}
    }

    public class FluentNavigator : IFluentNavigator
    {
        private readonly IUnityContainer _container;
        private readonly IRegionAdapter _regionAdapter;
        private dynamic _resolvedView;

        [ImportingConstructor]
        public FluentNavigator(IUnityContainer container, IRegionAdapter regionAdapter)
        {
            _container = container;
            _regionAdapter = regionAdapter;
        }


        public object Get()
        {
            return _resolvedView;
        }

        public IFluentNavigator Resolve(string param, object viewModel = null)
        {
            if (viewModel == null)
                switch (param)
                {
                    case "AlterProduct":
                        _resolvedView = _container.Resolve<AlterProductView>();
                        break;
                    case "AlterLegalPerson":
                        _resolvedView = _container.Resolve<AlterLegalPersonView>();
                        break;
                    case "AlterNaturalPerson":
                        _resolvedView = _container.Resolve<AlterNaturalPersonView>();
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
                        _resolvedView = _container.Resolve<AlterCustomerView>();
                        break;
                    case "ListClient":
                        _resolvedView = _container.Resolve<ListCustomerView>();
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

            else if (viewModel != null)
                switch (param)
                {
                    case "AlterProduct":
                        _resolvedView =
                            _container.Resolve<AlterProductView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterLegalPerson":
                        _resolvedView =
                            _container.Resolve<AlterLegalPersonView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterNaturalPerson":
                        _resolvedView =
                            _container.Resolve<AlterNaturalPersonView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "ListProduct":
                        _resolvedView =
                            _container.Resolve<ListProductView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterEmployee":
                        _resolvedView =
                            _container.Resolve<AlterEmployeeView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "ListEmployee":
                        _resolvedView =
                            _container.Resolve<ListEmployeeView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterClient":
                        _resolvedView =
                            _container.Resolve<AlterCustomerView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "ListClient":
                        _resolvedView =
                            _container.Resolve<ListCustomerView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterSale":
                        _resolvedView = _container.Resolve<AlterSaleView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "QuickSearch":
                        _resolvedView =
                            _container.Resolve<ListBaseEntityView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    default:
                        throw new ArgumentException("Parameter not implemented yet, ", "param");
                }
            return this;
        }

        public IFluentNavigator SetViewModel(object viewModel)
        {
            _resolvedView.DataContext = viewModel;
            return this;
        }

        public void Show(bool asDialog = false)
        {
            if (_resolvedView == null) return;
            if (_resolvedView is UserControl)
            {
                var window = new FrameWindow()
                    {
                        Content = _resolvedView
                    };
                if (asDialog) window.ShowDialog();
                else window.Show();
                return;
            }
            if (_resolvedView is Window)
            {
                ((Window) _resolvedView).Show();
                return;
            }
        }

        public bool PromptUser(string message)
        {
            return MessageBox.Show(message, "Prompt", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

        //public Window AsWindow(object resolvedView)
        //{
        //    if (_resolvedView is Window) return ((Window)_resolvedView);
        //    return new FrameWindow() { Content = resolvedView };
        //}
    }
}