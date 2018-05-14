using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using Common.Consts;
using System.Globalization;

namespace CommonGlobalSettings
{
    #region RefReport

    /// <summary>
    /// RefReportModel object mapped table 'tReport'.
    /// </summary>
    public class RefReportModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _reportName = string.Empty;
        protected byte[] _wYS;
        protected byte[] _wYG;
        protected byte[] _appendInfo = null;
        protected string _techInfo;
        protected string _reportText = string.Empty;
        protected string _doctorAdvice = string.Empty;
        protected int _isPositive;
        protected string _acrCode = string.Empty;
        protected string _acrAnatomic = string.Empty;
        protected string _acrPathologic = string.Empty;
        protected string _creater = string.Empty;
        protected DateTime? _createDt;
        protected string _submitter = string.Empty;
        protected DateTime? _submitDt;
        protected string _firstApprover = string.Empty;
        protected DateTime? _firstApproveDt;
        protected string _secondApprover = string.Empty;
        protected DateTime? _secondApproveDt;
        protected int _isDiagnosisRight;
        protected string _keyWord = string.Empty;
        protected string _reportQuality = string.Empty;
        protected string _rejectToObject = string.Empty;
        protected string _rejecter = string.Empty;
        protected DateTime? _rejectDt;
        protected int _status;
        protected string _comments = string.Empty;
        protected int _deleteMark;
        protected string _deleter = string.Empty;
        protected DateTime? _deleteDt;
        protected string _recuperator = string.Empty;
        protected DateTime? _reconvertDt;
        protected string _mender = string.Empty;
        protected string _menderName = string.Empty;
        protected DateTime? _modifyDt;
        protected int _isPrint;
        protected string _checkItemName = string.Empty;
        protected string _optional1 = string.Empty;
        protected string _optional2 = string.Empty;
        protected string _optional3 = string.Empty;
        protected int _isLeaveWord;
        protected string _wYSText = string.Empty;
        protected string _wYGText = string.Empty;
        protected int _isDraw;
        protected byte[] _drawerSign = null;
        protected DateTime? _drawTime;
        protected int _isLeaveSound;
        protected string _takeFilmDept = string.Empty;
        protected string _takeFilmRegion = string.Empty;
        protected string _takeFilmComment = string.Empty;
        protected int _printCopies;
        protected string _printTemplateGuid = string.Empty;
        protected string _domain = string.Empty;
        protected int _readOnly;
        protected string _submitDomain = string.Empty;
        protected string _rejectDomain = string.Empty;
        protected string _firstApproveDomain = string.Empty;
        protected string _secondApproveDomain = string.Empty;
        protected string _rejectSite = string.Empty;
        protected string _submitSite = string.Empty;
        protected string _firstApproveSite = string.Empty;
        protected string _secondApproveSite = string.Empty;
        protected string _submitterName = string.Empty;
        protected string _firstApproverName = string.Empty;
        protected string _secondApproverName = string.Empty;
        protected string _createrName = string.Empty;
        protected Nullable<System.DateTime> _updateTime;
        protected int _uploaded = 0;

        protected List<RefReportFileModel> _reportFiles = null;
        #endregion

        #region Constructors

        public RefReportModel() { _reportFiles = new List<RefReportFileModel>(); }

        #endregion

        #region Public Properties

