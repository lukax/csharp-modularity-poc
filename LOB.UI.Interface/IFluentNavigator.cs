#region Usings

using System.ComponentModel.Composition;
using LOB.UI.Interface.ViewModel.Base;

#endregion

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface IFluentNavigator
    {
        IBaseView GetView();
        IBaseViewModel GetViewModel();
        IFluentNavigator ResolveView(string param);
        IFluentNavigator ResolveView<TView>() where TView : IBaseView;
        IFluentNavigator ResolveViewModel(string param);
        IFluentNavigator ResolveViewModel<TViewModel>() where TViewModel : IBaseViewModel;
        IFluentNavigator SetViewModel(IBaseViewModel viewModel);
        void Show(bool asDialog = false);
        bool PromptUser(string message);
    }
}