#region Usings

using System;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public interface IFluentNavigator {
        
        IFluentNavigator Init { get; }
        IBaseView GetView();
        IBaseViewModel GetViewModel();
        IFluentNavigator SetView(IBaseView view);
        IFluentNavigator SetViewModel(IBaseViewModel viewModel);
        IFluentNavigator ResolveView(ViewID param);
        IFluentNavigator ResolveView<TView>() where TView : IBaseView;
        IFluentNavigator ResolveViewModel(ViewID param);
        IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        event OnOpenViewEventHandler OnOpenView;
        void AddToRegion(string regionName);

        [Obsolete("Use method AddToRegion")]
        void Show(bool asDialog = false);

        bool PromptUser(string message);

    }

    public sealed class OnOpenViewEventArgs : EventArgs {

        public OnOpenViewEventArgs(IBaseView baseView) { BaseView = baseView; }

        public IBaseView BaseView { get; private set; }

    }

    public delegate void OnOpenViewEventHandler(object sender, OnOpenViewEventArgs e);
}