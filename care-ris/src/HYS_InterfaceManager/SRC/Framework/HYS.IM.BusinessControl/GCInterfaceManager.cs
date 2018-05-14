using System;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using HYS.Common.DataAccess;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl.DataControl;
using HYS.IM.BusinessControl.SystemControl;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Rule;
using HYS.Adapter.Base;
using HYS.Common.Xml;
//US28109-TA93905
#region
using HYS.FileAdapter.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
 
#endregion
namespace HYS.IM.BusinessControl
{
    public class GCInterfaceManager : IProgress
    {
        public readonly string InterfacesFolder;
        private InterfaceRecManager InterfaceTable;
        public GCInterfaceManager(DataBase db, string interfacesFolder)
        {
            InterfacesFolder = interfacesFolder;
            InterfaceTable = new InterfaceRecManager(db);
        }

        public Dictionary<int, int> GetInterfaceCount(int[] deviceIDs)
        {
            return InterfaceTable.GetInterfaceCount(deviceIDs);
        }

        public GCInterface AddInterfaceToFolder(GCDevice gcDevice, string interfaceName, string interfacDesc)
        {
            GCError.ClearLastError();

            // validation
            if (gcDevice == null || gcDevice.Directory == null )
            {
                GCError.SetLastError("Invalid device object.");
                return null;
            }

            int progCount = 0;
            int fileCount = gcDevice.Directory.Files.Count;
            NotifyStart(fileCount + 2, 0, progCount++, "Create interface folder...");

            // create interface folder
            string targetFolder = InterfacesFolder + "\\" + interfaceName;
            try
            {
                if (Directory.Exists(targetFolder))
                {
                    GCError.SetLastError("Folder " + targetFolder + " have already exist, please try another name.");
                    NotifyComplete(false, "");
                    return null;
                }

                if (Directory.CreateDirectory(targetFolder) == null)
                {
                    GCError.SetLastError("Cannot create folder " + targetFolder + ", please try another name.");
                    NotifyComplete(false, "");
                    return null;
                }
            }
            catch (Exception e)
            {
                GCError.SetLastError("Cannot create folder " + targetFolder);
                GCError.SetLastError(e);
                NotifyComplete(false, "");
                return null;
            }

            // copy device files
            DeviceDir dir = gcDevice.Directory;
            string sourceFolder = gcDevice.FolderPath;
            string sourceFile = sourceFolder + "\\" + DeviceDirManager.IndexFileName;
            string targetFile = targetFolder + "\\" + DeviceDirManager.IndexFileName;

            try
            {
                NotifyGoing(progCount++, "Copying files... ");
                File.Copy(sourceFile, targetFile);
                foreach (DeviceFile f in dir.Files)
                {
                    NotifyGoing(progCount++, "Copying files...");
                    sourceFile = sourceFolder + "\\" + f.Location;
                    targetFile = targetFolder + "\\" + f.Location;

                    // support sub folder in device dir
                    string path = Path.GetDirectoryName(targetFile);
                    if (!Directory.Exists(path))
                    {
                        if (Directory.CreateDirectory(path) == null)
                        {
                            GCError.SetLastError("Cannot create folder " + path);
                            NotifyComplete(false, "");
                            return null;
                        }
                    }

                    File.Copy(sourceFile, targetFile);
                }
            }
            catch (Exception e)
            {
                GCError.SetLastError("Cannot copy file from " + sourceFile + " to " + targetFile );
                GCError.SetLastError(e);
                NotifyComplete(false, "");

                // roll back...
                FolderControl.DeleteDevice(targetFolder);
                return null;
            }

            NotifyGoing(progCount++, "Updating files...");
            
            // update DeviceDir: save interface name, which will be used by interface installation program
            GCDevice device = FolderControl.LoadDevice(targetFolder);
            device.Directory.Header.RefDeviceName = gcDevice.DeviceName;
            device.Directory.Header.RefDeviceID = gcDevice.DeviceID.ToString();
            device.Directory.Header.Description = interfacDesc;
            device.Directory.Header.Name = interfaceName;
            if (!FolderControl.SaveDevice(device))
            {
                GCError.SetLastError("Update device index file failed.");
                NotifyComplete(false, "");

                // roll back...
                FolderControl.DeleteDevice(targetFolder);
                return null;
            }

            // create interface object
            GCInterface i = new GCInterface();
            i.Directory = device.Directory;
            i.InterfaceName = interfaceName;
            i.FolderPath = targetFolder;
            i.Device = gcDevice;

            NotifyComplete(true, "");

            return i;
        }
        public bool DeleteInterfaceFromFolder(GCInterface gcInterface)
        {
            GCError.ClearLastError();

            // validation
            if (gcInterface == null)
            {
                GCError.SetLastError("Invalid gcInterface object.");
                return false;
            }

            // delete folder
            string folderPath = gcInterface.FolderPath;
            bool result = FolderControl.DeleteDevice(folderPath);

            if (!result)
            {
                GCError.SetLastError("Delete folder " + folderPath + "  failed.");
            }

            return result;
        }

