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
    /// RefProcedureModel object mapped table 'tRegProcedure'.
    /// </summary>
    [Serializable()]
    public class RefProcedureModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _procedureCode = string.Empty;
        protected string _examSystem = string.Empty;
        protected int _warningTime;
        protected string _filmSpec = string.Empty;
        protected int _filmCount;
        protected string _contrastName = string.Empty;
        protected string _contrastDose = string.Empty;
        protected int _imageCount;
        protected int _exposalCount;
        protected decimal _deposit;
        protected decimal _charge;
        protected string _modalityType = string.Empty;
        protected string _modality = string.Empty;
        protected string _registrar = string.Empty;
        protected DateTime? _registerDt;
        protected int _priority;
        protected string _technician = string.Empty;
        protected string _techDoctor = string.Empty;
        protected string _techNurse = string.Empty;
        protected string _operationStep = string.Empty;
        protected DateTime? _examineDt;
        protected string _mender = string.Empty;
        protected DateTime? _modifyDt;
        protected int _isPost;
        protected int _isExistImage;
        protected int _status;
        protected string _comments = string.Empty;
        protected DateTime? _bookingBeginDt;
        protected DateTime? _bookingEndDt;
        protected string _booker = string.Empty;
        protected int _isCharge;
        protected string _remoteRPID = string.Empty;
        protected string _optional1 = string.Empty;
        protected string _optional2 = string.Empty;
        protected string _optional3 = string.Empty;
        protected string _queueNo = string.Empty;
        protected byte[] _bookingNotice;
        protected string _bookingTimeAlias = string.Empty;
        protected DateTime? _createDt;
        protected string _reportGuid = null;
        protected string _medicineUsage = string.Empty;
        protected string _posture = string.Empty;
        protected string _orderGuid = null;
        protected string _domain = string.Empty;
        protected string _bookerName = string.Empty;
        protected string _registerName = string.Empty;
        protected string _technicianName = string.Empty;
        protected Nullable<System.DateTime> _updateTime;
        protected int _uploaded = 0;
        protected string _bodyCategory = string.Empty;
        protected string _bodypart = string.Empty;
        protected string _checkingItem = string.Empty;
        protected string _rPDesc = string.Empty;
        protected RefProcedureCodeModel _procedureCodeModel = null;
        #endregion

        #region Constructors

        public RefProcedureModel() { }

        #endregion

        #region Public Properties

        [DataField("ProcedureGuid")]
        public string ProcedureGuid
        {
            get { return _id; }
            set
            {
                _id = value;
            }
        }

        [DataField("ProcedureCode")]
        public string ProcedureCode
        {
            get { return _procedureCode; }
            set
            {
                _procedureCode = value;
            }
        }

        [DataField("ExamSystem")]
        public string ExamSystem
        {
            get { return _examSystem; }
            set
            {
                _examSystem = value;
            }
        }

        [DataField("WarningTime")]
        public int WarningTime
        {
            get { return _warningTime; }
            set { _warningTime = value; }
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

        [DataField("Deposit")]
        public decimal Deposit
        {
            get { return _deposit; }
            set { _deposit = value; }
        }

        [DataField("Charge")]
        public decimal Charge
        {
            get { return _charge; }
            set { _charge = value; }
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

        [DataField("Modality")]
        public string Modality
        {
            get { return _modality; }
            set
            {
                _modality = value;
            }
        }

        [DataField("Registrar")]
        public string Registrar
        {
            get { return _registrar; }
            set
            {
                _registrar = value;
            }
        }

        [DataField("RegisterDt")]
        public DateTime? RegisterDt
        {
            get { return _registerDt; }
            set { _registerDt = value; }
        }

        [DataField("Priority")]
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        [DataField("Technician")]
        public string Technician
        {
            get { return _technician; }
            set
            {
                _technician = value;
            }
        }

        [DataField("TechDoctor")]
        public string TechDoctor
        {
            get { return _techDoctor; }
            set
            {
                _techDoctor = value;
            }
        }

        [DataField("TechNurse")]
        public string TechNurse
        {
            get { return _techNurse; }
            set
            {
                _techNurse = value;
            }
        }

        [DataField("OperationStep")]
        public string OperationStep
        {
            get { return _operationStep; }
            set
            {
                _operationStep = value;
            }
        }

        [DataField("ExamineDt")]
        public DateTime? ExamineDt
        {
            get { return _examineDt; }
            set { _examineDt = value; }
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

        [DataField("IsPost")]
        public int IsPost
        {
            get { return _isPost; }
            set { _isPost = value; }
        }

        [DataField("IsExistImage")]
        public int IsExistImage
        {
            get { return _isExistImage; }
            set { _isExistImage = value; }
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

        [DataField("Booker")]
        public string Booker
        {
            get { return _booker; }
            set
            {
                _booker = value;
            }
        }

        [DataField("IsCharge")]
        public int IsCharge
        {
            get { return _isCharge; }
            set { _isCharge = value; }
        }

        [DataField("RemoteRPID")]
        public string RemoteRPID
        {
            get { return _remoteRPID; }
            set
            {
                _remoteRPID = value;
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

        [DataField("QueueNo")]
        public string QueueNo
        {
            get { return _queueNo; }
            set
            {
                _queueNo = value;
            }
        }

        [DataField("BookingNotice")]
        public byte[] BookingNotice
        {
            get { return _bookingNotice; }
            set { _bookingNotice = value; }
        }

        [DataField("BookingTimeAlias")]
        public string BookingTimeAlias
        {
            get { return _bookingTimeAlias; }
            set
            {
                _bookingTimeAlias = value;
            }
        }

        [DataField("CreateDt")]
        public DateTime? CreateDt
        {
            get { return _createDt; }
            set { _createDt = value; }
        }

        [DataField("ReportGuid")]
        public string ReportGuid
        {
            get { return _reportGuid; }
            set
            {
                _reportGuid = value;
            }
        }

        [DataField("MedicineUsage")]
        public string MedicineUsage
        {
            get { return _medicineUsage; }
            set
            {
                _medicineUsage = value;
            }
        }

        [DataField("Posture")]
        public string Posture
        {
            get { return _posture; }
            set
            {
                _posture = value;
            }
        }

        [DataField("OrderGuid")]
        public string OrderGuid
        {
            get { return _orderGuid; }
            set
            {
                _orderGuid = value;
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

        [DataField("BookerName")]
        public string BookerName
        {
            get { return _bookerName; }
            set { _bookerName = value; }
        }

        [DataField("RegistrarName")]
        public string RegistrarName
        {
            get { return _registerName; }
            set { _registerName = value; }
        }

        [DataField("TechnicianName")]
        public string TechnicianName
        {
            get { return _technicianName; }
            set { _technicianName = value; }
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

        public RefProcedureCodeModel ProcedureCodeModel
        {
            get { return _procedureCodeModel; }
            set { _procedureCodeModel = value; }
        }

        [DataField("BodyCategory")]
        public string BodyCategory
        {
            get { return _bodyCategory; }
            set { _bodyCategory = value; }
        }

        [DataField("BodyPart")]
        public string BodyPart
        {
            get { return _bodypart; }
            set { _bodypart = value; }
        }

        [DataField("CheckingItem")]
        public string CheckingItem
        {
            get { return _checkingItem; }
            set { _checkingItem = value; }
        }

        [DataField("RPDesc")]
        public string RPDesc
        {
            get { return _rPDesc; }
            set { _rPDesc = value; }
        }

        #endregion

        #region Public Methods

        public XElement SerializeXML()
        {
            XElement element = new XElement("Procedure",
            new XElement("PROCEDUREGUID", this.ProcedureGuid),
            new XElement("PROCEDURECODE", this.ProcedureCode),
            new XElement("EXAMSYSTEM", this.ExamSystem),
            new XElement("WARNINGTIME", this.WarningTime),
            new XElement("FILMSPEC", this.FilmSpec),
            new XElement("FILMCOUNT", this.FilmCount),
            new XElement("CONTRASTNAME", this.ContrastName),
            new XElement("CONTRASTDOSE", this.ContrastDose),
            new XElement("IMAGECOUNT", this.ImageCount),
            new XElement("EXPOSALCOUNT", this.ExposalCount),
            new XElement("DEPOSIT", this.Deposit),
            new XElement("CHARGE", this.Charge),
            new XElement("MODALITYTYPE", this.ModalityType),
            new XElement("MODALITY", this.Modality),
            new XElement("REGISTRAR", this.Registrar),
            new XElement("REGISTERDT", this.RegisterDt.HasValue ? this.RegisterDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("PRIORITY", this.Priority),
            new XElement("TECHNICIAN", this.Technician),
            new XElement("TECHDOCTOR", this.TechDoctor),
            new XElement("TECHNURSE", this.TechNurse),
            new XElement("OPERATIONSTEP", this.OperationStep),
            new XElement("EXAMINEDT", this.ExamineDt.HasValue ? this.ExamineDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("MENDER", this.Mender),
            new XElement("MODIFYDT", this.ModifyDt.HasValue ? this.ModifyDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("ISPOST", this.IsPost),
            new XElement("ISEXISTIMAGE", this.IsExistImage),
            new XElement("STATUS", this.Status),
            new XElement("COMMENTS", this.Comments),
            new XElement("BOOKINGBEGINDT", this.BookingBeginDt.HasValue ? this.BookingBeginDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("BOOKINGENDDT", this.BookingEndDt.HasValue ? this.BookingEndDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("BOOKER", this.Booker),
            new XElement("ISCHARGE", this.IsCharge),
            new XElement("REMOTERPID", this.RemoteRPID),
            new XElement("OPTIONAL1", this.Optional1),
            new XElement("OPTIONAL2", this.Optional2),
                //new XElement("OPTIONAL3", this.Optional3),
            new XElement("QUEUENO", this.QueueNo),
            new XElement("BOOKINGNOTICE", this.BookingNotice),
            new XElement("BOOKINGTIMEALIAS", this.BookingTimeAlias),
            new XElement("CREATEDT", this.CreateDt.HasValue ? this.CreateDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("REPORTGUID", this.ReportGuid),
            new XElement("MEDICINEUSAGE", this.MedicineUsage),
            new XElement("POSTURE", this.Posture),
            new XElement("ORDERGUID", this.OrderGuid),
            new XElement("BOOKERNAME", this.BookerName),
            new XElement("REGISTRARNAME", this.RegistrarName),
            new XElement("TECHNICIANNAME", this.TechnicianName),
            new XElement("UPDATETIME", this.UpdateTime.HasValue ? this.UpdateTime.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("UPLOADED", this.Uploaded),
            new XElement("DOMAIN", this.Domain),
            new XElement("BODYCATEGORY", this.BodyCategory),
            new XElement("BODYPART", this.BodyPart),
            new XElement("CHECKINGITEM", this.CheckingItem),
            new XElement("RPDESC", this.RPDesc));

            if (this._procedureCodeModel != null)
                element.Add(this._procedureCodeModel.SerializeXML());

            return element;
        }


        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("PROCEDUREGUID");
            this.ProcedureGuid = e == null ? "" : e.Value;

            e = element.Element("PROCEDURECODE");
            this.ProcedureCode = e == null ? "" : e.Value;

            e = element.Element("EXAMSYSTEM");
            this.ExamSystem = e == null ? "" : e.Value;

            e = element.Element("WARNINGTIME");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.WarningTime = 0;
            else
                this.WarningTime = Convert.ToInt32(e.Value);

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

            e = element.Element("DEPOSIT");
            this.Deposit = e == null ? 0 : Convert.ToDecimal(e.Value);

            e = element.Element("CHARGE");
            this.Charge = e == null ? 0 : Convert.ToDecimal(e.Value);

            e = element.Element("MODALITYTYPE");
            this.ModalityType = e == null ? "" : e.Value;

            e = element.Element("MODALITY");
            this.Modality = e == null ? "" : e.Value;

            e = element.Element("REGISTRAR");
            this.Registrar = e == null ? "" : e.Value;

            e = element.Element("REGISTERDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.RegisterDt = null;
            else
                this.RegisterDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("PRIORITY");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Priority = 0;
            else
                this.Priority = Convert.ToInt32(e.Value);

            e = element.Element("TECHNICIAN");
            this.Technician = e == null ? "" : e.Value;

            e = element.Element("TECHDOCTOR");
            this.TechDoctor = e == null ? "" : e.Value;

            e = element.Element("TECHNURSE");
            this.TechNurse = e == null ? "" : e.Value;

            e = element.Element("OPERATIONSTEP");
            this.OperationStep = e == null ? "" : e.Value;

            e = element.Element("EXAMINEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ExamineDt = null;
            else
                this.ExamineDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("MENDER");
            this.Mender = e == null ? "" : e.Value;

            e = element.Element("MODIFYDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ModifyDt = null;
            else
                this.ModifyDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("ISPOST");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsPost = 0;
            else
                this.IsPost = Convert.ToInt32(e.Value);

            e = element.Element("ISEXISTIMAGE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsExistImage = 0;
            else
                this.IsExistImage = Convert.ToInt32(e.Value);

            e = element.Element("STATUS");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.Status = 0;
            else
                this.Status = Convert.ToInt32(e.Value);

            e = element.Element("COMMENTS");
            this.Comments = e == null ? "" : e.Value;

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

            e = element.Element("BOOKER");
            this.Booker = e == null ? "" : e.Value;

            e = element.Element("ISCHARGE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.IsCharge = 0;
            else
                this.IsCharge = Convert.ToInt32(e.Value);

            e = element.Element("REMOTERPID");
            this.RemoteRPID = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL1");
            this.Optional1 = e == null ? "" : e.Value;

            e = element.Element("OPTIONAL2");
            this.Optional2 = e == null ? "" : e.Value;

            //e = element.Element("OPTIONAL3");
            //this.Optional3 = e == null ? "" : e.Value;

            e = element.Element("QUEUENO");
            this.QueueNo = e == null ? "" : e.Value;

            e = element.Element("BOOKINGTIMEALIAS");
            this.BookingTimeAlias = e == null ? "" : e.Value;

            e = element.Element("CREATEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.CreateDt = null;
            else
                this.CreateDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("REPORTGUID");
            this.ReportGuid = e == null ? "" : e.Value;

            e = element.Element("MEDICINEUSAGE");
            this.MedicineUsage = e == null ? "" : e.Value;

            e = element.Element("POSTURE");
            this.Posture = e == null ? "" : e.Value;

            e = element.Element("ORDERGUID");
            this.OrderGuid = e == null ? "" : e.Value;

            e = element.Element("BOOKERNAME");
            this.BookerName = e == null ? "" : e.Value;

            e = element.Element("REGISTRARNAME");
            this.RegistrarName = e == null ? "" : e.Value;

            e = element.Element("TECHNICIANNAME");
            this.TechnicianName = e == null ? "" : e.Value;

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


            e = element.Element("DOMAIN");
            this.Domain = e == null ? "" : e.Value;

            e = element.Element("BODYCATEGORY");
            this.BodyCategory = e == null ? "" : e.Value;

            e = element.Element("BODYPART");
            this.BodyPart = e == null ? "" : e.Value;

            e = element.Element("CHECKINGITEM");
            this.CheckingItem = e == null ? "" : e.Value;

            e = element.Element("RPDESC");
            this.RPDesc = e == null ? "" : e.Value;

            e = element.Element("ProcedureCode");
            if (e != null)
            {
                this._procedureCodeModel = new RefProcedureCodeModel();
                this._procedureCodeModel.DeSerializeXML(e);
            }
        }

        #endregion
    }
}
