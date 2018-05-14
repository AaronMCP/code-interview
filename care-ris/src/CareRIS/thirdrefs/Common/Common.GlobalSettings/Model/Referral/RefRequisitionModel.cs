using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using Common.Consts;
using System.Globalization;

namespace CommonGlobalSettings
{
    #region RefRequisitionModel

    /// <summary>
    /// Requisition object mapped table 'tRequisition'.
    /// </summary>
    public class RefRequisitionModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _accNo = string.Empty;
        protected string _relativePath = string.Empty;
        protected string _fileName = string.Empty;
        protected DateTime? _scanDt;
        protected string _backupMark = string.Empty;
        protected string _backupComment = string.Empty;
        protected string _domain = string.Empty;
        protected Nullable<System.DateTime> _updateTime;
        protected int _uploaded = 0;

        #endregion

        #region Constructors

        public RefRequisitionModel() { }

        #endregion

        #region Public Properties

        [DataField("RequisitionGuid")]
        public string RequisitionGuid
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 128)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
                _id = value;
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

        [DataField("RelativePath")]
        public string RelativePath
        {
            get { return _relativePath; }
            set
            {
                _relativePath = value;
            }
        }

        [DataField("FileName")]
        public string FileName
        {
            get { return _fileName; }
            set
            {
                _fileName = value;
            }
        }

        [DataField("ScanDt")]
        public DateTime? ScanDt
        {
            get { return _scanDt; }
            set { _scanDt = value; }
        }

        [DataField("BackupMark")]
        public string BackupMark
        {
            get { return _backupMark; }
            set
            {
                _backupMark = value;
            }
        }

        [DataField("BackupComment")]
        public string BackupComment
        {
            get { return _backupComment; }
            set
            {
                _backupComment = value;
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

        #region Pulbic Methods
        public XElement SerializeXML()
        {
            XElement element = new XElement("Requisition",
            new XElement("REQUISITIONGUID", this.RequisitionGuid),
            new XElement("ACCNO", this.AccNo),
            new XElement("RELATIVEPATH", this.RelativePath),
            new XElement("FILENAME", this.FileName),
            new XElement("SCANDT", this.ScanDt.HasValue ? this.ScanDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("BACKUPMARK", this.BackupMark),
            new XElement("BACKUPCOMMENT", this.BackupComment),
            new XElement("DOMAIN", this.Domain),
            new XElement("UPDATETIME", this.UpdateTime.HasValue ? this.UpdateTime.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("UPLOADED", this.Uploaded));
            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("REQUISITIONGUID");
            this.RequisitionGuid = e == null ? "" : e.Value;

            e = element.Element("ACCNO");
            this.AccNo = e == null ? "" : e.Value;

            e = element.Element("RELATIVEPATH");
            this.RelativePath = e == null ? "" : e.Value;

            e = element.Element("FILENAME");
            this.FileName = e == null ? "" : e.Value;

            e = element.Element("SCANDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ScanDt = null;
            else
                this.ScanDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("BACKUPMARK");
            this.BackupMark = e == null ? "" : e.Value;

            e = element.Element("BACKUPCOMMENT");
            this.BackupComment = e == null ? "" : e.Value;

            e = element.Element("DOMAIN");
            this.Domain = e == null ? "" : e.Value;

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
