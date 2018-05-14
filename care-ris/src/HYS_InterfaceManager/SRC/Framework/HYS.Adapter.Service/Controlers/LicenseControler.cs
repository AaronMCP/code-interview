using System;
using System.Text;
using System.Timers;
using System.ServiceProcess;
using System.Collections.Generic;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.License2;
using HYS.Common.Objects.Logging;

namespace HYS.Adapter.Service.Controlers
{
    public class LicenseControler
    {
        private ServiceBase _service;
        
        private Timer _timer;
        private const int TimerInterval = 15 * 60 * 1000;    //15 minute
        //private const int TimerInterval = 15 * 1000;  

        private string _name;
        private DeviceType _type;
        private DirectionType _direction;

        public LicenseControler(ServiceBase service)
        {
            _service = service;

            AttachAdapter();

            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }
        private void AttachAdapter()
        {
            DeviceDir dir = Program.DeviceMgt.DeviceDirInfor;
            if (dir == null || dir.Header == null) return;

            _name = dir.Header.RefDeviceName;
            _type = dir.Header.Type;
            _direction = dir.Header.Direction;

            Program.Log.Write("License Controler initialized " +
                " (DeviceName=" + _name +
                ", DeviceType=" + _type.ToString() +
                ", DeviceDirection=" + _direction.ToString() + ").");
        }

        public DeviceLicense Start(string[] args)
        {
            bool retry =
             (args == null ||
                args.Length < 1 ||
                args[0] != AdapterConfigArgument.InIM);

            //DeviceLicense device = ValidateLicense(retry);
            DeviceLicense device = new DeviceLicense();
            if (device == null) return null;

            //DeviceLicense device = new DeviceLicense();

            _timer.Interval = TimerInterval;
            _timer.Start();

            Program.Log.Write("License Controler started.");
            return device;
        }
        public void Stop()
        {
            _timer.Stop();
            Program.Log.Write("License Controler stopped.");
        }

        private DeviceLicense ValidateLicense(bool retry)
        {
            DeviceLicense device = null;
            Program.Log.Write("==== Validate License Begin ====");

            if (_name == GWLicenseHelper.LightMWLOutDeviceName)
            {
                device = GWLicenseHelper.LightMWLOutDeviceLicense;
                Program.Log.Write("Find Light MWL Outbound Interface License.");
            }
            else
            {
                GWLicenseAgent agent = new GWLicenseAgent(retry);
                GWLicense license = agent.LoginGetLicenseLogout(Program.Log);
                if (license == null) goto ExitValidation;

                device = license.FindDevice(_name, _type, _direction);
                if (device == null)
                {
                    Program.Log.Write(LogType.Warning, "Cannot find license information for this device.");
                    goto ExitValidation;
                }

                if (device.MaxInterfaceCount == 0)
                {
                    device = null;
                    Program.Log.Write(LogType.Warning, "License of this device was disabled. ");
                    goto ExitValidation;
                }

                if (device.IsExpired(license.Header.CreateDate))
                {
                    device = null;
                    Program.Log.Write(LogType.Warning, "License of this device was expired. License create date: " + license.Header.CreateDate.ToShortDateString());
                    goto ExitValidation;
                }
            }

            ExitValidation:

            Program.Log.Write("==== Validate License End ====");

            if (device == null)
            {
                Program.Log.Write(LogType.Warning, "Validate license failed, stop NT service.");
                _service.Stop();
            }

            return device;
        }

        void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            ValidateLicense(false);
            _timer.Enabled = true;
        }
    }
}
