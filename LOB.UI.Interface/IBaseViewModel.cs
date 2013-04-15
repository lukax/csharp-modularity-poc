#region Usings

using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Interface {
    public interface IBaseViewModel : IUIComponent {
        void InitializeServices();
        void Refresh();
    }
}