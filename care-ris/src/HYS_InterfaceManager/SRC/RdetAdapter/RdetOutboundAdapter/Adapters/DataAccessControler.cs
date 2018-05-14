using System;
using System.Text;
using System.Timers;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Xml;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Rule;
using HYS.RdetAdapter.Common;
using HYS.RdetAdapter.Configuration;
using HYS.Common.Objects.Logging;
using StarCommon;

namespace HYS.RdetAdapter.RdetOutboundAdapter
{
    public class DataAccessControler
    {
        public DataAccessControler()
        {
            InitializeTimer();
            InitializeClientRdet();
        }

        #region Timer Control

        public void Start()
        {
            Program.Log.Write("Adapter Start");
            Program.Log.Write("-----------------------------------");
            Program.Log.Write("Connect To: " + RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.ClientRdetParams.ServerIP + ":" + RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.ClientRdetParams.ServerPort);
            Program.Log.Write("Interval: " + RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.OutGeneralParams.TimerInterval + "ms");
            Program.Log.Write("-----------------------------------");
                        
            _timer.Interval = RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.OutGeneralParams.TimerInterval;
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

        private void InitializeClientRdet()
        {
            _ClientSocket = new ClientSocket(Program.Log);
            
            ClientRdetParams csp = RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.ClientRdetParams;
            _ClientSocket.ServerIP = csp.ServerIP;
            _ClientSocket.ServerPort = csp.ServerPort;
            _ClientSocket.SendTimeout = csp.SendTimeout;
            _ClientSocket.ReceiveTimeout = csp.RecTimeout;
            
        }


        private System.Timers.Timer _timer;
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

            foreach (RdetOutChannel ch in RdetOutboundAdapterConfigMgt.RdetOutAdapterConfig.OutboundChanels)
            {
                try
                {
                    if (!ch.Enable) continue;
                    if (OnDataRequest != null)
                        dsResult = OnDataRequest((IOutboundRule)ch.Rule, null);

                    if (Program.bStandalone) //debug
                    {
                        dsResult = new DataSet();
                        dsResult.ReadXml(Application.StartupPath + "\\temp\\" + "09-59-02.xml");
                    }

                    if (dsResult == null)
                    {
                        Program.Log.Write("Receive record dataset is null\n");
                        continue;
                    }
                    else
                    {
                        Program.Log.Write(LogType.Debug, "\nChannel " + ch.ChannelName + " Receive record count: " + dsResult.Tables[0].Rows.Count.ToString());

                        string sTempDir = Application.StartupPath + "\\temp";
                        if (!System.IO.Directory.Exists(sTempDir))
                            System.IO.Directory.CreateDirectory(sTempDir);

                        if (Program.Log.LogTypeLevel == LogType.Debug)
                            dsResult.WriteXml(Application.StartupPath + "\\temp\\" + DateTime.Now.ToString("hh-mm-ss") + ".xml");
                    }

                    SendCommand2Server(ch, dsResult);

                }
                catch (Exception ex)
                {
                    Program.Log.Write(ex);
                }
            }
            
        }

