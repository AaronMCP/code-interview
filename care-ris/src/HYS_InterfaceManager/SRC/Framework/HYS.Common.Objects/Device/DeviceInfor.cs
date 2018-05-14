using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;

namespace HYS.Common.Objects.Device
{
    public class DeviceInfor : XObject
    {
        private DeviceType _type;
        [Category("Should be modified when composing a device")]
        [Description("Type of device/interface.")]
        public DeviceType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private DirectionType _direction;
        [Category("Should be modified when composing a device")]
        [Description("Direction of device/interface.")]
        public DirectionType Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private string _id = "";
        [Category("Will be modify by IM when installing an interface")]
        [Description("Device/Interface ID. Sample with the main key in database.")]
        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name = "";
        [Category("Should be modified when composing a device")]
        [Description("Device/Interface name.")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _version = "";
        [Category("Should be modified when composing a device")]
        [Description("Device version.")]
        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        private bool _useCommandOnly = false;
        [Category("Should be modified when composing a device")]
        [Description("Tell IM whether only to use commands from the Command List to interact with interface while interface being installed/uninstalled/started/stopped/configured/monitored. If the value of this property is False, IM will interact with interface by invoking specific type of commands/files both in the Command List (firstly) and in the File List (secondly).")]
        public bool UseCommandOnly
        {
            get { return _useCommandOnly; }
            set { _useCommandOnly = value; }
        }

        private string _description = "";
        [Category("Should be modified when composing a device")]
        [Description("Device/Interface description.")]
        [XCData(true)]
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private string _configurationSummary = "";
        [XCData(true)]
        [Browsable(false)]
        public string ConfigurationSummary
        {
            get { return _configurationSummary; }
            set { _configurationSummary = value; }
        }

        private string _refDeviceID = "";
        [Category("Will be modify by IM when installing an interface")]
        [Description("Relative device ID. Sample with the main key in database.")]
        public string RefDeviceID
        {
            get { return _refDeviceID; }
            set { _refDeviceID = value; }
        }

        private string _refDeviceName = "";
        [Category("Will be modify by IM when installing an interface")]
        [Description("Relative device name.")]
        public string RefDeviceName
        {
            get { return _refDeviceName; }
            set { _refDeviceName = value; }
        }

        [Category("Will be modify by Adapter.Config (General Tab Page) when installing an interface on IM, better remain its default value.")]
        [Description("Event types to be handled.")]
        [XNode(false)]
        public string EventType
        {
            get 
            {
                StringBuilder sb = new StringBuilder();
                foreach (GWEventType et in EventTypes)
                {
                    sb.Append(et.ToString());
                }
                return sb.ToString();
            }
            set
            {
                EventTypes.Clear();
                GWEventType[] etList = GWEventType.Parse(value);
                if (etList == null) return;
                foreach (GWEventType et in etList)
                {
                    EventTypes.Add(et);
                }
            }
        }

        private XCollection<GWEventType> _eventTypes = new XCollection<GWEventType>();
        [Browsable(false)]
        public XCollection<GWEventType> EventTypes
        {
            get { return _eventTypes; }
            set { _eventTypes = value; }
        }
    }
}
