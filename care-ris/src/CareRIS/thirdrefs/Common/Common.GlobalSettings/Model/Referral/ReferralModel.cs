using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using Common.Consts;
using System.Globalization;

namespace CommonGlobalSettings
{
    /// <summary>
    /// ReferralModel mapped table 'tReferralList'
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralViewModel))]
    public class ReferralModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _patientID = string.Empty;
        protected string _localName = string.Empty;
        protected string _englishName = string.Empty;
        protected string _gender = string.Empty;
        protected DateTime? _birthday;
        protected string _telePhone = string.Empty;
        protected string _address = string.Empty;
        protected string _accNo = string.Empty;
        protected string _applyDoctor = string.Empty;
        protected DateTime? _applyDt;
        protected string _modalityType = string.Empty;
        protected string _procedureCode = string.Empty;
        protected string _checkingItem = string.Empty;
        protected string _healthHistory = string.Empty;
        protected string _observation = string.Empty;
        protected int _refPurpose;
        protected int _rPStatus;
        protected int _refStatus;
        protected string _examDomain = string.Empty;
        protected string _examAccNo = string.Empty;
        protected DateTime? _createDt;
        protected string _initialDomain = string.Empty;
        protected string _sourceDomain = string.Empty;
        protected string _targetDomain = string.Empty;
        protected string _targetSite = string.Empty;
        protected int _direction = 0;
        protected int _isExistSnapshot;
        protected string _getReportDomain = string.Empty;
        protected DateTime? _bookingBeginDt;
        protected DateTime? _bookingEndDt;
        protected string _orignalBizData = string.Empty;
        protected string _pakagedBizData = string.Empty;
        protected string _sourceSite = string.Empty;
        protected int _scope = 2;//multisite = 1, multidomain = 2; default multidomain
        protected int _memoCount = 0;
        protected string _refApplication = string.Empty;
        protected string _refReport = string.Empty;
        //protected int _direction;
        protected CoreBussinessModel _corBizModel = null;
        #endregion

        #region Constructors

        public ReferralModel() { }

        #endregion

        #region Public Properties

        [DataField("ReferralID")]
        public string ReferralID
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 128)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
                _id = value;
            }
        }

        [DataField("PatientID")]
        public string PatientID
        {
            get { return _patientID; }
            set
            {
                _patientID = value;
            }
        }

        [DataField("LocalName")]
        public string LocalName
        {
            get { return _localName; }
            set
            {
                _localName = value;
            }
        }

        [DataField("EnglishName")]
        public string EnglishName
        {
            get { return _englishName; }
            set
            {
                _englishName = value;
            }
        }

        [DataField("Gender")]
        public string Gender
        {
            get { return _gender; }
            set
            {
                _gender = value;
            }
        }

        [DataField("Birthday")]
        public DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }

        [DataField("TelePhone")]
        public string TelePhone
        {
            get { return _telePhone; }
            set
            {
                _telePhone = value;
            }
        }

        [DataField("Address")]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }

        [DataField("AccNo")]
        public string AccNo
        {
            get { return _accNo; }
            set
            {
                _accNo = value;
            }
        }

        [DataField("ApplyDoctor")]
        public string ApplyDoctor
        {
            get { return _applyDoctor; }
            set
            {
                _applyDoctor = value;
            }
        }

        [DataField("ApplyDt")]
        public DateTime? ApplyDt
        {
            get { return _applyDt; }
            set { _applyDt = value; }
        }

        [DataField("ModalityType")]
        public string ModalityType
        {
            get { return _modalityType; }
            set
            {
                _modalityType = value;
            }
        }

        [DataField("ProcedureCode")]
        public string ProcedureCode
        {
            get { return _procedureCode; }
            set
            {
                if (value != null && value.Length > 256)
                    throw new ArgumentOutOfRangeException("Invalid value for ProcedureCode", value, value.ToString());
                _procedureCode = value;
            }
        }

        [DataField("CheckingItem")]
        public string CheckingItem
        {
            get { return _checkingItem; }
            set
            {
                if (value != null && value.Length > 256)
                    throw new ArgumentOutOfRangeException("Invalid value for CheckingItem", value, value.ToString());
                _checkingItem = value;
            }
        }

        [DataField("HealthHistory")]
        public string HealthHistory
        {
            get { return _healthHistory; }
            set
            {
                if (value != null && value.Length > 512)
                    throw new ArgumentOutOfRangeException("Invalid value for HealthHistory", value, value.ToString());
                _healthHistory = value;
            }
        }

        [DataField("Observation")]
        public string Observation
        {
            get { return _observation; }
            set
            {
                if (value != null && value.Length > 512)
                    throw new ArgumentOutOfRangeException("Invalid value for Observation", value, value.ToString());
                _observation = value;
            }
        }

        [DataField("RefPurpose")]
        public int RefPurpose
        {
            get { return _refPurpose; }
            set { _refPurpose = value; }
        }

        [DataField("RPStatus")]
        public int RPStatus
        {
            get { return _rPStatus; }
            set { _rPStatus = value; }
        }

        [DataField("RefStatus")]
        public int RefStatus
        {
            get { return _refStatus; }
            set { _refStatus = value; }
        }

        [DataField("ExamDomain")]
        public string ExamDomain
        {
            get { return _examDomain; }
            set
            {
                _examDomain = value;
            }
        }

        [DataField("ExamAccNo")]
        public string ExamAccNo
        {
            get { return _examAccNo; }
            set
            {
                _examAccNo = value;
            }
        }

        [DataField("CreateDt")]
        public DateTime? CreateDt
        {
            get { return _createDt; }
            set { _createDt = value; }
        }

        [DataField("InitialDomain")]
        public string InitialDomain
        {
            get { return _initialDomain; }
            set
            {
                _initialDomain = value;
            }
        }

        [DataField("SourceDomain")]
        public string SourceDomain
        {
            get { return _sourceDomain; }
            set
            {
                _sourceDomain = value;
            }
        }

        [DataField("TargetDomain")]
        public string TargetDomain
        {
            get { return _targetDomain; }
            set
            {
                _targetDomain = value;
            }
        }

        [DataField("TargetSite")]
        public string TargetSite
        {
            get { return _targetSite; }
            set
            {
                _targetSite = value;
            }
        }

        [DataField("Direction")]
        public int Direction
        {
            get { return _direction; }
            set
            {
                _direction = value;
            }
        }

        [DataField("IsExistSnapshot")]
        public int IsExistSnapshot
        {
            get { return _isExistSnapshot; }
            set { _isExistSnapshot = value; }
        }

        [DataField("GetReportDomain")]
        public string GetReportDomain
        {
            get { return _getReportDomain; }
            set
            {
                _getReportDomain = value;
            }
        }

        [DataField("BookingBeginDt")]
        public DateTime? BookingBeginDt
        {
            get { return _bookingBeginDt; }
            set { _bookingBeginDt = value; }
        }

        [DataField("BookingEndDt")]
        public DateTime? BookingEndDt
        {
            get { return _bookingEndDt; }
            set { _bookingEndDt = value; }
        }

        [DataField("OriginalBizData")]
        public string OriginalBizData
        {
            get { return _orignalBizData; }
            set { _orignalBizData = value; }
        }

        [DataField("PackagedBizData")]
        public string PackagedBizData
        {
            get { return _pakagedBizData; }
            set { _pakagedBizData = value; }
        }

        [DataField("SourceSite")]
        public string SourceSite
        {
            get { return _sourceSite; }
            set { _sourceSite = value; }
        }

        [DataField("Scope")]
        public int Scope
        {
            get { return _scope; }
            set { _scope = value; }
        }
        //[DataField("Direction")]
        //public int Direction
        //{
        //    get { return _direction; }
        //    set { _direction = value; }
        //}

        [DataField("MemoCount")]
        public int MemoCount
        {
            get{return _memoCount;}
            set { _memoCount = value; }
        }

        [DataField("RefApplication")]
        public string RefApplication
        {
            get { return _refApplication; }
            set { _refApplication = value; }
        }

        [DataField("RefReport")]
        public string RefReport
        {
            get { return _refReport; }
            set { _refReport = value; }
        }
        #endregion

        #region Public method

        public XElement SerializeXML()
        {
            XElement element = new XElement("Referral",
                new XElement("REFERRALID", this.ReferralID),
                new XElement("PATIENTID", this.PatientID),
                new XElement("LOCALNAME", this.LocalName),
                new XElement("ENGLISHNAME", this.EnglishName),
                new XElement("GENDER", this.Gender),
                new XElement("BIRTHDAY", this.Birthday.HasValue ? this.Birthday.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("TELEPHONE", this.TelePhone),
                new XElement("ADDRESS", this.Address),
                new XElement("ACCNO", this.AccNo),
                new XElement("APPLYDOCTOR", this.ApplyDoctor),
                new XElement("APPLYDT", this.ApplyDt.HasValue ? this.ApplyDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("MODALITYTYPE", this.ModalityType),
                new XElement("PROCEDURECODE", this.ProcedureCode),
                new XElement("CHECKINGITEM", this.CheckingItem),
                new XElement("HEALTHHISTORY", this.HealthHistory),
                new XElement("OBSERVATION", this.Observation),
                new XElement("REFPURPOSE", this.RefPurpose),
                new XElement("RPSTATUS", this.RPStatus),
                new XElement("REFSTATUS", this.RefStatus),
                new XElement("EXAMDOMAIN", this.ExamDomain),
                new XElement("EXAMACCNO", this.ExamAccNo),
                new XElement("CREATEDT", this.CreateDt.HasValue ? this.CreateDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("INITIALDOMAIN", this.InitialDomain),
                new XElement("SOURCEDOMAIN", this.SourceDomain),
                new XElement("TARGETDOMAIN", this.TargetDomain),
                new XElement("TARGETSITE", this.TargetSite),
                new XElement("SOURCESITE", this.SourceSite),
                new XElement("SCOPE", this.Scope),
                new XElement("DIRECTION", this.Direction),
                new XElement("ISEXISTSNAPSHOT", this.IsExistSnapshot),
                new XElement("GETREPORTDOMAIN", this.GetReportDomain),
                new XElement("BOOKINGBEGINDT", this.BookingBeginDt.HasValue ? this.BookingBeginDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("BOOKINGENDDT", this.BookingEndDt.HasValue ? this.BookingEndDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("PACKAGEDBIZDATA", this.PackagedBizData),
                new XElement("REFAPPLICATION", this.RefApplication),
                new XElement("REFREPORT", this.RefReport));

            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("REFERRALID");
            this.ReferralID = e == null ? "" : e.Value;

            e = element.Element("PATIENTID");
            this.PatientID = e == null ? "" : e.Value;

            e = element.Element("LOCALNAME");
            this.LocalName = e == null ? "" : e.Value;

            e = element.Element("ENGLISHNAME");
            this.EnglishName = e == null ? "" : e.Value;

            e = element.Element("GENDER");
            this.Gender = e == null ? "" : e.Value;

            e = element.Element("BIRTHDAY");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Birthday = null;
            else
                this.Birthday = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("TELEPHONE");
            this.TelePhone = e == null ? "" : e.Value;

            e = element.Element("ADDRESS");
            this.Address = e == null ? "" : e.Value;

            e = element.Element("ACCNO");
            this.AccNo = e == null ? "" : e.Value;

            e = element.Element("APPLYDOCTOR");
            this.ApplyDoctor = e == null ? "" : e.Value;

            e = element.Element("APPLYDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ApplyDt = null;
            else
                this.ApplyDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("MODALITYTYPE");
            this.ModalityType = e == null ? "" : e.Value;

            e = element.Element("PROCEDURECODE");
            this.ProcedureCode = e == null ? "" : e.Value;

            e = element.Element("CHECKINGITEM");
            this.CheckingItem = e == null ? "" : e.Value;

            e = element.Element("HEALTHHISTORY");
            this.HealthHistory = e == null ? "" : e.Value;

            e = element.Element("OBSERVATION");
            this.Observation = e == null ? "" : e.Value;

            e = element.Element("REFPURPOSE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.RefPurpose = 0;
            else
                this.RefPurpose = Convert.ToInt32(e.Value);

            e = element.Element("RPSTATUS");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.RPStatus = 0;
            else
                this.RPStatus = Convert.ToInt32(e.Value);

            e = element.Element("REFSTATUS");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.RefStatus = 0;
            else
                this.RefStatus = Convert.ToInt32(e.Value);

            e = element.Element("EXAMDOMAIN");
            this.ExamDomain = e == null ? "" : e.Value;

            e = element.Element("EXAMACCNO");
            this.ExamAccNo = e == null ? "" : e.Value;

            e = element.Element("CREATEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.CreateDt = null;
            else
                this.CreateDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("INITIALDOMAIN");
            this.InitialDomain = e == null ? "" : e.Value;

            e = element.Element("SOURCEDOMAIN");
            this.SourceDomain = e == null ? "" : e.Value;

            e = element.Element("TARGETDOMAIN");
            this.TargetDomain = e == null ? "" : e.Value;

            e = element.Element("TARGETSITE");
            this.TargetSite = e == null ? "" : e.Value;

            e = element.Element("SOURCESITE");
            this.SourceSite = e == null ? "" : e.Value;

            e = element.Element("TARGETSITE");
            this.TargetSite = e == null ? "" : e.Value;

            e = element.Element("DIRECTION");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Direction = 0;
            else
                this.Direction = Convert.ToInt32(e.Value);

            e = element.Element("ISEXISTSNAPSHOT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsExistSnapshot = 0;
            else
                this.IsExistSnapshot = Convert.ToInt32(e.Value);

            e = element.Element("GETREPORTDOMAIN");
            this.GetReportDomain = e == null ? "" : e.Value;

            e = element.Element("BOOKINGBEGINDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.BookingBeginDt = null;
            else
                this.BookingBeginDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("BOOKINGENDDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.BookingEndDt = null;
            else
                this.BookingEndDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("PACKAGEDBIZDATA");
            this.PackagedBizData = e == null ? "" : e.Value;

            e = element.Element("REFAPPLICATION");
            this.RefApplication = e == null ? "" : e.Value;

            e = element.Element("REFREPORT");
            this.RefReport = e == null ? "" : e.Value;
        }

        #endregion

    }


    [Serializable()]
    public class ReferralListModel : ReferralBaseModel
    {
        private List<ReferralModel> _referlist = null;

        [XmlArray()]
        public List<ReferralModel> ReferList
        {
            get { return _referlist; }
            set
            {
                _referlist = value;
            }
        }
    }

    [Serializable()]
    public class ReferralViewModel : ReferralModel
    {
        public string RefStatus_V
        {
            get;
            set;
        }

        public string RPStatus_V
        {
            get;
            set;
        }
        public string Direction_V
        {
            get;
            set;
        }
        public string RefPurpose_V
        {
            get;
            set;
        }

        public string SourceSite_V
        {
            get;
            set;
        }
        public string SourceDomain_V
        {
            get;
            set;
        }
        public string TargetSite_V
        {
            get;
            set;
        }
        public string TargetDomain_V
        {
            get;
            set;
        }

        //public string SourceDomain_T
        //{
        //    get;
        //    set;
        //}

        //public string TargetDomain_T
        //{
        //    get;
        //    set;
        //}
    }
}




