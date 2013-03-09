#region Usings

using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface IFluentNavigator
    {
        object Get();
        TView As<TView>() where TView : class;
        IFluentNavigator ResolveView(string param, object viewModel = null);
        IFluentNavigator ResolveView<TView>(object viewModel = null);
        IFluentNavigator SetViewModel(object viewModel);
        void Show(bool asDialog = false);
        bool PromptUser(string message);
    }
}