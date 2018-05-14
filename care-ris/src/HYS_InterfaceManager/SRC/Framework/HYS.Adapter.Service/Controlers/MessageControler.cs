using System;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Service.Controlers
{
    public class MessageControler
    {
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set 
            {
                _enable = value;
            }
        }

        private string windowCaption;
        private AdapterMessage amRunning;
        private AdapterMessage amStopped;

        public MessageControler()
        {
            _enable = Program.ConfigMgt.Config.NotifyStatusToIM;
            windowCaption = Program.ConfigMgt.Config.IMWindowCaption;

            AttachAdapter();
        }
        private void AttachAdapter()
        {
            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;
            if (dir == null||dir.Header==null) return;

            int adapterID = 0;
            try
            {
                adapterID = int.Parse(dir.Header.ID.Trim());
            }
            catch
            {
                adapterID = -1;
            }

            amRunning = new AdapterMessage(adapterID, AdapterStatus.Running);
            amStopped = new AdapterMessage(adapterID, AdapterStatus.Stopped);

            Program.Log.Write("Messager initialized " +
                " (Enable=" + Enable.ToString() +
                ", AdapterID=" + adapterID.ToString() + 
                ", WindowCaption=" + windowCaption + ").");
        }

        public void NotifyServiceStart()
        {
            if (Enable && amRunning != null)
            {
                //int hwnd = Win32Api.FindWindow(null, windowCaption);
                //Program.Log.Write(hwnd.ToString() + " - " + windowCaption);

                if (amRunning.PostMessage(windowCaption))
                {
                    Program.Log.Write("Messager posts starting message succeeded.");
                }
                else
                {
                    Program.Log.Write(LogType.Warning,"Messager posts starting message failed." );
                }
            }
        }
        public void NotifyServiceStop()
        {
            if (Enable && amStopped != null)
            {
                //int hwnd = Win32Api.FindWindow(null, windowCaption);
                //Program.Log.Write(hwnd.ToString() + " - " + windowCaption);

                if (amStopped.PostMessage(windowCaption))
                {
                    Program.Log.Write("Messager posts stopping message succeeded.");
                }
                else
                {
                    Program.Log.Write(LogType.Warning, "Messager posts stopping message failed.");
                }
            }
        }
    }
}
