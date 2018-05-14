using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Device;
using HYS.Adapter.Service.Controlers;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Service.Services
{
    public partial class InboundService : AdapterService
    {
        public InboundService()
        {
            dataControler = new DataInAccessControler();
            BindInBoundAdapter();
        }

        private IInboundAdapterService inAdapter;
        private DataInAccessControler dataControler;

        private void BindInBoundAdapter()
        {
            if (CheckAdapter()) return;

            inAdapter = adapter as IInboundAdapterService;
            if (inAdapter == null)
            {
                Program.Log.Write(LogType.Error, "Cannot convert adapter instance into IInboundAdapterService.");
            }
            else
            {
                inAdapter.OnDataReceived += new DataReceiveEventHandler(inAdapter_OnDataReceived);
                Program.Log.Write("Adapter binded to inbound service.");
            }
        }

        private bool inAdapter_OnDataReceived(IInboundRule rule, DataSet data)
        {
            Program.Log.Write("-- Process inbound data begin --");

            bool result = false;

            if (data == null)
            {
                Program.Log.Write(LogType.Warning, "Receive a <null> data set.");
                goto EndSub;
            }

            if (rule == null)
            {
                Program.Log.Write(LogType.Warning, "Receive a <null> rule.");
                goto EndSub;
            }

            if (Program.ConfigMgt.Config.DumpData)
            {
                dataControler.WriteDataToFile(rule, data);
            }

            result = dataControler.ProcessInboundData(rule,data);

        EndSub:

            Program.Log.Write("-- Process inbound data end. Return value: " + result.ToString() + " --\r\n");

            return result;
        }
    }
}
