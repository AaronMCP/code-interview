using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.HL7v2.MLLP
{
    internal interface ISocketWorker
    {
        void Open();
        void Close();
        string Caption { get; }
        DateTime ActiveDT { get; }
    }
}
