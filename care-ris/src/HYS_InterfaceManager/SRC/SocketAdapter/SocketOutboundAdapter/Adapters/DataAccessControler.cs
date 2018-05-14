using System;
using System.Text;
using System.Timers;
using System.Data;
using System.Collections;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Xml;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.SocketAdapter.Command;
using HYS.SocketAdapter.Common;
using HYS.SocketAdapter.Configuration;

namespace HYS.SocketAdapter.SocketOutboundAdapter
{
    public class DataAccessControler
    {
        public DataAccessControler()
        {
            InitializeTimer();
            InitializeClientSocket();
        }

        #region Timer Control

        public void Start()
        {
            Program.Log.Write("Adapter Start");
            Program.Log.Write("-----------------------------------");
            Program.Log.Write("Connect To: " + SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams.ServerIP + ":" + SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams.ServerPort);
            Program.Log.Write("Interval: " + SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.OutGeneralParams.TimerInterval + "ms");
            Program.Log.Write("-----------------------------------");
                        
            _timer.Interval = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.OutGeneralParams.TimerInterval;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();

            Program.Log.Write("===================================");
            Program.Log.Write("Adapter Stop");
        }

        public bool IsRunning
        {
            get
            {
                return _timer.Enabled;
            }
        }

        private void InitializeTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
        }

        private void InitializeClientSocket()
        {
            ClientSocketParams csp = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams;

            _ClientSocket = new ClientSocket(Program.Log);
            _ClientSocket.CodePageName = csp.CodePageName;            
            
            _ClientSocket.ServerIP = csp.ServerIP;
            _ClientSocket.ServerPort = csp.ServerPort;
            _ClientSocket.SendTimeout = csp.SendTimeout;
            _ClientSocket.ReceiveTimeout = csp.RecTimeout;
            
        }

        private Timer _timer;
        private ClientSocket _ClientSocket;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

                _timer.Enabled = false;
                Program.Log.Write("\r\nQuery data started.");
                QueryData();
                Program.Log.Write("Query data finished.\r\n");                
            }
            catch (Exception Ex)
            {
                Program.Log.Write(Ex);
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

        #endregion

        #region Data Control

        public event DataRequestEventHanlder OnDataRequest=null;
        public event DataDischargeEventHanlder OnDataDischarge=null;

        /// <summary>
        /// 
        /// 1.Load Channel list,Mapping list
        /// 2.Request dataset by sigle channel
        /// 3.set mapping to 3rd procedure or insert record to 3rd table
        /// 
        /// </summary>
        private void QueryData()
        {
            DataSet dsCriteria = new DataSet();
            DataSet dsResult=null;

            
            foreach (SocketOutChannel ch in SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.OutboundChanels)
            {
                try
                {
                    if (!ch.Enable) continue;
                    if (OnDataRequest != null)
                        dsResult = OnDataRequest((IOutboundRule)ch.Rule, null);

                    //dsResult = new DataSet();
                    //dsResult.ReadXml("06-10-43.xml");

                    if (dsResult == null)
                    {
                        Program.Log.Write("Receive record dataset is null\r\n");
                        continue;
                    }
                    else
                    {
                        Program.Log.Write(LogType.Debug, "\r\nChannel " + ch.ChannelName + " Receive record count: " + dsResult.Tables[0].Rows.Count.ToString());
                        if (Program.Log.LogTypeLevel == LogType.Debug)
                            dsResult.WriteXml(DateTime.Now.ToString("hh-mm-ss") + ".xml");


                        SendCommand2Server(ch, dsResult);
                    }
                }
                catch (Exception err)
                {
                    Program.Log.Write(err);
                }
            }       
        }

        private void SendCommand2Server(SocketOutChannel ch, DataSet ds)
        {
            CommandSendData csd = new CommandSendData();

            PacketHead ph = new PacketHead();
            ph.PacketType = CommandSendData.PacketType;
            ph.DestinationIP = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams.ServerIP;
            ph.DestinationPort = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams.ServerPort;
            ph.SourceIP = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams.CallbackIP;
            ph.SourcePort = SocketOutboundAdapterConfigMgt.SocketOutAdapterConfig.ClientSocketParams.CallbackPort;

            csd.PacketHead = ph;


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                csd.Params.Clear();
                csd.CommandGUID = "";

                foreach (SocketOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
                {
                    //string TargetField = item.ThirdPartyDBPatamter.FieldName.Trim();
                    string TargetField = item.TargetField.Trim();
                    if (TargetField == "") continue;

                    if (TargetField.ToUpper() == ("CommandGUID").ToUpper())
                    {
                        csd.CommandGUID = dr[TargetField].ToString();
                        continue;
                    }
                    if (TargetField.ToUpper().ToUpper() == ("Commandtype").ToUpper())
                    {
                        csd.CommandType = (CommandBase.CommandTypeEnum)Convert.ToInt32(dr[TargetField].ToString());
                        continue;
                    }

                    if (ds.Tables[0].Columns.IndexOf( TargetField ) < 0)
                    {
                        Program.Log.Write(LogType.Error, TargetField + " is not exist in dataset!\r\n");
                    }
                    else
                    {
                        csd.Params.Add(TargetField, dr[TargetField].ToString());
                    }
                }

                if (csd.CommandGUID == null || csd.CommandGUID.Trim() == "")
                    csd.CommandGUID = Guid.NewGuid().ToString();

                //byte[] result = _ClientSocket.SendMsg(csd.EncodePackage());
                string result = this.SendMsgToServer(csd);

                if ( result != null )
                {
                    
                    CommandRespSendData crsd = new CommandRespSendData();
                    crsd.DecodePackagea(result);
                    if (OnDataDischarge != null)
                        OnDataDischarge(new string[] { dr["Data_ID"].ToString() });
                    Program.Log.Write(LogType.Error, "SendMsg Success! SendResult= " + crsd.SendResult + "\r\n");  


                    //if (crsd.SendResult == "1")
                    //{
                    //    if (OnDataDischarge != null)
                    //        OnDataDischarge(new string[] { dr["Data_ID"].ToString() });
                    //    Program.Log.Write(LogType.Debug, "SendMsg Success! SendResult=1\r\n");                        
                    //}
                    //else //2007-3-9 Add: Send success, Set Processflag =1, and write a log 
                    //{
                    //    if (OnDataDischarge != null)
                    //        OnDataDischarge(new string[] { dr["Data_ID"].ToString() });
                    //    Program.Log.Write(LogType.Error, "SendMsg Success! SendResult= " + crsd.SendResult + "\r\n");                        
                    //}
                }
                else
                {
                    Program.Log.Write(LogType.Error, "SendMsg Failure!\r\n");
                }
            }            
        }

        private string SendMsgToServer(CommandSendData csd)
        {
            try
            {
                _ClientSocket.Connect();
                
                PacketHead ph = csd.PacketHead;

                //ph.SourceIP = _ClientSocket.LocalIP;
                //ph.SourcePort = _ClientSocket.LocalPort;                
                            
                csd.PacketHead = ph;

                string result = _ClientSocket.SendMsg(csd.EncodePackage());
                _ClientSocket.DisConnect(false);
                return result;
            }
            catch(Exception ex)
            {
                Program.Log.Write(ex);
                return null;
            }
        }

        #endregion
    }

       
}
