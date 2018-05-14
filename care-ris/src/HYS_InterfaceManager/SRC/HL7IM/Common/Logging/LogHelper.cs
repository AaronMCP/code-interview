using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HYS.Common.Xml;

namespace HYS.IM.Common.Logging
{
    public class LogHelper
    {
        public const string DateFomat = "yyyyMMdd";
        public const string DateTimeFomat = "yyyy-MM-dd HH:mm:ss.ffffff";
        public const string FilePostfix = ".log";
        public const string FileDirectory = "Log";
        public const int LogProcessTime = 12;

        private static LogControler _xmlLog;
        private static LogControler _appLog;

        public static void EnableXmlLogging(LogControler log)
        {
            _xmlLog = log;
            XObjectManager.OnError += new XObjectExceptionHandler(XObjectManager_OnError);
        }
        public static void EnableApplicationLogging(LogControler log)
        {
            _appLog = log;
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        }

        private static void XObjectManager_OnError(object source, Exception error)
        {
            _xmlLog.Write(LogType.Error, "[Xml error]\r\n" +
                "Source: " + source + "\r\n" +
                "Exception: " + error);
        }
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            _appLog.Write(LogType.Error, "[Application error]\r\n" +
                "Source: " + sender + "\r\n" +
                "Exception: " + e.Exception);
        }
    }
}
