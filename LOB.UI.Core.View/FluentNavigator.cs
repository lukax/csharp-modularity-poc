#region Usings

using System;
using System.Windows;
using System.Windows.Controls;
using LOB.Domain.Base;
using LOB.UI.Core.View.Controls.Alter;
using LOB.UI.Core.View.Controls.Alter.Base;
using LOB.UI.Core.View.Controls.Alter.SubEntity;
using LOB.UI.Core.View.Controls.List;
using LOB.UI.Core.View.Controls.List.Base;
using LOB.UI.Core.View.Controls.List.SubEntity;
using LOB.UI.Core.View.Controls.Sell;
using LOB.UI.Interface;
using LOB.UI.Interface.ViewModel.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter;
using LOB.UI.Interface.ViewModel.Controls.Alter.Base;
using LOB.UI.Interface.ViewModel.Controls.Alter.SubEntity;
using LOB.UI.Interface.ViewModel.Controls.List;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using LOB.UI.Interface.ViewModel.Controls.List.SubEntity;
using LOB.UI.Interface.ViewModel.Controls.Sell;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.View
{
    public class FluentNavigator : IFluentNavigator
    {
        private readonly IUnityContainer _container;
        private readonly IRegionAdapter _regionAdapter;
        private IBaseView _resolvedView;
        private IBaseViewModel _resolvedViewModel;

        [InjectionConstructor]
        public FluentNavigator(IUnityContainer container, IRegionAdapter regionAdapter)
        {
            _container = container;
            _regionAdapter = regionAdapter;
        }

        public event OnOpenViewEventHandler OnOpenView;

        public IBaseView GetView()
        {
            if (_resolvedView == null)
                throw new ArgumentException("First resolveView the view", "ResolveView");
            if (_resolvedView.ViewModel == null)
                _resolvedView.ViewModel = _resolvedViewModel;

            _resolvedView.InitializeServices();
            return _resolvedView;
        }

        public IBaseViewModel GetViewModel()
        {
            if (_resolvedViewModel == null)
                throw new ArgumentException("First resolveView the ViewModel", "ResolveViewModel");
            return _resolvedViewModel;
        }

        public IFluentNavigator ResolveViewModel(string param)
        {
            switch (param)
            {
                case "SellProduct":
                    _resolvedViewModel = _container.Resolve<ISellProductViewModel>();
                    break;
                case "SellService":
                    _resolvedViewModel = _container.Resolve<ISellServiceViewModel>();
                    break;
                case "AlterProduct":
                    _resolvedViewModel = _container.Resolve<IAlterProductViewModel>();
                    break;
                case "AlterLegalPerson":
                    _resolvedViewModel = _container.Resolve<IAlterLegalPersonViewModel>();
                    break;
                case "AlterNaturalPerson":
                    _resolvedViewModel = _container.Resolve<IAlterNaturalPersonViewModel>();
                    break;
                case "ListProduct":
                    _resolvedViewModel = _container.Resolve<IListProductViewModel>();
                    break;
                case "AlterEmployee":
                    _resolvedViewModel = _container.Resolve<IAlterEmployeeViewModel>();
                    break;
                case "ListEmployee":
                    _resolvedViewModel = _container.Resolve<IListEmployeeViewModel>();
                    break;
                case "AlterClient":
                    _resolvedViewModel = _container.Resolve<IAlterCustomerViewModel>();
                    break;
                case "ListClient":
                    _resolvedViewModel = _container.Resolve<IListCustomerViewModel>();
                    break;
                case "AlterCategory":
                    _resolvedViewModel = _container.Resolve<IAlterCategoryViewModel>();
                    break;
                case "AlterService":
                    _resolvedViewModel = _container.Resolve<IAlterServiceViewModel<Service>>();
                    break;
                case "ListCategory":
                    _resolvedViewModel = _container.Resolve<IListCategoryViewModel>();
                    break;
                case "ListService":
                    _resolvedViewModel = _container.Resolve<IListServiceViewModel<Service>>();
                    break;
                case "AlterSale":
                    _resolvedViewModel = _container.Resolve<IAlterSaleViewModel>();
                    break;
                case "ListSale":
                    throw new NotImplementedException();
                case "AlterEmail":
                    _resolvedViewModel = _container.Resolve<IAlterEmailViewModel>();
                    break;
                case "ListEmail":
                    _resolvedViewModel = _container.Resolve<IListEmailViewModel>();
                    break;
                case "AlterPhoneNumber":
                    _resolvedViewModel = _container.Resolve<IAlterPhoneNumberViewModel>();
                    break;
                case "ListPhoneNumber":
                    _resolvedViewModel = _container.Resolve<IListPhoneNumberViewModel>();
                    break;
                case "AlterContactInfo":
                    _resolvedViewModel = _container.Resolve<IAlterContactInfoViewModel>();
                    break;
                default:
                    throw new ArgumentException("Parameter not implemented yet, ", "param");
            }
            return this;
        }

        public IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel
        {
            _resolvedViewModel = _container.Resolve<TViewModel>();
            return this;
        }

        public IFluentNavigator ResolveView(string param)
        {
            switch (param)
            {
                case "SellProduct":
                    _resolvedView = _container.Resolve<SellProductView>();
                    break;
                case "SellService":
                    _resolvedView = _container.Resolve<SellServiceView>();
                    break;
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
                case "ListSale":
                    throw new NotImplementedException();
                case "AlterEmail":
                    _resolvedView = _container.Resolve<AlterEmailView>();
                    break;
                case "ListEmail":
                    _resolvedView = _container.Resolve<ListEmailView>();
                    break;
                case "AlterPhoneNumber":
                    _resolvedView = _container.Resolve<AlterPhoneNumberView>();
                    break;
                case "ListPhoneNumber":
                    _resolvedView = _container.Resolve<ListPhoneNumberView>();
                    break;
                case "AlterContactInfo":
                    _resolvedView = _container.Resolve<AlterContactInfoView>();
                    break;
                default:
                    throw new ArgumentException("Parameter not implemented yet, ", "param");
            }

            return this;
        }

        public IFluentNavigator ResolveView<TView>() where TView : IBaseView
        {
            _resolvedView = _container.Resolve<TView>();
            return this;
        }

        public IFluentNavigator SetViewModel(IBaseViewModel viewModel)
        {
            _resolvedView.ViewModel = viewModel;
            return this;
        }

        public void Show(bool asDialog = false)
        {
            var asUc = GetView() as UserControl;
            if (asUc != null)
            {

                var window = _container.Resolve<ShellWindow>();
                window.Content = asUc;
                window.DataContext = _resolvedView.ViewModel;
                window.Height = asUc.Height + 50;
                window.Width = asUc.Width + 50;
                window.Title = (_resolvedView).Header;


                if (OnOpenView != null)
                    OnOpenView.Invoke(this, new OnOpenViewEventArgs((IBaseView)asUc));

                if (asDialog) window.ShowDialog();
                else window.Show();
            }
            else
            {
                var asW = _resolvedView as Window;
                if (asW != null)
                {
                    if (OnOpenView != null)
                        OnOpenView.Invoke(this, new OnOpenViewEventArgs((IBaseView)asW));

                    if (asDialog) asW.ShowDialog();
                    else asW.Show();
                }
            }
        }

        public bool PromptUser(string message)
        {
            return MessageBox.Show(message, "Prompt", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }
    }
}