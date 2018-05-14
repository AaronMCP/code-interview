using System;
using System.Collections.Generic;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Device;

namespace HYS.IM.BusinessControl.DataControl
{
    public class CombinationRecManager : DObjectManager
    {
        public CombinationRecManager(DataBase db)
            : base(db, "Combination", typeof(CombinationRec))
        {
            interfaceMgt = new InterfaceRecManager(db);
        }

        private InterfaceRecManager interfaceMgt;
        public DObjectCollection GetCombinedInterfaces(string interfaceName, DirectionType type)
        {
            if (interfaceName == null || interfaceName.Length < 1 || type == DirectionType.UNKNOWN) return null;

            if (type == DirectionType.BIDIRECTIONAL) return new DObjectCollection();
            
            string strSql = "";
            switch (type)
            {
                case DirectionType.OUTBOUND:
                    strSql = "SELECT * FROM " + TableName + " WHERE DataOut='" + interfaceName + "'";
                    break;
                case DirectionType.INBOUND:
                    strSql = "SELECT * FROM " + TableName + " WHERE DataIn='" + interfaceName + "'";
                    break;
            }

            DObjectCollection clist = Select(strSql);
            if (clist == null) return null;

            List<string> outList = new List<string>();
            foreach (CombinationRec c in clist)
            {
                switch (type)
                {
                    case DirectionType.OUTBOUND:
                        outList.Add(c.DataIn);
                        break;
                    case DirectionType.INBOUND:
                        outList.Add(c.DataOut);
                        break;
                }
            }
            if (outList.Count < 1) return new DObjectCollection();

            strSql = "SELECT * FROM " + interfaceMgt.TableName + " WHERE INTERFACE_NAME IN (";
            foreach (string outName in outList)
            {
                strSql += "'" + outName + "',";
            }
            strSql = strSql.TrimEnd(',') + ")";

            return interfaceMgt.Select(strSql);
        }
    }
}
