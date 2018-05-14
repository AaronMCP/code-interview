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
    public class SimpleLog : ILog
    {
        public SimpleLog()
        {
        }
        public SimpleLog(string logFileName)
        {
            LogFileName = logFileName;
        }

        public string LogFileName = "SimpleLog.txt";
        public static string GetDateTimeString()
        {
            DateTime dt = DateTime.Now;
            return dt.ToShortDateString() + " " + dt.ToLongTimeString() + " ";
        }

        public void Write(string msg)
        {
            Write(LogType.Debug, msg,true);
        }
        public void Write(Exception err)
        {
            if (err == null) return;
            Write(LogType.Error, err.ToString(), true);
        }
        public void Write(LogType type, string msg)
        {
            Write(type, msg, true);
        }
        public void Write(string msg, bool timeMark)
        {
            Write(LogType.Debug, msg, timeMark);
        }
        public void Write(LogType type, string msg, bool timeMark )
        {
            StringBuilder sb = new StringBuilder();

            if (timeMark) sb.Append(GetDateTimeString());

            switch (type)
            {
                case LogType.Error :
                    sb.Append("[ERROR] ");
                    break;
                case LogType.Information :
                    sb.Append("[INFORMATION] ");
                    break;
            }

            sb.Append(msg);

            using (StreamWriter sw = File.AppendText(LogFileName))
            {
                sw.WriteLine(sb.ToString());
            }
        }
        public void WriteAppStart(string appName, string[] args)
        {
            string msg =
                "XDS Gateway " + appName + " start at " + GetDateTimeString() + "\r\n" +
                "================================================\r\n" +
                "Product Name: " + Application.ProductName + "\r\n" +
                //"Product Version: " + Application.ProductVersion + "\r\n" +   //sometime this property will throw exception
                "Startup Path: " + Application.StartupPath + "\r\n";

            string strArg = "";
            if (args != null)
            {
                foreach (string a in args)
                {
                    strArg += a + " ";
                }
            }
            msg += "Arguments: " + strArg + "\r\n";

            Write(LogType.Debug, "", false);
            Write(LogType.Debug, msg, false);
        }
        public void WriteAppStart(string appName)
        {
            string msg =
                "XDS Gateway " + appName + " start at " + GetDateTimeString() + "\r\n" +
                "================================================\r\n" +
                "Product Name: " + Application.ProductName + "\r\n" +
                //"Product Version: " + Application.ProductVersion + "\r\n" +   //sometime this property will throw exception
                "Startup Path: " + Application.StartupPath + "\r\n";

            Write(LogType.Debug, "", false);
            Write(LogType.Debug, msg, false);
        }
        public void WriteAppExit(string appName)
        {
            string msg =
                "\r\n================================================\r\n" +
                "XDS Gateway " + appName + " exit at " + GetDateTimeString() + "\r\n\r\n";

            Write(LogType.Debug, msg, false);
        }

        public void View()
        {
            Process.Start("notepad.exe", LogFileName);
        }

        public void DumpToFile(string folder, string filename, XObject message)
        {
            try
            {
                //LogControler.DumpXObject(folder, filename, message);
            }
            catch (Exception err)
            {
                this.Write(err);
            }
        }

        public bool DumpData
        {
            get { return false; }
        }
    }
}
