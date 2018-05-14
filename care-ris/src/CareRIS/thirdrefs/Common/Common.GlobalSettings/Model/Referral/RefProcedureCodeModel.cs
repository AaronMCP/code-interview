using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Xml.Linq;

namespace CommonGlobalSettings
{
    #region RefProcedureCode

    /// <summary>
    /// RefProcedureCode object mapped table 'tProcedureCode'.
    /// </summary>
    public class RefProcedureCodeModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _description = string.Empty;
        protected string _englishDescription = string.Empty;
        protected string _modalityType = string.Empty;
        protected string _bodyPart = string.Empty;
        protected string _checkingItem = string.Empty;
        protected decimal _charge;
        protected string _preparation = string.Empty;
        protected int _frequency;
        protected string _bodyCategory = string.Empty;
        protected int _duration;
        protected string _filmSpec = string.Empty;
        protected int _filmCount;
        protected string _contrastName = string.Empty;
        protected string _contrastDose = string.Empty;
        protected int _imageCount;
        protected int _exposalCount;
        protected string _bookingNotice = string.Empty;
        protected string _shortcutCode = string.Empty;
        protected int _enhance;
        protected int _approveWarningTime;
        protected int _effective;
        protected string _domain = string.Empty;
        //protected string _referralID = string.Empty;
        protected int _externals;
        protected string _site = string.Empty;

        #endregion

        #region Constructors

        public RefProcedureCodeModel() { }

        #endregion

        #region Public Properties

