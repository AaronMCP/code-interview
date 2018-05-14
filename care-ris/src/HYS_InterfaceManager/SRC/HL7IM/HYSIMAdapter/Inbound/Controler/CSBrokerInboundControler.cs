using System;
using System.Data;
using System.Data.OleDb;
using System.Timers;
using HYS.IM.Common.Logging;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Adapters;
using System.Xml;
using System.IO;
using HYS.IM.MessageDevices.CSBAdapter.Inbound.Config;

namespace HYS.IM.MessageDevices.CSBAdapter.Inbound.Controler
{
    public partial class CSBrokerInboundControler
    {
        private EntityImpl _entity;
        private Timer _readerTimer = null;
        public CSBrokerInboundControler(EntityImpl publisher)
        {
            _entity = publisher;
            _readerTimer = new Timer(_entity.Context.ConfigMgr.Config.TimerInterval);
            _readerTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
            {
                try
                {
                    _readerTimer.Stop();

                    _entity.Context.Log.Write("");

                    _entity.Context.Log.Write("Begin process patient message.");
                    ProcessCSBrokerMessage("Patient");
                    _entity.Context.Log.Write("End process patient message.");

                    _entity.Context.Log.Write("Begin process order message.");
                    ProcessCSBrokerMessage("Order");
                    _entity.Context.Log.Write("End process order message.");

                    _entity.Context.Log.Write("Begin process report message.");
                    ProcessCSBrokerMessage("Report");
                    _entity.Context.Log.Write("End process report message.");
                }
                catch (Exception ex)
                {
                    _entity.Context.Log.Write(ex);
                }
                finally
                {
                    _readerTimer.Start();
                }
            };
        }

        private void ProcessCSBrokerMessage(string tableName)
        {
            try
            {
                OleDbCommand cmd = new OleDbCommand(@"sp_" + _entity.Context.ConfigMgr.Config.CSBrokerSQLOutboundName + "_" + tableName);
                cmd.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                using (OleDbConnection conn = new OleDbConnection(_entity.Context.ConfigMgr.Config.CSBrokerConnectionString))
                {
                    conn.Open();
                    cmd.Connection = conn;
                    OleDbDataAdapter oleAdapter = new OleDbDataAdapter(cmd);
                    oleAdapter.Fill(dt);
                    conn.Close();
                }

                dt.TableName = tableName;
                _entity.Context.Log.Write(string.Format("Found {0} record(s).", dt.Rows.Count));

                ProcessDataTable(dt);
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
            }
        }
        private void ProcessDataTable(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            ReplaceValueInDataTable(dt);

            int iRowCount = dt.Rows.Count;
            for (int i = 0; i < iRowCount; i++)
            {
                _entity.Context.Log.Write(string.Format("Dispatching {0} message {1}/{2}", dt.TableName, i + 1, iRowCount));
                ProcessDataRow(dt.Rows[i]);
            }
        }
        private void ProcessDataRow(DataRow dataRow)
        {
            try
            {
                bool res = false;
                string strResponse = string.Empty;
                if (_entity.Context.ConfigMgr.Config.MessageDispatchConfig.Model == MessageDispatchModel.Publish)
                {
                    res = DispatchMessage_Publish(dataRow);
                }
                else if (_entity.Context.ConfigMgr.Config.MessageDispatchConfig.Model == MessageDispatchModel.Request)
                {
                    res = DispatchMessage_Request(dataRow, ref strResponse);
                }
                else if (_entity.Context.ConfigMgr.Config.MessageDispatchConfig.Model == MessageDispatchModel.Custom)
                {
                    res = DispatchMessage_Custom(dataRow, ref strResponse);
                }

                if (res) UpdateDataRowStatus(dataRow, strResponse);
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
            }
        }
        private void UpdateDataRowStatus(DataRow row, string strResponse)
        {
            _entity.Context.Log.Write("Updating data process flag.");

            string strCmd = null;
            if (string.IsNullOrEmpty(strResponse))
            {
                strCmd = @"UPDATE [dbo].["
                    + _entity.Context.ConfigMgr.Config.CSBrokerSQLOutboundName
                    + @"_DATAINDEX]  SET [PROCESS_FLAG] = '1' WHERE [Data_ID] = ?";
            }
            else
            {
                _entity.Context.Log.Write(strResponse);
                DataSet ds = DeserializeXmlToDataSet(strResponse);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataRow dataRow = ds.Tables[0].Rows[0];
                    strCmd = @"UPDATE [dbo].[" + _entity.Context.ConfigMgr.Config.CSBrokerSQLOutboundName + @"_DATAINDEX] SET ";
                    if (dataRow["DATAINDEX_PROCESS_FLAG"].ToString() != string.Empty)
                    {
                        strCmd += string.Format("{0} = N'{1}',", "[PROCESS_FLAG]", dataRow["DATAINDEX_PROCESS_FLAG"].ToString().Replace("'", "''"));
                    }

                    if (dataRow["DATAINDEX_RECORD_INDEX_1"].ToString() != string.Empty)
                    {
                        strCmd += string.Format("{0} = N'{1}',", "[RECORD_INDEX_1]", dataRow["DATAINDEX_RECORD_INDEX_1"].ToString().Replace("'", "''"));
                    }

                    if (dataRow["DATAINDEX_RECORD_INDEX_2"].ToString() != string.Empty)
                    {
                        strCmd += string.Format("{0} = N'{1}',", "[RECORD_INDEX_2]", dataRow["DATAINDEX_RECORD_INDEX_2"].ToString().Replace("'", "''"));
                    }

                    if (dataRow["DATAINDEX_RECORD_INDEX_3"].ToString() != string.Empty)
                    {
                        strCmd += string.Format("{0} = N'{1}',", "[RECORD_INDEX_3]", dataRow["DATAINDEX_RECORD_INDEX_3"].ToString().Replace("'", "''"));
                    }

                    if (dataRow["DATAINDEX_RECORD_INDEX_4"].ToString() != string.Empty)
                    {
                        strCmd += string.Format("{0} = N'{1}',", "[RECORD_INDEX_4]", dataRow["DATAINDEX_RECORD_INDEX_4"].ToString().Replace("'", "''"));
                    }

                    strCmd = strCmd.TrimEnd(',');
                    strCmd += " WHERE [Data_ID] = ?";
                }
            }

