#region Usings

using System;
using LOB.UI.Contract.Infrastructure;

#endregion

namespace LOB.UI.Contract {
    public interface IBaseViewModel : IUIComponent {
        Guid Id { get; }
        ViewState ViewState { get; }
        bool IsChild { get; }
        void InitializeServices();
        void Refresh();
    }
}