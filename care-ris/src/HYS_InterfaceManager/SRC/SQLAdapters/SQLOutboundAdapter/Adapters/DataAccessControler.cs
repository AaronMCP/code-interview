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
using SQLOutboundAdapter.Objects;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;

using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Objects.Config;
using System.IO;


namespace SQLOutboundAdapter.Adapters
{
    public partial class DataAccessControler
    {
        public DataAccessControler()
        {
            InitializeTimer();
        }

        #region Timer Control

        public void Start()
        {
            if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Passtive)
            {
                Program.Log.Write("Adapter is running in passive mode.");
                return;
            }

            Program.Log.Write("Adapter Start");
            Program.Log.Write("-----------------------------------");
            Program.Log.Write("DB Connection String: " + Program.db.ConnectionString);
            //Program.Log.Write("SQL Statement: " + SQLOutAdapterConfigMgt.Configment);
            Program.Log.Write("Interval: " + SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerInterval.ToString() + "ms");
            Program.Log.Write("-----------------------------------");

            _timer.Interval = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.TimerInterval;
            _timer.Start();
        }

        public void Stop()
        {
            if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Passtive)
            {
                Program.Log.Write("Adapter is stopping in passive mode.");
                return;
            }

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

        private System.Timers.Timer _timer;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

                _timer.Enabled = false;
                Program.Log.Write("\r\nQuery data started.");
                if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileConnection)
                {
                    QueryDataToFile();
                }
                else
                {
                    QueryData();
                }
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

        public event DataRequestEventHanlder OnDataRequest;
        public event DataDischargeEventHanlder OnDataDischarge;

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

            foreach (SQLOutboundChanel ch in SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels)
            {
                if (!ch.Enable) continue;

                if (ch.OperationType == ThrPartyDBOperationType.SQLStatement && ch.Rule.QueryResult.MappingList.Count < 1)
                {
                    CallSQLStatement(null, ch);
                }
                else
                {
                    if (OnDataRequest != null)
                        dsResult = OnDataRequest((IOutboundRule)ch.Rule, null);  //DEBUG



                    //test code
                    //dsResult = TestCase.OnDataRequest(null, null);   //DEBUG

                    //dsResult = new DataSet();
                    //dsResult.ReadXml("DataSet_SP_out_Hello_QR_632989060663827076.xml");

                    if (dsResult != null)
                    {
                        foreach (DataTable dt in dsResult.Tables)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    if (dr[i] == DBNull.Value)
                                    {
                                        dr[i] = "";
                                        Program.Log.Write(LogType.Warning, "Find NULL column: " + dt.Columns[i].ColumnName);
                                    }
                                    //else
                                    //{
                                    //    if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ReplaceUnixLineEndingWithWindowsLineEnding)
                                    //    {
                                    //        string str = Convert.ToString(dr[i]);
                                    //        Program.Log.Write(LogType.Info, str);
                                    //        if (str != null)
                                    //        {
                                    //            str = System.Text.RegularExpressions.Regex.Replace(str, "[\r]", "");
                                    //            str = System.Text.RegularExpressions.Regex.Replace(str, "[\n]", "\r\n");//"\x0D\x0A");
                                    //            dr[i] = str;
                                    //        }
                                    //    }
                                    //}
                                }
                            }
                        }

                        if (dsResult.Tables.Count > 0)
                            if (dsResult.Tables[0].Rows.Count > 0)
                            {
                                Program.Log.Write("Receive record count: " + dsResult.Tables[0].Rows.Count.ToString());
                                if (ch.OperationType == ThrPartyDBOperationType.StorageProcedure)
                                {
                                    if (SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.OracleDriver)
                                    {
                                        WriteSPOracle(dsResult, ch);
                                    }
                                    else
                                    {
                                        WriteSP(dsResult, ch);
                                    }
                                }
                                else if (ch.OperationType == ThrPartyDBOperationType.Table)
                                {
                                    WriteTable(dsResult, ch);
                                }
                                else if (ch.OperationType == ThrPartyDBOperationType.SQLStatement)
                                {
                                    CallSQLStatement(dsResult, ch);
                                }
                            }
                    }
                   
                }
                //if (dsResult == null || dsResult.Tables.Count<1 || dsResult)
                //{
                //    Program.Log.Write("Receive record dataset is null");
                //    continue;
                //}
                //else 
                //    Program.Log.Write("Receive record count: " + dsResult.Tables[0].Rows.Count.ToString());

                //if (ch.OperationType == ThrPartyDBOperationType.StorageProcedure)
                //{                    
                //    WriteSP(dsResult, ch);
                //}
                //else
                //{
                //    WriteTable(dsResult, ch);
                //}
            }
            
        }

        private bool CallSQLStatement(DataSet ds, SQLOutboundChanel ch)
        {
            string sqlstatement = string.Empty;

            if (ds == null)
            {
                try
                {
                    OleDbCommand cmd = Program.db.GetCommand();

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = sqlstatement = MapingHelper.BuildSQLStatement(ch, null);

                    if (!Program.db.ExecCommand(cmd))
                    {
                        Program.Log.Write(LogType.Error, "Execute sql statement " + sqlstatement + " failed.");
                    }
                }
                catch (Exception Ex)
                {
                    Program.Log.Write(LogType.Error, "Execute sql statement " + sqlstatement + " error.");

                    Program.Log.Write(Ex);
                    Program.Log.Write(LogType.Info, Program.db.ConnectionString);
                    if (!ch.IgnoreDBException) return false;
                }
            }
            else
            {
                string[] data_Id = new string[1];
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        OleDbCommand cmd = Program.db.GetCommand();

                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = sqlstatement = MapingHelper.BuildSQLStatement(ch, dr);


                        if (Program.db.ExecCommand(cmd))
                        {
                            data_Id[0] = Convert.ToString(dr["data_id"]);
                            OnDataDischarge(data_Id);
                            //TestCase.OnDataDischarge(data_Id); //DEBUG
                            Program.Log.Write(LogType.Debug, "Execute sql statement " + sqlstatement + " successfully.");
                        }
                        else
                        {
                            Program.Log.Write(LogType.Error, "Execute sql statement " + sqlstatement + " failed.");
                        }
                    }
                    catch (Exception Ex)
                    {
                        Program.Log.Write(LogType.Error, "Execute sql statement " + sqlstatement + " error.");

                        Program.Log.Write(Ex);
                        Program.Log.Write(LogType.Info, Program.db.ConnectionString);
                        if (!ch.IgnoreDBException) return false;
                    }
                }
            }
            return true;
        }
       
        /// <summary>
        /// Write logical:
        /// 1.check PK field whether is existed or not
        /// 2.insert new record where PK is not exist
        /// 3.update record when PK is exist
        /// 4.delete record TODO:???
        /// 
        /// Insert logical:
        /// 1.select empty DataSet from 3rd database
        /// 2.insert new record
        /// 3.SqlAdapter.update
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        private bool WriteTable(DataSet ds, SQLOutboundChanel ch)
        {
            //OleDbCommand cmd = Program.db.GetCommand();

                // ------ 20070419 ------            
                string[] data_Id = new string[1];
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        OleDbCommand cmd = Program.db.GetCommand();

                        if (!RecordIsExist(dr, ch))
                            MapingHelper.BuildInsertCmd(ch, dr, cmd);
                        else
                            MapingHelper.BuildUpdateCmd(ch, dr, cmd);

                        cmd.CommandType = CommandType.Text;

                        if (Program.db.ExecCommand(cmd))
                        {
                            data_Id[0] = Convert.ToString(dr["data_id"]);
                            OnDataDischarge(data_Id);
                            //TestCase.OnDataDischarge(data_Id); //DEBUG
                        }
                    }
                    catch (Exception Ex)
                    {
                        Program.Log.Write(Ex);
                        Program.Log.Write(LogType.Info, Program.db.ConnectionString);
                        if (!ch.IgnoreDBException) return false;
                    }
                }
                return true;
                // ---------------------- 
            

            //try
            //{
            //    string[] data_Id =new string[1];              
            //     foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        if (!RecordIsExist(dr, ch))  
            //            MapingHelper.BuildInsertCmd(ch, dr, cmd);
            //        else
            //            MapingHelper.BuildUpdateCmd(ch, dr, cmd);

            //        cmd.CommandType = CommandType.Text;

            //        if (Program.db.ExecCommand(cmd))
            //        {
            //            data_Id[0] = Convert.ToString(dr["data_id"]);
            //            OnDataDischarge(data_Id); 
            //            //TestCase.OnDataDischarge(data_Id); //DEBUG
            //        }
            //    }
            //    return true;
            //}
            //catch(Exception Ex)
            //{
            //    Program.Log.Write(Ex);
            //    return false;
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        private bool WriteSP(DataSet ds, SQLOutboundChanel ch)
        {

            // ------ 20070419 ------  
                string[] data_Id = new string[1];    
                OleDbCommand cmd = Program.db.GetCommand();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = ch.OperationName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SetPamater(dr, cmd, ch);
                        if (Program.db.ExecCommand(cmd))
                        {
                            data_Id[0] = Convert.ToString(dr["data_id"]);
                            if (OnDataDischarge != null) OnDataDischarge(data_Id);
                            //TestCase.OnDataDischarge(data_Id); //DEBUG
                        }
                    }
                    catch (Exception Ex)
                    {
                        Program.Log.Write(Ex);
                        Program.Log.Write(LogType.Info, Program.db.ConnectionString);
                        if (!ch.IgnoreDBException) return false;
                    }
                }
                return true;
                // ---------------------- 

            //try
            //{
            //    string[] data_Id = new string[1];
            //    OleDbCommand cmd = Program.db.GetCommand();
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        cmd.Parameters.Clear();
            //        cmd.CommandText = ch.OperationName;
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        SetPamater(dr, cmd, ch);
            //        if (Program.db.ExecCommand(cmd))
            //        {
            //            data_Id[0] = Convert.ToString(dr["data_id"]);
            //            if (OnDataDischarge != null) OnDataDischarge(data_Id);
            //            //TestCase.OnDataDischarge(data_Id); //DEBUG
            //        }
            //    }
            //    return true;
            //}
            //catch (Exception Ex)
            //{
            //    Program.Log.Write(Ex);
            //    return false;
            //}
        }

        private void SetPamater(DataRow dr, OleDbCommand cmd, SQLOutboundChanel ch)
        {
            Program.Log.Write("---- sp parameter ----");

            foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName == null || item.ThirdPartyDBPatamter.FieldName.Length < 1) continue;

                //Program.Log.Write(item.TargetField);
                
                OleDbParameter parm = Program.db.GetParameter();
                
                parm.ParameterName = "@" + item.TargetField;
                parm.OleDbType = item.ThirdPartyDBPatamter.FieldType;
                parm.Direction = ParameterDirection.Input;
                
                //object obj =dr[item.TargetField.Trim()];
                //if(Convert.IsDBNull(obj) || Convert.ToString(obj).Trim()=="")
                //    parm.Value = System.DBNull.Value;
                //else
                //    parm.Value = obj;

                object obj = dr[item.TargetField.Trim()];

                if (Convert.IsDBNull(obj))
                {
                    if (item.Translating.Type == TranslatingType.DefaultValue)
                        parm.Value = item.Translating.ConstValue;
                    else
                        parm.Value = System.DBNull.Value;
                }
                else if (Convert.ToString(obj).Trim() == "")
                {
                    if (item.ThirdPartyDBPatamter.FieldType == OleDbType.Date)
                        parm.Value = System.DBNull.Value;
                    else
                        parm.Value = Convert.ToString(obj);
                }
                else
                {
                    //2007-3-23
                    if (item.ThirdPartyDBPatamter.FieldType == OleDbType.VarChar ||
                        item.ThirdPartyDBPatamter.FieldType == OleDbType.VarWChar ||
                        item.ThirdPartyDBPatamter.FieldType == OleDbType.Char ||
                        item.ThirdPartyDBPatamter.FieldType == OleDbType.WChar)                        
                        parm.Value = MapingHelper.FixSigleQuoteInSQLStringValue(obj.ToString());
                    else
                        parm.Value = obj;
                }

                Program.Log.Write(parm.ParameterName + "=" + parm.Value);

                //if(Convert.IsDBNull( item.TargetField.Trim()) || Convert.ToString(dr[]).Trim()=="")
                //parm.Value = //Convert.ChangeType(dr[item.TargetField], item.ThirdPartyDBPatamter.FieldType.GetType()); 

                cmd.Parameters.Add(parm);
            }

            Program.Log.Write("---------------------");
        }
        
        private bool RecordIsExist(DataRow dr, SQLOutboundChanel ch)
        {
            string strSQL = " select count(*) from " + ch.OperationName + MapingHelper.BuildWhereString(ch);
                            
            OleDbCommand cmd = Program.db.GetCommand();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;

            bool hasRedundancyFlag = false;

            foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName == null || item.ThirdPartyDBPatamter.FieldName.Length < 1) continue;

                if (item.RedundancyFlag)
                {
                    hasRedundancyFlag = true;

                    OleDbParameter parm = Program.db.GetParameter();

                    parm.ParameterName = "?";// + item.TargetField;
                    parm.OleDbType = item.ThirdPartyDBPatamter.FieldType;
                    parm.Direction = ParameterDirection.Input;
                    parm.Value = dr[item.TargetField];//Convert.ChangeType(dr[item.TargetField], item.ThirdPartyDBPatamter.FieldType.GetType());

                    cmd.Parameters.Add(parm);
                }
            }

            if (hasRedundancyFlag == false) return false;
            
            DataSet dsResult = new DataSet();
            Program.db.ExecCommand(cmd, dsResult);
            return Convert.ToInt32(dsResult.Tables[0].Rows[0][0]) > 0;
            
        }

        #endregion

        private void QueryDataToFile()
        {
            if (OnDataRequest == null || OnDataDischarge == null) return;
            ThirdDBConnection cfg = SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter;

            foreach (SQLOutboundChanel ch in SQLOutAdapterConfigMgt.SQLOutAdapterConfig.OutboundChanels)
            {
                try
                {
                    if (!ch.Enable) continue;
                    if (ch.OperationType != ThrPartyDBOperationType.Table) continue;
                    DataSet dsResult = OnDataRequest((IOutboundRule)ch.Rule, null);
                    int count = PrepareQueryResult(dsResult);
                    if (count < 1) continue;

                    // generate file name

                    string fn = RuleControl.GetRandomNumber();
                    string dataFileFolder = ConfigHelper.GetFullPath(cfg.FileFolder);
                    string csvFileName = string.Format("{0}{1}", fn, cfg.FileNameExtension);
                    string tmpFileName = string.Format("{0}{1}", fn, cfg.TempFileNameExtensin);
                    
                    // create CSV file

                    List<string> idList = new List<string>();
                    if (!CreateDataFile(ch, tmpFileName)) continue;
                    if (!InsertDataFile(ch, tmpFileName, dsResult, idList, count)) continue;
                    Program.Log.Write("Creating data file: " + csvFileName);
                    File.Move(Path.Combine(dataFileFolder, tmpFileName), Path.Combine(dataFileFolder, csvFileName));

                    // delete INI file

                    if (!cfg.KeepSchemaFile)
                    {
                        string schameFilePath = Path.Combine(dataFileFolder, cfg.SchemaFileName);
                        Program.Log.Write("Deleting schema file: " + schameFilePath);
                        File.Delete(schameFilePath);
                    }

                    // create IDX file

                    if (cfg.WriteIndexFile)
                    {
                        string indexFileName = string.Format("{0}.idx", fn);
                        string indexFileFolder = ConfigHelper.GetFullPath(cfg.IndexFileFolder);
                        string indexFilePath = Path.Combine(indexFileFolder, indexFileName);
                        Program.Log.Write("Creating index file: " + indexFilePath);
                        if (!Directory.Exists(indexFileFolder)) Directory.CreateDirectory(indexFileFolder);
                        using (StreamWriter sw = File.CreateText(indexFilePath)) { }
                    }

                    OnDataDischarge(idList.ToArray());
                }
                catch (Exception err)
                {
                    Program.Log.Write(err);
                }
            }
        }
        private int PrepareQueryResult(DataSet dsResult)
        {
            int count = 0;
            if (dsResult != null)
            {
                foreach (DataTable dt in dsResult.Tables)
                {
                    count += dt.Rows.Count;
                    foreach (DataRow dr in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (dr[i] == DBNull.Value)
                            {
                                dr[i] = "";
                                Program.Log.Write(LogType.Warning, "Find NULL column: " + dt.Columns[i].ColumnName);
                            }
                        }
                    }
                }
            }
            return count;
        }
        private bool CreateDataFile(SQLOutboundChanel chn, string fname)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE [{0}] (", fname);
            foreach (SQLOutQueryResultItem item in chn.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName == null || item.ThirdPartyDBPatamter.FieldName.Length < 1) continue;
                sb.AppendFormat("[{0}] TEXT,", item.ThirdPartyDBPatamter.FieldName);
            }
            string sql = sb.ToString().TrimEnd(',') + ")";
            
            OleDbCommand cmd = Program.db.GetCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;

            Program.Log.Write("Creating file: " + fname);
            return Program.db.ExecCommand(cmd);
        }
        private bool InsertDataFile(SQLOutboundChanel chn, string fname, DataSet ds, List<string> idList, int count)
        {
            int i = 1;
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    chn.OperationName = fname;
                    OleDbCommand cmd = Program.db.GetCommand();
                    MapingHelper.BuildInsertCmd(chn, dr, cmd);
                    cmd.CommandType = CommandType.Text;

                    Program.Log.Write(string.Format("Insert data into file. {0}/{1}", i++, count));
                    if (!Program.db.ExecCommand(cmd)) return false;

                    string id = Convert.ToString(dr["data_id"]);
                    idList.Add(id);
                }
            }
            return true;
        }
    }

    public class MapingHelper
    {
        static public string FixSigleQuoteInSQLStringValue(string sValue)
        {
            //return sValue.Replace("'", "''");
            return sValue;
        }

        static public bool BuildInsertCmd(SQLOutboundChanel ch, DataRow dr, OleDbCommand cmd)
        {

            if (ch.Rule.QueryResult.MappingList.Count < 1)
            {
                Program.Log.Write(LogType.Warning, "There is no mapinglist");
                return false;
            }

            string sbFields = "";
            string sbValues = "";
            
            foreach(SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName == null || item.ThirdPartyDBPatamter.FieldName.Length < 1) continue;

                //if( item.ThirdPartyDBPatamter == Titem.Translating.Type = TranslatingType.
                if (item.TargetField !=  "" && item.TargetField != null) //TODO: if 3rd database need not the field
                {
                    sbFields = sbFields + item.TargetField + "  , ";
                    
                    //sbValues = sbValues + "@" + item.TargetField.Trim() + " , ";
                    sbValues = sbValues + "?" + " , ";

                    OleDbParameter p = cmd.Parameters.Add("@" + item.TargetField.Trim(), item.ThirdPartyDBPatamter.FieldType);

                    object obj = dr[item.TargetField.Trim()];

                    if (Convert.IsDBNull(obj))
                    {
                        if (item.Translating.Type == TranslatingType.DefaultValue)
                            p.Value = item.Translating.ConstValue;
                        else
                           p.Value = System.DBNull.Value;
                    }
                    else if (Convert.ToString(obj).Trim() == "")
                    {
                        if (item.ThirdPartyDBPatamter.FieldType == OleDbType.Date)
                            p.Value = System.DBNull.Value;
                        else
                            p.Value = "";
                    }
                    else
                    {
                        //2007-3-23
                        if (item.ThirdPartyDBPatamter.FieldType == OleDbType.VarChar ||
                            item.ThirdPartyDBPatamter.FieldType == OleDbType.VarWChar ||
                            item.ThirdPartyDBPatamter.FieldType == OleDbType.Char ||
                            item.ThirdPartyDBPatamter.FieldType == OleDbType.WChar)
                            p.Value = MapingHelper.FixSigleQuoteInSQLStringValue(obj.ToString());
                        else
                            p.Value = obj;
                    }                 


                }                
            }

            if (sbFields.Length > 0) sbFields = sbFields.Substring(0, sbFields.Length - 2);
            if (sbValues.Length > 0) sbValues = sbValues.Substring(0, sbValues.Length - 2);

            string sResult = " insert into "+ ch.OperationName +"("+ sbFields + ")" + " values("+ sbValues +")";
            cmd.CommandText = sResult;

            return true;
        }

        static public bool BuildUpdateCmd(SQLOutboundChanel ch, DataRow dr, OleDbCommand cmd)
        {
            if (ch.Rule.QueryResult.MappingList.Count < 1)
            {
                Program.Log.Write(LogType.Warning, "There is no mapinglist");
                return false;
            }

            ArrayList alSet = new System.Collections.ArrayList();
            ArrayList alWhere = new System.Collections.ArrayList();

            StringBuilder sbSet = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();

            foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName == null || item.ThirdPartyDBPatamter.FieldName.Length < 1) continue;

                if (item.RedundancyFlag)
                {
                    //sbWhere.Append(item.TargetField + "=@" + item.TargetField + " and ");
                    sbWhere.Append(item.TargetField + "=?"  + " and ");
                    OleDbParameter p = new OleDbParameter("@" + item.TargetField, item.ThirdPartyDBPatamter.FieldType);
                    p.Value = dr[item.TargetField.Trim()];
                    alWhere.Add(p);
                }
                else
                {
                    //sbSet.Append(item.TargetField + "=@" + item.TargetField + ",");
                    sbSet.Append(item.TargetField + "=?"  + " , ");
                    OleDbParameter p = new OleDbParameter("@" + item.TargetField, item.ThirdPartyDBPatamter.FieldType);
                    
                    object obj = dr[item.TargetField.Trim()];

                    if (Convert.IsDBNull(obj))
                    {
                        if (item.Translating.Type == TranslatingType.DefaultValue)
                            p.Value = item.Translating.ConstValue;
                        else
                            p.Value = System.DBNull.Value;
                    }
                    else if (Convert.ToString(obj).Trim() == "")
                    {
                        if (item.ThirdPartyDBPatamter.FieldType == OleDbType.Date)
                            p.Value = System.DBNull.Value;
                    }
                    else
                    {
                        //2007-3-23
                        if (item.ThirdPartyDBPatamter.FieldType == OleDbType.VarChar ||
                            item.ThirdPartyDBPatamter.FieldType == OleDbType.VarWChar ||
                            item.ThirdPartyDBPatamter.FieldType == OleDbType.Char ||
                            item.ThirdPartyDBPatamter.FieldType == OleDbType.WChar)
                            p.Value = MapingHelper.FixSigleQuoteInSQLStringValue(obj.ToString());
                        else
                            p.Value = obj;
                    }
                        
                    alSet.Add(p);
                }
            }


            if (sbWhere.Length > 0)
                sbWhere.Remove(sbWhere.Length - 4, 4);

            if (sbSet.Length > 0)
                sbSet.Remove(sbSet.Length - 2, 2);

            cmd.CommandText = " update " + ch.OperationName + " set  " + sbSet.ToString() + " where " + sbWhere.ToString();

            foreach (OleDbParameter item in alSet)
                cmd.Parameters.Add(item);
            foreach (OleDbParameter item in alWhere)
                cmd.Parameters.Add(item);
            
            return true;
        }

        static public string BuildWhereString(SQLOutboundChanel ch)
        {
            if (ch.OperationType == ThrPartyDBOperationType.StorageProcedure)
                throw new Exception("StorgaeProcedure need not Build where string");
            if (ch.Rule.QueryResult.MappingList.Count < 1)
                return "";
            
            StringBuilder sbWhere = new StringBuilder();

            foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName == null || item.ThirdPartyDBPatamter.FieldName.Length < 1) continue;

                if (item.RedundancyFlag)
                {                    
                    //sbWhere.Append(item.TargetField + "=@" + item.TargetField + " and ");
                    sbWhere.Append(item.TargetField + "=?"  + " and ");
                }
                
            }

            string sResult="";
            if (sbWhere.Length > 0)
            {
                sbWhere.Remove(sbWhere.Length - 4, 4);
                sResult =  " where " + sbWhere.ToString();
            }
                        
            return sResult;
        }

        static public string BuildSQLStatement(SQLOutboundChanel ch,DataRow dr)
        {
            
            if (ch.OperationType != ThrPartyDBOperationType.SQLStatement)
            {
                return "";
            }

            Program.Log.Write("Start Build SQL Statement  ");

            StringBuilder sb = new StringBuilder();
            sb.Append(ch.OperationName);

            foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName == null || item.ThirdPartyDBPatamter.FieldName.Length < 1) continue;

                try
                {
                    sb.Replace("{" + item.ThirdPartyDBPatamter.FieldName + "}", dr[item.TargetField.Trim()].ToString());
                }catch(Exception ex)
                {
                    Program.Log.Write(LogType.Error,"Replace parameter "+item.ThirdPartyDBPatamter.FieldName +" error, "+ex.Message);
                }
                

            }
            Program.Log.Write(sb.ToString());
            
            return sb.ToString();


        }
        //static public string BuildInsString(SQLOutboundChanel ch)
        //{
        //    if (ch.OperationType == ThrPartyDBOperationType.StorageProcedure)
        //        throw new Exception("StorgaeProcedure need not Build insert string");
        //    if (ch.Rule.QueryResult.MappingList.Count < 1)
        //        return "";

        //    StringBuilder sbFields = new StringBuilder();
        //    StringBuilder sbValues = new StringBuilder();

        //    foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
        //    {
        //        //if( item.ThirdPartyDBPatamter == Titem.Translating.Type = TranslatingType.
        //        if (item.TargetField != "" && item.TargetField != null) //TODO: if 3rd database need not the field
        //        {
        //            sbFields.Append(item.TargetField + ",");

        //            sbValues.Append("@" + item.TargetField.Trim() + ",");
        //        }

        //        if (sbFields.Length > 0) sbFields.Remove(sbFields.Length - 1, 1);
        //        if (sbValues.Length > 0) sbValues.Remove(sbValues.Length - 1, 1);
        //    }
        //    string sResult = " insert into " + ch.OperationName + "(" + sbFields.ToString() + ")" + " values(" + sbValues.ToString() + ")";
        //    return sResult;
        //}

        //static public string BuildUpdString(SQLOutboundChanel ch)
        //{
        //    if (ch.OperationType == ThrPartyDBOperationType.StorageProcedure)
        //        throw new Exception("StorgaeProcedure need not Build insert string");
        //    if (ch.Rule.QueryResult.MappingList.Count < 1)
        //        return "";

        //    StringBuilder sbSet = new StringBuilder();
        //    StringBuilder sbWhere = new StringBuilder();

        //    foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
        //    {
        //        if (item.RedundancyFlag)
        //            sbWhere.Append(item.TargetField + "=@" + item.TargetField + " and ");
        //        else
        //            sbSet.Append(item.TargetField + "=@" + item.TargetField + ",");
        //    }


        //    if (sbWhere.Length > 0)
        //        sbWhere.Remove(sbWhere.Length - 4, 4);

        //    if (sbSet.Length > 0)
        //        sbSet.Remove(sbSet.Length - 1, 1);

        //    string sResult = " update " + ch.OperationName + " " + sbSet.ToString() + " where " + sbWhere.ToString();
        //    return sResult;
        //}


    }

   
}
