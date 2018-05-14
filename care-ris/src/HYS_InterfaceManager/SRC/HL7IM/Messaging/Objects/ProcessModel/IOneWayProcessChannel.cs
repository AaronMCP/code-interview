using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Messaging.Objects.ProcessModel
{
    public interface IOneWayProcessChannel
    {
        event OneWayMessageProcessHandler OnProcessing;
    }

    /// <summary>
    /// The hanlder implementation should modify the message when it return true. If return false, it should not modify the message.
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public delegate bool OneWayMessageProcessHandler(ref Message msg);
}
