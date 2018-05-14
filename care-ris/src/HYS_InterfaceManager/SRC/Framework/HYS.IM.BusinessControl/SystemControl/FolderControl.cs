using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using HYS.Common.Xml;
using HYS.IM.BusinessEntity;
using HYS.Common.Objects.Device;

namespace HYS.IM.BusinessControl.SystemControl
{
    /// <summary>
    /// Device file and folder control class.
    /// </summary>
    public class FolderControl
    {
        #region DeviceDir File I/O
        private static bool _SaveDeviceDir(IDevice device)
        {
            if (device == null) return false;
            string fname = device.FolderPath + "\\" + DeviceDirManager.IndexFileName;
            using (StreamWriter sw = File.CreateText(fname))
            {
                string xmlstr = device.Directory.ToXMLString();
                sw.Write(DeviceDirManager.XMLHeader + xmlstr);
                return true;
            }
        }
        private static DeviceDir _LoadDeviceDir(string deviceFolder)
        {
            string fname = deviceFolder + "\\" + DeviceDirManager.IndexFileName;
            using (StreamReader sr = File.OpenText(fname))
            {
                string xmlstr = sr.ReadToEnd();
                DeviceDir dir = XObjectManager.CreateObject(xmlstr, typeof(DeviceDir)) as DeviceDir;
                return dir;
            }
        }
        #endregion

        public static bool IsDevicePath( string path )
        {
            string fname = path + "\\" + DeviceDirManager.IndexFileName;
            return File.Exists(fname);
        }
       
        public static GCDevice LoadDevice(string devicePath)
        {
            if (!IsDevicePath(devicePath)) return null;

            try
            {
                GCError.ClearLastError();
                DeviceDir dir = _LoadDeviceDir(devicePath);
                return new GCDevice(dir, devicePath);
            }
            catch (Exception e)
            {
                GCError.SetLastError(e);
                return null;
            }
        }
        public static GCDeviceCollection LoadDeviceDirectory(string devicePath)
        {
            if (!IsDevicePath(devicePath)) return null;

            try
            {
                GCError.ClearLastError();
                GCDeviceCollection dlist = new GCDeviceCollection();
                string[] subFolders = Directory.GetDirectories(devicePath);
                foreach (string sf in subFolders)
                {
                    string folder = devicePath + "\\" + sf;
                    GCDevice device = LoadDevice(folder);
                    dlist.Add(device);
                }
                return dlist;
            }
            catch (Exception e)
            {
                GCError.SetLastError(e);
                return null;
            }
        }

        public static bool SaveDevice(IDevice device)
        {
            if (device==null) return false;

            try
            {
                GCError.ClearLastError();
                return _SaveDeviceDir(device);
            }
            catch (Exception e)
            {
                GCError.SetLastError(e);
                return false;
            }
        }

        public static bool DeleteDevice(string devicePath)
        {
            try
            {
                if (!Directory.Exists(devicePath)) return true;

                string[] subDirs = Directory.GetDirectories(devicePath);
                foreach (string dir in subDirs)
                {
                    if (!DeleteDevice(dir)) return false;
                }

                string[] files = Directory.GetFiles(devicePath);
                foreach (string f in files)
                {
                    File.Delete(f);
                }

                Directory.Delete(devicePath);
                return true;
            }
            catch (Exception e)
            {
                GCError.SetLastError(e);
                return false;
            }
        }

        public static void BrowseFolder(string path)
        {
            Process.Start("explorer.exe", path);
        }
    }
}
