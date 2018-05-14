using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;

namespace Server.ReportDAO
{
    public class GetReportInfoDAO
    {
        public object Execute(object param)
        {
            using (RisDAL oKodak = new RisDAL())
            {
                string clsType = string.Format("{0}_{1}", this.GetType().ToString(), oKodak.DriverClassName.ToUpper());
                if (oKodak != null)
                {
                    oKodak.Dispose();
                }

                Type type = Type.GetType(clsType);
                IReportDAO iRptDAO = Activator.CreateInstance(type) as IReportDAO;
                return iRptDAO.Execute(param);
            }
        }
    }

    internal class GetReportInfoDAO_ABSTRACT : IReportDAO
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

    internal class GetReportInfoDAO_SYBASE : IReportDAO
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

    internal class GetReportInfoDAO_MSSQL : IReportDAO
    {
        static int iWrittenCount = 0;
        static string _strReportInfoColumns = "";

        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetReportNameDAO!"));
                }

                string reportGuid = "";
                string rpGuids = "";
                string accNO = "";

                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "REPORTGUID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                            reportGuid = "";
                    }
                    else if (key.ToUpper() == "PROCEDUREGUID")
                    {
                        rpGuids = paramMap[key] as string;

                        if (rpGuids == null)
                            rpGuids = "";
                    }
                    else if (key.ToUpper() == "ACCNO")
                    {
                        accNO = paramMap[key] as string;

                        if (accNO == null)
                            accNO = "";
                    }
                }

                string sql = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n" +
                    "select " + GetAllReportColumn() + " \r\n"
                        + " from tRegPatient with (nolock), tRegOrder with (nolock), tProcedureCode with (nolock), tRegProcedure with (nolock) \r\n"
                        + " left join tReport with (nolock) on tRegProcedure.reportGuid = tReport.reportGuid \r\n"
                        + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid \r\n"
                        + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid \r\n"
                        + " and tRegProcedure.ProcedureCode = tProcedureCode.ProcedureCode \r\n";

                string sqlFile = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n" +
                                 " select distinct tReportFile.* from tReportFile, tReport "
                                 + " where tReportFile.reportGuid = tReport.reportGuid";


                #region Added by Kevin For SR

                string sqlreportcontent = "SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED \r\n" +
                                          " select * from tReportContent"
                                          + " where tReportContent.ReportId ='" + reportGuid + "' ";

                #endregion


                if (!string.IsNullOrWhiteSpace(reportGuid))
                {
                    sql += " and tReport.reportGuid = '" + reportGuid + "'";

                    sqlFile += " and tReport.reportGuid = '" + reportGuid + "'";
                }
                else if (!string.IsNullOrWhiteSpace(rpGuids))
                {
                    rpGuids = rpGuids.Trim(", ".ToCharArray());

                    string[] arr = rpGuids.Split(',');
                    if (arr.Length > 1)
                    {
                        if (!rpGuids.Contains("','"))
                        {
                            rpGuids = rpGuids.Replace(",", "','");
                            rpGuids = "'" + rpGuids + "'";
                        }
                        sql += " and tRegProcedure.ProcedureGuid in (" + rpGuids + ")";
                    }
                    else
                    {

                        rpGuids = "'" + rpGuids + "'";
                        sql += " and tRegProcedure.ProcedureGuid =" + rpGuids;
                    }

                    sqlFile += " and 1=2 ";
                }
                else if (!string.IsNullOrWhiteSpace(accNO))
                {
                    sql += " AND tRegOrder.ACCNO = '" + accNO.Trim() + "'";

                    sqlFile += " AND 1=2 ";
                }

                if (0 == iWrittenCount++ % 100)
                {
                    ServerPubFun.RISLog_Info(0, "GetReportInfoDAO=" + sql + ", reportFile=" + sqlFile, "", 0);
                }
                else
                {
                    ServerPubFun.RISLog_Info(0, "GetReportInfoDAO, rpGuid=" + rpGuids + ", reportGuid=" + reportGuid + ", iWrittenCount=" + iWrittenCount.ToString(), "", 0);
                }

                DataSet ds = new DataSet();

                using (RisDAL dal = new RisDAL())
                {

                    //
                    // report info
                    DataTable dt0 = new DataTable("ReportInfo");

                    dal.ExecuteQuery(sql, dt0);

                    ds.Tables.Add(dt0);

                    //
                    // report file
                    DataTable dt1 = new DataTable("ReportFile");

                    dal.ExecuteQuery(sqlFile, dt1);

                    ds.Tables.Add(dt1);

                    string sqlPrintTemplate = "select * from tPrintTemplate where Type=3 and TemplateName='QualityControlReportTemplate'";
                    DataTable dt2 = new DataTable("PrintTemplate");
                    dal.ExecuteQuery(sqlPrintTemplate, dt2);
                    ds.Tables.Add(dt2);
					#region Added by Kevin For SR

                    // reportcontent
                    DataTable dt3 = new DataTable("ReportContent");

                    dal.ExecuteQuery(sqlreportcontent, dt3);

                    ds.Tables.Add(dt3);

                    #endregion
                }


                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportInfoDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }

        public string GetAllReportColumn()
        {
            try
            {
                if (_strReportInfoColumns != null && _strReportInfoColumns.Length > 0)
                    return _strReportInfoColumns;


                _strReportInfoColumns = " tRegOrder.OrderGuid as tRegOrder__OrderGuid,tRegOrder.AccNo as tRegOrder__AccNo, tRegOrder.ApplyDept as tRegOrder__ApplyDept, "
                    + " tRegOrder.ApplyDoctor as tRegOrder__ApplyDoctor, tRegOrder.IsScan as tRegOrder__IsScan, "
                    + " tRegOrder.Comments as tRegOrder__Comments, tRegOrder.RemoteAccNo as tRegOrder__RemoteAccNo, tRegOrder.TotalFee as tRegOrder__TotalFee,tRegOrder.InitialDomain as tRegOrder__InitialDomain,"
                    + " tRegOrder.HisID as tRegOrder__HisID, tRegOrder.CardNo as tRegOrder__CardNo, tRegPatient.MedicareNo as tRegPatient__MedicareNo,tRegOrder.ReferralId as tRegOrder__ReferralId,tRegOrder.IsReferral as tRegOrder__IsReferral, "
                    + " tRegPatient.PatientGuid as tRegPatient__PatientGuid,tRegPatient.GlobalID as tRegPatient__GlobalID, tRegPatient.PatientID as tRegPatient__PatientID, tRegPatient.LocalName as tRegPatient__LocalName, "
                    + " tRegPatient.EnglishName as tRegPatient__EnglishName, tRegPatient.ReferenceNo as tRegPatient__ReferenceNo, tRegPatient.Birthday as tRegPatient__Birthday,"
                    + " tRegPatient.Gender as tRegPatient__Gender, tRegPatient.Address as tRegPatient__Address, tRegPatient.Telephone as tRegPatient__Telephone, "
                    + " tRegPatient.IsVIP as tRegPatient__IsVIP, tRegPatient.Comments as tRegPatient__Comments, tRegPatient.RemotePID as tRegPatient__RemotePID, "
                    + " tRegPatient.CreateDt as tRegPatient__CreateDt, "
                    + " tRegProcedure.ProcedureGuid as tRegProcedure__ProcedureGuid, tRegProcedure.ProcedureCode as tRegProcedure__ProcedureCode,"
                    + " tRegProcedure.ExamSystem as tRegProcedure__ExamSystem, tRegProcedure.WarningTime as tRegProcedure__WarningTime, tRegProcedure.FilmSpec as tRegProcedure__FilmSpec,"
                    + " tRegProcedure.FilmCount as tRegProcedure__FilmCount, tRegProcedure.ContrastName as tRegProcedure__ContrastName, tRegProcedure.ContrastDose as tRegProcedure__ContrastDose, "
                    + " tRegProcedure.ImageCount as tRegProcedure__ImageCount, tRegProcedure.ExposalCount as tRegProcedure__ExposalCount, tRegProcedure.Deposit as tRegProcedure__Deposit, "
                    + " tRegProcedure.Charge as tRegProcedure__Charge, tRegProcedure.ModalityType as tRegProcedure__ModalityType, tRegProcedure.Modality as tRegProcedure__Modality, "
                    + " tRegProcedure.Registrar as tRegProcedure__Registrar, tRegProcedure.RegisterDt as tRegProcedure__RegisterDt, tRegProcedure.Priority as tRegProcedure__Priority,"
                    + " tRegProcedure.Technician as tRegProcedure__Technician, tRegProcedure.TechDoctor as tRegProcedure__TechDoctor, tRegProcedure.TechNurse as tRegProcedure__TechNurse, "
                    + " tRegProcedure.OperationStep as tRegProcedure__OperationStep, tRegProcedure.ExamineDt as tRegProcedure__ExamineDt, tRegProcedure.Mender as tRegProcedure__Mender, "
                    + " tRegProcedure.ModifyDt as tRegProcedure__ModifyDt, tRegProcedure.IsExistImage as tRegProcedure__IsExistImage, tRegProcedure.Status as tRegProcedure__Status, "
                    + " tRegProcedure.Comments as tRegProcedure__Comments,  tRegProcedure.IsCharge as tRegProcedure__IsCharge, tRegProcedure.RemoteRPID as tRegProcedure__RemoteRPID, "
                    + " tRegProcedure.QueueNo as tRegProcedure__QueueNo, tRegProcedure.CreateDt as tRegProcedure__CreateDt, tRegOrder.VisitGuid as tRegOrder__VisitGuid, "
                   + "  tRegProcedure.Posture as tRegProcedure__Posture, tRegProcedure.MedicineUsage as tRegProcedure__MedicineUsage, "

                   + "  tRegOrder.InhospitalNo as tRegOrder__InhospitalNo, tRegOrder.ClinicNo as tRegOrder__ClinicNo, tRegOrder.PatientType as tRegOrder__PatientType, tRegOrder.Observation as tRegOrder__Observation,tRegOrder.Optional2 as tRegOrder__Inspection,"
                   + "  tRegOrder.HealthHistory as tRegOrder__HealthHistory, tRegOrder.InhospitalRegion as tRegOrder__InhospitalRegion, tRegOrder.BedNo as tRegOrder__BedNo,tRegOrder.BedSide as tRegOrder__BedSide, tRegOrder.CreateDt as tRegOrder__CreateDt,"
                   + "  tRegOrder.CurrentAge as tRegOrder__CurrentAge,tRegOrder.VisitComment as tRegOrder__VisitComment,tRegOrder.ChargeType as tRegOrder__ChargeType, tReport.ReportGuid as tReport__ReportGuid,"
                   + "  tReport.ReportName as tReport__ReportName, tReport.WYS as tReport__WYS, tReport.WYG as tReport__WYG, tReport.AppendInfo as tReport__AppendInfo, tReport.TechInfo as tReport__TechInfo, "
                   + "  tReport.ReportText as tReport__ReportText, tReport.DoctorAdvice as tReport__DoctorAdvice, tReport.IsPositive as tReport__IsPositive, tReport.AcrCode as tReport__AcrCode, "
                   + "  tReport.AcrAnatomic as tReport__AcrAnatomic, tReport.AcrPathologic as tReport__AcrPathologic, tReport.Creater as tReport__Creater, tReport.CreateDt as tReport__CreateDt, tReport.Submitter as tReport__Submitter,"
                   + "  tReport.SubmitDt as tReport__SubmitDt, tReport.FirstApprover as tReport__FirstApprover, tReport.FirstApproveDt as tReport__FirstApproveDt, tReport.SecondApprover as tReport__SecondApprover, "
                   + "  tReport.SecondApproveDt as tReport__SecondApproveDt, tReport.IsDiagnosisRight as tReport__IsDiagnosisRight, tReport.KeyWord as tReport__KeyWord, tReport.ReportQuality as tReport__ReportQuality, "
                   + "  tReport.RejectToObject as tReport__RejectToObject, tReport.Rejecter as tReport__Rejecter, tReport.RejectDt as tReport__RejectDt, tReport.Status as tReport__Status, "
                   + "  tReport.Comments as tReport__Comments, tReport.DeleteMark as tReport__DeleteMark, tReport.Deleter as tReport__Deleter, "
                   + "  tReport.DeleteDt as tReport__DeleteDt, tReport.Recuperator as tReport__Recuperator, tReport.ReconvertDt as tReport__ReconvertDt, tReport.Mender as tReport__Mender, "
                   + "  tReport.ModifyDt as tReport__ModifyDt, tReport.IsPrint as tReport__IsPrint, tReport.CheckItemName as tReport__CheckItemName, tReport.Optional1 as tReport__Optional1, "
                   + "  tReport.Optional2 as tReport__Optional2, tReport.Optional3 as tReport__Optional3, tReport.IsLeaveWord as tReport__IsLeaveWord, tReport.WYSText as tReport__WYSText, "
                   + "  tReport.WYGText as tReport__WYGText, tReport.IsDraw as tReport__IsDraw, tReport.DrawerSign as tReport__DrawerSign, tReport.DrawTime as tReport__DrawTime, "
                   + "  tReport.IsLeaveSound as tReport__IsLeaveSound, tReport.TakeFilmDept as tReport__TakeFilmDept, tReport.TakeFilmRegion as tReport__TakeFilmRegion, "
                   + "  tReport.PrintTemplateGuid as tReport__PrintTemplateGuid, tReport.Domain as tReport__Domain,"
                   + "  tReport.TakeFilmComment as tReport__TakeFilmComment, tReport.RebuildMark as tReport__RebuildMark, "
                   + "  tReport.PrintCopies as tReport__PrintCopies, "
                   + "  tProcedureCode.ProcedureCode as tProcedureCode__ProcedureCode, tProcedureCode.Description as tProcedureCode__Description, "
                   + "  tProcedureCode.EnglishDescription as tProcedureCode__EnglishDescription, tProcedureCode.ModalityType as tProcedureCode__ModalityType, "
                   + "  tProcedureCode.BodyPart as tProcedureCode__BodyPart, tProcedureCode.CheckingItem as tProcedureCode__CheckingItem, "
                   + "  tProcedureCode.BodyCategory as tProcedureCode__BodyCategory, "
                   + "  tReport.SubmitDomain as tReport__SubmitDomain, "
                   + "  tReport.RejectDomain as tReport__RejectDomain, "
                   + "  tReport.FirstApproveDomain as tReport__FirstApproveDomain, "
                   + "  tRegPatient.Alias as tRegPatient__Alias, "
                   + "  tRegPatient.ParentName as tRegPatient__ParentName, "
                   + "  tRegOrder.curPatientName as tRegOrder__curPatientName,tRegOrder.BodyWeight as tRegOrder__BodyWeight,"
                   + "  tRegOrder.curGender as tRegOrder__curGender, tRegOrder.IsCharge as tRegOrder__IsCharge,"
                   + "  tRegOrder.AgeInDays as tRegOrder__AgeInDays, tRegProcedure.Booker as tRegProcedure__Booker,"
                   + "  tRegProcedure.BookingBeginDt as tRegProcedure__BookingBeginDt, tRegProcedure.BookingEndDt as tRegProcedure__BookingEndDt "
                   + " , tRegOrder.BookingSite AS tRegOrder__BookingSite, tRegOrder.ExamSite AS tRegOrder__ExamSite, tRegOrder.RegSite AS tRegOrder__RegSite, tRegPatient.Site AS tRegPatient__Site, tReport.FirstApproveSite AS tReport__FirstApproveSite, tReport.RejectSite AS tReport__RejectSite, tReport.SecondApproveSite AS tReport__SecondApproveSite, tReport.SubmitSite AS tReport__SubmitSite "
                   + " , tRegOrder.ThreeDRebuild AS tRegOrder__ThreeDRebuild "
                   + " , tReport.ReportQuality2 as tReport__ReportQuality2 "
                   + " , TREGORDER.ORDERMESSAGE as TREGORDER__ORDERMESSAGE "
                   + " , TREGORDER.PathologicalFindings as TREGORDER__PathologicalFindings "
                   + " , TREGORDER.InternalOptional1 as TREGORDER__InternalOptional1 "
                   + " , TREGORDER.InternalOptional2 as TREGORDER__InternalOptional2 "
                   + " , TREGORDER.ExternalOptional1 as TREGORDER__ExternalOptional1 "
                   + " , TREGORDER.ExternalOptional2 as TREGORDER__ExternalOptional2 "
                   + " , TREGORDER.ExternalOptional3 as TREGORDER__ExternalOptional3 "
                   + ", tReport.ReportQualityComments as tReport__ReportQualityComments"
                    + ", tReport.CreaterName as tReport__CreaterName"
                    + ", tReport.SubmitterName as tReport__SubmitterName"
                    + ", tReport.FirstApproverName as tReport__FirstApproverName"
                    + ", tReport.SecondApproverName as tReport__SecondApproverName"
                    + ", tReport.ScoringVersion as tReport__ScoringVersion"
                    + ", tReport.AccordRate as tReport__AccordRate"
                    + ", tReport.SubmitterSign as tReport__SubmitterSign"
                    + ", tReport.FirstApproverSign as tReport__FirstApproverSign"
                    + ", tReport.SecondApproverSign as tReport__SecondApproverSign"
                    + ", tReport.SubmitterSignTimeStamp as tReport__SubmitterSignTimeStamp"
                    + ", tReport.FirstApproverSignTimeStamp as tReport__FirstApproverSignTimeStamp"
                    + ", tReport.SecondApproverSignTimeStamp as tReport__SecondApproverSignTimeStamp"
                    + ", tReport.IsModified as tReport__IsModified"
                    + ", tRegProcedure.BookerName as tRegProcedure__BookerName"
                    + ", tRegProcedure.RegistrarName as tRegProcedure__RegistrarName"
                    + ", tRegProcedure.TechnicianName as tRegProcedure__TechnicianName"
                    + ", tRegProcedure.UnwrittenCurrentOwner as tRegProcedure__UnwrittenCurrentOwner"
                    + ", tRegProcedure.UnapprovedCurrentOwner as tRegProcedure__UnapprovedCurrentOwner"
                    + ", tReport.MenderName as tReport__MenderName"
                    + ", tRegOrder.InjectDose as tRegOrder__InjectDose"
                    + ", tRegOrder.InjectTime as tRegOrder__InjectTime"
                    + ", tRegOrder.BodyHeight as tRegOrder__BodyHeight"
                    + ", tRegOrder.BloodSugar as tRegOrder__BloodSugar"
                    + ", tRegOrder.Insulin as tRegOrder__Insulin"
                    + ", tRegOrder.GoOnGoTime as tRegOrder__GoOnGoTime"
                    + ", tRegOrder.InjectorRemnant as tRegOrder__InjectorRemnant"
                    + ", tRegOrder.SubmitHospital as tRegOrder__SubmitHospital"
                    + ", tRegOrder.SubmitDept as tRegOrder__SubmitDept"
                    + ", tRegOrder.SubmitDoctor as tRegOrder__SubmitDoctor"
                    + " , TREGORDER.Optional1 as TREGORDER__Optional1 "
                    + " , TREGORDER.Optional2 as TREGORDER__Optional2 "
                    + " , TREGORDER.Optional3 as TREGORDER__Optional3 "
                   + ", tRegProcedure.RPDesc as tRegProcedure__RPDesc "
                   + ", tRegProcedure.BodyPart as tRegProcedure__BodyPart"
                   + ", tRegProcedure.CheckingItem as tRegProcedure__CheckingItem "
                   + ", TREGPROCEDURE.UNAPPROVEDASSIGNDATE AS TREGPROCEDURE__UNAPPROVEDASSIGNDATE"
                   + ", TREGPROCEDURE.UNAPPROVEDPREVIOUSOWNER AS TREGPROCEDURE__UNAPPROVEDPREVIOUSOWNER"
                   + ", TREGPROCEDURE.UNWRITTENASSIGNDATE AS TREGPROCEDURE__UNWRITTENASSIGNDATE"
                   + ", TREGPROCEDURE.UNWRITTENPREVIOUSOWNER AS TREGPROCEDURE__UNWRITTENPREVIOUSOWNER"
                   + ", tRegOrder.Assign2site AS tRegOrder__Assign2site"
                   + ", tRegOrder.CurrentSite AS tRegOrder__CurrentSite"
                   + ", tRegPatient.SocialSecurityNo AS tRegPatient__SocialSecurityNo"
                   + ", tRegProcedure.CheckItemName AS tRegProcedure__CheckItemName"
                   + ", tRegOrder.PhysicalCompany AS tRegOrder__PhysicalCompany"
                   + ", tRegOrder.PhysicalCompany AS tRegOrder__PhysicalCompany__Desc"
                   + ", tRegOrder.PhysicalCompany AS tRegOrder__PhysicalCompany__Telephone"
                   ;
                //_strReportInfoColumns = rptCol;


                return _strReportInfoColumns;
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetAllReportColumn=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "*";
        }
    }

    internal class GetReportInfoDAO_ORACLE : IReportDAO
    {
        public object Execute(object param)
        {
            try
            {
                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in GetReportNameDAO!"));
                }

                string reportGuid = "";
                string rpGuids = "";

                foreach (string key in paramMap.Keys)
                {
                    if (key.ToUpper() == "REPORTGUID")
                    {
                        reportGuid = paramMap[key] as string;

                        if (reportGuid == null)
                            reportGuid = "";
                    }
                    else if (key.ToUpper() == "PROCEDUREGUID")
                    {
                        rpGuids = paramMap[key] as string;

                        if (rpGuids == null)
                            rpGuids = "";
                    }
                }

                string sql = "select " + GetAllReportColumn()
                        + " from tRegPatient, tRegOrder, tProcedureCode, tRegProcedure "
                        + " left join tReport on tRegProcedure.reportGuid = tReport.reportGuid "
                        + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid "
                        + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid "
                        + " and tRegProcedure.ProcedureCode = tProcedureCode.ProcedureCode ";

                string sqlFile = " select distinct tReportFile.* from tReportFile, tReport "
                    + " where tReportFile.reportGuid = tReport.reportGuid ";



                if (reportGuid.Length > 0)
                {
                    sql += " and tReport.reportGuid = '" + reportGuid + "'";

                    sqlFile += " and tReport.reportGuid = '" + reportGuid + "'";
                }

                if (rpGuids.Length > 0 && reportGuid.Length == 0)
                {
                    rpGuids = rpGuids.Trim(", ".ToCharArray());
                    rpGuids = rpGuids.Replace(",", "','");
                    rpGuids = "'" + rpGuids + "'";

                    sql += " and tRegProcedure.ProcedureGuid in (" + rpGuids + ")";

                    sqlFile += " 1!=1";
                }

                DataSet ds = new DataSet();

                using (RisDAL dal = new RisDAL())
                {

                    //
                    // report info
                    DataTable dt0 = new DataTable("ReportInfo");



                    //ServerPubFun.DeclareOracleNoCase();

                    dal.ExecuteQuery(sql, dt0);

                    ds.Tables.Add(dt0);

                    //
                    // report file
                    DataTable dt1 = new DataTable("ReportFile");



                    dal.ExecuteQuery(sqlFile, dt1);

                    ds.Tables.Add(dt1);

                    if (dal != null)
                    {
                        dal.Dispose();
                    }
                }
                return ds;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportInfoDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return null;
        }

        public string GetAllReportColumn()
        {
            try
            {
                string sql = "select table_name as tblname, column_name as colname,"
                    + " data_type as coltype, data_length as collength"
                    + " from user_tab_cols"
                    + " where table_name in ('TREGPATIENT','TREGORDER', 'TREGPROCEDURE', 'TPROCEDURECODE', 'TREPORT')"
                    + " and column_name not like 'SYS_NC%$' ";

                DataSet ds = new DataSet();

                using (RisDAL dal = new RisDAL())
                {

                    dal.ExecuteQuery(sql, ds, "ReportColumn");

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        string rptCol = "";
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string alias = ds.Tables[0].Rows[i]["TBLNAME"].ToString() + ReportCommon.ReportCommon.FIELD_SEPERATOR + ds.Tables[0].Rows[i]["COLNAME"].ToString();
                            if (alias.Length > 30)
                            {
                                alias = alias.Substring(0, 30);
                            }

                            rptCol += ds.Tables[0].Rows[i]["TBLNAME"].ToString() + "." + ds.Tables[0].Rows[i]["COLNAME"].ToString() + " as "
                                + alias + ", ";
                        }

                        rptCol = rptCol.Trim(", ".ToCharArray());

                        if (dal != null)
                        {
                            dal.Dispose();
                        }

                        return rptCol;

                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "GetReportInfoDAO=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "*";
        }
    }
}