        public bool HasInterface(int deviceID)
        {
            return InterfaceTable.HasInterface(deviceID);
        }
        public bool IsValidInterfaceName(string name)
        {
            if (name == null) return false;

            string strName = name.Trim();
            if (strName.Length < 1) return false;

            string newName = Regex.Replace(strName, @"[^\w]", "");
            newName = Regex.Replace(newName, @"[^\x00-\xff]", "");
            if (newName.Length != strName.Length) return false;

            char c = newName[0];
            return c >= 'A' && c <= 'z';
        }
        public bool HasSampleInterfaceName(string name)
        {
            return InterfaceTable.HasSampleName(name);
        }
        public bool AddInterfaceToDatabase(GCInterface gcInterface)
        {
            //return TempFunctionHandler("Runing AddInterfaceToDatabase script...");

            GCError.ClearLastError();

            // validate interface
            if (gcInterface == null)
            {
                GCError.SetLastError("Invalid interface.");
                return false;
            }

            // insert interface record into database
            gcInterface.Directory = null;
            if (!InterfaceTable.InsertInterface(gcInterface))
            {
                GCError.SetLastError("Insert database failed.");
                return false;
            }

            // get interface id
            gcInterface.InterfaceID = InterfaceTable.GetMaxID();
            if (gcInterface.InterfaceID < 1)
            {
                GCError.SetLastError("Invalid interface ID.");
                return false;
            }

            // update DeviceDir: update interface id, which will be used by interface NT service to identify itself when notifying its status to IM via windows message. 
            GCDevice device = FolderControl.LoadDevice(gcInterface.FolderPath);
            device.Directory.Header.ID = gcInterface.InterfaceID.ToString();
            if (!FolderControl.SaveDevice(device))
            {
                GCError.SetLastError("Update device index file failed.");
                return false;
            }

            return true;
        } 
        public bool DeleteInterfaceFromDatabase(GCInterface gcInterface)
        {
            //return TempFunctionHandler("Runing DeleteInterfaceFromDatabase script...");

            GCError.ClearLastError();

            //validate interface
            if (gcInterface == null || gcInterface.InterfaceID < 0)
            {
                GCError.SetLastError("Invalid interface.");
                return false;
            }

            //delete record from database
            if (!InterfaceTable.DeleteInterface(gcInterface))
            {
                GCError.SetLastError("Delete interface from database failed.");
                return false;
            }

            return true;
        }

