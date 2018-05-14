using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;

namespace HYS.IM.BusinessControl.SystemControl
{
    public class ScriptControl
    {
        public static bool UpdateInterface(GCInterface gcInterface, IMCfg iemCfg)
        {
            Hashtable paramTable = GetInterfaceParamter(gcInterface, iemCfg);
            if (paramTable == null) return false;

            if (!UpdateInterfaceService(paramTable)) return false;
            if (!UpdateInterfaceMonitor(paramTable)) return false;
            if (!UpdateInterfaceConfig(paramTable)) return false;
            if (!UpdateInterfaceScript(paramTable, gcInterface.Directory, DeviceFileType.InstallScript, false)) return false;
            if (!UpdateInterfaceScript(paramTable, gcInterface.Directory, DeviceFileType.UninstallScript, false)) return false;
            if (!UpdateInterfaceScript(paramTable, gcInterface.Directory, DeviceFileType.StartScript, false)) return false;
            if (!UpdateInterfaceScript(paramTable, gcInterface.Directory, DeviceFileType.StopScript, false)) return false;
            if (!UpdateInterfaceScript(paramTable,gcInterface.Directory,DeviceFileType.OtherScript,false)) return false;
            if (!UpdateInterfaceScript(paramTable, gcInterface.Directory, DeviceFileType.DBInstallScript, false)) return false;
            if (!UpdateInterfaceScript(paramTable, gcInterface.Directory, DeviceFileType.DBUnintallScript, false)) return false;

            return true;
        }

