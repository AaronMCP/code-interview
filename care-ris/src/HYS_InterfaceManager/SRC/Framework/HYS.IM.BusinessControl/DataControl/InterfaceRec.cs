using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.DataAccess;

namespace HYS.IM.BusinessControl.DataControl
{
    public class InterfaceRec : DObject
    {
        private int _id;
        [DMainKey, DAutoIncrementing]
        [DField("INTERFACE_ID", "int IDENTITY(1,1) PRIMARY KEY")]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name = "";
        [DField("INTERFACE_NAME", "nvarchar(64)")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _description = "";
        [DField("INTERFACE_DESC", "nvarchar(255)")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private int _deviceID;
        [DField("DEVICE_ID", "int")]
        public int DeviceID
        {
            get { return _deviceID; }
            set { _deviceID = value; }
        }

        private string _deviceName = "";
        [DField("DEVICE_NAME", "nvarchar(64)")]
        public string DeviceName
        {
            get { return _deviceName; }
            set { _deviceName = value; }
        }

        private string _direction = "";
        [DField("DEVICE_DIRECT", "nvarchar(1)")]
        public string Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private string _type = "";
        [DField("DEVICE_TYPE", "nvarchar(2)")]
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _folder = "";
        [DField("FILE_FOLDER", "nvarchar(255)")]
        public string Folder
        {
            get { return _folder; }
            set { _folder = value; }
        }

        private string _file = "";
        [DField("INDEX_FILE", "nvarchar(255)")]
        public string IndexFile
        {
            get { return _file; }
            set { _file = value; }
        }

        private string _last_backup_dir = "";
        [DField("LAST_BACKUP_DIR", "nvarchar(255)")]
        public string LastBackupDir
        {
            get { return _last_backup_dir; }
            set { _last_backup_dir = value; }
        }

        private string _last_backup_datetime = "";
        [DField("LAST_BACKUP_DATETIME", "nvarchar(64)")]
        public string LastBackupDateTime
        {
            get { return _last_backup_datetime; }
            set { _last_backup_datetime = value; }
        }

        private string _eventType = "";
        [DField("EVENT_TYPE", "nvarchar(255)")]
        public string EventType
        {
            get { return _eventType; }
            set { _eventType = value; }
        }
    }
}
