using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Xml.Linq;
using Common.Consts;
using System.Globalization;

namespace CommonGlobalSettings
{
    /// <summary>
    /// Referral Order model mapped table 'tRegOrder'
    /// </summary>
    [Serializable()]
    public class RefOrderModel : ReferralBaseModel
    {
        #region Member Variables
        protected string _orderGuid = string.Empty;
        protected string _visitGuid = string.Empty;
        protected string _accNo = string.Empty;
        protected string _applyDept = string.Empty;
        protected string _applyDoctor = string.Empty;
        protected Nullable<System.DateTime> _createDt;
        protected int _isScan;
        protected string _comments = string.Empty;
        protected string _remoteAccNo = string.Empty;
        protected decimal _totalFee;
        protected string _optional1 = string.Empty;
        protected string _optional2 = string.Empty;
        protected string _optional3 = string.Empty;
        protected string _studyInstanceUID = string.Empty;
        protected string _hisID = string.Empty;
        protected string _cardNo = string.Empty;
        protected string _patientGuid = string.Empty;
        protected string _inhospitalNo = string.Empty;
        protected string _clinicNo = string.Empty;
        protected string _patientType = string.Empty;
        protected string _observation = string.Empty;
        protected string _healthHistory = string.Empty;
        protected string _inhospitalRegion = string.Empty;
        protected int _isEmergency;
        protected string _bedNo = string.Empty;
        protected string _currentAge = string.Empty;
        protected int _ageInDays;
        protected string _visitcomment = string.Empty;
        protected string _chargeType = string.Empty;
        protected string _erethismType = string.Empty;
        protected string _erethismCode = string.Empty;
        protected string _erethismGrade = string.Empty;
        protected string _domain = string.Empty;
        protected string _referralID = string.Empty;
        protected int _isReferral;
        protected string _examAccNo = string.Empty;
        protected string _examDomain = string.Empty;
        protected string _medicalAlert = string.Empty;
        protected string _eXAMALERT1 = string.Empty;
        protected string _eXAMALERT2 = string.Empty;
        protected string _lMP = string.Empty;
        protected string _initialDomain = string.Empty;
        protected string _eRequisition = string.Empty;
        protected string _curPatientName = string.Empty;
        protected string _curGender = string.Empty;
        protected int _priority;
        protected int _isCharge;
        protected int _bedside;
        protected int _isFilmSent;
        protected string _filmSentOperator = string.Empty;
        protected Nullable<System.DateTime> _filmSentDt;
        protected string _orderMessage = string.Empty;
        protected string _bookingSite = string.Empty;
        protected string _regSite = string.Empty;
        protected string _examSite = string.Empty;
        protected decimal _bodyWeight;
        protected int _filmFee;
        protected int _threeDRebuild;
        protected string _currentSite = string.Empty;
        protected Nullable<System.DateTime> _assignDt;
        protected string _assign2Site = string.Empty;
        protected string _studyID = string.Empty;
        protected string _pathologicalFindings = string.Empty;
        protected string _internalOptional1 = string.Empty;
        protected string _internalOptional2 = string.Empty;
        protected string _externalOptional1 = string.Empty;
        protected string _externalOptional2 = string.Empty;
        protected string _externalOptional3 = string.Empty;
        protected Nullable<System.DateTime> _updateTime;
        protected int _uploaded;

        protected List<RefProcedureModel> _tRefProcedures = new List<RefProcedureModel>();
        #endregion

        #region Primitive Properties

        [DataField("OrderGuid")]
        public string OrderGuid
        {
            get { return _orderGuid; }
            set { _orderGuid = value; }
        }

        [DataField("VisitGuid")]
        public string VisitGuid
        {
            get { return _visitGuid; }
            set { _visitGuid = value; }
        }

        [DataField("AccNo")]
        public string AccNo
        {
            get { return _accNo; }
            set { _accNo = value; }
        }

        [DataField("ApplyDept")]
        public string ApplyDept
        {
            get { return _applyDept; }
            set { _applyDept = value; }
        }

        [DataField("ApplyDoctor")]
        public string ApplyDoctor
        {
            get { return _applyDoctor; }
            set { _applyDoctor = value; }
        }

        [DataField("CreateDt")]
        public Nullable<System.DateTime> CreateDt
        {
            get { return _createDt; }
            set { _createDt = value; }
        }

