using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HYS.Common.Xml;

namespace HYS.IM.Common.Logging
{
    /// <summary>
    /// GCGateway Logging.
    /// </summary>
    /// 
    public class LogControler : ILog
    {
        public static string BaseDirectory = "";       // for web use 20081112
        private string _baseDirectory
        {
            get
            {
                if (BaseDirectory == "" || BaseDirectory == null) return Application.StartupPath;
                else return BaseDirectory;
            }
        }

        #region Local members
        private string _adapterName = "";
        private LogType _logTypeLevel = LogType.Debug;
        public LogType LogTypeLevel
        {
            get { return _logTypeLevel; }
        }
        private int _fileDuration = 0;
        public int FileDuration
        {
            get { return _fileDuration; }
        }
        private string _assemblyName = "";
        private string _fullFileName = "";

        private DateTime _lastDay;
        private bool _isLogProcess = false;

        private bool _isAutoManageFile = true;
        public bool IsAutoManageFile                // Deside whether managing files automatically
        {
            get { return _isAutoManageFile; }
            set { _isAutoManageFile = value; }
        }

        private bool _dumpData;
        public bool DumpData
        {
            get { return _dumpData; }
        }
        #endregion

        #region Constructors
        public LogControler()
        {
            PreLoading();
            CheckDirectory();

            _lastDay = DateTime.Now;
            string today = _lastDay.ToString(LogHelper.DateFomat);

            _assemblyName = Path.GetExtension(Application.ProductName).Replace(".", "");
            if (_assemblyName == "")
            {
                _assemblyName = Application.ProductName;
            }
            string fileName = CheckNameValid(_adapterName + "_" + _assemblyName + "_" + today + LogHelper.FilePostfix);
            _fullFileName = _baseDirectory + "\\" + LogHelper.FileDirectory + "\\" + fileName;

            ManageLogDirectory(_lastDay);
        }

        public LogControler(string assemblyName)
        {
            PreLoading();
            CheckDirectory();

            _lastDay = DateTime.Now;
            string today = _lastDay.ToString(LogHelper.DateFomat);

            this._assemblyName = Path.GetFileNameWithoutExtension(assemblyName);
            string fileName = CheckNameValid(_adapterName + "_" + this._assemblyName + "_" + today + LogHelper.FilePostfix);
            _fullFileName = _baseDirectory + "\\" + LogHelper.FileDirectory + "\\" + fileName;

            ManageLogDirectory(_lastDay);
        }

        public LogControler(string assemblyName, LogType typeLevel)
        {
            PreLoading();
            CheckDirectory();

            _lastDay = DateTime.Now;
            string today = _lastDay.ToString(LogHelper.DateFomat);

            this._assemblyName = Path.GetFileNameWithoutExtension(assemblyName);
            string fileName = CheckNameValid(_adapterName + "_" + this._assemblyName + "_" + today + LogHelper.FilePostfix);
            _fullFileName = _baseDirectory + "\\" + LogHelper.FileDirectory + "\\" + fileName;

            _logTypeLevel = typeLevel;
            _fileDuration = 0;

            ManageLogDirectory(_lastDay);
        }

        public LogControler(string assemblyName, int logFileDuration)
        {
            PreLoading();
            CheckDirectory();

            _lastDay = DateTime.Now;
            string today = _lastDay.ToString(LogHelper.DateFomat);

            this._assemblyName = Path.GetFileNameWithoutExtension(assemblyName);
            string fileName = CheckNameValid(_adapterName + "_" + this._assemblyName + "_" + today + LogHelper.FilePostfix);
            _fullFileName = _baseDirectory + "\\" + LogHelper.FileDirectory + "\\" + fileName;

            _logTypeLevel = LogType.Debug;
            _fileDuration = logFileDuration;

            ManageLogDirectory(_lastDay);
        }

        public LogControler(string assemblyName, LogType typeLevel, int logFileDuration)
        {
            PreLoading();
            CheckDirectory();

            _lastDay = DateTime.Now;
            string today = _lastDay.ToString(LogHelper.DateFomat);

            this._assemblyName = Path.GetFileNameWithoutExtension(assemblyName);
            string fileName = CheckNameValid(_adapterName + "_" + this._assemblyName + "_" + today + LogHelper.FilePostfix);
            _fullFileName = _baseDirectory + "\\" + LogHelper.FileDirectory + "\\" + fileName;

            _logTypeLevel = typeLevel;
            _fileDuration = logFileDuration;

            ManageLogDirectory(_lastDay);
        }

