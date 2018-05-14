using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataAccessLayer;
using LogServer;
using System.Web.Script.Serialization;

namespace Server.ReportDAO
{
    /// <summary>
    /// Save Report DAO
    /// </summary>
    public class SaveReportDAO
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

    internal class SaveReportDAO_ABSTRACT : IReportDAO
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

    internal class SaveReportDAO_SYBASE : IReportDAO
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

    internal class SaveReportDAO_MSSQL : IReportDAO
    {
        const string Result_Success = "0";
        const string Result_Fail = "-1";
        const string Result_Miss_Parameters = "-2";
        const string Result_DifferentRegOrder = "-3";
        const string Result_ErrorStatus = "-4";
        const string Result_RPnotExist = "-5";
        static bool kkkkk = true;
        static int iWrittenCount = 0;

        public object Execute(object param)
        {
            string errMSG = "";
            int iTestStep = 0;

            try
            {
                iTestStep = 100;

                Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                if (paramMap == null || paramMap.Count < 1)
                {
                    throw (new Exception("No parameter in SaveReportDAO!"));
                }

                using (RisDAL dal = new RisDAL())
                {

                    string rpGuids = "", curUserGuid = "", clientIP = "", UnapprovedCurrentOwner = "", isReturnVisit = "", printTemplateGuid = "";
                    int nextStatus = 0;
                    bool isRebuild = false;
                    bool bOverwriteOperator = true;
                    DataSet dsReportInfo = null;
                    bool isDataSigned = false;
                    DataTable dtSignModel = null;
                    string actionDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    bool isCancelSubmit = false;

                    #region Added by Kevin For SR

                    string strReportContent = "", strReportItem = "", strSrReportText = "", strNaturalText = "";

                    #endregion

                    iTestStep = 200;

                    foreach (string key in paramMap.Keys)
                    {
                        if (key.ToUpper() == "NEXTSTATUS")
                        {
                            nextStatus = System.Convert.ToInt32(paramMap[key] as string);
                        }
                        else if (key.ToUpper() == "PROCEDUREGUID")
                        {
                            rpGuids = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "ISREBUILD")
                        {
                            isRebuild = System.Convert.ToBoolean(paramMap[key] as string);
                        }
                        else if (key.ToUpper() == "DATASET")
                        {
                            dsReportInfo = paramMap[key] as DataSet;
                            if (dsReportInfo != null && dsReportInfo.Tables.Contains("SignHistoryModel"))
                            {
                                dtSignModel = dsReportInfo.Tables["SignHistoryModel"];
                            }
                        }
                        else if (key.ToUpper() == "USERID")
                        {
                            curUserGuid = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "CLIENTIP")
                        {
                            clientIP = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "BOVERWRITEOPERATOR")
                        {
                            bOverwriteOperator = System.Convert.ToBoolean(paramMap[key] as string);
                        }
                        else if (key.ToUpper() == "UNAPPROVEDCURRENTOWNER")
                        {
                            UnapprovedCurrentOwner = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "ISDATASIGNED")
                        {
                            isDataSigned = System.Convert.ToBoolean(paramMap[key] as string);
                        }
                        else if (key.ToUpper() == "ACTIONDATETIME")
                        {
                            actionDateTime = System.Convert.ToString(paramMap[key] as string);
                        }
                        else if (key.ToUpper() == "ISRETURNVISIT")
                        {
                            isReturnVisit = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "PRINTTEMPLATEGUID")
                        {
                            printTemplateGuid = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "ISCANCELSUBMIT")
                        {
                            isCancelSubmit = System.Convert.ToBoolean(paramMap[key] as string);
                        }
                        else if (key.ToUpper() == "SRTEMPLATEINFO")
                        {
                            strReportContent = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "SRHTML")
                        {
                            strSrReportText = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "SRELEMENTJSON")
                        {
                            strReportItem = paramMap[key] as string;
                        }
                        else if (key.ToUpper() == "SRNATURALHTML")
                        {
                            strNaturalText = paramMap[key] as string;
                        }
                    }

                    iTestStep = 300;

                    if (dsReportInfo == null ||
                        curUserGuid == null || curUserGuid.Length < 1)
                    {
                        System.Diagnostics.Debug.Assert(false, "Missing Parameter!");
                        throw (new Exception("Missing Parameter!"));
                    }

                    if (clientIP == null)
                        clientIP = "";

                    iTestStep = 400;

                    rpGuids = rpGuids.Trim(",; ".ToCharArray());
                    string[] rpList = rpGuids.Split(',');

                    if (dsReportInfo == null || dsReportInfo.Tables.Count < 1 || dsReportInfo.Tables[0].Rows.Count < 1)
                        return "Result = " + Result_Miss_Parameters;


                    bool bExist = true, bSameOrder = true;

                    //判断RP是否存在并且是同一个ORDER
                    ExistAndSameOrder(rpList, ref bExist, ref bSameOrder);

                    if (!bExist)
                    {
                        ServerPubFun.RISLog_Error(0, "RPs do not exist, " + rpList.ToString(), "", 0);

                        return "Result=" + Result_RPnotExist;
                    }

                    if (!bSameOrder)
                    {

                        return "Result = " + Result_DifferentRegOrder;
                    }

                    string reportGuid, reportName, wys, wyg, appendInfo, techInfo, reportText,
                        reportQuality, accordRate, scoringVersion, scoringSelectedTexts, reportQuality2, strDomain, rejecter, rejectToObject, doctorAdvice,
                        acrCode, acrAnatomic, acrPathologic, keyWord, isPositive, isDiagnosisRight,
                        comments, txtWYS, txtWYG, checkitemName, OrderGuid, VisitGuid, observation,
                        inspection, reportQualityComments, createrName, submitterName, UnwrittenCurrentOwner, UnApprovedCurrentOwner, menderName, assign2Site, isModified;

                    reportGuid = reportName = wys = wyg = appendInfo = techInfo = reportText
                        = reportQuality = accordRate = scoringVersion = scoringSelectedTexts = reportQuality2 = strDomain = rejecter = rejectToObject = doctorAdvice
                        = acrCode = acrAnatomic = acrPathologic = keyWord = isPositive = isDiagnosisRight
                        = comments = txtWYS = txtWYG = checkitemName = OrderGuid = VisitGuid = observation
                        = inspection = reportQualityComments = createrName = submitterName = UnwrittenCurrentOwner = UnApprovedCurrentOwner = menderName = assign2Site = isModified = "";


                    string strSubmitter = null;
                    DateTime? submitDt = null;

                    iTestStep = 500;

                    #region get field value

                    DataRow dr = dsReportInfo.Tables[0].Rows[0];

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITDT)
                        && dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITDT] != DBNull.Value)
                    {
                        submitDt = Convert.ToDateTime(dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITDT]);
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTGUID))
                    {
                        reportGuid = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTGUID] as string;
                    }



                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTNAME))
                    {
                        reportName = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTNAME] as string;
                        if (reportName == null)
                        {
                            reportName = "";
                        }
                        else
                        {
                            //  reportName = ReportCommon.ReportCommon.StringLeft_ANSI(reportName,
                            //     ServerPubFun.GetColumnWidth("tReport", "reportName"));
                        }
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_WYS))
                    {
                        Byte[] buff = dr[ReportCommon.ReportCommon.FIELDNAME_WYS] as Byte[];
                        if (buff == null)
                            wys = "";
                        else
                            wys = ReportCommon.Converter.GetStringFromBytes(buff);
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_WYG))
                    {
                        Byte[] buff = dr[ReportCommon.ReportCommon.FIELDNAME_WYG] as Byte[];
                        if (buff == null)
                            wyg = "";
                        else
                            wyg = ReportCommon.Converter.GetStringFromBytes(buff);
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_WYSTEXT))
                    {
                        txtWYS = dr[ReportCommon.ReportCommon.FIELDNAME_WYSTEXT] as string;

                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_WYGTEXT))
                    {
                        txtWYG = dr[ReportCommon.ReportCommon.FIELDNAME_WYGTEXT] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_APPENDINFO))
                    {
                        Byte[] buff = dr[ReportCommon.ReportCommon.FIELDNAME_APPENDINFO] as Byte[];
                        if (buff == null)
                            appendInfo = "";
                        else
                            appendInfo = ReportCommon.Converter.GetStringFromBytes(buff);
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_TECHINFO))
                    {
                        techInfo = System.Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_TECHINFO]);
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTTEXT))
                    {
                        reportText = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTTEXT] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_DOCTORADVICE))
                    {
                        doctorAdvice = dr[ReportCommon.ReportCommon.FIELDNAME_DOCTORADVICE] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ACRCODE))
                    {
                        acrCode = dr[ReportCommon.ReportCommon.FIELDNAME_ACRCODE] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ACRANATOMIC))
                    {
                        acrAnatomic = dr[ReportCommon.ReportCommon.FIELDNAME_ACRANATOMIC] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ACRPATHOLOGIC))
                    {
                        acrPathologic = dr[ReportCommon.ReportCommon.FIELDNAME_ACRPATHOLOGIC] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_KEYWORD))
                    {
                        keyWord = dr[ReportCommon.ReportCommon.FIELDNAME_KEYWORD] as string;
                        if (keyWord == null)
                        {
                            keyWord = "";
                        }
                        else
                        {
                            // keyWord = ReportCommon.ReportCommon.StringLeft_ANSI(keyWord,
                            //    ServerPubFun.GetColumnWidth("tReport", "Keyword"));
                        }
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ISPOSITIVE))
                    {
                        isPositive = dr[ReportCommon.ReportCommon.FIELDNAME_ISPOSITIVE].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ISDIAGNOSISRIGHT))
                    {
                        isDiagnosisRight = dr[ReportCommon.ReportCommon.FIELDNAME_ISDIAGNOSISRIGHT].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_ISMODIFIED))
                    {
                        isModified = Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_ISMODIFIED]);
                        if (isModified == null)
                        {
                            isModified = "0";
                        }
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTREJECTER))
                    {
                        rejecter = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTREJECTER].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REJECTTOOBJECT))
                    {
                        rejectToObject = dr[ReportCommon.ReportCommon.FIELDNAME_REJECTTOOBJECT].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTQUALITY))
                    {
                        reportQuality = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTQUALITY].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ACCORDRATE))
                    {
                        accordRate = dr[ReportCommon.ReportCommon.FIELDNAME_ACCORDRATE].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_SCORINGVERSION))
                    {
                        scoringVersion = dr[ReportCommon.ReportCommon.FIELDNAME_SCORINGVERSION].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains("ScoringSelectedTexts"))
                    {
                        scoringSelectedTexts = dr["ScoringSelectedTexts"].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTQUALITY + "2"))
                    {
                        reportQuality2 = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTQUALITY + "2"].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_CHECKITEMNAME))
                    {
                        checkitemName = dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_CHECKITEMNAME].ToString();
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_VISITGUID))
                    {
                        VisitGuid = dr[ReportCommon.ReportCommon.FIELDNAME_VISITGUID] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ORDERGUID))
                    {
                        OrderGuid = dr[ReportCommon.ReportCommon.FIELDNAME_ORDERGUID] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_OBSERVATION))
                    {
                        observation = dr[ReportCommon.ReportCommon.FIELDNAME_OBSERVATION] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_INSPECTION))
                    {
                        inspection = dr[ReportCommon.ReportCommon.FIELDNAME_INSPECTION] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITTER))
                    {
                        if (dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITTER] != null)
                            strSubmitter = dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITTER] as string;

                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_CREATERNAME))
                    {
                        if (dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_CREATERNAME] != DBNull.Value)
                            createrName = dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_CREATERNAME] as string;

                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITTERNAME))
                    {
                        if (dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITTERNAME] != DBNull.Value)
                            submitterName = dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_SUBMITTERNAME] as string;

                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_MENDERNAME))
                    {
                        if (dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_MENDERNAME] != DBNull.Value)
                            menderName = dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_MENDERNAME] as string;

                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_tReport__ReportQualityComments))
                    {
                        reportQualityComments = Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_tReport__ReportQualityComments]);
                        if (reportQualityComments == null)
                        {
                            reportQualityComments = "";
                        }
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_tRegProcedure__UnwrittenCurrentOwner))
                    {
                        UnwrittenCurrentOwner = Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_tRegProcedure__UnwrittenCurrentOwner]);
                        if (UnwrittenCurrentOwner == null)
                        {
                            UnwrittenCurrentOwner = "";
                        }
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_tRegProcedure__UnapprovedCurrentOwner))
                    {
                        UnApprovedCurrentOwner = Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_tRegProcedure__UnapprovedCurrentOwner]);
                        if (UnApprovedCurrentOwner == null)
                        {
                            UnApprovedCurrentOwner = "";
                        }
                    }

                    if (string.IsNullOrWhiteSpace(printTemplateGuid))
                    {
                        printTemplateGuid = System.Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_PRINTTEMPLATEGUID]);
                    }

                    int nIsLeaveWord = 0;
                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_COMMENTS))
                    {
                        comments = System.Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_COMMENTS]);
                        if (comments == null)
                        {
                            comments = "";

                        }

                        if (comments.Length > 0)
                        {
                            nIsLeaveWord = 1;
                        }

                    }

                    //
                    //validate
                    if (isPositive != "1" && isPositive != "2" && !string.IsNullOrEmpty(isPositive))
                    {
                        isPositive = "0";
                    }

                    if (isDiagnosisRight != "1" && isDiagnosisRight != "2" && !string.IsNullOrEmpty(isDiagnosisRight))
                    {
                        isDiagnosisRight = "0";
                    }


                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTDOMAIN))
                    {
                        strDomain = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTDOMAIN] as string;
                    }

                    if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ORDER_ASSIGN2SITE))
                    {
                        assign2Site = System.Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_ORDER_ASSIGN2SITE]);
                        if (assign2Site == null)
                        {
                            assign2Site = "";
                        }
                    }


                    #endregion

                    iTestStep = 600;

                    if (isNewReport(rpList))
                    #region Create Report
                    {
                        iTestStep = 601;

                        if (!CanSaveNewReport(nextStatus))
                        {
                            return "Result = " + Result_ErrorStatus;
                        }

                        iTestStep = 602;



                        iTestStep = 603;


                        reportName = MakeReportName(rpList);

                        iTestStep = 604;

                        //
                        //database operation
                        string sql = "";


                        iTestStep = 605;

                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.Clear();

                        if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft))
                        {
                            sql = string.Format("insert into tReport(ReportGuid,ReportName,IsPositive,"
                                + " AcrCode,AcrAnatomic,AcrPathologic,IsDiagnosisRight,ReportQuality,AccordRate,ScoringVersion,ReportQuality2,Status,Domain,"
                                + "DeleteMark,IsPrint,WYS,WYG,WYSText,WYGText,AppendInfo,TechInfo,ReportText,DoctorAdvice,Comments,Keyword,"
                                + "CheckitemName,IsLeaveWord,Creater,CreateDt,ReportQualityComments,CreaterName,Optional3) "
                                + "values(@ReportGuid,@ReportName,@IsPositive,@AcrCode,@AcrAnatomic,@AcrPathologic,"
                                + "@IsDiagnosisRight,@ReportQuality,@AccordRate,@ScoringVersion,@ReportQuality2,@Status,@Domain,@DeleteMark,@IsPrint,@WYS,@WYG,@WYSText,"
                                + "@WYGText,@AppendInfo,@TechInfo,@ReportText,@DoctorAdvice,@Comments,@Keyword,@CheckitemName,"
                                + "@IsLeaveWord,@Creater,@ActionDt,@ReportQualityComments,@CreaterName,@IsReturnVisit)");

                            cmd.CommandText = sql;
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@ReportName", reportName);
                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", nextStatus);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);
                            cmd.Parameters.AddWithValue("@DeleteMark", 0);
                            cmd.Parameters.AddWithValue("@IsPrint", 0);
                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            if (wys == "")
                            {
                                cmd.Parameters.AddWithValue("@ReportText", reportText);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@ReportText", strSrReportText);
                            }
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@Creater", curUserGuid);
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@CreaterName", createrName);
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);
                        }
                        if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                        {

                            sql = string.Format("insert into tReport(ReportGuid,ReportName,IsPositive,"
                             + " AcrCode,AcrAnatomic,AcrPathologic,IsDiagnosisRight,ReportQuality,AccordRate,ScoringVersion,ReportQuality2,Status,Domain,"
                             + "DeleteMark,IsPrint,WYS,WYG,WYSText,WYGText,AppendInfo,TechInfo,ReportText,DoctorAdvice,Comments,Keyword,"
                             + "CheckitemName,IsLeaveWord,Creater,CreateDt,submitDT, submitter,SubmitDomain,SubmitSite,ReportQualityComments,CreaterName,SubmitterName,Optional3) "
                             + "values(@ReportGuid,@ReportName,@IsPositive,@AcrCode,@AcrAnatomic,@AcrPathologic,"
                             + "@IsDiagnosisRight,@ReportQuality,@AccordRate,@ScoringVersion,@ReportQuality2,@Status,@Domain,@DeleteMark,@IsPrint,@WYS,@WYG,@WYSText,"
                             + "@WYGText,@AppendInfo,@TechInfo,@ReportText,@DoctorAdvice,@Comments,@Keyword,@CheckitemName,"
                             + "@IsLeaveWord,@Creater,@ActionDt,@ActionDt, @submitter,@SubmitDomain,@SubmitSite,@ReportQualityComments,@CreaterName,@SubmitterName,@IsReturnVisit);");

                            cmd.CommandText = sql;
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@ReportName", reportName);
                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", nextStatus);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);
                            cmd.Parameters.AddWithValue("@DeleteMark", 0);
                            cmd.Parameters.AddWithValue("@IsPrint", 0);
                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            cmd.Parameters.AddWithValue("@ReportText", reportText);
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@Creater", curUserGuid);
                            cmd.Parameters.AddWithValue("@submitter", curUserGuid);
                            cmd.Parameters.AddWithValue("@SubmitDomain", strDomain);
                            cmd.Parameters.AddWithValue("@SubmitSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@CreaterName", createrName);
                            cmd.Parameters.AddWithValue("@SubmitterName", submitterName);
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                        {

                            sql = string.Format("insert into tReport(ReportGuid,ReportName,IsPositive,"
                                  + " AcrCode,AcrAnatomic,AcrPathologic,IsDiagnosisRight,ReportQuality,AccordRate,ScoringVersion,ReportQuality2,Status,Domain,"
                                  + "DeleteMark,IsPrint,WYS,WYG,WYSText,WYGText,AppendInfo,TechInfo,ReportText,DoctorAdvice,Comments,Keyword,"
                                  + "CheckitemName,IsLeaveWord,Creater,CreateDt,submitDT, submitter,firstApproveDT,"
                                  + " firstApprover,SubmitDomain,FirstApproveDomain,SubmitSite,FirstApproveSite,ReportQualityComments,Optional3,IsModified,PrintTemplateGuid) "
                                  + "values(@ReportGuid,@ReportName,@IsPositive,@AcrCode,@AcrAnatomic,@AcrPathologic,"
                                  + "@IsDiagnosisRight,@ReportQuality,@AccordRate,@ScoringVersion,@ReportQuality2,@Status,@Domain,@DeleteMark,@IsPrint,@WYS,@WYG,@WYSText,"
                                  + "@WYGText,@AppendInfo,@TechInfo,@ReportText,@DoctorAdvice,@Comments,@Keyword,"
                                  + "@CheckitemName,@IsLeaveWord,@Creater,@ActionDt,@ActionDt, @submitter,@ActionDt,"
                                  + " @firstApprover,@SubmitDomain,@FirstApproveDomain,@SubmitSite,@FirstApproveSite,@ReportQualityComments,@IsReturnVisit,@IsModified,@PrintTemplateGuid)");

                            cmd.CommandText = sql;
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@ReportName", reportName);
                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", nextStatus);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);
                            cmd.Parameters.AddWithValue("@DeleteMark", 0);
                            cmd.Parameters.AddWithValue("@IsPrint", 0);
                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            cmd.Parameters.AddWithValue("@ReportText", reportText);
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@Creater", curUserGuid);
                            cmd.Parameters.AddWithValue("@submitter", curUserGuid);
                            cmd.Parameters.AddWithValue("@firstApprover", curUserGuid);
                            cmd.Parameters.AddWithValue("@SubmitDomain", strDomain);
                            cmd.Parameters.AddWithValue("@FirstApproveDomain", strDomain);
                            cmd.Parameters.AddWithValue("@SubmitSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@FirstApproveSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);
                            cmd.Parameters.AddWithValue("@IsModified", isModified);
                            cmd.Parameters.AddWithValue("@PrintTemplateGuid", printTemplateGuid);
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                        {
                            sql = string.Format("insert into tReport(ReportGuid,ReportName,IsPositive,"
                              + " AcrCode,AcrAnatomic,AcrPathologic,IsDiagnosisRight,ReportQuality,AccordRate,ScoringVersion,ReportQuality2,Status,Domain,"
                              + "DeleteMark,IsPrint,WYS,WYG,WYSText,WYGText,AppendInfo,TechInfo,ReportText,DoctorAdvice,Comments,Keyword,"
                              + "CheckitemName,IsLeaveWord,Creater,CreateDt,submitDT, submitter,firstApproveDT,"
                              + " firstApprover,secondApproveDT, secondApprover,SubmitDomain,FirstApproveDomain,SecondApproveDomain,SubmitSite,FirstApproveSite,SecondApproveSite,ReportQualityComments,Optional3,IsModified,PrintTemplateGuid) "
                              + "values(@ReportGuid,@ReportName,@IsPositive,@AcrCode,@AcrAnatomic,@AcrPathologic,"
                              + "@IsDiagnosisRight,@ReportQuality,@AccordRate,@ScoringVersion,@ReportQuality2,@Status,@Domain,@DeleteMark,@IsPrint,@WYS,@WYG,@WYSText,"
                              + "@WYGText,@AppendInfo,@TechInfo,@ReportText,@DoctorAdvice,@Comments,@Keyword,"
                              + "@CheckitemName,@IsLeaveWord,@Creater,@ActionDt,@ActionDt, @submitter,@ActionDt,"
                              + " @firstApprover,@ActionDt, @secondApprover,@SubmitDomain,@FirstApproveDomain,@SecondApproveDomain,"
                              + " @SubmitSite, @FirstApproveSite, @SecondApproveSite,@ReportQualityComments,@IsReturnVisit,@IsModified,@PrintTemplateGuid");


                            cmd.CommandText = sql;
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@ReportName", reportName);
                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", nextStatus);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);
                            cmd.Parameters.AddWithValue("@DeleteMark", 0);
                            cmd.Parameters.AddWithValue("@IsPrint", 0);
                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            cmd.Parameters.AddWithValue("@ReportText", reportText);
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@Creater", curUserGuid);
                            cmd.Parameters.AddWithValue("@submitter", curUserGuid);
                            cmd.Parameters.AddWithValue("@firstApprover", curUserGuid);
                            cmd.Parameters.AddWithValue("@secondApprover", curUserGuid);
                            cmd.Parameters.AddWithValue("@SubmitDomain", strDomain);
                            cmd.Parameters.AddWithValue("@FirstApproveDomain", strDomain);
                            cmd.Parameters.AddWithValue("@SecondApproveDomain", strDomain);
                            cmd.Parameters.AddWithValue("@SubmitSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@FirstApproveSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@SecondApproveSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);
                            cmd.Parameters.AddWithValue("@IsModified", isModified);
                            cmd.Parameters.AddWithValue("@PrintTemplateGuid", printTemplateGuid);
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                        {
                            sql = string.Format("insert into tReport(ReportGuid,ReportName,IsPositive,"
                           + " AcrCode,AcrAnatomic,AcrPathologic,IsDiagnosisRight,ReportQuality,AccordRate,ScoringVersion,ReportQuality2,Status,Domain,"
                           + "DeleteMark,IsPrint,WYS,WYG,WYSText,WYGText,AppendInfo,TechInfo,ReportText,DoctorAdvice,Comments,Keyword,"
                           + "CheckitemName,IsLeaveWord,Creater,CreateDt,RejectDT, Rejecter, rejectToObject,RejectDomain,RejectSite,ReportQualityComments,Optional3) "
                           + "values(@ReportGuid,@ReportName,@IsPositive,@AcrCode,@AcrAnatomic,@AcrPathologic,"
                           + "@IsDiagnosisRight,@ReportQuality,@AccordRate,@ScoringVersion,@ReportQuality2,@Status,@Domain,@DeleteMark,@IsPrint,@WYS,@WYG,@WYSText,"
                           + "@WYGText,@AppendInfo,@TechInfo,@ReportText,@DoctorAdvice,@Comments,@Keyword,@CheckitemName,"
                           + "@IsLeaveWord,@Creater,@ActionDt,@ActionDt,@Rejecter,@rejectToObject,@RejectDomain,@RejectSite,@ReportQualityComments,@IsReturnVisit);");

                            cmd.CommandText = sql;
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@ReportName", reportName);
                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", nextStatus);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);
                            cmd.Parameters.AddWithValue("@DeleteMark", 0);
                            cmd.Parameters.AddWithValue("@IsPrint", 0);
                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            cmd.Parameters.AddWithValue("@ReportText", reportText);
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@Creater", curUserGuid);
                            cmd.Parameters.AddWithValue("@Rejecter", rejecter);
                            cmd.Parameters.AddWithValue("@rejectToObject", rejectToObject);
                            cmd.Parameters.AddWithValue("@RejectDomain", strDomain);
                            cmd.Parameters.AddWithValue("@RejectSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);

                            for (int i = 0; i < rpList.GetLength(0); i++)
                            {
                                string rpguid = rpList.GetValue(i) as string;
                                if (!string.IsNullOrWhiteSpace(rpguid))
                                {
                                    sql += string.Format(" update tRegProcedure set UnwrittenCurrentOwner = '{0}', UnwrittenAssignDate = '{1}' where ProcedureGuid = '{2}'; ", rejectToObject, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), rpguid);
                                }
                            }
                        }

                        //save scoring info into tScoringResult
                        if (!string.IsNullOrWhiteSpace(scoringSelectedTexts))
                        {
                            sql += string.Format(" Update tScoringResult set IsFinalVersion =0 where IsFinalVersion = 1 and ObjectGuid in ('{0}') and Type='{1}' \r\n ", reportGuid, "2");
                            sql += string.Format("Insert into tScoringResult(Guid,ObjectGuid,Type,Result,Domain,Result2,Appraiser,Comment,AccordRate) values(NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                reportGuid, 2, scoringSelectedTexts.Replace("'", "''"), CommonGlobalSettings.Utilities.GetCurDomain(), reportQuality, curUserGuid, reportQualityComments, accordRate) + "\r\n";
                        }

                        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                        {
                            conn.Open();
                            cmd.Connection = conn;



                            string sql1 = "";
                            string sql2 = "";

                            for (int i = 0; i < rpList.GetLength(0); i++)
                            {
                                string rpguid = rpList.GetValue(i) as string;
                                if (rpguid != null && (rpguid = rpguid.ToString().Trim()).Length > 0)
                                {

                                    if (nextStatus == Convert.ToInt32(ReportCommon.RP_Status.Submit) && !string.IsNullOrWhiteSpace(UnapprovedCurrentOwner))
                                    {
                                        sql2 += " update tRegProcedure set status = " + nextStatus.ToString() + ",ReportGuid='" + reportGuid + "'" + ",UnapprovedCurrentOwner='" + UnapprovedCurrentOwner + "'" + ",UnapprovedAssignDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                                            + " where ProcedureGuid='" + rpguid + "' \r\n";
                                    }
                                    else
                                    {
                                        sql2 += " update tRegProcedure set status = " + nextStatus.ToString() + ",ReportGuid='" + reportGuid + "'"
                                            + " where ProcedureGuid='" + rpguid + "' \r\n";
                                    }
                                }
                            }

                            string sqlFile = MakeSQL4ReportFile(dsReportInfo, reportGuid);

                            //sql += " end \r\n";

                            iTestStep = 608;


                            iTestStep = 609;

                            try
                            {
                                //
                                // begin transaction
                                cmd.Transaction = conn.BeginTransaction(ServerPubFun.GetIsolationLevel());

                                iTestStep = 620;
                                ServerPubFun.RISLog_Info(0, "Create report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();

                                #region Added by Kevin For SR after report inserted

                                if (strReportContent != "")
                                {

                                    StructuredReportDAO.AddReportContent(strReportContent, reportGuid, curUserGuid, nextStatus, strDomain, strNaturalText,
                                        ref cmd);
                                    cmd.ExecuteNonQuery();
                                    if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove) ||
                                        nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                                    {
                                        StructuredReportDAO.AddReportItem(strReportItem, reportGuid, ref cmd);
                                    }
                                    ServerPubFun.RISLog_Info(0,
                                        "Create report and add reportcontent, iTestStep=" + iTestStep.ToString() +
                                        ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);
                                }

                                #endregion

                                iTestStep = 621;
                                ServerPubFun.RISLog_Info(0, "Create report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                // cmd.CommandText = sql1;
                                // cmd.ExecuteNonQuery();

                                // iTestStep = 623;
                                // ServerPubFun.RISLog_Info(0, "Create report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                cmd.CommandText = sql2;
                                cmd.ExecuteNonQuery();

                                iTestStep = 624;
                                ServerPubFun.RISLog_Info(0, "Create report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                cmd.CommandText = sqlFile;
                                cmd.ExecuteNonQuery();

                                if (observation != null)
                                {
                                    //#US27608
                                    //cmd.CommandText = string.Format("Update tRegOrder set Observation ='{0}' where OrderGuid='{1}'", observation, OrderGuid);
                                    cmd.CommandText = string.Format("Update tRegOrder set Observation =@observation where OrderGuid=@OrderGuidOfObservation");
                                    cmd.Parameters.AddWithValue("@observation", observation);
                                    cmd.Parameters.AddWithValue("@OrderGuidOfObservation", OrderGuid);
                                    cmd.ExecuteNonQuery();

                                }

                                if (inspection != null)
                                {
                                    //#US27608
                                    //cmd.CommandText = string.Format("Update tRegOrder set Optional2 = '{0}' where OrderGuid='{1}'", inspection, OrderGuid); //optional2 now use for inspection
                                    cmd.CommandText = string.Format("Update tRegOrder set Optional2 =@inspection where OrderGuid=@OrderGuidOfInspection");
                                    cmd.Parameters.AddWithValue("@inspection", observation);
                                    cmd.Parameters.AddWithValue("@OrderGuidOfInspection", OrderGuid);
                                    cmd.ExecuteNonQuery();
                                }

                                iTestStep = 625;
                                ServerPubFun.RISLog_Info(0, "Create report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                {
                                    //make sure all reports have been approved
                                    cmd.CommandText = string.Format(@"if not exists (select 1 from tRegProcedure rp inner join tRegOrder ro on rp.OrderGuid = ro.OrderGuid
                                                            where rp.OrderGuid= '{0}' and (rp.Status <> {1} or (ro.IsReferral >0 and rp.Status = {1}))) 
                                                                    Update tRegOrder set CurrentSite = examsite where OrderGuid='{0}'",
                                                                    OrderGuid, nextStatus.ToString()); //update currentSite
                                    //cmd.CommandText = string.Format("Update tRegOrder set CurrentSite = examsite where OrderGuid='{0}' and isReferral < 1", OrderGuid); //update CurrentSite
                                    cmd.ExecuteNonQuery();
                                }

                                cmd.Transaction.Commit();
                                //end transaction
                                //


                                iTestStep = 626;
                                ServerPubFun.RISLog_Info(0, "Create report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                            }
                            catch (Exception ex)
                            {
                                cmd.Transaction.Rollback();

                                string logText = "\r\nrpGuids=" + rpGuids
                                    + "\r\ncurUserGuid=" + curUserGuid
                                    + "\r\nnextStatus=" + nextStatus.ToString()
                                    + "\r\nisRebuild=" + isRebuild.ToString()
                                    + "\r\nreportGuid=" + reportGuid
                                    + "\r\nreportName=" + reportName
                                    + "\r\nwys=" + wys
                                    + "\r\nwyg=" + wyg
                                    + "\r\nappendInfo=" + appendInfo
                                    + "\r\ntechInfo=" + techInfo
                                    + "\r\nreportText=" + reportText
                                    + "\r\nreportQuality=" + reportQuality
                                    + "\r\naccordRate=" + accordRate
                                    + "\r\nscoringVersion=" + scoringVersion
                                    + "\r\nDomain=" + strDomain
                                    + "\r\nrejecter=" + rejecter
                                    + "\r\nrejectToObject=" + rejectToObject
                                    + "\r\ndoctorAdvice=" + doctorAdvice
                                    + "\r\nacrCode=" + acrCode
                                    + "\r\nacrAnatomic=" + acrAnatomic
                                    + "\r\nacrPathologic=" + acrPathologic
                                    + "\r\nkeyWord=" + keyWord
                                    + "\r\nisPositive=" + isPositive
                                    + "\r\nisDiagnosisRight=" + isDiagnosisRight
                                    + "\r\ncomments=" + comments
                                    + "\r\ntxtWYS=" + txtWYS
                                    + "\r\ntxtWYG=" + txtWYG
                                    + "\r\ncheckitemName=" + checkitemName
                                    + "\r\nSQL=" + sql;

                                ServerPubFun.RISLog_Error(0, "Error on CreateReport, MSG=" + ex.Message + ", iTestStep=" + iTestStep.ToString() + ", \r\nReportContent=" + logText, "", 0);



                                throw (ex);
                            }

                        }
                        iTestStep = 660;

                        // Log & Integration
                        tagReportInfo rptInfo = ServerPubFun.GetReportInfo(reportGuid);

                        iTestStep = 663;

                        OnCreate(rptInfo);

                        iTestStep = 666;

                        if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                        {
                            OnSubmit(rptInfo, ReportCommon.RP_Status.Examination);
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                        {
                            OnApprove(rptInfo, ReportCommon.RP_Status.Examination, dtSignModel);
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                        {
                            OnSecondApprove(rptInfo, ReportCommon.RP_Status.Examination, dtSignModel);
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                        {
                            OnReject(rptInfo, ReportCommon.RP_Status.Examination);
                        }

                        iTestStep = 670;
                        ServerPubFun.RISLog_Info(0, "Create report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);
                    }
                    #endregion
                    else
                    #region Save Report
                    {
                        iTestStep = 700;

                        if (nextStatus != System.Convert.ToInt32(ReportCommon.RP_Status.Submit) &&
                            nextStatus != System.Convert.ToInt32(ReportCommon.RP_Status.Reject) &&
                            nextStatus != System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove) &&
                            nextStatus != System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                        {
                            nextStatus = System.Convert.ToInt32(ReportCommon.RP_Status.Draft);
                        }

                        if (!CanSaveReport(reportGuid, nextStatus))
                        {
                            return "Result = " + Result_ErrorStatus;
                        }

                        iTestStep = 702;


                        if (!isExistsRPbyReportID(reportGuid))
                        {
                            ServerPubFun.RISLog_Error(0, "RPs do not exist, " + rpList.ToString(), "", 0);

                            return "Result=" + Result_RPnotExist;
                        }

                        iTestStep = 705;
                        int nOldStatus = ServerPubFun.GetReportOldStatus(reportGuid);
                        //tagReportInfo oldReportInfo = ServerPubFun.GetReportInfo(reportGuid);//? Bruce



                        iTestStep = 708;

                        bool isReplaceApproverDtOnRebuild = ServerPubFun.GetSystemProfile_Bool(ReportCommon.ProfileName.RebuildReport_Replace4ApproverAndDt, ReportCommon.ModuleID.Report);


                        iTestStep = 712;

                        string sql = "";


                        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                        cmd.CommandTimeout = 0;

                        if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft))
                        {

                            if (isCancelSubmit)
                            {
                                sql = string.Format("update tReport set status = {0} where reportguid ='{1}'", System.Convert.ToInt32(ReportCommon.RP_Status.Draft), reportGuid);
                            }
                            else
                            {
                                if (bOverwriteOperator)
                                {
                                    sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                       + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                       + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                       + " creater=@creater,createDt=@ActionDt,WYS=@wys,WYG = @wyg,"
                                       + " WYSText = @WYSText, WYGText = @WYGText,"
                                       + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                       + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                       + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                       + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null,ReportQualityComments=@ReportQualityComments,CreaterName=@CreaterName,MenderName=@MenderName,Optional3=@IsReturnVisit"
                                       + " where ReportGuid=@ReportGuid and DeleteMark = 0");

                                }
                                else
                                {
                                    sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                      + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                      + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                      + " WYS=@wys,WYG = @wyg,"
                                      + " WYSText = @WYSText, WYGText = @WYGText,"
                                      + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                      + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                      + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                      + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null,ReportQualityComments=@ReportQualityComments,MenderName=@MenderName,Optional3=@IsReturnVisit where ReportGuid=@ReportGuid ");

                                }



                                if (bOverwriteOperator)
                                {
                                    cmd.Parameters.AddWithValue("@creater", curUserGuid);
                                    cmd.Parameters.AddWithValue("@CreaterName", createrName);
                                }
                                cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                                cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                                cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                                cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                                cmd.Parameters.AddWithValue("@mender", curUserGuid);
                                cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                                cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                                cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                                cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                                cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                                cmd.Parameters.AddWithValue("@Status", nextStatus);
                                cmd.Parameters.AddWithValue("@Domain", strDomain);

                                cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                                cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                                cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                                cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                                cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                                cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                                cmd.Parameters.AddWithValue("@ReportText", reportText);
                                cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                                cmd.Parameters.AddWithValue("@Comments", comments);
                                cmd.Parameters.AddWithValue("@Keyword", keyWord);
                                cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                                cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                                cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                                cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                                cmd.Parameters.AddWithValue("@MenderName", menderName);
                                cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                                cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);
                            }
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                        {
                            sql = string.Format("update tReport set IsPositive=@IsPositive,"
                             + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                             + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                             + " WYS=@wys,WYG = @wyg,"
                                 + " WYSText = @WYSText, WYGText = @WYGText,"
                             + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                             + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                             + " Keyword = @keyword, CheckitemName = @checkitemname,"
                             + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null,ReportQualityComments=@ReportQualityComments,"
                             + " SubmitDomain=@SubmitDomain, SubmitSite=@SubmitSite,Optional3=@IsReturnVisit  where ReportGuid=@ReportGuid; ");


                            if (bOverwriteOperator)
                            {
                                sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                   + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                   + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                   + " submitter=@submitter,SubmitDt=@ActionDt,WYS=@wys,WYG = @wyg,"
                                   + " WYSText = @WYSText, WYGText = @WYGText,"
                                   + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                   + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                   + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                   + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null,"
                                   + " SubmitDomain=@SubmitDomain, SubmitSite=@SubmitSite ,ReportQualityComments=@ReportQualityComments,SubmitterName=@SubmitterName,MenderName=@MenderName,Optional3=@IsReturnVisit where ReportGuid=@ReportGuid; ");

                            }
                            else
                            {
                                sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                  + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                  + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                  + " WYS=@wys,WYG = @wyg,"
                                  + " WYSText = @WYSText, WYGText = @WYGText,"
                                  + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                  + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                  + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                  + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null,"
                                  + " SubmitDomain=@SubmitDomain, SubmitSite=@SubmitSite ,ReportQualityComments=@ReportQualityComments,MenderName=@MenderName,Optional3=@IsReturnVisit where ReportGuid=@ReportGuid; ");

                            }



                            if (bOverwriteOperator)
                            {
                                cmd.Parameters.AddWithValue("@submitter", curUserGuid);
                                cmd.Parameters.AddWithValue("@SubmitterName", submitterName);
                            }
                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@mender", curUserGuid);

                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", nextStatus);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);

                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            cmd.Parameters.AddWithValue("@ReportText", reportText);
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@SubmitDomain", strDomain);
                            string curSite = CommonGlobalSettings.Utilities.GetCurSite();
                            cmd.Parameters.AddWithValue("@SubmitSite", curSite);
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@MenderName", menderName);
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                        {
                            if (isModified == "1")
                            {
                                isModified = hasSumitedRecordInReportList(reportGuid) == true ? "1" : "0";
                            }
                            string strWhere = " where ReportGuid=@ReportGuid ";

                            if (isRebuild)
                            {

                                sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                + " WYS=@wys,WYG = @wyg,WYSText = @WYSText, WYGText = @WYGText,"
                                + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null ,"
                                + " FirstApproveDomain=@FirstApproveDomain, FirstApproveSite=@FirstApproveSite ,ReportQualityComments=@ReportQualityComments,MenderName=@MenderName,Optional3=@IsReturnVisit,IsModified=@IsModified,PrintTemplateGuid=@PrintTemplateGuid ");

                                if (isReplaceApproverDtOnRebuild)
                                {
                                    sql += "  ,firstApproveDt = @ActionDt ";
                                    sql += " ,firstApprover = '" + curUserGuid + "'";

                                }






                            }
                            else
                            {
                                sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                  + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                  + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                  + " WYS=@wys,WYG = @wyg,WYSText = @WYSText, WYGText = @WYGText,"
                                  + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                  + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                  + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                  + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null ,"
                                  + " FirstApproveDomain=@FirstApproveDomain, FirstApproveSite=@FirstApproveSite ,ReportQualityComments=@ReportQualityComments,MenderName=@MenderName,Optional3=@IsReturnVisit,IsModified=@IsModified,PrintTemplateGuid=@PrintTemplateGuid ");

                                sql += "  ,firstApproveDt = @ActionDt ";
                                sql += " ,firstApprover = '" + curUserGuid + "'";

                                if (strSubmitter == null || strSubmitter.Trim().Length == 0)
                                {
                                    sql += " ,submitDt = @ActionDt, submitter = '" + curUserGuid + "'";

                                }



                            }
                            sql += strWhere;

                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@mender", curUserGuid);
                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", nextStatus);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);

                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            cmd.Parameters.AddWithValue("@ReportText", reportText);
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@FirstApproveDomain", strDomain);
                            cmd.Parameters.AddWithValue("@FirstApproveSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@MenderName", "");//empty it!
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);
                            cmd.Parameters.AddWithValue("@IsModified", isModified);
                            cmd.Parameters.AddWithValue("@PrintTemplateGuid", printTemplateGuid);

                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                        {
                            if (isModified == "1")
                            {
                                isModified = hasSumitedRecordInReportList(reportGuid) == true ? "1" : "0";
                            }
                            string strWhere = " where ReportGuid=@ReportGuid ";

                            if (isRebuild)
                            {

                                sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                + " WYS=@wys,WYG = @wyg, WYSText = @WYSText, WYGText = @WYGText,"
                                + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null ,"
                                + " SecondApproveDomain=@SecondApproveDomain, SecondApproveSite=@SecondApproveSite ,ReportQualityComments=@ReportQualityComments,MenderName = @MenderName,Optional3=@IsReturnVisit,IsModified=@IsModified,PrintTemplateGuid=@PrintTemplateGuid ");

                                if (isReplaceApproverDtOnRebuild)
                                {
                                    sql += "  ,firstApproveDt = @ActionDt ";

                                    sql += " ,firstApprover = '" + curUserGuid + "'";

                                }

                            }
                            else
                            {
                                sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                  + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                  + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                  + " WYS=@wys,WYG = @wyg,WYSText = @WYSText, WYGText = @WYGText,"
                                  + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                  + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                  + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                  + " IsLeaveWord=@IsLeaveWord,RejectDT = null, Rejecter = null, rejectToObject = null,"
                                  + " SecondApproveDomain=@SecondApproveDomain, SecondApproveSite=@SecondApproveSite ,ReportQualityComments=@ReportQualityComments,MenderName = @MenderName,Optional3=@IsReturnVisit,IsModified=@IsModified,PrintTemplateGuid=@PrintTemplateGuid  ");

                                sql += "  ,secondApproveDt = @ActionDt ";
                                sql += " ,secondApprover = '" + curUserGuid + "'";

                                if (strSubmitter == null || strSubmitter.Trim().Length == 0)
                                {
                                    sql += " ,submitDt = @ActionDt, submitter = '" + curUserGuid + "'";

                                }



                            }
                            sql += strWhere;

                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@mender", curUserGuid);
                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", 120);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);

                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            cmd.Parameters.AddWithValue("@ReportText", reportText);
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@SecondApproveDomain", strDomain);
                            cmd.Parameters.AddWithValue("@SecondApproveSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@MenderName", "");//empty it!
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);
                            cmd.Parameters.AddWithValue("@IsModified", isModified);
                            cmd.Parameters.AddWithValue("@PrintTemplateGuid", printTemplateGuid);


                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                        {


                            sql = string.Format("update tReport set IsPositive=@IsPositive,"
                                   + " AcrCode=@AcrCode,AcrAnatomic=@AcrAnatomic,AcrPathologic=@AcrPathologic,mender=@mender,"
                                   + " modifyDt=@ActionDt,IsDiagnosisRight=@IsDiagnosisRight,ReportQuality=@ReportQuality,AccordRate=@AccordRate,ScoringVersion=@ScoringVersion,ReportQuality2=@ReportQuality2,Status=@Status,Domain=@Domain,"
                                   + " WYS=@wys,WYG = @wyg,"
                                   + " WYSText = @WYSText, WYGText = @WYGText,"
                                   + " AppendInfo = @AppendInfo, TechInfo = @TechInfo, ReportText = @ReportText,"
                                   + " DoctorAdvice = @DoctorAdvice, Comments = @comments,"
                                   + " Keyword = @keyword, CheckitemName = @checkitemname,"
                                   + " Submitter=@Submitter,SubmitDt = @SubmitDt,"
                                   + " IsLeaveWord=@IsLeaveWord,MenderName=@MenderName,Optional3=@IsReturnVisit,");

                            if (bOverwriteOperator)
                                sql += " RejectDT = @ActionDt,";



                            sql += " Rejecter = @Rejecter, rejectToObject = @rejectToObject,RejectDomain=@RejectDomain"
                                + " , RejectSite=@RejectSite ,ReportQualityComments=@ReportQualityComments "
                                + "  where ReportGuid=@ReportGuid; ";


                            cmd.Parameters.AddWithValue("@IsPositive", getIntWithDefaultNull(isPositive));
                            cmd.Parameters.AddWithValue("@AcrCode", acrCode);
                            cmd.Parameters.AddWithValue("@AcrAnatomic", acrAnatomic);
                            cmd.Parameters.AddWithValue("@AcrPathologic", acrPathologic);
                            cmd.Parameters.AddWithValue("@mender", curUserGuid);
                            cmd.Parameters.AddWithValue("@IsDiagnosisRight", isDiagnosisRight);
                            cmd.Parameters.AddWithValue("@ReportQuality", reportQuality);
                            cmd.Parameters.AddWithValue("@AccordRate", accordRate);
                            cmd.Parameters.AddWithValue("@ScoringVersion", scoringVersion);
                            cmd.Parameters.AddWithValue("@ReportQuality2", reportQuality2);
                            cmd.Parameters.AddWithValue("@Status", nextStatus);
                            cmd.Parameters.AddWithValue("@Domain", strDomain);
                            cmd.Parameters.AddWithValue("@Submitter", strSubmitter);
                            if (submitDt == null)
                                cmd.Parameters.AddWithValue("@SubmitDt", DBNull.Value);
                            else
                                cmd.Parameters.AddWithValue("@SubmitDt", submitDt);
                            cmd.Parameters.AddWithValue("@WYS", ReportCommon.Converter.GetBytes(wys));
                            cmd.Parameters.AddWithValue("@WYG", ReportCommon.Converter.GetBytes(wyg));
                            cmd.Parameters.AddWithValue("@WYSText", txtWYS);
                            cmd.Parameters.AddWithValue("@WYGText", txtWYG);
                            cmd.Parameters.AddWithValue("@AppendInfo", ReportCommon.Converter.GetBytes(appendInfo));
                            cmd.Parameters.AddWithValue("@TechInfo", techInfo);
                            cmd.Parameters.AddWithValue("@ReportText", reportText);
                            cmd.Parameters.AddWithValue("@DoctorAdvice", doctorAdvice);
                            cmd.Parameters.AddWithValue("@Comments", comments);
                            cmd.Parameters.AddWithValue("@Keyword", keyWord);
                            cmd.Parameters.AddWithValue("@CheckitemName", checkitemName);
                            cmd.Parameters.AddWithValue("@IsLeaveWord", nIsLeaveWord);
                            cmd.Parameters.AddWithValue("@Rejecter", rejecter);
                            cmd.Parameters.AddWithValue("@rejectToObject", rejectToObject);
                            cmd.Parameters.AddWithValue("@ReportGuid", reportGuid);
                            cmd.Parameters.AddWithValue("@RejectDomain", strDomain);
                            cmd.Parameters.AddWithValue("@RejectSite", CommonGlobalSettings.Utilities.GetCurSite());
                            cmd.Parameters.AddWithValue("@ReportQualityComments", reportQualityComments);
                            cmd.Parameters.AddWithValue("@MenderName", "");//empty it!
                            cmd.Parameters.AddWithValue("@ActionDt", actionDateTime);
                            cmd.Parameters.AddWithValue("@IsReturnVisit", isReturnVisit);

                            if (string.IsNullOrWhiteSpace(UnwrittenCurrentOwner))
                            {
                                sql += " update tRegProcedure set UnwrittenCurrentOwner = '" + rejectToObject + "', UnwrittenAssignDate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where reportGuid = '"
                                       + reportGuid + "' \r\n";
                            }
                            else
                            {
                                sql += " update tRegProcedure set UnwrittenCurrentOwner = '" + rejectToObject + "', UnwrittenAssignDate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                                    + "', UnapprovedCurrentOwner='" + UnApprovedCurrentOwner + "', UnapprovedAssignDate = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where reportGuid = '"
                                    + reportGuid + "' \r\n";

                            }


                            if (nOldStatus == Convert.ToInt32(ReportCommon.RP_Status.FirstApprove)
                                && !string.IsNullOrWhiteSpace(assign2Site) && !assign2Site.Equals(CommonGlobalSettings.Utilities.GetCurSite(), StringComparison.InvariantCultureIgnoreCase))
                            {
                                sql += " update tReport set  Rejecter = @Rejecter, rejectToObject = @rejectToObject,RejectDomain=@RejectDomain , RejectSite=@RejectSite ,ReportQualityComments=@ReportQualityComments ";
                                sql += string.Format(" where reportguid in( select reportguid from tregprocedure where orderguid ='{0}' and reportguid <> '{1}') \r\n", OrderGuid, reportGuid);
                            }

                        }
                        //save scoring info into tScoringResult
                        if (!string.IsNullOrWhiteSpace(scoringSelectedTexts))
                        {
                            sql += string.Format(" Update tScoringResult set IsFinalVersion =0 where IsFinalVersion = 1 and ObjectGuid in ('{0}') and Type='{1}' \r\n ", reportGuid, "2");
                            sql += string.Format("Insert into tScoringResult(Guid,ObjectGuid,Type,Result,Domain,Result2,Appraiser,Comment,AccordRate) values(NEWID(),'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                                reportGuid, 2, scoringSelectedTexts.Replace("'", "''"), CommonGlobalSettings.Utilities.GetCurDomain(), reportQuality, curUserGuid, reportQualityComments, accordRate) + "\r\n";
                        }
                        string sql1;


                        if (nextStatus == Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                        {
                            sql1 = " select 1 ";  //do nothing for secondapprove
                        }
                        else if (nextStatus == Convert.ToInt32(ReportCommon.RP_Status.Submit) && !string.IsNullOrWhiteSpace(UnapprovedCurrentOwner))
                        {
                            sql1 = " update tRegProcedure set status = " + nextStatus.ToString() + ",ReportGuid='" + reportGuid + "'" + ",UnapprovedCurrentOwner='" + UnapprovedCurrentOwner + "'" + ",UnapprovedAssignDate='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'"
                                       + " where reportGuid = '" + reportGuid + "' \r\n";
                        }
                        else
                        {
                            if (nextStatus == Convert.ToInt32((ReportCommon.RP_Status.Reject))
                                && nOldStatus == Convert.ToInt32(ReportCommon.RP_Status.FirstApprove)
                                && !string.IsNullOrWhiteSpace(assign2Site) && !assign2Site.Equals(CommonGlobalSettings.Utilities.GetCurSite(), StringComparison.InvariantCultureIgnoreCase))//referral to other site and reject in approved status
                            {
                                sql1 = string.Format("update tRegProcedure set status = {0} where orderGuid = '{1}' \r\n", nextStatus.ToString(), OrderGuid);
                            }
                            else
                            {
                                sql1 = " update tRegProcedure set status = " + nextStatus.ToString() + " where reportGuid = '"
                                           + reportGuid + "' \r\n";
                            }
                        }

                        iTestStep = 715;

                        #region old
                        #endregion

                        iTestStep = 722;

                        string sqlFile = MakeSQL4ReportFile(dsReportInfo, reportGuid);

                        iTestStep = 725;

                        using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                        {
                            conn.Open();
                            cmd.Connection = conn;

                            iTestStep = 728;

                            try
                            {
                                //
                                // begin transaction
                                cmd.Transaction = conn.BeginTransaction(ServerPubFun.GetIsolationLevel());

                                iTestStep = 732;
                                ServerPubFun.RISLog_Info(0, "Save report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();

                                iTestStep = 735;
                                ServerPubFun.RISLog_Info(0, "Save report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                cmd.CommandText = sql1;
                                cmd.ExecuteNonQuery();

                                iTestStep = 736;
                                ServerPubFun.RISLog_Info(0, "Save report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                //cmd.CommandText = sql2;
                                //cmd.ExecuteNonQuery();

                                cmd.CommandText = sqlFile;
                                cmd.ExecuteNonQuery();
								
								
								#region Added by Kevin For SR after report inserted

                                if (strReportContent != "")
                                {

                                    StructuredReportDAO.UpdateReportContent(strReportContent, reportGuid, curUserGuid, nextStatus, strNaturalText, ref cmd);
                                    cmd.ExecuteNonQuery();
                                    if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove) ||
                                        nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                                    {
                                        StructuredReportDAO.AddReportItem(strReportItem, reportGuid, ref cmd);
                                    }
                                    ServerPubFun.RISLog_Info(0,
                                        "Update report and update reportcontent, iTestStep=" + iTestStep.ToString() +
                                        ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);
                                }

                                #endregion
                                if (!isCancelSubmit)//cancel submit no need to update these fields
                                {
                                    if (observation != null)
                                    {
                                        //#US27608
                                        //cmd.CommandText = string.Format("Update tRegOrder set Observation ='{0}' where OrderGuid='{1}'", observation, OrderGuid);
                                        cmd.CommandText = string.Format("Update tRegOrder set Observation =@observation where OrderGuid=@OrderGuidOfObservation");
                                        cmd.Parameters.AddWithValue("@observation", observation);
                                        cmd.Parameters.AddWithValue("@OrderGuidOfObservation", OrderGuid);
                                        cmd.ExecuteNonQuery();

                                    }

                                    if (inspection != null)
                                    {
                                        //#US27608
                                        //cmd.CommandText = string.Format("Update tRegOrder set Optional2 ='{0}' where OrderGuid='{1}'", inspection, OrderGuid);
                                        cmd.CommandText = string.Format("Update tRegOrder set Optional2 =@inspection where OrderGuid=@OrderGuidOfInspection");
                                        cmd.Parameters.AddWithValue("@inspection", observation);
                                        cmd.Parameters.AddWithValue("@OrderGuidOfInspection", OrderGuid);
                                        cmd.ExecuteNonQuery();
                                    }
                                }

                                if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                {
                                    //make sure all reports have been approved
                                    cmd.CommandText = string.Format(@"if not exists (select 1 from tRegProcedure rp inner join tRegOrder ro on rp.OrderGuid = ro.OrderGuid
                                                            where rp.OrderGuid= '{0}' and (rp.Status <> {1} or (ro.IsReferral >0 and rp.Status = {1}))) 
                                                                    Update tRegOrder set CurrentSite = examsite where OrderGuid='{0}'",
                                                                    OrderGuid, nextStatus.ToString()); //update currentSite
                                    cmd.ExecuteNonQuery();
                                }

                                iTestStep = 737;
                                ServerPubFun.RISLog_Info(0, "Save report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);

                                #region save sign history(should same tran with save report)

                                #endregion

                                cmd.Transaction.Commit();
                                //end transaction
                                //

                            }
                            catch (Exception ex)
                            {
                                cmd.Transaction.Rollback();

                                string logText = "\r\nrpGuids=" + rpGuids
                                    + "\r\ncurUserGuid=" + curUserGuid
                                    + "\r\nnextStatus=" + nextStatus.ToString()
                                    + "\r\nisRebuild=" + isRebuild.ToString()
                                    + "\r\nreportGuid=" + reportGuid
                                    + "\r\nreportName=" + reportName
                                    + "\r\nwys=" + wys
                                    + "\r\nwyg=" + wyg
                                    + "\r\nappendInfo=" + appendInfo
                                    + "\r\ntechInfo=" + techInfo
                                    + "\r\nreportText=" + reportText
                                    + "\r\nreportQuality=" + reportQuality
                                    + "\r\naccordRate=" + accordRate
                                    + "\r\nscoringVersion=" + scoringVersion
                                    + "\r\nDomain=" + strDomain
                                    + "\r\nrejecter=" + rejecter
                                    + "\r\nrejectToObject=" + rejectToObject
                                    + "\r\ndoctorAdvice=" + doctorAdvice
                                    + "\r\nacrCode=" + acrCode
                                    + "\r\nacrAnatomic=" + acrAnatomic
                                    + "\r\nacrPathologic=" + acrPathologic
                                    + "\r\nkeyWord=" + keyWord
                                    + "\r\nisPositive=" + isPositive
                                    + "\r\nisDiagnosisRight=" + isDiagnosisRight
                                    + "\r\ncomments=" + comments
                                    + "\r\ntxtWYS=" + txtWYS
                                    + "\r\ntxtWYG=" + txtWYG
                                    + "\r\ncheckitemName=" + checkitemName
                                    + "\r\nSQL=" + sql;

                                ServerPubFun.RISLog_Error(0, "Error on SaveReport, MSG=" + ex.Message + ", iTestStep=" + iTestStep.ToString() + ", \r\nReportContent=" + logText, "", 0);



                                throw (ex);
                            }
                        }

                        iTestStep = 775;

                        // Hippa & integration
                        tagReportInfo rptInfo = ServerPubFun.GetReportInfo(reportGuid);

                        iTestStep = 778;

                        if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft))
                        {
                            if (isCancelSubmit)
                            {
                                OnCancelSubmit(rptInfo);
                            }
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                        {
                            OnSubmit(rptInfo, ReportCommon.ReportCommon.GetRPStatus(nOldStatus));
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                        {
                            if (isRebuild)
                            {
                                OnRebuild(rptInfo, dtSignModel);
                            }
                            else
                            {
                                OnApprove(rptInfo, ReportCommon.ReportCommon.GetRPStatus(nOldStatus), dtSignModel);
                            }
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                        {
                            if (isRebuild)
                            {
                                OnRebuild(rptInfo, dtSignModel);
                            }
                            else
                            {
                                OnSecondApprove(rptInfo, ReportCommon.ReportCommon.GetRPStatus(nOldStatus), dtSignModel);
                            }
                        }
                        else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                        {
                            OnReject(rptInfo, ReportCommon.ReportCommon.GetRPStatus(nOldStatus));
                        }

                      

                        iTestStep = 780;
                        ServerPubFun.RISLog_Info(0, "Save report, iTestStep=" + iTestStep.ToString() + ", userGuid=" + curUserGuid + ", clientIP=" + clientIP, "", 0);
                    }
                    #endregion

                    iTestStep = 800;

                    #region Log

                    string newreportlistguid = Guid.NewGuid().ToString();
                    string sqlLog = " insert tReportList(ReportListGuid,ReportGuid,ReportName,WYS,WYG,AppendInfo,TechInfo,ReportText,DoctorAdvice,"
                        + " IsPositive,IsDiagnosisRight,AcrCode,AcrAnatomic,AcrPathologic,Creater,CreateDt,Submitter,SubmitDt,"
                        + " FirstApprover,FirstApproveDt,SecondApprover,SecondApproveDt,RejectToObject,Rejecter,RejectDt,Status,"
                        + " DeleteMark,Domain,Comments,operationTime, mender,CreaterName,SubmitterName,MenderName, modifyDt)"
                        + " select '" + newreportlistguid +"',ReportGuid,ReportName,WYS,WYG,AppendInfo,TechInfo,ReportText,DoctorAdvice,IsPositive,"
                        + " IsDiagnosisRight,AcrCode,AcrAnatomic,AcrPathologic,Creater,CreateDt,Submitter,SubmitDt,FirstApprover,"
                        + " FirstApproveDt,SecondApprover,SecondApproveDt,RejectToObject,Rejecter,RejectDt,Status,DeleteMark,"
                        + " Domain,Comments,getdate(), mender,CreaterName,SubmitterName,MenderName, modifyDt from tReport where reportGuid = '" + reportGuid + "' \r\n";

                    dal.ExecuteNonQuery(sqlLog);
					
					#region Added by Kevin For SR
                    string sqladdreportcontentlist = " insert tReportContentList(ReportId, ReportVersion, ContentHtml, AuthorName,"
                                                     + "AutherId, CreateDate, Status, Domain, NaturalContentHtml, ReportListId)"
                                                     +
                                                     " select ReportId, ReportVersion, ContentHtml, AuthorName, AutherId, getdate(), Status, Domain, NaturalContentHtml,'" +
                                                     newreportlistguid + "' from tReportContent where reportId = '" +
                                                     reportGuid + "'\r\n";

                    dal.ExecuteNonQuery(sqladdreportcontentlist);
					#endregion
                    #endregion

                    iTestStep = 900;

                   

                    return "Result = " + Result_Success + "&ReportGuid = " + reportGuid;
                }
            }
            catch (Exception ex)
            {
                errMSG = ex.Message;

                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "SaveReportDAO_MSSQL, iTestStep=" + iTestStep.ToString() + ", MSG=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return "Result = " + errMSG; // Result_Fail;
        }

        private void ExistAndSameOrder(string[] rpGuids, ref bool bExist, ref bool bSameOrder)
        {
            bExist = true;
            bSameOrder = true;
            try
            {
                using (RisDAL dal = new RisDAL())
                {
                    string strOrderGuid = "";
                    for (int i = 0; i < rpGuids.Length; i++)
                    {
                        string sql = " select OrderGuid from tRegProcedure"
                        + " where ProcedureGuid = ('" + rpGuids[i] + "')";

                        DataTable dt = new DataTable();
                        dal.ExecuteQuery(sql, dt);

                        if (dt == null || dt.Rows.Count == 0)
                        {
                            bExist = false;
                            break;
                        }
                        if (strOrderGuid == "")
                        {
                            strOrderGuid = Convert.ToString(dt.Rows[0]["OrderGuid"]);
                        }
                        else
                        {
                            bSameOrder = strOrderGuid == Convert.ToString(dt.Rows[0]["OrderGuid"]) ? true : false;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                bExist = false;
                bSameOrder = false;
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0,
                    "SaveReportDAO::isExistsRP, msg=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }


        }

        private bool isExistsRPbyReportID(string rptid)
        {
            bool bExists = false;

            try
            {
                using (RisDAL dal = new RisDAL())
                {

                    string sql = " select 1 from tRegProcedure "
                        + " where reportGuid = '" + rptid.Trim(", ".ToCharArray()) + "'";

                    DataTable dt = new DataTable();
                    dal.ExecuteQuery(sql, dt);

                    bExists = (dt != null && dt.Rows.Count > 0);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0,
                    "SaveReportDAO::isExistsRPbyReportID, msg=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return bExists;
        }

        private bool isBelongSameRegOrder(string[] rpGuids)
        {
            if (rpGuids.Length < 2)
                return true;

            bool bSame = false;

            try
            {
                using (RisDAL dal = new RisDAL())
                {

                    string rps = "";
                    for (int i = 0; i < rpGuids.Length; i++)
                    {
                        rps += "'" + rpGuids.GetValue(i) + "',";
                    }
                    rps = rps.Trim(", ".ToCharArray());

                    string sql = " select distinct tRegOrder.OrderGuid from tRegOrder, tRegProcedure"
                        + " where tRegProcedure.OrderGuid = tRegOrder.OrderGuid"
                        + " and tRegProcedure.ProcedureGuid in (" + rps + ")";

                    DataTable dt = new DataTable();
                    dal.ExecuteQuery(sql, dt);

                    bSame = (dt != null && dt.Rows.Count == 1);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "isBelongSameRegOrder=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return bSame;
        }

        private bool isNewReport(string[] rpGuids)
        {
            if (!ReportCommon.ReportCommon.isNotEmptyStringArray(rpGuids))
                return false;

            bool bNew = false;

            try
            {
                using (RisDAL dal = new RisDAL())
                {

                    string rps = "";
                    for (int i = 0; i < rpGuids.Length; i++)
                    {
                        rps += "'" + rpGuids.GetValue(i) + "',";
                    }
                    rps = rps.Trim(", ".ToCharArray());

                    string sql = "select status from tRegProcedure "
                        + " where status>50 "
                        + " and ProcedureGuid in (" + rps + ")";

                    DataTable dt = new DataTable();
                    dal.ExecuteQuery(sql, dt);

                    bNew = (dt != null && dt.Rows.Count == 0);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "isNewReport=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return bNew;
        }

        private bool CanSaveReport(string reportGuid, int nextStatus)
        {
            bool bCanSave = false;

            try
            {
                using (RisDAL dal = new RisDAL())
                {

                    reportGuid = reportGuid.Trim();

                    string sql = "select rp.status from tRegProcedure rp "
                        + " where rp.reportGuid = '" + reportGuid + "'";

                    DataTable dt = new DataTable();
                    dal.ExecuteQuery(sql, dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int iCurStatus = System.Convert.ToInt32(dt.Rows[0][0]);

                        if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft))
                        {
                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                bCanSave = true;
                        }
                        else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                        {
                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                bCanSave = true;
                        }
                        else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                        {
                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                                bCanSave = true;
                        }
                        else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                        {
                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                                bCanSave = true;
                        }
                        else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                        {
                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                bCanSave = true;
                        }
                        else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Examination))
                        {
                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                bCanSave = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "CanSaveReport=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return bCanSave;
        }

        private bool CanSaveNewReport(int nextStatus)
        {
            bool bCanSave = false;

            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft) ||
                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                bCanSave = true;

            return bCanSave;
        }


        private string MakeReportName(string[] rpGuids)
        {
            string newName = "Error Report " + Guid.NewGuid().ToString();

            try
            {
                if (!ReportCommon.ReportCommon.isNotEmptyStringArray(rpGuids))
                {
                    throw (new Exception("Miss Parameter"));
                }

                using (RisDAL dal = new RisDAL())
                {

                    string rps = "";
                    for (int i = 0; i < rpGuids.Length; i++)
                    {
                        rps += "'" + rpGuids.GetValue(i) + "',";
                    }
                    rps = rps.Trim(", ".ToCharArray());

                    string sql = " select tRegOrder.AccNo, tProcedureCode.Description"
                        + " from tRegOrder, tProcedureCode, tRegProcedure"
                        + " where tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                        + " and tRegProcedure.ProcedureCode = tProcedureCode.ProcedureCode "
                        + " and tRegProcedure.ProcedureGuid in (" + rps + ")";

                    DataTable dt = new DataTable();
                    dal.ExecuteQuery(sql, dt);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        newName = //dt.Rows[0]["LocalName"].ToString() + "_"
                             ReportCommon.ReportCommon.StringRight(dt.Rows[0]["AccNo"].ToString(), 4);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            newName += "_" + dt.Rows[i]["Description"].ToString();
                        }
                        newName += DateTime.Now.Second.ToString();
                        newName = "_" + newName;
                    }

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Assert(false, ex.Message);

                ServerPubFun.RISLog_Error(0, "MakeReportName=" + ex.Message,
                    (new System.Diagnostics.StackFrame()).GetFileName(),
                    (new System.Diagnostics.StackFrame()).GetFileLineNumber());
            }

            return newName;// ReportCommon.ReportCommon.StringLeft_ANSI(newName, ServerPubFun.GetColumnWidth("tReport", "reportName"));
        }


        private void OnCreate(tagReportInfo rptInfo)
        {
            // Need to send gateway
            if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                return;

            // Gateway
            string guid = Guid.NewGuid().ToString();

            string sql = MakeSQL4GateWay(rptInfo, guid, 30, 16, 200);

            using (RisDAL dal = new RisDAL())
            {

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                {
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;

                    try
                    {
                        //
                        // begin transaction
                        //cmd.Transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                        if (0 == iWrittenCount++ % 100)
                        {
                            ServerPubFun.RISLog_Info(0, "OnCreate, SQL=" + sql,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }
                        else
                        {
                            ServerPubFun.RISLog_Info(0, "OnCreate, PID=" + rptInfo.patientID,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Create,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);

                        if (dal != null)
                        {
                            dal.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        //cmd.Transaction.Rollback();

                        System.Diagnostics.Debug.Assert(false, ex.Message);

                        ServerPubFun.RISLog_Error(0, "OnCreate, MSG=" + ex.Message,
                            (new System.Diagnostics.StackFrame()).GetFileName(),
                            (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Create,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                    }
                }
            }
        }

        private void OnSubmit(tagReportInfo rptInfo, ReportCommon.RP_Status oldStatus)
        {
            if (oldStatus == ReportCommon.RP_Status.Submit)
                return;

            // Need to send gateway
            if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                return;

            // Gateway
            string guid = Guid.NewGuid().ToString();

            string sql = MakeSQL4GateWay(rptInfo, guid, 32, 16, 203);

            using (RisDAL dal = new RisDAL())
            {

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                {
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;

                    try
                    {
                        if (0 == iWrittenCount++ % 100)
                        {
                            ServerPubFun.RISLog_Info(0, "OnSubmit, SQL=" + sql,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }
                        else
                        {
                            ServerPubFun.RISLog_Info(0, "OnSubmit, PID=" + rptInfo.patientID,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Submit,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);

                        if (dal != null)
                        {
                            dal.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        //cmd.Transaction.Rollback();

                        System.Diagnostics.Debug.Assert(false, ex.Message);

                        ServerPubFun.RISLog_Error(0, "OnSubmit, MSG=" + ex.Message,
                            (new System.Diagnostics.StackFrame()).GetFileName(),
                            (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Submit,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                    }
                }
            }
        }

        private void OnReject(tagReportInfo rptInfo, ReportCommon.RP_Status oldStatus)
        {            
            if (oldStatus == ReportCommon.RP_Status.Reject)
                return;


            // Need to send gateway
            if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                return;

            // Gateway
            string guid = Guid.NewGuid().ToString();

            string sql = MakeSQL4GateWay(rptInfo, guid, 32, 16, 205);

            using (RisDAL dal = new RisDAL())
            {

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                {
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;

                    try
                    {
                        if (0 == iWrittenCount++ % 100)
                        {
                            ServerPubFun.RISLog_Info(0, "OnReject, SQL=" + sql,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }
                        else
                        {
                            ServerPubFun.RISLog_Info(0, "OnReject, PID=" + rptInfo.patientID,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }

                        //
                        // begin transaction
                        //cmd.Transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Reject,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);

                        if (dal != null)
                        {
                            dal.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        //cmd.Transaction.Rollback();

                        System.Diagnostics.Debug.Assert(false, ex.Message);

                        ServerPubFun.RISLog_Error(0, "OnReject, MSG=" + ex.Message,
                            (new System.Diagnostics.StackFrame()).GetFileName(),
                            (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Reject,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                    }
                }
            }
        }

        private void OnApprove(tagReportInfo rptInfo, ReportCommon.RP_Status oldStatus,DataTable dtSignModel)
        {
            //sign history
            bool isDataSigned = false;
            SaveSignedHistory(dtSignModel);
            if (dtSignModel != null && dtSignModel.Rows.Count > 0)
            {
                isDataSigned = Convert.ToString(dtSignModel.Rows[0]["IsSigned"]) == "1" ? true : false;
            }


            // Need to send gateway
            if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                return;

            // Gateway
            string guid = Guid.NewGuid().ToString();

            string sql = MakeSQL4GateWay(rptInfo, guid, (oldStatus != ReportCommon.RP_Status.FirstApprove ? 32 : 31), 16, 206,isDataSigned);

            using (RisDAL dal = new RisDAL())
            {

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                {
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;

                    try
                    {
                        if (0 == iWrittenCount++ % 100)
                        {
                            ServerPubFun.RISLog_Info(0, "OnApprove, SQL=" + sql,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }
                        else
                        {
                            ServerPubFun.RISLog_Info(0, "OnApprove, PID=" + rptInfo.patientID,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }

                        //
                        // begin transaction
                        //cmd.Transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Approved,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);


                    }
                    catch (Exception ex)
                    {
                        //cmd.Transaction.Rollback();

                        System.Diagnostics.Debug.Assert(false, ex.Message);

                        ServerPubFun.RISLog_Error(0, "OnApprove, MSG=" + ex.Message,
                            (new System.Diagnostics.StackFrame()).GetFileName(),
                            (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Approved,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                    }
                }
            }
        }

        private void OnSecondApprove(tagReportInfo rptInfo, ReportCommon.RP_Status oldStatus, DataTable dtSignModel)
        {
            //sign history
            bool isDataSigned = false;
            SaveSignedHistory(dtSignModel);
            if (dtSignModel != null && dtSignModel.Rows.Count > 0)
            {
                isDataSigned = Convert.ToString(dtSignModel.Rows[0]["IsSigned"]) == "1" ? true : false;
            }

            // Need to send gateway
            if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                return;

            // Gateway
            string guid = Guid.NewGuid().ToString();

            string sql = MakeSQL4GateWay(rptInfo, guid, (oldStatus != ReportCommon.RP_Status.FirstApprove ? 32 : 31), 16, 206, isDataSigned);

            using (RisDAL dal = new RisDAL())
            {

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                {
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;

                    try
                    {
                        if (0 == iWrittenCount++ % 100)
                        {
                            ServerPubFun.RISLog_Info(0, "OnApprove, SQL=" + sql,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }
                        else
                        {
                            ServerPubFun.RISLog_Info(0, "OnApprove, PID=" + rptInfo.patientID,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }

                        //
                        // begin transaction
                        //cmd.Transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.SecondApproved,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "Second Report approve", true);


                    }
                    catch (Exception ex)
                    {
                        //cmd.Transaction.Rollback();

                        System.Diagnostics.Debug.Assert(false, ex.Message);

                        ServerPubFun.RISLog_Error(0, "OnApprove, MSG=" + ex.Message,
                            (new System.Diagnostics.StackFrame()).GetFileName(),
                            (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.SecondApproved,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "Second Report approve", false);
                    }
                }
            }
        }

        private void OnRebuild(tagReportInfo rptInfo, DataTable dtSignModel)
        {
            // Need to send gateway
            if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                return;

            //sign history
            bool isDataSigned = false;
            SaveSignedHistory(dtSignModel);
            if (dtSignModel != null && dtSignModel.Rows.Count > 0)
            {
                isDataSigned = Convert.ToString(dtSignModel.Rows[0]["IsSigned"]) == "1" ? true : false;
            }

            // Gateway
            string guid = Guid.NewGuid().ToString();

            string sql = MakeSQL4GateWay(rptInfo, guid, 31, 16, 204,isDataSigned);

            using (RisDAL dal = new RisDAL())
            {

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                {
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;

                    try
                    {
                        if (0 == iWrittenCount++ % 100)
                        {
                            ServerPubFun.RISLog_Info(0, "OnRebuild, SQL=" + sql,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }
                        else
                        {
                            ServerPubFun.RISLog_Info(0, "OnRebuild, PID=" + rptInfo.patientID,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }

                        //
                        // begin transaction
                        //cmd.Transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Rebuild,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);


                    }
                    catch (Exception ex)
                    {
                        //cmd.Transaction.Rollback();

                        System.Diagnostics.Debug.Assert(false, ex.Message);

                        ServerPubFun.RISLog_Error(0, "OnRebuild, MSG=" + ex.Message,
                            (new System.Diagnostics.StackFrame()).GetFileName(),
                            (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Rebuild,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                    }
                }
            }
        }

        private void OnCancelSubmit(tagReportInfo rptInfo)
        {

            // Need to send gateway
            if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                return;

            // Gateway
            string guid = Guid.NewGuid().ToString();

            string sql = MakeSQL4GateWay(rptInfo, guid, 32, 17, 200);

            using (RisDAL dal = new RisDAL())
            {

                using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dal.ConnectionString))
                {
                    conn.Open();

                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = conn;

                    try
                    {
                        if (0 == iWrittenCount++ % 100)
                        {
                            ServerPubFun.RISLog_Info(0, "OnCancelSubmit, SQL=" + sql,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }
                        else
                        {
                            ServerPubFun.RISLog_Info(0, "OnCancelSubmit, PID=" + rptInfo.patientID,
                                (new System.Diagnostics.StackFrame()).GetFileName(),
                                (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                        }

                        cmd.CommandText = sql;
                        cmd.ExecuteNonQuery();

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.CancelSubmit,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);


                    }
                    catch (Exception ex)
                    {
                        //cmd.Transaction.Rollback();

                        System.Diagnostics.Debug.Assert(false, ex.Message);

                        ServerPubFun.RISLog_Error(0, "OnSubmit, MSG=" + ex.Message,
                            (new System.Diagnostics.StackFrame()).GetFileName(),
                            (new System.Diagnostics.StackFrame()).GetFileLineNumber());

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Submit,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                    }
                }
            }
        }

        public static string MakeSQL4GateWay(tagReportInfo rptInfo, string guid, int event_type, int exam_status, int report_status, bool isDataSigned  = false)
        {
            return "insert GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                + " values('" + guid + "', getdate(), '" + event_type.ToString() + "', 'ReportGuid', 'Local')"
                + " insert GW_Patient(DATA_ID,DATA_DT,PATIENTID,OTHER_PID,PATIENT_NAME,PATIENT_LOCAL_NAME,"
                + "BIRTHDATE,SEX,PATIENT_ALIAS,ADDRESS,PHONENUMBER_HOME,MARITAL_STATUS,PATIENT_TYPE,"
                + "PATIENT_LOCATION,VISIT_NUMBER,BED_NUMBER,CUSTOMER_1,CUSTOMER_2,CUSTOMER_3,CUSTOMER_4,SSN_NUMBER,DRIVERLIC_NUMBER)"
                + "    values('" + guid + "', getdate(), '" + rptInfo.patientID + "','" + rptInfo.remotePID + "','"
                + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','"
                + rptInfo.birthday.ToString("yyyy-MM-dd") + "','"
                + rptInfo.gender + "', '"
                + rptInfo.patientAlias + "', '"
                + rptInfo.patientAddress + "', '"
                //+ rptInfo.patientPhone + "', '" + rptInfo.patientMarriage + "','" + (rptInfo.patientType == "I" ? "I" : "O") + "','"
                + rptInfo.patientPhone + "', '" + rptInfo.patientMarriage + "','"
                + rptInfo.patientType + "','"
                + rptInfo.inHospitalRegion + "', '" + rptInfo.visitNo + "', '"
                + rptInfo.bedNo + "', '" + rptInfo.patientName + "', '"
                + rptInfo.isVIP + "', '" + rptInfo.inHospitalNo + "', '"
                + ServerPubFun.getSQLString(rptInfo.patientComment) + "', '"
                + rptInfo.MedicareNo + "', '" + rptInfo.ReferenceNo + "') "
                + " insert GW_Order(DATA_ID,DATA_DT,ORDER_NO,PLACER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS,"
                + "PLACER_DEPARTMENT, PLACER, FILLER_DEPARTMENT, FILLER, REF_PHYSICIAN, REQUEST_REASON, "
                + "REUQEST_COMMENTS, EXAM_REQUIREMENT, SCHEDULED_DT, MODALITY, STATION_NAME, EXAM_LOCATION, "
                + "EXAM_DT, DURATION, TECHNICIAN, BODY_PART, PROCEDURE_CODE, PROCEDURE_DESC, EXAM_COMMENT, "
                + "CHARGE_STATUS, CHARGE_AMOUNT,CUSTOMER_1,CUSTOMER_2,CUSTOMER_3) "
                + "    values('" + guid + "', getdate(), '" + rptInfo.orderGuid + "','" + rptInfo.remoteAccNo + "', '"
                + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '" + exam_status.ToString() + "', '"
                + rptInfo.dept + "', '" + rptInfo.applyDoctor + "','"
                + rptInfo.dept + "', '" + rptInfo.applyDoctor + "', '"
                + rptInfo.applyDoctor + "', '"
                + ServerPubFun.getSQLString(rptInfo.observation) + "', '"
                + ServerPubFun.getSQLString(rptInfo.visitComments) + "', '"
                + ServerPubFun.getSQLString(rptInfo.orderComments) + "', '"
                + rptInfo.registerDt.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                + rptInfo.modalityType + "', '" + rptInfo.modality + "', '', '"
                + rptInfo.examineDt.ToString("yyyy-MM-dd HH:mm:ss") + "', '"
                + rptInfo.duration + "', '" + rptInfo.technician__LocalName + "', '"
                + rptInfo.bodypart + "', '" + rptInfo.procedureCode + "', '"
                + ServerPubFun.getSQLString(rptInfo.procedureDesc) + "', '"
                + ServerPubFun.getSQLString(rptInfo.orderComments) + "', '"
                + (rptInfo.isCharge == "1" ? "Y" : "N") + "', '"
                + rptInfo.charge + "','" + rptInfo.cardno + "','" + rptInfo.hisid + "','" + rptInfo.MedicareNo + "') "
                + " insert GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD,DIAGNOSE,COMMENTS,CUSTOMER_4,CUSTOMER_1)"
                + " values('" + guid + "', getdate(), '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
                + " '" + rptInfo.patientID + "', '" + report_status.ToString() + "', '" + rptInfo.modalityType + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportCreateDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                + " '"
                + ServerPubFun.getSQLString(rptInfo.operationStep) + "','"
                + ServerPubFun.getSQLString(rptInfo.wysText) + "','"
                + ServerPubFun.getSQLString(rptInfo.wygText) + "','"
                + ServerPubFun.getSQLString(rptInfo.techinfo) + "','"
                + (isDataSigned == true ? "Y" : "N") + "')";

        }

        public static string MakeSQL4ReportFile(DataSet ds, string reportGuid)
        {
            string sqlFile = string.Empty;

            if (ds != null && ds.Tables.Contains("ReportFile"))
            {
                DataTable dtFile = ds.Tables["ReportFile"];
                if (dtFile != null && dtFile.Rows.Count > 0)
                {
                    string fileGuids = "";

                    foreach (DataRow drf in dtFile.Rows)
                    {
                        sqlFile += " if exists(select 1 from tReportFile where FileGuid='" + drf["fileGuid"] + "') "
                            + " update tReportFile set RelativePath='" + drf["RelativePath"] + "', "
                            + " FileName='" + drf["FileName"] + "', "
                            + " BackupMark='" + drf["BackupMark"] + "', "
                            + " Domain='" + drf["Domain"] + "', "
                            + " ShowWidth=" + drf["ShowWidth"] + ", "
                            + " ShowHeight=" + drf["ShowHeight"] + ", "
                            + " ImagePosition=" + drf["ImagePosition"]
                            + " WHERE FileGuid='" + drf["fileGuid"] + "' "
                            + " else "
                            + " insert into tReportFile(ReportGuid,fileGuid,fileType,RelativePath,FileName,"
                            + "BackupMark,Domain,ShowWidth,ShowHeight,ImagePosition)"
                            + " values('" + drf["ReportGuid"] + "','" + drf["fileGuid"] + "',"
                            + drf["fileType"] + ",'" + drf["RelativePath"] + "','"
                            + drf["FileName"] + "','" + drf["BackupMark"] + "','"
                            + drf["Domain"] + "'," + drf["ShowWidth"] + "," + drf["ShowHeight"] + ","
                            + drf["ImagePosition"] + ") \r\n";

                        fileGuids += "'" + drf["fileGuid"] + "',";
                    }

                    fileGuids = fileGuids.Trim(',');

                    sqlFile += " delete from tReportFile where reportGuid='" + reportGuid + "' and FileGuid not in (" + fileGuids + ") \r\n";
                }
                else
                {
                    sqlFile += " delete from tReportFile where reportGuid='" + reportGuid + "' \r\n";
                }
            }
            else
            {
                sqlFile += " delete from tReportFile where reportGuid='" + reportGuid + "' \r\n";
            }

            return sqlFile;
        }

        private object getIntWithDefaultNull(string intStr)
        {
            if (string.IsNullOrEmpty(intStr))
                return DBNull.Value;

            return intStr;
        }

        private bool hasSumitedRecordInReportList(string reportGuid)
        {
            using (RisDAL okodak = new RisDAL())
            {
                try
                {
                    string sqlGetSubmitedRecords = string.Format("select count(1) from treportlist where reportguid ='{0}' and status = 110",reportGuid);
                    object objCount = okodak.ExecuteScalar(sqlGetSubmitedRecords);
                    if (objCount != null && Convert.ToInt32(objCount) > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);
                    ServerPubFun.RISLog_Error(0, "hasSumitedRecordInReportList, MSG=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                    return false;
                }
            }
        }

        public virtual void SaveSignedHistory(DataTable dtSignModel)
        {
            if (dtSignModel == null || dtSignModel.Rows.Count == 0)
            {
                return;
            }
            string strSqlInsert = string.Empty;
            string strSqlDeleteOldOne = string.Empty;
            string Action = Convert.ToString(dtSignModel.Rows[0]["Action"]);
            string Creater = Convert.ToString(dtSignModel.Rows[0]["Creater"]);
            string IsSigned = Convert.ToString(dtSignModel.Rows[0]["IsSigned"]);
            string CertSN = Convert.ToString(dtSignModel.Rows[0]["CertSN"]);
            string RawData = Convert.ToString(dtSignModel.Rows[0]["RawData"]);
            string SignedData = Convert.ToString(dtSignModel.Rows[0]["SignedData"]);
            string SignedTimestamp = Convert.ToString(dtSignModel.Rows[0]["SignedTimestamp"]);
            string OrderGuid = Convert.ToString(dtSignModel.Rows[0]["OrderGuid"]);
            string ReportGuid = Convert.ToString(dtSignModel.Rows[0]["ReportGuid"]);
            string Comments = Convert.ToString(dtSignModel.Rows[0]["Comments"]);
            string PatientID = Convert.ToString(dtSignModel.Rows[0]["PatientID"]);
            string LocalName = Convert.ToString(dtSignModel.Rows[0]["LocalName"]);
            string AccNo = Convert.ToString(dtSignModel.Rows[0]["AccNo"]);
            string CheckingItem = Convert.ToString(dtSignModel.Rows[0]["CheckingItem"]);
            string IsPositive = Convert.ToString(dtSignModel.Rows[0]["IsPositive"]);
            string ClinicNo = Convert.ToString(dtSignModel.Rows[0]["ClinicNo"]);
            string WYSText = Convert.ToString(dtSignModel.Rows[0]["WYSText"]);
            string WYGText = Convert.ToString(dtSignModel.Rows[0]["WYGText"]);
            string ExamDt = Convert.ToString(dtSignModel.Rows[0]["ExamDt"]);
            

            string strSqlUpdateReport = string.Empty;
            string signField = string.Empty;
            string signTimeStampField = string.Empty;
            switch (Action.ToLower())
            {
                case "submitreport":
                    signField = "submittersign";
                    signTimeStampField = "submittersigntimestamp";
                    break;
                case "approvereport":
                    signField = "firstapproversign";
                    signTimeStampField = "firstapproversigntimestamp";
                    break;
                case "secondapprovereport":
                    signField = "secondapproversign";
                    signTimeStampField = "secondapproversigntimestamp";
                    break;
                default:
                    return;
            }

            strSqlDeleteOldOne = string.Format("delete from tSignedHistory where reportguid = '{0}' and action ='{1}' and creater ='{2}'", ReportGuid, Action,Creater);

            strSqlInsert = "Insert into tSignedHistory(SignGuid,Action,Creater,IsSigned,OrderGuid,ReportGuid,"
                        + "CertSN,RawData,SignedData,SignedTimestamp,CreateDt,Comments,Domain,PatientID,LocalName,AccNo,CheckingItem,IsPositive,ClinicNo,WYSText,WYGText,ExamDt )"
                        + " Values(@SignGuid,@Action,@Creater,@IsSigned,"
                        + "@OrderGuid,@ReportGuid,@CertSN,@RawData,@SignedData,@SignedTimestamp,@CreateDt,@Comments,@Domain,"
                        + "@PatientID,@LocalName,@AccNo,@CheckingItem,@IsPositive,@ClinicNo,@WYSText,@WYGText,@ExamDt)";
            strSqlUpdateReport = string.Format("update treport set {0} = @{0},{1} = @{1} where reportguid ='{2}'", signField, signTimeStampField, ReportGuid);

            using (RisDAL okodak = new RisDAL())
            {
                try
                {
                    okodak.BeginTransaction();
                    //only first and second approve need always insert,others keep one record.
                    if (!Action.Equals("approvereport", StringComparison.OrdinalIgnoreCase) && !Action.Equals("secondapprovereport", StringComparison.OrdinalIgnoreCase))
                    {
                        okodak.ExecuteNonQuery(strSqlDeleteOldOne, RisDAL.ConnectionState.KeepOpen);
                    }

                    okodak.Parameters.Add("@SignGuid", Guid.NewGuid().ToString());
                    okodak.Parameters.Add("@Action", Action);
                    okodak.Parameters.Add("@Creater", Creater);
                    okodak.Parameters.AddInt("@IsSigned", IsSigned);
                    okodak.Parameters.Add("@CertSN", CertSN);
                    okodak.Parameters.Add("@RawData", RawData);
                    okodak.Parameters.Add("@SignedData", SignedData);
                    okodak.Parameters.Add("@SignedTimestamp", SignedTimestamp);
                    okodak.Parameters.AddDateTime("@CreateDt", DateTime.Now);
                    okodak.Parameters.Add("@OrderGuid", OrderGuid);
                    okodak.Parameters.Add("@ReportGuid", ReportGuid);
                    okodak.Parameters.Add("@Comments", Comments);
                    okodak.Parameters.Add("@PatientID", PatientID);
                    okodak.Parameters.Add("@LocalName", LocalName);
                    okodak.Parameters.Add("@AccNo", AccNo);
                    okodak.Parameters.Add("@ClinicNo", ClinicNo);
                    okodak.Parameters.Add("@IsPositive", IsPositive);
                    okodak.Parameters.Add("@WYSText", WYSText);
                    okodak.Parameters.Add("@WYGText", WYGText);
                    okodak.Parameters.Add("@CheckingItem", CheckingItem);
                    okodak.Parameters.Add("@ExamDt", ExamDt);
                    okodak.Parameters.Add("@Domain", CommonGlobalSettings.Utilities.GetCurDomain());
                    okodak.ExecuteNonQuery(strSqlInsert, RisDAL.ConnectionState.KeepOpen);

                    okodak.Parameters.Clear();
                    okodak.Parameters.Add("@" + signField, SignedData);
                    okodak.Parameters.Add("@" + signTimeStampField, SignedTimestamp);
                    okodak.ExecuteNonQuery(strSqlUpdateReport, RisDAL.ConnectionState.KeepOpen);
                    okodak.CommitTransaction();
                }
                catch (Exception ex)
                {
                    if (okodak != null)
                    {
                        okodak.RollbackTransaction();
                    }
                    System.Diagnostics.Debug.Assert(false, ex.Message);
                    ServerPubFun.RISLog_Error(0, "SaveSignedHistory, MSG=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }
            }
        }

        internal class SaveReportDAO_ORACLE : IReportDAO
        {
            const string Result_Success = "0";
            const string Result_Fail = "-1";
            const string Result_Miss_Parameters = "-2";
            const string Result_DifferentRegOrder = "-3";
            const string Result_ErrorStatus = "-4";

            public object Execute(object param)
            {
                try
                {
                    Dictionary<string, object> paramMap = param as Dictionary<string, object>;

                    if (paramMap == null || paramMap.Count < 1)
                    {
                        throw (new Exception("No parameter in SaveReportDAO!"));
                    }

                    using (RisDAL dal = new RisDAL())
                    {

                        string rpGuids = "", curUserGuid = "";
                        int nextStatus = 0;
                        bool isRebuild = false;
                        DataSet dsReportInfo = null;

                        foreach (string key in paramMap.Keys)
                        {
                            if (key.ToUpper() == "NEXTSTATUS")
                            {
                                nextStatus = System.Convert.ToInt32(paramMap[key] as string);
                            }
                            else if (key.ToUpper() == "PROCEDUREGUID")
                            {
                                rpGuids = paramMap[key] as string;
                            }
                            else if (key.ToUpper() == "ISREBUILD")
                            {
                                isRebuild = System.Convert.ToBoolean(paramMap[key] as string);
                            }
                            else if (key.ToUpper() == "DATASET")
                            {
                                dsReportInfo = paramMap[key] as DataSet;
                            }
                            else if (key.ToUpper() == "USERID")
                            {
                                curUserGuid = paramMap[key] as string;
                            }
                        }

                        if (dsReportInfo == null ||
                            curUserGuid == null || curUserGuid.Length < 1)
                        {
                            System.Diagnostics.Debug.Assert(false, "Missing Parameter!");
                            throw (new Exception("Missing Parameter!"));
                        }

                        rpGuids = rpGuids.Trim(",; ".ToCharArray());
                        string[] rpList = rpGuids.Split(',');

                        if (dsReportInfo == null || dsReportInfo.Tables.Count < 1 || dsReportInfo.Tables[0].Rows.Count < 1)
                            return "Result = " + Result_Miss_Parameters;

                        if (!isBelongSameRegOrder(rpList))
                            return "Result = " + Result_DifferentRegOrder;

                        string reportGuid, reportName, wys, wyg, appendInfo, techInfo, reportText, reportQuality, strDomain,
                            rejecter, rejectToObject, doctorAdvice, acrCode, acrAnatomic, acrPathologic, keyWord,
                            isPositive, isDiagnosisRight, comments, txtWYS, txtWYG;

                        reportGuid = reportName = wys = wyg = appendInfo = techInfo = reportText = reportQuality = strDomain
                            = rejecter = rejectToObject = doctorAdvice = acrCode = acrAnatomic = acrPathologic = keyWord
                            = isPositive = isDiagnosisRight = comments = txtWYS = txtWYG = "";

                        #region get field value


                        DataRow dr = dsReportInfo.Tables[0].Rows[0];

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTGUID))
                        {
                            reportGuid = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTGUID] as string;
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTNAME))
                        {
                            reportName = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTNAME] as string;
                            if (reportName == null)
                            {
                                reportName = "";
                            }
                            else
                            {
                                reportName = ReportCommon.ReportCommon.StringLeft_ANSI(reportName,
                                    ServerPubFun.GetColumnWidth("tReport", "reportName"));
                            }
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_WYS))
                        {
                            Byte[] buff = dr[ReportCommon.ReportCommon.FIELDNAME_WYS] as Byte[];
                            if (buff == null)
                                wys = "";
                            else
                                wys = ReportCommon.Converter.GetStringFromBytes(buff);
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_WYG))
                        {
                            Byte[] buff = dr[ReportCommon.ReportCommon.FIELDNAME_WYG] as Byte[];
                            if (buff == null)
                                wyg = "";
                            else
                                wyg = ReportCommon.Converter.GetStringFromBytes(buff);
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_WYSTEXT))
                        {
                            txtWYS = dr[ReportCommon.ReportCommon.FIELDNAME_WYSTEXT] as string;

                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_WYGTEXT))
                        {
                            txtWYG = dr[ReportCommon.ReportCommon.FIELDNAME_WYGTEXT] as string;
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_APPENDINFO))
                        {
                            Byte[] buff = dr[ReportCommon.ReportCommon.FIELDNAME_APPENDINFO] as Byte[];
                            if (buff == null)
                                appendInfo = "";
                            else
                                appendInfo = ReportCommon.Converter.GetStringFromBytes(buff);
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_TECHINFO))
                        {
                            techInfo = System.Convert.ToString(dr[ReportCommon.ReportCommon.FIELDNAME_TECHINFO]);
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTTEXT))
                        {
                            reportText = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTTEXT] as string;
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_DOCTORADVICE))
                        {
                            doctorAdvice = dr[ReportCommon.ReportCommon.FIELDNAME_DOCTORADVICE] as string;
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ACRCODE))
                        {
                            acrCode = dr[ReportCommon.ReportCommon.FIELDNAME_ACRCODE] as string;
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ACRANATOMIC))
                        {
                            acrAnatomic = dr[ReportCommon.ReportCommon.FIELDNAME_ACRANATOMIC] as string;
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ACRPATHOLOGIC))
                        {
                            acrPathologic = dr[ReportCommon.ReportCommon.FIELDNAME_ACRPATHOLOGIC] as string;
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_KEYWORD))
                        {
                            keyWord = dr[ReportCommon.ReportCommon.FIELDNAME_KEYWORD] as string;
                            if (keyWord == null)
                            {
                                keyWord = "";
                            }
                            else
                            {
                                keyWord = ReportCommon.ReportCommon.StringLeft_ANSI(keyWord,
                                    ServerPubFun.GetColumnWidth("tReport", "Keyword"));
                            }
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ISPOSITIVE))
                        {
                            isPositive = dr[ReportCommon.ReportCommon.FIELDNAME_ISPOSITIVE].ToString();
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_ISDIAGNOSISRIGHT))
                        {
                            isDiagnosisRight = dr[ReportCommon.ReportCommon.FIELDNAME_ISDIAGNOSISRIGHT].ToString();
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTREJECTER))
                        {
                            rejecter = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTREJECTER].ToString();
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REJECTTOOBJECT))
                        {
                            rejectToObject = dr[ReportCommon.ReportCommon.FIELDNAME_REJECTTOOBJECT].ToString();
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTQUALITY))
                        {
                            reportQuality = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTQUALITY].ToString();
                        }

                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORTDOMAIN))
                        {
                            strDomain = dr[ReportCommon.ReportCommon.FIELDNAME_REPORTDOMAIN] as string;
                        }


                        if (dsReportInfo.Tables[0].Columns.Contains(ReportCommon.ReportCommon.FIELDNAME_REPORT_COMMENTS))
                        {
                            comments = dr[ReportCommon.ReportCommon.FIELDNAME_REPORT_COMMENTS] as string;
                            if (comments == null)
                            {
                                comments = "";
                            }
                            else
                            {
                                comments = ReportCommon.ReportCommon.StringLeft_ANSI(comments,
                                    ServerPubFun.GetColumnWidth("tReport", "comments"));
                            }
                        }

                        //
                        //validate
                        if (isPositive != "1" && isPositive != "2")
                        {
                            isPositive = "0";
                        }

                        if (isDiagnosisRight != "1" && isDiagnosisRight != "2")
                        {
                            isDiagnosisRight = "0";
                        }

                        #endregion

                        if (isNewReport(rpList))
                        #region Create Report
                        {
                            if (!CanSaveNewReport(nextStatus))
                            {
                                return "Result = " + Result_ErrorStatus;
                            }

                            // Generate the Guid & Name of the new Report.
                            reportName = ReportCommon.ReportCommon.StringLeft_ANSI(MakeReportName(rpList),
                                    ServerPubFun.GetColumnWidth("tReport", "reportName"));

                            //
                            //database operation
                            string sql = " delete from tReport where reportGuid = '" + reportGuid + "'; ";// +
                            //   " delete from tReport_related_RP where reportGuid = '" + reportGuid + "'; ";

                            sql += " insert into tReport(ReportGuid,ReportName,IsPositive,AcrCode,AcrAnatomic,AcrPathologic,Creater,"
                                + " CreateDt,IsDiagnosisRight,KeyWord,ReportQuality,Status,Domain,Comments,DeleteMark,IsPrint) "
                                + " values('" + reportGuid + "','" + reportName + "'," + isPositive + ",'" + acrCode + "','"
                                + acrAnatomic + "','" + acrPathologic + "','" + curUserGuid + "', SYSDATE," + isDiagnosisRight + ",'"
                                + keyWord + "','" + reportQuality + "'," + nextStatus.ToString() + ",'" + strDomain + "','" + comments + "',0,0); ";

                            for (int i = 0; i < rpList.GetLength(0); i++)
                            {
                                string rpguid = rpList.GetValue(i) as string;
                                if (rpguid != null && (rpguid = rpguid.ToString().Trim()).Length > 0)
                                {
                                    sql += " delete from tReport_related_RP where ProcedureGuid = '" + rpguid + "'; "
                                        + " insert into tReport_related_RP(ProcedureGuid,ReportGuid,DeleteMark,Domain) values('" + rpguid + "','" + reportGuid + "', 0, ''); "
                                        + " update tRegProcedure set status = " + nextStatus.ToString() + " where ProcedureGuid='" + rpguid + "'; ";
                                }
                            }

                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                            {
                                sql += " update tReport set submitDT = SYSDATE, submitter ='" + curUserGuid
                                    + "' where reportGuid = '" + reportGuid + "'; ";
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                            {
                                sql += " update tReport set firstApproveDT = SYSDATE, firstApprover ='" + curUserGuid
                                    + "' where reportGuid = '" + reportGuid + "'; ";
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                            {
                                sql += " update tReport set secondApproveDT = SYSDATE, secondApprover ='" + curUserGuid
                                    + "' where reportGuid = '" + reportGuid + "'; ";
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                            {
                                sql += " update tReport set RejectDT = SYSDATE, Rejecter ='" + rejecter
                                    + "', rejectToObject = '" + rejectToObject + "' where reportGuid = '" + reportGuid + "'; ";
                            }

                            // report file
                            sql += " delete from tReportFile where reportGuid='" + reportGuid + "'; ";
                            if (dsReportInfo != null && dsReportInfo.Tables.Contains("ReportFile"))
                            {
                                DataTable dtFile = dsReportInfo.Tables["ReportFile"];
                                if (dtFile != null)
                                {
                                    foreach (DataRow drf in dtFile.Rows)
                                    {
                                        sql += " insert into tReportFile(ReportGuid,fileGuid,fileType,RelativePath,FileName,"
                                            + "BackupMark,Domain,ShowWidth,ShowHeight,ImagePosition)"
                                            + " values('" + drf["ReportGuid"] + "','" + drf["fileGuid"] + "',"
                                            + drf["fileType"] + ",'" + drf["RelativePath"] + "','"
                                            + drf["FileName"] + "','" + drf["BackupMark"] + "','"
                                            + drf["Domain"] + "'," + drf["ShowWidth"] + "," + drf["ShowHeight"] + ","
                                            + drf["ImagePosition"] + "); ";
                                    }
                                }
                            }

                            sql = "begin " + sql + " commit; end;";



                            dal.ExecuteNonQuery(sql);

                            if (wys != null && wys.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(wys);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "WYS", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (wyg != null && wyg.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(wyg);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "WYG", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (txtWYS != null && txtWYS.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(txtWYS);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "WYSText", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (txtWYG != null && txtWYG.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(txtWYG);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "WYGText", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (appendInfo != null && appendInfo.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(appendInfo);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "AppendInfo", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (techInfo != null && techInfo.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(techInfo);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "TechInfo", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (reportText != null && reportText.Length > 0)
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "ReportText", reportText, reportText.Length, RisDAL.ConnectionState.KeepOpen);

                            if (doctorAdvice != null && doctorAdvice.Length > 0)
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "DoctorAdvice", doctorAdvice, doctorAdvice.Length, RisDAL.ConnectionState.KeepOpen);

                            // Log & Integration
                            tagReportInfo rptInfo = ServerPubFun.GetReportInfo(reportGuid);

                            OnCreate(rptInfo);

                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                            {
                                OnSubmit(rptInfo, ReportCommon.RP_Status.Examination);
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                            {
                                OnApprove(rptInfo, ReportCommon.RP_Status.Examination);
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                            {
                                OnSecondApprove(rptInfo, ReportCommon.RP_Status.Examination);
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                            {
                                OnReject(rptInfo, ReportCommon.RP_Status.Examination);
                            }
                        }
                        #endregion
                        else
                        #region Save Report
                        {
                            if (!CanSaveReport(reportGuid, nextStatus))
                            {
                                return "Result = " + Result_ErrorStatus;
                            }



                            tagReportInfo oldReportInfo = ServerPubFun.GetReportInfo(reportGuid);

                            bool isReplaceApproverDtOnRebuild = ServerPubFun.GetSystemProfile_Bool(ReportCommon.ProfileName.RebuildReport_Replace4ApproverAndDt, ReportCommon.ModuleID.Report);


                            string sql = "";

                            sql += " update tReport set IsPositive = " + isPositive + ", AcrCode = '" + acrCode
                                + "', AcrAnatomic = '" + acrAnatomic + "', AcrPathologic = '" + acrPathologic
                                + "', IsDiagnosisRight = " + isDiagnosisRight + ", KeyWord = '" + keyWord
                                + "', ReportQuality = '" + reportQuality + "', Status = " + nextStatus.ToString()
                                + " , Domain = '" + strDomain + "', Comments = '" + comments + "'"
                                + " where reportGuid = '" + reportGuid + "'; ";

                            sql += " update tRegProcedure set status = " + nextStatus.ToString() + " where procedureGuid in "
                                + " (select procedureGuid from tReport_related_rp where reportGuid = '" + reportGuid + "' ); ";

                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft))
                            {
                                sql += " update tReport set creater = '" + curUserGuid + "', createDt = SYSDATE"
                                    + " where reportGuid = '" + reportGuid + "'; ";
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                            {
                                sql += " update tReport set submitter = '" + curUserGuid + "', SubmitDt = SYSDATE"
                                    + " where reportGuid = '" + reportGuid + "'; ";

                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                            {
                                if (isRebuild)
                                {
                                    sql += " update tReport set mender = '" + curUserGuid + "', ModifyDt = SYSDATE"
                                        + " where reportGuid = '" + reportGuid + "'; ";

                                    if (isReplaceApproverDtOnRebuild)
                                    {
                                        sql += " update tReport set firstApproveDt = SYSDATE"
                                            + " where reportGuid = '" + reportGuid + "'; ";

                                        sql += " update tReport set firstApprover = '" + curUserGuid + "'"
                                            + " where reportGuid = '" + reportGuid + "'; ";
                                    }
                                }
                                else
                                {
                                    sql += " update tReport set firstApprover = '" + curUserGuid + "', firstApproveDt = SYSDATE"
                                        + " where reportGuid = '" + reportGuid + "'; ";
                                }
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                            {
                                if (isRebuild)
                                {
                                    sql += " update tReport set mender = '" + curUserGuid + "', ModifyDt = SYSDATE"
                                        + " where reportGuid = '" + reportGuid + "'; ";

                                    if (isReplaceApproverDtOnRebuild)
                                    {
                                        sql += " update tReport set secondApproveDt = SYSDATE"
                                            + " where reportGuid = '" + reportGuid + "'; ";

                                        sql += " update tReport set secondApprover = '" + curUserGuid + "'"
                                            + " where reportGuid = '" + reportGuid + "'; ";
                                    }
                                }
                                else
                                {
                                    sql += " update tReport set secondApprover = '" + curUserGuid + "', secondApproveDt = SYSDATE"
                                        + " where reportGuid = '" + reportGuid + "'; ";
                                }
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                            {
                                // EK_HI00047517
                                sql += " update tReport set RejectDT = SYSDATE, Rejecter ='" + rejecter + "',"
                                    + " rejectToObject = '" + rejectToObject + "' where reportGuid = '" + reportGuid + "'; ";
                            }

                            if (nextStatus != System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                            {
                                sql += " update tReport set RejectDT = null, Rejecter = null,"
                                    + " rejectToObject = null where reportGuid = '" + reportGuid + "'; ";
                            }

                            // report file
                            sql += " delete from tReportFile where reportGuid='" + reportGuid + "'; ";
                            if (dsReportInfo != null && dsReportInfo.Tables.Contains("ReportFile"))
                            {
                                DataTable dtFile = dsReportInfo.Tables["ReportFile"];
                                if (dtFile != null)
                                {
                                    foreach (DataRow drf in dtFile.Rows)
                                    {
                                        sql += " insert into tReportFile(ReportGuid,fileGuid,fileType,RelativePath,FileName,"
                                            + "BackupMark,Domain,ShowWidth,ShowHeight,ImagePosition)"
                                            + " values('" + drf["ReportGuid"] + "','" + drf["fileGuid"] + "',"
                                            + drf["fileType"] + ",'" + drf["RelativePath"] + "','"
                                            + drf["FileName"] + "','" + drf["BackupMark"] + "','"
                                            + drf["Domain"] + "'," + drf["ShowWidth"] + "," + drf["ShowHeight"] + ","
                                            + drf["ImagePosition"] + "); ";
                                    }
                                }
                            }

                            sql = "begin " + sql + " commit; end;";



                            dal.ExecuteNonQuery(sql);

                            if (wys != null && wys.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(wys);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "WYS", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (wyg != null && wyg.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(wyg);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "WYG", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (txtWYS != null && txtWYS.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(txtWYS);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "WYSText", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (txtWYG != null && txtWYG.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(txtWYG);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "WYGText", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (appendInfo != null && appendInfo.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(appendInfo);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "AppendInfo", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (techInfo != null && techInfo.Length > 0)
                            {
                                byte[] buff = System.Text.Encoding.Default.GetBytes(techInfo);
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "TechInfo", buff, buff.Length, RisDAL.ConnectionState.KeepOpen);
                            }

                            if (reportText != null && reportText.Length > 0)
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "ReportText", reportText, reportText.Length, RisDAL.ConnectionState.KeepOpen);

                            if (doctorAdvice != null && doctorAdvice.Length > 0)
                                dal.WriteLargeObj("tReport", "reportGuid", reportGuid, "DoctorAdvice", doctorAdvice, doctorAdvice.Length, RisDAL.ConnectionState.KeepOpen);

                            //
                            // Hippa & integration
                            tagReportInfo rptInfo = ServerPubFun.GetReportInfo(reportGuid);

                            if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft))
                            {
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                            {
                                OnSubmit(rptInfo, ReportCommon.ReportCommon.GetRPStatus(oldReportInfo.status));
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                            {
                                if (isRebuild)
                                {
                                    OnRebuild(rptInfo);
                                }
                                else
                                {
                                    OnApprove(rptInfo, ReportCommon.ReportCommon.GetRPStatus(oldReportInfo.status));
                                }
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                            {
                                if (isRebuild)
                                {
                                    OnRebuild(rptInfo);
                                }
                                else
                                {
                                    OnSecondApprove(rptInfo, ReportCommon.ReportCommon.GetRPStatus(oldReportInfo.status));
                                }
                            }
                            else if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                            {
                                OnReject(rptInfo, ReportCommon.ReportCommon.GetRPStatus(oldReportInfo.status));
                            }
                        }
                        #endregion

                        #region Log

                        string sqlLog = " insert into tReportList(ReportListGuid,ReportGuid,ReportName,WYS,WYG,AppendInfo,TechInfo,ReportText,DoctorAdvice,"
                            + " IsPositive,IsDiagnosisRight,AcrCode,AcrAnatomic,AcrPathologic,Creater,CreateDt,Submitter,SubmitDt,"
                            + " FirstApprover,FirstApproveDt,SecondApprover,SecondApproveDt,RejectToObject,Rejecter,RejectDt,Status,"
                            + " DeleteMark,Domain,Comments,operationTime)"
                            + " select sys_guid(),ReportGuid,ReportName,WYS,WYG,AppendInfo,TechInfo,ReportText,DoctorAdvice,IsPositive,"
                            + " IsDiagnosisRight,AcrCode,AcrAnatomic,AcrPathologic,Creater,CreateDt,Submitter,SubmitDt,FirstApprover,"
                            + " FirstApproveDt,SecondApprover,SecondApproveDt,RejectToObject,Rejecter,RejectDt,Status,DeleteMark,"
                            + " Domain,Comments,SYSDATE from tReport where reportGuid = '" + reportGuid + "' ";

                        dal.ExecuteNonQuery(sqlLog);

                        #endregion

                        if (dal != null)
                        {
                            dal.Dispose();
                        }

                        return "Result = " + Result_Success + "&ReportGuid = " + reportGuid;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "SaveReportDAO_ORACLE, MSG=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }

                return "Result = " + Result_Fail;
            }

            private bool isBelongSameRegOrder(string[] rpGuids)
            {
                if (rpGuids.Length < 2)
                    return true;

                bool bSame = false;

                try
                {
                    using (RisDAL dal = new RisDAL())
                    {

                        string rps = "";
                        for (int i = 0; i < rpGuids.Length; i++)
                        {
                            rps += "'" + rpGuids.GetValue(i) + "',";
                        }
                        rps = rps.Trim(", ".ToCharArray());

                        string sql = " select distinct tRegOrder.OrderGuid from tRegOrder, tRegProcedure"
                            + " where tRegProcedure.OrderGuid = tRegOrder.OrderGuid"
                            + " and tRegProcedure.ProcedureGuid in (" + rps + ")";

                        DataTable dt = new DataTable();
                        dal.ExecuteQuery(sql, dt);

                        bSame = (dt != null && dt.Rows.Count == 1);

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "isBelongSameRegOrder=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }

                return bSame;
            }

            private bool isNewReport(string[] rpGuids)
            {
                if (!ReportCommon.ReportCommon.isNotEmptyStringArray(rpGuids))
                    return false;

                bool bNew = false;

                try
                {
                    using (RisDAL dal = new RisDAL())
                    {

                        string rps = "";
                        for (int i = 0; i < rpGuids.Length; i++)
                        {
                            rps += "'" + rpGuids.GetValue(i) + "',";
                        }
                        rps = rps.Trim(", ".ToCharArray());

                        string sql = "select 1 as tmp from tRegProcedure, tReport"
                            + " where tRegProcedure.ReportGuid = tReport.ReportGuid "
                            + " and tRegProcedure.ProcedureGuid in (" + rps + ")";

                        DataTable dt = new DataTable();
                        dal.ExecuteQuery(sql, dt);

                        bNew = (dt != null && dt.Rows.Count == 0);

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "isNewReport=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }

                return bNew;
            }

            private bool CanSaveReport(string reportGuid, int nextStatus)
            {
                bool bCanSave = false;

                try
                {
                    using (RisDAL dal = new RisDAL())
                    {

                        reportGuid = reportGuid.Trim();


                        string sql = "select rp.status from tRegProcedure rp, tReport r with (nolock)"
                            + " where rp.reportGuid = r.reportGuid "
                            + " and rp.reportGuid = '" + reportGuid + "'";

                        DataTable dt = new DataTable();
                        dal.ExecuteQuery(sql, dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int iCurStatus = System.Convert.ToInt32(dt.Rows[0][0]);

                            if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft))
                            {
                                if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft) ||
                                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                    bCanSave = true;
                            }
                            else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit))
                            {
                                if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                    bCanSave = true;
                            }
                            else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                            {
                                if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove) ||
                                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                                    bCanSave = true;
                            }
                            else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                            {
                                if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.SecondApprove))
                                    bCanSave = true;
                            }
                            else if (iCurStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject))
                            {
                                if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                                    bCanSave = true;
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "CanSaveReport=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }

                return bCanSave;
            }

            private bool CanSaveNewReport(int nextStatus)
            {
                bool bCanSave = false;

                if (nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Draft) ||
                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Reject) ||
                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.Submit) ||
                    nextStatus == System.Convert.ToInt32(ReportCommon.RP_Status.FirstApprove))
                    bCanSave = true;

                return bCanSave;
            }

            private string MakeReportName(string[] rpGuids)
            {
                string newName = "Error Report " + Guid.NewGuid().ToString();

                try
                {
                    if (!ReportCommon.ReportCommon.isNotEmptyStringArray(rpGuids))
                    {
                        throw (new Exception("Miss Parameter"));
                    }

                    using (RisDAL dal = new RisDAL())
                    {

                        string rps = "";
                        for (int i = 0; i < rpGuids.Length; i++)
                        {
                            rps += "'" + rpGuids.GetValue(i) + "',";
                        }
                        rps = rps.Trim(", ".ToCharArray());

                        string sql = " select tRegPatient.LocalName, tRegOrder.AccNo, tProcedureCode.Description"
                            + " from tRegPatient, tRegOrder, tProcedureCode, tRegProcedure"
                            + " where tRegPatient.PatientGuid = tRegOrder.PatientGuid"
                            + " and tRegOrder.OrderGuid = tRegProcedure.OrderGuid"
                            + " and tRegProcedure.ProcedureCode = tProcedureCode.ProcedureCode "
                            + " and tRegProcedure.ProcedureGuid in (" + rps + ")";

                        DataTable dt = new DataTable();
                        dal.ExecuteQuery(sql, dt);

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            newName = dt.Rows[0]["LOCALNAME"].ToString() + "_"
                                + ReportCommon.ReportCommon.StringRight(dt.Rows[0]["ACCNO"].ToString(), 4);

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                newName += "_" + dt.Rows[i]["DESCRIPTION"].ToString();
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Assert(false, ex.Message);

                    ServerPubFun.RISLog_Error(0, "MakeReportName=" + ex.Message,
                        (new System.Diagnostics.StackFrame()).GetFileName(),
                        (new System.Diagnostics.StackFrame()).GetFileLineNumber());
                }

                return ReportCommon.ReportCommon.StringLeft_ANSI(newName, ServerPubFun.GetColumnWidth("tReport", "reportName"));
            }

            private void OnCreate(tagReportInfo rptInfo)
            {
                try
                {
                    // Need to send gateway
                    if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                        return;

                    // Gateway
                    string guid = Guid.NewGuid().ToString();
                    string sql = "insert into GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                        + " values('" + guid + "', SYSDATE, '30', 'ReportGuid', 'Local'); "
                        + " insert into GW_Patient(DATA_ID,DATA_DT,PATIENTID,PATIENT_NAME,PATIENT_LOCAL_NAME,BIRTHDATE,SEX)"
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.patientID + "','" + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','" + rptInfo.birthday.ToString("yyyy-MM-dd") + "','" + rptInfo.gender + "'); "
                        + " insert into GW_Order(DATA_ID,DATA_DT,ORDER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS) "
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.orderGuid + "', '" + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '16'); "
                        + " insert into GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                        + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD)"
                        + " values('" + guid + "', SYSDATE, '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
                        + " '" + rptInfo.patientID + "', '200', '" + rptInfo.modality + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                        + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportCreateDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                        + " '" + rptInfo.operationStep + "'); ";

                    sql = "begin " + sql + " commit; end;";

                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteNonQuery(sql);

                        if (rptInfo.wysText != null && rptInfo.wysText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "DIAGNOSE", rptInfo.wysText, rptInfo.wysText.Length, RisDAL.ConnectionState.KeepOpen);
                        }

                        if (rptInfo.wygText != null && rptInfo.wygText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "COMMENTS", rptInfo.wygText, rptInfo.wygText.Length, RisDAL.ConnectionState.KeepOpen);
                        }
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
        CommonGlobalSettings.HippaName.ActionCode.Create,
        rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);
                    }

                }
                catch (Exception)
                {
                                   // Hippa
                Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                    CommonGlobalSettings.HippaName.ActionCode.Create,
                    rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                }

                
            }

            private void OnSubmit(tagReportInfo rptInfo, ReportCommon.RP_Status oldStatus)
            {
                try
                {


                    if (oldStatus == ReportCommon.RP_Status.Submit)
                        return;

                    // Need to send gateway
                    if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                        return;

                    // Gateway
                    string guid = Guid.NewGuid().ToString();
                    string sql = "insert into GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                        + " values('" + guid + "', SYSDATE, '32', 'ReportGuid', 'Local'); "
                        + " insert into GW_Patient(DATA_ID,DATA_DT,PATIENTID,PATIENT_NAME,PATIENT_LOCAL_NAME,BIRTHDATE,SEX)"
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.patientID + "','" + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','" + rptInfo.birthday.ToString("yyyy-MM-dd") + "','" + rptInfo.gender + "'); "
                        + " insert into GW_Order(DATA_ID,DATA_DT,ORDER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS) "
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.orderGuid + "', '" + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '16'); "
                        + " insert into GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                        + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD)"
                        + " values('" + guid + "', SYSDATE, '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
                        + " '" + rptInfo.patientID + "', '203', '" + rptInfo.modality + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                        + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportSubmitDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                        + " '" + ReportCommon.ReportCommon.StringLeft(rptInfo.operationStep, ServerPubFun.GetColumnWidth("GW_Report", "OBSERVATIONMETHOD") / 2) + "'); ";

                    sql = "begin " + sql + " commit; end;";

                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteNonQuery(sql);

                        if (rptInfo.wysText != null && rptInfo.wysText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "DIAGNOSE", rptInfo.wysText, rptInfo.wysText.Length, RisDAL.ConnectionState.KeepOpen);
                        }

                        if (rptInfo.wygText != null && rptInfo.wygText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "COMMENTS", rptInfo.wygText, rptInfo.wygText.Length, RisDAL.ConnectionState.KeepOpen);
                        }
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
        CommonGlobalSettings.HippaName.ActionCode.Submit,
        rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);

                    }
                }
                catch (Exception)
                {
                    // Hippa
                    Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                        CommonGlobalSettings.HippaName.ActionCode.Submit,
                        rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                }
                
            }

            private void OnReject(tagReportInfo rptInfo, ReportCommon.RP_Status oldStatus)
            {
                try
                {

                    if (oldStatus == ReportCommon.RP_Status.Reject)
                        return;

                    // Need to send gateway
                    if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                        return;

                    // Gateway
                    string guid = Guid.NewGuid().ToString();
                    string sql = "insert into GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                        + " values('" + guid + "', SYSDATE, '32', 'ReportGuid', 'Local'); "
                        + " insert into GW_Patient(DATA_ID,DATA_DT,PATIENTID,PATIENT_NAME,PATIENT_LOCAL_NAME,BIRTHDATE,SEX)"
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.patientID + "','" + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','" + rptInfo.birthday.ToString("yyyy-MM-dd") + "','" + rptInfo.gender + "'); "
                        + " insert into GW_Order(DATA_ID,DATA_DT,ORDER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS) "
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.orderGuid + "', '" + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '16'); "
                        + " insert into GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                        + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD)"
                        + " values('" + guid + "', SYSDATE, '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
                        + " '" + rptInfo.patientID + "', '205', '" + rptInfo.modality + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                        //+ " '" + rptInfo.reportApprover + "', '" + rptInfo.reportCreateDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                        + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportRejectDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                        + " '" + ReportCommon.ReportCommon.StringLeft(rptInfo.operationStep, ServerPubFun.GetColumnWidth("GW_Report", "OBSERVATIONMETHOD") / 2) + "'); ";

                    sql = "begin " + sql + " commit; end;";

                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteNonQuery(sql);

                        if (rptInfo.wysText != null && rptInfo.wysText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "DIAGNOSE", rptInfo.wysText, rptInfo.wysText.Length, RisDAL.ConnectionState.KeepOpen);
                        }

                        if (rptInfo.wygText != null && rptInfo.wygText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "COMMENTS", rptInfo.wygText, rptInfo.wygText.Length, RisDAL.ConnectionState.KeepOpen);
                        }
                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Reject,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);

                    }
                }
                catch (Exception)
                {
                    // Hippa
                    Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                        CommonGlobalSettings.HippaName.ActionCode.Reject,
                        rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                }
                

            }

            private void OnApprove(tagReportInfo rptInfo, ReportCommon.RP_Status oldStatus)
            {
                try
                {
                    // Need to send gateway
                    if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                        return;

                    // Gateway
                    string guid = Guid.NewGuid().ToString();
                    string sql = "insert into GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                        + " values('" + guid + "', SYSDATE, '" + (oldStatus != ReportCommon.RP_Status.FirstApprove ? "32" : "31") + "', 'ReportGuid', 'Local'); "
                        + " insert into GW_Patient(DATA_ID,DATA_DT,PATIENTID,PATIENT_NAME,PATIENT_LOCAL_NAME,BIRTHDATE,SEX)"
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.patientID + "','" + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','" + rptInfo.birthday.ToString("yyyy-MM-dd") + "','" + rptInfo.gender + "'); "
                        + " insert into GW_Order(DATA_ID,DATA_DT,ORDER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS) "
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.orderGuid + "', '" + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '16'); "
                        + " insert into GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                        + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD)"
                        + " values('" + guid + "', SYSDATE, '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
                        + " '" + rptInfo.patientID + "', '206', '" + rptInfo.modality + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                        + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportApproveDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                        + " '" + ReportCommon.ReportCommon.StringLeft(rptInfo.operationStep, ServerPubFun.GetColumnWidth("GW_Report", "OBSERVATIONMETHOD") / 2) + "'); ";

                    sql = "begin " + sql + " commit; end;";

                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteNonQuery(sql);

                        if (rptInfo.wysText != null && rptInfo.wysText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "DIAGNOSE", rptInfo.wysText, rptInfo.wysText.Length, RisDAL.ConnectionState.KeepOpen);
                        }

                        if (rptInfo.wygText != null && rptInfo.wygText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "COMMENTS", rptInfo.wygText, rptInfo.wygText.Length, RisDAL.ConnectionState.KeepOpen);
                        }
                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Approved,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);

                    }
                }
                catch (Exception)
                {
                                   // Hippa
                Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                    CommonGlobalSettings.HippaName.ActionCode.Approved,
                    rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                }

                
            }

            private void OnSecondApprove(tagReportInfo rptInfo, ReportCommon.RP_Status oldStatus)
            {
                try
                {

                    // Need to send gateway
                    if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                        return;

                    // Gateway
                    string guid = Guid.NewGuid().ToString();
                    string sql = "insert into GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                        + " values('" + guid + "', SYSDATE, '" + (oldStatus != ReportCommon.RP_Status.FirstApprove ? "32" : "31") + "', 'ReportGuid', 'Local'); "
                        + " insert into GW_Patient(DATA_ID,DATA_DT,PATIENTID,PATIENT_NAME,PATIENT_LOCAL_NAME,BIRTHDATE,SEX)"
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.patientID + "','" + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','" + rptInfo.birthday.ToString("yyyy-MM-dd") + "','" + rptInfo.gender + "'); "
                        + " insert into GW_Order(DATA_ID,DATA_DT,ORDER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS) "
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.orderGuid + "', '" + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '16'); "
                        + " insert into GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                        + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD)"
                        + " values('" + guid + "', SYSDATE, '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
                        + " '" + rptInfo.patientID + "', '206', '" + rptInfo.modality + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                        + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportApproveDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                        + " '" + ReportCommon.ReportCommon.StringLeft(rptInfo.operationStep, ServerPubFun.GetColumnWidth("GW_Report", "OBSERVATIONMETHOD") / 2) + "'); ";

                    sql = "begin " + sql + " commit; end;";

                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteNonQuery(sql);

                        if (rptInfo.wysText != null && rptInfo.wysText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "DIAGNOSE", rptInfo.wysText, rptInfo.wysText.Length, RisDAL.ConnectionState.KeepOpen);
                        }

                        if (rptInfo.wygText != null && rptInfo.wygText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "COMMENTS", rptInfo.wygText, rptInfo.wygText.Length, RisDAL.ConnectionState.KeepOpen);
                        }

                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.SecondApproved,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);


                    }
                }
                catch (Exception)
                {
                   // Hippa
                Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                    CommonGlobalSettings.HippaName.ActionCode.SecondApproved,
                    rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                }

            }


            private void OnRebuild(tagReportInfo rptInfo)
            {
                try
                {


                    // Need to send gateway
                    if (!ServerPubFun.GetSystemProfile_Bool("SendToGateServer", ReportCommon.ModuleID.Integration))
                        return;

                    // Gateway
                    string guid = Guid.NewGuid().ToString();
                    string sql = "insert into GW_DataIndex(data_id, data_dt, event_type, RECORD_INDEX_1, Data_Source)"
                        + " values('" + guid + "', SYSDATE, '31', 'ReportGuid', 'Local'); "
                        + " insert into GW_Patient(DATA_ID,DATA_DT,PATIENTID,PATIENT_NAME,PATIENT_LOCAL_NAME,BIRTHDATE,SEX)"
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.patientID + "','" + rptInfo.patientName + "',N'" + rptInfo.patientLocalName + "','" + rptInfo.birthday.ToString("yyyy-MM-dd") + "','" + rptInfo.gender + "'); "
                        + " insert into GW_Order(DATA_ID,DATA_DT,ORDER_NO,FILLER_NO,PATIENT_ID,EXAM_STATUS) "
                        + "    values('" + guid + "', SYSDATE, '" + rptInfo.orderGuid + "', '" + rptInfo.AccNO + "', '" + rptInfo.patientID + "', '16'); "
                        + " insert into GW_Report(data_id, data_dt, report_no, ACCESSION_NUMBER, PATIENT_ID, REPORT_STATUS, MODALITY, "
                        + " REPORT_TYPE, REPORT_FILE, REPORT_WRITER, REPORT_APPROVER, REPORTDT, OBSERVATIONMETHOD)"
                        + " values('" + guid + "', SYSDATE, '" + rptInfo.reportGuid + "', '" + rptInfo.AccNO + "',"
                        + " '" + rptInfo.patientID + "', '204', '" + rptInfo.modality + "', '0', '', '" + rptInfo.reportCreater_LocalName + "',"
                        + " '" + rptInfo.reportApprover_LocalName + "', '" + rptInfo.reportApproveDt.ToString("yyyy-MM-dd HH:mm:ss") + "',"
                        + " '" + ReportCommon.ReportCommon.StringLeft(rptInfo.operationStep, ServerPubFun.GetColumnWidth("GW_Report", "OBSERVATIONMETHOD") / 2) + "'); ";

                    sql = "begin " + sql + " commit; end;";



                    using (RisDAL dal = new RisDAL())
                    {

                        dal.ExecuteNonQuery(sql);

                        if (rptInfo.wysText != null && rptInfo.wysText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "DIAGNOSE", rptInfo.wysText, rptInfo.wysText.Length, RisDAL.ConnectionState.KeepOpen);
                        }

                        if (rptInfo.wygText != null && rptInfo.wygText.Length > 0)
                        {
                            dal.WriteLargeObj("GW_Report", "data_id", guid, "COMMENTS", rptInfo.wygText, rptInfo.wygText.Length, RisDAL.ConnectionState.KeepOpen);
                        }
                        // Hippa
                        Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                            CommonGlobalSettings.HippaName.ActionCode.Rebuild,
                            rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", true);


                    }
                }
                catch (Exception)
                {
                    // Hippa
                    Server.Utilities.HippaLogTool.HippaLogTool.AuditPatientCareAssignMsg(
                        CommonGlobalSettings.HippaName.ActionCode.Rebuild,
                        rptInfo.AccNO, rptInfo.reportGuid, rptInfo.reportName, rptInfo.patientID, rptInfo.patientName, "", false);
                }
                
            }
        }
    }
}

