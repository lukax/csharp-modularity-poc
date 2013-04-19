#region Usings

using System;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public interface IFluentNavigator {
        IFluentNavigator Init { get; }
        IBaseView<IBaseViewModel> GetView();
        IBaseViewModel GetViewModel();
        IFluentNavigator SetView(IBaseView<IBaseViewModel> view);
        IFluentNavigator SetViewModel(IBaseViewModel viewModel);
        IFluentNavigator ResolveView(ViewID param);
        IFluentNavigator ResolveView<TView>() where TView : IBaseView<IBaseViewModel>;
        IFluentNavigator ResolveViewModel(ViewID param);
        IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        event OnOpenViewEventHandler OnOpenView;
        IFluentNavigator AddToRegion(string regionName);
    }

    public sealed class OnOpenViewEventArgs : EventArgs {
        public OnOpenViewEventArgs(IBaseView<IBaseViewModel> baseView) { BaseView = baseView; }

        public IBaseView<IBaseViewModel> BaseView { get; private set; }
    }

    public delegate void OnOpenViewEventHandler(object sender, OnOpenViewEventArgs e);
}