using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using Common.Consts;
using System.Globalization;

namespace CommonGlobalSettings
{
    #region RefReportFileModel

    /// <summary>
    /// ReportFile object  mapped table 'tReportFile'.
    /// </summary>
    public class RefReportFileModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _reportGuid = string.Empty;
        protected int _fileType;
        protected string _relativePath = string.Empty;
        protected string _fileName = string.Empty;
        protected string _backupMark = string.Empty;
        protected string _backupComment = string.Empty;
        protected int _showWidth;
        protected int _showHeight;
        protected int _imagePosition;
        protected string _domain = string.Empty;
        protected DateTime? _cREATEDT;

        #endregion

        #region Constructors

        public RefReportFileModel() { }

        public RefReportFileModel(string reportGuid, int fileType, string relativePath, string fileName, string backupMark, string backupComment, int showWidth, int showHeight, int imagePosition, string domain, DateTime cREATEDT)
        {
            this._reportGuid = reportGuid;
            this._fileType = fileType;
            this._relativePath = relativePath;
            this._fileName = fileName;
            this._backupMark = backupMark;
            this._backupComment = backupComment;
            this._showWidth = showWidth;
            this._showHeight = showHeight;
            this._imagePosition = imagePosition;
            this._domain = domain;
            this._cREATEDT = cREATEDT;
        }

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

        [DataField("ReportGuid")]
        public string ReportGuid
        {
            get { return _reportGuid; }
            set
            {
                _reportGuid = value;
            }
        }

        [DataField("FileType")]
        public int FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
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

        [DataField("ShowWidth")]
        public int ShowWidth
        {
            get { return _showWidth; }
            set { _showWidth = value; }
        }

        [DataField("ShowHeight")]
        public int ShowHeight
        {
            get { return _showHeight; }
            set { _showHeight = value; }
        }

        [DataField("ImagePosition")]
        public int ImagePosition
        {
            get { return _imagePosition; }
            set { _imagePosition = value; }
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

        [DataField("CreateDt")]
        public DateTime? CreateDt
        {
            get { return _cREATEDT; }
            set { _cREATEDT = value; }
        }

        #endregion

        #region Public Methods

        public XElement SerializeXML()
        {
            XElement element = new XElement("ReportFile",
            new XElement("FILEGUID", this.FileGuid),
            new XElement("REPORTGUID", this.ReportGuid),
            new XElement("FILETYPE", this.FileType),
            new XElement("RELATIVEPATH", this.RelativePath),
            new XElement("FILENAME", this.FileName),
            new XElement("BACKUPMARK", this.BackupMark),
            new XElement("BACKUPCOMMENT", this.BackupComment),
            new XElement("SHOWWIDTH", this.ShowWidth),
            new XElement("SHOWHEIGHT", this.ShowHeight),
            new XElement("IMAGEPOSITION", this.ImagePosition),
            new XElement("DOMAIN", this.Domain),
            new XElement("CREATEDT", this.CreateDt.HasValue ? this.CreateDt.Value.ToString(ReferralConsts.DateTimeFormatter, DateTimeFormatInfo.InvariantInfo) : ""));
            return element;
        }

        public void DeSerializeXML(XElement element)
        {
            var e = element.Element("FILEGUID");
            this.FileGuid = e == null ? "" : e.Value;

            e = element.Element("REPORTGUID");
            this.ReportGuid = e == null ? "" : e.Value;

            e = element.Element("FILETYPE");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.FileType = 0;
            else
                this.FileType = Convert.ToInt32(e.Value);

            e = element.Element("RELATIVEPATH");
            this.RelativePath = e == null ? "" : e.Value;

            e = element.Element("FILENAME");
            this.FileName = e == null ? "" : e.Value;

            e = element.Element("BACKUPMARK");
            this.BackupMark = e == null ? "" : e.Value;

            e = element.Element("BACKUPCOMMENT");
            this.BackupComment = e == null ? "" : e.Value;

            e = element.Element("SHOWWIDTH");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ShowWidth = 0;
            else
                this.ShowWidth = Convert.ToInt32(e.Value);

            e = element.Element("SHOWHEIGHT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ShowHeight = 0;
            else
                this.ShowHeight = Convert.ToInt32(e.Value);

            e = element.Element("IMAGEPOSITION");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.ImagePosition = 0;
            else
                this.ImagePosition = Convert.ToInt32(e.Value);

            e = element.Element("DOMAIN");
            this.Domain = e == null ? "" : e.Value;

            e = element.Element("CREATEDT");
            if (e == null || string.IsNullOrEmpty(e.Value))
                this.CreateDt = null;
            else
                this.CreateDt = DateTime.Parse(e.Value, DateTimeFormatInfo.InvariantInfo);

        }
        #endregion
    }
    #endregion
}
