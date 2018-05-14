using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Translation;



namespace OutboundDBInstall
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //PreLoad();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FOutboundTriggerConfig());
        }

        public static Logging log = new Logging(Application.StartupPath + "\\" + "OutboundDBInstall.log");
        
        public static void PreLoad()
        {
                        
            try
            {
                LoggingHelper.EnableXmlLogging(log);
                log.WriteAppStart("OutboundDBInstall.exe");
                //OutboundDBConfigMgt.Save();

                OutboundDBConfigMgt.FileName = Application.StartupPath + "\\" + OutboundDBConfigMgt.FileName;
                OutboundDBConfigMgt.Load();
                //SaveToFile(OutboundDBConfigMgt.Config.OutboundConfig.InstallTriggerScript, "InstallTrigger_1.sql");
            }
            catch(Exception Ex)
            {
                try
                {
                    string sBakFile = "";

                    if (File.Exists(OutboundDBConfigMgt.FileName))
                    {
                        sBakFile = BackupFile(OutboundDBConfigMgt.FileName);
                        File.Delete(OutboundDBConfigMgt.FileName);
                    }
                    
                    //OutboundDBConfigMgtSave();
                    // Save Calss
                    using (StreamWriter sw = File.CreateText(OutboundDBConfigMgt.FileName))
                    {
                        string str = OutboundDBConfigMgt.Config.ToXMLString();
                        sw.Write(str);
                    }

                    log.Write(Ex);
                    log.Write(LogType.Error
                              , "Load Configuration file failure!\n" 
                                + "System had rename old configuration file to "+sBakFile
                                +" ,A new empty configuration had been produced!\n"
                             , true);
                }
                catch { }
            }
        }

        static string BackupFile(string sFileName)
        {
            for (int i = 1; i < Int32.MaxValue; i++)
            {
                string s = sFileName + ".bak" + Convert.ToString(i);
                if (!File.Exists(s))
                {
                    File.Copy(sFileName, s);
                    return s;
                }
            }
            return "";
        }

        static void SaveToFile(string str, string fname)
        {
            //FileStream fs = File.OpenWrite(fname);
            //StreamWriter sw = new StreamWriter(fs);

            //try
            //{
            //    sw.Write(str);
            //    sw.Flush();
            //}
            //finally
            //{
            //    sw.Close();
            //    fs.Close();                
            //}

            File.WriteAllText(fname, str, System.Text.Encoding.Unicode);
        }
    }
}