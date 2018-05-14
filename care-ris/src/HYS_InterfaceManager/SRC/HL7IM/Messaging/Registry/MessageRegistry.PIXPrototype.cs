using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Registry
{
    public partial class MessageRegistry
    {
        #region message types definition for generic HL7v2 communication

        private static MessageType _hl7V2_NotificationMessageType;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType HL7V2_NotificationMessageType
        {
            get
            {
                if (_hl7V2_NotificationMessageType == null)
                {
                    _hl7V2_NotificationMessageType = new MessageType();
                    _hl7V2_NotificationMessageType.Code = "NOTIFICATION";
                    _hl7V2_NotificationMessageType.CodeSystem = "HL7V2";
                    _hl7V2_NotificationMessageType.Meaning = "HL7 notification message in HL7v2 hat and pipe format.";
                }
                return _hl7V2_NotificationMessageType;
            }
        }

        private static MessageType _hl7V2_QueryRequestMessageType;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType HL7V2_QueryRequestMessageType
        {
            get
            {
                if (_hl7V2_QueryRequestMessageType == null)
                {
                    _hl7V2_QueryRequestMessageType = new MessageType();
                    _hl7V2_QueryRequestMessageType.Code = "QUERY_REQUEST";
                    _hl7V2_QueryRequestMessageType.CodeSystem = "HL7V2";
                    _hl7V2_QueryRequestMessageType.Meaning = "HL7 query request message in HL7v2 hat and pipe format.";
                }
                return _hl7V2_QueryRequestMessageType;
            }
        }

        private static MessageType _hl7V2_QueryResultMessageType;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType HL7V2_QueryResultMessageType
        {
            get
            {
                if (_hl7V2_QueryResultMessageType == null)
                {
                    _hl7V2_QueryResultMessageType = new MessageType();
                    _hl7V2_QueryResultMessageType.Code = "QUERY_RESULT";
                    _hl7V2_QueryResultMessageType.CodeSystem = "HL7V2";
                    _hl7V2_QueryResultMessageType.Meaning = "HL7 query response message in HL7v2 hat and pipe format.";
                }
                return _hl7V2_QueryResultMessageType;
            }
        }

        #endregion

        #region message types definition for the PID management in the PIX prototype

        private static MessageType _pix_RegisterPatient;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType PIX_RegisterPatient
        {
            get
            {
                if (_pix_RegisterPatient == null)
                {
                    _pix_RegisterPatient = new MessageType();
                    _pix_RegisterPatient.Code = "REGISTER_PATIENT";
                    _pix_RegisterPatient.CodeSystem = "PIX";
                    _pix_RegisterPatient.Meaning = "Schema defined by class HYS.IM.Messaging.Registry.PatientRegisterRequest.";
                }
                return _pix_RegisterPatient;
            }
        }

        private static MessageType _pix_UpdatePatient;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType PIX_UpdatePatient
        {
            get
            {
                if (_pix_UpdatePatient == null)
                {
                    _pix_UpdatePatient = new MessageType();
                    _pix_UpdatePatient.Code = "UPDATE_PATIENT";
                    _pix_UpdatePatient.CodeSystem = "PIX";
                    _pix_UpdatePatient.Meaning = "Schema defined by class HYS.IM.Messaging.Registry.PatientUpdateRequest.";
                }
                return _pix_UpdatePatient;
            }
        }

        private static MessageType _pix_MergePatient;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType PIX_MergePatient
        {
            get
            {
                if (_pix_MergePatient == null)
                {
                    _pix_MergePatient = new MessageType();
                    _pix_MergePatient.Code = "MERGE_PATIENT";
                    _pix_MergePatient.CodeSystem = "PIX";
                    _pix_MergePatient.Meaning = "Schema defined by class HYS.IM.Messaging.Registry.PatientMergeRequest.";
                }
                return _pix_MergePatient;
            }
        }

        private static MessageType _pix_UpdatePatientNotification;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType PIX_UpdatePatientNotification
        {
            get
            {
                if (_pix_UpdatePatientNotification == null)
                {
                    _pix_UpdatePatientNotification = new MessageType();
                    _pix_UpdatePatientNotification.Code = "UPDATE_PATIENT_NOTIFICATION";
                    _pix_UpdatePatientNotification.CodeSystem = "PIX";
                    _pix_UpdatePatientNotification.Meaning = "Schema defined by class HYS.IM.Messaging.Registry.PatientUpdateNotification.";
                }
                return _pix_UpdatePatientNotification;
            }
        }

        private static MessageType _pix_QueryRequest;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType PIX_QueryRequest
        {
            get
            {
                if (_pix_QueryRequest == null)
                {
                    _pix_QueryRequest = new MessageType();
                    _pix_QueryRequest.Code = "QUERY_REQUEST";
                    _pix_QueryRequest.CodeSystem = "PIX";
                    _pix_QueryRequest.Meaning = "Schema defined by class HYS.IM.Messaging.Registry.PIXQueryRequest.";
                }
                return _pix_QueryRequest;
            }
        }

        private static MessageType _pix_QueryResponse;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType PIX_QueryResponse
        {
            get
            {
                if (_pix_QueryResponse == null)
                {
                    _pix_QueryResponse = new MessageType();
                    _pix_QueryResponse.Code = "QUERY_RESPONSE";
                    _pix_QueryResponse.CodeSystem = "PIX";
                    _pix_QueryResponse.Meaning = "Schema defined by class HYS.IM.Messaging.Registry.PIXQueryResponse.";
                }
                return _pix_QueryResponse;
            }
        }

        private static MessageType _pdq_QueryRequest;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType PDQ_QueryRequest
        {
            get
            {
                if (_pdq_QueryRequest == null)
                {
                    _pdq_QueryRequest = new MessageType();
                    _pdq_QueryRequest.Code = "QUERY_REQUEST";
                    _pdq_QueryRequest.CodeSystem = "PDQ";
                    _pdq_QueryRequest.Meaning = "Schema defined by class HYS.IM.Messaging.Registry.PDQQueryRequest.";
                }
                return _pdq_QueryRequest;
            }
        }

        private static MessageType _pdq_QueryResponse;
        /// <summary>
        /// This message type is used in PIX prototype only.
        /// Do not need to use it in XDS Gateway 1.1 deliverables.
        /// </summary>
        public static MessageType PDQ_QueryResponse
        {
            get
            {
                if (_pdq_QueryResponse == null)
                {
                    _pdq_QueryResponse = new MessageType();
                    _pdq_QueryResponse.Code = "QUERY_RESPONSE";
                    _pdq_QueryResponse.CodeSystem = "PDQ";
                    _pdq_QueryResponse.Meaning = "Schema defined by class HYS.IM.Messaging.Registry.PDQQueryResponse.";
                }
                return _pdq_QueryResponse;
            }
        }

        #endregion
    }
}
