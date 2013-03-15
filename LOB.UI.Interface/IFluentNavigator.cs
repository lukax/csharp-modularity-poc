#region Usings

using System;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Interface
{
    public interface IFluentNavigator
    {
        IBaseView GetView();
        IBaseViewModel GetViewModel();
        IFluentNavigator ResolveView(string param);
        IFluentNavigator ResolveView<TView>() where TView : IBaseView;
        IFluentNavigator ResolveViewModel(string param);
        IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        IFluentNavigator SetViewModel(IBaseViewModel viewModel);
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