        private static Hashtable GetInterfaceParamter(GCInterface gcInterface, IMCfg iemCfg)
        {
            if (gcInterface == null ||
                gcInterface.Device == null ||
                gcInterface.Directory == null ||
                iemCfg == null) return null;

            Hashtable table = new Hashtable();
            table.Add(IMParameter.InterfaceID, gcInterface.InterfaceID);
            table.Add(IMParameter.InterfaceName, gcInterface.InterfaceName);
            table.Add(IMParameter.InterfaceDirectory, ConfigHelper.GetFullPath(gcInterface.FolderPath));
            table.Add(IMParameter.InterfaceDescription, gcInterface.Directory.Header.Description);
            table.Add(IMParameter.ReferenceDeviceID, gcInterface.Device.DeviceID);
            table.Add(IMParameter.ReferenceDeviceName, gcInterface.Device.DeviceName);
            table.Add(IMParameter.ServiceName, gcInterface.InterfaceName);
            table.Add(IMParameter.IMCaption, iemCfg.AppCaption);
            table.Add(IMParameter.DataDBConnection, iemCfg.DataDBConnection);
            table.Add(IMParameter.ConfigDBConnection, iemCfg.ConfigDBConnection);
            return table;
        }
        private static bool UpdateInterfaceScript(Hashtable paramTable, DeviceDir directory, DeviceFileType type, bool essentialFile)
        {
            DeviceFile[] files = directory.Files.FindFiles(type);
            
            if (files == null || files.Length < 1)
            {
                if (essentialFile)
                {
                    GCError.SetLastError("Cannot find script file of type: " + type.ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }

            foreach (DeviceFile file in files)
            {
                if (!UpdateInterfaceScript(paramTable, file, type, essentialFile)) return false;
            }

            return true;
        }
        private static bool UpdateInterfaceScript(Hashtable paramTable, DeviceFile file, DeviceFileType type, bool essentialFile)
        {
            if (file == null)
            {
                if (essentialFile)
                {
                    GCError.SetLastError("Cannot find script file of type: " + type.ToString());
                    return false;
                }
                else
                {
                    return true;
                }
            }

            string path = paramTable[IMParameter.InterfaceDirectory] as string;
            string filename = path + "\\" + file.Location;

            try
            {
                string str = "";

                using (StreamReader sr = File.OpenText(filename))
                {
                    str = sr.ReadToEnd();
                }

                foreach (DictionaryEntry de in paramTable)
                {
                    string key = de.Key as string;
                    string value = de.Value as string;
                    str = str.Replace(key, value);
                }

                using (StreamWriter sw = File.CreateText(filename))
                {
                    sw.Write(str);
                }

                return true;
            }
            catch (Exception err)
            {
                GCError.SetLastError("Error when processing file: " + filename);
                GCError.SetLastError(err);
                return false;
            }
        }
        private static bool UpdateInterfaceService(Hashtable paramTable)
        {
            string path = paramTable[IMParameter.InterfaceDirectory] as string;

            // update Adapter Service config
            AdapterServiceCfgMgt serviceMgt = new AdapterServiceCfgMgt();
            serviceMgt.FileName = path + "\\" + serviceMgt.FileName;
            if (serviceMgt.Load())
            {
                serviceMgt.Config.NotifyStatusToIM = true;
                serviceMgt.Config.IMWindowCaption = paramTable[IMParameter.IMCaption] as string;
                serviceMgt.Config.ConfigDBConnection = paramTable[IMParameter.ConfigDBConnection] as string;
                serviceMgt.Config.DataDBConnection = paramTable[IMParameter.DataDBConnection] as string;
                serviceMgt.Config.ServiceName = paramTable[IMParameter.ServiceName] as string;
                //US29442
                #region
                serviceMgt.Config.GarbageCollection.MaxRecordCountLimitation = 500;
                #endregion
                if (!serviceMgt.Save())
                {
                    GCError.SetLastError("Save config file failed. " + serviceMgt.FileName);
                    GCError.SetLastError(serviceMgt.LastError);
                    return false;
                }
            }
            else
            {
                GCError.SetLastError("Load config file failed. " + serviceMgt.FileName);
                GCError.SetLastError(serviceMgt.LastError);
                return false;
            }

            return true;
        }
        private static bool UpdateInterfaceMonitor(Hashtable paramTable)
        {
            string path = paramTable[IMParameter.InterfaceDirectory] as string;

            // update Adapter Monitor config
            AdapterMonitorCfgMgt monitorMgt = new AdapterMonitorCfgMgt();
            monitorMgt.FileName = path + "\\" + monitorMgt.FileName;
            if (monitorMgt.Load())
            {
                monitorMgt.Config.ConfigDBConnection = paramTable[IMParameter.ConfigDBConnection] as string;
                monitorMgt.Config.DataDBConnection = paramTable[IMParameter.DataDBConnection] as string;
                if (!monitorMgt.Save())
                {
                    GCError.SetLastError("Save config file failed. " + monitorMgt.FileName);
                    GCError.SetLastError(monitorMgt.LastError);
                    return false;
                }
            }
            else
            {
                GCError.SetLastError("Load config file failed. " + monitorMgt.FileName);
                GCError.SetLastError(monitorMgt.LastError);
                return false;
            }

            return true;
        }
        private static bool UpdateInterfaceConfig(Hashtable paramTable)
        {
            string path = paramTable[IMParameter.InterfaceDirectory] as string;

            //update Adapter Config config
            AdapterConfigCfgMgt configMgt = new AdapterConfigCfgMgt();
            configMgt.FileName = path + "\\" + configMgt.FileName;
            if (configMgt.Load())
            {
                configMgt.Config.ConfigDBConnection = paramTable[IMParameter.ConfigDBConnection] as string;
                configMgt.Config.DataDBConnection = paramTable[IMParameter.DataDBConnection] as string;
                if (!configMgt.Save())
                {
                    GCError.SetLastError("Save config file failed. " + configMgt.FileName);
                    GCError.SetLastError(configMgt.LastError);
                    return false;
                }
            }
            else
            {
                GCError.SetLastError("Load config file failed. " + configMgt.FileName);
                GCError.SetLastError(configMgt.LastError);
                return false;
            }

            return true;
        }

        public static bool UpdateDBConnection(string interfacesPath, string dataDB, string configDB)
        {
            string[] pathList = Directory.GetDirectories(ConfigHelper.GetFullPath(interfacesPath));
            if (pathList == null) return false;

            foreach (string path in pathList)
            {
                AdapterServiceCfgMgt mgtSerivce = new AdapterServiceCfgMgt();
                mgtSerivce.FileName = path + "\\" + mgtSerivce.FileName;
                if (mgtSerivce.Load())
                {
                    mgtSerivce.Config.DataDBConnection = dataDB;
                    mgtSerivce.Config.ConfigDBConnection = configDB;
                    if (!mgtSerivce.Save())
                    {
                        GCError.SetLastError(mgtSerivce.LastError);
                        return false;
                    }
                }
                else
                {
                    GCError.SetLastError(mgtSerivce.LastError);
                    return false;
                }

                AdapterConfigCfgMgt mgtConfig = new AdapterConfigCfgMgt();
                mgtConfig.FileName = path + "\\" + mgtConfig.FileName;
                if (mgtConfig.Load())
                {
                    mgtConfig.Config.DataDBConnection = dataDB;
                    mgtConfig.Config.ConfigDBConnection = configDB;
                    if (!mgtConfig.Save())
                    {
                        GCError.SetLastError(mgtConfig.LastError);
                        return false;
                    }
                }
                else
                {
                    GCError.SetLastError(mgtConfig.LastError);
                    return false;
                }

                AdapterMonitorCfgMgt mgtMonitor = new AdapterMonitorCfgMgt();
                mgtMonitor.FileName = path + "\\" + mgtMonitor.FileName;
                if (mgtMonitor.Load())
                {
                    mgtMonitor.Config.DataDBConnection = dataDB;
                    mgtMonitor.Config.ConfigDBConnection = configDB;
                    if (!mgtMonitor.Save())
                    {
                        GCError.SetLastError(mgtMonitor.LastError);
                        return false;
                    }
                }
                else
                {
                    GCError.SetLastError(mgtMonitor.LastError);
                    return false;
                }
            }

            return true;
        }
    }
}
