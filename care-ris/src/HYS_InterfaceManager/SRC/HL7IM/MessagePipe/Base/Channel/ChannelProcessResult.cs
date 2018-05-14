using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.MessageDevices.MessagePipe.Base.Channel
{
    public enum ChannelProcessResult
    {
        /// <summary>
        /// When return EntryCheckingError, the channel should not change message XML,
        /// but can add new properties into the message properties.
        /// And then the dispatcher will continue trying to dispatch message to next channel.
        /// 
        /// Specifically, the dispatcher will pass on the MessagePackage object to next channel directly.
        /// </summary>
        NotMatchEntryCriteria,
        /// <summary>
        /// When return ProcessingError, the channel should have accepted the message package but processing failed,
        /// and it is assumed that the message XML of the message package has been updated.
        /// 
        /// If the dispatcher want to continue trying to dispatch message to another channel
        /// (according to configuration: PassOnMessageToNextChannelIfProcessingError),
        /// it has to create a new message package instance with original message.
        /// 
        /// Specifically, the dispatcher will call the following contructor to create a new object for next channel.
        /// MessagePackage(Message orginalMsg, string orginalMsgXml, MessagePackage oldPackage)
        /// 
        /// Note: processed message XML should not passed on to the next channel,
        /// if you want to implement batch/serialize processing between channels
        /// (or connect dataflows of multiple channels), please connect multiple message pipes.
        /// </summary>
        ProcessingError,
        /// <summary>
        /// When return SendingError, the channel should have accepted the message package, successfully processed the messages,
        /// but send to other message entity failed. It is assumed that the message XML of the message package has been updated.
        /// 
        /// If the original message is dispatched from the subscriber (which means the message is published from another entity),
        /// when SendingError occur, and the publishing entity connects to message pipe via LPC,
        /// the message pipe will throw a LPC exception, so that the publisher can retry.
        /// </summary>
        SendingError,
        Success,
    }
}
