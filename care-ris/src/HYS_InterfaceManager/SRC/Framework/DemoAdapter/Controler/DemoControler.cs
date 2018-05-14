using System;
using System.IO;
using System.Timers;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Adapter.Base;

namespace DemoAdapter.Controlers
{
    public class DemoControler
    {
        private System.Timers.Timer _timer;
        
        public DemoControler()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
        }

        public bool IsRunning
        {
            get
            {
                return _timer.Enabled;
            }
        }

        public void Start()
        {
            string fname = Properties.Settings.Default.FileName;
            if (!Path.IsPathRooted(fname)) fname = Application.StartupPath + "\\" + fname;
            Logging.LogFileName = fname;

            _timer.Interval = Properties.Settings.Default.TimerInterval;
            _timer.Start();

            Logging.WriteAppStart("Demo Adapter");
        }

        public void Stop()
        {
            _timer.Stop();

            Logging.WriteAppExit("Demo Adapter");
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Logging.Write(LogType.Debug, "Heart beating...", true);
        }
    }
}
