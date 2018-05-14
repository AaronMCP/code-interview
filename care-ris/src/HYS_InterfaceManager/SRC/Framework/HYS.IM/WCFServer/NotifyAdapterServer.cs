using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Windows.Forms;
using HYS.Adapter.Base;
using System.Diagnostics;
using HYS.Common.Objects.Logging;

namespace HYS.IM.WCFServer
{
    internal class NotifyAdapterServer
    {
        private ServiceHost _host = null;
        private Uri _serviceUri = new Uri("net.pipe://localhost/");
        private string _pipeName = "StatusNotifier";

        private NotifyAdapterStatusService _service = new NotifyAdapterStatusService();


        public NotifyAdapterServer(string strServerName)
        {
            _pipeName = strServerName;
        }

        public void Start()
        {
            try
            {
                _host = new ServiceHost(typeof(NotifyAdapterStatusService), _serviceUri);
                _host.AddServiceEndpoint(typeof(INotifyAdapterStatus), new NetNamedPipeBinding(), _pipeName);
                _host.Open();
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Warning, "Start Notify Server failed.");
                Program.Log.Write(LogType.Warning,err.ToString());
            }
        }

        public void Stop()
        {
            try
            {
                if ((_host != null) && (_host.State != CommunicationState.Closed))
                {
                    _host.Close();
                    _host = null;
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Warning, "Stop Notify Server failed.");
                Program.Log.Write(LogType.Warning, err.ToString());
            }
        }
    }

    public class NotifyAdapterStatusService : INotifyAdapterStatus
    {
        #region INotifyAdapterStatus Members

        public void NotifyStatusChanged(int interfaceID, int status)
        {
            Process cProc = Process.GetCurrentProcess();
            Win32Api.PostMessage(cProc.MainWindowHandle, AdapterMessage.MsgID, interfaceID, (int)status);
        }

        #endregion
    }
}
