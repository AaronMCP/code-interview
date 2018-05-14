using System;
using System.Text;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Device;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class DeviceLicense : XObject
    {
        private DeviceName _name;
        public DeviceName Name
        {
            get { return _name; }
            set { _name = value; }
        }
        
        private DeviceType _type;
        public DeviceType Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private DirectionType _direction;
        public DirectionType Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        private byte _levelID;
        public byte LevelID
        {
            get { return _levelID; }
            set { _levelID = value; }
        }

        private byte _functionControl;
        public byte FunctionControl
        {
            get { return _functionControl; }
            set { _functionControl = value; }
        }

        public byte GetValue()
        {
            return (byte)((byte)(_levelID << 4) | _functionControl);
        }
        public void SetValue(byte value)
        {
            _levelID = (byte)(value >> 4);
            _functionControl = (byte)(value & 0x0F);
        }

        public DeviceLicense()
        {
        }
        public DeviceLicense(DeviceName name, DeviceType type, DirectionType direction, byte value)
        {
            _name = name;
            _type = type;
            _direction = direction;
            SetValue(value);
        }
        public DeviceLicense(DeviceName name, DeviceType type, DirectionType direction, byte levelID, byte functionControl)
        {
            _name = name;
            _type = type;
            _levelID = levelID;
            _direction = direction;
            _functionControl = functionControl;
        }
    }
}
