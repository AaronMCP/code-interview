using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using HYS.Common.Objects.Logging;
using HYS.Adapter.Base;

namespace HYS.MonitorTest
{
    public partial class LogConfig : Form
    {
        public LogConfig()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            LogType type = (LogType)Enum.Parse(typeof(LogType), comboBox.Text);
            Logging Log = new Logging(txtServerName.Text, type, (int)numericUpDown.Value);

            Log.WriteAppStart(txtServerName.Text, new string[] { "start" });

            for (int i = 0; i < 10; i++)
            {
                Log.Write(LogType.Debug, "Write Debug log!Thread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.SleepThread.Sleep");
                //Thread.Sleep(100);
            }

            for (int i = 0; i < 10; i++)
            {
                Log.Write(LogType.Info, "Write Info log!", "Adapter");
                //Thread.Sleep(100);
            }

            for (int i = 0; i < 10; i++)
            {
                Log.Write(LogType.Warning, "Write Warning log!");
                //Thread.Sleep(1000);
            }

            for (int i = 0; i < 10; i++)
            {
                Log.Write(LogType.Error, "Write Error log!");
                //Thread.Sleep(1000);
            }

            Log.WriteAppExit(txtServerName.Text);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            //DateTime.Compare(new DateTime(2006, 1, 7), new DateTime(2006, 1, 2));
            //DateTime dt = DateTime.Now;
            DateTime newDT = DateTime.Now;
            MessageBox.Show(newDT.ToString("yyyy-MM-dd hh:mm:ss.fff"));
        }
    }
}