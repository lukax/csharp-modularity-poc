#region Usings

using System;
using System.Windows;
using System.Windows.Controls;
using LOB.UI.Core.View.Controls.Util;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Unity;
using MessageBox = Xceed.Wpf.Toolkit.MessageBox;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    public class FluentNavigator : IFluentNavigator {

        private readonly IUnityContainer _container;
        private readonly IRegionAdapter _regionAdapter;
        private IBaseView _resolvedView;
        private IBaseViewModel _resolvedViewModel;

        [InjectionConstructor] public FluentNavigator(IUnityContainer container, IRegionAdapter regionAdapter) {
            _container = container;
            _regionAdapter = regionAdapter;
        }

        /// <summary>
        ///     Initialize with clean Fields
        /// </summary>
        public IFluentNavigator Init {
            get { return new FluentNavigator(_container, _regionAdapter); }
        }

        public event OnOpenViewEventHandler OnOpenView;

        public IBaseView GetView() {
            if(_resolvedView == null) throw new ArgumentException("First resolve the view", "ResolveView");
            if(_resolvedView.ViewModel == null) throw new ArgumentException("First resolve the view model", "ResolveViewModel");
            if(_resolvedViewModel != null) _resolvedViewModel.InitializeServices();
            _resolvedView.InitializeServices();
            return _resolvedView;
        }

        public IBaseViewModel GetViewModel() {
            if(_resolvedViewModel == null) throw new ArgumentException("First resolveView the ViewModel", "ResolveViewModel");
            return _resolvedViewModel;
        }

        public IFluentNavigator ResolveViewModel(OperationType param) {
            if(_resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var resolved = _container.Resolve(OperationTypeMapping.ViewModels[param]) as IBaseViewModel;
            if(resolved == null) throw new ArgumentException("param");
            SetViewModel(resolved);
            return this;
        }

        public IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel {
            if(_resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var resolved = _container.Resolve<TViewModel>();
            SetViewModel(resolved);
            return this;
        }

        public IFluentNavigator ResolveView(OperationType param) {
            if(_resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var resolved = _container.Resolve(OperationTypeMapping.Views[param]) as IBaseView;
            if(resolved == null) throw new ArgumentException("param");
            SetView(resolved);
            return this;
        }

        public IFluentNavigator ResolveView<TView>() where TView : IBaseView {
            if(_resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var resolved = _container.Resolve<TView>();
            SetView(resolved);
            return this;
        }

        public IFluentNavigator SetViewModel(IBaseViewModel viewModel) {
            _resolvedViewModel = viewModel;
            _resolvedView.ViewModel = viewModel;
            return this;
        }

        public IFluentNavigator SetView(IBaseView view) {
            _resolvedView = view;
            return this;
        }

        public void AddToRegion(string regionName) {
            _regionAdapter.AddView(GetView(), regionName);
        }

        public void Show(bool asDialog = false) {
            var asUc = GetView() as UserControl;
            if(asUc != null) {
                var window = _container.Resolve<FrameWindow>();
                window.Content = asUc;
                window.DataContext = _resolvedView.ViewModel;
                window.Height = asUc.Height + 50;
                window.Width = asUc.Width + 50;
                if(!string.IsNullOrEmpty(_resolvedView.Header)) window.Title = (_resolvedView).Header;

                if(OnOpenView != null) OnOpenView.Invoke(this, new OnOpenViewEventArgs((IBaseView) asUc));

                if(asDialog) window.ShowDialog();
                else window.Show();
            }
            else {
                var asW = _resolvedView as Window;
                if(asW != null) {
                    if(OnOpenView != null) OnOpenView.Invoke(this, new OnOpenViewEventArgs((IBaseView) asW));

                    if(asDialog) asW.ShowDialog();
                    else asW.Show();
                }
            }
        }

        public bool PromptUser(string message) {
            return MessageBox.Show(message, "Prompt", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
        }

    }
}