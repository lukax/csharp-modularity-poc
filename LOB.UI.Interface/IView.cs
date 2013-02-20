#region Usings

using System.ComponentModel.Composition;

#endregion

namespace LOB.UI.Interface
{
    [InheritedExport]
    public interface IView
    {
        void InitializeServices();
        void Refresh();
    }
}