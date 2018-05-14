using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetReportTemplateDAO
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

    internal class GetReportTemplateDAO_ABSTRACT : IReportDAO
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

    internal class GetReportTemplateDAO_SYBASE : IReportDAO
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

    internal class GetReportTemplateDAO_MSSQL : IReportDAO
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

                string parentGuid = "", userGuid = "", type = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "PARENTGUID")
                    {
                        parentGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERGUID" || key.ToUpper() == "USERID")
                    {
                        userGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "TYPE")
                    {
                        type = inMap[key] as string;
                    }
                }

                parentGuid = parentGuid == null ? "" : parentGuid.Trim();
                userGuid = userGuid == null ? "" : userGuid.Trim();
                type = type == null ? "" : type.Trim();

                string sql = "", sql0 = "", sql1 = "";

                if (type.CompareTo("1") == 0)
                {
                    /*sql = " select * from tReportTemplateDirec left join tReportTemplate on tReportTemplate.templateGuid = tReportTemplateDirec.templateGuid \r\n"
                        + " where (tReportTemplateDirec.type = 0 or (tReportTemplateDirec.type = 1 and tReportTemplateDirec.UserGuid='" + userGuid + "')) \r\n";

                    if (parentGuid.Length > 0)
                    {
                        sql = " and tReportTemplateDirec.parentid = '" + parentGuid + "' \r\n";
                    }

                    sql += " order by tReportTemplateDirec.type, tReportTemplateDirec.itemorder \r\n";
                     */
                    sql = string.Format("select * from tReportTemplateDirec where ParentID = 'UserTemplate' and (type=0 or UserGuid = '{0}') order by ItemOrder", userGuid);
                }
                else if (type.CompareTo("2") == 0)
                {
                    sql0 = " select * from tPhraseTemplate where (type = 0 or type = 2 or (type = 1 and UserGuid='" + userGuid + "')) order by type, modalityType \r\n";
                }
                else if (type.CompareTo("3") == 0)
                {
                    sql1 = " select SELECT TemplateGuid,Type,TemplateName,IsDefaultByType,Version,ModalityType,IsDefaultByModality  from tPrintTemplate \r\n";
                }
                else
                {
                    sql = " select * from tReportTemplateDirec left join tReportTemplate on tReportTemplate.templateGuid = tReportTemplateDirec.templateGuid \r\n"
                        + " where (tReportTemplateDirec.type = 0 or tReportTemplateDirec.type = 2 or (tReportTemplateDirec.type = 1 and tReportTemplateDirec.UserGuid='" + userGuid + "')) and tReportTemplateDirec.DirectoryType='report' \r\n";

                    if (parentGuid.Length > 0)
                    {
                        sql = " and tReportTemplateDirec.parentid = '" + parentGuid + "' \r\n";
                    }

                    sql += " order by tReportTemplateDirec.type, tReportTemplateDirec.itemorder \r\n";

                    ////sql0 = " select * from tPhraseTemplate where (type = 0 or (type = 1 and UserGuid='" + userGuid + "')) order by type, modalityType \r\n";
                    sql0 = " select tReportTemplateDirec.*, tPhraseTemplate.TemplateGuid,tPhraseTemplate.TemplateName,tPhraseTemplate.TemplateInfo,tPhraseTemplate.ShortcutCode from tReportTemplateDirec left join tPhraseTemplate on tPhraseTemplate.templateGuid = tReportTemplateDirec.templateGuid \r\n"
                        + " where (tReportTemplateDirec.type = 0 or tReportTemplateDirec.type = 2 or (tReportTemplateDirec.type = 1 and tReportTemplateDirec.UserGuid='" + userGuid + "')) and tReportTemplateDirec.DirectoryType='phrase' order by tReportTemplateDirec.type, tReportTemplateDirec.itemorder \r\n";

                    sql1 = " SELECT TemplateGuid,Type,TemplateName,IsDefaultByType,Version,ModalityType,IsDefaultByModality from tPrintTemplate \r\n";

                    

                }

                

                DataSet ds = new DataSet();

                using (RisDAL dal = new RisDAL())
                {

                    if (sql != null && sql.Length > 0)
                    {
                        DataTable dt = new DataTable("ReportTemplate");
                        dal.ExecuteQuery(sql, dt);
                        ds.Tables.Add(dt);
                    }

                    if (sql0 != null && sql0.Length > 0)
                    {
                        DataTable dt0 = new DataTable("Phrase");
                        dal.ExecuteQuery(sql0, dt0);
                        ds.Tables.Add(dt0);
                    }

                    if (sql1 != null && sql1.Length > 0)
                    {
                        DataTable dt1 = new DataTable("PrintTemplate");
                        dal.ExecuteQuery(sql1, dt1);
                        ds.Tables.Add(dt1);
                    }

                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportTemplateDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }

    internal class GetReportTemplateDAO_ORACLE : IReportDAO
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

                string parentGuid = "", userGuid = "", type = "";

                foreach (string key in inMap.Keys)
                {
                    if (key.ToUpper() == "PARENTGUID")
                    {
                        parentGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "USERGUID" || key.ToUpper() == "USERID")
                    {
                        userGuid = inMap[key] as string;
                    }
                    else if (key.ToUpper() == "TYPE")
                    {
                        type = inMap[key] as string;
                    }
                }

                parentGuid = parentGuid == null ? "" : parentGuid.Trim();
                userGuid = userGuid == null ? "" : userGuid.Trim();
                type = type == null ? "" : type.Trim();

                string sql = "", sql0 = "", sql1 = "";

                if (type.CompareTo("1") == 0)
                {
                    sql = " select * from tReportTemplateDirec left join tReportTemplate on tReportTemplate.templateGuid = tReportTemplateDirec.templateGuid "
                        + " where (tReportTemplateDirec.type = 0 or (tReportTemplateDirec.type = 1 and tReportTemplateDirec.UserGuid='" + userGuid + "')) ";

                    if (parentGuid.Length > 0)
                    {
                        sql = " and tReportTemplateDirec.parentid = '" + parentGuid + "' ";
                    }

                    sql += " order by tReportTemplateDirec.type, tReportTemplateDirec.itemorder ";
                }
                else if (type.CompareTo("2") == 0)
                {
                    sql0 = " select * from tPhraseTemplate where (type = 0 or (type = 1 and UserGuid='" + userGuid + "')) order by type, modalityType ";
                }
                else if (type.CompareTo("3") == 0)
                {
                    sql1 = " SELECT TemplateGuid,Type,TemplateName,IsDefaultByType,Version,ModalityType,IsDefaultByModality  from tPrintTemplate ";
                }
                else
                {
                    sql = " select * from tReportTemplateDirec left join tReportTemplate on tReportTemplate.templateGuid = tReportTemplateDirec.templateGuid "
                        + " where (tReportTemplateDirec.type = 0 or (tReportTemplateDirec.type = 1 and tReportTemplateDirec.UserGuid='" + userGuid + "')) ";

                    if (parentGuid.Length > 0)
                    {
                        sql = " and tReportTemplateDirec.parentid = '" + parentGuid + "' ";
                    }

                    sql += " order by tReportTemplateDirec.type, tReportTemplateDirec.itemorder ";

                    sql0 = " select * from tPhraseTemplate where (type = 0 or (type = 1 and UserGuid='" + userGuid + "')) order by type, modalityType ";

                    sql1 = " SELECT TemplateGuid,Type,TemplateName,IsDefaultByType,Version,ModalityType,IsDefaultByModality  from tPrintTemplate ";
                }

                

                DataSet ds = new DataSet();

                using (RisDAL dal = new RisDAL())
                {

                    if (sql != null && sql.Length > 0)
                    {
                        DataTable dt = new DataTable("ReportTemplate");
                        dal.ExecuteQuery(sql, dt);
                        ds.Tables.Add(dt);
                    }

                    if (sql0 != null && sql0.Length > 0)
                    {
                        DataTable dt0 = new DataTable("Phrase");
                        dal.ExecuteQuery(sql0, dt0);
                        ds.Tables.Add(dt0);
                    }

                    if (sql1 != null && sql1.Length > 0)
                    {
                        DataTable dt1 = new DataTable("PrintTemplate");
                        dal.ExecuteQuery(sql1, dt1);
                        ds.Tables.Add(dt1);
                    }

                    return ds;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportTemplateDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }
    }
}
