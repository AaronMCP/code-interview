using System;
using System.Data;
using System.Timers;
using System.Threading;
using System.Data.OleDb;
using System.Runtime.CompilerServices;
using HYS.Adapter.Base;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Service.Controlers
{
    // 20110915
    // When the mutex timeout, the situation will be too complex to handle, 
    // which will cause different exceptions or even deadlock (block the data outbound dataflow untill NT Service restart).
    // Therefore, we now use another (more optimistic) approach to solve the conflict between data access thread and garbage collection thread.
    // That is, set data inbound transaction's deadlock priority to HIGH, keep data inbound transaction as NORMAL, and garbage collection as LOW,
    // so that when any deadlock happen, garbage collection transaction is the first option to be a victim.

    // 20110113 
    // Hongkong site found the case that 
    // this mutex is not released (after the ReleaseMutex() is called) but the db connection is close,
    // which results in mutex time out when trying to wait for it in order to use (re-open) the connection again.
    // Here reduce the access scope of the class to trace whether something wrong with this mutex,
    // assuming that another thread/module close the connection but still holding the mutex.

    //public class SafeDBConnection
    [Obsolete("Do not use this class anymore, please use OleDbConnection directly.",true)]
    internal class SafeDBConnection
    {
        private static SafeDBConnection _instance;
        public static SafeDBConnection Instance
        {
            [MethodImpl(MethodImplOptions.Synchronized)]
            get
            {
                if (_instance == null)
                {
                    _instance = new SafeDBConnection(Program.ConfigMgt.Config.DataDBConnection);
                }
                return _instance;
            }
        }

        private SafeDBConnection(string dbConnection)
        {
            _connection = new OleDbConnection();
            _connection.ConnectionString = dbConnection;
        }

        private OleDbConnection _connection;
        public OleDbConnection Connection
        {
            get { return _connection; }
        }

        private bool _isOpen = false;
        public bool IsOpen
        {
            get { return _isOpen; }
        }

        public bool Open()
        {
            if (!LockDatabase()) return false;
            _connection.Open();
            return true;
        }
        public void Close()
        {
            try
            {
                ReleaseDatabase();
            }
            catch (Exception err)
            {
                throw err;
            }
            finally
            {
                _connection.Close();
            }
        }

        // As most adapter implementation works in single thread model,
        // this mutex is mainly used for sychorization between working thread and garbage collection thread.
        // : this comment is added in 20110113.

        private Mutex _mutex = new Mutex();

        [MethodImpl(MethodImplOptions.Synchronized)]
        private bool LockDatabase()
        {
            if (_mutex.WaitOne(Program.ConfigMgt.Config.MutexTimeOut, true))
            {
                _isOpen = true;
                Program.Log.Write("Thread (" + Thread.CurrentThread.ManagedThreadId + ") enter the protected area.");
                return true;
            }
            Program.Log.Write(LogType.Error, "Thread (" + Thread.CurrentThread.ManagedThreadId + ") mutex time out! (" + Program.ConfigMgt.Config.MutexTimeOut.ToString() + "ms)");

            // When the mutex time out, the thread in the protected area is most possibly using the database connection.
            // Any thread outside the protected area accesses the database via the same database connection will cause exception.
            // Actually, we can do nothing if the mutex time out except creating a new database connection to access the dirty data. 
            // But a better way is to set a longer time out.

            try
            {
                // 20110113 
                // Hongkong site found the case that 
                // this mutex is not released (after the ReleaseMutex() is called) but the db connection is close,
                // which results in mutex time out when trying to wait for it in order to use (re-open) the connection again.
                // Here add a log message to trace whether something wrong with this mutex,
                // assuming that there is system error in managing the mutex.

                Program.Log.Write(LogType.Info, string.Format(
                    "Mutext information: safed isvalid: {0}, safed isclosed: {1}.",
                    _mutex.SafeWaitHandle.IsInvalid, _mutex.SafeWaitHandle.IsClosed));

                Program.Log.Write(LogType.Info, string.Format(
                    "Mutext information: unsafed handle: {0}.",
                    _mutex.SafeWaitHandle.DangerousGetHandle().ToInt64()));
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void ReleaseDatabase()
        {
            if (!_isOpen)
            {
                // 20110113 
                // Hongkong site found the case that 
                // this mutex is not released (after the ReleaseMutex() is called) but the db connection is close,
                // which results in mutex time out when trying to wait for it in order to use (re-open) the connection again.
                // Here add a log message to trace whether something wrong with this mutex,
                // assuming that another thread/module close the connection but still holding the mutex.

                Program.Log.Write(LogType.Warning, "The safe database connection is already closed. Thread ID: " + Thread.CurrentThread.ManagedThreadId);
                return;
            }

            _mutex.ReleaseMutex();
            Program.Log.Write("Thread (" + Thread.CurrentThread.ManagedThreadId + ") leave the protected area.");
            _isOpen = false;
        }
    }
}
