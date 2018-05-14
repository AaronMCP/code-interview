using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCommon
{
    public class ReportCommon
    {
        #region private variable

        const char SEPARATOR_0 = '&';
        const char SEPARATOR_1 = '=';


        static string _FIELDNAME_PROCEDUREGUID = "tRegProcedure" + FIELD_SEPERATOR + "ProcedureGuid";
        static string _FIELDNAME_PROCEDURECODE = "tRegProcedure" + FIELD_SEPERATOR + "ProcedureCode";
        static string _FIELDNAME_ACCNO = "tRegOrder" + FIELD_SEPERATOR + "AccNo";
        static string _FIELDNAME_LOCALNAME = "tRegPatient" + FIELD_SEPERATOR + "LocalName";
        static string _FIELDNAME_GLOBALID = "tRegPatient" + FIELD_SEPERATOR + "GlobalID";
        static string _FIELDNAME_WYS = "tReport" + FIELD_SEPERATOR + "WYS";

        static string _FIELDNAME_FIRSTAPPROVERSIGN = "tReport" + FIELD_SEPERATOR + "FIRSTAPPROVERSIGN";
        static string _FIELDNAME_SECONDAPPROVERSIGN = "tReport" + FIELD_SEPERATOR + "SECONDAPPROVERSIGN";
        static string _FIELDNAME_SUBMITTERSIGN = "tReport" + FIELD_SEPERATOR + "SUBMITTERSIGN";

        static string _FIELDNAME_FIRSTAPPROVERSIGNTIMESTAMP = "tReport" + FIELD_SEPERATOR + "FIRSTAPPROVERSIGNTIMESTAMP";
        static string _FIELDNAME_SECONDAPPROVERSIGNTIMESTAMP = "tReport" + FIELD_SEPERATOR + "SECONDAPPROVERSIGNTIMESTAMP";
        static string _FIELDNAME_SUBMITTERSIGNTIMESTAMP = "tReport" + FIELD_SEPERATOR + "SUBMITTERSIGNTIMESTAMP";

        static string _FIELDNAME_CREATORSIGNIMAGE = "CREATORSIGN";
        static string _FIELDNAME_FIRSTAPPROVERSIGNIMAGE = "FIRSTAPPROVERSIGN";
        static string _FIELDNAME_SECONDAPPROVERSIGNIMAGE = "SECONDAPPROVERSIGN";
        static string _FIELDNAME_SUBMITTERSIGNIMAGE = "SUBMITTERSIGN";

        static string _FIELDNAME_FIRSTAPPROVERSIGNIMAGE_CA = "FIRSTAPPROVERSIGNIMAGE_CA";
        static string _FIELDNAME_SECONDAPPROVERSIGNIMAGE_CA = "SECONDAPPROVERSIGNIMAGE_CA";
        static string _FIELDNAME_SUBMITTERSIGNIMAGE_CA = "SUBMITTERSIGNIMAGE_CA";
        static string _FIELDNAME_WYG = "tReport" + FIELD_SEPERATOR + "WYG";
        static string _FIELDNAME_APPENDINFO = "tReport" + FIELD_SEPERATOR + "AppendInfo";
        static string _FIELDNAME_TECHINFO = "tReport" + FIELD_SEPERATOR + "TechInfo";
        static string _FIELDNAME_OBSERVATION = "tRegOrder" + FIELD_SEPERATOR + "Observation";
        static string _FIELDNAME_INSPECTION = "tRegOrder" + FIELD_SEPERATOR + "Inspection";
        static string _FIELDNAME_VISITGUID = "tRegOrder" + FIELD_SEPERATOR + "VisitGuid";
        static string _FIELDNAME_DOCTORADVICE = "tReport" + FIELD_SEPERATOR + "doctorAdvice";
        static string _FIELDNAME_ACRCODE = "tReport" + FIELD_SEPERATOR + "ACRCode";
        static string _FIELDNAME_ACRANATOMIC = "tReport" + FIELD_SEPERATOR + "ACRAnatomic";
        static string _FIELDNAME_ACRPATHOLOGIC = "tReport" + FIELD_SEPERATOR + "ACRPathologic";
        static string _FIELDNAME_REPORTGUID = "tReport" + FIELD_SEPERATOR + "reportGuid";
        static string _FIELDNAME_REPORTNAME = "tReport" + FIELD_SEPERATOR + "reportName";
        static string _FIELDNAME_REPORTTEXT = "tReport" + FIELD_SEPERATOR + "reportText";
        static string _FIELDNAME_KEYWORD = "tReport" + FIELD_SEPERATOR + "keyWord";
        static string _FIELDNAME_ISPOSITIVE = "tReport" + FIELD_SEPERATOR + "ispositive";
        static string _FIELDNAME_ISDIAGNOSISRIGHT = "tReport" + FIELD_SEPERATOR + "isDiagnosisRight";
        static string _FIELDNAME_PROCEDURECODEDESC = "tProcedureCode" + FIELD_SEPERATOR + "Description";
        static string _FIELDNAME_REPORTSTATUS = "tReport" + FIELD_SEPERATOR + "status";
        static string _FIELDNAME_REPORTCREATER = "tReport" + FIELD_SEPERATOR + "creater";
        static string _FIELDNAME_REPORTMENDER = "tReport" + FIELD_SEPERATOR + "mender";
        static string _FIELDNAME_REPORTSUBMITTER = "tReport" + FIELD_SEPERATOR + "submitter";
        static string _FIELDNAME_REPORTFIRSTAPPROVER = "tReport" + FIELD_SEPERATOR + "firstApprover";
        static string _FIELDNAME_REPORTSECONDAPPROVER = "tReport" + FIELD_SEPERATOR + "secondApprover";
        static string _FIELDNAME_REPORTREJECTER = "tReport" + FIELD_SEPERATOR + "rejecter";

        static string _FIELDNAME_REPORTREJECTTOOBJECT = "tReport" + FIELD_SEPERATOR + "rejecttoobject";
        static string _FIELDNAME_REPORTRECUPERATOR = "tReport" + FIELD_SEPERATOR + "recuperator";
        static string _FIELDNAME_REJECTTOOBJECT = "tReport" + FIELD_SEPERATOR + "rejecttoobject";
        static string _FIELDNAME_EXAMNUMBER = "PATIENTEXAMNO";
        static string _FIELDNAME_REPORTDOMAIN = "tReport" + FIELD_SEPERATOR + "domain";
        static string _FIELDNAME_REPORTSUBMITDOMAIN = "tReport" + FIELD_SEPERATOR + "submitdomain";
        static string _FIELDNAME_REPORTREJECTDOMAIN = "tReport" + FIELD_SEPERATOR + "rejectdomain";
        static string _FIELDNAME_REPORTAPPROVEDOMAIN = "tReport" + FIELD_SEPERATOR + "approvedomain";
        static string _FIELDNAME_PATIENTGUID = "tRegPatient" + FIELD_SEPERATOR + "PatientGuid";
        static string _FIELDNAME_ISCHARGE = "tRegOrder" + FIELD_SEPERATOR + "IsCharge";
        static string _FIELDNAME_BEDSIDE = "tRegOrder" + FIELD_SEPERATOR + "BedSide";
        static string _FIELDNAME_ISFILMSENT = "tRegOrder" + FIELD_SEPERATOR + "IsFilmSent";
        static string _FIELDNAME_FILMSENTOPERATOR = "tRegOrder" + FIELD_SEPERATOR + "FilmSentOperator";
        static string _FIELDNAME_FILMSENTDT = "tRegOrder" + FIELD_SEPERATOR + "FilmSentDt";
        static string _FIELDNAME_EFILMNUMBER = "tRegOrder" + FIELD_SEPERATOR + "EFilmNumber";
        static string _FIELDNAME_PRINTCOPIES = "tReport" + FIELD_SEPERATOR + "PrintCopies";
        

        #endregion

        public static string FIELD_SEPERATOR
        {
            get { return "__"; }
        }

        public static int DEFAULT_PAGESIZE
        {
            get { return 50; }
        }

        public static int FTP_LEVELDEPTH
        {
            get { return 2; }
        }

        #region Field Definition

        public static string FIELDNAME_PROCEDUREGUID { get { return _FIELDNAME_PROCEDUREGUID; } }

        public static string FIELDNAME_PROCEDURECODE { get { return _FIELDNAME_PROCEDURECODE; } }

        public static string FIELDNAME_ACCNO { get { return _FIELDNAME_ACCNO; } }

        public static string FIELDNAME_FIRSTAPPROVEDATE { get { return "tReport" + FIELD_SEPERATOR + "FirstApproveDt"; } }

        public static string FIELDNAME_FIRSTAPPROVER { get { return "tReport" + FIELD_SEPERATOR + "FirstApprover"; } }

        public static string FIELDNAME_SECONDAPPROVER { get { return "tReport" + FIELD_SEPERATOR + "SecondApprover"; } }

        public static string FIELDNAME_LOCALNAME { get { return _FIELDNAME_LOCALNAME; } }
        public static string FIELDNAME_GLOBALID { get { return _FIELDNAME_GLOBALID; } }

        public static string FIELDNAME_WYS { get { return _FIELDNAME_WYS; } }

        public static string FIELDNAME_CREATORSIGNIMAGE { get { return _FIELDNAME_CREATORSIGNIMAGE; } }
        public static string FIELDNAME_FIRSTAPPROVERSIGNIMAGE { get { return _FIELDNAME_FIRSTAPPROVERSIGNIMAGE; } }
        public static string FIELDNAME_SECONDAPPROVERSIGNIMAGE { get { return _FIELDNAME_SECONDAPPROVERSIGNIMAGE; } }
        public static string FIELDNAME_SUBMITTERSIGNIMAGE { get { return _FIELDNAME_SUBMITTERSIGNIMAGE; } }

        public static string FIELDNAME_FIRSTAPPROVERSIGNIMAGE_CA { get { return _FIELDNAME_FIRSTAPPROVERSIGNIMAGE_CA; } }
        public static string FIELDNAME_SECONDAPPROVERSIGNIMAGE_CA { get { return _FIELDNAME_SECONDAPPROVERSIGNIMAGE_CA; } }
        public static string FIELDNAME_SUBMITTERSIGNIMAGE_CA { get { return _FIELDNAME_SUBMITTERSIGNIMAGE_CA; } }

        public static string FIELDNAME_FIRSTAPPROVERSIGN { get { return _FIELDNAME_FIRSTAPPROVERSIGN; } }
        public static string FIELDNAME_SECONDAPPROVERSIGN { get { return _FIELDNAME_SECONDAPPROVERSIGN; } }
        public static string FIELDNAME_SUBMITTERSIGN { get { return _FIELDNAME_SUBMITTERSIGN; } }

        public static string FIELDNAME_FIRSTAPPROVERSIGNTIMESTAMP { get { return _FIELDNAME_FIRSTAPPROVERSIGNTIMESTAMP; } }
        public static string FIELDNAME_SECONDAPPROVERSIGNTIMESTAMP { get { return _FIELDNAME_SECONDAPPROVERSIGNTIMESTAMP; } }
        public static string FIELDNAME_SUBMITTERSIGNTIMESTAMP { get { return _FIELDNAME_SUBMITTERSIGNTIMESTAMP; } }

        public static string FIELDNAME_EXAMNUMBER { get { return _FIELDNAME_EXAMNUMBER; } }

        public static string FIELDNAME_WYG { get { return _FIELDNAME_WYG; } }

        public static string FIELDNAME_WYSTEXT { get { return "tReport" + FIELD_SEPERATOR + "WYSText"; } }

        public static string FIELDNAME_WYGTEXT { get { return "tReport" + FIELD_SEPERATOR + "WYGText"; } }

        public static string FIELDNAME_APPENDINFO { get { return _FIELDNAME_APPENDINFO; } }

        public static string FIELDNAME_TECHINFO { get { return _FIELDNAME_TECHINFO; } }

        public static string FIELDNAME_OBSERVATION { get { return _FIELDNAME_OBSERVATION; } }

        public static string FIELDNAME_INSPECTION { get { return _FIELDNAME_INSPECTION; } }

        public static string FIELDNAME_VISITGUID { get { return _FIELDNAME_VISITGUID; } }

        public static string FIELDNAME_DOCTORADVICE { get { return _FIELDNAME_DOCTORADVICE; } }

        public static string FIELDNAME_ACRCODE { get { return _FIELDNAME_ACRCODE; } }

        public static string FIELDNAME_ACRANATOMIC { get { return _FIELDNAME_ACRANATOMIC; } }

        public static string FIELDNAME_ACRPATHOLOGIC { get { return _FIELDNAME_ACRPATHOLOGIC; } }

        public static string FIELDNAME_REPORTGUID { get { return _FIELDNAME_REPORTGUID; } }

        public static string FIELDNAME_REPORTNAME { get { return _FIELDNAME_REPORTNAME; } }

        public static string FIELDNAME_REPORTTEXT { get { return _FIELDNAME_REPORTTEXT; } }

        public static string FIELDNAME_REPORTDOMAIN { get { return _FIELDNAME_REPORTDOMAIN; } }
        public static string FIELDNAME_REPORTSUBMITDOMAIN { get { return _FIELDNAME_REPORTSUBMITDOMAIN; } }
        public static string FIELDNAME_REPORTREJECTDOMAIN { get { return _FIELDNAME_REPORTREJECTDOMAIN; } }
        public static string FIELDNAME_REPORTAPPROVEDOMAIN { get { return _FIELDNAME_REPORTAPPROVEDOMAIN; } }

        public static string FIELDNAME_KEYWORD { get { return _FIELDNAME_KEYWORD; } }

        public static string FIELDNAME_ISPOSITIVE { get { return _FIELDNAME_ISPOSITIVE; } }

        public static string FIELDNAME_ISDIAGNOSISRIGHT { get { return _FIELDNAME_ISDIAGNOSISRIGHT; } }

        public static string FIELDNAME_PROCEDURECODEDESC { get { return _FIELDNAME_PROCEDURECODEDESC; } }

        public static string FIELDNAME_PROCEDURECODEBODYCATEGORY { get { return "tProcedureCode" + FIELD_SEPERATOR + "BodyCategory"; } }

        public static string FIELDNAME_PROCEDURECODEBODYPART { get { return "tProcedureCode" + FIELD_SEPERATOR + "BodyPart"; } }

        public static string FIELDNAME_REPORTSTATUS { get { return _FIELDNAME_REPORTSTATUS; } }

        public static string FIELDNAME_REPORTREJECTER { get { return "tReport" + FIELD_SEPERATOR + "Rejecter"; } }
        public static string FIELDNAME_REPORT_REJECTDT { get { return "tReport" + FIELD_SEPERATOR + "RejectDt"; } }
        public static string FIELDNAME_REJECTTOOBJECT { get { return _FIELDNAME_REJECTTOOBJECT; } }

        public static string FIELDNAME_MODALITYTYPE { get { return "tProcedureCode" + FIELD_SEPERATOR + "ModalityType"; } }

        public static string FIELDNAME_PATIENTID { get { return "tRegPatient" + FIELD_SEPERATOR + "PatientID"; } }
        public static string FIELDNAME_PATIENT_LOCALNAME { get { return "tRegPatient" + FIELD_SEPERATOR + "LocalName"; } }
        public static string FIELDNAME_PATIENT_ENGLISHNAME { get { return "tRegPatient" + FIELD_SEPERATOR + "EnglishName"; } }
        public static string FIELDNAME_PATIENT_ISVIP { get { return "tRegPatient" + FIELD_SEPERATOR + "IsVIP"; } }

        public static string FIELDNAME_RPCONTRASTNAME { get { return "tRegProcedure" + FIELD_SEPERATOR + "contrastname"; } }

        public static string FIELDNAME_RPSTATUS { get { return "tRegProcedure" + FIELD_SEPERATOR + "status"; } }

        public static string FIELDNAME_RPSTATUS__DESC { get { return "tRegProcedure" + FIELD_SEPERATOR + "status__DESC"; } }

        public static string FIELDNAME_MODALITY { get { return "tRegProcedure" + FIELD_SEPERATOR + "modality"; } }

        public static string FIELDNAME_ISEXISTIMAGE { get { return "tRegProcedure" + FIELD_SEPERATOR + "IsExistImage"; } }

        public static string FIELDNAME_REGPROCEDURE_EXAMINEDT { get { return "tRegProcedure" + FIELD_SEPERATOR + "ExamineDt"; } }

        public static string FIELDNAME_REGPROCEDURE_REGISTERDT { get { return "tRegProcedure" + FIELD_SEPERATOR + "RegisterDt"; } }

        public static string FIELDNAME_REPORT_CREATEDT { get { return "tReport" + FIELD_SEPERATOR + "CreateDt"; } }

        public static string FIELDNAME_REGPROCEDUREOPERATIONSTEP { get { return "tRegProcedure" + FIELD_SEPERATOR + "OperationStep"; } }

        public static string FIELDNAME_REGPROCEDURE_EXISTIMAGE { get { return "tRegProcedure" + FIELD_SEPERATOR + "isExistImage"; } }

        public static string FIELDNAME_REPORTQUALITY { get { return "tReport" + FIELD_SEPERATOR + "reportQuality"; } }

        public static string FIELDNAME_ACCORDRATE{ get { return "tReport" + FIELD_SEPERATOR + "AccordRate"; } }

        public static string FIELDNAME_SCORINGVERSION { get { return "tReport" + FIELD_SEPERATOR + "ScoringVersion"; } }

        public static string FIELDNAME_REPORT_SUBMITDT { get { return "tReport" + FIELD_SEPERATOR + "submitDt"; } }

        public static string FIELDNAME_REPORT_SUBMITTER { get { return "tReport" + FIELD_SEPERATOR + "submitter"; } }

        public static string FIELDNAME_REPORT_SUBMITTERNAME { get { return "tReport" + FIELD_SEPERATOR + "submitterName"; } }

        public static string FIELDNAME_REPORT_MENDERNAME { get { return "tReport" + FIELD_SEPERATOR + "menderName"; } }

        public static string FIELDNAME_REPORT_ISPRINT { get { return "tReport" + FIELD_SEPERATOR + "isPrint"; } }
        public static string FIELDNAME_REPORT_DELETEMARK { get { return "tReport" + FIELD_SEPERATOR + "DeleteMark"; } }
        public static string FIELDNAME_ORDER_CREATEDT { get { return "tRegOrder" + FIELD_SEPERATOR + "CreateDt"; } }

        public static string FIELDNAME_ORDERGUID { get { return "tRegOrder" + FIELD_SEPERATOR + "OrderGuid"; } }

        public static string FIELDNAME_ORDER_REGION { get { return "tRegOrder" + FIELD_SEPERATOR + "inHospitalRegion"; } }

        public static string FIELDNAME_ORDER_PATIENTTYPE { get { return "tRegOrder" + FIELD_SEPERATOR + "PatientType"; } }

        public static string FIELDNAME_ORDER_INHOSPITALNO { get { return "tRegOrder" + FIELD_SEPERATOR + "inHospitalNo"; } }

        public static string FIELDNAME_ORDER_CLINICNO { get { return "tRegOrder" + FIELD_SEPERATOR + "ClinicNo"; } }

        public static string FIELDNAME_ORDER_BEDNO { get { return "tRegOrder" + FIELD_SEPERATOR + "BedNo"; } }

        public static string FIELDNAME_ORDER_CURRENTAGE { get { return "tRegOrder" + FIELD_SEPERATOR + "CurrentAge"; } }

        public static string FIELDNAME_ORDER_DEPT { get { return "tRegOrder" + FIELD_SEPERATOR + "ApplyDept"; } }

        public static string FIELDNAME_GENDER { get { return "tRegPatient" + FIELD_SEPERATOR + "Gender"; } }

        public static string FIELDNAME_REPORT_CREATER { get { return "tReport" + FIELD_SEPERATOR + "Creater"; } }

        public static string FIELDNAME_REPORT_CREATERNAME { get { return "tReport" + FIELD_SEPERATOR + "CreaterName"; } }

        public static string FIELDNAME_REPORT_PRINTTEMPLATEGUID { get { return "tReport" + FIELD_SEPERATOR + "PrintTemplateGuid"; } }

        public static string FIELDNAME_ORDER_ISCHARGE { get { return _FIELDNAME_ISCHARGE; } }

        public static string FIELDNAME_ORDER_BEDSIDE { get { return _FIELDNAME_BEDSIDE; } }

        public static string FIELDNAME_ORDER_ISFILMSENT { get { return _FIELDNAME_ISFILMSENT; } }
        public static string FIELDNAME_ORDER_FILMSENTOPERATOR { get { return _FIELDNAME_FILMSENTOPERATOR; } }
        public static string FIELDNAME_ORDER_FILMSENDT { get { return _FIELDNAME_FILMSENTDT; } }
        public static string FIELDNAME_ORDER_EFILMNUMBER { get { return _FIELDNAME_EFILMNUMBER; } }

        public static string FIELDNAME_REPORT_PRINTCOPIES { get { return _FIELDNAME_PRINTCOPIES; } }

        public static string FIELDNAME_ORDER_ISSCAN { get { return "tRegOrder" + FIELD_SEPERATOR + "IsScan"; } }
        public static string FIELDNAME_ORDER_ISREFERRAL { get { return "tRegOrder" + FIELD_SEPERATOR + "IsReferral"; } }
        public static string FIELDNAME_ORDER_ASSIGN2SITE { get { return "tRegOrder" + FIELD_SEPERATOR + "Assign2Site"; } }

        public static string FIELDNAME_REPORT_CHECKITEMNAME { get { return "tReport" + FIELD_SEPERATOR + "CheckItemName"; } }
        public static string FIELDNAME_REPORT_COMMENTS { get { return "tReport" + FIELD_SEPERATOR + "Comments"; } }
        public static string FIELDNAME_REPORT_ISLEAVEWORD { get { return "tReport" + FIELD_SEPERATOR + "IsLeaveWord"; } }
        public static string FIELDNAME_UNAPPROVEWARNINGTIME { get { return "tRegProcedure" + FIELD_SEPERATOR + "WarningTime"; } }
        public static string FIELDNAME_REPORT_ISEXISTIMAGE { get { return "tRegProcedure" + FIELD_SEPERATOR + "IsExistImage"; } }
        public static string FIELDNAME_REPORT_ISDRAW { get { return "tReport" + FIELD_SEPERATOR + "IsDraw"; } }
        public static string FIELDNAME_REPORT_DRAWTIME { get { return "tReport" + FIELD_SEPERATOR + "DrawTime"; } }
        public static string FIELDNAME_REPORT_ISLEAVESOUND { get { return "tReport" + FIELD_SEPERATOR + "IsLeaveSound"; } }
        public static string FIELDNAME_REPORT_TAKEFILMDEPT { get { return "tReport" + FIELD_SEPERATOR + "TakeFilmDept"; } }
        public static string FIELDNAME_REPORT_TAKEFILMREGION { get { return "tReport" + FIELD_SEPERATOR + "TakeFilmRegion"; } }
        public static string FIELDNAME_REPORT_TAKEFILMCOMMENT { get { return "tReport" + FIELD_SEPERATOR + "TakeFilmComment"; } }
        public static string LOGTYPEPRINT { get { return "Print"; } }
        public static string LOGTYPEEXPORT { get { return "Export"; } }
        public static string FIELDNAME_PATIENTGUID { get { return _FIELDNAME_PATIENTGUID; } }
        public static string FIELDNAME_REPORT_OPTIONAL1 { get { return "tReport" + FIELD_SEPERATOR + "Optional1"; } }
        public static string FIELDNAME_REPORT_ISMODIFIED { get { return "tReport" + FIELD_SEPERATOR + "IsModified"; } }
        public static string FIELDNAME_ORDER_PHYSICALCOMPANY { get { return "tRegOrder" + FIELD_SEPERATOR + "PhysicalCompany"; } }
        public static string FIELDNAME_ORDER_PHYSICALCOMPANY_DESC { get { return "tRegOrder" + FIELD_SEPERATOR + "PhysicalCompany__Desc"; } }
        public static string FIELDNAME_ORDER_PHYSICALCOMPANY_TELEPHONE { get { return "tRegOrder" + FIELD_SEPERATOR + "PhysicalCompany__Telephone"; } }


        public const string FIELDNAME_tProcedureCode__ApprovedRadiologistWeight = "TPROCEDURECODE__APPROVEDRADIOLOGISTWEIGHT";
        public const string FIELDNAME_tProcedureCode__ApproveWarningTime = "TPROCEDURECODE__APPROVEWARNINGTIME";
        public const string FIELDNAME_tProcedureCode__BodyCategory = "TPROCEDURECODE__BODYCATEGORY";
        public const string FIELDNAME_tProcedureCode__BodyPart = "TPROCEDURECODE__BODYPART";
        public const string FIELDNAME_tProcedureCode__BodypartFrequency = "TPROCEDURECODE__BODYPARTFREQUENCY";
        public const string FIELDNAME_tProcedureCode__BookingNotice = "TPROCEDURECODE__BOOKINGNOTICE";
        public const string FIELDNAME_tProcedureCode__Charge = "TPROCEDURECODE__CHARGE";
        public const string FIELDNAME_tProcedureCode__CheckingItem = "TPROCEDURECODE__CHECKINGITEM";
        public const string FIELDNAME_tProcedureCode__CheckingItemFrequency = "TPROCEDURECODE__CHECKINGITEMFREQUENCY";
        public const string FIELDNAME_tProcedureCode__ContrastDose = "TPROCEDURECODE__CONTRASTDOSE";
        public const string FIELDNAME_tProcedureCode__ContrastName = "TPROCEDURECODE__CONTRASTNAME";
        public const string FIELDNAME_tProcedureCode__DefaultModality = "TPROCEDURECODE__DEFAULTMODALITY";
        public const string FIELDNAME_tProcedureCode__Description = "TPROCEDURECODE__DESCRIPTION";
        public const string FIELDNAME_tProcedureCode__Domain = "TPROCEDURECODE__DOMAIN";
        public const string FIELDNAME_tProcedureCode__Duration = "TPROCEDURECODE__DURATION";
        public const string FIELDNAME_tProcedureCode__Effective = "TPROCEDURECODE__EFFECTIVE";
        public const string FIELDNAME_tProcedureCode__EnglishDescription = "TPROCEDURECODE__ENGLISHDESCRIPTION";
        public const string FIELDNAME_tProcedureCode__Enhance = "TPROCEDURECODE__ENHANCE";
        public const string FIELDNAME_tProcedureCode__ExposalCount = "TPROCEDURECODE__EXPOSALCOUNT";
        public const string FIELDNAME_tProcedureCode__Externals = "TPROCEDURECODE__EXTERNALS";
        public const string FIELDNAME_tProcedureCode__FilmCount = "TPROCEDURECODE__FILMCOUNT";
        public const string FIELDNAME_tProcedureCode__FilmSpec = "TPROCEDURECODE__FILMSPEC";
        public const string FIELDNAME_tProcedureCode__Frequency = "TPROCEDURECODE__FREQUENCY";
        public const string FIELDNAME_tProcedureCode__ImageCount = "TPROCEDURECODE__IMAGECOUNT";
        public const string FIELDNAME_tProcedureCode__ModalityType = "TPROCEDURECODE__MODALITYTYPE";
        public const string FIELDNAME_tProcedureCode__Preparation = "TPROCEDURECODE__PREPARATION";
        public const string FIELDNAME_tProcedureCode__ProcedureCode = "TPROCEDURECODE__PROCEDURECODE";
        public const string FIELDNAME_tProcedureCode__RadiologistWeight = "TPROCEDURECODE__RADIOLOGISTWEIGHT";
        public const string FIELDNAME_tProcedureCode__ShortcutCode = "TPROCEDURECODE__SHORTCUTCODE";
        public const string FIELDNAME_tProcedureCode__Site = "TPROCEDURECODE__SITE";
        public const string FIELDNAME_tProcedureCode__TechnicianWeight = "TPROCEDURECODE__TECHNICIANWEIGHT";
        public const string FIELDNAME_tProcedureCode__UniqueID = "TPROCEDURECODE__UNIQUEID";

        public const string FIELDNAME_tRegOrder__AccNo = "TREGORDER__ACCNO";
        public const string FIELDNAME_tRegOrder__AgeInDays = "TREGORDER__AGEINDAYS";
        public const string FIELDNAME_tRegOrder__ApplyDept = "TREGORDER__APPLYDEPT";
        public const string FIELDNAME_tRegOrder__ApplyDoctor = "TREGORDER__APPLYDOCTOR";
        public const string FIELDNAME_tRegOrder__Assign2Site = "TREGORDER__ASSIGN2SITE";
        public const string FIELDNAME_tRegOrder__AssignDt = "TREGORDER__ASSIGNDT";
        public const string FIELDNAME_tRegOrder__BedNo = "TREGORDER__BEDNO";
        public const string FIELDNAME_tRegOrder__Bedside = "TREGORDER__BEDSIDE";
        public const string FIELDNAME_tRegOrder__BloodSugar = "TREGORDER__BLOODSUGAR";
        public const string FIELDNAME_tRegOrder__BodyHeight = "TREGORDER__BODYHEIGHT";
        public const string FIELDNAME_tRegOrder__BodyWeight = "TREGORDER__BODYWEIGHT";
        public const string FIELDNAME_tRegOrder__BookingSite = "TREGORDER__BOOKINGSITE";
        public const string FIELDNAME_tRegOrder__CardNo = "TREGORDER__CARDNO";
        public const string FIELDNAME_tRegOrder__ChargeType = "TREGORDER__CHARGETYPE";
        public const string FIELDNAME_tRegOrder__ClinicNo = "TREGORDER__CLINICNO";
        public const string FIELDNAME_tRegOrder__Comments = "TREGORDER__COMMENTS";
        public const string FIELDNAME_tRegOrder__CreateDt = "TREGORDER__CREATEDT";
        public const string FIELDNAME_tRegOrder__CurGender = "TREGORDER__CURGENDER";
        public const string FIELDNAME_tRegOrder__CurPatientName = "TREGORDER__CURPATIENTNAME";
        public const string FIELDNAME_tRegOrder__CurrentAge = "TREGORDER__CURRENTAGE";
        public const string FIELDNAME_tRegOrder__CurrentSite = "TREGORDER__CURRENTSITE";
        public const string FIELDNAME_tRegOrder__Domain = "TREGORDER__DOMAIN";
        public const string FIELDNAME_tRegOrder__ERequisition = "TREGORDER__EREQUISITION";
        public const string FIELDNAME_tRegOrder__ErethismCode = "TREGORDER__ERETHISMCODE";
        public const string FIELDNAME_tRegOrder__ErethismGrade = "TREGORDER__ERETHISMGRADE";
        public const string FIELDNAME_tRegOrder__ErethismType = "TREGORDER__ERETHISMTYPE";
        public const string FIELDNAME_tRegOrder__ExamAccNo = "TREGORDER__EXAMACCNO";
        public const string FIELDNAME_tRegOrder__EXAMALERT1 = "TREGORDER__EXAMALERT1";
        public const string FIELDNAME_tRegOrder__EXAMALERT2 = "TREGORDER__EXAMALERT2";
        public const string FIELDNAME_tRegOrder__ExamDomain = "TREGORDER__EXAMDOMAIN";
        public const string FIELDNAME_tRegOrder__ExamSite = "TREGORDER__EXAMSITE";
        public const string FIELDNAME_tRegOrder__ExternalOptional1 = "TREGORDER__EXTERNALOPTIONAL1";
        public const string FIELDNAME_tRegOrder__ExternalOptional2 = "TREGORDER__EXTERNALOPTIONAL2";
        public const string FIELDNAME_tRegOrder__ExternalOptional3 = "TREGORDER__EXTERNALOPTIONAL3";
        public const string FIELDNAME_tRegOrder__FilmFee = "TREGORDER__FILMFEE";
        public const string FIELDNAME_tRegOrder__FilmSentDt = "TREGORDER__FILMSENTDT";
        public const string FIELDNAME_tRegOrder__FilmSentOperator = "TREGORDER__FILMSENTOPERATOR";
        public const string FIELDNAME_tRegOrder__GoOnGoTime = "TREGORDER__GOONGOTIME";
        public const string FIELDNAME_tRegOrder__HealthHistory = "TREGORDER__HEALTHHISTORY";
        public const string FIELDNAME_tRegOrder__HisID = "TREGORDER__HISID";
        public const string FIELDNAME_tRegOrder__InhospitalNo = "TREGORDER__INHOSPITALNO";
        public const string FIELDNAME_tRegOrder__InhospitalRegion = "TREGORDER__INHOSPITALREGION";
        public const string FIELDNAME_tRegOrder__InitialDomain = "TREGORDER__INITIALDOMAIN";
        public const string FIELDNAME_tRegOrder__InjectDose = "TREGORDER__INJECTDOSE";
        public const string FIELDNAME_tRegOrder__InjectorRemnant = "TREGORDER__INJECTORREMNANT";
        public const string FIELDNAME_tRegOrder__InjectTime = "TREGORDER__INJECTTIME";
        public const string FIELDNAME_tRegOrder__Insulin = "TREGORDER__INSULIN";
        public const string FIELDNAME_tRegOrder__InternalOptional1 = "TREGORDER__INTERNALOPTIONAL1";
        public const string FIELDNAME_tRegOrder__InternalOptional2 = "TREGORDER__INTERNALOPTIONAL2";
        public const string FIELDNAME_tRegOrder__IsCharge = "TREGORDER__ISCHARGE";
        public const string FIELDNAME_tRegOrder__IsEmergency = "TREGORDER__ISEMERGENCY";
        public const string FIELDNAME_tRegOrder__IsFilmSent = "TREGORDER__ISFILMSENT";
        public const string FIELDNAME_tRegOrder__IsReferral = "TREGORDER__ISREFERRAL";
        public const string FIELDNAME_tRegOrder__IsScan = "TREGORDER__ISSCAN";
        public const string FIELDNAME_tRegOrder__LMP = "TREGORDER__LMP";
        public const string FIELDNAME_tRegOrder__MedicalAlert = "TREGORDER__MEDICALALERT";
        public const string FIELDNAME_tRegOrder__Observation = "TREGORDER__OBSERVATION";
        public const string FIELDNAME_tRegOrder__Optional1 = "TREGORDER__OPTIONAL1";
        public const string FIELDNAME_tRegOrder__Optional2 = "TREGORDER__OPTIONAL2";
        public const string FIELDNAME_tRegOrder__Optional3 = "TREGORDER__OPTIONAL3";
        public const string FIELDNAME_tRegOrder__OrderGuid = "TREGORDER__ORDERGUID";
        public const string FIELDNAME_tRegOrder__OrderMessage = "TREGORDER__ORDERMESSAGE";
        public const string FIELDNAME_tRegOrder__PathologicalFindings = "TREGORDER__PATHOLOGICALFINDINGS";
        public const string FIELDNAME_tRegOrder__PatientGuid = "TREGORDER__PATIENTGUID";
        public const string FIELDNAME_tRegOrder__PatientType = "TREGORDER__PATIENTTYPE";
        public const string FIELDNAME_tRegOrder__Priority = "TREGORDER__PRIORITY";
        public const string FIELDNAME_tRegOrder__ReferralID = "TREGORDER__REFERRALID";
        public const string FIELDNAME_tRegOrder__RegSite = "TREGORDER__REGSITE";
        public const string FIELDNAME_tRegOrder__RemoteAccNo = "TREGORDER__REMOTEACCNO";
        public const string FIELDNAME_tRegOrder__StudyID = "TREGORDER__STUDYID";
        public const string FIELDNAME_tRegOrder__StudyInstanceUID = "TREGORDER__STUDYINSTANCEUID";
        public const string FIELDNAME_tRegOrder__SubmitDept = "TREGORDER__SUBMITDEPT";
        public const string FIELDNAME_tRegOrder__SubmitDoctor = "TREGORDER__SUBMITDOCTOR";
        public const string FIELDNAME_tRegOrder__SubmitHospital = "TREGORDER__SUBMITHOSPITAL";
        public const string FIELDNAME_tRegOrder__TakeReportDate = "TREGORDER__TAKEREPORTDATE";
        public const string FIELDNAME_tRegOrder__ThreeDRebuild = "TREGORDER__THREEDREBUILD";
        public const string FIELDNAME_tRegOrder__TotalFee = "TREGORDER__TOTALFEE";
        public const string FIELDNAME_tRegOrder__UpdateTime = "TREGORDER__UPDATETIME";
        public const string FIELDNAME_tRegOrder__Uploaded = "TREGORDER__UPLOADED";
        public const string FIELDNAME_tRegOrder__visitcomment = "TREGORDER__VISITCOMMENT";
        public const string FIELDNAME_tRegOrder__VisitGuid = "TREGORDER__VISITGUID";

        public static string FIELDNAME_ORDER_FILMDRAWDEPT { get { return "TREGORDER__FILMDRAWDEPT"; } }
        public static string FIELDNAME_ORDER_FILMDRAWREGION { get { return "TREGORDER__FILMDRAWREGION"; } }
        public static string FIELDNAME_ORDER_FILMDRAWCOMMENT { get { return "TREGORDER__FILMDRAWCOMMENT"; } }
        public static string FIELDNAME_ORDER_FILMDRAWERSIGN { get { return "TREGORDER__FILMDRAWERSIGN"; } }

        public const string FIELDNAME_tRegPatient__Address = "TREGPATIENT__ADDRESS";
        public const string FIELDNAME_tRegPatient__Alias = "TREGPATIENT__ALIAS";
        public const string FIELDNAME_tRegPatient__Birthday = "TREGPATIENT__BIRTHDAY";
        public const string FIELDNAME_tRegPatient__Comments = "TREGPATIENT__COMMENTS";
        public const string FIELDNAME_tRegPatient__CreateDt = "TREGPATIENT__CREATEDT";
        public const string FIELDNAME_tRegPatient__Domain = "TREGPATIENT__DOMAIN";
        public const string FIELDNAME_tRegPatient__EnglishName = "TREGPATIENT__ENGLISHNAME";
        public const string FIELDNAME_tRegPatient__Gender = "TREGPATIENT__GENDER";
        public const string FIELDNAME_tRegPatient__GlobalID = "TREGPATIENT__GLOBALID";
        public const string FIELDNAME_tRegPatient__IsVIP = "TREGPATIENT__ISVIP";
        public const string FIELDNAME_tRegPatient__LocalName = "TREGPATIENT__LOCALNAME";
        public const string FIELDNAME_tRegPatient__Marriage = "TREGPATIENT__MARRIAGE";
        public const string FIELDNAME_tRegPatient__MedicareNo = "TREGPATIENT__MEDICARENO";
        public const string FIELDNAME_tRegPatient__Optional1 = "TREGPATIENT__OPTIONAL1";
        public const string FIELDNAME_tRegPatient__Optional2 = "TREGPATIENT__OPTIONAL2";
        public const string FIELDNAME_tRegPatient__Optional3 = "TREGPATIENT__OPTIONAL3";
        public const string FIELDNAME_tRegPatient__ParentName = "TREGPATIENT__PARENTNAME";
        public const string FIELDNAME_tRegPatient__PatientGuid = "TREGPATIENT__PATIENTGUID";
        public const string FIELDNAME_tRegPatient__PatientID = "TREGPATIENT__PATIENTID";
        public const string FIELDNAME_tRegPatient__ReferenceNo = "TREGPATIENT__REFERENCENO";
        public const string FIELDNAME_tRegPatient__RelatedID = "TREGPATIENT__RELATEDID";
        public const string FIELDNAME_tRegPatient__RemotePID = "TREGPATIENT__REMOTEPID";
        public const string FIELDNAME_tRegPatient__Site = "TREGPATIENT__SITE";
        public const string FIELDNAME_tRegPatient__SocialSecurityNo = "TREGPATIENT__SOCIALSECURITYNO";
        public const string FIELDNAME_tRegPatient__Telephone = "TREGPATIENT__TELEPHONE";
        public const string FIELDNAME_tRegPatient__UpdateTime = "TREGPATIENT__UPDATETIME";
        public const string FIELDNAME_tRegPatient__Uploaded = "TREGPATIENT__UPLOADED";

        public const string FIELDNAME_tRegProcedure__BodyCategory = "TREGPROCEDURE__BODYCATEGORY";
        public const string FIELDNAME_tRegProcedure__Bodypart = "TREGPROCEDURE__BODYPART";
        public const string FIELDNAME_tRegProcedure__Booker = "TREGPROCEDURE__BOOKER";
        public const string FIELDNAME_tRegProcedure__BookerName = "TREGPROCEDURE__BOOKERNAME";
        public const string FIELDNAME_tRegProcedure__BookingBeginDt = "TREGPROCEDURE__BOOKINGBEGINDT";
        public const string FIELDNAME_tRegProcedure__BookingEndDt = "TREGPROCEDURE__BOOKINGENDDT";
        public const string FIELDNAME_tRegProcedure__BookingNotice = "TREGPROCEDURE__BOOKINGNOTICE";
        public const string FIELDNAME_tRegProcedure__BookingTimeAlias = "TREGPROCEDURE__BOOKINGTIMEALIAS";
        public const string FIELDNAME_tRegProcedure__Charge = "TREGPROCEDURE__CHARGE";
        public const string FIELDNAME_tRegProcedure__CheckingItem = "TREGPROCEDURE__CHECKINGITEM";
        public const string FIELDNAME_tRegProcedure__Comments = "TREGPROCEDURE__COMMENTS";
        public const string FIELDNAME_tRegProcedure__ContrastDose = "TREGPROCEDURE__CONTRASTDOSE";
        public const string FIELDNAME_tRegProcedure__ContrastName = "TREGPROCEDURE__CONTRASTNAME";
        public const string FIELDNAME_tRegProcedure__CreateDt = "TREGPROCEDURE__CREATEDT";
        public const string FIELDNAME_tRegProcedure__Deposit = "TREGPROCEDURE__DEPOSIT";
        public const string FIELDNAME_tRegProcedure__Domain = "TREGPROCEDURE__DOMAIN";
        public const string FIELDNAME_tRegProcedure__ExamineDt = "TREGPROCEDURE__EXAMINEDT";
        public const string FIELDNAME_tRegProcedure__ExamSystem = "TREGPROCEDURE__EXAMSYSTEM";
        public const string FIELDNAME_tRegProcedure__ExposalCount = "TREGPROCEDURE__EXPOSALCOUNT";
        public const string FIELDNAME_tRegProcedure__FilmCount = "TREGPROCEDURE__FILMCOUNT";
        public const string FIELDNAME_tRegProcedure__FilmSpec = "TREGPROCEDURE__FILMSPEC";
        public const string FIELDNAME_tRegProcedure__ImageCount = "TREGPROCEDURE__IMAGECOUNT";
        public const string FIELDNAME_tRegProcedure__IsCharge = "TREGPROCEDURE__ISCHARGE";
        public const string FIELDNAME_tRegProcedure__IsExistImage = "TREGPROCEDURE__ISEXISTIMAGE";
        public const string FIELDNAME_tRegProcedure__IsPost = "TREGPROCEDURE__ISPOST";
        public const string FIELDNAME_tRegProcedure__MedicineUsage = "TREGPROCEDURE__MEDICINEUSAGE";
        public const string FIELDNAME_tRegProcedure__Mender = "TREGPROCEDURE__MENDER";
        public const string FIELDNAME_tRegProcedure__Modality = "TREGPROCEDURE__MODALITY";
        public const string FIELDNAME_tRegProcedure__ModalityType = "TREGPROCEDURE__MODALITYTYPE";
        public const string FIELDNAME_tRegProcedure__ModifyDt = "TREGPROCEDURE__MODIFYDT";
        public const string FIELDNAME_tRegProcedure__OperationStep = "TREGPROCEDURE__OPERATIONSTEP";
        public const string FIELDNAME_tRegProcedure__Optional1 = "TREGPROCEDURE__OPTIONAL1";
        public const string FIELDNAME_tRegProcedure__Optional2 = "TREGPROCEDURE__OPTIONAL2";
        public const string FIELDNAME_tRegProcedure__Optional3 = "TREGPROCEDURE__OPTIONAL3";
        public const string FIELDNAME_tRegProcedure__OrderGuid = "TREGPROCEDURE__ORDERGUID";
        public const string FIELDNAME_tRegProcedure__Posture = "TREGPROCEDURE__POSTURE";
        public const string FIELDNAME_tRegProcedure__PreStatus = "TREGPROCEDURE__PRESTATUS";
        public const string FIELDNAME_tRegProcedure__Priority = "TREGPROCEDURE__PRIORITY";
        public const string FIELDNAME_tRegProcedure__ProcedureCode = "TREGPROCEDURE__PROCEDURECODE";
        public const string FIELDNAME_tRegProcedure__ProcedureGuid = "TREGPROCEDURE__PROCEDUREGUID";
        public const string FIELDNAME_tRegProcedure__QueueNo = "TREGPROCEDURE__QUEUENO";
        public const string FIELDNAME_tRegProcedure__RegisterDt = "TREGPROCEDURE__REGISTERDT";
        public const string FIELDNAME_tRegProcedure__Registrar = "TREGPROCEDURE__REGISTRAR";
        public const string FIELDNAME_tRegProcedure__RegistrarName = "TREGPROCEDURE__REGISTRARNAME";
        public const string FIELDNAME_tRegProcedure__RemoteRPID = "TREGPROCEDURE__REMOTERPID";
        public const string FIELDNAME_tRegProcedure__ReportGuid = "TREGPROCEDURE__REPORTGUID";
        public const string FIELDNAME_tRegProcedure__RPDesc = "TREGPROCEDURE__RPDESC";
        public const string FIELDNAME_tRegProcedure__Status = "TREGPROCEDURE__STATUS";
        public const string FIELDNAME_tRegProcedure__TechDoctor = "TREGPROCEDURE__TECHDOCTOR";
        public const string FIELDNAME_tRegProcedure__Technician = "TREGPROCEDURE__TECHNICIAN";
        public const string FIELDNAME_tRegProcedure__Technician1 = "TREGPROCEDURE__TECHNICIAN1";
        public const string FIELDNAME_tRegProcedure__Technician2 = "TREGPROCEDURE__TECHNICIAN2";
        public const string FIELDNAME_tRegProcedure__Technician3 = "TREGPROCEDURE__TECHNICIAN3";
        public const string FIELDNAME_tRegProcedure__Technician4 = "TREGPROCEDURE__TECHNICIAN4";
        public const string FIELDNAME_tRegProcedure__TechnicianName = "TREGPROCEDURE__TECHNICIANNAME";
        public const string FIELDNAME_tRegProcedure__TechNurse = "TREGPROCEDURE__TECHNURSE";
        public const string FIELDNAME_tRegProcedure__UnapprovedAssignDate = "TREGPROCEDURE__UNAPPROVEDASSIGNDATE";
        public const string FIELDNAME_tRegProcedure__UnapprovedCurrentOwner = "TREGPROCEDURE__UNAPPROVEDCURRENTOWNER";
        public const string FIELDNAME_tRegProcedure__UnapprovedPreviousOwner = "TREGPROCEDURE__UNAPPROVEDPREVIOUSOWNER";
        public const string FIELDNAME_tRegProcedure__UnwrittenAssignDate = "TREGPROCEDURE__UNWRITTENASSIGNDATE";
        public const string FIELDNAME_tRegProcedure__UnwrittenCurrentOwner = "TREGPROCEDURE__UNWRITTENCURRENTOWNER";
        public const string FIELDNAME_tRegProcedure__UnwrittenPreviousOwner = "TREGPROCEDURE__UNWRITTENPREVIOUSOWNER";
        public const string FIELDNAME_tRegProcedure__UpdateTime = "TREGPROCEDURE__UPDATETIME";
        public const string FIELDNAME_tRegProcedure__Uploaded = "TREGPROCEDURE__UPLOADED";
        public const string FIELDNAME_tRegProcedure__WarningTime = "TREGPROCEDURE__WARNINGTIME";
        public const string FIELDNAME_tRegProcedure__CheckItemName = "TREGPROCEDURE__CHECKITEMNAME";

        public const string FIELDNAME_tReport__AcrAnatomic = "TREPORT__ACRANATOMIC";
        public const string FIELDNAME_tReport__AcrCode = "TREPORT__ACRCODE";
        public const string FIELDNAME_tReport__AcrPathologic = "TREPORT__ACRPATHOLOGIC";
        public const string FIELDNAME_tReport__AppendInfo = "TREPORT__APPENDINFO";
        public const string FIELDNAME_tReport__CheckItemName = "TREPORT__CHECKITEMNAME";
        public const string FIELDNAME_tReport__CombinedForCertification = "TREPORT__COMBINEDFORCERTIFICATION";
        public const string FIELDNAME_tReport__Comments = "TREPORT__COMMENTS";
        public const string FIELDNAME_tReport__CreateDt = "TREPORT__CREATEDT";
        public const string FIELDNAME_tReport__Creater = "TREPORT__CREATER";
        public const string FIELDNAME_tReport__CreaterName = "TREPORT__CREATERNAME";
        public const string FIELDNAME_tReport__DeleteDt = "TREPORT__DELETEDT";
        public const string FIELDNAME_tReport__DeleteMark = "TREPORT__DELETEMARK";
        public const string FIELDNAME_tReport__Deleter = "TREPORT__DELETER";
        public const string FIELDNAME_tReport__DoctorAdvice = "TREPORT__DOCTORADVICE";
        public const string FIELDNAME_tReport__Domain = "TREPORT__DOMAIN";
        public const string FIELDNAME_tReport__DrawerSign = "TREPORT__DRAWERSIGN";
        public const string FIELDNAME_tReport__DrawTime = "TREPORT__DRAWTIME";
        public const string FIELDNAME_tReport__FirstApproveDomain = "TREPORT__FIRSTAPPROVEDOMAIN";
        public const string FIELDNAME_tReport__FirstApproveDt = "TREPORT__FIRSTAPPROVEDT";
        public const string FIELDNAME_tReport__FirstApprover = "TREPORT__FIRSTAPPROVER";
        public const string FIELDNAME_tReport__FirstApproverName = "TREPORT__FIRSTAPPROVERNAME";
        public const string FIELDNAME_tReport__FirstApproveSite = "TREPORT__FIRSTAPPROVESITE";
        public const string FIELDNAME_tReport__IsDiagnosisRight = "TREPORT__ISDIAGNOSISRIGHT";
        public const string FIELDNAME_tReport__IsDraw = "TREPORT__ISDRAW";
        public const string FIELDNAME_tReport__IsLeaveSound = "TREPORT__ISLEAVESOUND";
        public const string FIELDNAME_tReport__IsLeaveWord = "TREPORT__ISLEAVEWORD";
        public const string FIELDNAME_tReport__IsPositive = "TREPORT__ISPOSITIVE";
        public const string FIELDNAME_tReport__IsPrint = "TREPORT__ISPRINT";
        public const string FIELDNAME_tReport__KeyWord = "TREPORT__KEYWORD";
        public const string FIELDNAME_tReport__Mender = "TREPORT__MENDER";
        public const string FIELDNAME_tReport__MenderName = "TREPORT__MENDERNAME";
        public const string FIELDNAME_tReport__ModifyDt = "TREPORT__MODIFYDT";
        public const string FIELDNAME_tReport__Optional1 = "TREPORT__OPTIONAL1";
        public const string FIELDNAME_tReport__Optional2 = "TREPORT__OPTIONAL2";
        public const string FIELDNAME_tReport__Optional3 = "TREPORT__OPTIONAL3";
        public const string FIELDNAME_tReport__PrintCopies = "TREPORT__PRINTCOPIES";
        public const string FIELDNAME_tReport__PrintTemplateGuid = "TREPORT__PRINTTEMPLATEGUID";
        public const string FIELDNAME_tReport__ReadOnly = "TREPORT__READONLY";
        public const string FIELDNAME_tReport__RebuildMark = "TREPORT__REBUILDMARK";
        public const string FIELDNAME_tReport__ReconvertDt = "TREPORT__RECONVERTDT";
        public const string FIELDNAME_tReport__Recuperator = "TREPORT__RECUPERATOR";
        public const string FIELDNAME_tReport__RejectDomain = "TREPORT__REJECTDOMAIN";
        public const string FIELDNAME_tReport__RejectDt = "TREPORT__REJECTDT";
        public const string FIELDNAME_tReport__Rejecter = "TREPORT__REJECTER";
        public const string FIELDNAME_tReport__RejectSite = "TREPORT__REJECTSITE";
        public const string FIELDNAME_tReport__RejectToObject = "TREPORT__REJECTTOOBJECT";
        public const string FIELDNAME_tReport__ReportGuid = "TREPORT__REPORTGUID";
        public const string FIELDNAME_tReport__ReportName = "TREPORT__REPORTNAME";
        public const string FIELDNAME_tReport__ReportQuality = "TREPORT__REPORTQUALITY";
        public const string FIELDNAME_tReport__ReportQuality2 = "TREPORT__REPORTQUALITY2";
        public const string FIELDNAME_tReport__ReportQualityComments = "TREPORT__REPORTQUALITYCOMMENTS";
        public const string FIELDNAME_tReport__ReportText = "TREPORT__REPORTTEXT";
        public const string FIELDNAME_tReport__ReportTextApprovedSign = "TREPORT__REPORTTEXTAPPROVEDSIGN";
        public const string FIELDNAME_tReport__ReportTextSubmittedSign = "TREPORT__REPORTTEXTSUBMITTEDSIGN";
        public const string FIELDNAME_tReport__SecondApproveDomain = "TREPORT__SECONDAPPROVEDOMAIN";
        public const string FIELDNAME_tReport__SecondApproveDt = "TREPORT__SECONDAPPROVEDT";
        public const string FIELDNAME_tReport__SecondApprover = "TREPORT__SECONDAPPROVER";
        public const string FIELDNAME_tReport__SecondApproverName = "TREPORT__SECONDAPPROVERNAME";
        public const string FIELDNAME_tReport__SecondApproveSite = "TREPORT__SECONDAPPROVESITE";
        public const string FIELDNAME_tReport__SignCombinedForCertification = "TREPORT__SIGNCOMBINEDFORCERTIFICATION";
        public const string FIELDNAME_tReport__Status = "TREPORT__STATUS";
        public const string FIELDNAME_tReport__SubmitDomain = "TREPORT__SUBMITDOMAIN";
        public const string FIELDNAME_tReport__SubmitDt = "TREPORT__SUBMITDT";
        public const string FIELDNAME_tReport__SubmitSite = "TREPORT__SUBMITSITE";
        public const string FIELDNAME_tReport__Submitter = "TREPORT__SUBMITTER";
        public const string FIELDNAME_tReport__SubmitterName = "TREPORT__SUBMITTERNAME";
        public const string FIELDNAME_tReport__TakeFilmComment = "TREPORT__TAKEFILMCOMMENT";
        public const string FIELDNAME_tReport__TakeFilmDept = "TREPORT__TAKEFILMDEPT";
        public const string FIELDNAME_tReport__TakeFilmRegion = "TREPORT__TAKEFILMREGION";
        public const string FIELDNAME_tReport__TechInfo = "TREPORT__TECHINFO";
        public const string FIELDNAME_tReport__UpdateTime = "TREPORT__UPDATETIME";
        public const string FIELDNAME_tReport__Uploaded = "TREPORT__UPLOADED";
        public const string FIELDNAME_tReport__WYG = "TREPORT__WYG";
        public const string FIELDNAME_tReport__WYGText = "TREPORT__WYGTEXT";
        public const string FIELDNAME_tReport__WYS = "TREPORT__WYS";
        public const string FIELDNAME_tReport__WYSText = "TREPORT__WYSTEXT";

        #endregion

        #region public string function

        /// <summary>
        /// Compare String Without Case
        /// </summary>
        /// <param name="str1"></param>
        /// <param name="str2"></param>
        /// <returns>
        /// < 0     : str1 < str2
        /// == 0    : str1 == str2
        /// >0      : str1 > str2
        /// </returns>
        public static int CompareStringWithoutCase(string str1, string str2)
        {
            return str1.ToUpper().CompareTo(str2.ToUpper());
        }

       

        public static bool isNotEmptyStringArray(string[] arrayStr)
        {
            bool notEmpty = false;
            for (int i = 0; i < arrayStr.Length; i++)
            {
                string s = arrayStr.GetValue(i) as string;
                if (s.Trim().Length > 0)
                    notEmpty = true;
            }

            return notEmpty;
        }

        public static string StringRight(string src, int count)
        {
            return src.Length >= count ? src.Substring(src.Length - count) : src;
        }

        public static string StringLeft(string src, int count)
        {
            if (src == null)
                return "";

            return src.Length >= count ? src.Substring(0, count) : src;
        }

        /// <summary>
        /// by bytes count, not splitting wide-character.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string StringLeft_ANSI(string src, int count)
        {
            if (src == null || count < 1)
                return "";

            byte[] buff = System.Text.Encoding.Default.GetBytes(src);

            if (count >= buff.Length)
                return src;

            int len = 0;
            bool bPrevWide = false;

            for (int i = 0; i < buff.Length; i++)
            {
                if (i >= count)
                {
                    len = i - (bPrevWide ? 1 : 0);

                    break;
                }

                byte b = (byte)buff.GetValue(i);

                if (bPrevWide)
                {
                    bPrevWide = false;
                }
                else
                {
                    if (b > 128)
                        bPrevWide = true;
                }
            }

            return System.Text.Encoding.Default.GetString(buff, 0, len);
        }

        public static string escape(string src)
        {
            src = src.Replace("%", "%25");

            src = src.Replace("&", "%26");
            src = src.Replace("#", "%23");
            src = src.Replace(",", "%2C");
            src = src.Replace(";", "%3B");
            src = src.Replace("|", "%7C");
            src = src.Replace("=", "%3D");

            return src;
        }

        public static string unescape(string src)
        {
            src = src.Replace("%26", "&");
            src = src.Replace("%23", "#");
            src = src.Replace("%2C", ",");
            src = src.Replace("%3B", ";");
            src = src.Replace("%7C", "|");
            src = src.Replace("%3D", "=");

            src = src.Replace("%25", "%");

            return src;
        }

        public static string ReplaceNoCase(string src, string oldValue, string newValue)
        {
            if (src == null || oldValue == null || newValue == null || oldValue.Length < 1 || oldValue == newValue)
                return src;

            string tmp = src.ToUpper();
            int iStart = 0;
            List<int> arrInt = new List<int>();

            oldValue = oldValue.ToUpper();

            while ((iStart = tmp.IndexOf(oldValue, iStart)) >= 0)
            {
                arrInt.Add(iStart);

                iStart += oldValue.Length;
            }

            for (int i = arrInt.Count - 1; i >= 0; i--)
            {
                int n = arrInt[i];

                src = src.Substring(0, n) + newValue + src.Substring(n + oldValue.Length);
            }

            return src;
        }

        public static bool isInteger(string src)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("^-?[0-9]+$");

            return reg.IsMatch(src);
        }

        public static bool isNumeric(string src)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^-?\d(\.\d)?$");

            return reg.IsMatch(src);
        }

        public static bool ContainChineseCharater(string src)
        {
            if (src == null)
                return false;

            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] >= 0x4e00 && src[i] <= 0x9fa5)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool isEnglishCharater(string src)
        {
            if (src == null)
                return false;

            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] > 128)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// compare strings, like "a,b,c" and "c,d,e" , whether contain the same substring on split by "separator"
        /// ignoring case and blank.
        /// </summary>
        /// <param name="src1"></param>
        /// <param name="src2"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static bool ContainSameSubString(string src1, string src2, char separator)
        {
            if (src1 == null || src2 == null || separator == null)
                return false;

            string[] a1 = src1.Split(separator);
            string[] a2 = src2.Split(separator);

            for (int i = 0; i < a1.Length; i++)
            {
                for (int m = 0; m < a2.Length; m++)
                {
                    string s1 = a1.GetValue(i) as string;
                    string s2 = a2.GetValue(m) as string;

                    if (s1 == null) s1 = "";
                    if (s2 == null) s2 = "";

                    s1 = s1.Trim().ToUpper();
                    s2 = s2.Trim().ToUpper();

                    if ((s1.Length + s2.Length) > 0 && s1.CompareTo(s2) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool isDateTimeString(string src)
        {
            try
            {
                DateTime dt;
                if (DateTime.TryParse(src, out dt))
                {
                    return true;
                }

            }
            catch (FormatException x)
            {
                Console.WriteLine("输入的日期格式错误");
            }

            //System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex (@"^\d*$");
            //if (reg.Match(src))
            //{
            //    return true;
            //}

            return false;
        }

        public static string GetFileNameFromPath(string path)
        {
            string f1 = path.Replace('/', '\\');
            int n = f1.LastIndexOf('\\');
            if (n >= 0 && n + 1 < f1.Length)
            {
                f1 = f1.Substring(n + 1);
                return f1;
            }
            else if (n < 0)
            {
                return path;
            }

            return "";
        }

        public static bool ContainEnglishCharacter(string src)
        {
            for (int i = 0; i < src.Length; i++)
            {
                if ((src[i] >= 'a' && src[i] <= 'z') ||
                    (src[i] >= 'A' && src[i] <= 'Z'))
                {
                    return true;
                }
            }

            return false;
        }

        public static DateTime GetDateTime(object obj)
        {
            try
            {
                return System.Convert.ToDateTime(obj);
            }
            catch (Exception)
            {
            }

            return DateTime.Now;
        }

        public static bool IsValidInput(string src)
        {
            if (src.Contains("'") ||
                src.Contains("\"") ||
                src.Contains("|") ||
                src.Contains(",") ||
                src.Contains(";"))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidInput2(string src)
        {
            if (src.Contains("'"))
            {
                return false;
            }

            return true;
        }

        #endregion

        /// <summary>
        /// MakeParameter
        /// </summary>
        /// <param name="paramMap"></param>
        /// <returns></returns>
        public static string MakeParameter(Dictionary<string, string> paramMap)
        {
            string rt = "";

            foreach (string key in paramMap.Keys)
            {
                rt += key + SEPARATOR_1 + escape(paramMap[key]) + SEPARATOR_0;
            }

            return rt;
        }

        /// <summary>
        /// GetParameters
        /// </summary>
        /// <param name="strParam"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetParameters(string strParam)
        {
            string[] p0 = strParam.Split(SEPARATOR_0);

            Dictionary<string, object> rt = new Dictionary<string, object>();

            for (int i = 0; i < p0.Length; i++)
            {
                string tt0 = p0.GetValue(i) as string;

                if (tt0 == null)
                    continue;

                string[] p1 = tt0.Split(SEPARATOR_1);
                if (p1.Length == 2)
                {
                    string key = p1[0].Trim();
                    string value = p1[1].Trim();
                    value = unescape(value);

                    if (key.Length > 0)
                    {
                        rt.Add(key, value);
                    }
                }
            }

            return rt;
        }

        /// <summary>
        /// MakeConditionItemString
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="co"></param>
        /// <param name="tablename"></param>
        /// <param name="columnname"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <returns></returns>
        public static string MakeConditionItemString(SQLServer_FieldType fieldType,
            ConditionColumn_ComparisonOperator co,
            string tablename, string columnname, string value1, string value2)
        {
            string rt = "";

            List<string> outlist = new List<string>();
            MakeConditionItemString(fieldType, co, tablename, columnname, value1, value2, outlist);

            foreach (string str in outlist)
            {
                rt += " and " + str;
            }

            return rt;
        }

        /// <summary>
        /// MakeConditionItemString
        /// </summary>
        /// <param name="fieldType"></param>
        /// <param name="co"></param>
        /// <param name="tablename"></param>
        /// <param name="columnname"></param>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="outList"></param>
        public static void MakeConditionItemString(SQLServer_FieldType fieldType,
            ConditionColumn_ComparisonOperator co,
            string tablename, string columnname, string value1, string value2, List<string> outList)
        {
            if (tablename == null || columnname == null || value1 == null)
            {
                string msg = "Missing Parameter in MakeConditionItemString!";
                System.Diagnostics.Debug.Assert(false, msg);
                
                return;
            }

            string[] ts = tablename.Split('|');
            string[] cs = columnname.Split('|');
            string[] v1s = value1.Split('|');
            string[] v2s = value2 == null ? null : value2.Split('|');

            int minBound = ts.Length;

            minBound = cs.Length < minBound ? cs.Length : minBound;
            minBound = v1s.Length < minBound ? v1s.Length : minBound;
            minBound = (v2s != null && v2s.Length < minBound) ? v2s.Length : minBound;

            for (int i = 0; i < minBound; i++)
            {
                v1s[i] = v1s[i].Replace("'", "''");

                switch (fieldType)
                {
                    case SQLServer_FieldType.DECIMAL:
                    case SQLServer_FieldType.FLOAT:
                    case SQLServer_FieldType.INTEGER:
                    case SQLServer_FieldType.MONEY:
                        #region Numeric
                        {
                            switch (co)
                            {
                                case ConditionColumn_ComparisonOperator.equal:
                                    outList.Add(ts[i] + "." + cs[i] + " = " + v1s[i]);
                                    break;
                                case ConditionColumn_ComparisonOperator.greaterthan:
                                    outList.Add(ts[i] + "." + cs[i] + " > " + v1s[i]);
                                    break;
                                case ConditionColumn_ComparisonOperator.IN:
                                    {
                                        v1s[i] = v1s[i].Replace(";", ",");
                                        v1s[i] = v1s[i].Trim(", ".ToCharArray());

                                        outList.Add(ts[i] + "." + cs[i] + " in (" + v1s[i] + ")");
                                    }
                                    break;
                                case ConditionColumn_ComparisonOperator.lessthan:
                                    outList.Add(ts[i] + "." + cs[i] + " < " + v1s[i]);
                                    break;
                                case ConditionColumn_ComparisonOperator.notequal:
                                    outList.Add(ts[i] + "." + cs[i] + " <> " + v1s[i]);
                                    break;
                                case ConditionColumn_ComparisonOperator.notgreaterthan:
                                    outList.Add(ts[i] + "." + cs[i] + " <= " + v1s[i]);
                                    break;
                                case ConditionColumn_ComparisonOperator.notIN:
                                    {
                                        v1s[i] = v1s[i].Replace(";", ",");
                                        v1s[i] = v1s[i].Trim(", ".ToCharArray());

                                        outList.Add(ts[i] + "." + cs[i] + " not in (" + v1s[i] + ")");
                                    }
                                    break;
                                case ConditionColumn_ComparisonOperator.notlessthan:
                                    outList.Add(ts[i] + "." + cs[i] + " >= " + v1s[i]);
                                    break;
                                case ConditionColumn_ComparisonOperator.between:
                                    {
                                        if (v2s != null && i < v2s.Length)
                                            outList.Add(ts[i] + "." + cs[i] + " between " + v1s[i] + " and " + v2s[i]);
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        break;
                    default:
                        #region Not Numeric
                        {
                            switch (co)
                            {
                                case ConditionColumn_ComparisonOperator.equal:
                                    outList.Add(ts[i] + "." + cs[i] + " = '" + v1s[i] + "'");
                                    break;
                                case ConditionColumn_ComparisonOperator.greaterthan:
                                    outList.Add(ts[i] + "." + cs[i] + " > '" + v1s[i] + "'");
                                    break;
                                case ConditionColumn_ComparisonOperator.IN:
                                    {
                                        if (!v1s[i].Contains("'"))
                                        {
                                            v1s[i] = v1s[i].Replace(",", "','");
                                            v1s[i] = v1s[i].Replace(";", "','");

                                            v1s[i] = "'" + v1s[i] + "'";
                                        }

                                        v1s[i] = v1s[i].Trim(", ".ToCharArray());

                                        outList.Add(ts[i] + "." + cs[i] + " in (" + v1s[i] + ")");
                                    }
                                    break;
                                case ConditionColumn_ComparisonOperator.lessthan:
                                    outList.Add(ts[i] + "." + cs[i] + " < '" + v1s[i] + "'");
                                    break;
                                case ConditionColumn_ComparisonOperator.notequal:
                                    outList.Add(ts[i] + "." + cs[i] + " <> '" + v1s[i] + "'");
                                    break;
                                case ConditionColumn_ComparisonOperator.notgreaterthan:
                                    outList.Add(ts[i] + "." + cs[i] + " <= '" + v1s[i] + "'");
                                    break;
                                case ConditionColumn_ComparisonOperator.notIN:
                                    {
                                        if (!v1s[i].Contains("'"))
                                        {
                                            v1s[i] = v1s[i].Replace(",", "','");
                                            v1s[i] = v1s[i].Replace(";", "','");

                                            v1s[i] = "'" + v1s[i] + "'";
                                        }

                                        v1s[i] = v1s[i].Trim(", ".ToCharArray());

                                        outList.Add(ts[i] + "." + cs[i] + " not in (" + v1s[i] + ")");
                                    }
                                    break;
                                case ConditionColumn_ComparisonOperator.notlessthan:
                                    outList.Add(ts[i] + "." + cs[i] + " >= '" + v1s[i] + "'");
                                    break;
                                case ConditionColumn_ComparisonOperator.between:
                                    {
                                        if (v2s != null && i < v2s.Length)
                                        {
                                            if (fieldType == SQLServer_FieldType.DATETIME)
                                            {
                                                string minValue = v1s[i].CompareTo(v2s[i]) >= 0 ? v2s[i] : v1s[i];
                                                string maxValue = v1s[i].CompareTo(v2s[i]) >= 0 ? v1s[i] : v2s[i];
                                                if (!maxValue.Contains(":"))
                                                {
                                                    maxValue += " 23:59:59";
                                                }

                                                outList.Add(ts[i] + "." + cs[i] + " between '" + minValue + "' and '" + maxValue + "'");
                                            }
                                            else
                                            {
                                                outList.Add(ts[i] + "." + cs[i] + " between '" + v1s[i] + "' and '" + v2s[i] + "'");
                                            }
                                        }
                                    }
                                    break;
                                case ConditionColumn_ComparisonOperator.like:
                                    outList.Add(ts[i] + "." + cs[i] + " like '%" + v1s[i] + "%'");
                                    break;
                                case ConditionColumn_ComparisonOperator.prefix_like:
                                    outList.Add(ts[i] + "." + cs[i] + " like '%" + v1s[i] + "'");
                                    break;
                                case ConditionColumn_ComparisonOperator.subfix_like:
                                    outList.Add(ts[i] + "." + cs[i] + " like '" + v1s[i] + "%'");
                                    break;
                                case ConditionColumn_ComparisonOperator.advance_like:
                                    {
                                        char[] op = " +-".ToCharArray();
                                        string value = v1s[i].TrimEnd(op).TrimStart(" +".ToCharArray());
                                        string outValue = "";
                                        List<string> adList = new List<string>();

                                        RecursiveAdvanceLike(value, op, ts[i], cs[i], adList);

                                        foreach (string tmp in adList)
                                        {
                                            outValue += tmp;
                                        }

                                        if ((outValue = outValue.Trim()).Length > 0)
                                        {
                                            if (outValue.StartsWith("and "))
                                            {
                                                outValue = outValue.Substring(4);
                                            }
                                            else if (outValue.StartsWith("or "))
                                            {
                                                outValue = outValue.Substring(3);
                                            }

                                            outValue = "(" + outValue + ")";

                                            outList.Add(outValue);
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        #endregion
                        break;
                }
            }
        }

        public static SQLServer_FieldType GetSQLServer_FieldType(int fieldType)
        {
            switch (fieldType)
            {
                case 34:
                    return SQLServer_FieldType.BINARY;
                case 35:
                    return SQLServer_FieldType.TEXT;
                case 56:
                    return SQLServer_FieldType.INTEGER;
                case 60:
                    return SQLServer_FieldType.MONEY;
                case 61:
                    return SQLServer_FieldType.DATETIME;
                case 62:
                    return SQLServer_FieldType.FLOAT;
                case 106:
                    return SQLServer_FieldType.DECIMAL;
                case 167:
                    return SQLServer_FieldType.VARCHAR;
                default:
                    return SQLServer_FieldType.VARCHAR;
            }

            return SQLServer_FieldType.VARCHAR;
        }

        public static ConditionColumn_ControlType GetConditionColumn_ControlType(int ctrlType)
        {
            if (2 == ctrlType)
                return ConditionColumn_ControlType.DateTime;
            else if (1 == ctrlType)
                return ConditionColumn_ControlType.CheckCombo;

            return ConditionColumn_ControlType.TextBox;
        }

        public static ConditionColumn_ComparisonOperator GetConditionColumn_ComparisonOperator(int co)
        {
            switch (co)
            {
                case 1:
                    return ConditionColumn_ComparisonOperator.greaterthan;
                case 2:
                    return ConditionColumn_ComparisonOperator.notlessthan;
                case 3:
                    return ConditionColumn_ComparisonOperator.lessthan;
                case 4:
                    return ConditionColumn_ComparisonOperator.notgreaterthan;
                case 5:
                    return ConditionColumn_ComparisonOperator.notequal;
                case 6:
                    return ConditionColumn_ComparisonOperator.IN;
                case 7:
                    return ConditionColumn_ComparisonOperator.notIN;
                case 8:
                    return ConditionColumn_ComparisonOperator.like;
                case 9:
                    return ConditionColumn_ComparisonOperator.prefix_like;
                case 10:
                    return ConditionColumn_ComparisonOperator.subfix_like;
                case 11:
                    return ConditionColumn_ComparisonOperator.between;
                case 12:
                    return ConditionColumn_ComparisonOperator.advance_like;
                default:
                    return ConditionColumn_ComparisonOperator.equal;
            }

            return ConditionColumn_ComparisonOperator.equal;
        }

        /// <summary>
        /// GetMoudleIDString
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public static string GetMoudleIDString(ModuleID mid)
        {
            return StringRight("0000" + System.Convert.ToInt32(mid).ToString("X"), 4);
        }

        public static string GetPanelIDString(PanelID pid)
        {
            return StringRight("0000" + System.Convert.ToInt32(pid).ToString("X"), 4);
        }

        public static string GetReportDAO_ImplementClass_PrefixName(Type typ)
        {
            string fulName = typ.ToString();
            int i1 = fulName.IndexOf('_');

            i1 = i1 < 0 ? fulName.Length : i1;

            string tmp1 = "";
            if (i1 > 0)
            {
                tmp1 = fulName.Substring(0, i1);
            }
            else
            {
                throw (new Exception("Wrong Type Name"));
            }

            return tmp1;
        }

        public static string GetReportDAO_ImplementClass_PrefixName(object obj)
        {
            string fulName = obj.ToString();
            int i1 = fulName.IndexOf('_');

            i1 = i1 < 0 ? fulName.Length : i1;

            string tmp1 = "";
            if (i1 > 0)
            {
                tmp1 = fulName.Substring(0, i1);
            }
            else
            {
                throw (new Exception("Wrong CLass Name"));
            }

            return tmp1;

            //string fulName = obj.ToString();
            //int i0 = fulName.IndexOf('.');
            //int i1 = fulName.IndexOf('_');

            //i0 = i0 < 0 ? 0 : i0;
            //i1 = i1 < 0 ? fulName.Length : i1;

            //string tmp1 = "";
            //if (i1 > i0)
            //{
            //    tmp1 = fulName.Substring(i0, i1 - i0);
            //}
            //else
            //{
            //    throw (new Exception("Wrong CLass Name"));
            //}

            //return tmp1;
        }

        public static RP_Status GetRPStatus(int iStatus)
        {
            switch (iStatus)
            {
                case (int)RP_Status.CheckIn: return RP_Status.CheckIn;
                case (int)RP_Status.Draft: return RP_Status.Draft;
                case (int)RP_Status.Examination: return RP_Status.Examination;
                case (int)RP_Status.Examing: return RP_Status.Examing;
                case (int)RP_Status.FirstApprove: return RP_Status.FirstApprove;
                case (int)RP_Status.NoCheck: return RP_Status.NoCheck;
                case (int)RP_Status.Reject: return RP_Status.Reject;
                case (int)RP_Status.Repeatshot: return RP_Status.Repeatshot;
                case (int)RP_Status.RPCancel: return RP_Status.RPCancel;
                case (int)RP_Status.SecondApprove: return RP_Status.SecondApprove;
                case (int)RP_Status.Submit: return RP_Status.Submit;
            }

            return RP_Status.Unknown;
        }

        #region private method

        private static void RecursiveAdvanceLike(string src, char[] op, string tblName, string fldName, List<string> adList)
        {
            if (src == null || op == null || adList == null || tblName == null || fldName == null ||
                src.Length < 1 || op.Length < 1 || tblName.Length < 1 || fldName.Length < 1)
            {
                return;
            }

            int idx = src.IndexOfAny(op);
            if (idx > 0)
            {
                string s1 = src.Substring(0, idx);
                string s2 = src.Substring(idx);

                if (s1 != null && (s1 = s1.Trim(op)).Length > 0)
                    adList.Add(" and " + tblName + "." + fldName + " like '%" + s1 + "%'");

                RecursiveAdvanceLike(s2, op, tblName, fldName, adList);
            }
            else if (0 == idx)
            {
                char c = src[0];
                int idx2 = src.IndexOfAny(op, 1);

                bool bRecursive = idx2 > 0;

                if (idx2 < 1)
                    idx2 = src.Length;

                string s1 = src.Substring(1, idx2 - 1);

                if (s1.Length > 0)
                {
                    if ('+' == c)
                    {
                        adList.Add(" and " + tblName + "." + fldName + " like '%" + s1 + "%'");
                    }
                    else if ('-' == c)
                    {
                        adList.Add(" and " + tblName + "." + fldName + " not like '%" + s1 + "%'");
                    }
                    else if (' ' == c)
                    {
                        adList.Add(" or " + tblName + "." + fldName + " like '%" + s1 + "%'");
                    }
                }

                if (bRecursive)
                    RecursiveAdvanceLike(src.Substring(idx2), op, tblName, fldName, adList);
            }
            else
            {
                adList.Add(" and " + tblName + "." + fldName + " like '%" + src + "%'");
            }
        }

        #endregion
    }

    public class ProfileName
    {
        #region Global

        public static string FTPPassword { get { return "FTPPassword"; } }

        public static string FTPPort { get { return "FTPPort"; } }

        public static string FTPPort4Web { get { return "FTPPort4Web"; } }

        public static string FTPRootFolder { get { return "FTPRootFolder"; } }

        public static string FTPServer { get { return "FTPServer"; } }

        public static string FTPServer4Web { get { return "FTPServer4Web"; } }

        public static string FTPUserID { get { return "FTPUserID"; } }

        public static string Logo { get { return "logo"; } }
        public static string IsolationLevel { get { return "IsolationLevel"; } }

        public static string NecessaryItemBackColor { get { return "NecessaryItemBackColor"; } }
        public static string AssignedOutColor { get { return "AssignedOutColor"; } }

        public static string StructureReportOn { get { return "StructureReportON"; } }

        #endregion

        #region Report Global

        public static string _REPORTLOGLEVEL_FORCLIENT { get { return "ReportLogLevel_forClient"; } }
        public static string _REPORTLOGLEVEL_FORSERVER { get { return "ReportLogLevel_forServer"; } }
        public static string Report_PageSize { get { return "Report_PageSize"; } }
        public static string Report_UnapprovedWaringColor { get { return "Report_UnapprovedWarningColor"; } }
        public static string Report_NeedPasswordSave { get { return "Report_NeedPasswordSave"; } }
        public static string Report_SplitterColor { get { return "SplitterColor"; } }
        public static string Report_NeedSaveImageOnPrinting { get { return "Report_SaveSnapshotOnPrint"; } }
        public static string Report_NeedSaveFileOnExport { get { return "Report_SaveSnapshotOnExport"; } }
        public static string ReportFtpSubFolder { get { return "ReportFtpSubFolder"; } }
        public static string ReportSnapshotFtpSubFolder { get { return "ReportSnapshotFtpSubFolder"; } }
        public static string Report_NeedImageOnCreating { get { return "Report_NeedImageOnCreating"; } }
        public static string Report_Integration_Mode { get { return "PACSIntegrationType"; } }
        public static string Report_Integration_User { get { return "PACSUser"; } }
        public static string Report_Integration_Pwd { get { return "PACSPassword"; } }
        public static string Report_Integration_Server { get { return "PACSServer"; } }
        public static string Report_Integration_WebServer { get { return "PACSWebServer"; } }
        public static string Report_Integration_WebConfigLocation { get { return "WebConfigLocation"; } }
        public static string Report_CloseAfterOpenReportFromUserReports { get { return "Report_CloseAfterOpenReportFromUserReports"; } }
        public static string Report_PreviousPID4LoadImage { get { return "Report_PreviousPID4LoadImage"; } }
        public static string Report_PopupMessageCreateReportNotImage { get { return "Report_PopupMessageCreateReportNotImage"; } }
        public static string Report_WarningStartingTime { get { return "ReportList_WarningStartingTime"; } }
        public static string Report_Integration_PACSImageFolder { get { return "PACSImageFolder"; } }
        public static string Report_Integration_ImageCompressRate { get { return "ImageCompressRate"; } }
        public static string Report_Integration_LoadDiffernetOrderImageSleepTime { get { return "LoadDiffernetOrderImageSleepTime"; } }
        public static string Report_MaxReportPrintCopies { get { return "MaxReportPrintCopies"; } }
        public static string Report_MaxReportPrintCopiesRestrict { get { return "MaxReportPrintCopiesRestrict"; } }
        public static string Report_MaxNumberOfHoldUnwrittenReports { get { return "Report_MaxNumberOfHoldUnwrittenReports"; } }
        public static string Report_MaxNumberOfHoldUnapprovedReports { get { return "Report_MaxNumberOfHoldUnapprovedReports"; } }
        public static string Report_NumberOfHoldUnwrittenReports { get { return "Report_NumberOfHoldUnwrittenReports"; } }
        public static string Report_NumberOfHoldUnapprovedReports { get { return "Report_NumberOfHoldUnapprovedReports"; } }
        public static string Report_MaxNumberOfHoldUnwrittenUnapprovedReports { get { return "Report_MaxNumberOfHoldUnwrittenUnapprovedReports"; } }

        public const string Report_EnableCertification = "Report_EnableCertification";

        public const string Report_HistoryReports_RelatedPatientBackColor = "Report_HistoryReports_RelatedPatientBackColor";
        public const string Report_HistoryReports_CenterDataBackColor = "Report_HistoryReports_CenterDataBackColor";
        public const string Report_CanAppendRelatedImage = "CanAppendRelatedImage";
        public const string Report_ReadModeForLoadingImage = "Report_ReadModeForLoadingImage";

        #endregion

        #region ConditionBuilder

        public static string ConditionBuilder_NeedMakePatientID { get { return "ConditionBuilder_NeedMakePatientID"; } }

        public static string ConditionBuilder_NeedMakeAccNO { get { return "ConditionBuilder_NeedMakeAccNO"; } }

        #endregion

        #region ReportList

        public static string Report_SortAllPage { get { return "SortAllPageList"; } }

        public static string ReportList_AssociatedReport { get { return "ReportList_RelatedReport"; } }

        public static string ReportList_AutoLoadImage { get { return "ReportList_AutoLoadImage"; } }
        

        public static string ReportList_AutoOpenRequisition { get { return "ReportList_AutoOpenRequisition"; } }
        public static string ReportList_AutoOpenMemo { get { return "ReportList_AutoOpenMemo"; } }

        public static string Report_Sorting_ReportList { get { return "Report_Sorting_ReportList"; } }

        public static string Report_Sorting_UnwrittenReport { get { return "Report_Sorting_UnwrittenReport"; } }

        public static string Report_Sorting_UnapprovedReport { get { return "Report_Sorting_UnapprovedReport"; } }

        public static string Report_Sorting_ApprovedReport { get { return "Report_Sorting_ApprovedReport"; } }
        public static string Report_Sorting_ReportPrint { get { return "Report_Sorting_ReportPrint"; } }

        public static string ReportList_HideCount { get { return "ReportList_HideCount"; } }

        public static string ReportList_RefreshListCountInterval { get { return "ReportList_RefreshListCountInterval"; } }

        public static string Report_UnwrittenReportPanel_ReportStatus { get { return "Report_UnwrittenReportPanel_ReportStatus"; } }

        public static string Report_UnapprovedReportPanel_ReportStatus { get { return "Report_UnapprovedReportPanel_ReportStatus"; } }

        public static string ReportList_ApprovedReportPanel_ConditionBuilderHeight { get { return "ReportList_ApprovedReportPanel_ConditionBuilderHeight"; } }

        public static string ReportList_UnapprovedReportPanel_ConditionBuilderHeight { get { return "ReportList_UnapprovedReportPanel_ConditionBuilderHeight"; } }

        public static string ReportList_UnwrittenReportPanel_ConditionBuilderHeight { get { return "ReportList_UnwrittenReportPanel_ConditionBuilderHeight"; } }

        public static string ReportList_ReportListPanel_ConditionBuilderHeight { get { return "ReportList_ReportListPanel_ConditionBuilderHeight"; } }

        public static string ReportList_ReportPrintPanel_ConditionBuilderHeight { get { return "ReportList_ReportPrintPanel_ConditionBuilderHeight"; } }

        public static string DraftReport_ShowDialog4SeniorDoctor { get { return "DraftReport_ShowDialog4SeniorDoctor"; } }
        public static string DraftReport_Replace4SeniorDoctor { get { return "DraftReport_Replace4SeniorDoctor"; } }
        public static string DraftReport_ShowDialog4OrdinateDoctor { get { return "DraftReport_ShowDialog4OrdinateDoctor"; } }
        public static string DraftReport_Replace4OrdinateDoctor { get { return "DraftReport_Replace4OrdinateDoctor"; } }
        public static string DraftReport_ShowDialog4SubOrdinateDoctor { get { return "DraftReport_ShowDialog4SubOrdinateDoctor"; } }
        public static string DraftReport_Replace4SubOrdinateDoctor { get { return "DraftReport_Replace4SubOrdinateDoctor"; } }

        public static string SubmitReport_ShowDialog4SeniorDoctor { get { return "SubmitReport_ShowDialog4SeniorDoctor"; } }
        public static string SubmitReport_Replace4SeniorDoctor { get { return "SubmitReport_Replace4SeniorDoctor"; } }
        public static string SubmitReport_ShowDialog4OrdinateDoctor { get { return "SubmitReport_ShowDialog4OrdinateDoctor"; } }
        public static string SubmitReport_Replace4OrdinateDoctor { get { return "SubmitReport_Replace4OrdinateDoctor"; } }
        public static string SubmitReport_ShowDialog4SubOrdinateDoctor { get { return "SubmitReport_ShowDialog4SubOrdinateDoctor"; } }
        public static string SubmitReport_Replace4SubOrdinateDoctor { get { return "SubmitReport_Replace4SubOrdinateDoctor"; } }

        public static string RejectReport_ShowDialog4SeniorDoctor { get { return "RejectReport_ShowDialog4SeniorDoctor"; } }
        public static string RejectReport_Replace4SeniorDoctor { get { return "RejectReport_Replace4SeniorDoctor"; } }
        public static string RejectReport_ShowDialog4OrdinateDoctor { get { return "RejectReport_ShowDialog4OrdinateDoctor"; } }
        public static string RejectReport_Replace4OrdinateDoctor { get { return "RejectReport_Replace4OrdinateDoctor"; } }
        public static string RejectReport_ShowDialog4SubOrdinateDoctor { get { return "RejectReport_ShowDialog4SubOrdinateDoctor"; } }
        public static string RejectReport_Replace4SubOrdinateDoctor { get { return "RejectReport_Replace4SubOrdinateDoctor"; } }

        public static string RebuildReport_Replace4ApproverAndDt { get { return "RebuildReport_Replace4ApproverAndDt"; } }

        public static string ReportList_reportlist_conditionbuilderMode { get { return "ReportList_reportlist_conditionbuilderMode"; } }
        public static string ReportList_unwrittenreport_conditionbuilderMode { get { return "ReportList_unwrittenreport_conditionbuilderMode"; } }
        public static string ReportList_unapprovereport_conditionbuilderMode { get { return "ReportList_unapprovereport_conditionbuilderMode"; } }
        public static string ReportList_approvedreport_conditionbuilderMode { get { return "ReportList_approvedreport_conditionbuilderMode"; } }
        public static string ReportList_reportprint_conditionbuilderMode { get { return "ReportList_reportprint_conditionbuilderMode"; } }

        public static string ReportList_HideHistoryReports { get { return "ReportList_HideHistoryReports"; } }
        public static string ReportList_HistoryReports_StartReportStatus { get { return "ReportList_HistoryReports_StartReportStatus"; } }
        public static string ReportList_HistoryReports_HideSelectedReport { get { return "ReportList_HistoryReports_HideSelectedReport"; } }
        public const string ReportList_ReportListInitialized = "ReportListInitialized";
        public const string ReportList_RemindWhenAutoRefresh = "RemindWhenAutoRefresh";
        public const string AssignedReportList_WarningColor = "AssignedReportList_WarningColor";
        public const string AssignedReportList_WarningTime = "AssignedReportList_WarningTime";

        public static string AssociatedReport { get { return "RelatedReport"; } }
        public static string AutoLoadImage { get { return "AutoLoadImage"; } }
        public static string AutoOpenRequisition { get { return "AutoOpenRequisition"; } }
        public static string AutoOpenMemo { get { return "AutoOpenMemo"; } }
        #endregion

        #region ReportEditor

        public static string profile_canLoadReportTemplate { get { return "canLoadReportTemplate"; } }
        public static string profile_canSave { get { return "canSave"; } }
        public static string profile_canSubmit { get { return "canSubmit"; } }
        public static string profile_canApprove { get { return "canApprove"; } }
        public static string profile_canReject { get { return "canReject"; } }
        public static string profile_canDelete { get { return "canDelete"; } }
        public static string profile_canPrint { get { return "canPrint"; } }
        public static string profile_canRebuild { get { return "canRebuild"; } }
        public static string profile_canHistory { get { return "canHistory"; } }
        public static string profile_canShowReg { get { return "canShowReg"; } }
        public static string profile_canShowApp { get { return "canShowApp"; } }
        public static string profile_canTeaching { get { return "canTeaching"; } }
        public static string profile_canSaveasTemplate { get { return "canSaveasTemplate"; } }
        public static string profile_canDisqualifyImage { get { return "canDisqualifyImage"; } }
        public static string profile_canExport { get { return "canExport"; } }
        public static string profile_isRichReport { get { return "isRichReport"; } }
        public static string profile_canView3rdReport { get { return "canView3rdReport"; } }
        public static string profile_RoleLevel { get { return "RoleLevel"; } }
        public const string profile_canFinish = "CanFinishReferral";
        public const string profile_canReferral = "CanReferral";
        public const string profile_TeleConsultationMode = "TeleConsultationMode";
        public static string profile_canSecondaryApprove { get { return "canSecondaryApprove"; } }
        public static string ReportEditor_SinglenessFont { get { return "ReportEditor_SinglenessFont"; } }
        public static string ReportEditor_ShowFontToolbar { get { return "ReportEditor_ShowFontToolbar"; } }
        public static string ReportEditor_BasicInfo_FontSize { get { return "ReportEditor_BasicInfo_FontSize"; } }
        
        

        public static string ReportEditor_PreviewBeforePrinted { get { return "ReportEditor_PreviewBeforePrinted"; } }
        public static string ReportEditor_PrintAfterApprove { get { return "ReportEditor_PrintAfterApprove"; } }
        public static string ReportEditor_PrintAfterSubmit { get { return "ReportEditor_PrintAfterSubmit"; } }
        #region Added by Blue for RC595 - US16558, 05/08/2014
        public static string ReportEditor_ReturnVisit { get { return "ReportEditor_ReturnVisit"; } }
        #endregion
        public static string ReportEditor_AppendTemplate { get { return "ReportEditor_AppendTemplate"; } }
        public static string ReportEditor_AppendImage { get { return "ReportEditor_AppendImage"; } }
        public static string ReportEditor_AllowModifyApprovedReport_Minutes { get { return "ReportEditor_AllowModifyApprovedReport_Minutes"; } }
        public static string ReportEditor_DefaultShowTemplate { get { return "ReportEditor_DefaultShowTemplate"; } }
        public static string ReportEditor_NeedEvaluationBeforeApprove { get { return "ReportEditor_NeedEvaluationBeforeApprove"; } }
        public static string ReportEditor_AutoLoadTemplate { get { return "ReportEditor_AutoLoadTemplate"; } }
        public static string ReportEditor_DefaultMatchingTemplateName { get { return "ReportEditor_DefaultMatchingTemplateName"; } }
        public static string ReportEditor_DefaultShowInfoList { get { return "ReportEditor_DefaultShowInfoList"; } }
        public static string ReportEditor_PictureListHeight { get { return "ReportEditor_PictureListHeight"; } }
        public static string ReportEditor_Rich1Height { get { return "ReportEditor_Rich1Height"; } }
        public static string ReportEditor_Rich2Height { get { return "ReportEditor_Rich2Height"; } }
        public static string ReportEditor_Rich3Height { get { return "ReportEditor_Rich3Height"; } }
        public static string ReportEditor_InfoListWidth { get { return "ReportEditor_InfoListWidth"; } }
        public static string ReportEditor_ImageDefaultSize { get { return "ReportEditor_ImageDefaultSize"; } }
        public static string ReportEditor_Font_Size { get { return "ReportEditor_Font_Size"; } }
        public static string ReportEditor_Font_FamilyName { get { return "ReportEditor_Font_FamilyName"; } }
        public static string ReportEditor_ExportPath { get { return "ReportEditor_ExportPath"; } }

        public static string ReportEditor_NeedConfirmOnSubmit { get { return "ReportEditor_NeedConfirmOnSubmit"; } }
        public static string ReportEditor_NeedConfirmOnApprove { get { return "ReportEditor_NeedConfirmOnApprove"; } }
        public static string ReportEditor_NotRecordSyle { get { return "ReportEditor_NotRecordSyle"; } }
        public static string ReportEditor_RichText_MaxSize { get { return "ReportEditor_RichText_MaxSize"; } }
        public static string ReportEditor_HISCOM_Uniform { get { return "Report_Uniform"; } }
        public static string ReportEditor_HISCOM_Username { get { return "Report_User"; } }
        public static string ReportEditor_HISCOM_Password { get { return "Report_Password"; } }
        public static string ReportEditor_Numberofcopies { get { return "ReportEditor_Numberofcopies"; } }
        public static string ReportEditor_AllowDeleteMyReport { get { return "ReportEditor_AllowDeleteMyReport"; } }
        public static string ReportEditor_AllowMendforSubordinateDoctor { get { return "ReportEditor_AllowMendforSubordinateDoctor"; } }
        public static string ReportEditor_AllowMendforOrdinateDoctor { get { return "ReportEditor_AllowMendforOrdinateDoctor"; } }

        #region Added by Blue Chen for US19331, 09/22/2014
        public static string ReportEditor_AllowRejectForOrdinateDoctor { get { return "ReportEditor_AllowRejectForOrdinateDoctor"; } }
        public static string ReportEditor_RejectReportPolicy { get { return "ReportEditor_RejectReportPolicy"; } }
        #endregion

        public static string ReportEditor_EditWYSWYGPolicy { get { return "ReportEditor_EditWYSWYGPolicy"; } }
        public static string ReportEditor_RespondForEditWYSWYGPolicy { get { return "ReportEditor_RespondForEditWYSWYGPolicy"; } }
        public static string ReportEditor_RightAreaWidth { get { return "ReportEditor_RightAreaWidth"; } }
        public static string ReportEditor_NecessaryItems { get { return "ReportEditor_NecessaryItems"; } }
        public static string ReportEditor_OpenSubmittedReportShowDialog { get { return "OpenOthersSubmittedReport_ShowDialog"; } }
        public static string ReportEditor_UseSpellChecking { get { return "ReportEditor_UseSpellChecking"; } }
        public static string ReportEditor_SpellCheckPopUpOn { get { return "ReportEditor_SpellCheckPopUpOn"; } }
        public static string ReportEditor_SpellCheckOptions { get { return "ReportEditor_SpellCheckOptions"; } }

        public static string ReportEditor_GenerateReportSnapshot { get { return "ReportEditor_GenerateReportSnapshot"; } }
        public static string ReportEditor_CanDelApprovedReport { get { return "ReportEditor_CanDelApprovedReport"; } }
        public static string ReportEditor_CanPhologyTrack { get { return "CanPathologyTrack"; } }
        public static string ReportEditor_LoadAllReportTemplate { get { return "LoadAllReportTemplate"; } }
        public static string ReportEditor_KnottyCaution { get { return "ReportEditor_KnottyCaution"; } }

        public static string ReportEditor_AutoSetFirstVisitMark { get { return "ReportEditor_AutoSetFirstVisitMark"; } }
        

        public const string ReportEditor_DelayFinishReferralAfterApprove = "ReportEditor_DelayFinishReferralAfterApprove";
        public const string ReportEditor_ReferralAfterApprove = "ReportEditor_FinishReferralAfterApprove";
        public const string ReportEditor_RightSideWidth = "ReportEditor_RightSideWidth";
        public const string ReportEditor_ShortBaseInfoFormat = "ReportEditor_ShortBaseInfoFormat";
        public const string ReportEditor_ToolbarSorting = "ReportEditor_ToolbarSorting";

        public const string ReportEditor_ExternalURLforReport = "ExternalURLforReport";
        public const string ReportEditor_PairCheckforWYSWYG = "PairCheckforWYSWYG";
        public const string ReportEditor_SignOnApprove = "ReportEditor_SignOnApprove";
        public const string ReportEditor_BackgroundInitCount = "ReportEditor_BackgroundInitCount";
        public const string ReportEditor_ExpandBaseInfo = "ReportEditor_ExpandBaseInfo";
        public const string ReportEditor_RebuildPolicy = "ReportEditor_RebuildPolicy";
        public const string ReportEditor_UsingReportModalityType = "ReportEditor_UsingReportModalityType";
        public const string ReportEditor_WriteReportModalityType = "WriteReportModalityType";
        public const string ReportEditor_ApproveReportModalityType = "ApproveReportModalityType";
        public const string ReportEditor_StandardReportWorkflow = "StandardReportWorkflow";
        /// <summary>
        /// Rally TA1081
        /// </summary>
        public const string ReportEditor_ClosedOnLocked = "ReportEditor_ClosedOnLocked";
        public const string ReportEditor_AutoNextReport = "ReportEditor_AutoNextReport";
        public const string ReportEditor_NotEnableCheckBoxOnInit = "ReportEditor_NotEnableCheckBoxOnInit";
        public const string ReportEditor_PopupSavingDialogOnSubmit = "ReportEditor_PopupSavingDialogOnSubmit";

        #region Added by Blue Chen for US19333, 09/24/2014
        public const string ReportEditor_NeedToScoringOnApprove = "ReportEditor_NeedToScoringOnApprove";
        public const string ReportEditor_PopupSavingDialogOnApprove = "ReportEditor_PopupSavingDialogOnApprove";
        public const string ReportEditor_NeedToScoringOnReject = "ReportEditor_NeedToScoringOnReject";
        public const string ReportEditor_PopupSavingDialogOnReject = "ReportEditor_PopupSavingDialogOnReject";
        public const string ReportEditor_AllowRejectOnScoring = "ReportEditor_AllowRejectOnScoring";
        #endregion

        public const string ReportEditor_AutoAssignStatus = "AutoAssignStatus";
        public const string ReportEditor_Assign2SiteListAftExam = "Assign2SiteListAftExam";
        public const string ReportEditor_Assign2SiteListAftSubmit = "Assign2SiteListAftSubmit";
        public const string ReportEditor_CriticalSignKeywords = "ReportEditor_CriticalSignKeywords";
        public static string ReportEditor_ReportTemplateRelatedPositive { get { return "ReportEditor_ReportTemplateRelatedPositive"; } }
        public const string ReportEditor_ReferralReportMode = "ReportEditor_ReferralReportMode";
        public const string ReportEditor_CloseAfterSubmit = "ReportEditor_CloseAfterSubmit";
        public const string ReportEditor_PrintUnApprovedReportShowWarning = "ReportEditor_PrintUnApprovedReportShowWarning";
        public const string ReportEditor_ReportTemplateSelectionInfo = "ReportEditor_ReportTemplateSelectionInfo";

        // 2015-06-09, Oscar added (US25173)
        public const string ReportEditor_RecordCostTime = "ReportEditor_RecordCostTime";

        // 2015-06-16, Oscar added (US25250)
        public const string ReportEditor_RememberLastApprover = "ReportEditor_RememberLastApprover";

        // 2015-06-17, Oscar added (US25245)
        public const string CanPrintTemporaryReport = "CanPrintTemporaryReport";

        // 2015-11-02, Oscar added (US28040)
        public const string Report_NeedToCalculateReportAssignWorkload = "Report_NeedToCalculateReportAssignWorkload";

        #endregion

        #region ReportPrintPanel

        public static string ReportPrint_isQuickPrint { get { return "ReportPrint_isQuickPrint"; } }
        public static string ReportPrint_MaxSelection { get { return "ReportPrint_MaxSelection"; } }
        public static string ReportPrint_MaxPrintCount { get { return "ReportPrint_MaxPrintCount"; } }
        public static string ReportPrint_HideCount { get { return "ReportPrint_HideCount"; } }

        #endregion

        #region Clinician

        public static string ClinicReport_ViewAllReports { get { return "ClinicReport_ViewAllReports"; } }
        public static string ClinicReport_SearchDayRange { get { return "ClinicReport_SearchDayRange"; } }
        public static string Clinic_LogLevel { get { return "Clinic_LogLevel"; } }

        #endregion

        #region Register

        public static string Register_Separator { get { return "separator"; } }
        public static string Register_PatientIDPrefix { get { return "PatientIDPrefix"; } }
        public static string Register_PatientIDLength { get { return "PatientIDLength"; } }
        public static string Register_AccNoPolicy { get { return "AccNoPolicy"; } }
        public static string Register_AccNoPrefix { get { return "AccNoPrefix"; } }
        public static string Register_AccNoLength { get { return "AccNoLength"; } }

        #endregion
    }

    public enum ModuleID
    {
        Global = 0x0000,
        Register = 0x0300,
        Report = 0x0400,
        Integration = 0x0E00,
        ExamApplication = 0x0200
    }

    public enum PanelID
    {
        ReportList = 0x0401,
        UnWritenList = 0x0402,
        UnApprovedList = 0x0403,
        ApprovedList = 0x0404,
        PrintingList = 0x0405,
        ReportEditor = 0x0406,
        Web_ClinicReportList = 0x0407,
        Web_ClinicReportViewer = 0x0408,
        AssignedReportList = 0x0409
    }

    /// <summary>
    /// 34	    binary
    /// 35	    text
    /// 56	    integer
    /// 60      money
    /// 61  	datetime
    /// 62      float
    /// 106 	decimal
    /// 167 	varchar
    /// </summary>
    public enum SQLServer_FieldType
    {
        BINARY = 34,
        TEXT = 35,
        DATETIME = 56,
        MONEY = 60,
        INTEGER = 61,
        FLOAT = 62,
        DECIMAL = 106,
        VARCHAR = 167
    }

    //10----NoCheckIn
    //20----CheckIn
    //25----Repeatshot(重拍)
    //30----Examing(正在检查)
    //40----Cancel
    //50----Examination
    //100----Draft
    //105----Reject
    //110----Submit
    //120----FirstApprove
    //130----SecondApprove
    public enum RP_Status
    {
        Unknown = 0,
        NoCheck = 10,
        CheckIn = 20,
        Repeatshot = 25,
        Examing = 30,
        RPCancel = 40,
        Examination = 50,
        Draft = 100,
        Reject = 105,
        Submit = 110,
        FirstApprove = 120,
        SecondApprove = 130
    }

    /// <summary>
    ///TextBox = 0,
    ///CheckCombo = 1,
    ///DateTime = 2
    /// </summary>
    public enum ConditionColumn_ControlType
    {
        TextBox = 0,
        CheckCombo = 1,
        DateTime = 2
    }

    /// <summary>
    /// Default = 0,
    /// Dictionary = 2,
    /// Query = 3
    /// </summary>
    public enum ConditionColumn_DataType
    {
        Default = 0,
        Dictionary = 2,
        Query = 3,
    }

    public enum ConditionColumn_ComparisonOperator
    {
        equal = 0,
        greaterthan = 1,
        notlessthan = 2,
        lessthan = 3,
        notgreaterthan = 4,
        notequal = 5,
        IN = 6,
        notIN = 7,
        like = 8,
        prefix_like = 9,
        subfix_like = 10,
        between = 11,
        advance_like = 12
    }

    public enum ReportFile_Type
    {
        Unknown = 0,
        ReportImage = 1,
        PrintSnapShot = 2
    }

    public enum ReportLog_Type
    {
        Unknown = 0,
        PrintLog = 1,
        PrintSnapshot = 2,
        EventLog_Exam2Draft = 11,
        EventLog_Draft2Submit = 12,
        EventLog_Submit2Approve = 13,
        EventLog_ApproveStart2End = 14,
        EventLog_Exam2Print = 15,

        // 2015-06-08, Oscar added (US25173)
        EventLog_SubmitCostTime = 21,
        EventLog_ApproveCostTime = 22,
    }

    public enum DictionaryTag
    {
        Gender = 1,
        Department = 2,
        InhospitalRegion = 3,
        FilmSpec = 4,
        PatientType = 5,
        AgeUnit = 6,
        JobTitle = 7,
        ApplyDoctor = 8,
        Marriage = 9,
        ErethismCode = 10,
        ErethismGrade = 11,
        BodyCategory = 12,
        RPStatus = 13,
        PrintTemplateType = 14,
        BookingDuration1 = 15,
        FileType = 16,
        AccordRate = 17,
        BookingDuration2 = 18,
        ReportQuality = 19,
        Positive = 21,
        ChargeType = 52,
        Observation = 65,
        Inspection = 66,
        YesNo = 70,
        ContrastName = 80,
        Priority = 115,
        ReportExternalURL = 116,
        DisqualifyImageNotifyIP = 121,
        ReportListExternalAction = 118
    }

    public enum LockType
    {
        Booking = 1,
        Register = 2,
        Report = 3,
        QC = 4
    }

    public class Converter
    {
        public static int toInt(string value)
        {
            try
            {
                return System.Convert.ToInt32(value);
            }
            catch (Exception)
            {
            }

            return 0;
        }

        public static int toInt(object value)
        {
            try
            {
                return System.Convert.ToInt32(value);
            }
            catch (Exception)
            {
            }

            return 0;
        }

        public static void toUpperColumn(ref System.Data.DataSet ds)
        {
            if (ds == null)
                return;

            try
            {
                foreach (System.Data.DataTable dt in ds.Tables)
                {
                    foreach (System.Data.DataColumn dc in dt.Columns)
                    {
                        dc.ColumnName = dc.ColumnName.ToUpper();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("toUpperColumn=" + ex.Message);
            }
        }

        public static string GetStringFromDataRow(System.Data.DataRow dr, string fieldName)
        {
            try
            {
                if (dr != null && dr.Table.Columns.Contains(fieldName))
                {
                    string ret = "";

                    if (dr[fieldName] is DBNull)
                    {
                        ret = "";
                    }
                    else if (dr.Table.Columns[fieldName].DataType == System.Type.GetType("System.Byte[]"))
                    {
                        ret = GetStringFromBytes(dr[fieldName]);
                    }
                    else
                    {
                        ret = dr[fieldName].ToString();
                    }

                    return ret;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return "";
        }

        public static string GetStringFromDataRow(System.Data.DataRow dr, int fieldIndex)
        {
            try
            {
                if (dr != null && dr.Table.Columns.Count > fieldIndex)
                {
                    string ret = "";

                    if (dr[fieldIndex] is DBNull)
                    {
                        ret = "";
                    }
                    else if (dr.Table.Columns[fieldIndex].DataType == System.Type.GetType("System.Byte[]"))
                    {
                        ret = GetStringFromBytes(dr[fieldIndex]);
                    }
                    else
                    {
                        ret = dr[fieldIndex].ToString();
                    }

                    return ret;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return "";
        }

        public static int GetIntFromDataRow(System.Data.DataRow dr, string fieldName, int defaultInt)
        {
            try
            {
                if (dr != null && dr.Table.Columns.Contains(fieldName))
                {
                    if (dr[fieldName] is DBNull)
                    {
                        return defaultInt;
                    }
                    else
                    {
                        return System.Convert.ToInt32(dr[fieldName]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return defaultInt;
        }

        public static int GetIntFromDataRow(System.Data.DataRow dr, int fieldIndex, int defaultInt)
        {
            try
            {
                if (dr != null && dr.Table.Columns.Count > fieldIndex)
                {
                    if (dr[fieldIndex] is DBNull)
                    {
                        return defaultInt;
                    }
                    else
                    {
                        return System.Convert.ToInt32(dr[fieldIndex]);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return defaultInt;
        }

        public static byte[] GetBytes(object value)
        {
            try
            {
                if (value != null && !(value is DBNull))
                    return System.Text.Encoding.Default.GetBytes(value as string);
            }
            catch (Exception)
            {
            }

            return new byte[] { 0 };
        }

        public static string GetStringFromBytes(object buff)
        {
            try
            {
                if (buff != null && !(buff is DBNull))
                    return System.Text.Encoding.Default.GetString(buff as byte[]);
            }
            catch (Exception)
            {
            }

            return "";
        }
    }

    public class IOFun
    {
        public static void DeleteDirectory(string path)
        {
            if (path == null || !System.IO.Directory.Exists(path))
                return;

            try
            {
                DeleteSubDirAndFile(path);

                System.IO.Directory.Delete(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public static void DeleteSubDirAndFile(string path)
        {
            if (path == null || !System.IO.Directory.Exists(path))
                return;

            string[] files = System.IO.Directory.GetFiles(path);
            foreach (string tmp in files)
            {
                //System.IO.File fi = System.IO.File.GetAttributes(tmp);
                //fi.IsReadOnly = false;

                //System.IO.File.SetAttributes(tmp, fi);

                System.IO.File.Delete(tmp);
            }

            string[] subPath = System.IO.Directory.GetDirectories(path);

            foreach (string tmp in subPath)
            {
                DeleteSubDirAndFile(tmp);
            }

            for (int i = 0; i < subPath.Length; i++)
            {
                try
                {
                    System.IO.Directory.Delete(subPath[i]);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
    }

    /// <summary>
    ///  0 rtf
    ///  1 html
    ///  2 jpg
    ///  3 txt
    ///  4 xml
    ///  5 pdf
    ///  6 directly printer
    /// </summary>
    public enum ExportType
    {
        RTF = 0,
        HTML = 1,
        JPG,
        TXT,
        XML,
        PDF,
        PRINTER
    }

    public enum ReportList_Refresh
    {
        None = 0,
        ByReport = 1,
        ByRP = 2,
        RefreshList = 3
    }

    public enum ReportList_ActionOnEmptyCondition
    {
        NotCheck = 0,
        StopWithNotice,
        StopWithoutNotice
    }
}
