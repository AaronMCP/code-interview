using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using HYS.IM.Common.Logging;
using HYS.IM.Common.WCFHelper.Configurability;
using HYS.IM.Common.WCFHelper;
using System.Threading;

namespace HYS.IM.MessageDevices.SOAPAdapter.SOAPServer.Controler
{
    /// <summary>
    /// There is not state machine specification for the session so far.
    /// The status only indicate which processing phase a session is in.
    /// </summary>
    public enum SOAPReceiverSessionStatus
    {
        IncomingSOAPEnvelopeReceived,           // receive incoming SOAP envelope
        IncomingMessageXmlGenerated,            // transform to XDS Gateway message XML (be used for dispatching in custom/test model)
        IncomingMessageDispatchingKeyGenerated, // get the key for dipatching in custom/test model
        IncomingMessageCreated,                 // create XDS Gateway message object
        OutgoingMessageReceived,                // receive responsed XDS Gateway message object
        OutgoingSOAPEnvelopeGenerated,          // transform to outgoing SOAP envelope
    }

    public struct SOAPReceiverSessionStatusEvent
    {
        public SOAPReceiverSessionStatus Status;
        public DateTime Time;
    }

    public class SOAPReceiverSession
    {
        private static int _sessionIDGenerator = 0;
        private static int GetNewsessionID()
        {
            //thread safe, and handle overflow automatically
            return Interlocked.Increment(ref _sessionIDGenerator);
        }

        public readonly int ThreadID;
        public readonly int SessionID;

        public SOAPReceiverSession(string soapMsgRcv, bool enableStatusLog)
        {
            SessionID = GetNewsessionID();
            ThreadID = Thread.CurrentThread.ManagedThreadId;
            EnableStatusLog = enableStatusLog;
            IncomingSOAPEnvelope = soapMsgRcv;
        }
        public SOAPReceiverSession(string soapMsgRcv)
            : this(soapMsgRcv, false)
        {
        }

        private string _sessionInfo;
        public override string ToString()
        {
            if (_sessionInfo == null)
            {
                _sessionInfo = string.Format(
                    "SOAP session id: {0} on thread id: {1}",
                    SessionID, ThreadID);
            }
            return _sessionInfo;
        }

        private SOAPReceiverSessionStatus _status;
        public SOAPReceiverSessionStatus Status
        {
            get { return _status; }
        }

        private string _incomingSOAPEnvelope = "";
        public string IncomingSOAPEnvelope
        {
            get { return _incomingSOAPEnvelope; }
            set
            {
                _incomingSOAPEnvelope = value;
                SetStatusLog(_status = SOAPReceiverSessionStatus.IncomingSOAPEnvelopeReceived);
            }
        }

        private string _incomingMessageXml = "";
        public string IncomingMessageXml
        {
            get { return _incomingMessageXml; }
            set
            {
                _incomingMessageXml = value;
                SetStatusLog(_status = SOAPReceiverSessionStatus.IncomingMessageXmlGenerated);
            }
        }

        private string _incomingMessageDispatchingKey = "";
        public string IncomingMessageDispatchingKey
        {
            get { return _incomingMessageDispatchingKey; }
            set
            {
                _incomingMessageDispatchingKey = value;
                SetStatusLog(_status = SOAPReceiverSessionStatus.IncomingMessageDispatchingKeyGenerated);
            }
        }

        private HYS.IM.Messaging.Objects.Message _incomingMessage = null;
        public HYS.IM.Messaging.Objects.Message IncomingMessage
        {
            get { return _incomingMessage; }
            set
            {
                _incomingMessage = value;
                SetStatusLog(_status = SOAPReceiverSessionStatus.IncomingMessageCreated);
            }
        }

        private HYS.IM.Messaging.Objects.Message _outgoingMessage = null;
        public HYS.IM.Messaging.Objects.Message OutgoingMessage
        {
            get { return _outgoingMessage; }
            set
            {
                _outgoingMessage = value;
                SetStatusLog(_status = SOAPReceiverSessionStatus.OutgoingMessageReceived);
            }
        }

        private string _outgoingSOAPEnvelope = "";
        public string OutgoingSOAPEnvelope
        {
            get { return _outgoingSOAPEnvelope; }
            set
            {
                _outgoingSOAPEnvelope = value;
                SetStatusLog(_status = SOAPReceiverSessionStatus.OutgoingSOAPEnvelopeGenerated);
            }
        }

        #region for testing

        private readonly bool EnableStatusLog;
        private List<SOAPReceiverSessionStatusEvent> _statusLog;
        private void SetStatusLog(SOAPReceiverSessionStatus s)
        {
            if (EnableStatusLog)
            {
                if (_statusLog == null) _statusLog = new List<SOAPReceiverSessionStatusEvent>();
                SOAPReceiverSessionStatusEvent e = new SOAPReceiverSessionStatusEvent();
                e.Time = DateTime.Now;
                e.Status = s;
                _statusLog.Add(e);
            }
        }

        public string GetStatusLog()
        {
            return GetStatusLog(" -> ");
        }
        public string GetStatusLog(string seperator)
        {
            if (!EnableStatusLog) return "";
            if (_statusLog == null) return "(null)";

            StringBuilder sb = new StringBuilder();
            foreach (SOAPReceiverSessionStatusEvent s in _statusLog)
            {
                sb.Append(s.Time.ToString(LogHelper.DateTimeFomat)).
                    Append(' ').Append(s.Status.ToString()).Append('|');
            }

            string str = (seperator == null) ? "" : seperator;
            return sb.ToString().TrimEnd('|').Replace("|", seperator);
        }
        public string GetStartTime()
        {
            if (!EnableStatusLog) return "";
            if (_statusLog == null) return "(null)";
            if (_statusLog.Count < 1) return "(empty)";

            return _statusLog[0].Time.ToString(LogHelper.DateTimeFomat);
        }
        public string GetTimeSpanMS()
        {
            if (!EnableStatusLog) return "";
            if (_statusLog == null) return "(null)";
            if (_statusLog.Count < 2) return "(empty)";

            TimeSpan ts = _statusLog[_statusLog.Count - 1].Time.Subtract(_statusLog[0].Time);
            return ts.TotalMilliseconds.ToString();
        }

        #endregion
    }
}
