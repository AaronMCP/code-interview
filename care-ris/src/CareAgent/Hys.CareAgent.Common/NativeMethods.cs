#region

using System;
using System.Runtime.InteropServices;

#endregion

namespace Hys.CareAgent.Common
{
    public static class NativeMethods
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}