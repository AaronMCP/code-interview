using System;
using System.IO;
using System.Text;
using System.Timers;
using System.Collections.Generic;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Logging;

namespace HYS.XmlAdapter.Common.Files
{
    public class DirectoryMonitor : IEntity
    {
        private DirectoryMonitorConfig _config;
        public DirectoryMonitorConfig Config
        {
            get { return _config; }
            set { _config = value; }
        }

        private ILogging _log;
        private void WriteLog(string msg)
        {
            WriteLog(LogType.Debug, msg);
        }
        private void WriteLog(LogType t, string msg)
        {
            if (_log != null) _log.Write(t, "[DirectoryMonitor] " + msg);
        }

        public DirectoryMonitor(DirectoryMonitorConfig config, ILogging log)
        {
            _log = log;
            _config = config;
            if (_config == null) _config = new DirectoryMonitorConfig();

            _timer = new Timer(_config.TimerInterval);
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }
        public DirectoryMonitor(DirectoryMonitorConfig config)
            : this(config, null)
        {
        }

        private Timer _timer;
        public bool IsListening
        {
            get { return _timer.Enabled; }
        }
        public bool Start()
        {
            WriteLog("Timer start.");
            _timer.Start();
            return true;
        }
        public bool Stop()
        {
            _timer.Stop();
            WriteLog("Timer stop.");
            return true;
        }

        private static string DataFolder = "data";
        private static string IndexFolder = "index";
        private static string IndexFileExtension = ".idx";

        private string[] GetSourceFileList(string sourcePath)
        {
            string dataPath = ConfigHelper.GetFullPath(sourcePath + "\\" + DataFolder);
            string indexPath = ConfigHelper.GetFullPath(sourcePath + "\\" + IndexFolder);
            if (!Directory.Exists(indexPath) || !Directory.Exists(dataPath)) return null;

            List<string> dataFileList = new List<string>();
            string[] indexFileList = Directory.GetFiles(indexPath, "*" + IndexFileExtension);
            foreach (string file in indexFileList)
            {
                string fn = Path.GetFileNameWithoutExtension(file);
                string[] flist = Directory.GetFiles(dataPath, fn + ".*");
                foreach (string f in flist) dataFileList.Add(f);
            }

            return dataFileList.ToArray();
        }
        private void MoveBackupFile(string sourcePath, string fileName, bool success)
        {
            string dataPath = ConfigHelper.GetFullPath(sourcePath + "\\" + DataFolder);
            string indexPath = ConfigHelper.GetFullPath(sourcePath + "\\" + IndexFolder);

            string sFile, tFile, tPath;
            string rndNumber = GetRandomString() + ".";
            string dataFileName = Path.GetFileName(fileName);
            string indexFileName = Path.GetFileNameWithoutExtension(fileName) + IndexFileExtension;
            
            if (success)
            {
                sFile = dataPath + "\\" + dataFileName;
                tPath = dataPath + "\\" + _config.SuccessFolder + "\\" + DateTime.Now.ToString("yyyyMM");
                tFile = tPath + "\\" + rndNumber + dataFileName;

                WriteLog("Move success file from " + sFile + " to " + tFile);
                if (!Directory.Exists(tPath)) Directory.CreateDirectory(tPath);
                File.Move(sFile, tFile);

                sFile = indexPath + "\\" + indexFileName;
                tPath = indexPath + "\\" + _config.SuccessFolder + "\\" + DateTime.Now.ToString("yyyyMM");
                tFile = tPath + "\\" + rndNumber + indexFileName;

                WriteLog("Move success file from " + sFile + " to " + tFile);
                if (!Directory.Exists(tPath)) Directory.CreateDirectory(tPath);
                File.Move(sFile, tFile);
            }
            else
            {
                sFile = dataPath + "\\" + dataFileName;
                tPath = dataPath + "\\" + _config.FailureFolder + "\\" + DateTime.Now.ToString("yyyyMM");
                tFile = tPath + "\\" + rndNumber + dataFileName;

                WriteLog("Move failure file from " + sFile + " to " + tFile);
                if (!Directory.Exists(tPath)) Directory.CreateDirectory(tPath);
                File.Move(sFile, tFile);

                sFile = indexPath + "\\" + indexFileName;
                tPath = indexPath + "\\" + _config.FailureFolder + "\\" + DateTime.Now.ToString("yyyyMM");
                tFile = tPath + "\\" + rndNumber + indexFileName;

                WriteLog("Move failure file from " + sFile + " to " + tFile);
                if (!Directory.Exists(tPath)) Directory.CreateDirectory(tPath);
                File.Move(sFile, tFile);
            }
        }
        private void DeleteFile(string sourcePath, string fileName)
        {
            string dataPath = ConfigHelper.GetFullPath(sourcePath + "\\" + DataFolder);
            string indexPath = ConfigHelper.GetFullPath(sourcePath + "\\" + IndexFolder);

            string fname;
            string dataFileName = Path.GetFileName(fileName);
            string indexFileName = Path.GetFileNameWithoutExtension(fileName) + IndexFileExtension;

            fname = dataPath + "\\" + dataFileName;
            WriteLog("Delete file: " + fname);
            File.Delete(fname);

            fname = indexPath + "\\" + indexFileName;
            WriteLog("Delete file: " + fname);
            File.Delete(fname);
        }

        public event RequestEventHandler OnRequest;
        private bool NotifyRequest(string fileName)
        {
            if (OnRequest == null) return false;
            WriteLog("Notify file: " + fileName);

            string sendData = "";
            string receiveData = "";
            using (StreamReader sr = File.OpenText(fileName))
            {
                receiveData = sr.ReadToEnd();
            }

            return OnRequest(receiveData, ref sendData);
        }
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            string sourcePath = ConfigHelper.GetFullPath(_config.SourcePath);
            WriteLog("Check directory begin. (" + sourcePath + ")");

            try
            {
                string[] filelist = GetSourceFileList(sourcePath);
                if (filelist != null)
                {
                    SortedList<string, string> sortedList = new SortedList<string, string>();
                    foreach (string file in filelist) sortedList.Add(file, file);
                    WriteLog("Find " + sortedList.Count.ToString() + " file(s)");
                    foreach (KeyValuePair<string, string> pair in sortedList)
                    {
                        string file = pair.Value;
                        bool ret = NotifyRequest(file);

                        if (ret && _config.DeleteProcessedFile)
                        {
                            DeleteFile(sourcePath, file);
                        }
                        else
                        {
                            MoveBackupFile(sourcePath, file, ret);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                WriteLog(LogType.Error, err.ToString());
            }

            WriteLog("Check directory end. (" + sourcePath + ")");
            _timer.Start();
        }

        private static int _number = 0;
        public static string GetRandomString()
        {
            long number = unchecked(DateTime.Now.Ticks + (_number++));
            return DateTime.Now.ToString("yyyyMMddhhmmss") + number.ToString();
        }
    }
}
