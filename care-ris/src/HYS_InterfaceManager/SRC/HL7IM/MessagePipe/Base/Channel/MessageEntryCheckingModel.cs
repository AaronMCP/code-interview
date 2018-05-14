using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.MessageDevices.MessagePipe.Base.Channel
{
    public enum MessageEntryCheckingModel
    {
        //Accept any message
        AcceptAnyUnacceptedMessage,
        //Accept message according to message type
        AcceptUnacceptedMessageAccordingToMessageType,
        //Accept message according to message content
        AcceptUnacceptedMessageAccordingToEntryCriteria,
    }
}
