using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Windows.Forms;
using HYS.IM.Messaging.Base;
using System.Diagnostics;
using HYS.IM.Common.Logging;
using HYS.HL7IM.Manager.Forms;

namespace CSH.eHeath.HL7Gateway.Manager
{
    internal class NotifyAdapterServer
    {
        private ServiceHost _host = null;
        private Uri _serviceUri = null;
        private string _pipeName = "StatusNotifier";

        private NotifyAdapterStatusService _service = new NotifyAdapterStatusService();


        public NotifyAdapterServer(string strServerName)
        {
            _pipeName = strServerName;
            _serviceUri = new Uri("net.pipe://localhost/" + _pipeName + "/");
        }

        public void Start()
        {
            try
            {
                _host = new ServiceHost(typeof(NotifyAdapterStatusService), _serviceUri);
                _host.AddServiceEndpoint(typeof(INotifyAdapterStatus), new NetNamedPipeBinding(), _pipeName);
                _host.Open();
                Program.Log.Write("Notify server started.");
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
                    Program.Log.Write("Notify server stopped.");
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

        public void NotifyStatusChanged(string strServiceName, int status)
        {
            Process cProc = Process.GetCurrentProcess();
            if (cProc.MainWindowHandle == null || cProc.MainWindowHandle == IntPtr.Zero)
            {
                return;
            }

            FormMain frm = Form.FromHandle(cProc.MainWindowHandle) as FormMain;

            frm.RefreshServiceStatus(strServiceName, status);
            
        }

        #endregion
    }
}
