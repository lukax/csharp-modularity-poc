#region Usings

using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using LOB.Core.Localization;
using LOB.UI.Interface;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Core.View.Infrastructure {
    [Export(typeof(IFluentNavigator))]
    public class FluentNavigator : IFluentNavigator {
        private readonly CompositionContainer _container;
        private readonly IRegionAdapter _regionAdapter;
        private IBaseView<IBaseViewModel> _resolvedView;
        private IBaseViewModel _resolvedViewModel;

        [ImportingConstructor]
        public FluentNavigator( //CompositionContainer container, 
            IRegionAdapter regionAdapter) {
            //_container = container;
            _regionAdapter = regionAdapter;
        }

        /// <summary>
        ///     Initialize with clean Fields
        /// </summary>
        public IFluentNavigator Init {
            get {
                return new FluentNavigator( //_container,
                    _regionAdapter);
            }
        }

        public event OnOpenViewEventHandler OnOpenView;

        public IBaseView<IBaseViewModel> GetView() {
            if(_resolvedView == null) throw new ArgumentException(Strings.Notification_Navigator_View_ResolveFirst);
            if(_resolvedView.ViewModel == null) throw new ArgumentException(Strings.Notification_Navigator_ViewModel_ResolveFirst);
            return _resolvedView;
        }

        public IBaseViewModel GetViewModel() {
            if(_resolvedViewModel == null) throw new ArgumentException(Strings.Notification_Navigator_ViewModel_ResolveFirst);
            return _resolvedViewModel;
        }

        public IFluentNavigator ResolveViewModel(ViewModelState param) {
            if(_resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");

//            var resolved = _container.GetInstance(ViewDictionary.Views[param]) as IBaseViewModel;
//            dynamic resolvedd = resolved;
//            try {
//                resolvedd.ViewModelState = param; //TODO: Maybe change this later
//            } catch(NullReferenceException ex) {
//#if DEBUG
//                Debug.WriteLine(ex.Message);
//#endif
//            }
//            if(resolved == null) throw new ArgumentException("param");
//            SetViewModel(() => resolved);
            return this;
        }

        public IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel {
            //if(_resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            //var resolved = _container.GetInstance<TViewModel>();
            //SetViewModel(resolved);
            return this;
        }

        public IFluentNavigator ResolveView(ViewModelState param) {
            if(_resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            //var resolved = _container.GetInstance(ViewDictionary.Views[param]) as IBaseView<IBaseViewModel>;
            //if(resolved == null) throw new ArgumentException("param");
            //SetView(resolved);
            return this;
        }

        public IFluentNavigator ResolveView<TView>() where TView : IBaseView<IBaseViewModel> {
            if(_resolvedViewModel != null) throw new InvalidOperationException("First Init the FluentNavigator to clean fields.");
            //var resolved = _container.GetInstance<TView>();
            //SetView(resolved);
            return this;
        }

        public IFluentNavigator SetViewModel(Func<IBaseViewModel> viewModel) {
            _resolvedViewModel = viewModel();
            if(_resolvedView != null) _resolvedView.ViewModel = _resolvedViewModel;
            return this;
        }

        public IFluentNavigator SetView(Func<IBaseView<IBaseViewModel>> view) {
            _resolvedView = view();
            return this;
        }

        public IFluentNavigator AddToRegion(string regionName) {
            //var view = GetView();
            //view.ViewModel.ViewID.IsChild(false);
            //_regionAdapter.AddView(view, regionName);
            return this;
        }
    }
}