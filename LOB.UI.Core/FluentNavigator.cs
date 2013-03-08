#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using LOB.UI.Core.View;
using LOB.UI.Core.View.Controls.Alter;
using LOB.UI.Core.View.Controls.Alter.Base;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Controls.List;
using LOB.UI.Core.View.Controls.List.Base;
using LOB.UI.Core.View.Controls.List.SubEntity;
using LOB.UI.Core.View.Controls.Sale;
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
            {
                switch (param)
                {
                    case "SellProduct":
                        _resolvedView =
                            _container.Resolve<SellProductView>();
                        break;
                    case "SellService":
                        _resolvedView =
                            _container.Resolve<SellServiceView>();
                        break;
                    case "AlterProduct":
                        _resolvedView =
                            _container.Resolve<AlterProductView>();
                        break;
                    case "AlterLegalPerson":
                        _resolvedView =
                            _container.Resolve<AlterLegalPersonView>();
                        break;
                    case "AlterNaturalPerson":
                        _resolvedView =
                            _container.Resolve<AlterNaturalPersonView>();
                        break;
                    case "ListProduct":
                        _resolvedView =
                            _container.Resolve<ListProductView>();
                        break;
                    case "AlterEmployee":
                        _resolvedView =
                            _container.Resolve<AlterEmployeeView>();
                        break;
                    case "ListEmployee":
                        _resolvedView =
                            _container.Resolve<ListEmployeeView>();
                        break;
                    case "AlterClient":
                        _resolvedView =
                            _container.Resolve<AlterCustomerView>();
                        break;
                    case "ListClient":
                        _resolvedView =
                            _container.Resolve<ListCustomerView>();
                        break;
                    case "AlterCategory":
                        _resolvedView =
                            _container.Resolve<AlterCategoryView>();
                        break;
                    case "AlterService":
                        _resolvedView =
                            _container.Resolve<AlterServiceView>();
                        break;
                    case "ListCategory":
                        _resolvedView =
                            _container.Resolve<ListCategoryView>();
                        break;
                    case "ListService":
                        _resolvedView =
                            _container.Resolve<ListServiceView>();
                        break;
                    case "AlterSale":
                        _resolvedView = _container.Resolve<AlterSaleView>();
                        break;
                    case "ListSale":
                        throw new NotImplementedException();
                    case "AlterEmail":
                        _resolvedView = _container.Resolve<AlterEmailView>();
                        break;
                    case "ListEmail":
                        _resolvedView = _container.Resolve<ListEmailView>();
                        break;
                    case "AlterPhoneNumber":
                        _resolvedView =
                            _container.Resolve<AlterPhoneNumberView>();
                        break;
                    case "ListPhoneNumber":
                        _resolvedView =
                            _container.Resolve<ListPhoneNumberView>();
                        break;
                    case "AlterContactInfo":
                        _resolvedView = _container.Resolve<AlterContactInfoView>();
                        break;
                    case "QuickSearch":
                        _resolvedView =
                            _container.Resolve<ListBaseEntityView>();
                        break;
                    default:
                        throw new ArgumentException("Parameter not implemented yet, ", "param");
                }
            }

            else
            {
                var defaultOverrideParam = new ParameterOverride("viewModel", viewModel);
                switch (param)
                {
                    case "SellProduct":
                        _resolvedView =
                            _container.Resolve<SellProductView>(defaultOverrideParam);
                        break;
                    case "SellService":
                        _resolvedView =
                            _container.Resolve<SellServiceView>(defaultOverrideParam);
                        break;
                    case "AlterProduct":
                        _resolvedView =
                            _container.Resolve<AlterProductView>(defaultOverrideParam);
                        break;
                    case "AlterLegalPerson":
                        _resolvedView =
                            _container.Resolve<AlterLegalPersonView>(defaultOverrideParam);
                        break;
                    case "AlterNaturalPerson":
                        _resolvedView =
                            _container.Resolve<AlterNaturalPersonView>(defaultOverrideParam);
                        break;
                    case "ListProduct":
                        _resolvedView =
                            _container.Resolve<ListProductView>(defaultOverrideParam);
                        break;
                    case "AlterEmployee":
                        _resolvedView =
                            _container.Resolve<AlterEmployeeView>(defaultOverrideParam);
                        break;
                    case "ListEmployee":
                        _resolvedView =
                            _container.Resolve<ListEmployeeView>(defaultOverrideParam);
                        break;
                    case "AlterClient":
                        _resolvedView =
                            _container.Resolve<AlterCustomerView>(defaultOverrideParam);
                        break;
                    case "ListClient":
                        _resolvedView =
                            _container.Resolve<ListCustomerView>(defaultOverrideParam);
                        break;
                    case "AlterCategory":
                        _resolvedView =
                            _container.Resolve<AlterCategoryView>(defaultOverrideParam);
                        break;
                    case "AlterService":
                        _resolvedView =
                            _container.Resolve<AlterServiceView>(defaultOverrideParam);
                        break;
                    case "ListCategory":
                        _resolvedView =
                            _container.Resolve<ListCategoryView>(defaultOverrideParam);
                        break;
                    case "ListService":
                        _resolvedView =
                            _container.Resolve<ListServiceView>(defaultOverrideParam);
                        break;
                    case "AlterSale":
                        _resolvedView = _container.Resolve<AlterSaleView>(defaultOverrideParam);
                        break;
                    case "ListSale":
                        throw new NotImplementedException();
                    case "AlterEmail":
                        _resolvedView = _container.Resolve<AlterEmailView>(defaultOverrideParam);
                        break;
                    case "ListEmail":
                        _resolvedView = _container.Resolve<ListEmailView>(defaultOverrideParam);
                        break;
                    case "AlterPhoneNumber":
                        _resolvedView =
                            _container.Resolve<AlterPhoneNumberView>(defaultOverrideParam);
                        break;
                    case "ListPhoneNumber":
                        _resolvedView =
                            _container.Resolve<ListPhoneNumberView>(defaultOverrideParam);
                        break;
                    case "AlterContactInfo":
                        _resolvedView = _container.Resolve<AlterContactInfoView>(defaultOverrideParam);
                        break;
                    case "QuickSearch":
                        _resolvedView =
                            _container.Resolve<ListBaseEntityView>(defaultOverrideParam);
                        break;
                    default:
                        throw new ArgumentException("Parameter not implemented yet, ", "param");
                }
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
            var asUc = Get() as UserControl;
            if (asUc != null)
            {
                var window = new FrameWindow { Content = asUc, DataContext = _resolvedView.DataContext, Height = asUc.Height + 50, Width = asUc.Width + 50 };
                if (_resolvedView is ITabProp) window.Title = ((ITabProp)_resolvedView).Header;
                if (asDialog) window.ShowDialog();
                else window.Show();
                return;
            }
            var asW = _resolvedView as Window;
            if (asW != null)
            {
                asW.Show();
                return;
            }
        }

        public bool PromptUser(string message)
        {
            return MessageBox.Show(message, "Prompt", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}