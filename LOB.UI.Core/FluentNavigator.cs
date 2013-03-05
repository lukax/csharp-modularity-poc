#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using LOB.UI.Core.View;
using LOB.UI.Core.View.Controls.Alter;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Controls.List;
using LOB.UI.Core.View.Controls.List.SubEntity;
using LOB.UI.Interface;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core
{
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
            if (_resolvedView == null)
                throw new ArgumentException("First resolveView the view", "Resolve");
            //if (_resolvedView.DataContext == null)
            //    throw new ArgumentException("First add a viewModel to the respective view");
            if (_resolvedView is IView)
                ((IView)_resolvedView).InitializeServices();

            return _resolvedView;
        }
        public TView As<TView>() where TView : class
        {
            return (TView)Get();
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
                    case "AlterCategory":
                        _resolvedView = _container.Resolve<AlterCategoryView>();
                        break;
                    case "AlterService":
                        _resolvedView = _container.Resolve<AlterServiceView>();
                        break;
                    case "ListCategory":
                        _resolvedView = _container.Resolve<ListCategoryView>();
                        break;
                    case "ListService":
                        _resolvedView = _container.Resolve<ListServiceView>();
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
                    case "AlterCategory":
                        _resolvedView = _container.Resolve<AlterCategoryView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "AlterService":
                        _resolvedView = _container.Resolve<AlterServiceView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "ListCategory":
                        _resolvedView = _container.Resolve<ListCategoryView>(new ParameterOverride("viewModel", viewModel));
                        break;
                    case "ListService":
                        _resolvedView = _container.Resolve<ListServiceView>(new ParameterOverride("viewModel", viewModel));
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
        public IFluentNavigator Resolve<TView>(object viewModel = null)
        {
            _resolvedView = _container.Resolve<TView>();
            return this;
        }
        public IFluentNavigator SetViewModel(object viewModel)
        {
            _resolvedView.DataContext = viewModel;
            return this;
        }
        public void Show(bool asDialog = false)
        {
            var local = Get();
            if (local is UserControl)
            {
                var window = new FrameWindow{ Content = _resolvedView};
                if (_resolvedView is ITabProp) window.Title = ((ITabProp) _resolvedView).Header;
                if (asDialog) window.ShowDialog();
                else window.Show();
                return;
            }
            if (_resolvedView is Window)
            {
                ((Window)_resolvedView).Show();
                return;
            }
        }
        public bool PromptUser(string message)
        {
            return MessageBox.Show(message, "Prompt", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}