using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HYS.Adapter.Base
{
    public class GWLogData
    {
        public const string DateFomat = "yyyyMMdd";
        public const string DateTimeFomat = "yyyy-MM-dd HH:mm:ss.ffffff";
        public const string FilePostfix = ".log";
        public const string FileDirectory = "Log";
        public const int LogProcessTime = 12;

        //public static string GetFilePath(string fileDirectoryPath, string serverName, DateTime dateTime)
        //{
        //    string filePath = fileDirectoryPath + "\\" + serverName + "_" + dateTime.ToString(DateFomat) + FilePostfix;
        //    return filePath;
        //}

        //public static string GetDirectoryPath()
        //{
        //    return Application.StartupPath + "\\" + FileDirectory;
        //}
    }
}
