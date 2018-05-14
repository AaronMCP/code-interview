using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.DataAccess;

namespace HYS.IM.BusinessControl.DataControl
{
    public class DeviceRec : DObject
    {
        private int _id;
        [DMainKey, DAutoIncrementing]
        [DField("DEVICE_ID", "int IDENTITY(1,1) PRIMARY KEY")]
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name = "";
        [DField("DEVICE_NAME", "nvarchar(64)")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
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

        private string _description = "";
        [DField("DEVICE_DESC", "nvarchar(255)")]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
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
    }
}
