using System;
using System.Collections.Generic;
using System.Text;
using HYS.Common.DataAccess;
using HYS.IM.BusinessEntity;

namespace HYS.IM.BusinessControl.DataControl
{
    /// <summary>
    /// Device data table access control class 
    /// </summary>
    public class DeviceRecManager : DObjectManager
    {
        public DeviceRecManager( DataBase db )
            : base(db, "Device", typeof(DeviceRec))
        {
        }

        public bool InsertDevice(GCDevice device)
        {
            DeviceRec rec = DataHelper.CreateDeviceRec(device);
            if (rec == null) return false;
            return Insert(rec);
        }
        public bool DeleteDevice(int deviceID)
        {
            DeviceRec rec = new DeviceRec();
            rec.ID = deviceID;
            return Delete(rec);
        }
        public bool UpdateDevice(GCDevice device)
        {
            DeviceRec rec = DataHelper.CreateDeviceRec(device);
            if (rec == null) return false;
            return Update(rec);
        }
        public GCDevice GetDeviceByID(int deviceID)
        {
            string sql = "SELECT * FROM " + TableName + " WHERE DEVICE_ID=" + deviceID.ToString();
            
            DObjectCollection dlist = Select(sql);
            if (dlist == null || dlist.Count < 1) return null;

            DeviceRec rec = dlist[0] as DeviceRec;
            return new GCDeviceAgent(rec);
        }
        public DObjectCollection GetOutboundDevice()
        {
            string sql = "SELECT * FROM " + TableName + " WHERE DEVICE_DIRECT='O'";
            return Select(sql);
        }
        public DObjectCollection GetInboundDevice()
        {
            string sql = "SELECT * FROM " + TableName + " WHERE DEVICE_DIRECT='I'";
            return Select(sql);
        }
        public DObjectCollection GetBidirectionalDevice()
        {
            string sql = "SELECT * FROM " + TableName + " WHERE DEVICE_DIRECT='B'";
            return Select(sql);
        }
        public bool HasSameDevice(GCDevice device)
        {
            DeviceRec rec = DataHelper.CreateDeviceRec(device);
            if (rec == null) return true;

            string sql = "SELECT * FROM " + TableName + " WHERE DEVICE_DIRECT='" + rec.Direction + "' AND DEVICE_TYPE='" + rec.Type + "' AND DEVICE_NAME='" + rec.Name + "'";

            DObjectCollection dlist = Select(sql);
            if (dlist == null) return true;     //db query failed.
            if (dlist.Count > 0) return true;

            return false;
        }
    }
}
