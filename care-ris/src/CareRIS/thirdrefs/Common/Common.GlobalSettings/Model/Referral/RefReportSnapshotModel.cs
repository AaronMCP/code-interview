using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Xml.Linq;
using Common.Consts;
using System.Globalization;

namespace CommonGlobalSettings
{
    #region RefReportSnapshot
    /// <summary>
    /// RefPatientModel mapped table 'tRefReportSnapshot'
    /// </summary>
    [Serializable()]
    public class RefReportSnapshotModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _referralId = string.Empty;
        protected string _relativePath = string.Empty;
        protected string _fileName = string.Empty;
        protected DateTime? _createDt;
        protected string _backupMark = string.Empty;
        protected string _backupComment = string.Empty;
        protected string _domain = string.Empty;

        #endregion

        #region Constructors

        public RefReportSnapshotModel() { }

        #endregion

        #region Public Properties

        [DataField("FileGuid")]
        public string FileGuid
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 128)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
                _id = value;
            }
        }

        [DataField("ReferralId")]
        public string ReferralId
        {
            get { return _referralId; }
            set
            {
                _referralId = value;
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

        [DataField("CreateDt")]
        public DateTime? CreateDt
        {
            get { return _createDt; }
            set { _createDt = value; }
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



        #endregion

        #region Public Methods

        public XElement SerializeXML()
        {
            XElement element = new XElement("RefReportSnapshot",
            new XElement("FILEGUID", this.FileGuid),
            new XElement("REFERRALID", this.ReferralId),
            new XElement("RELATIVEPATH", this.RelativePath),
            new XElement("FILENAME", this.FileName),
            new XElement("CREATEDT", this.CreateDt.HasValue ? this.CreateDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""),
            new XElement("BACKUPMARK", this.BackupMark),
            new XElement("BACKUPCOMMENT", this.BackupComment),
            new XElement("DOMAIN", this.Domain));
            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("FILEGUID");
            this.FileGuid = e == null ? "" : e.Value;

            e = element.Element("REFERRALID");
            this.ReferralId = e == null ? "" : e.Value;

            e = element.Element("RELATIVEPATH");
            this.RelativePath = e == null ? "" : e.Value;

            e = element.Element("FILENAME");
            this.FileName = e == null ? "" : e.Value;

            e = element.Element("CREATEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.CreateDt = null;
            else
                this.CreateDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

            e = element.Element("BACKUPMARK");
            this.BackupMark = e == null ? "" : e.Value;

            e = element.Element("BACKUPCOMMENT");
            this.BackupComment = e == null ? "" : e.Value;

            e = element.Element("DOMAIN");
            this.Domain = e == null ? "" : e.Value;


        }

        #endregion
    }

    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(RefReportSnapshotModel))]
    public class RefReportSnapshotListModel : ReferralBaseModel
    {
        private List<RefReportSnapshotModel> rptSts = null;

        public List<RefReportSnapshotModel> ReportSnapshots
        {
            get { return rptSts; }
            set { rptSts = value; }
        }
    }
    #endregion
}
