using System;
using System.IO;
using System.Data;
using System.Timers;
using System.Data.OleDb;
using System.Collections.Generic;
using HYS.Adapter.Base;
using HYS.Common.Objects.Rule;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Service.Controlers
{
    public class GarbageControler : DataAccessControler
    {
        private Timer _timer;

        private string _interfaceName;

        public GarbageControler()
        {
            AttachAdapter();

            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }

        private void AttachAdapter()
        {
            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;
            if (dir == null || dir.Header == null) return;

            _interfaceName = dir.Header.Name;

            Program.Log.Write("Garbage Colletor initialized " +
                " (InterfaceName=" + _interfaceName +
                ", Enable=" + Program.ConfigMgt.Config.GarbageCollection.Enable.ToString() +
                ", ParticularTime=" + Program.ConfigMgt.Config.GarbageCollection.StartAtParticularTime.ToString() + ").");
        }

        public void Start()
        {
            if (!Program.ConfigMgt.Config.GarbageCollection.Enable)
            {
                Program.Log.Write(LogType.Warning, "Garbage Colletor disabled.");
                return;
            }

            if (Program.ConfigMgt.Config.GarbageCollection.StartAtParticularTime)
            {
                _timer.Interval = Program.ConfigMgt.Config.GarbageCollection.ParticularTime.Interval;
            }
            else
            {
                _timer.Interval = Program.ConfigMgt.Config.GarbageCollection.Interval;
            }

            _timer.Start();

            Program.Log.Write("Garbage Colletor started.");
        }

        public void Stop()
        {
            _timer.Stop();
            Program.Log.Write("Garbage Colletor stopped.");
        }

        #region expired code
        //private void ProcessGarbageCollection()
        //{
        //    //if (!Program.ConfigMgt.Config.GarbageCollection.Enable)
        //    //{
        //    //    Program.Log.Write(LogType.Warning, "Skip garbage collection.");
        //    //    return;
        //    //}

        //    int result = -1;
        //    SafeDBConnection cn = SafeDBConnection.Instance;
        //    //OleDbConnection cn = null;

        //    //if (!LockDatabase())
        //    //{
        //    //    Program.Log.Write(LogType.Warning, "Cannot access database without mutex.");
        //    //    return;
        //    //}

        //    //#region critical resource
            
        //    try
        //    {
        //        //cn = new OleDbConnection();
        //        //cn.ConnectionString = Program.ConfigMgt.Config.DataDBConnection;

        //        string spName = RuleControl.GetCreateGarbageCollectionSPName(_interfaceName);

        //        OleDbCommand cmd = new OleDbCommand(spName, cn.Connection);
        //        cmd.CommandTimeout = Program.ConfigMgt.Config.GarbageCollection.SqlCommandTimeOutInSecond;
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //        OleDbParameter paramProcessFlag = new OleDbParameter();
        //        paramProcessFlag.Direction = ParameterDirection.Input;
        //        paramProcessFlag.ParameterName = "@ProcessFlag";
        //        paramProcessFlag.OleDbType = OleDbType.VarWChar;
        //        cmd.Parameters.Add(paramProcessFlag);

        //        if (Program.ConfigMgt.Config.GarbageCollection.CheckProcessFlag)
        //        {
        //            paramProcessFlag.Value = RuleControl.ProcessFlagValueForProcessed;
        //            Program.Log.Write("Check process flag.");
        //        }
        //        else
        //        {
        //            paramProcessFlag.Value = DBNull.Value;
        //            Program.Log.Write("Do not check process flag.");
        //        }

        //        OleDbParameter paramFromTime = new OleDbParameter();
        //        paramFromTime.Direction = ParameterDirection.Input;
        //        paramFromTime.ParameterName = "@FromDateTime";
        //        paramFromTime.OleDbType = OleDbType.DBTimeStamp;
        //        paramFromTime.Value = DBNull.Value;
        //        cmd.Parameters.Add(paramFromTime);

        //        OleDbParameter paramToTime = new OleDbParameter();
        //        paramToTime.Direction = ParameterDirection.Input;
        //        paramToTime.ParameterName = "@ToDateTime";
        //        paramToTime.OleDbType = OleDbType.DBTimeStamp;
        //        cmd.Parameters.Add(paramToTime);

        //        if (Program.ConfigMgt.Config.GarbageCollection.CheckExpireTime)
        //        {
        //            DateTime dtNow = DateTime.Now;
        //            DateTime dtExpire = dtNow.Subtract(Program.ConfigMgt.Config.GarbageCollection.ExpireTime);
        //            Program.Log.Write("Check expire time: " + dtExpire.ToShortDateString() + " " + dtExpire.ToLongTimeString());
        //            paramToTime.Value = dtExpire;
        //        }
        //        else
        //        {
        //            paramToTime.Value = DBNull.Value;
        //            Program.Log.Write("Do not check expire time.");
        //        }

        //        OleDbParameter paramResult = new OleDbParameter();
        //        paramResult.Direction = ParameterDirection.Output;
        //        paramResult.ParameterName = "@result";
        //        paramResult.OleDbType = OleDbType.Integer;
        //        cmd.Parameters.Add(paramResult);

        //        cn.Open();
        //        result = cmd.ExecuteNonQuery();
        //        cn.Close();

        //        Program.Log.Write("Process garbage collection success. Result value: " + paramResult.Value + ", number of rows: " + result.ToString());
        //    }
        //    catch (Exception err)
        //    {
        //        Program.Log.Write(LogType.Warning, "Process garbage collection failed.");
        //        DumpDataBaseError(err, "");
        //    }
        //    finally
        //    {
        //        if (cn != null) cn.Close();
        //    }

        //    //#endregion

        //    //ReleaseDatabase();
        //}
        #endregion

        private void ProcessGarbageCollection2()
        {
            bool result = false;
            //SafeDBConnection cn = SafeDBConnection.Instance;
            OleDbConnection cnn = new OleDbConnection(Program.ConfigMgt.Config.DataDBConnection);

            try
            {
                string spName = RuleControl.GetCreateGarbageCollectionSPName(_interfaceName);

                //OleDbCommand cmd = new OleDbCommand(spName, cn.Connection);
                OleDbCommand cmd = new OleDbCommand(spName, cnn);
                cmd.CommandTimeout = Program.ConfigMgt.Config.GarbageCollection.SqlCommandTimeOutInSecond;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                OleDbParameter paramToTime = new OleDbParameter();
                paramToTime.Direction = ParameterDirection.Input;
                paramToTime.ParameterName = "@ToDateTime";
                paramToTime.OleDbType = OleDbType.DBTimeStamp;
                cmd.Parameters.Add(paramToTime);

                if (Program.ConfigMgt.Config.GarbageCollection.CheckExpireTime)
                {
                    DateTime dtNow = DateTime.Now;
                    DateTime dtExpire = dtNow.Subtract(Program.ConfigMgt.Config.GarbageCollection.ExpireTime);
                    Program.Log.Write("Check expire time: " + dtExpire.ToShortDateString() + " " + dtExpire.ToLongTimeString());
                    paramToTime.Value = dtExpire;
                }
                else
                {
                    paramToTime.Value = DBNull.Value;
                    Program.Log.Write("Do not check expire time.");
                }

                OleDbParameter paramResult = new OleDbParameter();
                paramResult.Direction = ParameterDirection.Output;
                paramResult.ParameterName = "@result";
                paramResult.OleDbType = OleDbType.Integer;
                cmd.Parameters.Add(paramResult);

                //cn.Open();
                cnn.Open();
                cmd.ExecuteNonQuery();
                //cn.Close();
                cnn.Close();
                cnn.Dispose();

                result = true;
                Program.Log.Write("Process garbage collection success. result: " + result.ToString() + ", number of rows: " + paramResult.Value);
            }
            catch (Exception err)
            {
                Program.Log.Write(LogType.Warning, "Process garbage collection failed.");
                DumpDataBaseError(err, "");
            }
            finally
            {
                //if (!result) cn.Close();
                if (!result) cnn.Close(); cnn.Dispose();
            }
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;

            bool doGC = true;

            if (Program.ConfigMgt.Config.GarbageCollection.StartAtParticularTime)
            {
                doGC = Program.ConfigMgt.Config.GarbageCollection.ParticularTime.TimeIsUp();
            }

            if (doGC)
            {
                Program.Log.Write("-- Garbage Collector begin --");
                ProcessGarbageCollection2(); 
                Program.Log.Write("-- Garbage Collector end --\r\n");
            }

            _timer.Enabled = true;
        }
    }
}
