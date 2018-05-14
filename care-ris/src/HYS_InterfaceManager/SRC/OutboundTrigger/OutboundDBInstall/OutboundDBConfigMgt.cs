using System;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using HYS.Common.Xml;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Rule;


namespace OutboundDBInstall
{
    #region OutboundDBConfigMgt

    public class OutboundDBConfigMgt
    {
        public static OutboundConfig Config = new OutboundConfig();

        public static GWDataBaseInfo GWDBInfo = new GWDataBaseInfo();

        public static DeviceDirManager DeviceDir = new DeviceDirManager("devicedir");

        public static string FileName = "OutboundDBInstallConfig.xml";

        public static void Load()
        {
            //#region Save Config
            //using (StreamWriter sw = File.CreateText(FileName))
            //{
            //    Config = new OutboundDBInstallConfig();
            //    string str = Config.ToXMLString();
            //    sw.Write(str);
            //}
            //#endregion

            #region Load Config
            using (StreamReader sr = File.OpenText(FileName))
            {
                try
                {
                    string str = sr.ReadToEnd();
                    if (str.Equals(""))
                    {
                        Config = new OutboundConfig();
                    }
                    else
                    {
                        Config = XObjectManager.CreateObject(str, typeof(OutboundConfig)) as OutboundConfig;
                    }
                }
                catch
                {
                    if (Config == null)
                        Config = new OutboundConfig();
                }
                finally
                {
                    if (Config == null)
                        Config = new OutboundConfig();
                }
            }
            #endregion

            #region Load DeviceDir

            DeviceDir.LoadDeviceDir();

            int iDevice_ID;
            if (!int.TryParse(DeviceDir.DeviceDirInfor.Header.ID, out iDevice_ID))
            {
                Program.log.Write(LogType.Error, "Device_ID invalid");
                Config.Device_ID = -1;
            }
            else
                Config.Device_ID = iDevice_ID;

            Config.INameOutbound = DeviceDir.DeviceDirInfor.Header.Name;

            #endregion

            #region GWDBInfo.Init
            AdapterConfigCfgMgt mgt = new AdapterConfigCfgMgt();
            if (mgt.Load())
            {                
                GWDBInfo.Init(mgt.Config.ConfigDBConnection);
            }
            else
            {
                Program.log.Write(mgt.LastError);
            }
            #endregion
            
        }

        public static void Save()
        {

            #region InstallTrigger
            string sFileName;

            sFileName = Application.StartupPath + "\\" + RuleScript.InstallTrigger.FileName;
            if (File.Exists( sFileName ))
                File.Delete( sFileName );
            //FileStream fs = File.OpenWrite(sFileName);
            //StreamWriter sw = new StreamWriter(fs);

            File.WriteAllText(sFileName, Config.InstallTriggerScript, System.Text.Encoding.Unicode);

            //try
            //{
            //    sw.Write(Config.InstallTriggerScript);
            //    sw.Flush();
            //}
            //finally
            //{
            //    sw.Close();
            //    fs.Close();
            //}
            #endregion

            #region UnstallTrigger
            sFileName = Application.StartupPath + "\\" + RuleScript.UninstallTrigger.FileName;
            if (File.Exists( sFileName ))
                File.Delete( sFileName );
            //fs = File.OpenWrite( sFileName );
            //sw = new StreamWriter(fs);

            File.WriteAllText(sFileName, Config.UninstallTriggerScript, System.Text.Encoding.Unicode);

            //try
            //{
            //    sw.Write(Config.UninstallTriggerScript);
            //    sw.Flush();
            //}
            //finally
            //{
            //    sw.Close();
            //    fs.Close();
            //}
            #endregion

            #region InstallConfigDBScript
            sFileName = Application.StartupPath + "\\" + RuleScript.InstallConfigDB.FileName;

            if (File.Exists(sFileName))
                File.Delete(sFileName);
            //fs = File.OpenWrite(sFileName);
            //sw = new StreamWriter(fs);

            File.WriteAllText(sFileName, Config.InstallConfigDBScript, System.Text.Encoding.Unicode);

            //try
            //{
            //    sw.Write(Config.InstallConfigDBScript);
            //    sw.Flush();
            //}
            //finally
            //{
            //    sw.Close();
            //    fs.Close();
            //}
            #endregion

            #region UninstallConfigDBScript
            sFileName = Application.StartupPath + "\\" + RuleScript.UninstallConfigDB.FileName;
            if (File.Exists(sFileName))
                File.Delete(sFileName);
            //fs = File.OpenWrite(sFileName);
            //sw = new StreamWriter(fs);

            File.WriteAllText(sFileName, Config.UninstallConfigDBScript, System.Text.Encoding.Unicode);

            //try
            //{
            //    sw.Write(Config.UninstallConfigDBScript);
            //    sw.Flush();
            //}
            //finally
            //{
            //    sw.Close();
            //    fs.Close();
            //}
            #endregion

            #region Save Config
            using (StreamWriter sw = File.CreateText(FileName))
            {
                string str = Config.ToXMLString();
                sw.Write(str);
            }
            #endregion
        }      
       
    }

    #endregion
}


