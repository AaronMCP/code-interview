using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HYS.Adapter.Base
{
    public class AdapterMessage
    {
        public AdapterMessage()
        {
        }
        public AdapterMessage(int interfaceID, AdapterStatus status)
        {
            _interfaceID = interfaceID;
            _status = status;
        }

        public override string ToString()
        {
            return InterfaceID.ToString() + " " + Status.ToString();
        }

        private int _interfaceID;
        public int InterfaceID
        {
            get { return _interfaceID; }
        }

        private AdapterStatus _status;
        public AdapterStatus Status
        {
            get { return _status; }
        }

        public static AdapterMessage FromMessage(Message msg)
        {
            if (msg.Msg != MsgID) return null;
            AdapterMessage am = new AdapterMessage();
            am._interfaceID = msg.WParam.ToInt32();
            am._status = (AdapterStatus) (msg.LParam.ToInt32());
            return am;
        }

        public const int MsgID = 0xFFFF;

        public bool PostMessage(string windowCaption)
        {
            //int hwnd = Win32Api.FindWindow(null, windowCaption);
            //if (hwnd == 0) return false;

            //Win32Api.PostMessage((IntPtr)hwnd, MsgID, InterfaceID, (int)Status);
            try
            {
                NotifyAdapterServerClient client = new NotifyAdapterServerClient("StatusNotifier");
                client.NotifyStatusChanged(InterfaceID, (int)Status);
                return true;
            }
            catch (Exception err)
            {
                return false;
            }            
        }

        //public bool PostMessage3(string windowCaption)
        //{
        //    int hwnd = Win32Api.FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, windowCaption);
        //    Logging log = new Logging("c:\\adapterMessasge.log");
        //    log.Write(hwnd.ToString() + " ~ " + windowCaption + " ~ " + System.Diagnostics.Process.GetCurrentProcess().Id);
        //    if (hwnd == 0)
        //    {
        //        int errCode = Win32Api.GetLastError();
        //        log.Write("Last error: " + errCode.ToString());
        //        return false;
        //    }

        //    Win32Api.PostMessage((IntPtr)hwnd, MsgID, InterfaceID, (int)Status);
        //    return true;
        //}

        //public bool PostMessage2(string windowCaption)
        //{
        //    Process[] procList = Process.GetProcessesByName("DemoAdapter");

        //    Logging log = new Logging("c:\\adapterMessasge.log");

        //    if (procList == null || procList.Length < 1)
        //    {
        //        log.Write("cannot find process");
        //    }

        //    foreach (Process proc in procList)
        //    {
        //        log.Write( "process: " + proc.Id.ToString() + "-" + proc.ProcessName + "-" + proc.MainWindowTitle + "-" + proc.MainWindowHandle.ToString());
        //    }

        //    IntPtr hwnd = procList[0].MainWindowHandle;
        //    log.Write(hwnd.ToString() + " - DemoAdapter - Title: " + procList[0].MainWindowTitle + System.Diagnostics.Process.GetCurrentProcess().Id);

        //    Win32Api.PostMessage(hwnd, MsgID, InterfaceID, (int)Status);
        //    return true;
        //}
    }
}
