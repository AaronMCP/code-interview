using System;
using System.Data.OleDb;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using HYS.Common.DataAccess;
using HYS.IM.BusinessEntity;

namespace HYS.IM.BusinessControl.DataControl
{
    public class InterfaceRecManager : DObjectManager
    {
        public InterfaceRecManager(DataBase db)
            : base(db, "Interface", typeof(InterfaceRec))
        {
        }

        public bool HasInterface(int deviceID)
        {
            string sql = "SELECT * FROM " + TableName + " WHERE DEVICE_ID=" + deviceID.ToString();
            DObjectCollection olist = Select(sql);
            return olist == null || olist.Count > 0;
        }
        public bool HasSampleName(string name)
        {
            string sql = "SELECT * FROM " + TableName + " WHERE INTERFACE_NAME='" + name + "'";
            DObjectCollection olist = Select(sql);
            return olist == null || olist.Count > 0;
        }
        public bool InsertInterface(GCInterface i)
        {
            InterfaceRec rec = DataHelper.CreateInterfaceRec(i);
            if (rec == null) return false;
            return Insert(rec);
        }
        public bool DeleteInterface(GCInterface i)
        {
            if (i == null) return false;

            InterfaceRec rec = new InterfaceRec();
            rec.ID = i.InterfaceID;
            if (!Delete(rec)) return false;

            string sql = "DELETE FROM Combination WHERE DataIn = '" + i.InterfaceName + "'";
            return Execute(sql);
        }

        public Dictionary<int, int> GetInterfaceCount(int[] deviceIDs)
        {
            if (deviceIDs == null || deviceIDs.Length < 1) return null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ")
                .Append("Interface.Device_ID AS device_id, ")
                .Append("count(Interface.Interface_ID) AS interface_count ")
                .Append("FROM Interface ")
                .Append("WHERE Interface.Device_ID in (");

            foreach (int id in deviceIDs)
            {
                sb.Append(id.ToString()).Append(",");
            }

            string sql = sb.ToString().TrimEnd(',');
            sql += ") GROUP BY interface.Device_ID";

            Dictionary<int, int> list = null;
            OleDbDataReader reader = DataBase.DoQuery(sql);
            if (reader != null)
            {
                list = new Dictionary<int, int>();
                foreach (DbDataRecord record in reader)
                {
                    int deviceID = (int)record["device_id"];
                    int interfaceCount = (int)record["interface_count"];
                    list.Add(deviceID, interfaceCount);
                }
            }
            DataBase.CloseDBConnection();

            foreach (int id in deviceIDs)
            {
                if (list.ContainsKey(id)) continue;
                list.Add(id, 0);
            }

            return list;
        }
    }
}
