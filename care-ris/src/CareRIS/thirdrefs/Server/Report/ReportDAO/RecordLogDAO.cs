using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class RecordLogDAO
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

    internal class RecordLogDAO_ABSTRACT : IReportDAO
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

    internal class RecordLogDAO_SYBASE : IReportDAO
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

    internal class RecordLogDAO_MSSQL : IReportDAO
    {
        static int iWrittenLog = 0;

        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter in SaveReportDAO!"));
                }

                string[] reportGuids = null;
                string curUserGuid = "", comments = "", snapShotSrvPath = "", modalityType = "", modality = "";
                int logType = 0, cnt = 1, costTime = 0;
                string strType = "",strMultiCostTime=null;
                string strPrintTemplateGuid = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "LOGTYPE")
                    {
                        string tmp = inMap[key] as string;

                        if (tmp != null && tmp.Length > 0)
                            logType = System.Convert.ToInt32(tmp);
                    }
                    else if (key.ToUpper() == "REPORTGUIDS" || key.ToUpper() == "REPORTGUID")
                    {
                        string tmp = inMap[key] as string;

                        if (tmp != null && (tmp = tmp.Trim(", ".ToCharArray())).Length > 0)
                            reportGuids = tmp.Split(',');
                    }
                    else if (key.ToUpper() == "COUNT")
                    {
                        string tmp = inMap[key] as string;

                        if (tmp != null && tmp.Length > 0)
                            cnt = System.Convert.ToInt32(tmp);
                    }
                    else if (key.ToUpper() == "USERID" || key.ToUpper() == "USERGUID")
                    {
                        curUserGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "COMMENTS")
                    {
                        comments = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "SNAPSHOTSRVPATH")
                    {
                        snapShotSrvPath = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "MODALITYTYPE")
                    {
                        modalityType = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "MODALITY")
                    {
                        modality = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "COSTTIME")
                    {
                        string tmp = inMap[key] as string;

                        if (tmp != null && tmp.Length > 0)
                            costTime = System.Convert.ToInt32(tmp);
                    }
                    else if (key.ToUpper() == "TYPE")
                    {
                        strType = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "MULTICOSTTIME")
                    {
                        strMultiCostTime = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "PRINTTEMPLATEGUID")
                    {
                        strPrintTemplateGuid = inMap[key] as string;
                    }
                }

                string sql = "";

                string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();

                if (strMultiCostTime != null && strMultiCostTime.Trim().Length > 0)
                {
                    string[] arrCostTime=strMultiCostTime.Split('|');
                    
                     sql+=" insert into tEventLog(guid, eventCode, ModalityType, costtime, createdt, operator,Domain)"
                            + " values('" + Guid.NewGuid().ToString() + "', " + (int)ReportCommon.ReportLog_Type.EventLog_Exam2Draft + ", '" + modalityType + "', "
                            + arrCostTime[0].ToString() + ", getdate(), '" + curUserGuid + "','"+strDomain+"') \r\n";

                     sql += " insert into tEventLog(guid, eventCode, ModalityType, costtime, createdt, operator,Domain)"
                            + " values('" + Guid.NewGuid().ToString() + "', " + (int)ReportCommon.ReportLog_Type.EventLog_Draft2Submit + ", '" + modalityType + "', "
                            + arrCostTime[1].ToString() + ", getdate(), '" + curUserGuid + "','" + strDomain + "')  \r\n";

                     sql += " insert into tEventLog(guid, eventCode, ModalityType, costtime, createdt, operator,Domain)"
                            + " values('" + Guid.NewGuid().ToString() + "', " + (int)ReportCommon.ReportLog_Type.EventLog_Submit2Approve + ", '" + modalityType + "', "
                            + arrCostTime[2].ToString() + ", getdate(), '" + curUserGuid + "','" + strDomain + "')  \r\n";

                     sql += " insert into tEventLog(guid, eventCode, ModalityType, costtime, createdt, operator,Domain)"
                            + " values('" + Guid.NewGuid().ToString() + "', " + (int)ReportCommon.ReportLog_Type.EventLog_ApproveStart2End + ", '" + modalityType + "', "
                            + arrCostTime[3].ToString() + ", getdate(), '" + curUserGuid + "','" + strDomain + "')  \r\n";


                }
                else
                {

                    if (logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_Exam2Draft) ||
                        logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_Draft2Submit) ||
                        logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_Submit2Approve) ||
                        logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_ApproveStart2End) ||
                        logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_Exam2Print))
                    {
                        if (reportGuids.Length > 0)
                        {
                            sql = "insert into tEventLog(guid, eventCode, ModalityType, costtime, createdt, operator,Domain)"
                                + " values('" + Guid.NewGuid().ToString() + "', " + logType.ToString() + ", '" + modalityType + "', "
                                + costTime.ToString() + ", getdate(), '" + curUserGuid + "','" + strDomain + "')";
                           
                        }
                    }
                    // 2015-06-08, Oscar added (US25173)
                    else if (logType == (int)ReportCommon.ReportLog_Type.EventLog_SubmitCostTime
                        || logType == (int)ReportCommon.ReportLog_Type.EventLog_ApproveCostTime)
                    {
                        if (reportGuids.Length > 0)
                        {
                            sql += UpsertEventLog(logType, modalityType, costTime, curUserGuid, strDomain, reportGuids[0]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < reportGuids.Length; i++)
                        {
                            string tmp = reportGuids.GetValue(i) as string;
                            if (tmp != null && tmp.Length > 0)
                            {
                                if (cnt < 1)
                                {
                                    sql += " insert into tReportPrintLog(fileGuid, ReportGuid,Printer,PrintDt,Counts,Comments,snapShotSrvPath,Type,Domain) "
                                        + " values(newid(), '" + tmp + "', '" + curUserGuid + "', getdate(), "
                                        + cnt.ToString() + ", '" + comments + "', '" + snapShotSrvPath + "','" + strType + "','" + strDomain + "') \r\n";
                                }
                                else
                                {
                                    sql += " update tReport set isPrint=1,Optional1='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', PrintTemplateGuid ='" + strPrintTemplateGuid + "' where reportGuid='" + tmp + "' \r\n"
                                        + " insert into tReportPrintLog(fileGuid, ReportGuid,Printer,PrintDt,Counts,Comments,snapShotSrvPath,Type,PrintTemplateGuid,Domain)"
                                        + " values(newid(), '" + tmp + "', '" + curUserGuid + "', getdate(), "
                                        + cnt.ToString() + ", '" + comments + "', '" + snapShotSrvPath + "','" + strType + "','"+strPrintTemplateGuid+ "','"+ strDomain + "') \r\n";
                                }
                            }
                        }
                    }
                }
                if (0 == iWrittenLog++ % 100)
                {
                    ServerPubFun.RISLog_Info(0, "RecordLogDAO, UserGuid=" + curUserGuid + ", SQL=" + sql,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }
                else
                {
                    ServerPubFun.RISLog_Info(0, "RecordLogDAO, UserGuid=" + curUserGuid + ", reportGuids=" + reportGuids.ToString(),
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }

                if (sql.Length > 0)
                {
                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteNonQuery(sql);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "RecordLogDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return false;
        }

        // 2015-06-09, Oscar added (US25173)
        string UpsertEventLog(int logType, string modalityType, int costTime, string userGuid, string domain, string reportGuid)
        {
            var where = string.Format("EventCode = {0} AND ReportGuid = '{1}'", logType, reportGuid);
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("IF EXISTS (SELECT 1 FROM dbo.tEventLog WHERE {0})", where));
            sb.AppendLine(string.Format("UPDATE dbo.tEventLog SET CostTime += {0}, Operator = '{1}' WHERE {2}", costTime, userGuid, where));
            sb.AppendLine("ELSE");
            sb.AppendLine(string.Format(
                "INSERT INTO dbo.tEventLog (Guid, EventCode, ModalityType, CostTime, CreateDt, Operator, Domain, ReportGuid) VALUES ('{0}',  {1}, '{2}', {3}, GETDATE(), '{4}', '{5}', '{6}')",
                Guid.NewGuid(), logType, modalityType, costTime, userGuid, domain, reportGuid));
            return sb.ToString();
        }
    }

    internal class RecordLogDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;

                if (inMap == null || inMap.Count < 1)
                {
                    throw (new Exception("No parameter in SaveReportDAO!"));
                }

                string[] reportGuids = null;
                string curUserGuid = "", comments = "", snapShotSrvPath = "", modalityType = "", modality = "";
                int logType = 0, cnt = 1, costTime = 0;
                string strType = "";
                string strPrintTemplateGuid = "";
                string strMultiCostTime = "";
                string strDomain = CommonGlobalSettings.Utilities.GetCurDomain();
                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "LOGTYPE")
                    {
                        string tmp = inMap[key] as string;

                        if (tmp != null && tmp.Length > 0)
                            logType = System.Convert.ToInt32(tmp);
                    }
                    else if (key.ToUpper() == "REPORTGUIDS" || key.ToUpper() == "REPORTGUID")
                    {
                        string tmp = inMap[key] as string;

                        if (tmp != null && (tmp = tmp.Trim(", ".ToCharArray())).Length > 0)
                            reportGuids = tmp.Split(',');
                    }
                    else if (key.ToUpper() == "COUNT")
                    {
                        string tmp = inMap[key] as string;

                        if (tmp != null && tmp.Length > 0)
                            cnt = System.Convert.ToInt32(tmp);
                    }
                    else if (key.ToUpper() == "USERID" || key.ToUpper() == "USERGUID")
                    {
                        curUserGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "COMMENTS")
                    {
                        comments = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "SNAPSHOTSRVPATH")
                    {
                        snapShotSrvPath = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "MODALITYTYPE")
                    {
                        modalityType = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "MODALITY")
                    {
                        modality = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "COSTTIME")
                    {
                        string tmp = inMap[key] as string;

                        if (tmp != null && tmp.Length > 0)
                            costTime = System.Convert.ToInt32(tmp);
                    }
                    else if (key.ToUpper() == "TYPE")
                    {
                        strType = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "MULTICOSTTIME")
                    {
                        strMultiCostTime = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "PRINTTEMPLATEGUID")
                    {
                        strPrintTemplateGuid = inMap[key] as string;
                    }
                }

                string sql = "";

                if (logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_Exam2Draft) ||
                    logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_Draft2Submit) ||
                    logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_Submit2Approve) ||
                    logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_ApproveStart2End) ||
                    logType == System.Convert.ToInt32(ReportCommon.ReportLog_Type.EventLog_Exam2Print))
                {
               
                    if (reportGuids.Length > 0)
                    {
                        sql = " insert into tEventLog(guid, eventCode, ModalityType, costtime, createdt, operator,Domain)"
                            + " values('" + reportGuids[0] + "', " + logType.ToString() + ", '" + modalityType + "', "
                            + costTime.ToString() + ", sysdate, '" + curUserGuid + "','" + strDomain + "'); ";
                    }
                }
                else
                {
                    for (int i = 0; i < reportGuids.Length; i++)
                    {
                        string tmp = reportGuids.GetValue(i) as string;
                        if (tmp != null && tmp.Length > 0)
                        {
                            if (cnt < 1)
                            {
                                sql += " insert into tReportPrintLog(fileGuid,ReportGuid,Printer,PrintDt,Counts,Comments,snapShotSrvPath,Type,Domain)"
                                    + " values(sys_guid(), '" + tmp + "', '" + curUserGuid + "', sysdate, "
                                    + cnt.ToString() + ", '" + comments + "', '" + snapShotSrvPath + "','" + strType + "','" + strDomain + "'); ";
                            }
                            else
                            {
                                sql += " update tReport set isPrint=1 ,Optional1='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+ "',PrintTemplateGuid ='" + strPrintTemplateGuid + "' where reportGuid='" + tmp + "'; "
                                    + " insert into tReportPrintLog(fileGuid,ReportGuid,Printer,PrintDt,Counts,Comments,snapShotSrvPath,Type,PrintTemplateGuid,Domain)"
                                    + " values(sys_guid(), '" + tmp + "', '" + curUserGuid + "', sysdate, "
                                    + cnt.ToString() + ", '" + comments + "', '" + snapShotSrvPath + "','" + strType + "','" + strPrintTemplateGuid + "','" + strDomain + "'); ";
                            }
                        }
                    }
                }

                if (sql.Length > 0)
                    sql = "begin " + sql + " commit; end;";

                

                if (sql.Length > 0)
                {
                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteNonQuery(sql);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "RecordLogDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return false;
        }
    }
}
