using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.Messaging.Objects;
using HYS.Common.Xml;

namespace HYS.IM.Messaging.Registry
{
    public partial class MessageRegistry
    {
        private static MessageType _emptyMessageType;
        /// <summary>
        /// This message type has no use. Just for demo.
        /// </summary>
        public static MessageType EmptyMessageType
        {
            get
            {
                if (_emptyMessageType == null)
                {
                    _emptyMessageType = new MessageType();
                    _emptyMessageType.Code = "EMPTY_MESSAGE";
                    _emptyMessageType.CodeSystem = "XDSGW";
                }
                return _emptyMessageType;
            }
        }

        #region message types definition in XDS Gateway 1.1

        private static MessageType _dataTrackingLogMessageType;
        public static MessageType DataTrackingLogMessageType
        {
            get
            {
                if (_dataTrackingLogMessageType == null)
                {
                    _dataTrackingLogMessageType = new MessageType();
                    _dataTrackingLogMessageType.Code = "XDSGW_LOG";
                    _dataTrackingLogMessageType.CodeSystem = "EMR";
                    _dataTrackingLogMessageType.Meaning = "Transaction log in RHIS logging schema.";
                }
                return _dataTrackingLogMessageType;
            }
        }

        private static MessageType _departmentSubmitDocumentMessageType;
        public static MessageType DepartmentSubmitDocumentMessageType
        {
            get
            {
                if (_departmentSubmitDocumentMessageType == null)
                {
                    _departmentSubmitDocumentMessageType = new MessageType();
                    _departmentSubmitDocumentMessageType.Code = "DEPT_SUBMIT";
                    _departmentSubmitDocumentMessageType.CodeSystem = "EMR";
                    _departmentSubmitDocumentMessageType.Meaning = "Submitting document metadata in EMR(Renji) schema.";
                }
                return _departmentSubmitDocumentMessageType;
            }
        }

        #endregion
    }
}
