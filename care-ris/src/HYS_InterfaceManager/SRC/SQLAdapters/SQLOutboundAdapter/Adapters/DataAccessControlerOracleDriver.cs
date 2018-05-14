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
using Oracle.DataAccess.Client;

namespace SQLOutboundAdapter.Adapters
{
    // 20141010
    // Use Oracle driver connections to access 3rd party db to avoid too many concurrent TCP/IP connection.
    // To fix the issue that oledb can not run storeprocedure for oracle of x64 bit
    public partial class DataAccessControler
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        private bool WriteSPOracle(DataSet ds, SQLOutboundChanel ch)
        {

            // ------ 20070419 ------  
            string[] data_Id = new string[1];
            using (OracleConnection conn = new OracleConnection(
                   SQLOutAdapterConfigMgt.SQLOutAdapterConfig.ThirdPartyInteractConfig.ConnectionParameter.ConnectionStr))
            {
                Program.Log.Write("--- begin connecting with 3rd party db ---");
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        cmd.Parameters.Clear();
                        cmd.CommandText = ch.OperationName;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SetPamater(dr, cmd, ch);
                        cmd.ExecuteNonQuery();
                        data_Id[0] = Convert.ToString(dr["data_id"]);
                        if (OnDataDischarge != null) 
                            OnDataDischarge(data_Id);
                            
                        
                    }
                    catch (Exception Ex)
                    {
                        Program.Log.Write(Ex);
                        Program.Log.Write(LogType.Info, Program.db.ConnectionString);
                        if (!ch.IgnoreDBException) 
                            return false;
                    }
                }
            }
            return true;
         
        }


        private void SetPamater(DataRow dr, OracleCommand cmd, SQLOutboundChanel ch)
        {
            Program.Log.Write("---- sp parameter ----");

            foreach (SQLOutQueryResultItem item in ch.Rule.QueryResult.MappingList)
            {
                if (item.ThirdPartyDBPatamter.FieldName == null || item.ThirdPartyDBPatamter.FieldName.Length < 1) 
                    continue;



                OracleParameter parm = new OracleParameter();

                parm.ParameterName = "@" + item.TargetField;
                parm.DbType = OleDBType2DBType(item.ThirdPartyDBPatamter.FieldType);
                parm.Direction = ParameterDirection.Input;


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

 
                cmd.Parameters.Add(parm);
            }

            Program.Log.Write("---------------------");
        }

        

        private DbType OleDBType2DBType(System.Data.OleDb.OleDbType oleDBType)
        {
            DbType dbType = DbType.Object;
            switch (oleDBType)
            {
                case System.Data.OleDb.OleDbType.BigInt:
                    dbType = DbType.Int64;
                    break;
                case System.Data.OleDb.OleDbType.Binary:
                    dbType = DbType.Binary;
                    break;
                case System.Data.OleDb.OleDbType.Boolean:
                    dbType = DbType.Boolean;
                    break;
                case System.Data.OleDb.OleDbType.BSTR:
                    dbType = DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Char:
                    dbType = DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Currency:
                    dbType = DbType.Currency;
                    break;
                case System.Data.OleDb.OleDbType.Date:
                    dbType = DbType.Date;
                    break;
                case System.Data.OleDb.OleDbType.DBDate:
                    dbType = DbType.Date;
                    break;
                case System.Data.OleDb.OleDbType.DBTime:
                    dbType = DbType.Time;
                    break;
                case System.Data.OleDb.OleDbType.DBTimeStamp:
                    dbType = DbType.Date;
                    break;
                case System.Data.OleDb.OleDbType.Decimal:
                    dbType = DbType.Decimal;
                    break;
                case System.Data.OleDb.OleDbType.Double:
                    dbType = DbType.Double;
                    break;
                case System.Data.OleDb.OleDbType.Empty:
                    dbType = DbType.Object;
                    break;
                case System.Data.OleDb.OleDbType.Error:
                    dbType = DbType.Object;
                    break;
                case System.Data.OleDb.OleDbType.Filetime:
                    dbType = DbType.UInt64;
                    break;
                case System.Data.OleDb.OleDbType.Guid:
                    dbType = DbType.Guid;
                    break;
                case System.Data.OleDb.OleDbType.IDispatch:
                    dbType = DbType.Object;
                    break;
                case System.Data.OleDb.OleDbType.Integer:
                    dbType = DbType.Int64;
                    break;
                case System.Data.OleDb.OleDbType.IUnknown:
                    dbType = DbType.Object;
                    break;
                case System.Data.OleDb.OleDbType.LongVarBinary:
                    dbType = DbType.Binary;
                    break;
                case System.Data.OleDb.OleDbType.LongVarChar:
                    dbType = DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.LongVarWChar:
                    dbType = DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Numeric:
                    dbType = DbType.VarNumeric;
                    break;
                case System.Data.OleDb.OleDbType.PropVariant:
                    dbType = DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Single:
                    dbType = DbType.Double;
                    break;
                case System.Data.OleDb.OleDbType.SmallInt:
                    dbType = DbType.Int16;
                    break;
                case System.Data.OleDb.OleDbType.TinyInt:
                    dbType = DbType.Int16;
                    break;
                case System.Data.OleDb.OleDbType.UnsignedBigInt:
                    dbType = DbType.UInt64;
                    break;
                case System.Data.OleDb.OleDbType.UnsignedInt:
                    dbType = DbType.UInt64;
                    break;
                case System.Data.OleDb.OleDbType.UnsignedSmallInt:
                    dbType = DbType.UInt16;
                    break;
                case System.Data.OleDb.OleDbType.UnsignedTinyInt:
                    dbType = DbType.Byte;
                    break;
                case System.Data.OleDb.OleDbType.VarBinary:
                    dbType = DbType.Binary;
                    break;
                case System.Data.OleDb.OleDbType.VarChar:
                    dbType = DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.Variant:
                    dbType = DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.VarNumeric:
                    dbType = DbType.UInt64;
                    break;
                case System.Data.OleDb.OleDbType.VarWChar:
                    dbType = DbType.String;
                    break;
                case System.Data.OleDb.OleDbType.WChar:
                    dbType = DbType.String;
                    break;


            }
            return dbType;
        }

    }
}
