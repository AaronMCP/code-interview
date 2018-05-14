using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.Common.Objects.Logging
{
    public interface ILogging
    {
        void Write(string msg);
        void Write(LogType type, string msg);
        void Write(Exception err);
    }
}
