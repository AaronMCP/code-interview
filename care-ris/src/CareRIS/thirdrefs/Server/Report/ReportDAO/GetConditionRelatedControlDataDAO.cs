using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;


namespace Server.ReportDAO
{
    public class GetConditionRelatedControlDataDAO
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

    internal class GetConditionRelatedControlDataDAO_ABSTRACT : IReportDAO
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

    internal class GetConditionRelatedControlDataDAO_MSSQL : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> inMap = param as Dictionary<string, object>;
                string sql=null;
                DataSet ds=new DataSet();
                using (RisDAL dal = new RisDAL())
                {
                    if (inMap == null || inMap.Count < 1)
                    {
                        throw (new Exception("No parameter!"));
                    }

                    foreach (string key in inMap.Keys)
                    {
                        string value = "";
                        value = inMap[key].ToString().ToUpper();

                        if (value == "MODALITYTYPE-MODALITY")
                        {
                            sql = "select distinct modality as text,modalityType, modality as value  from tModality";
                            DataTable dt = new DataTable("ModalityType_Modality");
                            dal.ExecuteQuery(sql, dt);
                            ds.Tables.Add(dt);
                        }
                        else if (value == "MODALITYTYPE-DESCRIPTION")
                        {
                            sql = "select distinct Description as text, modalityType,Description as value  from tProcedureCode";
                            DataTable dt = new DataTable("ModalityType_DESCRIPTION");
                            dal.ExecuteQuery(sql, dt);
                            ds.Tables.Add(dt);
                        }
                        else if (value == "MODALITYTYPE-BODYPART")
                        {
                            sql = "select distinct bodypart as text, modalityType,bodypart as value from tProcedureCode";
                            DataTable dt = new DataTable("ModalityType_BodyPart");
                            dal.ExecuteQuery(sql, dt);
                            ds.Tables.Add(dt);
                        }
                        else if (value == "MODALITYTYPE-CHECKINGITEM")
                        {
                            //EK_HI00064564 Foman 2007.12.26   "select distinct modalityType,CheckingItem as text,  CheckingItem as value  from tProcedureCode""
                            sql = "select distinct CheckingItem as text, modalityType, CheckingItem as value  from tProcedureCode";
                            DataTable dt = new DataTable("ModalityType_CheckingItem");
                            dal.ExecuteQuery(sql, dt);
                            ds.Tables.Add(dt);
                        }
                    }
                }

                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetConditionRelatedControlDataDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }
            return null;
        }
    }

    internal class GetConditionRelatedControlDataDAO_SYBASE : IReportDAO
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

    internal class GetConditionRelatedControlDataDAO_ORACLE : IReportDAO
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
}