        private void SendCommand2Server(RdetOutChannel ch, DataSet ds)
        {
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                // build command
                if (!ds.Tables[0].Columns.Contains("Command"))
                {
                    Program.Log.Write(LogType.Error, "Configuration File error!Cannot find column 'Command'!\r\n" +
                                                    "channel name:" + ch.ChannelName + "\r\n");
                                                    
                    return;
                    //throw new Exception("");
                }
                
                try
                {
                    CmdReqBase request = null;
                    
                    if (CommandToken.IsNewPatient(dr["Command"].ToString()))
                    {
                        request = BuildCmdReqNewPatient(ch, dr);
                    }
                    else if (CommandToken.IsUpdatePatient(dr["Command"].ToString()))
                    {
                        request = BuildCmdReqUpdatePatient(ch, dr);
                    }
                    else if (CommandToken.IsNewImage(Convert.ToString(dr["Command"])))
                    {
                        request = BuildCmdReqNewImage(ch, dr);
                    }

                    if (request == null)
                    {
                        Program.Log.Write("Invalid DataRow! Command=" + dr["Command"].ToString() + " \r\n");
                        continue;
                    }



                    // Send Command 
                    CmdRespBase resp;
                    if (Program.bStandalone)
                    {
                        resp = new CmdRespBase();
                        resp.AddParameter(CommandToken.StudyInstanceUID, "StudyInstenceUID001");
                        resp.ErrorCode = "0";
                    }
                    else
                      resp = _ClientSocket.SendCommand(request);

                    _ClientSocket.DisConnect(false);
                    if (resp == null)
                    {
                        Program.Log.Write("There is no correct response or no response data to request ! \r\n");
                        continue;
                    }

                    if (Convert.ToInt32(resp.ErrorCode) == 0)
                    {
                        if (OnDataDischarge != null)
                            this.OnDataDischarge(new string[] { Convert.ToString(dr["Data_ID"]) });

                        //Write StudyinstanceUID
                        if (CommandToken.IsNewPatient(request.Command))
                        {
                            UpdateStudyInstanceUID(Convert.ToString(dr["Data_ID"]), resp.GetParamValue(CommandToken.StudyInstanceUID), _fStudyInstanceUID);
                        }                        
                    }
                    else
                    {
                        RdetError Err = RdetErrorMgt.GetRdetError(Convert.ToInt32(resp.ErrorCode.Trim()));
                        Program.Log.Write(LogType.Error, "------------Error Exist! ----------------------------------------\r\n"
                                                        + "ErrorCode=" + Err.Code.ToString() + "\r\n"
                                                        + "Error Description: " + Err.ErrorDescription + "\r\n"
                                                        + "Error Resolution : " + Err.ErrorDescription + "\r\n"
                                                        + "----------------------------------------------------------------\r\n", true);
                    }
                    
                }
                catch (Exception ex)
                {
                    Program.Log.Write(LogType.Error, "Unknow Error:" + ex.Message + "\r\n"+
                                                     "channel Name:"+ch.ChannelName+"\r\n");
                }
            }
             
        }

        private GWDataDBField _fStudyInstanceUID = null;

