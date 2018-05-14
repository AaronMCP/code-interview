using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.MessageDevices.MessagePipe.Base
{
    public class MessageProcessEvent
    {
        public readonly DateTime Time;
        public readonly string ChannelName;
        public readonly string ProcessorName;

        public MessageProcessEvent(string channelName, string processorName)
        {
            Time = DateTime.Now;
            ChannelName = channelName;
            ProcessorName = processorName;
        }
    }
}
