using System;
using System.Data;
using System.Timers;
using System.Data.OleDb;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Command;
using HYS.SocketAdapter.Common;
using HYS.SocketAdapter.Configuration;


namespace HYS.SocketAdapter.SocketInboundAdapter
{
    public class DataAccessControler
    {
        public DataAccessControler()
        {
            InitializeServerSocket();
        }

        #region Timer Control

        public bool Start()
        {
            Program.Log.Write("Adapter Start");            

            _ServerSocket.ReceiveTimeout = SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.RecTimeout;
            _ServerSocket.SendTimeout = SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.SendTimeout;
            if (!_ServerSocket.Start()) return false;

            Program.Log.Write("-----------------------------------");
            Program.Log.Write("Listen At = " + SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.ListenIP + ":" + SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.ListenPort);
            Program.Log.Write("-----------------------------------");

            return true;
        }

        public void Stop()
        {
            _ServerSocket.Stop();

            Program.Log.Write("===================================");
            Program.Log.Write("Adapter Stop");
        }

        public bool IsRunning
        {
            get
            {
                return _ServerSocket.IsListening;
            }
        }

        private void InitializeServerSocket()
        {
            string ListenIP = "";
            if (SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.ListenIP.Trim() == "")
                ListenIP = "127.0.0.1";
            else
                ListenIP = SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.ListenIP;

            int ListenPort = SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.ListenPort;
            
            _ServerSocket = new ServerSocket( ListenIP, ListenPort, Program.Log );
            _ServerSocket.CodePageName = SocketInboundAdapterConfigMgt.SocketInAdapterConfig.ListenServerSocketParams.CodePageName;

            _ServerSocket.OnClientDataReceived += new ClientDataReceivedStrEvent(ClientDataReceivedByte);
                        
        }

        //private System.Timers.Timer _timer;
        private ServerSocket _ServerSocket;
        private string m_sRec ;
        private string m_sSend;

        private CommandSendData _CommandSendData = new CommandSendData();
        private CommandRespSendData _CommandRespSendData = new CommandRespSendData();
        private CommandGetResult _CommandGetResult = new CommandGetResult();
        private CommandRespGetResult _CommandRespGetResult = new CommandRespGetResult();

        private string ClientDataReceivedByte( string bufRec )
        {
            try
            {                
                m_sRec = bufRec;            // Receive data
                if (TreatData())
                    return m_sSend;      // Send Result
                else
                    return null;
                
            }
            catch (Exception ex)
            {
                Program.Log.Write(ex);
                return null;
            }
        }

        #endregion

        #region Data Control

        public event DataReceiveEventHandler OnDataReceived=null;

        private bool TreatData()
        {
            PacketHead ph;
            Program.Log.Write(LogType.Debug, "--------------Parse data started...--------------\r\n", true);
            // Parse Data Type & Build m_sSend for return 
            if (_CommandGetResult.DecodePackage(m_sRec))
            {
                // Directly return OK
                _CommandRespGetResult.CommandGUID = _CommandGetResult.CommandGUID;
                _CommandRespGetResult.ComeFrom = _CommandGetResult.ComeFrom;

                ph = new PacketHead();
                ph.PacketType = CommandRespGetResult.PacketType;
                ph.DestinationIP = _CommandGetResult.PacketHead.SourceIP;
                ph.DestinationPort = _CommandGetResult.PacketHead.DestinationPort;
                ph.SourceIP = _CommandGetResult.PacketHead.DestinationIP;
                ph.SourcePort = _CommandGetResult.PacketHead.DestinationPort;
                _CommandRespGetResult.PacketHead = ph;

                _CommandRespGetResult.Result = "1";
                m_sSend = _CommandRespGetResult.EncodePackage();
                return true;
            }

            if (!_CommandSendData.DecodePackage(m_sRec))
                return false;

            // if Commandtype = SendDataCommand,  Return OK
            _CommandRespSendData.CommandGUID = _CommandSendData.CommandGUID;
            _CommandRespSendData.SendResult = "1";

            ph = new PacketHead();
            ph.PacketType = CommandRespSendData.PacketType;
            ph.DestinationIP = _CommandSendData.PacketHead.SourceIP;
            ph.DestinationPort = _CommandSendData.PacketHead.SourcePort;
            ph.SourceIP = _CommandSendData.PacketHead.DestinationIP;
            ph.SourcePort = _CommandSendData.PacketHead.DestinationPort;
            _CommandRespSendData.PacketHead = ph;

            m_sSend = _CommandRespSendData.EncodePackage();

            // Parse data into dataset and transport into framework
            DataSet ds1 = ReadData( _CommandSendData );
            SocketInChannel ch = FindChannel(ds1);

            if (ch == null)
            {
                Program.Log.Write(LogType.Error, "Cannot find corresponding channel for this type of data.\r\n");
                return false;
            }

            DataSet ds2 = TranslateData(ch, ds1);

            if (ds2 == null)
            {
                Program.Log.Write(LogType.Error, "TranslateData failed.\r\n" );
                return false;
            }

            Program.Log.Write("Receive record count: " + ds2.Tables[0].Rows.Count.ToString());
            //Program.Log.Write(LogType.Debug, ds2.to;// DEBUG
            if (Program.Log.LogTypeLevel == LogType.Debug)
                ds2.WriteXml(DateTime.Now.ToString("hh-mm-ss")+".xml");

            //return true;

            if (OnDataReceived != null)
            {
                Program.Log.Write("before OnDataReceived(ch.Rule, ds2);");
                if( OnDataReceived(ch.Rule, ds2) )
                    _CommandRespSendData.SendResult = ((int)CommandBase.CommandResultEnum.SUCESS).ToString();
                else
                    _CommandRespSendData.SendResult = ((int)CommandBase.CommandResultEnum.E_COMMAND).ToString();
                Program.Log.Write("after OnDataReceived(ch.Rule, ds2);");
            }
            else
            {
                Program.Log.Write("OnDataReceived = null");
            }
            Program.Log.Write(LogType.Debug, "--------------Parse data finished--------------\r\n", true);
            return true;
        }


