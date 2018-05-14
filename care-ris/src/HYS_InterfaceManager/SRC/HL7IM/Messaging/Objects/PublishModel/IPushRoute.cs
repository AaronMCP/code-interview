using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Objects.PublishModel
{
    /// <summary>
    /// IPushRoute: a logical pathway for pushing communication.
    /// 
    /// Route stands for logical Push pathway.
    /// Channel stands for physical MSMQ/LPC/etc. pathway.
    /// </summary>
    public interface IPushRoute
    {
        string ID { get;}
    }
}
