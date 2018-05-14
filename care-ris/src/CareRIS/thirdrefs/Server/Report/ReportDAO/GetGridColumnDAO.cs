using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    /// <summary>
    /// Get User-defined Grid Column setting
    /// </summary>
    public class GetGridColumnDAO
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

        /// <summary>
        /// GetGridCol
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="userGuid"></param>
        /// <returns>DataSet contains columns' info</returns>
        public DataSet GetGridCol(string gridName, string userGuid)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());

                Type type = Type.GetType(clsType);
                IReportGridColumn iGridCol = Activator.CreateInstance(type) as IReportGridColumn;
                return iGridCol.GetGridCol(gridName, userGuid);
            }
        }
    }

    /// <summary>
    /// for abstract
    /// </summary>
    internal class GetGridColumnDAO_ABSTRACT : IReportDAO, IReportGridColumn
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }

        public DataSet GetGridCol(string gridName, string userGuid)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportGridColumn iGridCol = Activator.CreateInstance(type) as IReportGridColumn;
            return iGridCol.GetGridCol(gridName, userGuid);
        }
    }

    /// <summary>
    /// for sybase
    /// </summary>
    internal class GetGridColumnDAO_SYBASE : IReportDAO, IReportGridColumn
    {
        public object Execute(object param)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
            return iRptDAO.Execute(param);
        }

        public DataSet GetGridCol(string gridName, string userGuid)
        {
            string clsType = string.Format("{0}_MSSQL",
                ReportCommon.ReportCommon.GetReportDAO_ImplementClass_PrefixName(this.GetType()));

            Type type = Type.GetType(clsType);
            IReportGridColumn iGridCol = Activator.CreateInstance(type) as IReportGridColumn;
            return iGridCol.GetGridCol(gridName, userGuid);
        }
    }

    /// <summary>
    /// for MS SQL-Server
    /// </summary>
    internal class GetGridColumnDAO_MSSQL : IReportDAO, IReportGridColumn
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

                string gridName = "", userGuid = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "GRIDNAME")
                    {
                        gridName = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERGUID" || key.ToUpper() == "USERID")
                    {
                        userGuid = inMap[key] as string;
                    }
                }

                return GetGridCol(gridName, userGuid);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetGridColumnDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }

        public DataSet GetGridCol(string gridName, string userGuid)
        {
            try
            {
                gridName = gridName == null ? "" : gridName.Trim();
                userGuid = userGuid == null ? "" : userGuid.Trim();

                if (gridName.Length > 0)
                {
                    /*
                    string sql = "if exists(select 1 from tGridColumn, tGridColumnOption"
                        + " where tGridColumn.Guid = tGridColumnOption.Guid"
                        + " and tGridColumnOption.listname = '" + gridName + "' and tGridColumn.UserGuid = '" + userGuid + "') \r\n"
                        + "  select * from tGridColumn, tGridColumnOption"
                        + "  where tGridColumn.Guid = tGridColumnOption.Guid"
                        + "  and tGridColumnOption.listname = '" + gridName + "'"
                        + "  and tGridColumn.UserGuid = '" + userGuid + "' order by tGridColumn.OrderID \r\n"
                        + " else if exists(select 1 from tGridColumn, tGridColumnOption"
                        + " where tGridColumn.Guid = tGridColumnOption.Guid"
                        + " and tGridColumnOption.listname = '" + gridName + "' and tGridColumn.UserGuid = '') \r\n"
                        + "  select * from tGridColumn, tGridColumnOption"
                        + "  where tGridColumn.Guid = tGridColumnOption.Guid"
                        + "  and tGridColumnOption.listname = '" + gridName + "'"
                        + "  and tGridColumn.UserGuid = '' order by tGridColumn.OrderID \r\n"
                        + " else \r\n"
                        + "  select * from tGridColumnOption"
                        + "  left join tGridColumn on tGridColumn.Guid = tGridColumnOption.Guid"
                        + "	 and tGridColumn.UserGuid = ''"
                        + "  where tGridColumnOption.listname = '" + gridName + "' order by tGridColumnOption.OrderID";
                    */
                    string sql = string.Format("select tGridColumn.Guid,tGridColumn.ColumnWidth,tGridColumn.IsHidden,tGridColumnOption.ColumnID," +
                            " tGridColumnOption.TableName,tGridColumnOption.ColumnName,tGridColumnOption.Expression,"+
                            " tGridColumnOption.ModuleID, tGridColumnOption.isImageColumn, tGridColumnOption.ImagePath"+
                            " from tGridColumn left join tGridColumnOption on " +
                            " tGridColumn.Guid=tGridColumnOption.Guid where "+
                            " tGridColumn.UserGuid='' AND tGridColumnOption.ListName='{0}' "+
                            " order by tGridColumn.OrderID ", gridName);

                    

                    DataSet ds = new DataSet();

                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteQuery(sql, ds, "GridColumn");

                        return ds;
                    }
                }
                else
                {
                    throw (new Exception("Missing Parameter!"));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetGridColumnDAO_MSSQL, MSG="+ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }

    /// <summary>
    /// for Oracle
    /// </summary>
    internal class GetGridColumnDAO_ORACLE : IReportDAO, IReportGridColumn
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

                string gridName = "", userGuid = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "GRIDNAME")
                    {
                        gridName = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERGUID" || key.ToUpper() == "USERID")
                    {
                        userGuid = inMap[key] as string;
                    }
                }

                return GetGridCol(gridName, userGuid);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);
                
                ServerPubFun.RISLog_Error(0, "GetGridColumnDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }

        public DataSet GetGridCol(string gridName, string userGuid)
        {
            try
            {
                gridName = gridName == null ? "" : gridName.Trim();
                userGuid = userGuid == null ? "" : userGuid.Trim();

                if (gridName.Length > 0)
                {
                    string sql = " select * from tGridColumnOption"
                        + " left join tGridColumn on tGridColumn.Guid = tGridColumnOption.Guid"
                        + "	and tGridColumn.UserGuid is null"
                        + " where tGridColumnOption.listname = '" + gridName + "'"
                        + " order by tGridColumnOption.OrderID";

                    

                    DataSet ds = new DataSet();

                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteQuery(sql, ds, "GridColumn");

                        return ds;
                    }
                }
                else
                {
                    throw (new Exception("Missing Parameter!"));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetGridColumnDAO_ORACLE, MSG=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }
}
