using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Base
{
    [Flags]
    public enum InteractionTypes
    {
        Unknown = 0x0,
        Publisher = 0x1,
        Subscriber = 0x2,
        Requester = 0x4,
        Responser = 0x8,
    }
}
