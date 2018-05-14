using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Platform.CrossCutting.LogContract
{
    public interface ICommonLog
    {
        void Log(LogLevel logLevel, object message);
    }
}
