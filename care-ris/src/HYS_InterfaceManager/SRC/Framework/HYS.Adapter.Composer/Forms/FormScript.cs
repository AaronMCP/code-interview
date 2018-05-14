using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Runtime.CompilerServices;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;

namespace HYS.Adapter.Composer.Forms
{
    public partial class FormScript : Form
    {
        public FormScript()
        {
            InitializeComponent();

            _dirMgt = new DeviceDirManager();
            string filename = Program.ConfigMgt.Config.DeviceDirFileName;
            _dirMgt.FileName = ConfigHelper.GetFullPath(filename);
        }

        private DeviceDirManager _dirMgt;

        private void FormScript_Load(object sender, EventArgs e)
        {
            if (!_dirMgt.LoadDeviceDir())
            {
                this.labelError.Visible = true;
                this.comboBoxType.Enabled = false;
            }
        }

        private void buttonIMParam_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Text = "IM Paramters";

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.ScrollBars = ScrollBars.Both;
            tb.WordWrap = false;
            tb.Multiline = true;

            foreach (string str in IMParameter.List)
            {
                tb.Text += str + "\r\n";
            }

            tb.Select(0, 0);
            frm.Controls.Add(tb);
            frm.ShowDialog(this);
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strType = this.comboBoxType.Text;
            if (strType == null || strType.Length < 1) return;

            DeviceFileType type = (DeviceFileType)Enum.Parse(typeof(DeviceFileType), this.comboBoxType.Text);
            DeviceFile file = _dirMgt.DeviceDirInfor.Files.FindFirstFile(type);

            string filename = null;
            if (file != null)
            {
                filename = ConfigHelper.GetFullPath(file.Location);
                if (!File.Exists(filename)) filename = null;
            }

            if (filename == null)
            {
                if (MessageBox.Show(this, "Cannot find " + type.ToString() + ". Do you want to create one?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string fn = type.ToString() + ".bat";
                    string fname = ConfigHelper.GetFullPath(fn);
                    using (StreamWriter sw = File.CreateText(fname))
                    {
                        sw.WriteLine("REM " + type.ToString());
                    }

                    if (file == null)
                    {
                        file = new DeviceFile();
                        file.Backupable = true;
                        file.Location = fn;
                        file.Type = type;

                        _dirMgt.DeviceDirInfor.Files.Add(file);
                        if (!_dirMgt.SaveDeviceDir())
                        {
                            MessageBox.Show("Save DeviceDir file failed.");
                            return;
                        }
                    }
                }
            }

            this.textBoxScript.Tag = file;
            buttonLoad_Click(sender, e);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            DeviceFile file = this.textBoxScript.Tag as DeviceFile;
            if (file == null) return;

            string filename = ConfigHelper.GetFullPath(file.Location);
            using (StreamReader sr = File.OpenText(filename))
            {
                string str = sr.ReadToEnd();
                this.textBoxScript.Text = str;
            }

            this.textBoxLocation.Text = filename;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DeviceFile file = this.textBoxScript.Tag as DeviceFile;
            if (file == null) return;

            string filename = ConfigHelper.GetFullPath(file.Location);
            using (StreamWriter sw = File.CreateText(filename))
            {
                string str = this.textBoxScript.Text;
                sw.Write(str);
            }
        }

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            DeviceFile file = this.textBoxScript.Tag as DeviceFile;
            if (file == null) return;

            buttonSave_Click(sender, e);

            string filename = ConfigHelper.GetFullPath(file.Location);

            ProcessControl pc = new ProcessControl();
            bool res = pc.Execute(filename, "");
            MessageBox.Show(res.ToString() + " " + pc.ExitCode);

            //ProcessStartInfo pi = new ProcessStartInfo();
            //pi.FileName = filename;
            //pi.CreateNoWindow = false;
            //pi.Arguments = " >> " + filename + ".log";

            //Process p = Process.Start(pi);
            //if (p != null)
            //{
            //    p.Exited += new EventHandler(process_Exited);
            //    p.EnableRaisingEvents = true;
            //}
            //else
            //{
            //    MessageBox.Show("Execute script failed.");
            //}
        }

        private void process_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;
            MessageBox.Show("Process exit code: " + p.ExitCode.ToString());
        }

        private void buttonGetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController sc = new ServiceController(this.textBoxServiceName.Text);
                MessageBox.Show(sc.Status.ToString());
            }
            catch (Exception err)
            {
                MessageBox.Show("Get service status failed. \r\n" + err.ToString());
            }
        }

        private void buttonServices_Click(object sender, EventArgs e)
        {
            Form frm = new Form();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Text = "Service List";

            TextBox tb = new TextBox();
            tb.Dock = DockStyle.Fill;
            tb.ScrollBars = ScrollBars.Both;
            tb.WordWrap = false;
            tb.Multiline = true;

            ServiceController[] sclist = ServiceController.GetServices();
            foreach (ServiceController sc in sclist)
            {
                tb.Text += sc.Status.ToString() + "\t" + sc.ServiceName + "\r\n";
            }

            tb.Select(0, 0);
            frm.Controls.Add(tb);
            frm.ShowDialog(this);
        }
    }

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

        private int _timeOut = 1000;
        public int TimeOut
        {
            get { return _timeOut; }
            set { _timeOut = value; }
        }

        private System.Threading.ManualResetEvent waitEvent;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public bool Execute(string filename, string arg)
        {
            if (filename == null || filename.Length < 1)
            {
                //GCError.SetLastError("Invalid arugment(s).");
                return false;
            }

            //try
            //{
                ProcessStartInfo pi = new ProcessStartInfo();
                pi.FileName = filename;
                pi.CreateNoWindow = false;
                pi.Arguments = " >> " + filename + ".log";

                waitEvent.Reset();

                Process p = Process.Start(pi);
                if (p != null)
                {
                    p.Exited += new EventHandler(process_Exited);
                    p.EnableRaisingEvents = true;
                }
                else
                {
                    //GCError.SetLastError("Cannot start process." + filename);
                    return false;
                }

                bool res = waitEvent.WaitOne(TimeOut, true);
                if (!res)
                {
                    //GCError.SetLastError("Execure script time out." + filename);
                }

                return res;
            //}
            //catch (Exception err)
            //{
            //    //GCError.SetLastError("Start process failed." + filename);
            //    //GCError.SetLastError(err);
            //    return false;
            //}
        }

        private void process_Exited(object sender, EventArgs e)
        {
            Process p = sender as Process;
            if (p != null) _exitCode = p.ExitCode;

            waitEvent.Set();
        }
    }
}