        [DataField("ProcedureCode")]
        public string ProcedureCode
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 128)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
                _id = value;
            }
        }

        [DataField("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
            }
        }

        [DataField("EnglishDescription")]
        public string EnglishDescription
        {
            get { return _englishDescription; }
            set
            {
                _englishDescription = value;
            }
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

        [DataField("BodyPart")]
        public string BodyPart
        {
            get { return _bodyPart; }
            set
            {
                _bodyPart = value;
            }
        }

        [DataField("CheckingItem")]
        public string CheckingItem
        {
            get { return _checkingItem; }
            set
            {
                _checkingItem = value;
            }
        }

        [DataField("Charge")]
        public decimal Charge
        {
            get { return _charge; }
            set { _charge = value; }
        }

        [DataField("Preparation")]
        public string Preparation
        {
            get { return _preparation; }
            set
            {
                _preparation = value;
            }
        }

        [DataField("Frequency")]
        public int Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        [DataField("BodyCategory")]
        public string BodyCategory
        {
            get { return _bodyCategory; }
            set
            {
                _bodyCategory = value;
            }
        }

        [DataField("Duration")]
        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        [DataField("FilmSpec")]
        public string FilmSpec
        {
            get { return _filmSpec; }
            set
            {
                _filmSpec = value;
            }
        }

        [DataField("FilmCount")]
        public int FilmCount
        {
            get { return _filmCount; }
            set { _filmCount = value; }
        }

        [DataField("ContrastName")]
        public string ContrastName
        {
            get { return _contrastName; }
            set
            {
                _contrastName = value;
            }
        }

        [DataField("ContrastDose")]
        public string ContrastDose
        {
            get { return _contrastDose; }
            set
            {
                _contrastDose = value;
            }
        }

        [DataField("ImageCount")]
        public int ImageCount
        {
            get { return _imageCount; }
            set { _imageCount = value; }
        }

        [DataField("ExposalCount")]
        public int ExposalCount
        {
            get { return _exposalCount; }
            set { _exposalCount = value; }
        }

        [DataField("BookingNotice")]
        public string BookingNotice
        {
            get { return _bookingNotice; }
            set
            {
                _bookingNotice = value;
            }
        }

        [DataField("ShortcutCode")]
        public string ShortcutCode
        {
            get { return _shortcutCode; }
            set
            {
                _shortcutCode = value;
            }
        }

        [DataField("Enhance")]
        public int Enhance
        {
            get { return _enhance; }
            set { _enhance = value; }
        }

        [DataField("ApproveWarningTime")]
        public int ApproveWarningTime
        {
            get { return _approveWarningTime; }
            set { _approveWarningTime = value; }
        }

        [DataField("Effective")]
        public int Effective
        {
            get { return _effective; }
            set { _effective = value; }
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

        [DataField("Externals")]
        public int Externals
        {
            get { return _externals; }
            set { _externals = value; }
        }

        [DataField("Site")]
        public string Site
        {
            get {return _site;}
            set { _site = value; }
        }

        #endregion

        #region Public methods

        public XElement SerializeXML()
        {
            XElement element = new XElement("ProcedureCode",
            new XElement("PROCEDURECODE", this.ProcedureCode),
            new XElement("DESCRIPTION", this.Description),
            new XElement("ENGLISHDESCRIPTION", this.EnglishDescription),
            new XElement("MODALITYTYPE", this.ModalityType),
            new XElement("BODYPART", this.BodyPart),
            new XElement("CHECKINGITEM", this.CheckingItem),
            new XElement("CHARGE", this.Charge),
            new XElement("PREPARATION", this.Preparation),
            new XElement("FREQUENCY", this.Frequency),
            new XElement("BODYCATEGORY", this.BodyCategory),
            new XElement("DURATION", this.Duration),
            new XElement("FILMSPEC", this.FilmSpec),
            new XElement("FILMCOUNT", this.FilmCount),
            new XElement("CONTRASTNAME", this.ContrastName),
            new XElement("CONTRASTDOSE", this.ContrastDose),
            new XElement("IMAGECOUNT", this.ImageCount),
            new XElement("EXPOSALCOUNT", this.ExposalCount),
            new XElement("BOOKINGNOTICE", this.BookingNotice),
            new XElement("SHORTCUTCODE", this.ShortcutCode),
            new XElement("ENHANCE", this.Enhance),
            new XElement("APPROVEWARNINGTIME", this.ApproveWarningTime),
            new XElement("EFFECTIVE", this.Effective),
            new XElement("DOMAIN", this.Domain),
            new XElement("SITE", this.Site));
            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("PROCEDURECODE");
            this.ProcedureCode = e == null ? "" : e.Value;

            e = element.Element("DESCRIPTION");
            this.Description = e == null ? "" : e.Value;

            e = element.Element("ENGLISHDESCRIPTION");
            this.EnglishDescription = e == null ? "" : e.Value;

            e = element.Element("MODALITYTYPE");
            this.ModalityType = e == null ? "" : e.Value;

            e = element.Element("BODYPART");
            this.BodyPart = e == null ? "" : e.Value;

            e = element.Element("CHECKINGITEM");
            this.CheckingItem = e == null ? "" : e.Value;

            e = element.Element("CHARGE");
            this.Charge = e == null ? 0 : Convert.ToDecimal(e.Value); ;

            e = element.Element("PREPARATION");
            this.Preparation = e == null ? "" : e.Value;

            e = element.Element("FREQUENCY");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Frequency = 0;
            else
                this.Frequency = Convert.ToInt32(e.Value);

            e = element.Element("BODYCATEGORY");
            this.BodyCategory = e == null ? "" : e.Value;

            e = element.Element("DURATION");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Duration = 0;
            else
                this.Duration = Convert.ToInt32(e.Value);

            e = element.Element("FILMSPEC");
            this.FilmSpec = e == null ? "" : e.Value;

            e = element.Element("FILMCOUNT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.FilmCount = 0;
            else
                this.FilmCount = Convert.ToInt32(e.Value);

            e = element.Element("CONTRASTNAME");
            this.ContrastName = e == null ? "" : e.Value;

            e = element.Element("CONTRASTDOSE");
            this.ContrastDose = e == null ? "" : e.Value;

            e = element.Element("IMAGECOUNT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ImageCount = 0;
            else
                this.ImageCount = Convert.ToInt32(e.Value);

            e = element.Element("EXPOSALCOUNT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ExposalCount = 0;
            else
                this.ExposalCount = Convert.ToInt32(e.Value);

            e = element.Element("BOOKINGNOTICE");
            this.BookingNotice = e == null ? "" : e.Value;

            e = element.Element("SHORTCUTCODE");
            this.ShortcutCode = e == null ? "" : e.Value;

            e = element.Element("ENHANCE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Enhance = 0;
            else
                this.Enhance = Convert.ToInt32(e.Value);

            e = element.Element("APPROVEWARNINGTIME");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ApproveWarningTime = 0;
            else
                this.ApproveWarningTime = Convert.ToInt32(e.Value);

            e = element.Element("EFFECTIVE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Effective = 0;
            else
                this.Effective = Convert.ToInt32(e.Value);

            e = element.Element("DOMAIN");
            this.Domain = e == null ? "" : e.Value;


            e = element.Element("SITE");
            this.Site = e == null ? "" : e.Value;
        }
        #endregion
    }
    #endregion
}