        [DataField("IsScan")]
        public int IsScan
        {
            get { return _isScan; }
            set { _isScan = value; }
        }

        [DataField("Comments")]
        public string Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }

        [DataField("RemoteAccNo")]
        public string RemoteAccNo
        {
            get { return _remoteAccNo; }
            set { _remoteAccNo = value; }
        }

        [DataField("TotalFee")]
        public decimal TotalFee
        {
            get { return _totalFee; }
            set { _totalFee = value; }
        }

        [DataField("Optional1")]
        public string Optional1
        {
            get { return _optional1; }
            set { _optional1 = value; }
        }

        [DataField("Optional2")]
        public string Optional2
        {
            get { return _optional2; }
            set { _optional2 = value; }
        }

        [DataField("Optional3")]
        public string Optional3
        {
            get { return _optional3; }
            set { _optional3 = value; }
        }

        [DataField("StudyInstanceUID")]
        public string StudyInstanceUID
        {
            get { return _studyInstanceUID; }
            set { _studyInstanceUID = value; }
        }

        [DataField("HisID")]
        public string HisID
        {
            get { return _hisID; }
            set { _hisID = value; }
        }

        [DataField("CardNo")]
        public string CardNo
        {
            get { return _cardNo; }
            set { _cardNo = value; }
        }

        [DataField("PatientGuid")]
        public string PatientGuid
        {
            get { return _patientGuid; }
            set { _patientGuid = value; }
        }

        [DataField("InhospitalNo")]
        public string InhospitalNo
        {
            get { return _inhospitalNo; }
            set { _inhospitalNo = value; }
        }

        [DataField("ClinicNo")]
        public string ClinicNo
        {
            get { return _clinicNo; }
            set { _clinicNo = value; }
        }

        [DataField("PatientType")]
        public string PatientType
        {
            get { return _patientType; }
            set { _patientType = value; }
        }

        [DataField("Observation")]
        public string Observation
        {
            get { return _observation; }
            set { _observation = value; }
        }

        [DataField("HealthHistory")]
        public string HealthHistory
        {
            get { return _healthHistory; }
            set { _healthHistory = value; }
        }

        [DataField("InhospitalRegion")]
        public string InhospitalRegion
        {
            get { return _inhospitalRegion; }
            set { _inhospitalRegion = value; }
        }

        [DataField("IsEmergency")]
        public int IsEmergency
        {
            get { return _isEmergency; }
            set { _isEmergency = value; }
        }

        [DataField("BedNo")]
        public string BedNo
        {
            get { return _bedNo; }
            set { _bedNo = value; }
        }

        [DataField("CurrentAge")]
        public string CurrentAge
        {
            get { return _currentAge; }
            set { _currentAge = value; }
        }

        [DataField("AgeInDays")]
        public int AgeInDays
        {
            get { return _ageInDays; }
            set { _ageInDays = value; }
        }

        [DataField("Visitcomment")]
        public string Visitcomment
        {
            get { return _visitcomment; }
            set { _visitcomment = value; }
        }

        [DataField("ChargeType")]
        public string ChargeType
        {
            get { return _chargeType; }
            set { _chargeType = value; }
        }

        [DataField("ErethismType")]
        public string ErethismType
        {
            get { return _erethismType; }
            set { _erethismType = value; }
        }

        [DataField("ErethismCode")]
        public string ErethismCode
        {
            get { return _erethismCode; }
            set { _erethismCode = value; }
        }

        [DataField("ErethismGrade")]
        public string ErethismGrade
        {
            get { return _erethismGrade; }
            set { _erethismGrade = value; }
        }

        [DataField("Domain")]
        public string Domain
        {
            get { return _domain; }
            set { _domain = value; }
        }

        [DataField("ReferralID")]
        public string ReferralID
        {
            get { return _referralID; }
            set { _referralID = value; }
        }

        [DataField("IsReferral")]
        public int IsReferral
        {
            get { return _isReferral; }
            set { _isReferral = value; }
        }

        [DataField("ExamAccNo")]
        public string ExamAccNo
        {
            get { return _examAccNo; }
            set { _examAccNo = value; }
        }

        [DataField("ExamDomain")]
        public string ExamDomain
        {
            get { return _examDomain; }
            set { _examDomain = value; }
        }

