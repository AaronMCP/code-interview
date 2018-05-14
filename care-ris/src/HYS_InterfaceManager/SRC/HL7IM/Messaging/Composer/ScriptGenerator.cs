using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
//using HYS.IM.Common.DataAccess;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Composer
{
    public static class ScriptGenerator
    {
        public const string CreateDBBatFileName = "CreateDB.bat";
        public const string DropDBBatFileName = "DropDB.bat";

        public const string CreateVirtualPathBatFileName = "CreateVirtualPath.bat";     // for iis5
        public const string DropVirtualPathBatFileName = "DropVirtualPath.bat";         // for iis5

        public const string CreateVirtualPathBatFileName_iis6 = "CreateVirtualPath_iis6.bat";
        public const string DropVirtualPathBatFileName_iis6 = "DropVirtualPath_iis6.bat";

        public static void WriteCreateDBBatFile()
        {
            string logfolderpath = Path.Combine(Application.StartupPath, "log");
            string createlogfilepath = Path.Combine(logfolderpath, "CreateDB.log");
            string droplogfilepath = Path.Combine(logfolderpath, "DropDB.log");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("mkdir \"" + logfolderpath + "\"");
            sb.Append("\"").Append(Program.ConfigMgt.Config.DBParameter.OSQLFileName).Append("\" ")
                .Append(Program.ConfigMgt.Config.DBParameter.OSQLArgument).AppendLine(" -d master -i DropDB.sql >> \"" + droplogfilepath + "\"");
            sb.Append("\"").Append(Program.ConfigMgt.Config.DBParameter.OSQLFileName).Append("\" ")
                .Append(Program.ConfigMgt.Config.DBParameter.OSQLArgument).AppendLine(" -d master -i CreateDB.sql >> \"" + createlogfilepath + "\"");
            string str = sb.ToString();

            string fname = Path.Combine(Application.StartupPath, CreateDBBatFileName);
            using (StreamWriter sw = File.CreateText(fname))
            {
                sw.Write(str);    
            }
        }

        public static void WriteDropDBBatFile()
        {
            string logfolderpath = Path.Combine(Application.StartupPath, "log");
            string logfilepath = Path.Combine(logfolderpath, "DropDB.log");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("mkdir \"" + logfolderpath + "\"");
            sb.Append("\"").Append(Program.ConfigMgt.Config.DBParameter.OSQLFileName).Append("\" ")
                .Append(Program.ConfigMgt.Config.DBParameter.OSQLArgument).AppendLine(" -d master -i DropDB.sql >> \"" + logfilepath + "\"");
            string str = sb.ToString();

            string fname = Path.Combine(Application.StartupPath, DropDBBatFileName);
            using (StreamWriter sw = File.CreateText(fname))
            {
                sw.Write(str);    
            }
        }

        public static void WriteCreateVirtualPathBatFile()
        {
            // string physicalPath = Path.Combine(Application.StartupPath, "Portal");   // v 1.0
            string physicalPath = Path.Combine(ConfigHelper.DismissDotDotInThePath(Program.GetSolutionRoot()), "Web");  // v 1.1
            string logfolderpath = Path.Combine(Application.StartupPath, "log");
            string logfilepath = Path.Combine(logfolderpath, "CreateVirtualPath.log");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("mkdir \"" + logfolderpath + "\"");

            // for IIS 5
            sb.AppendLine("Cscript.exe C:\\Inetpub\\AdminScripts\\mkwebdir.vbs -c localhost -w 1 -v \""
                + Program.ConfigMgt.Config.WebSetting.VirtualPathName
                + "\",\"" + physicalPath + "\""
                + " >> \"" + logfilepath + "\"");
            sb.AppendLine("Cscript.exe C:\\Inetpub\\AdminScripts\\adsutil.vbs APPCREATEINPROC W3SVC/1/Root/"
                + Program.ConfigMgt.Config.WebSetting.VirtualPathName
                + " >> \"" + logfilepath + "\"");
            sb.AppendLine("Cscript.exe C:\\Inetpub\\AdminScripts\\chaccess.vbs -a W3SVC/1/Root/"
                + Program.ConfigMgt.Config.WebSetting.VirtualPathName
                + " +script"
                + " >> \"" + logfilepath + "\"");

            string str = sb.ToString();

            string fname = Path.Combine(Application.StartupPath, CreateVirtualPathBatFileName);
            using (StreamWriter sw = File.CreateText(fname))
            {
                sw.Write(str);
            }
        }

        public static void WriteDropVirtualPathBatFile()
        {
            string logfolderpath = Path.Combine(Application.StartupPath, "log");
            string logfilepath = Path.Combine(logfolderpath, "DropVirtualPath.log");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("mkdir \"" + logfolderpath + "\"");

            // for IIS 5
            sb.AppendLine("Cscript.exe C:\\Inetpub\\AdminScripts\\adsutil.vbs DELETE W3SVC/1/Root/" 
                + Program.ConfigMgt.Config.WebSetting.VirtualPathName
                + " >> \"" + logfilepath + "\"");
            
            string str = sb.ToString();

            string fname = Path.Combine(Application.StartupPath, DropVirtualPathBatFileName);
            using (StreamWriter sw = File.CreateText(fname))
            {
                sw.Write(str);
            }
        }

        public static void WriteCreateVirtualPathBatFile_iis6()
        {
            //  string physicalPath = Path.Combine(Application.StartupPath, "Portal");      // v 1.0
            string physicalPath = Path.Combine(ConfigHelper.DismissDotDotInThePath(Program.GetSolutionRoot()), "Web");  // v 1.1
            string logfolderpath = Path.Combine(Application.StartupPath, "log");
            string logfilepath = Path.Combine(logfolderpath, "CreateVirtualPath_iis6.log");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("mkdir \"" + logfolderpath + "\"");

            // for IIS 6
            //%systemroot%\system32\cscript.exe %systemroot%\system32\iisvdir.vbs /create W3SVC/1/Root "Renji_EMR_Integration" "D:\XDSGWB06\Portal" >> "log\CreateVirtualPath_iis6.log"

            sb.AppendLine("%systemroot%\\system32\\cscript.exe %systemroot%\\system32\\iisvdir.vbs /create W3SVC/1/Root \""
                + Program.ConfigMgt.Config.WebSetting.VirtualPathName
                + "\" \"" + physicalPath + "\""
                + " >> \"" + logfilepath + "\"");

            string str = sb.ToString();

            string fname = Path.Combine(Application.StartupPath, CreateVirtualPathBatFileName_iis6);
            using (StreamWriter sw = File.CreateText(fname))
            {
                sw.Write(str);
            }
        }

        public static void WriteDropVirtualPathBatFile_iis6()
        {
            string logfolderpath = Path.Combine(Application.StartupPath, "log");
            string logfilepath = Path.Combine(logfolderpath, "DropVirtualPath_iis6.log");

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("mkdir \"" + logfolderpath + "\"");

            // for IIS 6
            // %systemroot%\system32\cscript.exe %systemroot%\system32\iisvdir.vbs /delete W3SVC/1/Root/Renji_EMR_Integration >> "log\DropVirtualPath_iis6.log"

            sb.AppendLine("%systemroot%\\system32\\cscript.exe %systemroot%\\system32\\iisvdir.vbs /delete W3SVC/1/Root/"
                + Program.ConfigMgt.Config.WebSetting.VirtualPathName
                + " >> \"" + logfilepath + "\"");

            string str = sb.ToString();

            string fname = Path.Combine(Application.StartupPath, DropVirtualPathBatFileName_iis6);
            using (StreamWriter sw = File.CreateText(fname))
            {
                sw.Write(str);
            }
        }

        public static void WriteWebPortalShortCut()
        {
            string vPathName = Program.ConfigMgt.Config.WebSetting.VirtualPathName;
            string fileName = Program.ConfigMgt.Config.Name + " Management.url";
            fileName = Path.Combine(Application.StartupPath, fileName);

            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine("[DEFAULT]");
                sw.WriteLine("BASEURL=http://localhost/" + vPathName + "/Default.aspx");
                sw.WriteLine("[InternetShortcut]");
                sw.WriteLine("URL=http://localhost/" + vPathName + "/Default.aspx");
            }
        }

        public static void WriteApplyWebConfigShortCut()
        {
            string vPathName = Program.ConfigMgt.Config.WebSetting.VirtualPathName;
            string fileName = "Apply Web Configuration.url";
            fileName = Path.Combine(Application.StartupPath, fileName);

            using (StreamWriter sw = File.CreateText(fileName))
            {
                sw.WriteLine("[DEFAULT]");
                sw.WriteLine("BASEURL=http://localhost/" + vPathName + "/RefreshConfig.aspx");
                sw.WriteLine("[InternetShortcut]");
                sw.WriteLine("URL=http://localhost/" + vPathName + "/RefreshConfig.aspx");
            }
        }
    }
}
