#region Usings

using System.Runtime.InteropServices;

#endregion

namespace MahApps.Metro.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MARGINS
    {
        public int leftWidth;
        public int rightWidth;
        public int topHeight;
        public int bottomHeight;
    }
}