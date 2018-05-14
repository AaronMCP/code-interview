using System;
using System.Collections.Generic;
using System.Text;
using HYS.Adapter.Base;
using HYS.Common.Objects;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Translation;
using HYS.SocketAdapter.Common;
using HYS.SocketAdapter.Configuration;



namespace HYS.SocketAdapter.SocketOutboundAdapterConfiguration
{
    [AdapterConfigEntry("SOCKET_Out_CONFIG", DirectionType.OUTBOUND, "Socket Outbound Adapter for GC Gateway")]
    public class ConfigMain :  IOutboundAdapterConfig        
    {
        private FSocketOutConfiguration frm;

        #region IOutboundAdapterConfig Members

        public HYS.Common.Objects.Rule.IOutboundRule[] GetRules()
        {
            List <IOutboundRule> rules = new List<IOutboundRule>();
            foreach (SocketOutChannel ch in SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.OutboundChanels)
            {
                if (!IsExist(ch, GWDataDBField.i_IndexGuid.FieldName))
                {
                    InsertDataID(ch);
                }
                if (!IsExist(ch, GWDataDBField.i_DataDateTime.FieldName))
                {
                    InsertDataDT(ch);
                }

                rules.Add(ch.Rule);
            }

            return rules.ToArray();
        }

        #endregion

        #region IAdapterConfig Members

        public IConfigUI[] GetConfigUI()
        {
            frm = new FSocketOutConfiguration();

            return new IConfigUI[] { frm };
        }

        public string GetConfigurationSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Server IP:").Append(SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams.ServerIP).Append("; ");
            sb.Append("Server Port:").Append(SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams.ServerPort.ToString()).Append("; ");
            return sb.ToString();
        }

        public HYS.Common.Objects.Rule.LookupTable[] GetLookupTables()
        {
            List<LookupTable> lts = new List<LookupTable>();
            foreach (LookupTable table in SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.LookupTables)
                lts.Add(table);
            return lts.ToArray();
        }

        public AdapterOption Option
        {
            get { return new AdapterOption(DirectionType.OUTBOUND); }
        }

        #endregion

        #region IAdapterBase Members

        public bool Exit(string[] arguments)
        {
            return true;
        }

        public bool Initialize(string[] arguments)
        {
            Program.PreLoading();

            if (arguments != null && arguments.Length > 0)
            {
                string str = arguments[0];
                //Program.Log.Write("GWDataDB connection: " + str); //contains db pw
                Program.GWDataDBConnection = str;
            }

            return true;
        }

        public Exception LastError
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion

        #region Insert DataID & DataTD
        private void InsertDataID(SocketOutChannel chn)
        {
            SocketOutQueryResultItem item = new SocketOutQueryResultItem();

            item.ThirdPartyDBPatamter.FieldName = "";
            item.TargetField = "data_id";
            item.GWDataDBField = GWDataDBField.i_IndexGuid;
            item.SourceField = item.GWDataDBField.FieldName;

            chn.Rule.QueryResult.MappingList.Add(item);
        }

        private void InsertDataDT(SocketOutChannel chn)
        {
            SocketOutQueryResultItem item = new SocketOutQueryResultItem();

            item.ThirdPartyDBPatamter.FieldName = "";
            item.TargetField = "data_dt";
            item.GWDataDBField = GWDataDBField.i_DataDateTime;
            item.SourceField = item.GWDataDBField.FieldName;

            chn.Rule.QueryResult.MappingList.Add(item);
        }

        private bool IsExist(SocketOutChannel ch, string fieldname)
        {
            foreach (SocketOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.TargetField == fieldname)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }

    //[ApdaterEntry("SQL_IN_Adapter", DirectionType.Inbound, "SQL Inbound Adapter for GC Gateway")]
    //public class AdapterMain : IInBoundAdapter
    //{

    //    private SQLInboundAdapterConfiguration frm;

    //    #region IInBoundAdapter Members

    //    #endregion

    //    #region IAdapter Members

    //    public bool Exit(string[] arguments)
    //    {
    //        return true;
    //    }

    //    public IConfigUI[] GetConfigUI()
    //    {
    //        frm = new SQLInboundAdapterConfiguration();

    //        return new IConfigUI[] { frm };
    //    }

    //    public DeviceDir GetInfor()
    //    {
    //        DeviceDirManager dirMgt = new DeviceDirManager();
    //        dirMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
    //        dirMgt.LoadDeviceDir();
    //        return dirMgt.DeviceDirInfor;
    //    }

    //    public bool Initialize(string[] arguments)
    //    {
    //        Program.PreLoading();
    //        return true;
    //    }

    //    public Exception LastError
    //    {
    //        get { return null; }
    //    }

    //    public AdapterOption Option
    //    {
    //        get { return new AdapterOption(DirectionType.Inbound); }
    //    }

    //    public bool Start(string[] arguments)
    //    {
    //        return true;
    //    }

    //    public AdapterStatus Status
    //    {
    //        get { return AdapterStatus.Other; }
    //    }

    //    public bool Stop(string[] arguments)
    //    {
    //        return true;
    //    }

    //    #endregion

    //    #region IInBoundAdapter Members

    //    public event DataReceiveEventHandler OnDataReceived;

    //    #endregion
    //}
}
