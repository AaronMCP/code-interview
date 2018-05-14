using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    /// <summary>
    /// RefLogModel object mapped table 'tReferralLog'.
    /// </summary>
    public class RefLogModel : ReferralBaseModel
    {
        #region Member Variables

        protected string _id;
        protected string _oPERATORGUID = string.Empty;
        protected string _oPERATORNAME = string.Empty;
        protected DateTime? _oPERATEDT;
        protected string _sOURCEDOMAIN = string.Empty;
        protected string _tARGETDOMAIN = string.Empty;
        protected string _eVENTDESC = string.Empty;
        protected string _mEMO = string.Empty;
        protected DateTime? _cREATEDT;
        protected int _refPurpose;
        protected string _sourceSite = string.Empty;
        protected string _targetSite = string.Empty;

        #endregion

        #region Constructors

        public RefLogModel() { }

        public RefLogModel(string oPERATORGUID, string oPERATORNAME, DateTime oPERATEDT, string sOURCEDOMAIN, string tARGETDOMAIN, string eVENTDESC, string mEMO, DateTime cREATEDT, int rEfpurpose, string sourceSite = "", string targetSite = "")
        {
            this._oPERATORGUID = oPERATORGUID;
            this._oPERATORNAME = oPERATORNAME;
            this._oPERATEDT = oPERATEDT;
            this._sOURCEDOMAIN = sOURCEDOMAIN;
            this._tARGETDOMAIN = tARGETDOMAIN;
            this._eVENTDESC = eVENTDESC;
            this._mEMO = mEMO;
            this._cREATEDT = cREATEDT;
            this._refPurpose = rEfpurpose;
            this._sourceSite = sourceSite;
            this._targetSite = targetSite;
        }

        #endregion

        #region Public Properties

        [DataField("REFERRALID")]
        public string REFERRALID
        {
            get { return _id; }
            set
            {
                if (value != null && value.Length > 128)
                    throw new ArgumentOutOfRangeException("Invalid value for Id", value, value.ToString());
                _id = value;
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

        [DataField("OPERATORNAME")]
        public string OPERATORNAME
        {
            get { return _oPERATORNAME; }
            set
            {
                _oPERATORNAME = value;
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

        [DataField("EVENTDESC")]
        public string EVENTDESC
        {
            get { return _eVENTDESC; }
            set
            {
                _eVENTDESC = value;
            }
        }

        [DataField("MEMO")]
        public string MEMO
        {
            get { return _mEMO; }
            set
            {
                _mEMO = value;
            }
        }

        [DataField("CREATEDT")]
        public DateTime? CREATEDT
        {
            get { return _cREATEDT; }
            set { _cREATEDT = value; }
        }

        [DataField("RefPurpose")]
        public int REFPURPOSE
        {
            get { return _refPurpose; }
            set { _refPurpose = value; }
        }

        [DataField("SourceSite")]
        public string SOURCESITE
        {
            get { return _sourceSite; }
            set { _sourceSite = value; }
        }

        [DataField("TargetSite")]
        public string TARGETSITE
        {
            get { return _targetSite; }
            set { _targetSite = value; }
        }
        #endregion
    }
}
