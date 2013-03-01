#region Usings

using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface INavigator
    {
        object GetView { get; }
        void Startup<TView>(object viewModel = null) where TView : class;
        void OpenView<TView>(string regionName, object viewModel = null) where TView : class;
        INavigator ResolveView(string param, object viewModel = null);
        void StartView(bool asDialog = false);
        bool PromptUser(string message);
    }

    [InheritedExport]
    public interface IFluentNavigator
    {
        IFluentNavigator Resolve(string param, object viewModel = null);
        IFluentNavigator SetViewModel(object viewModel);
        void Show(bool asDialog = false);
        object Get();
    }
}