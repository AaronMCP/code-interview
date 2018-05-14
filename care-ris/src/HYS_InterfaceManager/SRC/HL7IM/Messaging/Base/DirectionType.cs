using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Base
{
    [Flags]
    public enum DirectionTypes
    {
        Unknown = 0x0,
        Inbound = 0x1,
        Outbound = 0x2,
    }
}
