using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Diagnostics;
using HYS.IM.Common.Logging;
using HYS.IM.Messaging.Base.Config;
using HYS.IM.Messaging.Management;
using HYS.IM.Messaging.Management.Config;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace HYS.IM.Messaging.Management.Scripts
{
    public class ScriptMgt
    {
        public static string ExecuteBatFile(string file, string workpath, ILog log)
        {
            string str = null;

            // because it is need to read cmd from file
            if (!File.Exists(file))
            {
                str = "File " + file + " does not exist.";
                if (log != null) log.Write(str);
                return str;
            }

            if (!Directory.Exists(workpath))
            {
                str = "Path " + workpath + " does not exist.";
                if (log != null) log.Write(str);
                return str;
            }

            StreamReader sOut = null;
            StreamWriter sIn = null;

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardError = true;
                psi.WorkingDirectory = workpath;

                using (Process proc = Process.Start(psi))
                {
                    sOut = proc.StandardOutput;
                    sIn = proc.StandardInput;

                    str = string.Format("# {0} begin to run.", file);
                    sIn.WriteLine(str);

                    //sIn.WriteLine(file);
                    using (StreamReader sr = File.OpenText(file))
                    {
                        while (!sr.EndOfStream)
                        {
                            str = sr.ReadLine();
                            sIn.WriteLine(str);
                        }
                    }

                    str = string.Format("# {0} run successfully. Exiting.", file);
                    sIn.WriteLine(str);

                    sIn.WriteLine("exit");
                }

                str = sOut.ReadToEnd().Trim();
                if (log != null) log.Write(str);
                return str;
            }
            catch (Exception err)
            {
                if (log != null) log.Write(err);
                return err.Message;
            }
            finally
            {
                if (sOut != null) sOut.Close();
                if (sIn != null) sIn.Close();
            }
        }

        public static string ExecuteBatFile(string cmd, string argument, string workpath, ILog log)
        {
            string str = null;

            if (!Directory.Exists(workpath))
            {
                str = "Path " + workpath + " does not exist.";
                if (log != null) log.Write(str);
                return str;
            }

            StreamReader sOut = null;
            StreamWriter sIn = null;

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardError = true;
                psi.WorkingDirectory = workpath;
                psi.CreateNoWindow = true;

                using (Process proc = Process.Start(psi))
                {
                    sOut = proc.StandardOutput;
                    sIn = proc.StandardInput;

                    str = string.Format("# begin to run.");
                    sIn.WriteLine(str);

                    str = string.Format("{0} {1}", cmd, argument);
                    sIn.WriteLine(str);

                    str = string.Format("# run successfully. Exiting.");
                    sIn.WriteLine(str);

                    sIn.WriteLine("exit");
                }

                str = sOut.ReadToEnd().Trim();
                if (log != null) log.Write(str);
                return str;
            }
            catch (Exception err)
            {
                if (log != null) log.Write(err);
                return err.Message;
            }
            finally
            {
                if (sOut != null) sOut.Close();
                if (sIn != null) sIn.Close();
            }
        }

        public static string ExecuteBatFileRemote(string mgtSvcConfigFileName, string cmd, string argument, string workpath, ILog log)
        {
            string str = "";

            ConfigManager<ServiceConfig> mgt = new ConfigManager<ServiceConfig>(mgtSvcConfigFileName);
            if (!mgt.Load())
            {
                str = "Load file failed: " + mgt.FileName;
                if (log != null)
                {
                    log.Write(LogType.Error, str);
                    log.Write(mgt.LastError);
                }
                return str;
            }

            try
            {
                bool hasChannel = false;
                foreach (IChannel chn in ChannelServices.RegisteredChannels)
                {
                    if (chn is HttpChannel)
                    {
                        hasChannel = true;
                        break;
                    }
                }

                if (!hasChannel)
                {
                    HttpChannel chn = new HttpChannel();
                    ChannelServices.RegisterChannel(chn, false);
                }

                IManagementService helper = Activator.GetObject(typeof(IManagementService), mgt.Config.RemotingUrl) as IManagementService;
                if (helper == null)
                {
                    str = "Create remote object failed: " + mgt.Config.RemotingUrl;
                    if (log != null) log.Write(LogType.Error, str);
                    return str;
                }

                ScriptTask t = new ScriptTask();
                t.File = cmd;
                t.Argument = argument;
                t.WorkPath = workpath;

                ScriptTaskResult r = helper.Execute(t);
                if (r == null)
                {
                    str = "Remote object return null: " + mgt.Config.RemotingUrl;
                    if (log != null) log.Write(LogType.Error, str);
                    return str;
                }

                str = string.Format("file={0} argument={1} workPath={2}", cmd, argument, workpath);
                if (r.Success)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Successfully execute ").AppendLine(str);
                    sb.Append(r.Message);

                    str = sb.ToString();
                    if (log != null) log.Write(str);
                    return str;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Execute failed : ").AppendLine(str);
                    sb.Append(r.Message);

                    str = sb.ToString();
                    if (log != null) log.Write(str);
                    return str;
                }
            }
            catch (Exception err)
            {
                str = "Call remting service failed: " + mgt.Config.RemotingUrl;
                log.Write(LogType.Error, str);
                log.Write(err);
                return str;
            }
        }

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static bool ExecuteAssembly(string filename, string arg, bool showForm, ILog log)
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

                //string logPath = Application.StartupPath + "\\Log";
                //if (!Directory.Exists(logPath)) Directory.CreateDirectory(logPath);
                //string logFile = logPath + "\\command.log";
                //File.AppendAllText(logFile, sb.ToString());
                if (log != null) log.Write(sb.ToString());

                if (showForm)
                {
                    dlg.Dispose();
                }

                return true;
            }
            catch (Exception err)
            {
                if (log != null) log.Write(LogType.Error, "Start process failed." + filename + " " + arg);
                if (log != null) log.Write(err);
                return false;
            }
        }
    }
}
