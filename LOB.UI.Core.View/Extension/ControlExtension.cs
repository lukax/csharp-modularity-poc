#region Usings

using System;
using System.Windows.Controls;

#endregion

namespace LOB.UI.Core.View.Extension
{
    public static class ControlExtension
    {
        public static void UpdateSafely(this Control control, Action func)
        {
            if (!control.CheckAccess())
                control.Dispatcher.BeginInvoke(func);
            else
                func.Invoke();
        }
    }
}