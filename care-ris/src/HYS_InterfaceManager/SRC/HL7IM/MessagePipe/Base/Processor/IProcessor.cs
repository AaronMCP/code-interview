using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Common.Logging;

namespace HYS.MessageDevices.MessagePipe.Base.Processor
{
    public interface IProcessor
    {
        bool Initialize(ProcessorInitializationParameter parameter);
        /// <summary>
        /// In duplex channel, the response message processing processor can access request message in the session
        /// </summary>
        /// <param name="session">The processing context. (readonly) </param>
        /// <param name="message">The message/xml to be processed. (writable) </param>
        /// <returns></returns>
        bool ProcessMessage(MessagePackage message);
        //bool ProcessMessage(MessageProcessSession session, MessagePackage message);
        bool Uninitilize();
    }
}