        [DataField("MedicalAlert")]
        public string MedicalAlert
        {
            get { return _medicalAlert; }
            set { _medicalAlert = value; }
        }

        [DataField("EXAMALERT1")]
        public string EXAMALERT1
        {
            get { return _eXAMALERT1; }
            set { _eXAMALERT1 = value; }
        }

        [DataField("EXAMALERT2")]
        public string EXAMALERT2
        {
            get { return _eXAMALERT2; }
            set { _eXAMALERT2 = value; }
        }

        [DataField("LMP")]
        public string LMP
        {
            get { return _lMP; }
            set { _lMP = value; }
        }

        [DataField("InitialDomain")]
        public string InitialDomain
        {
            get { return _initialDomain; }
            set { _initialDomain = value; }
        }

        [DataField("ERequisition")]
        public string ERequisition
        {
            get { return _eRequisition; }
            set { _eRequisition = value; }
        }

        [DataField("CurPatientName")]
        public string CurPatientName
        {
            get { return _curPatientName; }
            set { _curPatientName = value; }
        }

        [DataField("CurGender")]
        public string CurGender
        {
            get { return _curGender; }
            set { _curGender = value; }
        }

        [DataField("Priority")]
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        [DataField("IsCharge")]
        public int IsCharge
        {
            get { return _isCharge; }
            set { _isCharge = value; }
        }

        [DataField("Bedside")]
        public int Bedside
        {
            get { return _bedside; }
            set { _bedside = value; }
        }

        [DataField("IsFilmSent")]
        public int IsFilmSent
        {
            get { return _isFilmSent; }
            set { _isFilmSent = value; }
        }

        [DataField("FilmSentOperator")]
        public string FilmSentOperator
        {
            get { return _filmSentOperator; }
            set { _filmSentOperator = value; }
        }

        [DataField("FilmSentDt")]
        public Nullable<System.DateTime> FilmSentDt
        {
            get { return _filmSentDt; }
            set { _filmSentDt = value; }
        }

        [DataField("OrderMessage")]
        public string OrderMessage
        {
            get { return _orderMessage; }
            set { _orderMessage = value; }
        }

        [DataField("BookingSite")]
        public string BookingSite
        {
            get { return _bookingSite; }
            set { _bookingSite = value; }
        }

        [DataField("RegSite")]
        public string RegSite
        {
            get { return _regSite; }
            set { _regSite = value; }
        }

        [DataField("ExamSite")]
        public string ExamSite
        {
            get { return _examSite; }
            set { _examSite = value; }
        }

        [DataField("BodyWeight")]
        public decimal BodyWeight
        {
            get { return _bodyWeight; }
            set { _bodyWeight = value; }
        }

        [DataField("FilmFee")]
        public int FilmFee
        {
            get { return _filmFee; }
            set { _filmFee = value; }
        }

        [DataField("ThreeDRebuild")]
        public int ThreeDRebuild
        {
            get { return _threeDRebuild; }
            set { _threeDRebuild = value; }
        }

        [DataField("CurrentSite")]
        public string CurrentSite
        {
            get { return _currentSite; }
            set { _currentSite = value; }
        }

        [DataField("AssignDt")]
        public Nullable<System.DateTime> AssignDt
        {
            get { return _assignDt; }
            set { _assignDt = value; }
        }

        [DataField("Assign2Site")]
        public string Assign2Site
        {
            get { return _assign2Site; }
            set { _assign2Site = value; }
        }

        [DataField("StudyID")]
        public string StudyID
        {
            get { return _studyID; }
            set { _studyID = value; }
        }

        [DataField("PathologicalFindings")]
        public string PathologicalFindings
        {
            get { return _pathologicalFindings; }
            set { _pathologicalFindings = value; }
        }

        [DataField("InternalOptional1")]
        public string InternalOptional1
        {
            get { return _internalOptional1; }
            set { _internalOptional1 = value; }
        }

        [DataField("InternalOptional2")]
        public string InternalOptional2
        {
            get { return _internalOptional2; }
            set { _internalOptional2 = value; }
        }

        [DataField("ExternalOptional1")]
        public string ExternalOptional1
        {
            get { return _externalOptional1; }
            set { _externalOptional1 = value; }
        }

