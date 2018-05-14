using HYS.Common.Dicom.Net;
using System;
using System.Collections.Generic;

namespace HYS.Common.Dicom
{
    public class DLogMgt1
    {
        public static bool DumpData = false;

        private static Exception _lastError;
        public static Exception LastError
        {
            get { return _lastError; }
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
            SetLog(DLogType.Debug, null, message);
        }
        internal static void SetLog(Object sender, Session session)
        {
            if (session == null) return;
            SetLog(DLogType.Debug, sender, "---------------------------------");
            SetLog(DLogType.Debug, sender, "Calling IP: " + session.CallingIP);
            SetLog(DLogType.Debug, sender, "Calling AETitle: " + session.CallingAE);
            SetLog(DLogType.Debug, sender, "Called AETitle: " + session.CalledAE);
            SetLog(DLogType.Debug, sender, "---------------------------------");
        }
        internal static void SetLog(Object sender, string message)
        {
            SetLog(DLogType.Debug, sender, message);
        }
        internal static void SetLog(DLogType type, string message)
        {
            SetLog(type, null, message);
        }
        internal static void SetLog(DLogType type, Object sender, string message)
        {
            if (OnLog != null) OnLog(type, sender, message);
        }

        public static event EventHandler OnError;
        public static event DLogHandler OnLog;
    }

    public delegate void DLogHandler(DLogType type, Object sender, string message);
}