        public bool RunDBInstallScript(GCInterface gcInterface)
        {
            if (ExecuteDatabaseScript(gcInterface, RuleScript.InstallTable))
            {
                if (ExecuteDatabaseScript(gcInterface, RuleScript.InstallLUT))
                {
                    if (ExecuteDatabaseScript(gcInterface, RuleScript.InstallTrigger))
                    {
                        if (ExecuteDatabaseScript(gcInterface, RuleScript.InstallConfigDB))
                        {
                            ExecuteDatabaseScript(gcInterface, RuleScript.InstallSP);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        public bool RunDBUninstallScript(GCInterface gcInterface)
        {
            return RunDBUninstallScript(gcInterface, false);
        }
        public bool RunDBUninstallScript(GCInterface gcInterface, bool backup)
        {
            if (ExecuteDatabaseScript(gcInterface, RuleScript.UninstallSP, backup))
            {
                if (ExecuteDatabaseScript(gcInterface, RuleScript.UninstallConfigDB, backup))
                {
                    if (ExecuteDatabaseScript(gcInterface, RuleScript.UninstallTrigger, backup))
                    {
                        if (ExecuteDatabaseScript(gcInterface, RuleScript.UninstallLUT, backup))
                        {
                            //Configuration GUI will not make any change on table definition,
                            //so we do not need to re-create table while configuration changed. 
                            //If we did it, 1. data will be lost, 2. trigger will be deleted.

                            if (backup)
                            {
                                return true;
                            }
                            else
                            {
                                return ExecuteDatabaseScript(gcInterface, RuleScript.UninstallTable, backup);
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool InstallInterface(GCInterface gcInterface)
        {
            if (!ValidateInterface(gcInterface)) return false;
            //return TempFunctionHandler("Runing installation script");
            bool res = ExecuteCommand(gcInterface, CommandType.Install, true);
            if (gcInterface.Directory.Header.UseCommandOnly) return res;
            else return ExecuteFile(gcInterface, DeviceFileType.InstallScript, true);
        }
        public bool UninstallInterface(GCInterface gcInterface)
        {
            if (!ValidateInterface(gcInterface)) return false;
            //return TempFunctionHandler("Runing uninstallation script");
            bool res = ExecuteCommand(gcInterface, CommandType.Uninstall, true);
            if (gcInterface.Directory.Header.UseCommandOnly) return res;
            else return ExecuteFile(gcInterface, DeviceFileType.UninstallScript, true);
        }
        public bool MonitorInterface(GCInterface gcInterface)
        {
            if (!ValidateInterface(gcInterface)) return false;
            //return TempFunctionHandler("Open interface monitor tools.");
            bool res = ExecuteCommand(gcInterface, CommandType.Monitor, true);
            if (gcInterface.Directory.Header.UseCommandOnly) return res;
            else return ExecuteFile(gcInterface, DeviceFileType.MonitorAssembly, false);
        }
        public bool ConfigInterface(GCInterface gcInterface)
        {
            if (!ValidateInterface(gcInterface)) return false;
            //return TempFunctionHandler("Open interface configuration tools.");
            bool res = ExecuteCommand(gcInterface, CommandType.Config, true);
            if (gcInterface.Directory.Header.UseCommandOnly) return res;
            else return ExecuteFile(gcInterface, DeviceFileType.ConfigAssembly, false);
        }
      
        public bool StartInterface(GCInterface gcInterface)
        {
            if (!ValidateInterface(gcInterface)) return false;
            //return TempFunctionHandler("Start interface's NT service.");
           
            bool res= ExecuteCommand(gcInterface, CommandType.Start, true);
            
            if (gcInterface.Directory.Header.UseCommandOnly) return res;
            else res = ExecuteFile(gcInterface, DeviceFileType.StartScript, true);

            if (!res) return res;
            return ServiceControl.SetServiceStartStyle(gcInterface.InterfaceName, ServiceControl.Automatic);
        }

        public bool StartInterfaceForFileHub(GCInterface gcInterface)
        {
            if (!ValidateInterface(gcInterface)) return false;
            //return TempFunctionHandler("Start interface's NT service.");
         
            bool res = ExecuteCommand(gcInterface, CommandType.Start, true);

            if (gcInterface.Directory.Header.UseCommandOnly) return res;
            else res = ExecuteFile(gcInterface, DeviceFileType.StartScript, true);

            if (!res) return res;
            return ServiceControl.SetServiceStartStyle(gcInterface.InterfaceName, ServiceControl.Automatic);
        }


        public bool StopInterface(GCInterface gcInterface)
        {
            if (!ValidateInterface(gcInterface)) return false;
            //return TempFunctionHandler("Stop interface's NT service.");
           
            bool res = ExecuteCommand(gcInterface, CommandType.Stop, true);

            if (gcInterface.Directory.Header.UseCommandOnly) return res;
            else res = ExecuteFile(gcInterface, DeviceFileType.StopScript, true);

            if (!res) return res;
            return ServiceControl.SetServiceStartStyle(gcInterface.InterfaceName, ServiceControl.Manual);
        }

        public bool RestartInterfaces()
        {
            GCInterfaceCollection list = QueryInterfaceList(true);
            if (list == null) return false;

            foreach (GCInterface i in list)
            {
                if (i.Status == AdapterStatus.Running)
                {
                    if (!StopInterface(i)) return false;
                    if (!StartInterface(i)) return false;
                }
            }

            return true;
        }

        public string[] DetectConfig(GCInterface gcInterface, string path)
        {
            try
            {
                GCError.ClearLastError();
                if (gcInterface == null || !Directory.Exists(path)) return null;

                List<string> list = new List<string>();
                foreach (DeviceFile f in gcInterface.Directory.Files)
                {
                    if (!f.Backupable) continue;
                    string fname = path + "\\" + f.Location;
                    if (File.Exists(fname)) list.Add(fname);
                }

                return list.ToArray();
            }
            catch (Exception err)
            {
                GCError.SetLastError("Detect config failed.");
                GCError.SetLastError(err);
                return null;
            }
        }
        public bool ExportConfig(GCInterface gcInterface, string[] filelist, string path)
        {
            try
            {
                GCError.ClearLastError();
                if (filelist == null || !Directory.Exists(path)) return false;

                int i = 0;
                NotifyStart(filelist.Length, 0, i++, "");
                foreach (string f in filelist)
                {
                    string fn = Path.GetFileName(f);
                    string fname = path + "\\" + fn;
                    File.Copy(f, fname, true);
                    NotifyGoing(i++, "");
                }
                NotifyComplete(true, "");

                DateTime dt = DateTime.Now;
                gcInterface.InterfaceRec.LastBackupDir = path;
                gcInterface.InterfaceRec.LastBackupDateTime = dt.ToShortDateString() + " " + dt.ToLongTimeString();
                if (InterfaceTable.Update(gcInterface.InterfaceRec))
                {   
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception err)
            {
                GCError.SetLastError("Export config failed.");
                GCError.SetLastError(err);
                NotifyComplete(false, "");
                return false;
            }
        }
        public bool ImportConfig(string[] filelist, string path)
        {
            try
            {
                GCError.ClearLastError();
                if (filelist == null || !Directory.Exists(path)) return false;

                int i = 0;
                NotifyStart(filelist.Length, 0, i++, "");
                foreach (string f in filelist)
                {
                    string fn = Path.GetFileName(f);
                    string fname = path + "\\" + fn;
                    File.Copy(f,fname,true);
                    NotifyGoing(i++, "");
                }

                NotifyComplete(true, "");
                return true;
            }
            catch (Exception err)
            {
                GCError.SetLastError("Import config failed.");
                GCError.SetLastError(err);
                NotifyComplete(false, "");
                return false;
            }
        }

        /// <summary>
        /// Note: when copying passive SQL interface, this is only a shadow copy, which results in a new NT service, new tables (always empty), new GC SP and the same inbound/outbound SPs.
        /// </summary>
        /// <param name="fromInterface"></param>
        /// <param name="toInterfaceName"></param>
        /// <param name="toInterfaceDescription"></param>
        /// <returns></returns>
        public GCInterface CopyInterface(GCInterface fromInterface, string toInterfaceName, string toInterfaceDescription)
        {
            try
            {
                GCError.ClearLastError();
                if (fromInterface == null || string.IsNullOrEmpty(toInterfaceName)) return null;
                string fromFolder = ConfigHelper.GetFullPath(fromInterface.FolderPath);
                if (!Directory.Exists(fromFolder)) return null;

                // copy files

                string toPath = Path.Combine(InterfacesFolder, toInterfaceName);
                string toFolder = ConfigHelper.GetFullPath(toPath);
                if (!Directory.Exists(toFolder)) Directory.CreateDirectory(toFolder);
                string sourceDirFile = Path.Combine(fromFolder, DeviceDirManager.IndexFileName);
                string targetDirFile = Path.Combine(toFolder, DeviceDirManager.IndexFileName);
                File.Copy(sourceDirFile, targetDirFile, true);

                int index = 0;
                NotifyStart(fromInterface.Directory.Files.Count + 2, 0, index++, "Copying files...");
                foreach (DeviceFile df in fromInterface.Directory.Files)
                {
                    string f = df.Location;
                    string sourceFile = ConfigHelper.GetFullPath(fromFolder, f);
                    string fn = ConfigHelper.GetRelativePath(fromFolder, sourceFile);
                    string targetFile = ConfigHelper.GetFullPath(toFolder, fn);
                    string path = Path.GetDirectoryName(targetFile);
                    if (!Directory.Exists(path))
                    {
                        if (Directory.CreateDirectory(path) == null)
                        {
                            GCError.SetLastError("Cannot create folder " + path);
                            NotifyComplete(false, "");
                            return null;
                        }
                    }
                    File.Copy(sourceFile, targetFile, true);
                    NotifyGoing(index++, "Copying files...");
                }

                // Update device dir

                GCInterface i = null;
                DeviceDirManager mgr = new DeviceDirManager(targetDirFile);
                if (mgr.LoadDeviceDir())
                {
                    mgr.DeviceDirInfor.Header.ID = "0";
                    mgr.DeviceDirInfor.Header.Name = toInterfaceName;
                    mgr.DeviceDirInfor.Header.Description = toInterfaceDescription;

                    // update interface name and description

                    if (!mgr.SaveDeviceDir())
                    {
                        GCError.SetLastError(mgr.LastError);
                        return null;
                    }

                    // copy object

                    i = new GCInterface();
                    i.Directory = mgr.DeviceDirInfor;
                    i.Device = new GCDevice(mgr.DeviceDirInfor, toPath);
                    i.Device.DeviceID = int.Parse(mgr.DeviceDirInfor.Header.RefDeviceID);
                    i.Device.DeviceName = mgr.DeviceDirInfor.Header.RefDeviceName;
                    i.InterfaceName = toInterfaceName;
                    i.FolderPath = toPath;

                    // AddInterfaceToDatabase & update interface ID to DeviceDir file.

                    if (!AddInterfaceToDatabase(i)) return null;
                    NotifyGoing(index++, "Registering interface...");

                    // update service config

                    DeviceFile df = mgr.DeviceDirInfor.Files.FindFirstFile(DeviceFileType.ServiceConfig);
                    string fn = ConfigHelper.GetFullPath(toFolder, df.Location);
                    AdapterServiceCfgMgt mgt = new AdapterServiceCfgMgt(fn);
                    if (mgt.Load())
                    {
                        mgt.Config.ServiceName = toInterfaceName;
                        if (!mgt.Save())
                        {
                            GCError.SetLastError(mgt.LastError);
                            return null;
                        }
                    }
                    else
                    {
                        GCError.SetLastError(mgt.LastError);
                        return null;
                    }
                }
                else
                {
                    GCError.SetLastError(mgr.LastError);
                    return null;
                }

                NotifyComplete(true, "Copy interface completed.");

                return i;
            }
            catch (Exception err)
            {
                GCError.SetLastError(err);
                NotifyComplete(false, "Copy interface failed.");
                return null;
            }
        }

        private GCInterfaceCollection GetInterfaceList(DObjectCollection dlist, bool withStatus)
        {
            GCInterfaceCollection ilist = new GCInterfaceCollection();
            foreach (InterfaceRec r in dlist)
            {
                GCInterface i = new GCInterface(r);
                ilist.Add(i);

                if (withStatus)
                {
                    i.Status = ServiceControl.GetServiceStatus(i.InterfaceName);
                }
            }
            return ilist;
        }
        public GCInterfaceCollection QueryInterfaceList(bool withStatus)
        {
            GCError.ClearLastError();

            DObjectCollection dlist = InterfaceTable.SelectAll();
            if (dlist == null)
            {
                GCError.SetLastError("Access database failed.");
                return null;
            }

            return GetInterfaceList(dlist, withStatus);
        }
        public GCInterfaceCollection QueryInterfaceList()
        {
            return QueryInterfaceList(true);
        }

        private bool TempFunctionHandler(string funcName)
        {
            GCError.ClearLastError();

            if (MessageBox.Show( funcName + "\r\n\r\nSucceeded?",
                "Just a test", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                GCError.SetLastError("Just a test.");
                return false;
            }
        }
        private bool ValidateInterface(GCInterface gcInterface)
        {
            GCError.ClearLastError();

            // validate interface
            if (gcInterface == null || gcInterface.Directory == null)
            {
                GCError.SetLastError("Invalid interface.");
                return false;
            }

            return true;
        }

        private bool ExecuteDatabaseScript(GCInterface gcInterface, RuleScript script)
        {
            return ExecuteDatabaseScript(gcInterface, script, false);
        }
        private bool ExecuteDatabaseScript(GCInterface gcInterface, RuleScript script, bool backup)
        {
            GCError.ClearLastError();

            // validate interface
            if (script == null || script.Type == RuleScriptType.None )
            {
                GCError.SetLastError("Invalid script.");
                return false;
            }

            string sqlFileName = null;
            if (backup)
            {
                sqlFileName = script.GetBackupFileName();
            }
            else
            {
                sqlFileName = script.FileName;
            }

            try
            {
                sqlFileName = ConfigHelper.GetFullPath(gcInterface.FolderPath + "\\" + sqlFileName);
                if (!File.Exists(sqlFileName)) return true;

                if (!DataBase.OSQLExec(sqlFileName))
                {
                    GCError.SetLastError("Run sql script failed.\r\nOSQL.EXE Path:" + DataBase.OSQLFileName + "\r\nDatabase: " + DataBase.OSQLDatabase + "\r\nSQL File Path:" + sqlFileName);
                    return false;
                }
            }
            catch (Exception err)
            {
                GCError.SetLastError("Run sql script exception.\r\nOSQL.EXE Path:" + DataBase.OSQLFileName + "\r\nDatabase: " + DataBase.OSQLDatabase + "\r\nSQL File Path:" + sqlFileName);
                GCError.SetLastError(err);
                return false;
            }

            return true;
        }
        private bool ExecuteFile(GCInterface gcInterface, DeviceFileType type, bool wait)
        {
            switch (type)
            {
                case DeviceFileType.StartScript:
                    return ServiceControl.SetServiceStatus(gcInterface.InterfaceName, AdapterStatus.Running);
                case DeviceFileType.StopScript:
                    return ServiceControl.SetServiceStatus(gcInterface.InterfaceName, AdapterStatus.Stopped);
            }

            // map type
            DeviceFileType t = type;
            if (type == DeviceFileType.InstallScript ||
                type == DeviceFileType.UninstallScript)
                t = DeviceFileType.ServiceAssembly;

            // find script
            DeviceFile file = gcInterface.Directory.Files.FindFirstFile(t);
            if (file == null)
            {
                GCError.SetLastError("Cannot find file : " + t.ToString() + " in " + gcInterface.ToString());
                return false;
            }

            // find installer
            DeviceFile installer = gcInterface.Directory.Files.FindFirstFile(DeviceFileType.Installer);
            if (installer == null)
            {
                GCError.SetLastError("Cannot find installer in " + gcInterface.ToString());
                return false;
            }

            ProcessControl pc = new ProcessControl();
            string exefilename = ConfigHelper.GetFullPath(gcInterface.FolderPath + "\\" + file.Location);
            string installfilename = ConfigHelper.GetFullPath(gcInterface.FolderPath + "\\" + installer.Location);

            switch (type)
            {
                case DeviceFileType.ConfigAssembly :
                    return ProcessControl.ExecuteAssemblyDirectly(exefilename, AdapterConfigArgument.InIMWizard);
                case DeviceFileType.MonitorAssembly :
                    return ProcessControl.ExecuteAssemblyDirectly(exefilename, "");
                case DeviceFileType.InstallScript:
                    return pc.ExecuteAssembly(installfilename, "\"" + exefilename + "\"", true);
                case DeviceFileType.UninstallScript:
                    return pc.ExecuteAssembly(installfilename, "-u \"" + exefilename + "\"", true);
            }

            return false;
        }

        // execute scripts
        // Notics: Cisco Security Agent will block this function
        //[Obsolete("Execute scripts. Notics: Cisco Security Agent may block this function.", false)]
        private bool ExecuteCommand(GCInterface gcInterface, CommandType type, bool wait)
        {
            // find script
            Command[] cmdList = gcInterface.Directory.Commands.FindCommands(type);
            if (cmdList != null && cmdList.Length > 0)
            {
                // run script
                foreach (Command cmd in cmdList)
                {
                    string exefilename = ConfigHelper.GetFullPath(gcInterface.FolderPath + "\\" + cmd.Path);
                    if (wait)
                    {
                        ProcessControl pc = new ProcessControl();
                        if (!pc.ExecuteAssembly(exefilename, cmd.Argument)) return false;
                    }
                    else
                    {
                        if (!ProcessControl.ExecuteAssemblyDirectly(exefilename, cmd.Argument)) return false;
                    }
                }
            }

            return true;
        }

        #region IProgress Members

        public event ProgressStartEventHandler OnStart;
        private void NotifyStart(int max, int min, int val, string title)
        {
            if (OnStart != null) OnStart(max, min, val, title);
        }

        public event ProgressGoingEventHandler OnGoing;
        private void NotifyGoing(int val, string caption)
        {
            if (OnGoing != null) OnGoing(val, caption);
        }

        public event ProgressCompleteEventHandler OnComplete;
        private void NotifyComplete(bool succeed, string message)
        {
            if (OnComplete != null) OnComplete(succeed, message);
        }

        #endregion

    }
}