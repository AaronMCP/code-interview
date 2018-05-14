using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HYS.Messaging.Objects;

namespace HYS.MessageDevices.MessagePipe.Base
{
    public interface ISender
    {
        bool NotifyMessagePublish(Message message);
        bool NotifyMessageRequest(Message request, out Message response);
    }
}
