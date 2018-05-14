using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace HYS.Adapter.Base
{
    public class Win32Api
    {
        //[DllImport("kernel32.dll")]
        //public static extern int GetLastError();

        [DllImport("user32.dll")]
        public static extern int FindWindow(string strclassName, string strWindowName);

        //[DllImport("user32.dll")]
        //public static extern int FindWindowEx(IntPtr hwndParent, IntPtr hwndChild, string strclassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern void PostMessage(IntPtr hwnd, int msgId, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam); 

        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hWnd, int nCmd);

        public const int SW_RESTORE = 9;
        public const int SW_SHOW = 5;
    }
}
