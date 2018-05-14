using System;
using System.Data;
using System.Timers;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Logging;
using SQLInboundAdapter.Objects;
using HYS.Common.Objects.Rule;
using HYS.SQLInboundAdapterObjects;
using System.IO;
using HYS.Common.Objects.Config;
using System.Reflection;

namespace SQLInboundAdapter.Adapters
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
            if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Passtive)
            {
                Program.Log.Write("Adapter is running in passive mode.");
                return;
            }
            else if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Active)
            {

                Program.Log.Write("Adapter Start");
                Program.Log.Write("-----------------------------------");
                Program.Log.Write("DB Connection String: " + Program.db.ConnectionString);
                Program.Log.Write("Interval: " + SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.TimerInterval + "ms");
                Program.Log.Write("-----------------------------------");

                _timer.Interval = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.TimerInterval;
            }
            else if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Access)
            {
                Program.Log.Write("Adapter Start");
                Program.Log.Write("-----------------------------------");
                Program.Log.Write("DB Connection String: " + Program.db.ConnectionString);
                Program.Log.Write("Interval: " + SQLInAdapterConfigMgt.SQLInAdapterConfig.ThrPartyAppConfig.TimerInterval + "ms");
                Program.Log.Write("-----------------------------------");

                _timer.Interval = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThrPartyAppConfig.TimerInterval;
            }
            _timer.Start();
        }

        public void Stop()
        {
            if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Passtive)
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

                if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Active)
                {
                    Program.Log.Write("Query data started.");
                    if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileConnection)
                    {
                        QueryDataFile();
                    }
                    else
                    {
                        if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.OracleDriver)
                        {
                            QueryData3();
                        }
                        else if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ShareConnectionAmongChannels)
                        {
                            QueryData2();
                        }
                        else
                        {
                            QueryData();
                        }
                    }
                    Program.Log.Write("Query data finished.\r\n");
                }
                else if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.InteractType == InteractType.Access)
                {
                    Program.Log.Write("Load External File started.");
                    if (!string.IsNullOrEmpty(SQLInAdapterConfigMgt.SQLInAdapterConfig.ThrPartyAppConfig.FilePath))
                    {
                        LoadExternalFile();
                    }
                    Program.Log.Write("Load External File finished.\r\n");
                }
            }
            catch (Exception ex)
            {
                Program.Log.Write(ex);
            }
            finally
            {
                _timer.Enabled = true;
            }
        }

        #endregion

        #region Data Control

        public event DataReceiveEventHandler OnDataReceived;

        private void QueryData()
        {
            //DataSet ds = Program.db.ExecuteQuery(ConfigurationMgt.Config.SQLStatement);

            foreach (SQLInboundChanel ch in SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundChanels)
            {
                try
                {
                    if (!ch.Enable) continue;

                    List<DataSet> dslist = ReadData(ch);
                    foreach (DataSet ds1 in dslist)
                    {
                        DataSet ds2 = TranslateData(ch, ds1);

                        Program.db.CloseDBConnection();

                        if (ds2 == null)
                        {
                            Program.Log.Write(LogType.Error, "Query data failed.\r\n" + Program.db.LastError);
                            return;
                        }

                        Program.Log.Write("Receive record count: " + ds2.Tables[0].Rows.Count.ToString());
                        //TestCase.OnDataReceived(ch.Rule, ds2);// DEBUG    // 20110113 this rubbish statement has been here for so long time

                        if (OnDataReceived != null)
                        {
                            Program.Log.Write("before OnDataReceived(ch.Rule, ds2);");
                            OnDataReceived(ch.Rule, ds2);
                            Program.Log.Write("after OnDataReceived(ch.Rule, ds2);");
                        }
                        else
                        {
                            Program.Log.Write("OnDataReceived = null");
                        }

                        //ds2.Tables[0].WriteXml(DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss" + ".xml"));
                    }
                }
                catch (Exception err)
                {
                    Program.Log.Write(err);
                }
            }
        }

        private List<DataSet> ReadData(SQLInboundChanel ch)
        {
            List<DataSet> list = new List<DataSet>();

            if (ch.OperationType == ThrPartyDBOperationType.StorageProcedure)
            {
                if (ch.Rule.IsInputParameterSPEnable())
                {
                    DataSet dsParam = GetInputParameter(ch);
                    if (dsParam != null && dsParam.Tables.Count > 0)
                    {
                        foreach (DataRow drParam in dsParam.Tables[0].Rows)
                        {
                            OleDbCommand cmd = Program.db.GetCommand();
                            cmd.CommandText = ch.OperationName;

                            SetParameter(ch, cmd, drParam);

                            DataSet ds = CallSP(ch, cmd);
                            list.Add(ds);
                        }
                    }
                }
                else
                {
                    OleDbCommand cmd = Program.db.GetCommand();
                    cmd.CommandText = ch.OperationName;

                    SetParameter(ch, cmd, null);

                    DataSet ds = CallSP(ch, cmd);
                    list.Add(ds);
                }

            }
            else
            {
                DataSet ds = ExecuteSQL(ch);
                list.Add(ds);
            }

            return list;
        }

        private DataSet ExecuteSQL(SQLInboundChanel ch)
        {
            //build select string
            //string sSelect = BuildSelectString(ch);

            ////build where string
            //string sWhere = ch.Rule.QueryCriteria.SQLStatement;
            //if (sWhere != null && sWhere.Trim() != "")
            //    sWhere = " where " + sWhere;

            //cmd.CommandText = BuildSelectString(ch) + sWhere;

            OleDbCommand cmd = Program.db.GetCommand();

            cmd.CommandText = ch.Rule.QueryCriteria.SQLStatement;
            cmd.CommandType = CommandType.Text;

            Program.Log.Write("CommandText='" + cmd.CommandText + "'");

            try
            {
                DataSet ds = new DataSet();
                if (!Program.db.ExecCommand(cmd, ds))
                    return null;
                else
                    return ds;
            }
            catch (OleDbException err)
            {
                //Program.Log.Write(err);

                string errMsg = err.ToString();
                Program.Log.Write(LogType.Error, errMsg);

                if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.SuicideWhenOleDbException)
                {
                    string errCode = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.SuicideWhenOleDbExceptionErrorCodeExclude;
                    if (!string.IsNullOrEmpty(errCode))
                    {
                        if (errMsg.IndexOf(errCode) >= 0)
                        {
                            Program.Log.Write(LogType.Error, "Ignore error code: " + errCode);
                            return null;
                        }
                    }

                    Program.Log.Write(LogType.Error, "Killing myself now.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }

                return null;
            }
        }

        private DataSet CallSP(SQLInboundChanel ch, OleDbCommand cmd)
        {
            if (ch.CallSPAsSQLText)
            {
                cmd.CommandType = CommandType.Text;
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
            }

            #region Log Prameter
            Program.Log.Write(" ============ Storage Procedure Parameter===============");
            foreach (OleDbParameter p in cmd.Parameters)
            {
                string str = "";
                if (p.Value == null) str = "(null)";
                else if (p.Value == DBNull.Value) str = "(dbnull)";
                else str = p.Value.ToString();

                Program.Log.Write(p.ParameterName + "=" + str);
            }
            Program.Log.Write(" ========================= Parameter End ===============");
            #endregion

            Program.Log.Write("CommandText='" + cmd.CommandText + "'");

            try
            {
                DataSet ds = new DataSet();
                if (!Program.db.ExecCommand(cmd, ds))
                    return null;
                else
                    return ds;
            }
            catch (OleDbException err)
            {
                //Program.Log.Write(err);

                string errMsg = err.ToString();
                Program.Log.Write(LogType.Error, errMsg);

                if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.SuicideWhenOleDbException)
                {
                    string errCode = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.SuicideWhenOleDbExceptionErrorCodeExclude;
                    if (!string.IsNullOrEmpty(errCode))
                    {
                        if (errMsg.IndexOf(errCode) >= 0)
                        {
                            Program.Log.Write(LogType.Error, "Ignore error code: " + errCode);
                            return null;
                        }
                    }

                    Program.Log.Write(LogType.Error, "Killing myself now.");
                    System.Diagnostics.Process.GetCurrentProcess().Kill();
                }

                return null;
            }
        }

        private DataSet TranslateData(SQLInboundChanel ch, DataSet ds1)
        {

            // Build schema
            DataSet ds2 = new DataSet();
            DataTable table = new DataTable(ds1.Tables[0].TableName);
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
                    object obj = dr1[item.ColumnName];

                    if (Convert.IsDBNull(obj))
                    {
                        dr2[item.ColumnName] = System.DBNull.Value;
                        continue;
                    }

                    if (item.DataType == typeof(System.DateTime))
                    {
                        //dr2[item.ColumnName] = Convert.ToDateTime(dr1[item.ColumnName]).ToString("yyyy-MM-dd hh:mm:ss");
                        dr2[item.ColumnName] = Convert.ToDateTime( obj ).ToString(GWDataDB.DateTimeFormat);
                    }
                    else
                      dr2[item.ColumnName] = Convert.ToString(dr1[item.ColumnName]);
                }
                table.Rows.Add(dr2);
            }

            return ds2;
        }

        /// <summary>
        /// ch.OperationType = ThrPartyDBOperationType.Table || View
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        private string BuildSelectString( SQLInboundChanel ch)
        {
            string sSelect = "";
            foreach (SQLInQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                sSelect = sSelect + item.ThirdPartyDBPatamter.FieldName + " , ";
            }
            sSelect = sSelect.Substring(0, sSelect.Length - 2);

            sSelect = " select "+sSelect + " from "+ch.OperationName;

            return sSelect;
        }

        /// <summary>
        /// ch.OperationType = ThrPartyDBOperationType.StorageProcedure
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool SetParameter(SQLInboundChanel ch, OleDbCommand cmd, DataRow row)
        {
            foreach (SQLInQueryCriteriaItem item in ch.Rule.QueryCriteria.MappingList)
            {
                OleDbParameter p = new OleDbParameter();
                p.OleDbType = item.ThirdPartyDBPatamter.FieldType;
                p.ParameterName = "@" + item.ThirdPartyDBPatamter.FieldName;

                if (item.IsNull)
                {
                    p.Value = DBNull.Value;
                }
                else if (item.IsGetFromStorageProcedure)
                {
                    string paramName = item.ThirdPartyDBPatamter.FieldName;
                    if (row.Table.Columns.Contains(paramName))
                    {
                        p.Value = row[paramName];
                    }
                    else
                    {
                        Program.Log.Write(LogType.Warning, "Cannot find input parameter from SP result set, parameter name: " + paramName);
                        p.Value = item.Translating.ConstValue;
                    }
                }
                else
                {
                    p.Value = item.Translating.ConstValue;
                    //if (item.Translating.Type == TranslatingType.FixValue ||
                    //    item.Translating.Type == TranslatingType.DefaultValue)
                    //    cmd.Parameters.Add(p);
                    //else
                    //    Program.Log.Write(LogType.Warning, item.ThirdPartyDBPatamter.FieldName + ": StorageProcedure Only Support FixValue or DefaultValue parameter!");
                }

                cmd.Parameters.Add(p);
            }
            return true;
        }

        private string _gwDataDBConnectionString = null;
        private string GWDataDBConnectionString
        {
            get
            {
                if (_gwDataDBConnectionString != null) return _gwDataDBConnectionString;

                HYS.Common.Objects.Config.AdapterServiceCfgMgt ConfigMgt = new HYS.Common.Objects.Config.AdapterServiceCfgMgt();
                ConfigMgt.FileName = Application.StartupPath + "\\" + ConfigMgt.FileName;

                if (ConfigMgt.Load())
                {
                    Program.Log.Write("Load adapter service config succeeded. " + ConfigMgt.FileName);
                    _gwDataDBConnectionString = ConfigMgt.Config.DataDBConnection;
                }
                else
                {
                    Program.Log.Write(LogType.Error, "Load adapter service config failed. " + ConfigMgt.FileName);
                }

                return _gwDataDBConnectionString;
            }
        }

        private string _interfaceName = null;
        public string InterfaceName
        {
            get
            {
                if (_interfaceName != null) return _interfaceName;

                HYS.Common.Objects.Device.DeviceDirManager DeviceMgt = new HYS.Common.Objects.Device.DeviceDirManager();
                DeviceMgt.FileName = Application.StartupPath + "\\" + HYS.Common.Objects.Device.DeviceDirManager.IndexFileName;
                if (DeviceMgt.LoadDeviceDir())
                {
                    Program.Log.Write("Load DeviceDir succeeded. " + DeviceMgt.FileName);
                    _interfaceName = DeviceMgt.DeviceDirInfor.Header.Name;
                }
                else
                {
                    Program.Log.Write(LogType.Error, "Load DeviceDir failed. " + DeviceMgt.FileName);
                }

                return _interfaceName;
            }
        }

        private DataSet GetInputParameter(SQLInboundChanel ch)
        {
            string dbCnn = "";
            string spName = "";

            try
            {
                dbCnn = GWDataDBConnectionString;
                spName = ch.Rule.GenerateInputParameterSPName(InterfaceName);

                Program.Log.Write("=== Begin executing SP to get input parameter ===");
                Program.Log.Write("DB Connection: " + dbCnn);
                Program.Log.Write("SP Name: " + spName);

                DataSet ds = new DataSet();
                using (OleDbCommand cmd = new OleDbCommand())
                {
                    cmd.CommandText = spName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (OleDbConnection conn = new OleDbConnection(dbCnn))
                    {
                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        {
                            cmd.Connection = conn;

                            conn.Open();
                            da.Fill(ds);
                            conn.Close();
                        }
                    }
                }

                int tc = ds.Tables.Count;
                Program.Log.Write("Result Data Table Count: " + tc.ToString());
                if (tc > 0) Program.Log.Write("Result Data Row Count: " + ds.Tables[0].Rows.Count.ToString());

                Program.Log.Write("=== Finish executing SP to get input parameter ===");

                return ds;
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Error, "=== Error executing SP to get input parameter ===");
                Program.Log.Write(err);
                return null;
            }
        }
        
        #endregion

        private class FileDataSet
        {
            public string IndexFileLocation { get; set; }
            public string DataFileLocation { get; set; }
            public string DataFileName { get; set; }
            public DataSet DataSet { get; set; }
        }

        private void QueryDataFile()
        {
            foreach (SQLInboundChanel ch in SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundChanels)
            {
                try
                {
                    if (!ch.Enable) continue;
                    if (ch.OperationType != ThrPartyDBOperationType.Table) continue;

                    Program.Log.Write("Begin processing channel: " + ch.ChannelName);

                    List<FileDataSet> flist = FindDataFiles();
                    if (flist.Count > 0)
                    {
                        foreach (FileDataSet f in flist)
                        {
                            FileDataSet fds = ReadDataFile(ch, f);
                            if (fds == null)
                            {
                                Program.Log.Write(LogType.Error, "Read data failed.\r\n" + Program.db.LastError);
                                continue;
                            }

                            DataSet ds1 = fds.DataSet;
                            DataSet ds2 = TranslateData(ch, ds1);
                            Program.db.CloseDBConnection();

                            if (ds2 == null)
                            {
                                Program.Log.Write(LogType.Error, "Process data failed.");
                                continue;
                            }

                            Program.Log.Write("Receive record count: " + ds2.Tables[0].Rows.Count.ToString());

                            bool res = false;
                            if (OnDataReceived != null)
                            {
                                Program.Log.Write("before OnDataReceived(ch.Rule, ds2);");
                                res = OnDataReceived(ch.Rule, ds2);
                                Program.Log.Write("after OnDataReceived(ch.Rule, ds2); result: " + res.ToString());
                            }
                            else
                            {
                                Program.Log.Write(LogType.Error, "OnDataReceived = null");
                            }

                            HandleDataFile(fds, res);
                        }
                    }

                    Program.Log.Write("End processing channel: " + ch.ChannelName);
                }
                catch (Exception err)
                {
                    Program.Log.Write(err);
                }
            }
        }
        private List<FileDataSet> FindDataFiles()
        {
            List<FileDataSet> list = new List<FileDataSet>();
            ThirdDBConnection config = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter;
            if (config.IndexFileDriven)
            {
                string iFolder = ConfigHelper.GetFullPath(config.IndexFileFolder);
                string[] iflist = Directory.GetFiles(iFolder, config.FileNamePattern);
                Program.Log.Write(string.Format("Find {0} index file in folder {1}", iflist.Length, iFolder));

                foreach (string f in iflist)
                {
                    string iFile = ConfigHelper.GetFullPath(iFolder, f);
                    string iFileName = Path.GetFileNameWithoutExtension(iFile);
                    string dFileNamePattern = string.Format("{0}.*", iFileName);
                    string dFolder = ConfigHelper.GetFullPath(config.FileFolder);
                    string[] dflist = Directory.GetFiles(dFolder, dFileNamePattern);
                    if (dflist.Length < 1)
                    {
                        Program.Log.Write(LogType.Error,
                            string.Format("Cannot find data file with pattern {0} in folder {1}",
                            dFileNamePattern, dFolder));
                        continue;
                    }

                    string dFile = ConfigHelper.GetFullPath(dFolder, dflist[0]);
                    FileDataSet fds = new FileDataSet();
                    fds.IndexFileLocation = iFile;
                    fds.DataFileLocation = dFile;
                    fds.DataFileName = Path.GetFileName(dFile);
                    Program.Log.Write(string.Format("Find data file named {0} from {1} with index file {2}",
                        fds.DataFileName, fds.DataFileLocation, fds.IndexFileLocation));

                    list.Add(fds);
                }
            }
            else
            {
                string dFolder = ConfigHelper.GetFullPath(config.FileFolder);
                string[] dflist = Directory.GetFiles(dFolder, config.FileNamePattern);
                Program.Log.Write(string.Format("Find {0} data file in folder {1}", dflist.Length, dFolder));

                foreach (string f in dflist)
                {
                    string dFile = ConfigHelper.GetFullPath(dFolder, f);
                    FileDataSet fds = new FileDataSet();
                    fds.DataFileLocation = dFile;
                    fds.DataFileName = Path.GetFileName(dFile);
                    Program.Log.Write(string.Format("Find data file named {0} from {1}",
                        fds.DataFileName, fds.DataFileLocation));

                    list.Add(fds);
                }
            }
            return list;
        }
        private FileDataSet ReadDataFile(SQLInboundChanel ch, FileDataSet fds)
        {
            if (fds == null) return null;

            if (SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileTransformBeforeRead)
            {
                Program.Log.Write("Transforming file: " + fds.DataFileName);
                string filelocation = Path.Combine(SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileFolder, fds.DataFileName);
                string filecontent = File.ReadAllText(filelocation);
                filecontent = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.FileTransformRule.Replace(filecontent, Program.Log);
                File.WriteAllText(filelocation, filecontent);
            }

            OleDbCommand cmd = Program.db.GetCommand();
            cmd.CommandText = ch.Rule.QueryCriteria.SQLStatement
                .Replace("{FileName}", string.Format("[{0}]", fds.DataFileName));
            cmd.CommandType = CommandType.Text;

            Program.Log.Write("CommandText='" + cmd.CommandText + "'");

            DataSet ds = new DataSet();
            if (!Program.db.ExecCommand(cmd, ds)) return null;
            fds.DataSet = ds;

            return fds;
        }
        private void HandleDataFile(FileDataSet fds, bool success)
        {
            bool hasIndex = !string.IsNullOrEmpty(fds.IndexFileLocation);
            if (success)
            {
                if (hasIndex)
                {
                    Program.Log.Write("Deleting success file: " + fds.IndexFileLocation);
                    File.Delete(fds.IndexFileLocation);
                }
                Program.Log.Write("Deleting success file: " + fds.DataFileLocation);
                File.Delete(fds.DataFileLocation);
            }
            else
            {
                ThirdDBConnection config = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter;
                if (config.MoveFileWhenError)
                {
                    string mFolder = ConfigHelper.GetFullPath(config.MoveFileFolder);
                    if (!Directory.Exists(mFolder)) Directory.CreateDirectory(mFolder);
                    if (hasIndex)
                    {
                        string mIFolder = Path.Combine(mFolder, "Index");
                        if (!Directory.Exists(mIFolder)) Directory.CreateDirectory(mIFolder);
                        string iFileName = Path.GetFileName(fds.IndexFileLocation);
                        string iFile = Path.Combine(mIFolder, iFileName);
                        Program.Log.Write("Moving error index file to: " + iFile);
                        File.Move(fds.IndexFileLocation, iFile);
                    }
                    string mDFolder = Path.Combine(mFolder, "Data");
                    if (!Directory.Exists(mDFolder)) Directory.CreateDirectory(mDFolder);
                    string dFile = Path.Combine(mDFolder, fds.DataFileName);
                    Program.Log.Write("Moving error data file to: " + dFile);
                    File.Move(fds.DataFileLocation, dFile);
                }
                else
                {
                    if (hasIndex)
                    {
                        Program.Log.Write("Deleting error file: " + fds.IndexFileLocation);
                        File.Delete(fds.IndexFileLocation);
                    }
                    Program.Log.Write("Deleting error file: " + fds.DataFileLocation);
                    File.Delete(fds.DataFileLocation);
                }
            }
        }

        private void LoadExternalFile()
        {
            try
            {
                Program.Log.Write("Load External File :" + SQLInAdapterConfigMgt.SQLInAdapterConfig.ThrPartyAppConfig.FilePath);
                string filePath = Path.GetDirectoryName(SQLInAdapterConfigMgt.SQLInAdapterConfig.ThrPartyAppConfig.FilePath);
                if (filePath == "")
                {
                    filePath = Application.StartupPath + "\\" + SQLInAdapterConfigMgt.SQLInAdapterConfig.ThrPartyAppConfig.FilePath;
                }
                else
                {
                    filePath = SQLInAdapterConfigMgt.SQLInAdapterConfig.ThrPartyAppConfig.FilePath;
                }
                Assembly assembly = Assembly.LoadFile(filePath);
                foreach (Type type1 in assembly.GetTypes())
                {
                    Type[] types = type1.GetInterfaces();
                    foreach (Type type2 in types)
                    {
                        if (type2 == typeof(IThrPartyAccessApp))
                        {
                            Program.Log.Write("Find Class :" + type1.ToString());
                            var instance = (IThrPartyAccessApp)Activator.CreateInstance(type1);
                            instance.LoadMethod(Program.Log);
                            break;
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                Program.Log.Write("Load External File error:" + ex.ToString());
            }
        }
    }
}
