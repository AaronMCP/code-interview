using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Xml;
using HYS.Common.Objects.Logging;
using System.Runtime.CompilerServices;
using System.Threading;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class GWLicenseAgent
    {
        private uint _devID;
        public GWLicenseAgent()
            : this(GWLicenseHelper.SentinelDevID)
        {
        }
        public GWLicenseAgent(uint devID)
        {
            _devID = devID;
        }

        //private DogManageClass dm;
        private GWLicenseResult Login()
        {
            //DogManager dm = DogManager.Instance;
            //uint value = dm.Login(_devID);

            //dm = new DogManageClass();
            //uint value = (uint)dm.GetLicense((int)_devID);
            //if (value == 1000) value = 0;

            //if (value == 226)
            //{
            //    Thread.Sleep(5000);     // 5s
            //    value = (uint)dm.GetLicense((int)_devID);
            //    if (value == 226)
            //    {
            //        Thread.Sleep(10000);    //10s
            //        value = (uint)dm.GetLicense((int)_devID);
            //    }
            //}

            //return new GWLicenseResult(value);
            return new GWLicenseResult(0);
        }
        private GWLicenseResult Logout()
        {
            //DogManager dm = DogManager.Instance;
            //uint value = dm.ReleaseLicense();

            //uint value = (uint)dm.ReleaseLicense();
            //dm = null;
            //return new GWLicenseResult(value);
            return new GWLicenseResult(0);
        }
        private GWLicenseResult ReadLicense(ILogging log)
        {
            //DogManager dm = DogManager.Instance;
            //byte[] bArray = new byte[GWLicenseHelper.BytesLength];
            //uint value = dm.ReadGateWayData(bArray, 0, GWLicenseHelper.BytesLength);

            //int feactureID = dm.GetGateWayFeatureID();
            //int feactureLength = dm.GetGateWayFeatureMaxLength();
            //if (feactureLength != GWLicenseHelper.BytesLength)
            //{
            //    if (log != null) log.Write(LogType.Error, "GateWayFeatureMaxLength is not 128, maybe the version of software dog is not currect.");
            //    return new GWLicenseResult(uint.MaxValue);
            //}

            //byte[] bArray = new byte[GWLicenseHelper.BytesLength];
            //uint value = (uint)dm.ReadRawData(feactureID, 0, GWLicenseHelper.BytesLength, bArray);
            //return new GWLicenseResult(value, bArray);
            return new GWLicenseResult(0, null);
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
                GWLicenseResult res = Login();
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
                    else
                    {
                        if (log != null) log.Write(LogType.Error, "Read GC Gateway license data from dog failed. Error code: " + res.Value.ToString());
                        if (frm != null)
                            MessageBox.Show(frm, "Read GC Gateway license data from dog failed.",
                                "GC Gateway DogManager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    res = Logout();
                    if (res.Success)
                    {
                        if (log != null) log.Write("Logout software dog succeeded.");
                    }
                    else
                    {
                        if (log != null) log.Write(LogType.Error, "Logout software dog failed. Error code: " + res.Value.ToString());
                        if (frm != null)
                            MessageBox.Show(frm, "Logout dog failed.", "GC Gateway DogManager",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (log != null) log.Write(LogType.Error, "Login software dog failed. Error code: " + res.Value.ToString());
                    if (frm != null)
                        MessageBox.Show(frm, "Login dog failed.", "GC Gateway DogManager",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        //    return GWLicenseHelper.GetDefaultLicense();
        //}
    }
}
