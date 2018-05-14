using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using HYS.IM.Common.Logging;

namespace HYS.IM.IPMonitor.Policies
{
    public abstract class PolicyControlBase
    {
        protected PolicyConfigBase _config;
        protected object _entry;
        protected string _policyName;

        private Timer _timer;
        private bool _running;

        public PolicyControlBase(PolicyConfigBase cfg, object entry, string policyName)
        {
            _config = cfg;
            _entry = entry;
            _policyName = policyName;

            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }

        public void Start()
        {
            if (_running) return;

            if (!_config.Enable)
            {
                Program.Log.Write(LogType.Warning, _policyName + " Timer is Disable.");
                return;
            }

            _running = true;
            _timer.Interval = _config.IntervalInMS;
            _timer.Start();

            Program.Log.Write("Start Execute " + _policyName + " Timer (Interval:" + _config.IntervalInMS + "ms).");
        }
        public void Stop()
        {
            if (!_running) return;

            _running = false;
            _timer.Stop();

            Program.Log.Write("Stop Execute " + _policyName + " Timer.");
        }
        
        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                _timer.Stop();

                Program.Log.Write("");
                Program.Log.Write("--- Begin Execute " + _policyName + " ---");

                ExecutePolicy();
            }
            catch (Exception err)
            {
                Program.Log.Write(err);
            }
            finally
            {
                Program.Log.Write("--- End Execute " + _policyName + " ---");
                if(_running) _timer.Start();
            }
        }
        protected abstract void ExecutePolicy();
    }
}