        public LogControler(string assemblyName, LogConfig config)
            : this((config.HostName != null && config.HostName.Length > 0) ?
            config.HostName + "_" + assemblyName : assemblyName,
            config.LogType, config.DurationDay)
        {
            _dumpData = config.DumpData;
        }

        #endregion

        #region Load DeviceDir
        public void PreLoading()
        {
            //// Load DeviceDir
            //DeviceDirManager DeviceMgt = new DeviceDirManager();
            //DeviceMgt.FileName = _baseDirectory + "\\" + DeviceDirManager.IndexFileName;
            //if (DeviceMgt.LoadDeviceDir())
            //{
            //    _adapterName = DeviceMgt.DeviceDirInfor.Header.Name;
            //    _logTypeLevel = DeviceMgt.DeviceDirInfor.LogInfo.LogType;
            //    _fileDuration = DeviceMgt.DeviceDirInfor.LogInfo.FileDuration;
            //}
            //else
            //{
            //    _adapterName = "GCGateway";
            //    _logTypeLevel = LogType.Debug;
            //    _fileDuration = 0;

            //    //WriteLogException("Load DeviceDir failed in log module! Set adapterName as default value: \"GCGateway\"");
            //}

            _adapterName = "eHealth";
            _logTypeLevel = LogType.Debug;
            _fileDuration = 0;
        }
        #endregion

        #region DateTiem Info & DataManage
        private void CheckDirectory()
        {
            string directory = _baseDirectory + "\\" + LogHelper.FileDirectory;
            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
            catch (Exception err)
            {
                WriteLogException(err.Message);
            }
        }

        private string CheckNameValid(string name)
        {
            string originName = name;
            try
            {
                #region Check existing of the invalid character
                if (name.Contains("\\"))
                {
                    name.Replace("\\", "");
                }
                if (name.Contains("/"))
                {
                    name.Replace("/", "");
                }
                if (name.Contains(":"))
                {
                    name.Replace(":", "");
                }
                if (name.Contains("*"))
                {
                    name.Replace("*", "");
                }
                if (name.Contains("?"))
                {
                    name.Replace("?", "");
                }
                if (name.Contains("\""))
                {
                    name.Replace("\"", "");
                }
                if (name.Contains(">"))
                {
                    name.Replace(">", "");
                }
                if (name.Contains("<"))
                {
                    name.Replace("<", "");
                }
                if (name.Contains("|"))
                {
                    name.Replace("|", "");
                }
                if (name.StartsWith("."))
                {
                    name.Remove(0, 1);
                }
                #endregion
            }
            catch (Exception err)
            {
                WriteLogException(err.Message);
            }

            if (name == originName)
            {
                return name;
            }
            else
            {
                WriteLogException("The module name is invalid as part of file name and it has been changed to: " + name);
                return name;
            }
        }

        private string GetDateTimeString(DateTime now)
        {
            try
            {
                return now.ToString(LogHelper.DateTimeFomat);
            }
            catch (Exception err)
            {
                WriteLogException(err.Message);
                return null;
            }
        }

        private void MatchDate(DateTime now)
        {
            _lastDay = now;
            string today = _lastDay.ToString(LogHelper.DateFomat);
            string fileName = _adapterName + "_" + _assemblyName + "_" + today + LogHelper.FilePostfix;
            _fullFileName = _baseDirectory + "\\" + LogHelper.FileDirectory + "\\" + fileName;
            _isLogProcess = false;
        }

        private void ManageLogDirectory(DateTime now)
        {
            string directoryPath = _baseDirectory + "\\" + LogHelper.FileDirectory;
            if (Directory.Exists(directoryPath))
            {
                string[] fileNames = Directory.GetFiles(directoryPath, _adapterName + "_" + _assemblyName + "*", SearchOption.TopDirectoryOnly);
                foreach (string file in fileNames)
                {
                    try
                    {
                        DateTime fileCreationTime = File.GetCreationTime(file);
                        DateTime criteriaDate = now.AddDays(_fileDuration * (-1));

                        if (Int32.Parse(fileCreationTime.ToString("yyyyMMdd")) < Int32.Parse(criteriaDate.ToString("yyyyMMdd")))
                        {
                            File.Delete(file);
                        }
                    }
                    catch (Exception err)
                    {
                        WriteLogException(err.Message);
                    }
                }
                _isLogProcess = true;
            }
        }
        #endregion

