#region Usings

using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Interface
{
    public interface IBaseViewModel : IUIComponent
    {
        string Header { get; set; }
        void InitializeServices();
        void Refresh();
    }
}