        [DataField("ReportGuid")]
        public string ReportGuid
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 128)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
                _id = value;
            }
        }

        [DataField("ReportName")]
        public string ReportName
        {
            get { return _reportName; }
            set
            {
                _reportName = value;
            }
        }

        [DataField("WYS")]
        public byte[] WYS
        {
            get { return _wYS; }
            set { _wYS = value; }
        }

        [DataField("WYG")]
        public byte[] WYG
        {
            get { return _wYG; }
            set { _wYG = value; }
        }

        [DataField("AppendInfo")]
        public byte[] AppendInfo
        {
            get { return _appendInfo; }
            set { _appendInfo = value; }
        }

        [DataField("TechInfo")]
        public string TechInfo
        {
            get { return _techInfo; }
            set { _techInfo = value; }
        }

        [DataField("ReportText")]
        public string ReportText
        {
            get { return _reportText; }
            set
            {
                _reportText = value;
            }
        }

        [DataField("DoctorAdvice")]
        public string DoctorAdvice
        {
            get { return _doctorAdvice; }
            set
            {
                _doctorAdvice = value;
            }
        }

        [DataField("IsPositive")]
        public int IsPositive
        {
            get { return _isPositive; }
            set { _isPositive = value; }
        }

        [DataField("AcrCode")]
        public string AcrCode
        {
            get { return _acrCode; }
            set
            {
                _acrCode = value;
            }
        }

        [DataField("AcrAnatomic")]
        public string AcrAnatomic
        {
            get { return _acrAnatomic; }
            set
            {
                _acrAnatomic = value;
            }
        }

        [DataField("AcrPathologic")]
        public string AcrPathologic
        {
            get { return _acrPathologic; }
            set
            {
                _acrPathologic = value;
            }
        }

        [DataField("Creater")]
        public string Creater
        {
            get { return _creater; }
            set
            {
                _creater = value;
            }
        }

        [DataField("CreateDt")]
        public DateTime? CreateDt
        {
            get { return _createDt; }
            set { _createDt = value; }
        }

        [DataField("Submitter")]
        public string Submitter
        {
            get { return _submitter; }
            set
            {
                _submitter = value;
            }
        }

        [DataField("SubmitDt")]
        public DateTime? SubmitDt
        {
            get { return _submitDt; }
            set { _submitDt = value; }
        }

        [DataField("FirstApprover")]
        public string FirstApprover
        {
            get { return _firstApprover; }
            set
            {
                _firstApprover = value;
            }
        }

        [DataField("FirstApproveDt")]
        public DateTime? FirstApproveDt
        {
            get { return _firstApproveDt; }
            set { _firstApproveDt = value; }
        }

        [DataField("SecondApprover")]
        public string SecondApprover
        {
            get { return _secondApprover; }
            set
            {
                _secondApprover = value;
            }
        }

        [DataField("SecondApproveDt")]
        public DateTime? SecondApproveDt
        {
            get { return _secondApproveDt; }
            set { _secondApproveDt = value; }
        }

        [DataField("IsDiagnosisRight")]
        public int IsDiagnosisRight
        {
            get { return _isDiagnosisRight; }
            set { _isDiagnosisRight = value; }
        }

        [DataField("KeyWord")]
        public string KeyWord
        {
            get { return _keyWord; }
            set
            {
                _keyWord = value;
            }
        }

        [DataField("ReportQuality")]
        public string ReportQuality
        {
            get { return _reportQuality; }
            set
            {
                _reportQuality = value;
            }
        }

        [DataField("RejectToObject")]
        public string RejectToObject
        {
            get { return _rejectToObject; }
            set
            {
                _rejectToObject = value;
            }
        }

        [DataField("Rejecter")]
        public string Rejecter
        {
            get { return _rejecter; }
            set
            {
                _rejecter = value;
            }
        }

        [DataField("RejectDt")]
        public DateTime? RejectDt
        {
            get { return _rejectDt; }
            set { _rejectDt = value; }
        }

        [DataField("Status")]
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        [DataField("Comments")]
        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
            }
        }

        [DataField("DeleteMark")]
        public int DeleteMark
        {
            get { return _deleteMark; }
            set { _deleteMark = value; }
        }

        [DataField("Deleter")]
        public string Deleter
        {
            get { return _deleter; }
            set
            {
                _deleter = value;
            }
        }

        [DataField("DeleteDt")]
        public DateTime? DeleteDt
        {
            get { return _deleteDt; }
            set { _deleteDt = value; }
        }

        [DataField("Recuperator")]
        public string Recuperator
        {
            get { return _recuperator; }
            set
            {
                _recuperator = value;
            }
        }

        [DataField("ReconvertDt")]
        public DateTime? ReconvertDt
        {
            get { return _reconvertDt; }
            set { _reconvertDt = value; }
        }

        [DataField("Mender")]
        public string Mender
        {
            get { return _mender; }
            set
            {
                _mender = value;
            }
        }

        [DataField("ModifyDt")]
        public DateTime? ModifyDt
        {
            get { return _modifyDt; }
            set { _modifyDt = value; }
        }

        [DataField("IsPrint")]
        public int IsPrint
        {
            get { return _isPrint; }
            set { _isPrint = value; }
        }

        [DataField("CheckItemName")]
        public string CheckItemName
        {
            get { return _checkItemName; }
            set
            {
                _checkItemName = value;
            }
        }

        [DataField("Optional1")]
        public string Optional1
        {
            get { return _optional1; }
            set
            {
                _optional1 = value;
            }
        }

        [DataField("Optional2")]
        public string Optional2
        {
            get { return _optional2; }
            set
            {
                _optional2 = value;
            }
        }

        [DataField("Optional3")]
        public string Optional3
        {
            get { return _optional3; }
            set
            {
                _optional3 = value;
            }
        }

        [DataField("IsLeaveWord")]
        public int IsLeaveWord
        {
            get { return _isLeaveWord; }
            set { _isLeaveWord = value; }
        }

        [DataField("WYSText")]
        public string WYSText
        {
            get { return _wYSText; }
            set
            {
                _wYSText = value;
            }
        }

        [DataField("WYGText")]
        public string WYGText
        {
            get { return _wYGText; }
            set
            {
                _wYGText = value;
            }
        }

        [DataField("IsDraw")]
        public int IsDraw
        {
            get { return _isDraw; }
            set { _isDraw = value; }
        }

        [DataField("DrawerSign")]
        public byte[] DrawerSign
        {
            get { return _drawerSign; }
            set { _drawerSign = value; }
        }

        [DataField("DrawTime")]
        public DateTime? DrawTime
        {
            get { return _drawTime; }
            set { _drawTime = value; }
        }

        [DataField("IsLeaveSound")]
        public int IsLeaveSound
        {
            get { return _isLeaveSound; }
            set { _isLeaveSound = value; }
        }

        [DataField("TakeFilmDept")]
        public string TakeFilmDept
        {
            get { return _takeFilmDept; }
            set
            {
                _takeFilmDept = value;
            }
        }

        [DataField("TakeFilmRegion")]
        public string TakeFilmRegion
        {
            get { return _takeFilmRegion; }
            set
            {
                _takeFilmRegion = value;
            }
        }

        [DataField("TakeFilmComment")]
        public string TakeFilmComment
        {
            get { return _takeFilmComment; }
            set
            {
                _takeFilmComment = value;
            }
        }

        [DataField("PrintCopies")]
        public int PrintCopies
        {
            get { return _printCopies; }
            set { _printCopies = value; }
        }

        [DataField("PrintTemplateGuid")]
        public string PrintTemplateGuid
        {
            get { return _printTemplateGuid; }
            set
            {
                _printTemplateGuid = value;
            }
        }

        [DataField("Domain")]
        public string Domain
        {
            get { return _domain; }
            set
            {
                _domain = value;
            }
        }

        [DataField("ReadOnly")]
        public int ReadOnly
        {
            get { return _readOnly; }
            set { _readOnly = value; }
        }

        [DataField("SubmitDomain")]
        public string SubmitDomain
        {
            get { return _submitDomain; }
            set
            {
                _submitDomain = value;
            }
        }

        [DataField("RejectDomain")]
        public string RejectDomain
        {
            get { return _rejectDomain; }
            set
            {
                _rejectDomain = value;
            }
        }

        [DataField("FirstApproveDomain")]
        public string FirstApproveDomain
        {
            get { return _firstApproveDomain; }
            set
            {
                _firstApproveDomain = value;
            }
        }

        [DataField("SecondApproveDomain")]
        public string SecondApproveDomain
        {
            get { return _secondApproveDomain; }
            set
            {
                _secondApproveDomain = value;
            }
        }

        [DataField("RejectSite")]
        public string RejectSite
        {
            get { return _rejectSite; }
            set { _rejectSite = value; }
        }

        [DataField("SubmitSite")]
        public string SubmitSite
        {
            get { return _submitSite; }
            set { _submitSite = value; }
        }

        [DataField("FirstApproveSite")]
        public string FirstApproveSite
        {
            get { return _firstApproveSite; }
            set { _firstApproveSite = value; }
        }

        [DataField("SecondApproveSite")]
        public string SecondApproveSite
        {
            get { return _secondApproveSite; }
            set { _secondApproveSite = value; }
        }

        [DataField("SubmitterName")]
        public string SubmitterName
        {
            get { return _submitterName; }
            set { _submitterName = value; }
        }

        [DataField("FirstApproverName")]
        public string FirstApproverName
        {
            get { return _firstApproverName; }
            set { _firstApproverName = value; }
        }

        [DataField("SecondApproverName")]
        public string SecondApproverName
        {
            get { return _secondApproverName; }
            set { _secondApproverName = value; }
        }

        [DataField("CreaterName")]
        public string CreaterName
        {
            get { return _createrName; }
            set { _createrName = value; }
        }

        [DataField("MenderName")]
        public string MenderName
        {
            get { return _menderName; }
            set
            {
                _menderName = value;
            }
        }

        [DataField("UpdateTime")]
        public Nullable<System.DateTime> UpdateTime
        {
            get { return _updateTime; }
            set { _updateTime = value; }
        }

        [DataField("Uploaded")]
        public int Uploaded
        {
            get { return _uploaded; }
            set { _uploaded = value; }
        }

        public List<RefReportFileModel> ReportFileModels
        {
            get
            {
                if (_reportFiles == null)
                { _reportFiles = new List<RefReportFileModel>(); }
                return _reportFiles;
            }
            set
            {
                _reportFiles = value;
            }
        }

        #endregion

        #region Pulbic Methods

        public XElement SerializeXML()
        {
            XElement element = new XElement("Report",
            new XElement("REPORTGUID", this.ReportGuid),
            new XElement("REPORTNAME", this.ReportName),
            new XElement("WYS", Convert.ToBase64String(this.WYS)),
            new XElement("WYG", Convert.ToBase64String(this.WYG)),
                //new XElement("APPENDINFO", this.AppendInfo),
            new XElement("TECHINFO", this.TechInfo),
            new XElement("REPORTTEXT", this.ReportText),
            new XElement("DOCTORADVICE", this.DoctorAdvice),
            new XElement("ISPOSITIVE", this.IsPositive),
            new XElement("ACRCODE", this.AcrCode),
            new XElement("ACRANATOMIC", this.AcrAnatomic),
            new XElement("ACRPATHOLOGIC", this.AcrPathologic),
            new XElement("CREATER", this.Creater),
            new XElement("CREATEDT", this.CreateDt.HasValue ? this.CreateDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("SUBMITTER", this.Submitter),
            new XElement("SUBMITDT", this.SubmitDt.HasValue ? this.SubmitDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("FIRSTAPPROVER", this.FirstApprover),
            new XElement("FIRSTAPPROVEDT", this.FirstApproveDt.HasValue ? this.FirstApproveDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("SECONDAPPROVER", this.SecondApprover),
            new XElement("SECONDAPPROVEDT", this.SecondApproveDt.HasValue ? this.SecondApproveDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("ISDIAGNOSISRIGHT", this.IsDiagnosisRight),
            new XElement("KEYWORD", this.KeyWord),
            new XElement("REPORTQUALITY", this.ReportQuality),
            new XElement("REJECTTOOBJECT", this.RejectToObject),
            new XElement("REJECTER", this.Rejecter),
            new XElement("REJECTDT", this.RejectDt.HasValue ? this.RejectDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("STATUS", this.Status),
            new XElement("COMMENTS", Convert.ToBase64String(Encoding.Unicode.GetBytes(this.Comments))),
            new XElement("DELETEMARK", this.DeleteMark),
            new XElement("DELETER", this.Deleter),
            new XElement("DELETEDT", this.DeleteDt.HasValue ? this.DeleteDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("RECUPERATOR", this.Recuperator),
            new XElement("RECONVERTDT", this.ReconvertDt.HasValue ? this.ReconvertDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("MENDER", this.Mender),
            new XElement("MODIFYDT", this.ModifyDt.HasValue ? this.ModifyDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("ISPRINT", this.IsPrint),
            new XElement("CHECKITEMNAME", this.CheckItemName),
            new XElement("OPTIONAL1", this.Optional1),
            new XElement("OPTIONAL2", this.Optional2),
            new XElement("OPTIONAL3", this.Optional3),
            new XElement("ISLEAVEWORD", this.IsLeaveWord),
            new XElement("WYSTEXT", this.WYSText),
            new XElement("WYGTEXT", this.WYGText),
            new XElement("ISDRAW", this.IsDraw),
            new XElement("DRAWERSIGN", this.DrawerSign),
            new XElement("DRAWTIME", this.DrawTime.HasValue ? this.DrawTime.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("ISLEAVESOUND", this.IsLeaveSound),
            new XElement("TAKEFILMDEPT", this.TakeFilmDept),
            new XElement("TAKEFILMREGION", this.TakeFilmRegion),
            new XElement("TAKEFILMCOMMENT", this.TakeFilmComment),
            new XElement("PRINTCOPIES", this.PrintCopies),
            new XElement("PRINTTEMPLATEGUID", this.PrintTemplateGuid),
            new XElement("DOMAIN", this.Domain),
            new XElement("READONLY", this.ReadOnly),
            new XElement("SUBMITDOMAIN", this.SubmitDomain),
            new XElement("REJECTDOMAIN", this.RejectDomain),
            new XElement("FIRSTAPPROVEDOMAIN", this.FirstApproveDomain),
            new XElement("SECONDAPPROVEDOMAIN", this.SecondApproveDomain),
            new XElement("REJECTSITE", this.RejectSite),
            new XElement("SUBMITSITE", this.SubmitSite),
            new XElement("FIRSTAPPROVESITE", this.FirstApproveSite),
            new XElement("SECONDAPPROVESITE", this.SecondApproveSite),
            new XElement("SUBMITTERNAME", this.SubmitterName),
            new XElement("FIRSTAPPROVERNAME", this.FirstApproverName),
            new XElement("SENCONDAPPROVERNAME", this.SecondApproverName),
            new XElement("CREATERNAME", this.CreaterName),
            new XElement("MENDERNAME", this.MenderName),
            new XElement("UPDATETIME", this.UpdateTime.HasValue ? this.UpdateTime.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("UPLOADED", this.Uploaded));

            foreach (var reportFile in _reportFiles)
            {
                element.Add(reportFile.SerializeXML());
            }
            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("REPORTGUID");
            this.ReportGuid = e == null ? "" : e.Value;

            e = element.Element("REPORTNAME");
            this.ReportName = e == null ? "" : e.Value;

            e = element.Element("WYS");
            this.WYS = e == null ? null : Convert.FromBase64String(e.Value);

            e = element.Element("WYG");
            this.WYG = e == null ? null : Convert.FromBase64String(e.Value);

            //e = element.Element("APPENDINFO");
            //this.AppendInfo = e == null ? "" : e.Value;

            e = element.Element("TECHINFO");
            this.TechInfo = e == null ? "" : e.Value;

            e = element.Element("REPORTTEXT");
            this.ReportText = e == null ? "" : e.Value;

            e = element.Element("DOCTORADVICE");
            this.DoctorAdvice = e == null ? "" : e.Value;

            e = element.Element("ISPOSITIVE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsPositive = 0;
            else
                this.IsPositive = Convert.ToInt32(e.Value);

            e = element.Element("ACRCODE");
            this.AcrCode = e == null ? "" : e.Value;

            e = element.Element("ACRANATOMIC");
            this.AcrAnatomic = e == null ? "" : e.Value;

            e = element.Element("ACRPATHOLOGIC");
            this.AcrPathologic = e == null ? "" : e.Value;

            e = element.Element("CREATER");
            this.Creater = e == null ? "" : e.Value;

            e = element.Element("CREATEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.CreateDt = null;
            else
                this.CreateDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("SUBMITTER");
            this.Submitter = e == null ? "" : e.Value;

            e = element.Element("SUBMITDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.SubmitDt = null;
            else
                this.SubmitDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("FIRSTAPPROVER");
            this.FirstApprover = e == null ? "" : e.Value;

            e = element.Element("FIRSTAPPROVEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.FirstApproveDt = null;
            else
                this.FirstApproveDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("SECONDAPPROVER");
            this.SecondApprover = e == null ? "" : e.Value;

            e = element.Element("SECONDAPPROVEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.SecondApproveDt = null;
            else
                this.SecondApproveDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("ISDIAGNOSISRIGHT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsDiagnosisRight = 0;
            else
                this.IsDiagnosisRight = Convert.ToInt32(e.Value);

            e = element.Element("KEYWORD");
            this.KeyWord = e == null ? "" : e.Value;

            e = element.Element("REPORTQUALITY");
            this.ReportQuality = e == null ? "" : e.Value;

            e = element.Element("REJECTTOOBJECT");
            this.RejectToObject = e == null ? "" : e.Value;

            e = element.Element("REJECTER");
            this.Rejecter = e == null ? "" : e.Value;

            e = element.Element("REJECTDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.RejectDt = null;
            else
                this.RejectDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("STATUS");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Status = 0;
            else
                this.Status = Convert.ToInt32(e.Value);

            e = element.Element("COMMENTS");
            this.Comments = e == null ? "" : Encoding.Unicode.GetString(Convert.FromBase64String(e.Value));

            e = element.Element("DELETEMARK");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.DeleteMark = 0;
            else
                this.DeleteMark = Convert.ToInt32(e.Value);

            e = element.Element("DELETER");
            this.Deleter = e == null ? "" : e.Value;

            e = element.Element("DELETEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.DeleteDt = null;
            else
                this.DeleteDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("RECUPERATOR");
            this.Recuperator = e == null ? "" : e.Value;

            e = element.Element("RECONVERTDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ReconvertDt = null;
            else
                this.ReconvertDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("MENDER");
            this.Mender = e == null ? "" : e.Value;

            e = element.Element("MODIFYDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ModifyDt = null;
            else
                this.ModifyDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("ISPRINT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsPrint = 0;
            else
                this.IsPrint = Convert.ToInt32(e.Value);

            e = element.Element("CHECKITEMNAME");
            this.CheckItemName = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL1");
            this.Optional1 = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL2");
            this.Optional2 = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL3");
            this.Optional3 = e == null ? "" : e.Value;

            e = element.Element("ISLEAVEWORD");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsLeaveWord = 0;
            else
                this.IsLeaveWord = Convert.ToInt32(e.Value);

            e = element.Element("WYSTEXT");
            this.WYSText = e == null ? "" : e.Value;

            e = element.Element("WYGTEXT");
            this.WYGText = e == null ? "" : e.Value;

            e = element.Element("ISDRAW");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsDraw = 0;
            else
                this.IsDraw = Convert.ToInt32(e.Value);

            //e = element.Element("DRAWERSIGN");
            //this.DrawerSign = e == null ? "" : e.Value;

            e = element.Element("DRAWTIME");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.DrawTime = null;
            else
                this.DrawTime = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("ISLEAVESOUND");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsLeaveSound = 0;
            else
                this.IsLeaveSound = Convert.ToInt32(e.Value);

            e = element.Element("TAKEFILMDEPT");
            this.TakeFilmDept = e == null ? "" : e.Value;

            e = element.Element("TAKEFILMREGION");
            this.TakeFilmRegion = e == null ? "" : e.Value;

            e = element.Element("TAKEFILMCOMMENT");
            this.TakeFilmComment = e == null ? "" : e.Value;

            e = element.Element("PRINTCOPIES");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.PrintCopies = 0;
            else
                this.PrintCopies = Convert.ToInt32(e.Value);

            e = element.Element("PRINTTEMPLATEGUID");
            this.PrintTemplateGuid = e == null ? "" : e.Value;

            e = element.Element("DOMAIN");
            this.Domain = e == null ? "" : e.Value;

            e = element.Element("READONLY");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ReadOnly = 0;
            else
                this.ReadOnly = Convert.ToInt32(e.Value);

            e = element.Element("SUBMITDOMAIN");
            this.SubmitDomain = e == null ? "" : e.Value;

            e = element.Element("REJECTDOMAIN");
            this.RejectDomain = e == null ? "" : e.Value;

            e = element.Element("FIRSTAPPROVEDOMAIN");
            this.FirstApproveDomain = e == null ? "" : e.Value;

            e = element.Element("SECONDAPPROVEDOMAIN");
            this.SecondApproveDomain = e == null ? "" : e.Value;

            e = element.Element("REJECTSITE");
            this.RejectSite = e == null ? "" : e.Value;

            e = element.Element("SUBMITSITE");
            this.SubmitSite = e == null ? "" : e.Value;

            e = element.Element("FIRSTAPPROVESITE");
            this.FirstApproveSite = e == null ? "" : e.Value;

            e = element.Element("SECONDAPPROVESITE");
            this.SecondApproveSite = e == null ? "" : e.Value;

            e = element.Element("SUBMITTERNAME");
            this.SubmitterName = e == null ? "" : e.Value;
            e = element.Element("FIRSTAPPROVERNAME");
            this.FirstApproverName = e == null ? "" : e.Value;
            e = element.Element("SENCONDAPPROVERNAME");
            this.SecondApproverName = e == null ? "" : e.Value;

            e = element.Element("CREATERNAME");
            this.CreaterName = e == null ? "" : e.Value;

            e = element.Element("MENDERNAME");
            this.MenderName = e == null ? "" : e.Value;

            e = element.Element("UPDATETIME");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.UpdateTime = null;
            else
                this.UpdateTime = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("UPLOADED");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Uploaded = 0;
            else
                this.Uploaded = Convert.ToInt32(e.Value);

            var reportfielNodes = element.Elements("ReportFile");

            foreach (var fileNode in reportfielNodes)
            {
                RefReportFileModel rptFile = new RefReportFileModel();
                rptFile.DeSerializeXML(fileNode);

                _reportFiles.Add(rptFile);
            }
        }

        #endregion
    }
    #endregion
}
