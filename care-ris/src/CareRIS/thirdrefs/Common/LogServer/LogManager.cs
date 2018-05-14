using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Collections.Specialized;
using System.Threading;
using System.Windows.Forms;

using System.Reflection;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net;
using System.Management;
using log4net;
using System.Linq;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace LogServer
{
    public class LogManager : ILogManager
    {
        private long m_lLastDay = -1;
	    private long m_lLastMonth = -1;
	    private long m_lLastYear = -1;
	    private long m_lLastHour = -1;
        private string m_szLogHomeDir = "";
        private string m_szLogReserveDay = "";
        private static object monitorLock = new object();
        private bool isClient = false;

        private string logserviceStatus = string.Empty;
        private static log4net.ILog logger = log4net.LogManager.GetLogger("RISLogger");
        

        public string LogserviceStatus
        {
            get { return logserviceStatus; }
            set { logserviceStatus = value; }
        }

        public long lLastDay
        {
            get
            {
                return m_lLastDay;
            }
            set
            {
                m_lLastDay = value;
            }
        }

        public long lLastMonth
        {
            get
            {
                return m_lLastMonth;
            }
            set
            {
                m_lLastMonth = value;
            }
        }
        public long lLastYear
        {
            get
            {
                return m_lLastYear;
            }
            set
            {
                m_lLastYear = value;
            }
        }
        public long lLastHour
        {
            get
            {
                return m_lLastHour;
            }
            set
            {
                m_lLastHour = value;
            }
        }

        public string LogHomeDir
        {
            get
            {
                return m_szLogHomeDir;
            }
            set
            {
                m_szLogHomeDir = value;
            }

            
        }

        public string LogReserveDay
        {
            get
            {
                return m_szLogReserveDay;
            }
            set
            {
                m_szLogReserveDay = value;
            }

          
        }
        private string szLocation;

        public string SzLocation
        {
            get { return szLocation; }
            set { szLocation = value; }
        }

        private string loginName = "not set";

        public string LoginName
        {
            get { return loginName; }
            set { loginName = value; }
        }

        private string hospitalName = "not set";

        public string HospitalName
        {
            get { return hospitalName; }
            set { hospitalName = value; }
        }


        public LogManager()
        {
            string szDirectoryRoot = System.IO.Directory.GetDirectoryRoot(Environment.SystemDirectory);
            string szReserverDay = string.Empty;
            string szHomeDirPath = string.Empty;
            string subSystem = System.Configuration.ConfigurationManager.AppSettings["SubSystem"];
            string szHomeDirPathDefault = szDirectoryRoot + "Carestream\\"+subSystem+"\\Log";
            string szReserverDayKeyDefaultval = "15";
            //Get the log file directory from the configuration file
            string szLogConfigDetails = ConfigurationManager.AppSettings["ConfigDicFilePath"];
            string szLocation = ConfigurationManager.AppSettings["CurrentLocation"];

            //add to properties
            SzLocation = "SmartClient-"+ szLocation;


            DsConfigDic dsConfigDic = new DsConfigDic();
            DsConfigDic.ConfigDicRow rowLogHomeDirCfg = null;
            DsConfigDic.ConfigDicRow rowReserverDay = null;

            //added for log service
            LogserviceStatus = ConfigurationManager.AppSettings["ris_log_webservice"];

            try
            {
                if(szLocation == "Client")
                    dsConfigDic.ReadXml(szLogConfigDetails);
                // get the home directory where the log folder will be created
                if (szLocation == "Client")
                {
                    rowLogHomeDirCfg = dsConfigDic.ConfigDic.FindByConfigNameModuleID("LogHomeDir", "0A00");
                    rowReserverDay = dsConfigDic.ConfigDic.FindByConfigNameModuleID("LogReserveDays", "0A00");
                    isClient = true;
                }
                else if (szLocation == "Server")
                {
                    //rowLogHomeDirCfg.Value = szDirectoryRoot + "Kodak\\GCRIS2.0\\Server\\Log";
                    //rowReserverDay.Value = "15";
                    isClient = false;
                }
                if (rowLogHomeDirCfg != null)
                {
                    szHomeDirPath = rowLogHomeDirCfg.Value;

                }
                else
                {
                    if (szLocation == "Server")
                        szHomeDirPath = szDirectoryRoot + "Carestream\\"+subSystem+"\\Server\\Log";
                    else
                        szHomeDirPath = szDirectoryRoot + "Carestream\\" + subSystem + "\\Client\\Log";
                        

                }

                if (rowReserverDay != null)
                {
                    szReserverDay = rowReserverDay.Value;

                }
                else
                {
                    szReserverDay = szReserverDayKeyDefaultval;

                }
            }
            catch
            {
                if (szLocation == "Server")
                    szHomeDirPath = szDirectoryRoot + "Carestream\\" + subSystem + "\\Server\\Log";
                else 
                    szHomeDirPath = szHomeDirPathDefault;
                szReserverDay = szReserverDayKeyDefaultval;
            }
            m_szLogHomeDir = szHomeDirPath;
            m_szLogReserveDay = szReserverDay;

           

        }


        public virtual void Performance(string strCode,string strAction,string strDescription)
        {
            string strLog = string.Format("{0},{1},{2},'{3}'", strCode, strAction,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff"), strDescription);
            WriteZZLog(strLog);
        }

        public virtual void Debug(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            //long lSeverity = 1;
            //SendLog(lModule, szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);

            WriteLocalLog("Debug", lModule, szModuleInstanceName, lCode, szDescription, szSourceFile, lLineNo);
           

            
        }

        public virtual void Info(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            //long lSeverity = 2;
            //SendLog(lModule, szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);

            WriteLocalLog("Info", lModule, szModuleInstanceName, lCode, szDescription, szSourceFile, lLineNo);


          
        }
        public virtual void Warn(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            //long lSeverity = 3;
            //SendLog(lModule, szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);

            WriteLocalLog("Warn", lModule, szModuleInstanceName, lCode, szDescription, szSourceFile, lLineNo);


          
        }
        public virtual void Error(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            //long lSeverity = 4;
            //SendLog(lModule, szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);

            WriteLocalLog("Error", lModule, szModuleInstanceName, lCode, szDescription, szSourceFile, lLineNo);

           
        }
        public virtual void Fatal(long lModule, string szModuleInstanceName, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            //long lSeverity = 5;
            //SendLog(lModule, szModuleInstanceName, lSeverity, lCode, szDescription, szExtension, szSourceFile, lLineNo);

            WriteLocalLog("Fatal", lModule, szModuleInstanceName, lCode, szDescription, szSourceFile, lLineNo);


           
        }

        private static object lockobj = new object();

        public void WriteLocalLog(string LogType,long lModule,string szModuleInstanceName,long lCode,string szDescription,string szSourceFile,long lLineNo)
        {


           
            lock (lockobj)
            {



                DateTime dtLocalTime = DateTime.Now;
                long lThisDay, lThisMonth, lThisYear, lThisHour;
                lThisDay = dtLocalTime.Day;
                lThisMonth = dtLocalTime.Month;
                lThisYear = dtLocalTime.Year;
                lThisHour = dtLocalTime.Hour;

                if ((lThisDay != lLastDay) || (lThisMonth != lLastMonth) || (lThisYear != lLastYear))
                {
                    // delete old log files
                    DeleteOldLogFiles(dtLocalTime, Convert.ToInt32(LogReserveDay), LogHomeDir);
                }

                string szLogForTodaysDate = LogHomeDir + "\\" + string.Format("{0:####}" + "-" + "{1:0#}" + "-" + "{2:0#}", lThisYear, lThisMonth, lThisDay);
                string szLogForCurrentHourOfTodaysDate = szLogForTodaysDate + "\\" + string.Format("{0:####}" + "-" + "{1:0#}" + "-" + "{2:0#}" + "-" + "{3:0#}" + ".log", lThisYear, lThisMonth, lThisDay, lThisHour);
                log4net.Repository.Hierarchy.Hierarchy h = (log4net.Repository.Hierarchy.Hierarchy)log4net.LogManager.GetRepository();
                foreach (log4net.Appender.IAppender a in h.Root.Appenders)
                {
                    if (a is log4net.Appender.FileAppender)
                    {
                        log4net.Appender.FileAppender fa = (log4net.Appender.FileAppender)a;
                        string logFileLocation = szLogForCurrentHourOfTodaysDate;
                        fa.File = logFileLocation;
                        fa.ActivateOptions();
                        break;
                    }
                }

                log4net.ILog logger = log4net.LogManager.GetLogger("RISLogger");
                switch (LogType)
                {
                    case "Performance":
                        logger.Debug("'" + lModule + "'-'" + szModuleInstanceName + "'-'" + lCode + "'-'" + szDescription + "'-'" + szSourceFile + "'-'" + lLineNo + "'");
                        break;

                    case "Debug":
                        logger.Debug("'" + lModule + "'-'" + szModuleInstanceName + "'-'" + lCode + "'-'" + szDescription + "'-'" + szSourceFile + "'-'" + lLineNo + "'");
                        break;
                    case "Info":
                        logger.Info("'" + lModule + "'-'" + szModuleInstanceName + "'-'" + lCode + "'-'" + szDescription + "'-'" + szSourceFile + "'-'" + lLineNo + "'");
                        break;
                    case "Warn":
                        logger.Warn("'" + lModule + "'-'" + szModuleInstanceName + "'-'" + lCode + "'-'" + szDescription + "'-'" + szSourceFile + "'-'" + lLineNo + "'");
                        break;
                    case "Error":
                        logger.Error("'" + lModule + "'-'" + szModuleInstanceName + "'-'" + lCode + "'-'" + szDescription + "'-'" + szSourceFile + "'-'" + lLineNo + "'");
                        break;
                    case "Fatal":
                        logger.Fatal("'" + lModule + "'-'" + szModuleInstanceName + "'-'" + lCode + "'-'" + szDescription + "'-'" + szSourceFile + "'-'" + lLineNo + "'");
                        break;
                }

                lLastHour = lThisHour;
                lLastDay = lThisDay;
                lLastMonth = lThisMonth;
                lLastYear = lThisYear;
            }
 
        }


        public string GetLogHomeDir()
        {
            return this.m_szLogHomeDir;
        }

        public string GetLogReserveDay()
        {
            return this.m_szLogReserveDay;
        }

        public virtual void SendLog(long lModule, string szModuleInstanceName, long lSeverity, long lCode, string szDescription, string szExtension, string szSourceFile, long lLineNo)
        {
            DateTime dtSystemTime = DateTime.Now;
           StringBuilder szLog = new StringBuilder();
           szLog.AppendFormat("{0:u}"+ " ", dtSystemTime);
           szLog.AppendFormat("`" + "{0:d}" + "`" + "{1}" + "`" + "{2:d}", lModule, szModuleInstanceName, lSeverity);
           szLog.AppendFormat("`" + "{0}" + "`" + "{1}" + "`" + "{2}", szDescription, szExtension, szSourceFile);
           szLog.AppendFormat("`" + "{0:d}" + "\r\n", lLineNo);
           WriteLog(szLog);

        }
        private void WriteLog(StringBuilder szLog)
        {
            try
            {
                Monitor.Enter(monitorLock);
                DateTime dtLocalTime = DateTime.Now;
                long lThisDay, lThisMonth, lThisYear, lThisHour;
                lThisDay = dtLocalTime.Day;
                lThisMonth = dtLocalTime.Month;
                lThisYear = dtLocalTime.Year;
                lThisHour = dtLocalTime.Hour;
                string szLogForTodaysDate = string.Empty;
                string szLogForCurrentHourOfTodaysDate = string.Empty;

                //string szDirectoryRoot = System.IO.Directory.GetDirectoryRoot(Environment.SystemDirectory);
                //string szReserverDay = string.Empty;
                //string szHomeDirPath = string.Empty;

                //string szHomeDirPathDefault = szDirectoryRoot + "Kodak\\GCRIS2.0\\Log";
                //string szReserverDayKeyDefaultval = "30";
                ////Get the log file directory from the configuration file
                //string szLogConfigDetails = ConfigurationManager.AppSettings["ConfigDicFilePath"];
                //string szLocation = ConfigurationManager.AppSettings["CurrentLocation"];
                //DsConfigDic dsConfigDic = new DsConfigDic();
                //DsConfigDic.ConfigDicRow rowLogHomeDirCfg = null;
                //DsConfigDic.ConfigDicRow rowReserverDay = null;
                //try
                //{
                //  dsConfigDic.ReadXml(szLogConfigDetails);
                //  // get the home directory where the log folder will be created
                //  if (szLocation == "Client")
                //  {
                //      rowLogHomeDirCfg = dsConfigDic.ConfigDic.FindByConfigNameModuleID("LogHomeDir", "0A00");
                //      rowReserverDay = dsConfigDic.ConfigDic.FindByConfigNameModuleID("LogReserveDays", "0A00");
                //  }
                //  else if (szLocation == "Server")
                //  {
                //      rowLogHomeDirCfg = dsConfigDic.ConfigDic.FindByConfigNameModuleID("LogHomeDir", "0B00");
                //      rowReserverDay = dsConfigDic.ConfigDic.FindByConfigNameModuleID("LogReserveDays", "0B00");
                //  }
                //  if (rowLogHomeDirCfg != null)
                //  {
                //      szHomeDirPath = rowLogHomeDirCfg.Value;

                //  }
                //  else
                //  {
                //      szHomeDirPath = szHomeDirPathDefault;

                //  }

                //  if (rowReserverDay != null)
                //  {
                //      szReserverDay = rowReserverDay.Value;

                //  }
                //  else
                //  {
                //      szReserverDay = szReserverDayKeyDefaultval;

                //  }
                //}
                //catch 
                //{
                //    szHomeDirPath = szHomeDirPathDefault;
                //    szReserverDay = szReserverDayKeyDefaultval;
                //}

                if (!Directory.Exists(LogHomeDir))
                {
                    Directory.CreateDirectory(LogHomeDir);
                }

                if ((lThisDay != lLastDay) || (lThisMonth != lLastMonth) || (lThisYear != lLastYear))
                {
                    // delete old log files
                    DeleteOldLogFiles(dtLocalTime, Convert.ToInt32(LogReserveDay), LogHomeDir);
                }

                if (!Directory.Exists(LogHomeDir))
                {
                    // Create the directory it does not exist.
                    Directory.CreateDirectory(LogHomeDir);
                }

                if (Directory.Exists(LogHomeDir))
                {
                    //check if there is sub directory for today's date
                    szLogForTodaysDate = LogHomeDir + "\\" + string.Format("{0:####}" + "-" + "{1:0#}" + "-" + "{2:0#}", lThisYear, lThisMonth, lThisDay);
                    if (!Directory.Exists(szLogForTodaysDate))
                    {
                        Directory.CreateDirectory(szLogForTodaysDate);
                    }
                }
                if (Directory.Exists(szLogForTodaysDate))
                {
                    //check  if for the current day the log file exists for the current hour
                    szLogForCurrentHourOfTodaysDate = szLogForTodaysDate + "\\" + string.Format("{0:####}" + "-" + "{1:0#}" + "-" + "{2:0#}" + "-" + "{3:0#}" + ".log", lThisYear, lThisMonth, lThisDay, lThisHour);
                    if (!File.Exists(szLogForCurrentHourOfTodaysDate))
                    {
                        File.Create(szLogForCurrentHourOfTodaysDate).Close();
                        using (StreamWriter sw = new StreamWriter(szLogForCurrentHourOfTodaysDate))
                        {
                            sw.Write(szLog);
                            sw.Close();
                        }

                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(szLogForCurrentHourOfTodaysDate))
                        {
                            sw.Write(szLog);
                            sw.Close();
                        }
                    }

                }
                lLastHour = lThisHour;
                lLastDay = lThisDay;
                lLastMonth = lThisMonth;
                lLastYear = lThisYear;
                Monitor.Exit(monitorLock);
            }
            catch
            {
                Monitor.Exit(monitorLock);
                if (isClient)
                    MessageBox.Show("Log directory access deny, Please check directory access authorization!");
                else
                    Console.Beep();
                
            }

        }

        private void DeleteOldLogFiles(DateTime dtLocalTime,int iLogReserveDays, string szHomeDirPath )
        {
            try
            {
                //delete obsolete log files
                deleteOldFiles(szHomeDirPath, iLogReserveDays);
            }
            catch (System.Exception ex)
            {
                WriteZZLog("Error on DeleteOldLogFiles, " + ex.Message);
            }
        }

        static private void deleteOldFiles(string path, double daysOld)
        {
            deleteOldFiles(path, DateTime.Now.AddDays(-daysOld));
        }

        static private void deleteOldFiles(string path, DateTime olderThanDate)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);

                    FileInfo[] files = dirInfo.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        if (file.LastWriteTime < olderThanDate)
                        {
                            file.IsReadOnly = false;
                            file.Delete();
                        }
                    }

                    // Now recurse down the directories
                    DirectoryInfo[] dirs = dirInfo.GetDirectories();
                    foreach (DirectoryInfo dir in dirs)
                    {
                        deleteOldFiles(dir.FullName, olderThanDate);
                        deleteOldFiles(path + "\\" + dir.FullName, olderThanDate);
                        if (dir.CreationTime < olderThanDate)
                        {
                            dir.Delete(true);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                WriteZZLog("Error on deleteOldFiles, " + ex.Message);
            }
        }

        public static void WriteZZLog(string txt)
        {
            string filepath = @"c:\zzLog.txt";
            if (System.IO.File.Exists(filepath))
            {
                System.IO.StreamWriter sw = System.IO.File.AppendText(filepath);
                sw.WriteLine(txt);
                sw.WriteLine("\r\n");
                sw.Close();
            }
        }



        public  string GetCurrentMachineIPAddress()
        {
            string _address = string.Empty;
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    _address = ip.ToString();
                    break;
                }
            }

            return _address;
        }

        public  string GetMACAddressByWMI()
        {
            string macaddr = string.Empty;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"] == true)
                {
                    macaddr = mo["MacAddress"].ToString(); ;
                }
                mo.Dispose();
            }
            return macaddr;
        }
    }
}
