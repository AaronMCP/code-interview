using System;
using System.Runtime.CompilerServices;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using HYS.Common.Objects.Logging;
using HYS.Common.Objects.SentinelDog;
using System.Collections;
using HYS.Common.Objects.LDKDog;

namespace HYS.Common.Objects.License2
{
    public class GWLicenseAgent
    {
        private uint _devID;
        public GWLicenseAgent()
            : this(GWLicenseHelper.SentinelDevID)
        {
        }
        public GWLicenseAgent(bool retry)
            : this(GWLicenseHelper.SentinelDevID)
        {
            _retry = retry;
        }
        public GWLicenseAgent(uint devID)
        {
            _devID = devID;
        }

        private SentinelKey _stlKey;
        private const int FEATUREID_GATEWAY = 4;
        //private const int FEATURE_MAX_LENGTH_GATEWAY = 128;

        //private DogManageClass dm;
        private GWLicenseResult Login(ILogging log)
        {          
            return new GWLicenseResult();
        }
       /* private GWLicenseResult Logout(ILogging log)
        {
            //DogManager dm = DogManager.Instance;
            //uint value = dm.ReleaseLicense();

            //uint value = (uint)dm.ReleaseLicense();
            uint value = _stlKey.SFNTReleaseLicense();
            //dm = null;
            _stlKey = null;
            return new GWLicenseResult(value);
        }*/
        private GWLicenseResult ReadLicense(ILogging log)
        {
            return new GWLicenseResult();
        }
        
        public GWLicense LoginGetLicenseLogout()
        {
            return LoginGetLicenseLogout(null, null);
        }
        public GWLicense LoginGetLicenseLogout(Form frm)
        {
            return LoginGetLicenseLogout(frm, null);
        }
        public GWLicense LoginGetLicenseLogout(ILogging log)
        {
            return LoginGetLicenseLogout(null, log);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public GWLicense LoginGetLicenseLogout(Form frm, ILogging log)
        {
            //if (!EnsureNetbiosServiceRunning(log)) return null;

            if (frm != null) return _LoginGetLicenseLogout(frm, log);

            _log = log;
            _license = null;
            _event = new ManualResetEvent(false);

            Thread thr = new Thread(new ThreadStart(DoLoginGetLicenseLogout));
            thr.SetApartmentState(ApartmentState.STA);
            thr.Start();

            //bool res = _event.WaitOne(10 * 1000, true);  //10 s 
            bool res = _event.WaitOne(3600 * 1000, true);  //60 min

            if (res)
            {
                return _license;
            }
            else
            {
                if (log != null) log.Write(LogType.Warning, "Get license timeout.");
                return null;
            }
        }

        private static int TimeOut = 30000;
        public static string NetbiosServiceName = "LmHosts";
        public static bool EnsureNetbiosServiceRunning(ILogging log)
        {
            try
            {
                ServiceController sc = new ServiceController(NetbiosServiceName);
                if (sc.Status == ServiceControllerStatus.Running) return true;

                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 0, 0, TimeOut));
                return true;
            }
            catch (Exception err)
            {
                if (log != null) log.Write(LogType.Error, err.ToString());
                return false;
            }
        }

        private bool _retry;
        private ILogging _log;
        private GWLicense _license;
        private ManualResetEvent _event;
        private void DoLoginGetLicenseLogout()
        {
            _license = _LoginGetLicenseLogout(null, _log);
            _event.Set();
        }
        private GWLicense _LoginGetLicenseLogout(Form frm, ILogging log)
        {
            try
            {
                GWLicense license = null;
                if (log != null) log.Write("-- Read License Begin --");

                if (frm != null) frm.Cursor = Cursors.WaitCursor;
                GWLicenseResult res = Login(log);
                if (frm != null) frm.Cursor = Cursors.Default;

                if (res.Success)
                {
                    if (log != null) log.Write("Login software dog succeeded.");

                    res = ReadLicense(log);
                    if (res.Success)
                    {
                        if (log != null) log.Write("Read GC Gateway license data from dog succeeded.");
                        license = res.License;
                    }

                }

                if (log != null) log.Write("-- Read License End --");
                return license;
            }
            catch (Exception err)
            {
                if (log != null)
                {
                    log.Write("-- Read License Error --");
                    log.Write(LogType.Error, err.ToString());
                }
                return null;
            }
        }

        //public GWLicense LoginGetLicenseLogout(Form frm, ILogging log)
        //{
        //    if (log != null) log.Write(LogType.Warning, "Get Default License. (for internal testing only)");
        //    if (frm != null) MessageBox.Show(frm, "Get Default License. (for internal testing only)");
        //    return GWLicenseHelper.GetFullLicense();
        //}
    }
}
