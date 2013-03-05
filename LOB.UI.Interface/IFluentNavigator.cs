using System.ComponentModel.Composition;

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface IFluentNavigator
    {
        object Get();
        TView As<TView>() where TView : class;
        IFluentNavigator Resolve(string param, object viewModel = null);
        IFluentNavigator Resolve<TView>(object viewModel = null);
        IFluentNavigator SetViewModel(object viewModel);
        void Show(bool asDialog = false);
        bool PromptUser(string message);
    }
}