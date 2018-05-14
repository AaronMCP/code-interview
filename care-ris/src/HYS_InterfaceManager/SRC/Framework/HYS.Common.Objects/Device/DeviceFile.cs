using System;
using System.IO;
using System.ComponentModel;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Device
{
    public class DeviceFile : XObject
    {
        private DeviceFileType _type = DeviceFileType.Other;
        [Description("Type of file. When the value of property EnableCommands in DeviceDir header information is False, IM will interact with the interface by invoking specific type of files in File List.")]
        public DeviceFileType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private bool _needBackup = false;
        [Description("Whether to be imported/outported by IM when user wants to backup the configuration of this interface.")]
        public bool Backupable
        {
            get { return _needBackup; }
            set { _needBackup = value; }
        }

        private string _location;
        [Description("File name. Should be an indirect path, from the directory of this interface.")]
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }

        private string _description;
        [Description("File description.")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public override string ToString()
        {
            return "[" + Type + "] - " + Path.GetFileName(Location);
        }

        public DeviceFile()
        {
        }
        public DeviceFile(DeviceFileType type, string filename, bool needBackup)
        {
            _type = type;
            _location = filename;
            _needBackup = needBackup;
        }
        public DeviceFile(DeviceFileType type, string filename)
            : this(type, filename, false)
        {
        }
    }
}
