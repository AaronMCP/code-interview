using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;

namespace DemoAdapter.Controlers
{
    /// <summary>
    /// A very simple logging.
    /// </summary>
    public class Logging
    {
        public static string LogFileName;
        public static string GetDateTimeString()
        {
            DateTime dt = DateTime.Now;
            return dt.ToShortDateString() + " " + dt.ToLongTimeString() + " ";
        }

        public static void Write(string msg)
        {
            Write(LogType.Debug, msg,true);
        }
        public static void Write(Exception err)
        {
            if (err == null) return;
            Write(LogType.Error, err.ToString(), true);
        }
        public static void Write(LogType type, string msg)
        {
            Write(type, msg, true);
        }
        public static void Write(LogType type, string msg, bool timeMark )
        {
            StringBuilder sb = new StringBuilder();

            if (timeMark) sb.Append(GetDateTimeString());
            
            switch (type)
            {
                case LogType.Error :
                    sb.Append(" [ERROR] ");
                    break;
                case LogType.Warning :
                    sb.Append(" [WARNING] ");
                    break;
            }

            sb.Append(msg);

            using (StreamWriter sw = File.AppendText(LogFileName))
            {
                sw.WriteLine(sb.ToString());
            }
        }
        public static void WriteAppStart(string appName)
        {
            string msg =
                "GC Gateway " + appName + " start at " + GetDateTimeString() + "\r\n" +
                "================================================\r\n" +
                "Product Name: " + Application.ProductName + "\r\n" +
                "Product Version: " + Application.ProductVersion + "\r\n" +
                "Startup Path: " + Application.StartupPath + "\r\n";

            Write(LogType.Debug, msg, false);
        }
        public static void WriteAppExit(string appName)
        {
            string msg =
                "\r\n================================================\r\n" +
                "GC Gateway " + appName + " exit at " + GetDateTimeString() + "\r\n\r\n";

            Write(LogType.Debug, msg, false);
        }

        public static void View()
        {
            Process.Start("notepad.exe", LogFileName);
        }
    }

    public enum LogType
    {
        Debug,
        Warning,
        Error
    }
}
