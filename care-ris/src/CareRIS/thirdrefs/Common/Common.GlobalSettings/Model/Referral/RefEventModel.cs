using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CommonGlobalSettings
{
    /// <summary>
    /// Referral Event model mapped table 'tReferralEvent'
    /// </summary>
    [Serializable()]
    public class RefEventModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _rEFERRALID = string.Empty;
        protected string _oPERATORGUID = string.Empty;
        protected DateTime? _oPERATEDT;
        protected string _sOURCEDOMAIN = string.Empty;
        protected string _tARGETDOMAIN = string.Empty;
        protected int _eVENT;
        protected int _sTATUS;
        protected string _content = string.Empty;
        protected int _tag = 0;
        protected string _eXAMDOMAIN = string.Empty;
        protected string _eXAMACCNO = string.Empty;
        protected string _oPERATORNAME = string.Empty;
        protected string _memo = string.Empty;
        protected int _scope;
        protected string _sourceSite = string.Empty;
        protected string _targetSite = string.Empty;
        #endregion

        #region Constructors

        public RefEventModel() { }

        #endregion

        #region Public Properties

        [DataField("EVENTGUID")]
        public string EVENTGUID
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 128)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
                _id = value;
            }
        }

        [DataField("REFERRALID")]
        public string REFERRALID
        {
            get { return _rEFERRALID; }
            set
            {
                _rEFERRALID = value;
            }
        }

        [DataField("OPERATORGUID")]
        public string OPERATORGUID
        {
            get { return _oPERATORGUID; }
            set
            {
                _oPERATORGUID = value;
            }
        }

        [DataField("OPERATEDT")]
        public DateTime? OPERATEDT
        {
            get { return _oPERATEDT; }
            set { _oPERATEDT = value; }
        }

        [DataField("SOURCEDOMAIN")]
        public string SOURCEDOMAIN
        {
            get { return _sOURCEDOMAIN; }
            set
            {
                _sOURCEDOMAIN = value;
            }
        }

        [DataField("TARGETDOMAIN")]
        public string TARGETDOMAIN
        {
            get { return _tARGETDOMAIN; }
            set
            {
                _tARGETDOMAIN = value;
            }
        }

        [DataField("EVENT")]
        public int EVENT
        {
            get { return _eVENT; }
            set { _eVENT = value; }
        }

        [DataField("STATUS")]
        public int STATUS
        {
            get { return _sTATUS; }
            set { _sTATUS = value; }
        }

        [DataField("Content")]
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }

        [DataField("TAG")]
        public int TAG
        {
            get { return _tag; }
            set
            {
                _tag = value;
            }
        }

        [DataField("EXAMDOMAIN")]
        public string EXAMDOMAIN
        {
            get { return _eXAMDOMAIN; }
            set
            {
                _eXAMDOMAIN = value;
            }
        }

        [DataField("EXAMACCNO")]
        public string EXAMACCNO
        {
            get { return _eXAMACCNO; }
            set
            {
                _eXAMACCNO = value;
            }
        }

        [DataField("OPERATORNAME")]
        public string OPERATORNAME
        {
            get { return _oPERATORNAME; }
            set
            {
                _oPERATORNAME = value;
            }
        }

        [DataField("Memo")]
        public string Memo
        {
            get { return _memo; }
            set
            {
                _memo = value;
            }
        }

        [DataField("Scope")]
        public int Scope
        {
            get { return _scope; }
            set
            {
                _scope = value;
            }
        }

        [DataField("SourceSite")]
        public string SourceSite
        {
            get { return _sourceSite; }
            set
            {
                _sourceSite = value;
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

        #endregion
    }
}
