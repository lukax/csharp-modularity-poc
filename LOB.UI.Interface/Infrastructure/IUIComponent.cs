#region Usings

using System;

#endregion

namespace LOB.UI.Interface.Infrastructure {
    public interface IUIComponent : IDisposable {

        UIOperation Operation { get; }
        string Header { get; }

    }
}