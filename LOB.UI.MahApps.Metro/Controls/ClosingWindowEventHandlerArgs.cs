#region Usings

using System;

#endregion

namespace MahApps.Metro.Controls
{
    public class ClosingWindowEventHandlerArgs : EventArgs
    {
        public bool Cancelled { get; set; }
    }
}