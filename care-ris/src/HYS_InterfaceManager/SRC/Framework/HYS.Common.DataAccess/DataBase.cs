using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using Oracle.DataAccess.Client;

namespace HYS.Common.DataAccess
{
    public class DataBase
    {
        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        public DataBase(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool TestDBConnection()
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(ConnectionString))
                {
                    conn.Open();
                    conn.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                NotifyException("",e);
                return false;
            }
        }


        /// <summary>
        /// Connection Oracle testing by Oracle Driver
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool TestDBConnection2()
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(_connectionString))
                {
                    conn.Open();
                    conn.Close();
                }

                return true;
            }
            catch (Exception e)
            {
                NotifyException("", e);
                return false;
            }
        }

        private Exception _lastError = null;
        private void NotifyException(string sql, Exception err)
        {
            _lastError = err;
            if (OnError != null) OnError(ConnectionString, sql, err);
        }
        public event DataAccessExceptionHanlder OnError;
        public Exception LastError
        {
            get { return _lastError; }
        }

        private OleDbConnection _dbConnection = null;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CloseDBConnection()
        {
            if (_dbConnection != null) _dbConnection.Close();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public OleDbDataReader DoQuery(string sql)
        {
            try
            {
                if (sql == null || sql.Length < 1) return null;

                OleDbConnection conn = new OleDbConnection(ConnectionString);
                conn.Open();

                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader dbData = cmd.ExecuteReader();

                _dbConnection = conn;
                return dbData;
            }
            catch (Exception e)
            {
                NotifyException(sql, e);
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DataSet ExecuteQuery(string sql)
        {
            try
            {
                if (sql == null || sql.Length < 1) return null;

                _dbConnection = new OleDbConnection(ConnectionString);
                OleDbDataAdapter ad = new OleDbDataAdapter(sql, _dbConnection);

                DataSet ds = new DataSet();
                ad.Fill(ds);

                return ds;
            }
            catch (Exception e)
            {
                //throw e;
                NotifyException(sql, e);
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public DataView GetDataView(string tableName)
        {
            if (tableName == null || tableName.Length < 1) return null;
            string sql = "SELECT * FROM " + tableName;

            try
            {
                _dbConnection = new OleDbConnection(ConnectionString);
                OleDbDataAdapter ad = new OleDbDataAdapter(sql, _dbConnection);

                DataSet ds = new DataSet();
                ad.Fill(ds, tableName);
                //ds.WriteXml("dataset_demo.xml",XmlWriteMode.IgnoreSchema);

                return ds.Tables[tableName].DefaultView;
            }
            catch (Exception e)
            {
                //throw e;
                NotifyException(sql, e);
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public string[] GetTableNames()
        {
            try
            {
                List<string> list = new List<string>();

                OleDbConnection conn = new OleDbConnection(ConnectionString);
                conn.Open();

                string[] restrictions = new string[4];
                restrictions[3] = "TABLE";

                DataTable table = conn.GetSchema("Tables", restrictions);

                if (table != null)
                {
                    foreach (DataRow r in table.Rows)
                    {
                        string str = r["TABLE_NAME"] as string;
                        if (str != null) list.Add(str);
                    }
                }

                _dbConnection = conn;
                return list.ToArray();
                //return table;
            }
            catch (Exception e)
            {
                NotifyException("GetSchema(\"Tables\")", e);
                return null;
            }
        }

        public static int OSQLTimeOut = 20000;
        private static ManualResetEvent waitEvent = new ManualResetEvent(false);

        public static string OSQLFileName = "C:\\Program Files\\Microsoft SQL Server\\90\\Tools\\Binn\\osql.exe";
        public static string OSQLArgument = "-S (local) -E";
        public static string OSQLDatabase = "master";
        public static bool OSQLExec(string sqlFileName)
        {
            return OSQLExec(OSQLFileName, OSQLDatabase, sqlFileName);
        }
        public static bool OSQLExec(string dbName, string sqlFileName)
        {
            return OSQLExec(OSQLFileName, dbName, sqlFileName);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool OSQLExec(string osqlFileName, string dbName, string sqlFileName)
        {
            if (!File.Exists(osqlFileName) ||
                !File.Exists(sqlFileName)) return false;

            string db = dbName;
            if (db == null || db.Length < 1) db = OSQLDatabase;

            string cmd = "\"" + osqlFileName + "\"";
            string arg = OSQLArgument + " -d " + db + " -i \"" + sqlFileName + "\"";

            Form dlg = new Form();
            dlg.Text = "Executing SQL Script...";
            dlg.Size = new Size(500, 500);
            dlg.ControlBox = false;
            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.BackColor = Color.Black;
            tb.ForeColor = Color.White;
            tb.ScrollBars = ScrollBars.Vertical;
            tb.WordWrap = true;
            tb.Multiline = true;
            dlg.Controls.Add(tb);
            //dlg.Owner = this;
            dlg.TopMost = true;
            dlg.Cursor = Cursors.WaitCursor;
            dlg.Show();

            tb.AppendText("---------------- Invoking Begin " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "----------------\r\n");
            Application.DoEvents();

            ProcessStartInfo psi = new ProcessStartInfo(cmd, arg);
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            tb.AppendText("Command: " + cmd + "\r\n");
            tb.AppendText("Argument: " + arg + "\r\n");

            //waitEvent.Reset();
            Process proc = Process.Start(psi);
            //proc.WaitForExit();

            //if the output is too much may cause a dead lock,
            //therefore move WaitForExit() after ReadLine()

            string server = null;
            while (proc.StandardOutput.Peek() > -1)
            {
                server = proc.StandardOutput.ReadLine().Trim();

                tb.AppendText(server + "\r\n");
                tb.SelectionStart = tb.Text.Length - 1;
                tb.ScrollToCaret();
                Application.DoEvents();
                Thread.Sleep(50);
            }

            proc.WaitForExit();

            tb.AppendText("---------------- Invoking End " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "----------------\r\n\r\n");
            tb.SelectionStart = tb.Text.Length - 1;
            tb.ScrollToCaret();
            Application.DoEvents();
            Thread.Sleep(1000);

            //string logPath = Path.GetDirectoryName(sqlFileName) + "\\..\\..\\Log";  // to IM log
            string logPath = Application.StartupPath + "\\Log";
            if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
            string logFile = logPath + "\\osql.log";
            File.AppendAllText(logFile, tb.Text);

            dlg.Dispose();

            return true;
        }
        //public static bool OSQLExec(string osqlFileName, string dbName, string sqlFileName)
        //{
        //    if (!File.Exists(osqlFileName) ||
        //        !File.Exists(sqlFileName)) return false;

        //    string db = dbName;
        //    if (db == null || db.Length < 1) db = OSQLDatabase;

        //    string cmd = "\"" + osqlFileName + "\"";
        //    string arg = OSQLArgument + " -d " + db + " -i \"" + sqlFileName + "\"";

        //    ProcessStartInfo psi = new ProcessStartInfo();
        //    psi.Arguments = arg;
        //    psi.FileName = cmd;

        //    waitEvent.Reset();

        //    Process proc = Process.Start(psi);
        //    if (proc != null)
        //    {
        //        proc.Exited += new EventHandler(proc_Exited);
        //        proc.EnableRaisingEvents = true;
        //    }

        //    bool res = waitEvent.WaitOne(OSQLTimeOut, true);
        //    if (!res)
        //    {
        //        return false;
        //    }

        //    return true;
        //}


        static void proc_Exited(object sender, EventArgs e)
        {
            waitEvent.Set();
        }
    }
}
