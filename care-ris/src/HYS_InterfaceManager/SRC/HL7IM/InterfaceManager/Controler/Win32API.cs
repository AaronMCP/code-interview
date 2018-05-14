using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HYS.HL7IM.Manager.Controler
{
    internal class Win32API
    {
        [DllImport("User32.dll")]
        internal static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);
        internal const int WS_SHOWNORMAL = 1;
    }
}
