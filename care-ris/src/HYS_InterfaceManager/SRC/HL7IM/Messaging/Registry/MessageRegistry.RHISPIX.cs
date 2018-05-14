using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.IM.Messaging.Objects;

namespace HYS.IM.Messaging.Registry
{
    public partial class MessageRegistry
    {
        #region message types definition for RHIS PIX communication

        private static MessageType _rhispix_NotificationMessageType;
        public static MessageType RHISPIX_NotificationMessageType
        {
            get
            {
                if (_rhispix_NotificationMessageType == null)
                {
                    _rhispix_NotificationMessageType = new MessageType();
                    _rhispix_NotificationMessageType.Code = "NOTIFICATION";
                    _rhispix_NotificationMessageType.CodeSystem = "RHISPIX";
                    _rhispix_NotificationMessageType.Meaning = "Generic notification message (request message without meaningful response) with schema defined by RHIS PIX web serivce.";
                }
                return _rhispix_NotificationMessageType;
            }
        }

        private static MessageType _rhispix_RequestMessageType;
        public static MessageType RHISPIX_RequestMessageType
        {
            get
            {
                if (_rhispix_RequestMessageType == null)
                {
                    _rhispix_RequestMessageType = new MessageType();
                    _rhispix_RequestMessageType.Code = "REQUEST";
                    _rhispix_RequestMessageType.CodeSystem = "RHISPIX";
                    _rhispix_RequestMessageType.Meaning = "Generic request message with schema defined by RHIS PIX web serivce.";
                }
                return _rhispix_RequestMessageType;
            }
        }

        private static MessageType _rhispix_ResponseMessageType;
        public static MessageType RHISPIX_ResponseMessageType
        {
            get
            {
                if (_rhispix_ResponseMessageType == null)
                {
                    _rhispix_ResponseMessageType = new MessageType();
                    _rhispix_ResponseMessageType.Code = "RESPONSE";
                    _rhispix_ResponseMessageType.CodeSystem = "RHISPIX";
                    _rhispix_ResponseMessageType.Meaning = "Generic response message with schema defined by RHIS PIX web serivce.";
                }
                return _rhispix_ResponseMessageType;
            }
        }

        /// <summary>
        /// If RHIS PIX SOAP server/client could accept/send out messages in HL7v2 XML schema,
        /// the following message types will no longer be needed.
        /// </summary>

        //private static MessageType _rhispix_RegisterPatient;
        //public static MessageType RHISPIX_RegisterPatient
        //{
        //    get
        //    {
        //        if (_rhispix_RegisterPatient == null)
        //        {
        //            _rhispix_RegisterPatient = new MessageType();
        //            _rhispix_RegisterPatient.Code = "REGISTER_PATIENT";
        //            _rhispix_RegisterPatient.CodeSystem = "RHISPIX";
        //            _rhispix_RegisterPatient.Meaning = "Patient registration message with schema defined by RHIS PIX web serivce.";
        //        }
        //        return _rhispix_RegisterPatient;
        //    }
        //}

        //private static MessageType _rhispix_UpdatePatient;
        //public static MessageType RHISPIX_UpdatePatient
        //{
        //    get
        //    {
        //        if (_rhispix_UpdatePatient == null)
        //        {
        //            _rhispix_UpdatePatient = new MessageType();
        //            _rhispix_UpdatePatient.Code = "UPDATE_PATIENT";
        //            _rhispix_UpdatePatient.CodeSystem = "RHISPIX";
        //            _rhispix_UpdatePatient.Meaning = "Patient update message with schema defined by RHIS PIX web serivce.";
        //        }
        //        return _rhispix_UpdatePatient;
        //    }
        //}

        //private static MessageType _rhispix_MergePatient;
        //public static MessageType RHISPIX_MergePatient
        //{
        //    get
        //    {
        //        if (_rhispix_MergePatient == null)
        //        {
        //            _rhispix_MergePatient = new MessageType();
        //            _rhispix_MergePatient.Code = "MERGE_PATIENT";
        //            _rhispix_MergePatient.CodeSystem = "RHISPIX";
        //            _rhispix_MergePatient.Meaning = "Patient merge message with schema defined by RHIS PIX web serivce.";
        //        }
        //        return _rhispix_MergePatient;
        //    }
        //}

        //private static MessageType _rhispix_UpdatePatientNotification;
        //public static MessageType RHISPIX_UpdatePatientNotification
        //{
        //    get
        //    {
        //        if (_rhispix_UpdatePatientNotification == null)
        //        {
        //            _rhispix_UpdatePatientNotification = new MessageType();
        //            _rhispix_UpdatePatientNotification.Code = "UPDATE_PATIENT_NOTIFICATION";
        //            _rhispix_UpdatePatientNotification.CodeSystem = "RHISPIX";
        //            _rhispix_UpdatePatientNotification.Meaning = "Patient update notification message with schema defined by RHIS PIX web serivce.";
        //        }
        //        return _rhispix_UpdatePatientNotification;
        //    }
        //}

        //private static MessageType _rhispix_QueryRequest;
        //public static MessageType RHISPIX_QueryRequest
        //{
        //    get
        //    {
        //        if (_rhispix_QueryRequest == null)
        //        {
        //            _rhispix_QueryRequest = new MessageType();
        //            _rhispix_QueryRequest.Code = "QUERY_REQUEST";
        //            _rhispix_QueryRequest.CodeSystem = "RHISPIX";
        //            _rhispix_QueryRequest.Meaning = "PIX query request message with schema defined by RHIS PIX web serivce.";
        //        }
        //        return _rhispix_QueryRequest;
        //    }
        //}

        //private static MessageType _rhispix_QueryResponse;
        //public static MessageType RHISPIX_QueryResponse
        //{
        //    get
        //    {
        //        if (_rhispix_QueryResponse == null)
        //        {
        //            _rhispix_QueryResponse = new MessageType();
        //            _rhispix_QueryResponse.Code = "QUERY_RESPONSE";
        //            _rhispix_QueryResponse.CodeSystem = "RHISPIX";
        //            _rhispix_QueryResponse.Meaning = "PIX query response message with schema defined by RHIS PIX web serivce.";
        //        }
        //        return _rhispix_QueryResponse;
        //    }
        //}

        //private static MessageType _rhispdq_QueryRequest;
        //public static MessageType RHISPDQ_QueryRequest
        //{
        //    get
        //    {
        //        if (_rhispdq_QueryRequest == null)
        //        {
        //            _rhispdq_QueryRequest = new MessageType();
        //            _rhispdq_QueryRequest.Code = "QUERY_REQUEST";
        //            _rhispdq_QueryRequest.CodeSystem = "RHISPDQ";
        //            _rhispdq_QueryRequest.Meaning = "PDQ query request message with schema defined by RHIS PIX web serivce.";
        //        }
        //        return _rhispdq_QueryRequest;
        //    }
        //}

        //private static MessageType _rhispdq_QueryResponse;
        //public static MessageType RHISPDQ_QueryResponse
        //{
        //    get
        //    {
        //        if (_rhispdq_QueryResponse == null)
        //        {
        //            _rhispdq_QueryResponse = new MessageType();
        //            _rhispdq_QueryResponse.Code = "QUERY_RESPONSE";
        //            _rhispdq_QueryResponse.CodeSystem = "RHISPDQ";
        //            _rhispdq_QueryResponse.Meaning = "PDQ query response message with schema defined by RHIS PIX web serivce.";
        //        }
        //        return _rhispdq_QueryResponse;
        //    }
        //}

        #endregion
    }
}
