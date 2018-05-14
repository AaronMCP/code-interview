using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetConditionColumnDAO
    {
        public object Execute(object param)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());

                Type type = Type.GetType(clsType);
                IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
                return iRptDAO.Execute(param);
            }
        }
    }

    internal class GetConditionColumnDAO_ABSTRACT : IReportDAO
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }

    internal class GetConditionColumnDAO_MSSQL : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter!"));
                }

                string conditionName = "", userGuid = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "CONDITIONNAME")
                    {
                        conditionName = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERGUID" || key.ToUpper() == "USERID")
                    {
                        userGuid = inMap[key] as string;
                    }
                }

                /*string sql = "select *,"
                    + " (select syscolumns.xtype from sysobjects, syscolumns"
                    + "     where syscolumns.id=sysobjects.id and sysobjects.xtype='U'"
                    + "     and sysobjects.name=tablename and syscolumns.name=columnname) as fieldType"
                    + " from tConditionColumn where conditionName = '" + conditionName + "' and IsHidden = 0 order by orderID";
                 */
                string sql = "select * from tConditionColumn where conditionName = '" + conditionName + "' and IsHidden = 0 order by orderID";


                DataSet ds = new DataSet();

                using (RisDAL dal = new RisDAL())
                {

                    DataTable dt = new DataTable("ConditionColumn");

                    dal.ExecuteQuery(sql, dt);

                    ds.Tables.Add(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        int itemID = System.Convert.ToInt32(dr["itemID"]);
                        int dataType = dr["DataType"] is DBNull ? 0 : System.Convert.ToInt32(dr["DataType"]);
                        string datasrc = dr["datasource"] as string;

                        if (datasrc == null || (datasrc = datasrc.Trim()).Length < 1 || ds.Tables.Contains(itemID.ToString()))
                            continue;
                        if (dataType == System.Convert.ToInt32(ReportCommon.ConditionColumn_DataType.Query) || dataType == System.Convert.ToInt32(ReportCommon.ConditionColumn_DataType.Dictionary))
                        {
                            sql = datasrc;

                            DataTable dt0 = new DataTable(itemID.ToString());

                            dal.ExecuteQuery(sql, dt0);

                            ds.Tables.Add(dt0);
                        }
                    }

                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetConditionColumnDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }

    internal class GetConditionColumnDAO_SYBASE : IReportDAO
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }
    }

    internal class GetConditionColumnDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter!"));
                }

                string conditionName = "", userGuid = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "CONDITIONNAME")
                    {
                        conditionName = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERGUID" || key.ToUpper() == "USERID")
                    {
                        userGuid = inMap[key] as string;
                    }
                }

                //ServerPubFun.DeclareOracleNoCase();

                string sql = "select tConditionColumn.*,"
                    + " (select case "
                    + "     when data_type = 'BLOB' then 34 "
                    + "     when data_type = 'CLOB' then 35 "
                    + "     when data_type = 'DATE' then 56 "
                    + "     when data_type='NUMBER' then 61 "
                    + "     when instr(data_type, 'CHAR') > 0 then 167 "
                    + "     else 0 end "
                    + " from user_tab_cols "
                    + " where table_name = tConditionColumn.tablename"
                    + " and column_name = tConditionColumn.Columnname) as fieldType"
                    + " from tConditionColumn where conditionName = '" + conditionName + "' and IsHidden=0  order by orderID";

                DataSet ds = new DataSet();

                using (RisDAL dal = new RisDAL())
                {

                    DataTable dt = new DataTable("ConditionColumn");

                    dal.ExecuteQuery(sql, dt);

                    ds.Tables.Add(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        int itemID = System.Convert.ToInt32(dr["itemID"]);
                        int dataType = dr["DataType"] is DBNull ? 0 : System.Convert.ToInt32(dr["DataType"]);
                        string datasrc = dr["datasource"] as string;

                        if (datasrc == null || (datasrc = datasrc.Trim()).Length < 1)
                            continue;


                        if (dataType == System.Convert.ToInt32(ReportCommon.ConditionColumn_DataType.Query) || dataType == System.Convert.ToInt32(ReportCommon.ConditionColumn_DataType.Dictionary))
                        {
                            sql = datasrc;

                            DataTable dt0 = new DataTable(itemID.ToString());

                            dal.ExecuteQuery(sql, dt0);

                            ds.Tables.Add(dt0);
                        }
                    }

                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetConditionColumn=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }
}
