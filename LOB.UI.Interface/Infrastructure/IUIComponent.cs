#region Usings

using System;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public interface IUIComponent : IDisposable {
        ViewID ViewID { get; }
        string Header { get; }
    }
}