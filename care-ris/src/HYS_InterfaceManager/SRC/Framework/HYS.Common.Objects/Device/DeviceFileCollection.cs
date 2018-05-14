using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Device
{
    public class DeviceFileCollection : XObjectCollection
    {
        public DeviceFileCollection() : base( typeof ( DeviceFile ) )
        {
        }

        public DeviceFile FindFirstFile(DeviceFileType type)
        {
            foreach (DeviceFile f in this)
            {
                if (f.Type == type) return f;
            }
            return null;
        }

        public DeviceFile[] FindFiles(DeviceFileType type)
        {
            List<DeviceFile> list = new List<DeviceFile>();
            foreach (DeviceFile f in this)
            {
                if (f.Type == type) list.Add(f);
            }
            return list.ToArray();
        }

        public bool HasBackupableFile()
        {
            foreach (DeviceFile f in this)
            {
                if (f.Backupable) return true;
            }
            return false;
        }
    }
}
