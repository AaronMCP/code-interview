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

namespace SQLInboundAdapter.Adapters
{
    // 20121106
    // Use less db connections to access 3rd party db to avoid too many concurrent TCP/IP connection.
    // As db connection pool may have been disabled on Sybase server in HK, we have to control the number of connection manually.
    public partial class DataAccessControler
    {
        private void QueryData2()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(
                    SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr))
                {
                    Program.Log.Write("--- begin connecting with 3rd party db ---");
                    conn.Open();

                    foreach (SQLInboundChanel ch in SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundChanels)
                    {
                        if (!ch.Enable) continue;

                        List<DataSet> dslist = ReadData2(conn, ch);
                        foreach (DataSet ds1 in dslist)
                        {
                            DataSet ds2 = TranslateData(ch, ds1);

                            if (ds2 == null)
                            {
                                Program.Log.Write(LogType.Error, "Query data failed.\r\n" + Program.db.LastError);
                                return;
                            }

                            Program.Log.Write("Receive record count: " + ds2.Tables[0].Rows.Count.ToString());

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
                        }

                    }

                    conn.Close();
                }
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }

            Program.Log.Write("--- end connecting with 3rd party db ---");
        }

        private List<DataSet> ReadData2(OleDbConnection cnn, SQLInboundChanel ch)
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
                            cmd.Connection = cnn;

                            SetParameter(ch, cmd, drParam);

                            DataSet ds = CallSP2(ch, cmd);
                            list.Add(ds);
                        }
                    }
                }
                else
                {
                    OleDbCommand cmd = Program.db.GetCommand();
                    cmd.CommandText = ch.OperationName;
                    cmd.Connection = cnn;

                    SetParameter(ch, cmd, null);

                    DataSet ds = CallSP2(ch, cmd);
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

        private DataSet CallSP2(SQLInboundChanel ch, OleDbCommand cmd)
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
                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                {
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (OleDbException err)
            {
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

        private DataSet ExecuteSQL2(OleDbConnection cnn, SQLInboundChanel ch)
        {
            OleDbCommand cmd = Program.db.GetCommand();

            cmd.CommandText = ch.Rule.QueryCriteria.SQLStatement;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;

            Program.Log.Write("CommandText='" + cmd.CommandText + "'");

            try
            {
                DataSet ds = new DataSet();
                using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                {
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (OleDbException err)
            {
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
    }
}
