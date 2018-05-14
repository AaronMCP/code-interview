using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Messaging.Objects;

namespace HYS.MessageDevices.MessagePipe.Base
{
    public interface IDispatcher
    {
        /// <summary>
        /// Pub/Sub mechanism only care sending status, do not care processing status.
        /// 
        /// The following channel result should map to the "True" return:
        /// - NotMatchEntryCriteria
        /// - Processing Error 
        /// - ProcessSuccess
        /// The following channel result should map to the "False" return:
        /// - SendingError
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool DispatchSubscriberMessage(Message message);
        /// <summary>
        /// Req/Rsp mechanism care both sending status and processing status.
        /// 
        /// The following channel result should map to the "True" return:
        /// -  ProcessSuccess
        /// The following channel result should map to the "False" return:
        /// - NotMatchEntryCriteria (none of the channels match entiry criteria)
        /// - Processing Error 
        /// - SendingError
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        bool DispatchResponserMessage(Message request, out Message response);
    }
}
