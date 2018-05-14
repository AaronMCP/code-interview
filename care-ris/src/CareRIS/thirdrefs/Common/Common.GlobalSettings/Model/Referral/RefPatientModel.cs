using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Xml.Linq;
using Common.Consts;
using System.Globalization;

namespace CommonGlobalSettings
{
    #region RefPatientModel

    /// <summary>
    /// RefPatientModel mapped table 'tRegPatient'
    /// </summary>
    [Serializable()]
    public class RefPatientModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _patientID = string.Empty;
        protected string _localName = string.Empty;
        protected string _englishName = string.Empty;
        protected string _referenceNo = string.Empty;
        protected DateTime? _birthday;
        protected string _gender = string.Empty;
        protected string _address = string.Empty;
        protected string _telephone = string.Empty;
        protected int _isVIP;
        protected DateTime? _createDt;
        protected string _comments = string.Empty;
        protected string _remotePID = string.Empty;
        protected string _optional1 = string.Empty;
        protected string _optional2 = string.Empty;
        protected string _optional3 = string.Empty;
        protected string _globalId = string.Empty;
        protected string _domain = string.Empty;
        //protected string _referralID = string.Empty;
        protected string _medicareNo = string.Empty;
        protected string _socialSecurityNo = string.Empty;
        protected Nullable<System.DateTime> _updateTime;
        protected int _uploaded = 0;
        #endregion

        #region Constructors

        public RefPatientModel() { }

        #endregion

        #region Public Properties

        [DataField("PatientGuid")]
        public string PatientGuid
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

        [DataField("ReferenceNo")]
        public string ReferenceNo
        {
            get { return _referenceNo; }
            set
            {
                _referenceNo = value;
            }
        }

        [DataField("Birthday")]
        public DateTime? Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
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

        [DataField("Address")]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }

        [DataField("Telephone")]
        public string Telephone
        {
            get { return _telephone; }
            set
            {
                _telephone = value;
            }
        }

        [DataField("IsVIP")]
        public int IsVIP
        {
            get { return _isVIP; }
            set { _isVIP = value; }
        }

        [DataField("CreateDt")]
        public DateTime? CreateDt
        {
            get { return _createDt; }
            set { _createDt = value; }
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

        [DataField("RemotePID")]
        public string RemotePID
        {
            get { return _remotePID; }
            set
            {
                _remotePID = value;
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

        [DataField("GlobalId")]
        public string GlobalId
        {
            get { return _globalId; }
            set
            {
                _globalId = value;
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

        //[DataField("ReferralID")]
        //public string ReferralID
        //{
        //    get { return _referralID; }
        //    set
        //    {
        //        _referralID = value;
        //    }
        //}

        [DataField("MedicareNo")]
        public string MedicareNo
        {
            get { return _medicareNo; }
            set
            {
                _medicareNo = value;
            }
        }

        [DataField("SocialSecurityNo")]
        public string SocialSecurityNo
        {
            get { return _socialSecurityNo; }
            set { _socialSecurityNo = value; }
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

        #endregion

        #region Public Methods

        public XElement SerializeXML()
        {
            XElement element = new XElement("Patient",
                                new XElement("PATIENTGUID", this.PatientGuid),
                                new XElement("PATIENTID", this.PatientID),
                                new XElement("LOCALNAME", this.LocalName),
                                new XElement("ENGLISHNAME", this.EnglishName),
                                new XElement("REFERENCENO", this.ReferenceNo),
                                new XElement("BIRTHDAY", this.Birthday.HasValue ? this.Birthday.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                                new XElement("GENDER", this.Gender),
                                new XElement("ADDRESS", this.Address),
                                new XElement("TELEPHONE", this.Telephone),
                                new XElement("ISVIP", this.IsVIP),
                                new XElement("CREATEDT", this.CreateDt.HasValue ? this.CreateDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                                new XElement("COMMENTS", this.Comments),
                                new XElement("REMOTEPID", this.RemotePID),
                                new XElement("OPTIONAL1", this.Optional1),
                                new XElement("OPTIONAL2", this.Optional2),
                                new XElement("OPTIONAL3", this.Optional3),
                                new XElement("GLOBALID", this.GlobalId),
                                new XElement("DOMAIN", this.Domain),
                                new XElement("MEDICARENO", this.MedicareNo),
                                new XElement("SOCIALSECURITYNO", this.SocialSecurityNo),
                                new XElement("UPDATETIME", this.UpdateTime.HasValue ? this.UpdateTime.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
                                new XElement("UPLOADED", this.Uploaded));
            return element;
        }


        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("PATIENTGUID");
            this.PatientGuid = e == null ? "" : e.Value;

            e = element.Element("PATIENTID");
            this.PatientID = e == null ? "" : e.Value;

            e = element.Element("LOCALNAME");
            this.LocalName = e == null ? "" : e.Value;

            e = element.Element("ENGLISHNAME");
            this.EnglishName = e == null ? "" : e.Value;

            e = element.Element("REFERENCENO");
            this.ReferenceNo = e == null ? "" : e.Value;

            e = element.Element("BIRTHDAY");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Birthday = null;
            else
                this.Birthday = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("GENDER");
            this.Gender = e == null ? "" : e.Value;

            e = element.Element("ADDRESS");
            this.Address = e == null ? "" : e.Value;

            e = element.Element("TELEPHONE");
            this.Telephone = e == null ? "" : e.Value;

            e = element.Element("ISVIP");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsVIP = 0;
            else
                this.IsVIP = Convert.ToInt32(e.Value);

            e = element.Element("CREATEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.CreateDt = null;
            else
                this.CreateDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("COMMENTS");
            this.Comments = e == null ? "" : e.Value;

            e = element.Element("REMOTEPID");
            this.RemotePID = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL1");
            this.Optional1 = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL2");
            this.Optional2 = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL3");
            this.Optional3 = e == null ? "" : e.Value;

            e = element.Element("GLOBALID");
            this.GlobalId = e == null ? "" : e.Value;

            e = element.Element("DOMAIN");
            this.Domain = e == null ? "" : e.Value;

            e = element.Element("MEDICARENO");
            this.MedicareNo = e == null ? "" : e.Value;

            e = element.Element("SOCIALSECURITYNO");
            this.SocialSecurityNo = e == null ? "" : e.Value;

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

        }


        #endregion


    }
    #endregion
}
