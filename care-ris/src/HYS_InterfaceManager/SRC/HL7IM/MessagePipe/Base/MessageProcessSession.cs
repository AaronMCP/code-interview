using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace HYS.MessageDevices.MessagePipe.Base
{
    /// <summary>
    /// MessageProcessSession should be created by the channel,
    /// when an incoming MessagePackage match the entry criteria,
    /// MessageProcessSession contains the context information 
    /// to support processing inside the channel.
    /// </summary>
    [Obsolete("Currently we only implement simple one-way and simple duplex channel, this class is not necessary.", true)]
    public class MessageProcessSession
    {
        private static int _sessionIDGenerator = 0;
        private static int GetNewSessionID()
        {
            //thread safe, and handle overflow automatically
            return Interlocked.Increment(ref _sessionIDGenerator);
        }

        public readonly int ThreadID;
        public readonly int SessionID;
        public readonly string ChannelName;
        public readonly MessagePackage RequestingMessagePackage;

        public MessageProcessSession(MessagePackage requestingMessagePackage, 
            string processingChannelName)
        {
            SessionID = GetNewSessionID();
            ThreadID = Thread.CurrentThread.ManagedThreadId;
            RequestingMessagePackage = requestingMessagePackage;
            ChannelName = processingChannelName;
        }

        private string _sessionInfo;
        public override string ToString()
        {
            if (_sessionInfo == null)
            {
                _sessionInfo = string.Format(
                    "Message processing session id: {0} on thread id: {1} with channel: {2}",
                    SessionID, ThreadID, ChannelName);
            }
            return _sessionInfo;
        }

        /// <summary>
        /// Use in duplex channel only.
        /// The sender of message pipe is responsible for creating this message package object.
        /// </summary>
        public MessagePackage ResponsingMessagePackage { get; set; }
    }
}
