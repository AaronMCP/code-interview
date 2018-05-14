using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Queuing
{
    public enum ProtocolType
    {
        LPC,                // local/inner process call for publish/subscribe model and request/response model
        MSMQ,               // for publish/subscribe model currently (difficult/low efficiency to implement request/response model)
        RPC_NamedPipe,      // RPC for local machine
        RPC_TCP,            // RPC for LAN/Intranet
        RPC_SOAP,           // RPC for WAN/Internet
    }
}
