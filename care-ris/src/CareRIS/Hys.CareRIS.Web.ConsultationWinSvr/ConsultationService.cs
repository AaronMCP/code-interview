using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Csh.Hcis.GC.RisPro.Web.ConsultationWinSvr
{
    public partial class ConsultationService : ServiceBase
    {
        private static readonly ILog _Logger = LogManager.GetLogger("App");
        private System.Timers.Timer _UpdateProgressTimer;
        private string _WebHost = "";
        private string _WebApiHost = "";
        private int _UpdateProgressInterval = 5;

        public ConsultationService()
        {
            InitializeComponent();

            System.Configuration.AppSettingsReader appConfig = new System.Configuration.AppSettingsReader();
            _WebHost = (string)(appConfig.GetValue("WebHost", typeof(string)));
            _WebApiHost = (string)(appConfig.GetValue("WebApiHost", typeof(string)));
            _UpdateProgressInterval = (int)(appConfig.GetValue("UpdateProgressInterval", typeof(int)));

            _UpdateProgressTimer = new System.Timers.Timer();
            _UpdateProgressTimer.Enabled = false;
            _UpdateProgressTimer.Interval = _UpdateProgressInterval * 1000;
            _UpdateProgressTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateProgressTimer_Elapsed);
        }

        private void UpdateProgressTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _UpdateProgressTimer.Enabled = false;
            try
            {
                _Logger.Debug("UpdateProgressTimer start...");
                UpdateProgressService.UpdateProgressProcess(_WebHost, _WebApiHost);
            }
            catch(Exception ex)
            {
                _Logger.Error("UpdateProgressTimer error:" + ex.ToString());
            }
            _UpdateProgressTimer.Enabled = true;
        }

        protected override void OnStart(string[] args)
        {
            _UpdateProgressTimer.Enabled = true;
        }

        protected override void OnStop()
        {
            _UpdateProgressTimer.Enabled = false;
        }
    }
}
