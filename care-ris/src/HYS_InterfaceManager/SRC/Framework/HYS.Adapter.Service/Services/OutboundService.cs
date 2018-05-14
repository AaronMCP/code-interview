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
    partial class OutboundService : AdapterService
    {
        public OutboundService()
        {
            dataControler = new DataOutAccessControler();
            BindInBoundAdapter();
        }

        private IOutboundAdapterService outAdapter;
        private DataOutAccessControler dataControler;

        private void BindInBoundAdapter()
        {
            if (CheckAdapter()) return;

            outAdapter = adapter as IOutboundAdapterService;
            if (outAdapter == null)
            {
                Program.Log.Write(LogType.Error, "Cannot convert adapter instance into IOutboundAdapterService.");
            }
            else
            {
                outAdapter.OnDataRequest += new DataRequestEventHanlder(outAdapter_OnDataRequest);
                outAdapter.OnDataDischarge += new DataDischargeEventHanlder(outAdapter_OnDataDischarge);
                Program.Log.Write("Adapter binded to outbound service.");
            }
        }

        private bool outAdapter_OnDataDischarge(string[] guidList)
        {
            Program.Log.Write("-- Discharge data begin --");

            bool result = false;

            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;
            if (dir == null)
            {
                Program.Log.Write(LogType.Warning, "Cannot get adapter DeviceDir information.");
                goto EndSub;
            }

            if (guidList == null)
            {
                Program.Log.Write(LogType.Warning, "Receive a <null> guidList.");
                goto EndSub;
            }

            if (Program.ConfigMgt.Config.DumpData)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string id in guidList)
                {
                    sb.AppendLine(id);
                }

                Program.Log.Write("GUID List (" + guidList.Length.ToString() + "):");
                Program.Log.Write(sb.ToString(),false);
            }

            result = dataControler.SetProcessFlag(dir.Header.Name, guidList);

        EndSub:

            Program.Log.Write("-- Discharge data end. Return value: " + result.ToString() + " --\r\n");

            return result;
        }

        private DataSet outAdapter_OnDataRequest(IOutboundRule rule, DataSet criteria)
        {
            Program.Log.Write("-- Prepare outbound data begin --");

            DataSet result = null;

            if (rule == null)
            {
                Program.Log.Write(LogType.Warning, "Receive a <null> rule.");
                goto EndSub;
            }

            if (criteria == null)
            {
                Program.Log.Write(LogType.Warning, "Receive a <null> criteria data set.");
            }

            if (Program.ConfigMgt.Config.DumpData)
            {
                dataControler.WriteDataToFile(DataAccessControler.GetSPName(rule) + "_QC", criteria);
            }

            result = dataControler.PrepareOutboundData(rule, criteria);

            if (Program.ConfigMgt.Config.DumpData)
            {
                dataControler.WriteDataToFile(DataAccessControler.GetSPName(rule) + "_QR", result);
            }

        EndSub:

            if (result == null)
            {
                Program.Log.Write("-- Prepare outbound data end. Return data set: <null> --\r\n");
            }
            else
            {
                int rowCount = -1;
                if (result.Tables.Count > 0) rowCount = result.Tables[0].Rows.Count;
                Program.Log.Write("-- Prepare outbound data end. Return data set, number of rows : " + rowCount.ToString() + " --\r\n");
            }
            
            return result;
        }
    }
}
