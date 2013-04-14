#region Usings

using System;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public interface IUIComponent : IDisposable {

        ViewID Operation { get; }
        string Header { get; }

    }
}