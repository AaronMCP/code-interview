using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    /// <summary>
    /// Report List Data Layer
    /// </summary>
    public class GetReportsListDAO
    {
        /// <summary>
        /// Execute method
        /// </summary>
        /// <param name="param">Filters map</param>
        /// <returns>Report list dataset</returns>
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

    /// <summary>
    /// for Abstract class
    /// </summary>
    internal class GetReportsListDAO_ABSTRACT : IReportDAO
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

    /// <summary>
    /// for Sybase
    /// </summary>
    internal class GetReportsListDAO_SYBASE : IReportDAO
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

    /// <summary>
    /// for MS SQL-Server
    /// </summary>
    internal class GetReportsListDAO_MSSQL : IReportDAO
    {
        #region private variable
        const string GRIDNAME_DEFAULT = "Reportlist";
        static int iWrittenCount = 0;
        static int _rwfMode = -1;

        static Dictionary<string, string> _mapSQLCol = null;
        #endregion

        public object Execute(object param)
        {
            try
            {
                #region Parse the parameters

                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    //throw (new Exception("No parameter in GetReportsListDAO!"));

                    ServerPubFun.RISLog_Info(0, "No parameter in GetReportsListDAO!", "", 0);
                }

                using (RisDAL dal = new RisDAL())
                {

                    string condition = "", userGuid = "", panelName = GRIDNAME_DEFAULT;
                    int nPagesize = 0, nCurpage = 0;
                    string strOrderBy = "";
                    bool bOfflineData = false;

                    foreach (string key in paramMap.Keys)
                    {
                        switch (key.ToUpper())
                        {
                            case "CONDITION":
                                {
                                    condition = paramMap[key] as string;

                                    if (condition == null)
                                        condition = "";
                                }
                                break;
                            case "PAGESIZE":
                                {
                                    nPagesize = System.Convert.ToInt32(paramMap[key] as string);
                                }
                                break;
                            case "CURPAGE":
                                {
                                    nCurpage = System.Convert.ToInt32(paramMap[key] as string);
                                }
                                break;
                            case "USERID":
                                {
                                    userGuid = paramMap[key] as string;
                                }
                                break;
                            case "PANELNAME":
                                {
                                    panelName = paramMap[key] as string;
                                }
                                break;
                            case "SORTING":
                                {
                                    strOrderBy = paramMap[key] as string;
                                }
                                break;
                            case "OFFLINEDATA":
                                {
                                    bOfflineData = (System.Convert.ToString(paramMap[key]) == "1");
                                }
                                break;
                        }
                    }

                    condition = condition == null ? "" : condition;
                    userGuid = userGuid == null ? "" : userGuid;
                    panelName = panelName == null ? "" : panelName;
                    nPagesize = nPagesize < 1 ? ReportCommon.ReportCommon.DEFAULT_PAGESIZE : nPagesize;
                    nCurpage = nCurpage < 1 ? 0 : nCurpage;

                #endregion

                    //#region RWF

                    //if (_rwfMode == -1)
                    //    _rwfMode = ServerPubFun.GetSystemProfile_Int("RWFmode");

                    //if (_rwfMode == 1)
                    //{
                    //    CS.GCRIS.RWF.SCU.RwfScu.queryWorklist(condition);

                    //    // ONLY for RWF test
                    //    condition = CS.GCRIS.RWF.SCU.RwfScu.composeSQLfromConditionMap(CS.GCRIS.RWF.SCU.RwfScu.parserCondition(condition));
                    //}

                    //#endregion

                    #region Compose SQL sentense

                    string sqlcol = GetSqlCol(panelName, userGuid).Trim();
                    if (!sqlcol.Contains("tRegProcedure.Status as RPStatus"))
                    {
                        sqlcol += ",tRegProcedure.Status as RPStatus";
                    }
                    if (!sqlcol.Contains("tRegProcedure.Optional3 as IsLocked"))
                    {
                        sqlcol += ",tRegProcedure.Optional3 as IsLocked";
                    }
                    if (!sqlcol.Contains("tRegProcedure.ExamSystem as tRegProcedure__ExamSystem"))
                    {
                        sqlcol += ",tRegProcedure.ExamSystem as tRegProcedure__ExamSystem";
                    }
                    if (!sqlcol.Contains("convert(varchar(max),tRegOrder.OrderMessage) as OrderMessageXml "))
                    {
                        sqlcol += ",convert(varchar(max),tRegOrder.OrderMessage) as OrderMessageXml ";
                    }
                    if (!sqlcol.Contains("tReport.CreaterName as tReport__CreaterName"))
                    {
                        sqlcol += ",tReport.CreaterName as tReport__CreaterName";
                    }
                    if (!sqlcol.Contains("tReport.SubmitterName as tReport__SubmitterName"))
                    {
                        sqlcol += ",tReport.SubmitterName as tReport__SubmitterName";
                    }
                    if (!sqlcol.Contains("tReport.FirstapproverName as tReport__FirstapproverName"))
                    {
                        sqlcol += ",tReport.FirstapproverName as tReport__FirstapproverName";
                    }
                     if (!sqlcol.Contains("tRegOrder.FilmDrawDept as tRegOrder__FilmDrawDept"))
                    {
                        sqlcol += ",tRegOrder.FilmDrawDept as tRegOrder__FilmDrawDept";
                    }
                       if (!sqlcol.Contains("tRegOrder.FilmDrawRegion as tRegOrder__FilmDrawRegion"))
                    {
                        sqlcol += ",tRegOrder.FilmDrawRegion as tRegOrder__FilmDrawRegion";
                    }
                       if (!sqlcol.Contains("tRegOrder.FilmDrawComment as tRegOrder__FilmDrawComment"))
                    {
                        sqlcol += ",tRegOrder.FilmDrawComment as tRegOrder__FilmDrawComment";
                    }
                       if (!sqlcol.Contains("tRegOrder.FilmDrawerSign as tRegOrder__FilmDrawerSign"))
                    {
                        sqlcol += ",tRegOrder.FilmDrawerSign as tRegOrder__FilmDrawerSign";
                    }
                       if (!sqlcol.Contains("tReport.TakeFilmDept as tReport__TakeFilmDept"))
                    {
                        sqlcol += ",tReport.TakeFilmDept as tReport__TakeFilmDept";
                    }
                       if (!sqlcol.Contains("tReport.TakeFilmRegion as tReport__TakeFilmRegion"))
                    {
                        sqlcol += ",tReport.TakeFilmRegion as tReport__TakeFilmRegion";
                    }
                       if (!sqlcol.Contains("tReport.TakeFilmComment as tReport__TakeFilmComment"))
                    {
                        sqlcol += ",tReport.TakeFilmComment as tReport__TakeFilmComment ";
                    }
                    
                    sqlcol = sqlcol.Length > 0 ? sqlcol : "No Grid Column Setting!";

                    // 2015-11-06, Oscar removed (US28272)
                    //string plainColumns = sqlcol;

                    //plainColumns = removeBrackets(plainColumns);
                    //plainColumns = plainColumns.Replace(" AS ", " as ");
                    //int asTmp0 = 0;
                    //while ((asTmp0 = plainColumns.IndexOf(" as ")) > 0)
                    //{
                    //    int comma0 = plainColumns.LastIndexOf(',', asTmp0);

                    //    if (comma0 < 0)
                    //        plainColumns = " " + plainColumns.Substring(asTmp0 + 4);
                    //    else
                    //        plainColumns = plainColumns.Substring(0, comma0 + 1) + " " + plainColumns.Substring(asTmp0 + 4);
                    //}

                    //while ((asTmp0 = plainColumns.IndexOf(".")) > 0)
                    //{
                    //    int comma0 = plainColumns.LastIndexOf(',', asTmp0);

                    //    if (comma0 < 0)
                    //        plainColumns = " " + plainColumns.Substring(asTmp0 + 1);
                    //    else
                    //        plainColumns = plainColumns.Substring(0, comma0 + 1) + " " + plainColumns.Substring(asTmp0 + 1);
                    //}

                    string sql = "";

                    string strWhere = " from tRegPatient with (nolock), tRegOrder with (nolock), tRegProcedure with (nolock) \r\n"
                             + " left join tReport with (nolock) on tRegProcedure.reportGuid = tReport.reportGuid \r\n"
                             + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid \r\n"
                             + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid \r\n"
                             + " and tRegProcedure.status >= " + System.Convert.ToInt32(ReportCommon.RP_Status.Examination);

                    string strWhereforArchive = " from RISArchive..tRegPatient tRegPatient with (nolock), RISArchive..tRegOrder tRegOrder with (nolock), RISArchive..tRegProcedure tRegProcedure with (nolock) \r\n"
                             + " left join RISArchive..tReport tReport with (nolock) on tRegProcedure.reportGuid = tReport.reportGuid \r\n"
                             + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid \r\n"
                             + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid \r\n"
                             + " and tRegProcedure.status >= " + System.Convert.ToInt32(ReportCommon.RP_Status.Examination);

                    if (condition.Length > 0)
                    {
                        while ((condition = condition.Trim()).Length > 0 && condition.ToUpper().EndsWith("AND"))
                        {
                            condition = condition.Substring(0, condition.Length - 3);
                        }

                        strWhere += " and " + condition;
                        strWhereforArchive += " and " + condition;
                    }
                    if (strWhere.Length > 4096)
                    {
                        throw new Exception("The query conditions is too long!");
                    }

                    #endregion

                    #region Execute Query

                    DataSet ds = new DataSet();

                    //DataSet tmp = new DataSet();

                    if (0 == iWrittenCount++ % 100)
                    {
                        ServerPubFun.RISLog_Info(0, "GetReportsListDAO_MSSQL, curPage=" + nCurpage.ToString() + ", SQL=" + sql, "", 0);
                    }
                    else
                    {
                        ServerPubFun.RISLog_Info(0, "GetReportsListDAO_MSSQL, curPage=" + nCurpage.ToString() + ", condition=" + condition + ", iWrittenCount=" + iWrittenCount.ToString(), "", 0);
                    }

                    //{Bruce Deng 20071128
                    //dal.ExecuteQuery(sql, tmp, "ReportsList");
                    dal.Parameters.Clear();
                    dal.Parameters.AddInt("@PageIndex", nCurpage);
                    dal.Parameters.AddInt("@PageSize", nPagesize);
                    dal.Parameters.AddVarChar("@Columns", sqlcol, 8000);
                    dal.Parameters.AddVarChar("@Where", strWhere, 8000);
                    dal.Parameters.AddVarChar("@WhereArchive", bOfflineData ? strWhereforArchive : "", 8000);
                    //dal.Parameters.AddVarChar("@plainColumns", plainColumns, 8000);
                    dal.Parameters.AddVarChar("@OrderBy", bOfflineData ? strOrderBy.Replace(".", "__") : strOrderBy, 8000);
                    int nTotalCount = 0;
                    dal.Parameters.AddInt("@TotalCount", nTotalCount, ParameterDirection.Output);
                    DataTable dt1 = new DataTable();
                    dal.ExecuteQuerySP("SP_REPORT_PAGE", dt1);
                    dt1.TableName = "ReportList";

                    if (dal.Parameters["@TotalCount"].Value != null)
                    {
                        nTotalCount = Convert.ToInt32(dal.Parameters["@TotalCount"].Value);
                    }
                    //}

                    if (-1 == nTotalCount)
                    {
                        nTotalCount = dt1.Rows.Count;
                    }

                    if (dt1 != null)
                    {
                        ////DataTable dt = tmp.Tables[0].Clone();
                        //DataTable dt = new DataTable();
                        //dt.TableName = "ReportPage";
                        //foreach (DataColumn dc in dt1.Columns)
                        //{
                        //    dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                        //}

                        //if (dt1.Rows.Count > 0)
                        //{
                        //    if (dt1.Rows.Count <= (Int64)nCurpage * nPagesize)
                        //    {
                        //        int ipage = (dt1.Rows.Count - 1) / nPagesize;
                        //        for (int i = ipage * nPagesize; i < dt1.Rows.Count; i++)
                        //        {
                        //            DataRow dr = dt.NewRow();

                        //            foreach (DataColumn col in dt.Columns)
                        //            {
                        //                dr[col.ColumnName] = dt1.Rows[i][col.ColumnName];
                        //            }

                        //            dt.Rows.Add(dr);
                        //        }

                        //        nCurpage = ipage;
                        //    }
                        //    else
                        //    {
                        //        for (int i = nCurpage * nPagesize; i < (nCurpage + 1) * nPagesize && i < dt1.Rows.Count; i++)
                        //        {
                        //            DataRow dr = dt.NewRow();

                        //            foreach (DataColumn col in dt.Columns)
                        //            {
                        //                dr[col.ColumnName] = dt1.Rows[i][col.ColumnName];
                        //            }

                        //            dt.Rows.Add(dr);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    nCurpage = 0;
                        //}

                        //ds.Tables.Add(dt);
                        //ds.Tables.Add(dt1);

                        //if (dt1.Rows.Count > 0)
                        //{
                        //    if (dt1.Rows.Count <= (Int64)nCurpage * nPagesize)
                        //    {
                        //        int ipage = (dt1.Rows.Count - 1) / nPagesize;

                        //        nCurpage = ipage;
                        //    }
                        //}
                        //else
                        //{
                        //    nCurpage = 0;
                        //}

                        //StringBuilder sb = new StringBuilder();
                        //sb.AppendFormat("rownum > {0} AND rownum <= {1}", nCurpage * nPagesize, (nCurpage + 1) * nPagesize);

                        //dt1.DefaultView.RowFilter = sb.ToString();
                        //DataTable dt = dt1.DefaultView.ToTable("ReportPage");
                        DataTable dt = dt1.Copy();//.DefaultView.ToTable("ReportPage");
                        dt.TableName = "ReportPage";

                        ds.Tables.Add(dt);
                        ds.Tables.Add(dt1);
                    }


                    #endregion


                    Dictionary<string, object> outMap = new Dictionary<string, object>();

                    outMap.Add("DataSet", ds);
                    outMap.Add("CurPage", nCurpage);
                    outMap.Add("TotalCount", nTotalCount);

                    return outMap;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportsListDAO_MSSQL, MSG=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                throw new Exception(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Get the user-defined grid column
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="userGuid"></param>
        /// <returns>
        ///     String like "TableName0.Column0 as TableName0__Column0, TableName1.Column1 as TableName1__Column1"
        /// </returns>
        private string GetSqlCol(string gridName, string userGuid)
        {
            try
            {
                if (_mapSQLCol == null)
                {
                    _mapSQLCol = new Dictionary<string, string>();
                }

                string key = gridName.ToUpper().Trim() + "," + userGuid.ToUpper().Trim();
                if (_mapSQLCol.ContainsKey(key))
                {
                    return _mapSQLCol[key];
                }

                GetGridColumnDAO ggc = new GetGridColumnDAO();

                DataSet ds = ggc.GetGridCol(gridName, userGuid);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string sqlCol = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        string expression = dr["Expression"] as string;
                        string tablename = dr["TableName"] as string;
                        string columnname = dr["ColumnName"] as string;

                        // expression & #alias#
                        if (expression != null && (expression = expression.Trim()).Length > 0)
                        {
                            expression = expression.Replace("#alias#", tablename + ReportCommon.ReportCommon.FIELD_SEPERATOR + columnname);

                            sqlCol += expression + ", ";
                        }
                        else
                        {
                            sqlCol += tablename + "." + columnname + " as "
                                + tablename + ReportCommon.ReportCommon.FIELD_SEPERATOR + columnname + ", ";
                        }
                    }

                    sqlCol = sqlCol.Trim(", ".ToCharArray());

                    if (!_mapSQLCol.ContainsKey(key))
                    {
                        _mapSQLCol.Add(key, sqlCol);
                    }

                    return sqlCol;
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false, "Can't get the Grid Columns!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetSqlCol, MSG=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "'No Columns Setting'";
        }

        private string removeBrackets(string src)
        {
            int count = 0;
            int m0 = 0;
            while ((m0 = src.IndexOf('(')) >= 0)
            {
                int m1 = 0;
                for (m1 = m0; m1 < src.Length; ++m1)
                {
                    if (src[m1] == '(')
                        ++count;
                    else if (src[m1] == ')')
                    {
                        --count;

                        if (count == 0)
                        {
                            break;
                        }
                    }
                }

                src = src.Substring(0, m0) + " " + src.Substring(m1 + 1);
            }

            return src;
        }
    }

    /// <summary>
    /// for Oracle
    /// </summary>
    internal class GetReportsListDAO_ORACLE : IReportDAO
    {
        #region private variable
        const string GRIDNAME_DEFAULT = "Reportlist";
        #endregion

        public object Execute(object param)
        {
            try
            {
                #region Parse the parameters

                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    //throw (new Exception("No parameter in GetReportsListDAO!"));

                    ServerPubFun.RISLog_Info(0, "No parameter in GetReportsListDAO!", "", 0);
                }

                using (RisDAL dal = new RisDAL())
                {

                    string condition = "", userGuid = "", panelName = GRIDNAME_DEFAULT;
                    int nPagesize = 0, nCurpage = 0;

                    foreach (string key in paramMap.Keys)
                    {
                        if (key.ToUpper() == "CONDITION")
                        {
                            condition = paramMap[key] as string;

                            if (condition == null)
                                condition = "";
                        }
                        else if (key.ToUpper() == "PAGESIZE")
                        {
                            nPagesize = System.Convert.ToInt32(paramMap[key] as string);
                        }
                        else if (key.ToUpper() == "CURPAGE")
                        {
                            nCurpage = System.Convert.ToInt32(paramMap[key] as string);
                        }
                        else if (key.ToUpper() == "USERID")
                        {
                            userGuid = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "PANELNAME")
                        {
                            panelName = paramMap[key] as string;
                        }
                    }

                    condition = condition == null ? "" : condition;
                    userGuid = userGuid == null ? "" : userGuid;
                    panelName = panelName == null ? "" : panelName;
                    nPagesize = nPagesize < 1 ? ReportCommon.ReportCommon.DEFAULT_PAGESIZE : nPagesize;
                    nCurpage = nCurpage < 1 ? 0 : nCurpage;

                #endregion

                    #region Compose SQL sentense

                    string sqlcol = GetSqlCol(panelName, userGuid).Trim();
                    sqlcol = sqlcol.Length > 0 ? sqlcol : "No Grid Column Setting!";

                    string sql = "";

                    if (nPagesize * (nCurpage + 1) >= 5000 || nPagesize * (nCurpage + 1) < 0)
                    {
                        sql += "select " + sqlcol + ",tRegProcedure.Status as RPStatus,tRegProcedure.Optional3 as IsLocked  \r\n"
                             + " from tRegPatient, tRegOrder, tRegProcedure \r\n"
                             + " left join tReport on tRegProcedure.reportGuid = tReport.reportGuid \r\n"
                             + " , tProcedureCode"
                             + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid \r\n"
                             + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid \r\n"
                             + " and tRegProcedure.ProcedureCode = tProcedureCode.ProcedureCode \r\n"
                             + " and tRegProcedure.status >= " + System.Convert.ToInt32(ReportCommon.RP_Status.Examination);
                    }
                    else
                    {
                        sql += "select " + sqlcol + ",tRegProcedure.Status as RPStatus,tRegProcedure.Optional3 as IsLocked  \r\n"
                             + " from tRegPatient, tRegOrder, tRegProcedure \r\n"
                             + " left join tReport on tRegProcedure.reportGuid = tReport.reportGuid \r\n"
                             + " , tProcedureCode"
                             + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid \r\n"
                             + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid \r\n"
                             + " and tRegProcedure.ProcedureCode = tProcedureCode.ProcedureCode \r\n"
                             + " and rownum < " + (nPagesize * (nCurpage + 1)).ToString()
                             + " and tRegProcedure.status >= " + System.Convert.ToInt32(ReportCommon.RP_Status.Examination);
                    }

                    if (condition.Length > 0)
                    {
                        while ((condition = condition.Trim()).Length > 0 && condition.ToUpper().EndsWith("AND"))
                        {
                            condition = condition.Substring(0, condition.Length - 3);
                        }

                        sql += " and " + getOracleDateString(condition);
                    }

                    #endregion

                    #region Execute Query

                    DataSet ds = new DataSet();

                    //if (nCurpage < 10)
                    //{
                    //    sql = " select * from (" + sql + ") _ReportA where _rowindex between " + (nCurpage * nPagesize + 1).ToString() + " and " + ((nCurpage + 1) * nPagesize).ToString();

                    //    ReportCommon.ReportCommon.WriteDebugLog("CurPage=" + nCurpage.ToString() + "\r\n" + sql);

                    //    dal.ExecuteQuery(sql, ds, "ReportsList");
                    //}
                    //else
                    {
                        DataSet tmp = new DataSet();



                        dal.ExecuteQuery(sql, tmp, "ReportsList");

                        if (tmp != null && tmp.Tables.Count > 0)
                        {
                            //DataTable dt = tmp.Tables[0].Clone();
                            DataTable dt = new DataTable();

                            foreach (DataColumn dc in tmp.Tables[0].Columns)
                            {
                                dt.Columns.Add(dc.ColumnName, dc.DataType, dc.Expression);
                            }

                            if (tmp.Tables[0].Rows.Count > 0)
                            {
                                if (tmp.Tables[0].Rows.Count <= (Int64)nCurpage * nPagesize)
                                {
                                    int ipage = (tmp.Tables[0].Rows.Count - 1) / nPagesize;
                                    for (int i = ipage * nPagesize; i < tmp.Tables[0].Rows.Count; i++)
                                    {
                                        DataRow dr = dt.NewRow();

                                        foreach (DataColumn col in dt.Columns)
                                        {
                                            dr[col.ColumnName] = tmp.Tables[0].Rows[i][col.ColumnName];
                                        }

                                        dt.Rows.Add(dr);
                                    }

                                    nCurpage = ipage;
                                }
                                else
                                {
                                    for (int i = nCurpage * nPagesize; i < (nCurpage + 1) * nPagesize && i < tmp.Tables[0].Rows.Count; i++)
                                    {
                                        DataRow dr = dt.NewRow();

                                        foreach (DataColumn col in dt.Columns)
                                        {
                                            dr[col.ColumnName] = tmp.Tables[0].Rows[i][col.ColumnName];
                                        }

                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                            else
                            {
                                nCurpage = 0;
                            }

                            ds.Tables.Add(dt);
                        }
                    }

                    #endregion



                    Dictionary<string, object> outMap = new Dictionary<string, object>();

                    outMap.Add("DataSet", ds);
                    outMap.Add("CurPage", nCurpage);

                    return outMap;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportsListDAO_ORACLE=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }

        /// <summary>
        /// Get the user-defined grid column
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="userGuid"></param>
        /// <returns>
        ///     String like "TableName0.Column0 as TableName0__Column0, TableName1.Column1 as TableName1__Column1"
        /// </returns>
        private string GetSqlCol(string gridName, string userGuid)
        {
            try
            {
                GetGridColumnDAO ggc = new GetGridColumnDAO();

                DataSet ds = ggc.GetGridCol(gridName, userGuid);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    string sqlCol = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        string expression = dr["Expression"] as string;
                        string tablename = dr["TableName"] as string;
                        string columnname = dr["ColumnName"] as string;

                        // expression & #alias#
                        if (expression != null && (expression = expression.Trim()).Length > 0)
                        {
                            expression = expression.Replace("#alias#", tablename + ReportCommon.ReportCommon.FIELD_SEPERATOR + columnname);

                            sqlCol += expression + ", ";
                        }
                        else
                        {
                            sqlCol += tablename + "." + columnname + " as "
                                + tablename + ReportCommon.ReportCommon.FIELD_SEPERATOR + columnname + ", ";
                        }
                    }

                    sqlCol = sqlCol.Trim(", ".ToCharArray());

                    return sqlCol;
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false, "Can't get the Grid Columns!");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportsListDAO_ORACLE=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "'No Columns Setting'";
        }

        /// <summary>
        /// getOracleDateString
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public string getOracleDateString(string src)
        {
            string p = src.ToUpper();
            int ibtw = 0;
            ibtw = p.IndexOf("BETWEEN");

            if (ibtw > 0)
            {
                int i1 = src.IndexOf(" '", ibtw);
                int i2 = src.IndexOf("' ", ibtw);
                int i3 = src.IndexOf(" '", i1 + 2);
                int i4 = src.IndexOf("' ", i2 + 2);

                if (i1 > 0 && i2 > i1 && i3 > i2 && i4 > i3)
                {
                    string s1 = src.Substring(0, i1);
                    string s2 = src.Substring(i1, i2 - i1 + 1);
                    //string s3 = src.Substring(i2, i3 - i2);
                    string s4 = src.Substring(i3, i4 - i3 + 1);
                    string s5 = src.Substring(i4 + 2);

                    return s1 + " to_date(" + s2 + ", 'yyyy-mm-dd HH24:MI:SS') and " + " to_date(" + s4 + ", 'yyyy-mm-dd HH24:MI:SS') " + s5;
                }
                else if (i3 > 0 && i4 < 1)
                {
                    string s1 = src.Substring(0, i1);
                    string s2 = src.Substring(i1, i2 - i1 + 1);
                    string s4 = src.Substring(i3);

                    return s1 + " to_date(" + s2 + ", 'yyyy-mm-dd HH24:MI:SS') and " + " to_date(" + s4 + ", 'yyyy-mm-dd HH24:MI:SS') ";
                }
            }

            return src;
        }
    }
}