        #region Write general log methods
        public void Write(string msg)
        {
            Write(LogType.Debug, msg, "", true);
        }
        public void Write(string msg, string module)
        {
            Write(LogType.Debug, msg, module, true);
        }
        public void Write(Exception err)
        {
            if (err == null) return;
            Write(LogType.Error, err.ToString(), "", true);
        }
        public void Write(Exception err, string module)
        {
            if (err == null) return;
            Write(LogType.Error, err.ToString(), module, true);
        }
        public void Write(LogType type, string msg)
        {
            Write(type, msg, "", true);
        }
        public void Write(LogType type, string msg, string module)
        {
            Write(type, msg, module, true);
        }
        public void Write(string msg, bool IsShowDateTime)
        {
            Write(LogType.Debug, msg, "", IsShowDateTime);
        }
        public void Write(string msg, string module, bool IsShowDateTime)
        {
            Write(LogType.Debug, msg, module, IsShowDateTime);
        }
        public void Write(LogType type, string msg, bool IsShowDateTime)
        {
            Write(type, msg, "", IsShowDateTime);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Write(LogType type, string msg, string module, bool IsShowDateTime)
        {
            #region Before logging
            if (type < _logTypeLevel)
            {
                return;
            }
            DateTime now = DateTime.Now;

            if (IsAutoManageFile)
            {
                if (_lastDay.ToShortDateString() != now.ToShortDateString())
                {
                    MatchDate(now);
                }

                //if (_isLogProcess == false && Int32.Parse(DateTime.Now.ToString("HH")) >= LogHelper.LogProcessTime)   // a bug here: only delete file in afternoon or evening.
                if (_isLogProcess == false)     //if NT service keep running and no GUI runs, log file deleting can only happen one time a day.  20071122
                {
                    ManageLogDirectory(now);
                }
            }
            #endregion

            StringBuilder sb = new StringBuilder();
            try
            {
                if (IsShowDateTime)
                {
                    #region Write General Infomation of Log
                    //Log DateTime
                    sb.Append("" + GetDateTimeString(now) + "");

                    //Log Severity
                    sb.Append("  [");
                    switch (type)
                    {
                        case LogType.Debug:
                            sb.Append(LogType.Debug.ToString());
                            break;
                        case LogType.Information:
                            sb.Append(LogType.Information.ToString());
                            break;
                        case LogType.Warning:
                            sb.Append(LogType.Warning.ToString());
                            break;
                        case LogType.Error:
                            sb.Append(LogType.Error.ToString());
                            break;
                    }
                    sb.Append("]");

                    //Log Module
                    if (module != null && module.Trim() != "")
                    {
                        sb.Append("  {" + module + "}");
                    }
                    #endregion

                    sb.Append("   " + msg);
                }
                else
                {
                    sb.Append(msg);
                }
            }
            catch (ArgumentOutOfRangeException err)
            {
                WriteLogException(err.Message);
            }
            catch (Exception err)
            {
                WriteLogException(err.Message);
            }

            #region Write data into log file
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(_fullFileName, FileMode.Append, FileAccess.Write, FileShare.Read)))
                {
                    sw.WriteLine(sb.ToString());
                }
            }
            catch (Exception err)
            {
                WriteLogException(err, msg);
            }
            #endregion
        }
        #endregion

        #region Write Log when start or exit
        public void WriteAppStart(string appName, string[] args)
        {
            DateTime now = DateTime.Now;

            #region Before starting
            if (IsAutoManageFile)
            {
                if (_lastDay.ToShortDateString() != now.ToShortDateString())
                {
                    MatchDate(now);
                }

                //if (_isLogProcess == false && Int32.Parse(DateTime.Now.ToString("HH")) >= LogHelper.LogProcessTime)
                if (_isLogProcess == false)
                {
                    ManageLogDirectory(now);
                }
            }
            #endregion

            string msg = GetDateTimeString(now) + "  [" + LogType.Debug.ToString() + "]" + "============================================================================================================" +
                    "\r\n" + GetDateTimeString(now) + "  [" + LogType.Debug.ToString() + "]" + "   XDS Gateway " + appName + " starts at " + GetDateTimeString(now) +
                    "\r\n" + "Product Name: " + Application.ProductName +
                    "\r\n" + "Product Version: " + Application.ProductVersion +    //sometime this property will throw exception
                    "\r\n" + "Startup Path: " + _baseDirectory;

            if (args != null)
            {
                string strArg = "";
                foreach (string a in args)
                {
                    strArg += " " + a;
                }
                msg += "\r\n" + "Arguments:" + strArg;
            }

            //Write log of application starting
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(_fullFileName, FileMode.Append, FileAccess.Write, FileShare.None)))
                {
                    sw.WriteLine(msg + "\r\n");
                }
            }
            catch (Exception err)
            {
                WriteLogException(err, msg);
            }
        }
        public void WriteAppStart(string appName)
        {
            WriteAppStart(appName, null);
        }
        public void WriteAppExit(string appName)
        {
            DateTime now = DateTime.Now;

            #region Before eixsting
            if (IsAutoManageFile)
            {
                if (_lastDay.ToShortDateString() != now.ToShortDateString())
                {
                    MatchDate(now);
                }

                //if (_isLogProcess == false && Int32.Parse(DateTime.Now.ToString("HH")) >= LogHelper.LogProcessTime)
                if (_isLogProcess == false)
                {
                    ManageLogDirectory(now);
                }
            }
            #endregion

            string msg = "\r\n" + GetDateTimeString(now) + "  [" + LogType.Debug.ToString() + "]" + "   XDS Gateway " + appName + " exits at " + GetDateTimeString(now) +
                                 "\r\n" + GetDateTimeString(now) + "  [" + LogType.Debug.ToString() + "]" + "============================================================================================================\r\n\r\n";

            //Write log of application Exiting
            try
            {
                using (StreamWriter sw = new StreamWriter(File.Open(_fullFileName, FileMode.Append, FileAccess.Write, FileShare.None)))
                {
                    sw.WriteLine(msg);
                }
            }
            catch (Exception err)
            {
                WriteLogException(err, msg);
            }
        }
        #endregion

        #region Open log file
        public void View()
        {
            Process.Start("notepad.exe", _fullFileName);
        }
        #endregion

        #region Log exception of log module
        private string _logExceptionFile;
        private string logExceptionFile
        {
            get
            {
                if (_logExceptionFile == null)
                {
                    _logExceptionFile = _baseDirectory + "\\" + LogHelper.FileDirectory + "\\LogModule.log";
                }
                return _logExceptionFile;
            }
        }
        private void WriteLogException(string msg)
        {
            WriteLogException(null, msg);
        }
        private void WriteLogException(Exception e)
        {
            WriteLogException(e, null);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void WriteLogException(Exception e, string msg)
        {
            try
            {
                if (e == null && msg == null)
                {
                    return;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(GetDateTimeString(DateTime.Now) + "  ");

                    if (e == null && msg != null)
                    {
                        sb.Append(msg + "\r\n");
                    }
                    else if (e != null && msg == null)
                    {
                        sb.Append("[Exception in log module]  ");
                        sb.Append(e.Message + "\r\n");
                    }
                    else
                    {
                        sb.Append("[Exception when logging]  ");
                        sb.Append(e.Message + "\r\n");
                        sb.Append("Original Log Info: " + msg + "\r\n");
                    }

                    using (StreamWriter sw = new StreamWriter(File.Open(logExceptionFile, FileMode.Append, FileAccess.Write, FileShare.None)))
                    {
                        sw.WriteLine(sb.ToString());
                    }
                }
            }
            catch (Exception err)
            {
                //MessageBox.Show("Log module is out of work!\r\n" + err.Message);
            }
        }
        #endregion

        private string GetFullPath(string path)
        {
            if (Path.IsPathRooted(path)) return path;
            return Path.Combine(_baseDirectory, path);
        }
        public static string DumpPath = "Dump";
        public static string XMLHeader = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
        public void DumpXObject(string folder, string filename, XObject msg)
        {
            if (msg == null) return;

            string path = GetFullPath(DumpPath + "\\" + folder);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string fname = Path.Combine(path, filename);
            using (StreamWriter sw = File.CreateText(fname))
            {
                string str = XMLHeader + msg.ToXMLString();
                sw.Write(str);
            }
        }
        public void DumpToFile(string folder, string filename, XObject message)
        {
            try
            {
                DumpXObject(folder, filename, message);
            }
            catch (Exception err)
            {
                this.Write(err);
            }
        }
    }
}
