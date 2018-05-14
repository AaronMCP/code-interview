using System;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Base
{
    /// <summary>
    /// Please use this class in stand alone application (not adapter plugin), in order to prevent duplicated logging.
    /// </summary>
    public class LoggingHelper
    {
        static Logging _dbLog;
        static Logging _xmlLog;
        static Logging _appLog;

        public static void EnableXmlLogging(Logging log)
        {
            _xmlLog = log;
            XObjectManager.OnError += new XObjectExceptionHandler(XObjectManager_OnError);
        }
        //public static void EnableDatabaseLogging(Logging log)
        //{
        //    _dbLog = log;
        //    DataAccessHelper.OnError += new DataAccessExceptionHanlder(DataAccessHelper_OnError);
        //}
        public static void EnableDatabaseLogging(DataBase db, Logging log)
        {
            _dbLog = log;
            db.OnError += new DataAccessExceptionHanlder(DataAccessHelper_OnError);
        }
        public static void EnableApplicationLogging(Logging log)
        {
            _appLog = log;
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
        }

        static void XObjectManager_OnError(object source, Exception error)
        {
            _xmlLog.Write(LogType.Error, "[Xml error]\r\n" +
                "Source: " + source + "\r\n" +
                "Exception: " + error);
        }
        static void DataAccessHelper_OnError(string cnn, string sql, Exception err)
        {
            _dbLog.Write(LogType.Error, "[Database error]\r\n" +
                "Connection String: " + cnn + "\r\n" +    //contains database password
                "SQL Statement: " + sql + "\r\n" +
                "Exception: " + err);
        }
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            _appLog.Write(LogType.Error, "[Application error]\r\n" +
                "Source: " + sender + "\r\n" +
                "Exception: " + e.Exception);
        }
    }
}
