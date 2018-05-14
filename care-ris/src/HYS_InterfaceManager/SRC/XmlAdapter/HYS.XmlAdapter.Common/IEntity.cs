using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.XmlAdapter.Common
{
    public interface IEntity
    {
        event RequestEventHandler OnRequest;
        bool IsListening { get;}
        bool Start();
        bool Stop();
    }

    public delegate bool RequestEventHandler(string receiveData, ref string sendData);
}
