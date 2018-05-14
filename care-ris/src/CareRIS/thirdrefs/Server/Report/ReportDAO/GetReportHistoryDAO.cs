using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetReportHistoryDAO
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

    internal class GetReportHistoryDAO_ABSTRACT : IReportDAO
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

    internal class GetReportHistoryDAO_SYBASE : IReportDAO
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

    internal class GetReportHistoryDAO_MSSQL : IReportDAO
    {
        static int iWrittenCount = 0;

        public object Execute(object param)
        {
            
            Dictionary<string, object> outMap = new Dictionary<string, object>();
            try
            {
                using (RisDAL dal = new RisDAL())
                {
                    #region Parse the parameters

                    Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                    if (paramMap == null || paramMap.Count < 1)
                    {
                        throw (new Exception("No parameter in GetReportHistoryDAO!"));
                    }

                string patientID = "", reportGuid = "", strGlobalID = "", strDBCenter = "0", strisSR = "";
                string strHasReportHistory = null;
                string szStartReportStatus = string.Empty;
                bool bOfflineData = false;

                    foreach (string key in paramMap.Keys)
                    {
                        switch (key.ToUpper())
                        {
                            case "PATIENTGUID":
                            case "PATIENTID":
                                {
                                    patientID = paramMap[key] as string;

                                    if (patientID == null)
                                        patientID = "";
                                }
                                break;
                            case "REPORTGUID":
                            case "REPORTID":
                                {
                                    reportGuid = paramMap[key] as string;

                                if (reportGuid == null)
                                    reportGuid = "";
                            }
                            break;
                        case "HASREPORTHISTORY":
                            {
                                strHasReportHistory = paramMap[key] as string;
                            }
                            break;
                        case "GLOBALID":
                            {
                                strGlobalID = paramMap[key] as string;
                            }
                            break;
                        case "DBCENTER":
                            {
                                strDBCenter = paramMap[key] as string;
                            }
                            break;
                        case "STARTREPORTSTATUS":
                            {
                                szStartReportStatus = paramMap[key] as string;
                            }
                            break;
                        case "OFFLINEDATA":
                            {
                                bOfflineData = (System.Convert.ToString(paramMap[key]) == "1");
                            }
                            break;
                        case "ISSR":
                            {
                                strisSR = paramMap[key] as string;
                            }
                            break;
                    }
                }

                    if (string.IsNullOrEmpty(szStartReportStatus))
                    {
                        szStartReportStatus = System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove).ToString();
                    }

                    bool bRelatedPatient = ServerPubFun.GetSystemProfile_Bool("RelatePatient", ReportCommon.ModuleID.Global);

                    #endregion

                    if (strHasReportHistory != null)
                    #region Check whether history reports exist
                    {
                        // We check it using the procedure guid.
                        // Actually clients use the reportGuid to transmit the rpGuids
                        string[] rpGuids = reportGuid.Split(",".ToCharArray());
                        string strExpression = "";
                        foreach (string rpguid in rpGuids)
                        {
                            if (string.IsNullOrEmpty(rpguid))
                                continue;

                            string tmpRP = rpguid.Trim(",;|. ".ToCharArray());

                            if (string.IsNullOrEmpty(tmpRP))
                                continue;

                            strExpression += "'" + tmpRP + "',";
                        }

                        if (!string.IsNullOrEmpty(strExpression))
                        {
                            strExpression = strExpression.TrimEnd(new char[] { ',' });
                            strExpression = string.Format(" AND tRegProcedure.ProcedureGuid not in ({0})", strExpression);
                        }

                        string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n"
                            + " select tRegProcedure.reportGuid, tRegProcedure.ProcedureGuid "
                            + " from tRegPatient with (nolock), tRegOrder with (nolock), tRegProcedure with (nolock) "
                            + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"
                            + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                            + " and tRegProcedure.status >= " + szStartReportStatus + strExpression
                            + " and tRegPatient.PatientID = '" + patientID + "'";

                        if (bRelatedPatient)
                        {
                            sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n"
                                + " declare @pguid nvarchar(64) select @pguid=RelatedID from tRegPatient where PatientID = '" + patientID + "' "
                                + " select tRegProcedure.reportGuid, tRegProcedure.ProcedureGuid "
                                + " from tRegPatient with (nolock), tRegOrder with (nolock), tRegProcedure with (nolock) "
                                + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"
                                + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                                + " and tRegProcedure.status >= " + szStartReportStatus + strExpression
                                + " and ( tRegPatient.PatientID = '" + patientID + "' or (tRegPatient.RelatedID = @pguid AND tRegPatient.RelatedID <> '') )";
                        }

                        DataTable dtReport = dal.ExecuteQuery(sql);
                        if (dtReport == null || dtReport.Rows.Count == 0)
                        {
                            outMap.Add("HasReportHistory", "0");
                            return outMap;
                        }

                        bool bHasReportHistory = false;
                        bHasReportHistory = dtReport.Rows.Count > 0;

                        outMap.Add("HasReportHistory", bHasReportHistory ? "1" : "0");
                        return outMap;


                    }
                    #endregion
                    else
                    {
                        DataSet ds = new DataSet();


                        if (patientID != null && patientID.Length > 0)
                        #region History reports
                        {
                            // We check it using the procedure guid.
                            // Actually clients use the reportGuid to transmit the rpGuids
                            string[] rpGuids = reportGuid.Split(",".ToCharArray());
                            string strExpression = "";
                            foreach (string rpguid in rpGuids)
                            {
                                if (string.IsNullOrEmpty(rpguid))
                                    continue;

                                string tmpRP = rpguid.Trim(",;|. ".ToCharArray());

                                if (string.IsNullOrEmpty(tmpRP))
                                    continue;

                                strExpression += "'" + tmpRP + "',";
                            }

                            if (!string.IsNullOrEmpty(strExpression))
                            {
                                strExpression = strExpression.TrimEnd(new char[] { ',' });
                                strExpression = string.Format(" AND tRegProcedure.ProcedureGuid not in ({0})", strExpression);
                            }

                            GetReportInfoDAO_MSSQL rptInfo = new GetReportInfoDAO_MSSQL();
                            string allCol = rptInfo.GetAllReportColumn();

                            allCol = allCol.Replace("tRegPatient.Birthday as tRegPatient__Birthday,", "convert(nvarchar(10), tRegPatient.Birthday, 120) as tRegPatient__Birthday,");
                            allCol = allCol.Replace("tRegProcedure.Status as tRegProcedure__Status", "isnull(cast(tRegProcedure.status as varchar(8)), '50') as tRegProcedure__Status");
                            //allCol = allCol.Replace(", TREGORDER.ORDERMESSAGE as TREGORDER__ORDERMESSAGE", ""); reportlist wys/wyg comparing need show ordermessage
                            allCol = allCol.Replace("tProcedureCode.ProcedureCode as tProcedureCode__ProcedureCode,", "");
                            allCol = allCol.Replace("tProcedureCode.Description as tProcedureCode__Description,", "");
                            allCol = allCol.Replace("tProcedureCode.EnglishDescription as tProcedureCode__EnglishDescription,", "");
                            allCol = allCol.Replace("tProcedureCode.ModalityType as tProcedureCode__ModalityType,", "");
                            allCol = allCol.Replace("tProcedureCode.BodyPart as tProcedureCode__BodyPart,", "");
                            allCol = allCol.Replace("tProcedureCode.CheckingItem as tProcedureCode__CheckingItem,", "");
                            allCol = allCol.Replace("tProcedureCode.BodyCategory as tProcedureCode__BodyCategory,", "");
                            allCol = allCol.Replace("tReport.ScoringVersion as tReport__ScoringVersion,", "");
                            allCol = allCol.Replace("tReport.AccordRate as tReport__AccordRate,", "");
                            string sql = string.Empty;

                        if (bRelatedPatient)
                        {
                            sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n"
                                + " declare @pguid nvarchar(64) select @pguid=RelatedID from tRegPatient where PatientID = '" + patientID + "' "
                                + " select " + allCol.Trim(',')
                                + " ,'1' as localreport, (select ContentHtml from tReportContent where ReportId = tReport.ReportGuid) as ContentHtml, (select NaturalContentHtml from tReportContent where ReportId = tReport.ReportGuid) as NaturalContentHtml from tRegPatient with (nolock), tRegOrder with (nolock), tRegProcedure with (nolock)  "
                                + " left join tReport with (nolock) on tRegProcedure.ReportGuid = tReport.ReportGuid "
                                + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"
                                + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                                + " and tRegProcedure.status >= " + szStartReportStatus + strExpression;

                                // Here, the latter sentence is obviously faster than "IN" subquery.
                                //sql += " and ( tRegPatient.PatientID = '" + patientID + "'"
                                //    + " or tRegPatient.PatientGuid in (select RelatedID from tRegPatient where PatientID = '" + patientID + "') )";                            
                                sql += " and ( tRegPatient.PatientID = '" + patientID + "' or (tRegPatient.RelatedID = @pguid AND tRegPatient.RelatedID <> '') )";

                                // Offline Data
                                if (bOfflineData)
                                {
                                    sql += " UNION select " + allCol.Trim(',')
                                    + " ,'1' as localreport from RISArchive..tRegPatient tRegPatient with (nolock), RISArchive..tRegOrder tRegOrder with (nolock), RISArchive..tRegProcedure tRegProcedure with (nolock)"
                                    + "  left join RISArchive..tReport tReport with (nolock) on tRegProcedure.ReportGuid = tReport.ReportGuid "
                                    + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"
                                    + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                                    + " and tRegProcedure.status >= " + szStartReportStatus + strExpression;

                                sql += " and ( tRegPatient.PatientID = '" + patientID + "' or (tRegPatient.RelatedID = @pguid AND tRegPatient.RelatedID <> '') )";
                            }
                        }
                        else
                        {
                            sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n"
                                + " select " + allCol.Trim(',')
                                + " ,'1' as localreport, (select ContentHtml from tReportContent where ReportId = tReport.ReportGuid) as ContentHtml, (select NaturalContentHtml from tReportContent where ReportId = tReport.ReportGuid) as NaturalContentHtml from tRegPatient with (nolock), tRegOrder with (nolock), tRegProcedure with (nolock)"
                                + "  left join tReport with (nolock) on tRegProcedure.ReportGuid = tReport.ReportGuid "
                                + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"
                                + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                                + " and tRegProcedure.status >= " + szStartReportStatus + strExpression;

                                sql += " and tRegPatient.PatientID = '" + patientID + "'";

                                // Offline Data
                                if (bOfflineData)
                                {
                                    sql += " UNION select " + allCol.Trim(',')
                                    + " ,'1' as localreport from RISArchive..tRegPatient tRegPatient with (nolock), RISArchive..tRegOrder tRegOrder with (nolock), RISArchive..tRegProcedure tRegProcedure with (nolock) "
                                    + "  left join RISArchive..tReport tReport with (nolock) on tRegProcedure.ReportGuid = tReport.ReportGuid "
                                    + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"
                                    + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                                    + " and tRegProcedure.status >= " + szStartReportStatus + strExpression;

                                    sql += " and tRegPatient.PatientID = '" + patientID + "'";
                                }
                            }

                            sql += " order by tReport__CreateDt desc";

                            DataTable dt = new DataTable("Reports");

                            if (0 == iWrittenCount++ % 100)
                            {
                                ServerPubFun.RISLog_Info(0, "GetReportHistoryDAO_MSSQL=" + sql, "", 0);
                            }
                            else
                            {
                                ServerPubFun.RISLog_Info(0, "GetReportHistoryDAO_MSSQL, patientID=" + patientID + ", iWrittenCount=" + iWrittenCount.ToString(), "", 0);
                            }

                            dal.ExecuteQuery(sql, dt);

                            ds.Tables.Add(dt);
                        }
                        #endregion

                    if (reportGuid != null && reportGuid.Length > 0)
                    #region modify list
                    {
                        string sql = "";
                        if (strisSR == "true")
                        {
                            sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n" +
                           "select tReportList.*,tReportContentList.ContentHtml,tReportContentList.NaturalContentHtml from tReportList,tReportContentList  "
                           + " where tReportList.reportGuid = '" + reportGuid + "' and tReportList.ReportListGuid = tReportContentList.ReportListId order by operationTime desc";
                        }
                        else
                        {
                            sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n" +
                           "select * from tReportList  "
                           + " where tReportList.reportGuid = '" + reportGuid + "' order by operationTime desc";
                        }
                       

                            DataTable dt = new DataTable("History");

                            if (0 == iWrittenCount++ % 100)
                            {
                                ServerPubFun.RISLog_Info(0, "GetReportHistoryDAO_MSSQL=" + sql, "", 0);
                            }
                            else
                            {
                                ServerPubFun.RISLog_Info(0, "GetReportHistoryDAO_MSSQL, reportGuid=" + reportGuid + ", iWrittenCount=" + iWrittenCount.ToString(), "", 0);
                            }

                            //if (strDBCenter == "1")
                            //{
                            //    KodakDAL okodak = new KodakDAL(2);
                            //    okodak.ExecuteQuery(sql, dt);
                            //}
                            //else
                            //{
                            dal.ExecuteQuery(sql, dt);
                            //}

                            ds.Tables.Add(dt);
                        }
                        #endregion


                        outMap.Add("DataSet", ds);

                        return outMap;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportHistoryDAO_MSSQL, MSG=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }

        //private bool Conn2DBCenter()
        //{
        //    try
        //    {
        //        KodakDAL okodak1 = new KodakDAL();
        //        KodakDAL okodak2 = new KodakDAL(2);

        //        return (okodak1.ConnectionString.ToUpper() != okodak2.ConnectionString.ToUpper());
        //    }
        //    catch (Exception ex)
        //    {
        //        //System.Diagnostics.Debug.Assert(false, ex.Message);

        //        ServerPubFun.RISLog_Error(0, "Conn2DBCenter, MSG=" + ex.Message,
        //            (new System.Diagnostics.StackFrame()).GetFileName(),
        //            (new System.Diagnostics.StackFrame()).GetFileLineNumber());
        //    }

        //    return false;
        //}

    }

    internal class GetReportHistoryDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetReportHistoryDAO!"));
                }

                string patientID = "", reportGuid = "";
                string szStartReportStatus = System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove).ToString();

                foreach (string key in paramMap.Keys)
                {
                    string keyUpper = key.ToUpper();
                    if (keyUpper == "PATIENTGUID" || keyUpper == "PATIENTID")
                    {
                        patientID = paramMap[key] as string;

                        if (patientID == null)
                            patientID = "";
                    }
                    else if (keyUpper == "REPORTGUID" || keyUpper == "REPORTID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                            reportGuid = "";
                    }
                }

                DataSet ds = new DataSet();
                using (RisDAL dal = new RisDAL())
                {

                    if (patientID != null && patientID.Length > 0)
                    {
                        GetReportInfoDAO_ORACLE rptInfo = new GetReportInfoDAO_ORACLE();
                        string allCol = rptInfo.GetAllReportColumn();

                        //string sql = "select distinct tRegPatient.PatientID, tRegPatient.LocalName, tReport.reportGuid, tReport.reportName, tReport.CreateDt"
                        string sql = "select distinct tRegPatient.PatientID  "
                            + " , " + allCol.Trim(',')
                            + " from tRegPatient, tRegOrder, tRegProcedure, tReport"
                            + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"
                            + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                            + " and tRegProcedure.reportGuid = tReport.reportGuid"
                            + " and tRegProcedure.status >= " + szStartReportStatus
                            + " and tRegPatient.PatientID = '" + patientID + "'"
                            + " order by tReport.CreateDt desc";

                        DataTable dt = new DataTable("Reports");



                        dal.ExecuteQuery(sql, dt);

                        ds.Tables.Add(dt);
                    }

                    if (reportGuid != null && reportGuid.Length > 0)
                    {
                        string sql = "select rtrim(cast(tReportList.status as varchar(32))) as "
                            + ReportCommon.ReportCommon.FIELDNAME_REPORTSTATUS + ", tReportList.*"
                            + " from tReportList"
                            + " where reportGuid = '" + reportGuid + "' order by operationTime desc";

                        DataTable dt = new DataTable("History");



                        dal.ExecuteQuery(sql, dt);

                        ds.Tables.Add(dt);
                    }

                    Dictionary<string, object> outMap = new Dictionary<string, object>();

                    outMap.Add("DataSet", ds);
                    return outMap;
                }
                
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportHistoryDAO_ORACLE, MSG=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }
}
