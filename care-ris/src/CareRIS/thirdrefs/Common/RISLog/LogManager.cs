using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;

namespace CareRIS.Log
{
    public class LogManager : ILogManager
    {

        private static string m_szLogHomeDir = "";
        private static object monitorLock = new object();
        private static LogManager instance;

        private static string _logPath;

        public static string LogPath
        {
            get { return _logPath; }
            set { _logPath = value; }
        }

        private static string _logLevel;

        public static string LogLevel
        {
            get { return _logLevel; }
            set { _logLevel = value; }
        }


        private LogManager()
        {

        }

        public static LogManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LogManager();
                    instance.SetLogPath();
                    instance.DelExpireFiles();
                }
                return instance;
            }
        }
        private void SetLogPath()
        {
            /*在宿主应用程序的配置文件中，加入日志文件的配置项，如下：
                         * 如果不加， 默认为在当前应用程序目录中自动创建一个Log子目录, 一天一个日志文件
                        <?xml version="1.0" encoding="utf-8" ?>
                        <configuration>
                          <appSettings>
                            <add key="logpath" value="c:\\logdemo"/>
                            <add key="loglevel" value="4"/>
                          </appSettings>
                        </configuration>
             */
            string strPath = string.IsNullOrWhiteSpace(_logPath) ? ConfigurationManager.AppSettings["logpath"] : _logPath;
            if (string.IsNullOrWhiteSpace(strPath))
            {
                m_szLogHomeDir = System.Environment.CurrentDirectory + "\\Log";
            }
            else
            {
                m_szLogHomeDir = strPath;
            }
        }
        /// <summary>
        ///   Debug Log Info.<br></br>
        /// </summary>
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public virtual void Debug(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            long lSeverity = 1;
            SendLog(szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);
        }

        /// <summary>
        ///   Inform Log Info.<br></br>
        /// </summary>    
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public virtual void Info(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            long lSeverity = 2;
            SendLog(szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);
        }

        /// <summary>
        ///   Warning Log Info.<br></br>
        /// </summary>      
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public virtual void Warn(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            long lSeverity = 3;
            SendLog(szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);
        }

        /// <summary>
        ///   Error Log Info.<br></br>
        /// </summary>     
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public virtual void Error(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            long lSeverity = 4;
            SendLog(szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);
        }

        /// <summary>
        ///   Fatal Log Info.<br></br>
        /// </summary>  
        /// <param name="szModuleInstanceName">Module Instance Name</param>
        /// <param name="lCode">Internal ID</param>
        /// <param name="szDescription">Info description string.</param>
        /// <param name="szExtension">extension description</param>
        /// <param name="szSourceFile">File Location</param>
        /// <param name="lLineNo">Line number of File</param>
        public virtual void Fatal(string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            long lSeverity = 5;
            SendLog(szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);
        }

        public virtual void SendLog(string szModuleInstanceName, long lSeverity, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {

            string strLoglevel = string.IsNullOrWhiteSpace(_logLevel) ? ConfigurationManager.AppSettings["loglevel"] : _logLevel;
            int nLoglevel=4;
            try
            {
                if (!string.IsNullOrWhiteSpace(strLoglevel))
                {
                    nLoglevel = int.Parse(strLoglevel);
                }
            }
            catch (Exception exx)
            {
            }
            if (lSeverity < nLoglevel)
            {
                return;
            }

            StringBuilder szLog = new StringBuilder();
            string strLevel = "";
            switch (lSeverity)
            {
                case 1:
                    strLevel = "debug  ";
                    break;
                case 2:
                    strLevel = "info   ";
                    break;
                case 3:
                    strLevel = "warning";
                    break;
                case 4:
                    strLevel = "error  ";
                    break;
                case 5:
                    strLevel = "fatal  ";
                    break;
                default:
                    strLevel = "unknow";
                    break;
            }
            szLog.AppendFormat("{0}:" + "{1:u}" + " ", strLevel, DateTime.Now);
            szLog.AppendFormat("`" + "`" + "{0}", szModuleInstanceName);
            szLog.AppendFormat("`" + "{0}" + "`" + "{1}" + "`" + "{2}", szDescription, szExtension, szSourceFile);
            szLog.AppendFormat("`" + "{0:d}" + "\r\n", lLineNo);
            WriteLog(szLog);

        }
        private void WriteLog(StringBuilder szLog)
        {
            try
            {
                Monitor.Enter(monitorLock);



                if (!Directory.Exists(m_szLogHomeDir))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(m_szLogHomeDir);
                }


                string szLogFile = m_szLogHomeDir + "\\" + string.Format("{0:####}" + "-" + "{1:0#}" + "-" + "{2:0#}" + "-" + "{3:0#}" + ".log", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,DateTime.Now.Hour);
                if (!File.Exists(szLogFile))
                {
                    File.Create(szLogFile).Close();
                    using (StreamWriter sw = new StreamWriter(szLogFile))
                    {
                        sw.Write(szLog);
                        sw.Close();
                    }

                }
                else
                {
                    using (StreamWriter sw = File.AppendText(szLogFile))
                    {
                        sw.Write(szLog);
                        sw.Close();
                    }
                }




                Monitor.Exit(monitorLock);
            }
            catch
            {
                Monitor.Exit(monitorLock);
                throw new Exception("Log directory access deny, Please check directory access authorization!");

            }

        }

        /// <summary>
        /// 删除过期的日志文件，保留15天
        /// </summary>
        private void DelExpireFiles()
        {
            DateTime dtReserve = DateTime.Now.AddDays(-15);
            if (!Directory.Exists(m_szLogHomeDir))
            {
                return;
            }

            foreach (string strFile in Directory.GetFiles(m_szLogHomeDir))
            {

                FileInfo fi = new FileInfo(strFile);
                if (fi.LastWriteTime < dtReserve)
                {
                    try
                    {
                        System.IO.File.Delete(strFile);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

    }
}
