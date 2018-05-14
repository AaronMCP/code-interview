using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Messaging.Objects.ProcessModel
{
    public interface IDuplexProcessChannel
    {
        event DuplexMessagePreProcessHandler OnPreProcessing;
        event DuplexMessagePostProcessHanlder OnPostProcessing;
    }

    /// <summary>
    /// The hanlder implementation should modify the requesting message when it return true. If return false, it should not modify the requesting message.
    /// </summary>
    /// <param name="requestMsg"></param>
    /// <returns></returns>
    public delegate bool DuplexMessagePreProcessHandler(ref Message requestMsg);
    /// <summary>
    /// The hanlder implementation should modify the responsing message when it return true. If return false, it should not modify the responsing message.
    /// The requesting message should not be modified at any time.
    /// </summary>
    /// <param name="requestMsg"></param>
    /// <param name="responseMsg"></param>
    /// <returns></returns>
    public delegate bool DuplexMessagePostProcessHanlder(Message requestMsg, ref Message responseMsg);
}
