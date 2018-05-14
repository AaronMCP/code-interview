using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.IM.Common.Logging
{
    public interface ILog
    {
        void Write(string msg);
        void Write(LogType type, string msg);
        void Write(Exception err);
        void DumpToFile(string folder, string filename, XObject message);
        bool DumpData { get;}
    }
}
