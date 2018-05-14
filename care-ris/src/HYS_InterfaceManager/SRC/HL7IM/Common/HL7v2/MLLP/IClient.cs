using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    public interface IClient
    {
        bool Open();
        SocketResult SendData(string content);
        bool Close();
    }
}
