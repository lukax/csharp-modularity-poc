#region Usings

using System;
using LOB.UI.Interface.Infrastructure;

#endregion

namespace LOB.UI.Interface {
    public interface IBaseViewModel : IUIComponent {
        Guid Id { get; }
        ViewState ViewState { get; }
        bool IsChild { get; set; }
        void InitializeServices();
        void Refresh();
    }
}