using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HYS.IM.Common.WCFHelper.SwA
{
    public class SwaLogMgt
    {
        public static bool DumpData = false;
        public static event SwaLogHandler OnLog;

        internal static void SetLog(object sender, string message)
        {
            SetLog(SwaLogType.Debug, sender, message);
        }
        internal static void SetLog(object sender, Exception error)
        {
            SetLog(SwaLogType.Error, sender, error.ToString());
        }
        internal static void SetLog(object sender, ArraySegment<byte> data)
        {
            if (!DumpData) return;
            byte[] buffer = new byte[data.Count];
            Array.Copy(data.Array, data.Offset, buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer);
            SetLog(SwaLogType.Information, sender, message);
        }
        internal static void SetLog(SwaLogType type, object sender, string message)
        {
            if (OnLog != null) OnLog(type, sender, message);
        }
    }

    public delegate void SwaLogHandler(SwaLogType type, object sender, string message);

    public enum SwaLogType
    {
        Debug,
        Information,
        Error,
    }
}
