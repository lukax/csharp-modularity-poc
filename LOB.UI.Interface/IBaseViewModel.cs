#region Usings

using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Interface {
    public interface IBaseViewModel : IUIComponent {
        ViewModelState State { get; set; }
        void InitializeServices();
        void Refresh();
    }
}