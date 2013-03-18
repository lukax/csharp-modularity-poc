#region Usings

using System;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Interface
{
    public interface IFluentNavigator
    {
        IFluentNavigator Init { get; }
        IBaseView GetView();
        IBaseViewModel GetViewModel();
        IFluentNavigator SetView(IBaseView view);
        IFluentNavigator SetViewModel(IBaseViewModel viewModel);
        IFluentNavigator ResolveView(string param);
        IFluentNavigator ResolveView<TView>() where TView : IBaseView;
        IFluentNavigator ResolveViewModel(string param);
        IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        event OnOpenViewEventHandler OnOpenView;
        void Show(bool asDialog = false);
        bool PromptUser(string message);
    }

    public sealed class OnOpenViewEventArgs : EventArgs
    {
        public OnOpenViewEventArgs(IBaseView baseView)
        {
            BaseView = baseView;
        }

        public IBaseView BaseView { get; private set; }
    }

    public delegate void OnOpenViewEventHandler(object sender, OnOpenViewEventArgs e);
}