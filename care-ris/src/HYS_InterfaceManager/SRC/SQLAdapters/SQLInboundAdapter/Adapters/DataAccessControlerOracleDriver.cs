using System;
using System.Data;
using System.Timers;
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
using Oracle.DataAccess.Client;

namespace SQLInboundAdapter.Adapters
{
    // 20141010
    // Use Oracle driver connections to access 3rd party db to avoid too many concurrent TCP/IP connection.
    // To fix the issue that oledb can not run storeprocedure for oracle of x64 bit
    public partial class DataAccessControler
    {
        private void QueryData3()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(
                    SQLInAdapterConfigMgt.SQLInAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr.Replace("Provider=;", "").Replace("Database=;", "")))
                {
                    Program.Log.Write("--- begin connecting with 3rd party db ---");
                    conn.Open();

                    foreach (SQLInboundChanel ch in SQLInAdapterConfigMgt.SQLInAdapterConfig.InboundChanels)
                    {
                        if (!ch.Enable) continue;

                        List<DataSet> dslist = ReadData3(conn, ch);
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

        private List<DataSet> ReadData3(OracleConnection cnn, SQLInboundChanel ch)
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
                            OracleCommand cmd = new OracleCommand();
                            cmd.CommandText = ch.OperationName;
                            cmd.Connection = cnn;

                            SetParameter(ch, cmd, drParam);

                            DataSet ds = CallSP3(ch, cmd);
                            list.Add(ds);
                        }
                    }
                }
                else
                {
                    OracleCommand cmd = new OracleCommand();
                    cmd.CommandText = ch.OperationName;
                    cmd.Connection = cnn;

                    SetParameter(ch, cmd, null);

                    DataSet ds = CallSP3(ch, cmd);
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

        private DataSet CallSP3(SQLInboundChanel ch, OracleCommand cmd)
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
            foreach (OracleParameter p in cmd.Parameters)
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

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("mc", OracleDbType.RefCursor).Direction = ParameterDirection.Output;


            try
            {
                DataSet ds = new DataSet();
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (OracleException err)
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

        private DataSet ExecuteSQL3(OracleConnection cnn, SQLInboundChanel ch)
        {
            OracleCommand cmd = new OracleCommand();

            cmd.CommandText = ch.Rule.QueryCriteria.SQLStatement;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = cnn;


            Program.Log.Write("CommandText='" + cmd.CommandText + "'");

            try
            {
                DataSet ds = new DataSet();
                using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                {
                    da.Fill(ds);
                    return ds;
                }
            }
            catch (OracleException err)
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


        /// <summary>
        /// ch.OperationType = ThrPartyDBOperationType.StorageProcedure
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        private bool SetParameter(SQLInboundChanel ch, OracleCommand cmd, DataRow row)
        {
            foreach (SQLInQueryCriteriaItem item in ch.Rule.QueryCriteria.MappingList)
            {
                OracleParameter p = new OracleParameter();
                p.DbType = OleDBType2DBType(item.ThirdPartyDBPatamter.FieldType);
                
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


        private DbType OleDBType2DBType(System.Data.OleDb.OleDbType oleDBType)
        {
            DbType dbType=DbType.Object;
            switch(oleDBType)
            {
                case System.Data.OleDb.OleDbType.BigInt:
                    dbType=DbType.Int64;
                    break;
                case System.Data.OleDb.OleDbType.Binary:
                    dbType=DbType.Binary;
                    break;
                case System.Data.OleDb.OleDbType.Boolean:
                    dbType=DbType.Boolean;
                    break;
                case System.Data.OleDb.OleDbType.BSTR:
                    dbType=DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Char:
                    dbType=DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Currency:
                    dbType=DbType.Currency;
                    break;
                case System.Data.OleDb.OleDbType.Date:
                    dbType=DbType.Date;
                    break;
                case System.Data.OleDb.OleDbType.DBDate:
                    dbType=DbType.Date;
                    break;
                case System.Data.OleDb.OleDbType.DBTime:
                    dbType=DbType.Time;
                    break;
                case System.Data.OleDb.OleDbType.DBTimeStamp:
                    dbType=DbType.Date;
                    break;
                case System.Data.OleDb.OleDbType.Decimal:
                    dbType=DbType.Decimal;
                    break;
                case System.Data.OleDb.OleDbType.Double:
                    dbType=DbType.Double;
                    break;
                case System.Data.OleDb.OleDbType.Empty:
                    dbType=DbType.Object;
                    break;
                case System.Data.OleDb.OleDbType.Error:
                    dbType=DbType.Object;
                    break;
                case System.Data.OleDb.OleDbType.Filetime:
                    dbType=DbType.UInt64;
                    break;
                case System.Data.OleDb.OleDbType.Guid:
                    dbType=DbType.Guid;
                    break;
                case System.Data.OleDb.OleDbType.IDispatch:
                    dbType=DbType.Object;
                    break;
                case System.Data.OleDb.OleDbType.Integer:
                    dbType=DbType.Int64;
                    break;
                case System.Data.OleDb.OleDbType.IUnknown:
                    dbType=DbType.Object;
                    break;
                case System.Data.OleDb.OleDbType.LongVarBinary:
                    dbType=DbType.Binary;
                    break;
                case System.Data.OleDb.OleDbType.LongVarChar:
                    dbType=DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.LongVarWChar:
                    dbType=DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Numeric:
                    dbType=DbType.VarNumeric;
                    break;
                case System.Data.OleDb.OleDbType.PropVariant:
                    dbType=DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Single:
                    dbType=DbType.Double;
                    break;
                case System.Data.OleDb.OleDbType.SmallInt:
                    dbType = DbType.Int16;
                    break;
                case System.Data.OleDb.OleDbType.TinyInt:
                    dbType=DbType.Int16;
                    break;
                case System.Data.OleDb.OleDbType.UnsignedBigInt:
                    dbType=DbType.UInt64;
                    break;
                case System.Data.OleDb.OleDbType.UnsignedInt:
                    dbType=DbType.UInt64;
                    break;
                case System.Data.OleDb.OleDbType.UnsignedSmallInt:
                    dbType=DbType.UInt16;
                    break;
                case System.Data.OleDb.OleDbType.UnsignedTinyInt:
                    dbType=DbType.Byte;
                    break;
                case System.Data.OleDb.OleDbType.VarBinary:
                    dbType=DbType.Binary;
                    break;
                case System.Data.OleDb.OleDbType.VarChar:
                    dbType=DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Variant:
                    dbType=DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.VarNumeric:
                    dbType=DbType.UInt64;
                    break;
                case System.Data.OleDb.OleDbType.VarWChar:
                    dbType=DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.WChar:
                    dbType=DbType.String;
                    break;


            }
            return  dbType;
        }

    }
}