        private DataSet ReadData( CommandSendData csd )
        {
            // Build DataSet Scheme

            DataSet ds1 = new DataSet();
            DataTable table = new DataTable( DateTime.Now.ToString("hh-mm-ss"));

            ds1.Tables.Add(table);

            table.Columns.Add( new DataColumn("CommandGUID", typeof(System.String) ));
            table.Columns.Add(new DataColumn("Commandtype", typeof(System.String)));    

            foreach (string paramname in csd.Params.Keys)
            {
                table.Columns.Add(new DataColumn(paramname, typeof(System.String)));                                
            }

            // Insert Data to DataSet
            DataRow dr = table.NewRow();
            dr["CommandGUID"] = csd.CommandGUID;
            dr["Commandtype"] = Convert.ToInt32(csd.CommandType).ToString();

            foreach (string paraname in csd.Params.Keys)
                dr[paraname] = csd.Params[paraname];

            table.Rows.Add(dr);
            return ds1;
        }

        private SocketInChannel FindChannel(DataSet ds)
        {
            SocketInChannel chResult = null;
            DataRow dr = ds.Tables[0].Rows[0];

            foreach (SocketInChannel ch in SocketInboundAdapterConfigMgt.SocketInAdapterConfig.InboundChanels)
            {
                if (ch.Rule.QueryCriteria.Type == QueryCriteriaRuleType.SQLStatement ||
                    ch.Rule.QueryCriteria.Type == QueryCriteriaRuleType.None)
                    continue;

                List<KKMath.LogicItem> ilist = new List<KKMath.LogicItem>();
                
                int i = 0;
                for( i=0;i<ch.Rule.QueryCriteria.MappingList.Count;i++)
                {
                    SocketInQueryCriteriaItem ci = ch.Rule.QueryCriteria.MappingList[i];
                    //if ( !  KKMath.OperationIsTrue(dr[ci.ThirdPartyDBPatamter.FieldName].ToString().ToUpper() , ci.Operator , ci.Translating.ConstValue.ToUpper()))
                    //    break;

                    QueryCriteriaType type = ci.Type;
                    bool value = KKMath.OperationIsTrue(dr[ci.ThirdPartyDBPatamter.FieldName].ToString().ToUpper(), ci.Operator, ci.Translating.ConstValue.ToUpper());
                    KKMath.LogicItem item = new KKMath.LogicItem(value, type);
                    ilist.Add(item);
                }

                bool ret = KKMath.JoinLogicItem(ilist);
                if (ret)
                {
                    chResult = ch;
                    break;
                }

                //if (i == ch.Rule.QueryCriteria.MappingList.Count)
                //{
                //    chResult = ch;
                //    break;
                //}
                
            }
            return chResult;
        }

        private DataSet TranslateData(SocketInChannel ch, DataSet ds1)
        {

            // Build schema
            DataSet ds2 = new DataSet();
            DataTable table = new DataTable( ds1.Tables[0].TableName);
            ds2.Tables.Add(table);

            foreach (DataColumn col1 in ds1.Tables[0].Columns)
            {
                DataColumn col2 = new DataColumn(col1.ColumnName, typeof(System.String));
                table.Columns.Add(col2);
            }

            // Copy Data
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                DataRow dr2 = table.NewRow();

                foreach (DataColumn item in ds1.Tables[0].Columns)
                {
                    if (item.DataType == typeof(System.DateTime))
                    {
                        dr2[item.ColumnName] = Convert.ToDateTime(dr1[item.ColumnName]).ToString("yyyy-MM-dd hh:mm:ss");
                    }
                    else
                      dr2[item.ColumnName] = Convert.ToString(dr1[item.ColumnName]);
                }
                table.Rows.Add(dr2);
            }

            return ds2;
        }

       #endregion
    }
}
