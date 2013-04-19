#region Usings

using System;
using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    [InheritedExport]
    public interface IFluentNavigator {
        IFluentNavigator Init { get; }
        IBaseView<TViewModel> GetView<TViewModel>() where TViewModel: IBaseViewModel;
        IBaseViewModel GetViewModel();
        IFluentNavigator SetView<TViewModel>(IBaseView<TViewModel> view) where TViewModel : IBaseViewModel;
        IFluentNavigator SetViewModel(IBaseViewModel viewModel);
        IFluentNavigator ResolveView(ViewID param);
        IFluentNavigator ResolveView<TView>() where TView : IBaseView<IBaseViewModel>;
        IFluentNavigator ResolveViewModel(ViewID param);
        IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        event OnOpenViewEventHandler OnOpenView;
        void AddToRegion(string regionName);

        [Obsolete("Use method AddToRegion")]
        void Show(bool asDialog = false);

        bool PromptUser(string message);
    }

    public sealed class OnOpenViewEventArgs : EventArgs {
        public OnOpenViewEventArgs(IBaseView<IBaseViewModel> baseView) { BaseView = baseView; }

        public IBaseView<IBaseViewModel> BaseView { get; private set; }
    }

    public delegate void OnOpenViewEventHandler(object sender, OnOpenViewEventArgs e);
}