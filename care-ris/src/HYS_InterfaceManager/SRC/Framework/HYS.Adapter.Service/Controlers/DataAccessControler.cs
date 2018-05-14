using System;
using System.IO;
using System.Data;
using System.Threading;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Service.Controlers
{
    public class DataAccessControler
    {
        public DataAccessControler()
        {
        }

        //private static Mutex _mutex = new Mutex();

        //protected static bool LockDatabase()
        //{
        //    if (_mutex.WaitOne(Program.ConfigMgt.Config.MutexTimeOut, true)) return true;
        //    Program.Log.Write(LogType.Error, "Mutex time out! (" + Program.ConfigMgt.Config.MutexTimeOut.ToString() + "ms)");
        //    return false;
        //}

        //protected static void ReleaseDatabase()
        //{
        //    _mutex.ReleaseMutex();
        //}

        public static string GetSPName(IRule rule)
        {
            return GWDataDB.GetSPName(Program.DeviceMgt.DeviceDirInfor.Header.Name, rule);
        }

        protected void DumpDataBaseError(Exception err, IRule rule, DataSet data)
        {
            Program.Log.Write("-------- Database Error --------",false);

            Program.Log.Write("\r\n[Exception]", false);

            Program.Log.Write(err.ToString(), false);

            Program.Log.Write("\r\n[SP Name]", false);

            Program.Log.Write(GetSPName(rule), false);

            Program.Log.Write("\r\n[Data]", false);

            WriteDataToFile(rule, data);

            Program.Log.Write("\r\n--------------------------------", false);
        }

        protected void DumpDataBaseError(Exception err, string sql)
        {
            Program.Log.Write("-------- Database Error --------", false);

            Program.Log.Write("\r\n[Exception]", false);

            Program.Log.Write(err.ToString(), false);

            Program.Log.Write("\r\n[SQL Statement]", false);

            Program.Log.Write(sql, false);

            Program.Log.Write("\r\n--------------------------------", false);
        }

        public void WriteDataToFile(IRule rule, DataSet data)
        {
            if (rule == null) return;
            WriteDataToFile(GetSPName(rule), data);
        }

        public void WriteDataToFile(string keyName, DataSet data)
        {
            if (keyName == null) return;

            if (data == null)
            {
                Program.Log.Write(LogType.Warning, "Date set is <null>, can not be serializated.");
                return;
            }

            string path = Application.StartupPath + "\\Temp";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            string fname = path + "\\DataSet_" + keyName + "_" + DateTime.Now.Ticks.ToString() + ".xml";
            data.WriteXml(fname);

            Program.Log.Write(LogType.Debug, "Date set is serialized to " + fname);
        }
    }
}
