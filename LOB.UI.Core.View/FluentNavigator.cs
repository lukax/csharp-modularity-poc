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
using LOB.UI.Core.View.Names;
using LOB.UI.Interface;
using LOB.UI.Interface.Names;
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

        public IBaseView GetView(bool wrapInWindow = false)
        {
            if (_resolvedView == null)
                throw new ArgumentException("First resolveView the view", "ResolveView");
            if (_resolvedView.ViewModel == null)
                _resolvedView.ViewModel = _resolvedViewModel;
            if (wrapInWindow)
            {
                var window = _container.Resolve<ShellWindow>();
                window.Content = _resolvedView;
                window.DataContext = _resolvedView.ViewModel;
                window.Title = _resolvedView.Header;
                _resolvedView.InitializeServices();
                return window;
            }
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
            _resolvedViewModel = _container.Resolve(OperationType.ViewModels[OperationNamesParser.Parse(param)]) as IBaseViewModel;
            return this;
        }

        public IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel
        {
            _resolvedViewModel = _container.Resolve<TViewModel>();
            return this;
        }

        public IFluentNavigator ResolveView(string param)
        {
            _resolvedView = _container.Resolve(OperationType.Views[OperationNamesParser.Parse(param)]) as IBaseView;
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