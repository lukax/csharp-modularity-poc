#region Usings

using System.ComponentModel.Composition;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Interface {
    [InheritedExport]
    public interface IBaseViewModel : IUIComponent {
        void InitializeServices();
        void Refresh();
    }
}