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
            this._container = container;
            this._regionAdapter = regionAdapter;
        }

        /// <summary>
        ///     Initialize with clean Fields
        /// </summary>
        public IFluentNavigator Init {
            get { return new FluentNavigator(this._container, this._regionAdapter); }
        }

        public event OnOpenViewEventHandler OnOpenView;

        public IBaseView GetView() {
            if(this._resolvedView == null) throw new ArgumentException("First resolve the view", "ResolveView");
            if(this._resolvedView.ViewModel == null) throw new ArgumentException("First resolve the view model", "ResolveViewModel");
            if(this._resolvedViewModel != null) this._resolvedViewModel.InitializeServices();
            this._resolvedView.InitializeServices();
            return this._resolvedView;
        }

        public IBaseViewModel GetViewModel() {
            if(this._resolvedViewModel == null) throw new ArgumentException("First resolveView the ViewModel", "ResolveViewModel");
            return this._resolvedViewModel;
        }

        public IFluentNavigator ResolveViewModel(OperationType param) {
            if(this._resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var resolved = this._container.Resolve(OperationTypeMapping.ViewModels[param]) as IBaseViewModel;
            if(resolved == null) throw new ArgumentException("param");
            this.SetViewModel(resolved);
            return this;
        }

        public IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel {
            if(this._resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var resolved = this._container.Resolve<TViewModel>();
            this.SetViewModel(resolved);
            return this;
        }

        public IFluentNavigator ResolveView(OperationType param) {
            if(this._resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var resolved = this._container.Resolve(OperationTypeMapping.Views[param]) as IBaseView;
            if(resolved == null) throw new ArgumentException("param");
            this.SetView(resolved);
            return this;
        }

        public IFluentNavigator ResolveView<TView>() where TView : IBaseView {
            if(this._resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            var resolved = this._container.Resolve<TView>();
            this.SetView(resolved);
            return this;
        }

        public IFluentNavigator SetViewModel(IBaseViewModel viewModel) {
            this._resolvedViewModel = viewModel;
            this._resolvedView.ViewModel = viewModel;
            return this;
        }

        public IFluentNavigator SetView(IBaseView view) {
            this._resolvedView = view;
            return this;
        }

        public void AddToRegion(string regionName) {
            this._regionAdapter.AddView(this.GetView(), regionName);
        }

        public void Show(bool asDialog = false) {
            var asUc = this.GetView() as UserControl;
            if(asUc != null) {
                var window = this._container.Resolve<FrameWindow>();
                window.Content = asUc;
                window.DataContext = this._resolvedView.ViewModel;
                window.Height = asUc.Height + 50;
                window.Width = asUc.Width + 50;
                if(!string.IsNullOrEmpty(this._resolvedView.Header)) window.Title = (this._resolvedView).Header;

                if(this.OnOpenView != null) this.OnOpenView.Invoke(this, new OnOpenViewEventArgs((IBaseView) asUc));

                if(asDialog) window.ShowDialog();
                else window.Show();
            }
            else {
                var asW = this._resolvedView as Window;
                if(asW != null) {
                    if(this.OnOpenView != null) this.OnOpenView.Invoke(this, new OnOpenViewEventArgs((IBaseView) asW));

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