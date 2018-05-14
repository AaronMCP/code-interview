using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.DataAccess;
using HYS.IM.BusinessEntity;
using HYS.IM.BusinessControl.SystemControl;
using HYS.IM.BusinessControl.DataControl;
using HYS.Common.Objects.Device;

namespace HYS.IM.BusinessControl
{
    /// <summary>
    /// Device management class.
    /// Implementation of "Device Management" use case.
    /// </summary>
    public class GCDeviceManager
    {
        public DeviceRecManager DeviceTable;
        public readonly string DevicesFolder;
        public GCDeviceManager(DataBase db, string devicesFolder)
        {
            DevicesFolder = devicesFolder;
            DeviceTable = new DeviceRecManager(db);
        }

        public GCDevice AddDevice(string folder)
        {
            GCError.ClearLastError();

            // check if the folder contains a device
            if (!FolderControl.IsDevicePath(folder))
            {
                GCError.SetLastError("Invalid device folder.");
                return null;
            }

            // load device information from DeviceDir file
            GCDevice device = FolderControl.LoadDevice(folder);
            if (device == null)
            {
                GCError.SetLastError("Invalid device index file.");
                return null;
            }

            // check whether a same device has already been installed
            // (if we do not do this, license control on max interface count will be useless.
            if (DeviceTable.HasSameDevice(device))
            {
                GCError.SetLastError("Device (Type: " + device.Directory.Header.Type.ToString()
                    + ", Direction: " + device.Directory.Header.Direction.ToString()
                    + ") has already been installed.");
                return null;
            }

            // insert device information into database
            if (!DeviceTable.InsertDevice(device))
            {
                GCError.SetLastError("Insert database failed.");
                return null;
            }

            // get device id for return;
            device.DeviceID = DeviceTable.GetMaxID();
            if (device.DeviceID < 1)
            {
                GCError.SetLastError("Invalid device ID.");
                return null;
            }

            // create device folder
            string folderName = DevicesFolder + "\\" + device.DeviceName;
            try
            {
                FolderControl.DeleteDevice(folderName);     //according to defect EK_HI00045030 , 2006/11/20
                Directory.CreateDirectory(folderName);
            }
            catch (Exception err)
            {
                GCError.SetLastError(err);
                GCError.SetLastError("Cannot create directory " + folderName);
                DeviceTable.DeleteDevice(device.DeviceID);
                return null;
            }

            // copy device files
            string sourceFile = "";
            string targetFile = "";
            try
            {
                sourceFile = device.FolderPath + "\\" + DeviceDirManager.IndexFileName;
                targetFile = folderName + "\\" + DeviceDirManager.IndexFileName;
                File.Copy(sourceFile, targetFile);

                foreach (DeviceFile f in device.Directory.Files)
                {
                    sourceFile = device.FolderPath + "\\" + f.Location;
                    targetFile = folderName + "\\" + f.Location;

                    // support sub folder in device dir
                    string path = Path.GetDirectoryName(targetFile);
                    if (!Directory.Exists(path))
                    {
                        if (Directory.CreateDirectory(path) == null)
                        {
                            GCError.SetLastError("Cannot create folder " + path);
                            return null;
                        }
                    }

                    File.Copy(sourceFile, targetFile);
                }
            }
            catch (Exception err)
            {
                GCError.SetLastError(err);
                GCError.SetLastError("Cannot copy file from " + sourceFile + " to " + targetFile);
                DeviceTable.DeleteDevice(device.DeviceID);
                return null;
            }

            // update DeviceDir: save device id 
            GCDevice newDevice = FolderControl.LoadDevice(folderName);
            newDevice.Directory.Header.ID = device.DeviceID.ToString();
            if (!FolderControl.SaveDevice(newDevice))
            {
                GCError.SetLastError("Update device index file failed.");
                return null;
            }

            // update device record
            device.FolderPath = folderName;
            if (!DeviceTable.UpdateDevice(device))
            {
                GCError.SetLastError("Update database failed.");
                return null;
            }

            return device;
        }
        public bool DeleteDevice(GCDevice device)
        {
            GCError.ClearLastError();
            
            //validate device
            if (device == null||device.DeviceID < 0)
            {
                GCError.SetLastError("Invalid device.");
                return false;
            }

            //delete record from database
            if (!DeviceTable.DeleteDevice(device.DeviceID))
            {
                GCError.SetLastError("Delete device from database failed.");
                return false;
            }

            //delete device folder
            if (!FolderControl.DeleteDevice(device.FolderPath))
            {
                GCError.SetLastError("Delete device folder " + device.FolderPath + " failed.");
                return false;
            }

            return true;
        }

        private GCDeviceCollection GetDeviceList(DObjectCollection dlist)
        {
            GCDeviceCollection deviceList = new GCDeviceCollection();
            foreach (DeviceRec d in dlist)
            {
                GCDeviceAgent device = new GCDeviceAgent(d);
                deviceList.Add(device);
            }
            return deviceList;
        }
        public GCDeviceCollection QueryOutboundDevice()
        {
            GCError.ClearLastError();

            DObjectCollection dlist = DeviceTable.GetOutboundDevice();
            if (dlist == null)
            {
                GCError.SetLastError("Access database failed.");
                return null;
            }

            return GetDeviceList(dlist);
        }
        public GCDeviceCollection QueryInboundDevice()
        {
            GCError.ClearLastError();

            DObjectCollection dlist = DeviceTable.GetInboundDevice();
            if (dlist == null)
            {
                GCError.SetLastError("Access database failed.");
                return null;
            }

            return GetDeviceList(dlist);
        }
        public GCDeviceCollection QueryBidirectionalDevice()
        {
            GCError.ClearLastError();

            DObjectCollection dlist = DeviceTable.GetBidirectionalDevice();
            if (dlist == null)
            {
                GCError.SetLastError("Access database failed.");
                return null;
            }

            return GetDeviceList(dlist);
        }
        public GCDeviceCollection QueryDeviceList()
        {
            GCError.ClearLastError();

            DObjectCollection dlist = DeviceTable.SelectAll();
            if (dlist == null)
            {
                GCError.SetLastError("Access database failed.");
                return null;
            }

            return GetDeviceList(dlist);
        }
    }
}
