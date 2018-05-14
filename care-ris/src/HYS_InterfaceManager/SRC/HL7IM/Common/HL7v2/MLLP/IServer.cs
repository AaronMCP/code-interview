using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public interface IServer
    {
        event RequestEventHandler OnRequest;
        bool IsListening { get; }
        bool Start();
        bool Stop();
    }

    public delegate bool RequestEventHandler(string receiveData, ref string sendData);
}