        [DataField("ExternalOptional2")]
        public string ExternalOptional2
        {
            get { return _externalOptional2; }
            set { _externalOptional2 = value; }
        }

        [DataField("ExternalOptional3")]
        public string ExternalOptional3
        {
            get { return _externalOptional3; }
            set { _externalOptional3 = value; }
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

        public List<RefProcedureModel> RefProcedures
        {
            get
            {
                return _tRefProcedures;
            }
            set { _tRefProcedures = value; }
        }
        #endregion

        #region Public Methods

        public XElement SerializeXML()
        {
            XElement element = new XElement("Order",
                new XElement("ORDERGUID", this.OrderGuid),
                new XElement("VISITGUID", this.VisitGuid),
                new XElement("ACCNO", this.AccNo),
                new XElement("APPLYDEPT", this.ApplyDept),
                new XElement("APPLYDOCTOR", this.ApplyDoctor),
                new XElement("CREATEDT", this.CreateDt.HasValue ? this.CreateDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("ISSCAN", this.IsScan),
                new XElement("COMMENTS", this.Comments),
                new XElement("REMOTEACCNO", this.RemoteAccNo),
                new XElement("TOTALFEE", this.TotalFee),
                new XElement("OPTIONAL1", this.Optional1),
                new XElement("OPTIONAL2", this.Optional2),
                new XElement("OPTIONAL3", this.Optional3),
                new XElement("STUDYINSTANCEUID", this.StudyInstanceUID),
                new XElement("HISID", this.HisID),
                new XElement("CARDNO", this.CardNo),
                new XElement("PATIENTGUID", this.PatientGuid),
                new XElement("INHOSPITALNO", this.InhospitalNo),
                new XElement("CLINICNO", this.ClinicNo),
                new XElement("PATIENTTYPE", this.PatientType),
                new XElement("OBSERVATION", this.Observation),
                new XElement("HEALTHHISTORY", this.HealthHistory),
                new XElement("INHOSPITALREGION", this.InhospitalRegion),
                new XElement("ISEMERGENCY", this.IsEmergency),
                new XElement("BEDNO", this.BedNo),
                new XElement("CURRENTAGE", this.CurrentAge),
                new XElement("AGEINDAYS", this.AgeInDays),
                new XElement("VISITCOMMENT", this.Visitcomment),
                new XElement("CHARGETYPE", this.ChargeType),
                new XElement("ERETHISMTYPE", this.ErethismType),
                new XElement("ERETHISMCODE", this.ErethismCode),
                new XElement("ERETHISMGRADE", this.ErethismGrade),
                new XElement("DOMAIN", this.Domain),
                new XElement("REFERRALID", this.ReferralID),
                new XElement("ISREFERRAL", this.IsReferral),
                new XElement("EXAMACCNO", this.ExamAccNo),
                new XElement("EXAMDOMAIN", this.ExamDomain),
                new XElement("MEDICALALERT", this.MedicalAlert),
                new XElement("EXAMALERT1", this.EXAMALERT1),
                new XElement("EXAMALERT2", this.EXAMALERT2),
                new XElement("LMP", this.LMP),
                new XElement("INITIALDOMAIN", this.InitialDomain),
                new XElement("EREQUISITION", this.ERequisition),
                new XElement("CURPATIENTNAME", this.CurPatientName),
                new XElement("CURGENDER", this.CurGender),
                new XElement("PRIORITY", this.Priority),
                new XElement("ISCHARGE", this.IsCharge),
                new XElement("BEDSIDE", this.Bedside),
                new XElement("ISFILMSENT", this.IsFilmSent),
                new XElement("FILMSENTOPERATOR", this.FilmSentOperator),
                new XElement("FILMSENTDT", this.FilmSentDt.HasValue ? this.FilmSentDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("ORDERMESSAGE", this.OrderMessage),
                new XElement("BOOKINGSITE", this.BookingSite),
                new XElement("REGSITE", this.RegSite),
                new XElement("EXAMSITE", this.ExamSite),
                new XElement("BODYWEIGHT", this.BodyWeight),
                new XElement("FILMFEE", this.FilmFee),
                new XElement("THREEDREBUILD", this.ThreeDRebuild),
                new XElement("CURRENTSITE", this.CurrentSite),
                new XElement("ASSIGNDT", this.AssignDt.HasValue ? this.AssignDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("ASSIGN2SITE", this.Assign2Site),
                new XElement("STUDYID", this.StudyID),
                new XElement("PATHOLOGICALFINDINGS", this.PathologicalFindings),
                new XElement("INTERNALOPTIONAL1", this.InternalOptional1),
                new XElement("INTERNALOPTIONAL2", this.InternalOptional2),
                new XElement("EXTERNALOPTIONAL1", this.ExternalOptional1),
                new XElement("EXTERNALOPTIONAL2", this.ExternalOptional2),
                new XElement("EXTERNALOPTIONAL3", this.ExternalOptional3),
                new XElement("UPDATETIME", this.UpdateTime.HasValue ? this.UpdateTime.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                new XElement("UPLOADED", this.Uploaded));

            foreach (var procedure in _tRefProcedures)
            {
                element.Add(procedure.SerializeXML());
            }

            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("ORDERGUID");
            this.OrderGuid = e == null ? "" : e.Value;

            e = element.Element("VISITGUID");
            this.VisitGuid = e == null ? "" : e.Value;

            e = element.Element("ACCNO");
            this.AccNo = e == null ? "" : e.Value;

            e = element.Element("APPLYDEPT");
            this.ApplyDept = e == null ? "" : e.Value;

            e = element.Element("APPLYDOCTOR");
            this.ApplyDoctor = e == null ? "" : e.Value;

            e = element.Element("CREATEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.CreateDt = null;
            else
                this.CreateDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("ISSCAN");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsScan = 0;
            else
                this.IsScan = Convert.ToInt32(e.Value);

            e = element.Element("COMMENTS");
            this.Comments = e == null ? "" : e.Value;

            e = element.Element("REMOTEACCNO");
            this.RemoteAccNo = e == null ? "" : e.Value;

            e = element.Element("TOTALFEE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.TotalFee = 0;
            else
                this.TotalFee = Convert.ToDecimal(e.Value);
            e = element.Element("OPTIONAL1");
            this.Optional1 = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL2");
            this.Optional2 = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL3");
            this.Optional3 = e == null ? "" : e.Value;

            e = element.Element("STUDYINSTANCEUID");
            this.StudyInstanceUID = e == null ? "" : e.Value;

            e = element.Element("HISID");
            this.HisID = e == null ? "" : e.Value;

            e = element.Element("CARDNO");
            this.CardNo = e == null ? "" : e.Value;

            e = element.Element("PATIENTGUID");
            this.PatientGuid = e == null ? "" : e.Value;

            e = element.Element("INHOSPITALNO");
            this.InhospitalNo = e == null ? "" : e.Value;

            e = element.Element("CLINICNO");
            this.ClinicNo = e == null ? "" : e.Value;

            e = element.Element("PATIENTTYPE");
            this.PatientType = e == null ? "" : e.Value;

            e = element.Element("OBSERVATION");
            this.Observation = e == null ? "" : e.Value;

            e = element.Element("HEALTHHISTORY");
            this.HealthHistory = e == null ? "" : e.Value;

            e = element.Element("INHOSPITALREGION");
            this.InhospitalRegion = e == null ? "" : e.Value;

            e = element.Element("ISEMERGENCY");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsEmergency = 0;
            else
                this.IsEmergency = Convert.ToInt32(e.Value);

            e = element.Element("BEDNO");
            this.BedNo = e == null ? "" : e.Value;

            e = element.Element("CURRENTAGE");
            this.CurrentAge = e == null ? "" : e.Value;

            e = element.Element("AGEINDAYS");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.AgeInDays = 0;
            else
                this.AgeInDays = Convert.ToInt32(e.Value);

            e = element.Element("VISITCOMMENT");
            this.Visitcomment = e == null ? "" : e.Value;

            e = element.Element("CHARGETYPE");
            this.ChargeType = e == null ? "" : e.Value;

            e = element.Element("ERETHISMTYPE");
            this.ErethismType = e == null ? "" : e.Value;

            e = element.Element("ERETHISMCODE");
            this.ErethismCode = e == null ? "" : e.Value;

            e = element.Element("ERETHISMGRADE");
            this.ErethismGrade = e == null ? "" : e.Value;

            e = element.Element("DOMAIN");
            this.Domain = e == null ? "" : e.Value;

            e = element.Element("REFERRALID");
            this.ReferralID = e == null ? "" : e.Value;

            e = element.Element("ISREFERRAL");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsReferral = 0;
            else
                this.IsReferral = Convert.ToInt32(e.Value);

            e = element.Element("EXAMACCNO");
            this.ExamAccNo = e == null ? "" : e.Value;

            e = element.Element("EXAMDOMAIN");
            this.ExamDomain = e == null ? "" : e.Value;

            e = element.Element("MEDICALALERT");
            this.MedicalAlert = e == null ? "" : e.Value;

            e = element.Element("EXAMALERT1");
            this.EXAMALERT1 = e == null ? "" : e.Value;

            e = element.Element("EXAMALERT2");
            this.EXAMALERT2 = e == null ? "" : e.Value;

            e = element.Element("LMP");
            this.LMP = e == null ? "" : e.Value;

            e = element.Element("INITIALDOMAIN");
            this.InitialDomain = e == null ? "" : e.Value;

            e = element.Element("EREQUISITION");
            this.ERequisition = e == null ? "" : e.Value;

            e = element.Element("CURPATIENTNAME");
            this.CurPatientName = e == null ? "" : e.Value;

            e = element.Element("CURGENDER");
            this.CurGender = e == null ? "" : e.Value;

            e = element.Element("PRIORITY");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Priority = 0;
            else
                this.Priority = Convert.ToInt32(e.Value);

            e = element.Element("ISCHARGE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsCharge = 0;
            else
                this.IsCharge = Convert.ToInt32(e.Value);

            e = element.Element("BEDSIDE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Bedside = 0;
            else
                this.Bedside = Convert.ToInt32(e.Value);

            e = element.Element("ISFILMSENT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsFilmSent = 0;
            else
                this.IsFilmSent = Convert.ToInt32(e.Value);

            e = element.Element("FILMSENTOPERATOR");
            this.FilmSentOperator = e == null ? "" : e.Value;

            e = element.Element("FILMSENTDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.FilmSentDt = null;
            else
                this.FilmSentDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("ORDERMESSAGE");
            this.OrderMessage = e == null ? "" : e.Value;

            e = element.Element("BOOKINGSITE");
            this.BookingSite = e == null ? "" : e.Value;

            e = element.Element("REGSITE");
            this.RegSite = e == null ? "" : e.Value;

            e = element.Element("EXAMSITE");
            this.ExamSite = e == null ? "" : e.Value;

            e = element.Element("BODYWEIGHT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.BodyWeight = 0;
            else
                this.BodyWeight = Convert.ToDecimal(e.Value);
            e = element.Element("FILMFEE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.FilmFee = 0;
            else
                this.FilmFee = Convert.ToInt32(e.Value);

            e = element.Element("THREEDREBUILD");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ThreeDRebuild = 0;
            else
                this.ThreeDRebuild = Convert.ToInt32(e.Value);

            e = element.Element("CURRENTSITE");
            this.CurrentSite = e == null ? "" : e.Value;

            e = element.Element("ASSIGNDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.AssignDt = null;
            else
                this.AssignDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("ASSIGN2SITE");
            this.Assign2Site = e == null ? "" : e.Value;

            e = element.Element("STUDYID");
            this.StudyID = e == null ? "" : e.Value;

            e = element.Element("PATHOLOGICALFINDINGS");
            this.PathologicalFindings = e == null ? "" : e.Value;

            e = element.Element("INTERNALOPTIONAL1");
            this.InternalOptional1 = e == null ? "" : e.Value;

            e = element.Element("INTERNALOPTIONAL2");
            this.InternalOptional2 = e == null ? "" : e.Value;

            e = element.Element("EXTERNALOPTIONAL1");
            this.ExternalOptional1 = e == null ? "" : e.Value;

            e = element.Element("EXTERNALOPTIONAL2");
            this.ExternalOptional2 = e == null ? "" : e.Value;

            e = element.Element("EXTERNALOPTIONAL3");
            this.ExternalOptional3 = e == null ? "" : e.Value;

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

            var rps = element.Elements("Procedure");

            foreach (var rp in rps)
            {
                RefProcedureModel procedure = new RefProcedureModel();
                procedure.DeSerializeXML(rp);
                _tRefProcedures.Add(procedure);
            }

        }

        #endregion


    }
}