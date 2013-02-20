#region Usings

using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface INavigator
    {
        void Startup<TView>() where TView : class;
        void Startup<TView>(object viewModel) where TView : class;
        void OpenView<TView>(string regionName) where TView : class;
        void OpenView<TView>(string regionName, object viewModel) where TView : class;
        object ResolveView(string param);
    }
}