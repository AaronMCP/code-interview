using System;
using System.Collections.Generic;
using System.Text;
using HYS.IM.BusinessEntity;
using HYS.Common.Objects.Device;

namespace HYS.IM.BusinessControl.DataControl
{
    public class DataHelper
    {
        public static InterfaceRec CreateInterfaceRec(GCInterface i)
        {
            if (i == null) return null;

            GCDevice device = i.Device;
            DeviceDir idir = i.Directory;
            InterfaceRec rec = i.InterfaceRec;

            rec.Folder = i.FolderPath;
            rec.Name = idir.Header.Name;
            rec.DeviceID = device.DeviceID;
            rec.DeviceName = device.DeviceName;
            rec.IndexFile = DeviceDirManager.IndexFileName;
            rec.Type = ((int)device.Directory.Header.Type).ToString();
            rec.Description = device.Directory.Header.Description;
            rec.EventType = idir.Header.EventType;

            switch (device.Directory.Header.Direction)
            {
                case DirectionType.INBOUND:
                    rec.Direction = "I";
                    break;
                case DirectionType.OUTBOUND:
                    rec.Direction = "O";
                    break;
                case DirectionType.BIDIRECTIONAL :
                    rec.Direction = "B";
                    break;
            }

            return rec;
        }

        public static DeviceRec CreateDeviceRec(GCDevice device)
        {
            if (device == null) return null;
            DeviceRec rec = new DeviceRec();

            rec.ID = device.DeviceID;
            rec.Folder = device.FolderPath;
            rec.IndexFile = DeviceDirManager.IndexFileName;
            rec.Name = device.Directory.Header.Name;
            rec.Type = ((int)device.Directory.Header.Type).ToString();
            rec.Description = device.Directory.Header.Description;

            switch (device.Directory.Header.Direction)
            {
                case DirectionType.INBOUND:
                    rec.Direction = "I";
                    break;
                case DirectionType.OUTBOUND:
                    rec.Direction = "O";
                    break;
                case DirectionType.BIDIRECTIONAL:
                    rec.Direction = "B";
                    break;
            }

            return rec;
        }

        public static DirectionType GetDirection(string direction)
        {
            switch (direction)
            {
                case "I": return DirectionType.INBOUND;
                case "O": return DirectionType.OUTBOUND;
                case "B": return DirectionType.BIDIRECTIONAL;
                default: return DirectionType.UNKNOWN;
            }
        }

        public static string GetDirectionName(string direction)
        {
            DirectionType t = GetDirection(direction);
            if (t == DirectionType.UNKNOWN) return "";
            return t.ToString();
        }

        public static DeviceType GetType(string type)
        {
            try
            {
                int i = int.Parse(type);
                return (DeviceType)i;
            }
            catch
            {
                return DeviceType.UNKNOWN;
            }
        }

        public static string GetTypeName(string type)
        {
            DeviceType t = GetType(type);
            return t.ToString();
        }
    }
}