        private CmdReqBase BuildCmdReqNewPatient(RdetOutChannel ch, DataRow dr)
        {
            CmdReqNewPatient request = new CmdReqNewPatient();
            foreach (RdetOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if(item.ThirdPartyDBPatamter.FieldName.Trim()=="") 
                    continue;
                if (item.ThirdPartyDBPatamter.FieldName.Trim().ToLower() == CommandToken.CommandHeadToken.ToLower()) //'Command'
                    continue;

                if (item.ThirdPartyDBPatamter.FieldName.Trim().ToUpper() == ("StudyInstanceUID").ToUpper())
                    _fStudyInstanceUID = item.GWDataDBField;

                string FName = item.TargetField;
                string FValue= StarConvert.DBValueToString( dr[FName],"");
                if(FValue.Trim()=="") 
                    continue;

                // 2007-04-02: Out DateTime format is implemented by gateway framework, here need not translate

                //if (FName.Trim().ToLower() == ("BirthDate").ToLower())
                //{
                //    FValue = Convert.ToDateTime(FValue).ToString("yyyyMMdd");
                //}

                //if (FName.Trim().ToLower() == ("StudyDate").ToLower())
                //{
                //    FValue = Convert.ToDateTime(FValue).ToString("yyyyMMdd");
                //}

                //if (FName.Trim().ToLower() == ("StudyTime").ToLower())
                //{
                //    FValue = Convert.ToDateTime(FValue).ToString("HHmmss");
                //}

              
                request.AddParameter(FName, FValue);
            }
            return request;
        }

              
        private CmdReqBase BuildCmdReqUpdatePatient(RdetOutChannel ch, DataRow dr)
        {
            CmdReqUpdatePatient request = new CmdReqUpdatePatient();
            foreach (RdetOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName.Trim() == "")
                    continue;
                if (item.ThirdPartyDBPatamter.FieldName.Trim().ToLower() == CommandToken.CommandHeadToken.ToLower()) //'Command'
                    continue;
                //string FName = item.ThirdPartyDBPatamter.FieldName.Trim();
                string FName = item.TargetField;
                string FValue = StarConvert.DBValueToString(dr[FName], "");
                if (FValue.Trim() == "")
                    continue;

                // 2007-04-02: Out DateTime format is implemented by gateway framework, here need not translate
                //if (FName.Trim().ToLower() == ("BirthDate").ToLower())
                //{
                //    FValue = Convert.ToDateTime(FValue).ToString("yyyyMMdd");
                //}

                //if (FName.Trim().ToLower() == ("StudyDate").ToLower())
                //{
                //    FValue = Convert.ToDateTime(FValue).ToString("yyyyMMdd");
                //}

                //if (FName.Trim().ToLower() == ("StudyTime").ToLower())
                //{
                //    FValue = Convert.ToDateTime(FValue).ToString("HHmmss");
                //}

                request.AddParameter(FName, FValue);
            }
            return request;
        }


        private CmdReqBase BuildCmdReqNewImage(RdetOutChannel ch, DataRow dr)
        {
            CmdReqNewImage request = new CmdReqNewImage();
            //CmdReqNewPatient request = new CmdReqNewPatient();
            foreach (RdetOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName.Trim() == "")
                    continue;
                if (item.ThirdPartyDBPatamter.FieldName.Trim().ToLower() == CommandToken.CommandHeadToken.ToLower()) //'Command'
                    continue;
                //string FName = item.ThirdPartyDBPatamter.FieldName.Trim();
                string FName = item.TargetField;
                string FValue = StarConvert.DBValueToString(dr[FName], "");
                if (FValue.Trim() == "")
                    continue;
                request.AddParameter(FName, FValue);
            }
            return request;
        }

        #region Write StudyInstanceUID to Outbound DB

        /*
        private static void UpdateGUID(DataTable dt, DataColumn dc, string columnName, GWDataDBField field)
        {
            if (dc.ColumnName == columnName)
            {
                Dictionary<string, string> rpIDs = new Dictionary<string, string>();
                foreach (DataRow dr in dt.Rows)
                {
                    object o = dr[dc];
                    string rpID = (o == null) ? "" : o.ToString();
                    if (rpID.Length < 1)
                    {
                        string guid = DicomMappingHelper.GetGUID(dr);
                        rpID = DHelper.GetDicomGUID();
                        rpIDs.Add(guid, rpID);
                        dr[dc] = rpID;
                    }
                }
                if (rpIDs.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (KeyValuePair<string, string> pair in rpIDs)
                    {
                        string tableName = GWDataDB.GetTableName(Program.InterfaceName, GWDataDBTable.Order);
                        string pkName = GWDataDBField.o_DATA_ID.FieldName;
                        string fieldName = field.FieldName;
                        sb.Append("UPDATE ").Append(tableName)
                            .Append(" SET ").Append(fieldName).Append(" = '").Append(pair.Value).Append("'")
                            .Append(" WHERE ").Append(pkName).Append(" = '").Append(pair.Key).Append("';\r\n");
                    }
                    if (Program.Database.DoQuery(sb.ToString()) == null)
                    {
                        Program.Log.Write(LogType.Error, "Update " + columnName + " into database failed.");
                    }
                    else
                    {
                        Program.Log.Write("Update " + columnName + " into database succeeded.");
                    }
                }
            }
        }*/

        private bool UpdateStudyInstanceUID(string sData_ID, string sStudyInstanceUID, GWDataDBField field)
        {
            try
            {
                if (field == null)
                {
                    Program.Log.Write(LogType.Error, "No StudyInstanceUID map!");
                    return false;
                }

                string sTableName =  GWDataDB.GetTableName(Program.InterfaceName, field.Table);
                string sSql = " update " + sTableName + " set " + field.FieldName + " ='" + sStudyInstanceUID + "'" +
                              " where data_id='" + sData_ID + "'";

                if (Program.Database.DoQuery(sSql) == null)
                {
                    Program.Log.Write(LogType.Error, "Update StudyINstanceUID into" + field.ToString() + "  failed.");
                }
                else
                {
                    Program.Log.Write(LogType.Debug, "Update StudyINstanceUID into" + field.ToString() + "  succeeded.");
                }
                return true;
            }
            catch (Exception ex)
            {
                Program.Log.Write(LogType.Error, "Write StudyInstanceUID to GWDataDB Error:"+ex.Message);
                return false;
            }


        }
        #endregion


        #endregion
    }

       
}
