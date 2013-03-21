#region Usings

using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Interface
{
    public interface IBaseView : IUIComponent
    {
        IBaseViewModel ViewModel { get; set; }
        string Header { get; set; }
        int? Index { get; set; }
        void InitializeServices();
        void Refresh();
    }
}