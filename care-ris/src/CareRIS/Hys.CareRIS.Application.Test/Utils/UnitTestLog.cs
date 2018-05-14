using System.Diagnostics;
using Hys.Platform.CrossCutting.LogContract;

namespace Hys.Consultation.Application.Test.Utils
{
    internal class UnitTestLog : ICommonLog
    {
        public void Log(LogLevel logLevel, object message)
        {
            Debug.WriteLine(message);
        }
    }
}