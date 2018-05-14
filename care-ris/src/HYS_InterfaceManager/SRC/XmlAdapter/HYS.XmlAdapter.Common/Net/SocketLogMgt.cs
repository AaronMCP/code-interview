using System;
using System.Collections.Generic;
using System.Text;

namespace HYS.XmlAdapter.Common.Net
{
    public class SocketLogMgt
    {
        public static bool DumpData = false;

        private static Exception _lastError;
        public static Exception LastError
        {
            get { return _lastError; }
        }
        public static string LastErrorInfor
        {
            get
            {
                if (_lastError == null) return "";
                return _lastError.ToString();
            }
        }

        internal static void SetLastError(Exception err)
        {
            SetLastError(null, err);
        }
        internal static void SetLastError(Object sender, Exception err)
        {
            _lastError = err;
            if (OnError != null) OnError(sender, EventArgs.Empty);
        }

        internal static void SetLog(string message)
        {
            SetLog(SocketLogType.Debug, null, message);
        }
        internal static void SetLog(SocketConfig config)
        {
            SetLog(null, config);
        }
        internal static void SetLog(Object sender, string message)
        {
            SetLog(SocketLogType.Debug, sender, message);
        }
        internal static void SetLog(SocketLogType type, string message)
        {
            SetLog(type, null, message);
        }
        internal static void SetLog(Object sender, SocketConfig config)
        {
            if (config == null) return;
            SetLog(SocketLogType.Debug, sender, "---------------------------------");
            SetLog(SocketLogType.Debug, sender, "IP: " + config.IPAddress);
            SetLog(SocketLogType.Debug, sender, "Port: " + config.Port.ToString());
            SetLog(SocketLogType.Debug, sender, "BackLog: " + config.BackLog.ToString());
            SetLog(SocketLogType.Debug, sender, "SendTimeout: " + config.SendTimeout.ToString());
            SetLog(SocketLogType.Debug, sender, "ReceiveTimeout: " + config.ReceiveTimeout.ToString());
            SetLog(SocketLogType.Debug, sender, "ConnectionTimeoutSecond: " + config.ConnectionTimeoutSecond.ToString());
            SetLog(SocketLogType.Debug, sender, "ReceiveEndSign: " + config.ReceiveEndSign);
            SetLog(SocketLogType.Debug, sender, "CodePageName: " + config.CodePageName);
            SetLog(SocketLogType.Debug, sender, "---------------------------------");
        }
        internal static void SetLog(SocketLogType type, Object sender, string message)
        {
            if (OnLog != null) OnLog(type, sender, message);
        }

        public static event EventHandler OnError;
        public static event SocketLogHandler OnLog;
    }

    public delegate void SocketLogHandler(SocketLogType type, Object sender, string message);

    public enum SocketLogType
    {
        Debug,
        Warning,
        Error,
    }
}
