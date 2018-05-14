using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Messaging.Objects.RequestModel
{
    /// <summary>
    /// IPullRoute: a logical pathway for pulling communication.
    /// 
    /// Route stands for logical Pull pathway.
    /// Channel stands for physical Remoting/LPC/etc. pathway.
    /// </summary>
    public interface IPullRoute
    {
        string ID { get;}
    }
}