            OleDbCommand cmd = new OleDbCommand(strCmd);
            cmd.CommandType = CommandType.Text;
            OleDbParameter dataId = new OleDbParameter("?DATAINDEX_DATA_ID", OleDbType.Guid);
            dataId.Value = new Guid(row["DATAINDEX_DATA_ID"].ToString());
            cmd.Parameters.Add(dataId);

            using (OleDbConnection conn = new OleDbConnection(_entity.Context.ConfigMgr.Config.CSBrokerConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void ReplaceValueInDataTable(DataTable tb)
        {


            if (tb == null) 
                return;
          

            try
            {
                if (_entity.Context.ConfigMgr.Config.EnaleKanJiReplacement)
                {
                    KanJiReplacement(ref tb);
                }

                if (!_entity.Context.ConfigMgr.Config.EnableValueReplacement) 
                    return;
                _entity.Context.Log.Write("Begin value replacement in CS Broker DataSet.");

                int i = 1;
                int c = tb.Rows.Count;
                foreach (DataRow dr in tb.Rows)
                {
                    _entity.Context.Log.Write(string.Format("Processing record {0}/{1}", i++, c));
                    foreach (ValueReplacementRule r in _entity.Context.ConfigMgr.Config.ValueReplacement)
                    {
                        if (!tb.Columns.Contains(r.FieldName))
                        {
                            _entity.Context.Log.Write(LogType.Warning, "Cannot find column: " + r.FieldName);
                            continue;
                        }

                        string oldValue = dr[r.FieldName] as string;
                        //string newValue = Regex.Replace(oldValue, r.MatchExpression, r.ReplaceExpression);
                        string newValue = r.Replace(oldValue);
                        dr[r.FieldName] = newValue;
                        _entity.Context.Log.Write(string.Format("{0}:{1}->{2}", r.FieldName, oldValue, newValue));
                    }
                }
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
            }

            _entity.Context.Log.Write("Finish value replacement in CS Broker DataSet.");
        }
        private DataSet DeserializeXmlToDataSet(string csbDataSetXml)
        {
            try
            {
                _entity.Context.Log.Write("Begin deserialize CS Broker DataSet.");
                DataSet ds = new DataSet();
                XmlReadMode m = ds.ReadXml(XmlReader.Create(new StringReader(csbDataSetXml)));
                _entity.Context.Log.Write(string.Format("Finish deserialize CS Broker DataSet. Name: {0}, RowCount: {1}, XmlReadMode: {2}.",
                    ds.DataSetName, ds.Tables.Count > 0 ? ds.Tables[0].Rows.Count : -1, m));
                return ds;
            }
            catch (Exception err)
            {
                _entity.Context.Log.Write(err);
                return null;
            }
        }

        public bool StartTimer()
        {
            _readerTimer.Start();
            return true;
        }
        public bool StopTimer()
        {
            _readerTimer.Stop();
            return true;
        }
        private void KanJiReplacement(ref DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0 || !dt.Columns.Contains("PATIENT_PATIENT_NAME"))
                return;
            string kanjiReplacementChar=_entity.Context.ConfigMgr.Config.KanJiReplacementChar;
            foreach (DataRow dr in dt.Rows)
            {
                string patientName=Convert.ToString(dr["PATIENT_PATIENT_NAME"]);

                dr["PATIENT_PATIENT_NAME"] = Level3KanJiReplacement.Replacement(patientName, kanjiReplacementChar);
            }
        }
    }
}
