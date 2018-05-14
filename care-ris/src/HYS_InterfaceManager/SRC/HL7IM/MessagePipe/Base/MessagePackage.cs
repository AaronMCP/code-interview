using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Messaging.Objects;
using HYS.Common.Xml;
using System.Collections;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Base
{
    /// <summary>
    /// As the message content may be changed 
    /// after going through a process channel and passed on to another entity,
    /// there is no need to pass on the message property or processing status together with the message.
    /// Therefore we do not define the message property or processing status in Messaging.Object,
    /// and the key-value pairs in the message property or processing status 
    /// should only be used inside message pipe.
    /// 
    /// MessagePackage should be created by dispatcher, 
    /// and then be passed on to each channel untill matching the entry criteria,
    /// and then be packaged into a session and be processed by the channel.
    /// </summary>
    public class MessagePackage
    {
        /// <summary>
        /// The XDSGW-schemaed XML string of the message 
        /// is passed through each processor and may be changed by the processor.
        /// Please do the XML serialize/deserialize of the message outside of the MessagePackage,
        /// for example in the dispatcher/sender of message pipe.
        /// The MessagePackage is a pure data package
        /// without processing logic.
        /// </summary>
        private string _messageXml;
        public string GetMessageXml()
        {
            return _messageXml;
        }
        public void SetMessageXml(string msgXml, string processorDeviceName)
        {
            if (!_acceptedByChannel)
                throw new Exception("Should not change message xml before accepting this message package.");

            _messageXml = msgXml;
            SetLog(_channelName, processorDeviceName);
        }

        /// <summary>
        /// Share never modify contents in the OriginalMessage during message processing inside message pipe.
        /// </summary>
        public readonly Message OriginalMessage;
        public MessagePackage(Message orginalMsg, string orginalMsgXml)
        {
            OriginalMessage = orginalMsg;
            _messageXml = orginalMsgXml;
        }
        public MessagePackage(Message orginalMsg, string orginalMsgXml, MessagePackage oldPackage)
        {
            OriginalMessage = orginalMsg;
            _messageXml = orginalMsgXml;
            _messageProperties = oldPackage._messageProperties;
        }
        
        /// <summary>
        /// Message property is to facilitate faster message dispatching to different channels.
        /// </summary>
        private Hashtable _messageProperties;
        public IDictionaryEnumerator GetOriginalMessageProperties()
        {
            if (_messageProperties == null) return null;
            return _messageProperties.GetEnumerator();
        }
        public void SetPropertyOfOriginalMessage(string key, string value)
        {
            if (_messageProperties == null) _messageProperties = new Hashtable();
            if (!_messageProperties.ContainsKey(key)) _messageProperties.Add(key, value);
        }
        public string GetPropertyOfOriginalMessage(string key)
        {
            if (_messageProperties == null || !_messageProperties.ContainsKey(key)) return null;
            return _messageProperties[key] as string;
        }

        /// <summary>
        /// Message processing log is to support debug and performance monitoring inside a channel.
        /// </summary>
        private List<MessageProcessEvent> _processingLog;
        private void SetLog(string channelName, string processorName)
        {
            if (_processingLog == null) _processingLog = new List<MessageProcessEvent>();
            _processingLog.Add(new MessageProcessEvent(channelName, processorName));
        }
        public MessageProcessEvent[] GetLogList()
        {
            if (_processingLog == null) return new MessageProcessEvent[] { };
            return _processingLog.ToArray<MessageProcessEvent>();
        }
        public string GetLogInfo(string seperator)
        {
            if (_processingLog == null) return "(null)";
            if (_processingLog.Count < 1) return "(empty)";
            StringBuilder sb = new StringBuilder();
            foreach (MessageProcessEvent r in _processingLog)
            {
                sb.Append(r.Time.ToString(LogHelper.DateTimeFomat))
                    .Append(' ').Append(r.ChannelName)
                    .Append(':').Append(r.ProcessorName).Append('|');
            }
            return sb.ToString().TrimEnd('|').Replace("|", seperator);
        }
        public string GetLogInfo()
        {
            return GetLogInfo(" -> ");
        }

        /// <summary>
        /// This is the only contorled status system of the MessagePackage.
        /// Before accepted by a channel, the message XML should not be changed.
        /// It has two usage:
        /// 
        /// - Protect channel from getting dirty message xml from dispatcher,
        ///   as when processing error in the pervious channel, 
        ///   dispatcher may need to dispatch the message to the next channel.
        ///   
        /// - Do not need to create new instance of MessagePackage when dispatching it to next channel,
        ///   when the pervious channel did not accept it.
        ///   
        /// </summary>
        private string _channelName;
        private bool _acceptedByChannel;
        public void Accept(string chnName)
        {
            _acceptedByChannel = true;
            _channelName = chnName;
        }
        public bool IsAccepted()
        {
            return _acceptedByChannel;
        }
    }
}
