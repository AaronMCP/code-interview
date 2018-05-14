using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Text;

namespace HYS.IM.BusinessControl.SystemControl
{
    public class ProcessControl
    {
        public ProcessControl()
        {
            waitEvent = new ManualResetEvent(false);
        }

        private int _exitCode = -999999;
        public int ExitCode
        {
            get
            {
                return _exitCode;
            }
        }

        private int _timeOut = 20000;
        public int TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }

        private System.Threading.ManualResetEvent waitEvent;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool ExecuteAssembly(string filename, string arg, bool showForm)
        {
            try
            {
                Form dlg = null;
                TextBox tb = null;
                StringBuilder sb = new StringBuilder();

                string str1 = "---------------- Invoking Begin " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "----------------\r\n";
                string str2 = "Command: " + filename + "\r\n";
                string str3 = "Argument: " + arg + "\r\n";
                sb.Append(str1).Append(str2).Append(str3);

                if (showForm)
                {
                    dlg = new Form();
                    dlg.Text = "Executing Command...";
                    dlg.Size = new Size(500, 500);
                    dlg.ControlBox = false;
                    tb = new TextBox();
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

                    tb.Text += str1 + str2 + str3;
                    Application.DoEvents();
                }

                ProcessStartInfo psi = new ProcessStartInfo(filename, arg);
                psi.WorkingDirectory = Path.GetDirectoryName(filename);
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;

                //waitEvent.Reset();
                Process proc = Process.Start(psi);
                proc.WaitForExit();

                //if the output is too much may cause a dead lock, see HYS.Common.DataAccess.DataBase.OSQLExec() for solution,
                //that is to move WaitForExit() after ReadLine(), but then we cannot get the complete output message.

                while (proc.StandardOutput.Peek() > -1)
                {
                    string msg = proc.StandardOutput.ReadLine().Trim();
                    sb.AppendLine(msg);

                    if (showForm)
                    {
                        tb.Text += msg + "\r\n";
                        tb.SelectionStart = tb.Text.Length - 1;
                        tb.ScrollToCaret();
                        Application.DoEvents();
                        Thread.Sleep(50);
                    }
                }

                string str4 = "---------------- Invoking End " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "----------------\r\n\r\n";
                sb.Append(str4);
                
                if (showForm)
                {
                    tb.Text += str4;
                    tb.SelectionStart = tb.Text.Length - 1;
                    tb.ScrollToCaret();
                    Application.DoEvents();
                    Thread.Sleep(1000);
                }

                //string logPath = Path.GetDirectoryName(filename) + "\\..\\..\\Log";     //to IM log
                string logPath = Application.StartupPath + "\\Log";
                if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
                string logFile = logPath + "\\command.log";
                File.AppendAllText(logFile, sb.ToString());

                if (showForm)
                {
                    dlg.Dispose();
                }

                return true;
            }
            catch (Exception err)
            {
                GCError.SetLastError("Start process failed." + filename + " " + arg);
                GCError.SetLastError(err);
                return false;
            }
        }

        public bool ExecuteAssembly(string filename, string arg)
        {
            return ExecuteAssembly(filename, arg, false);
        }

        public static bool ExecuteAssemblyDirectly(string filename, string arg)
        {
            try
            {
                _process = null;

                ProcessStartInfo pi = new ProcessStartInfo();
                pi.WorkingDirectory = Path.GetDirectoryName(filename);
                pi.FileName = filename;
                pi.Arguments = arg;

                //Process proc = Process.Start(filename,arg);
                Process proc = Process.Start(pi);
                if (proc != null)
                {
                    proc.EnableRaisingEvents = false;
                    _process = proc;
                    return true;
                }
                else
                {
                    GCError.SetLastError("Cannot start process." + filename + " " + arg);
                    return false;
                }
            }
            catch (Exception err)
            {
                GCError.SetLastError("Start process failed." + filename + " " + arg);
                GCError.SetLastError(err);
                return false;
            }
        }

        private void process_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;
            if (p != null) _exitCode = p.ExitCode;

            waitEvent.Set();
        }

        public static Process _process;
    }
}