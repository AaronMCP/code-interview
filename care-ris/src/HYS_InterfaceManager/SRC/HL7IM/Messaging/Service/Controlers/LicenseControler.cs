using System;
using System.Text;
using System.Timers;
using System.ServiceProcess;
using System.Collections.Generic;
using HYS.Common.Objects.Config;
using HYS.Common.Objects.Device;
using HYS.Common.Objects.License2;
using HYS.Common.Objects.Logging;
using HYS.IM.Messaging.Base.Config;

namespace HYS.IM.Messaging.Service.Controlers
{
    public class LicenseControler
    {
        private ServiceBase _service;

        private GWLoggingWapper _log;
        private Timer _timer;
        private const int TimerInterval = 15 * 60 * 1000;    //15 minute
        //private const int TimerInterval = 15 * 1000;  

        private string _name;
        private DeviceType _type;
        private DirectionType _direction;

        public LicenseControler(ServiceBase service)
        {
            _service = service;
            _log = new GWLoggingWapper(Program.Log);

            AttachAdapter();

            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }
        private void AttachAdapter()
        {
            LicenseConfig licenseInfo = Program.License.Config;
            if (licenseInfo == null) return;

            _name = licenseInfo.DeviceName;
            _type = (DeviceType)Enum.Parse(typeof(DeviceType), licenseInfo.Type);
            _direction = (DirectionType)Enum.Parse(typeof(DirectionType), licenseInfo.Direction);

            _log.Write("License Controler initialized " +
                " (DeviceName=" + _name +
                ", DeviceType=" + _type.ToString() +
                ", DeviceDirection=" + _direction.ToString() + ").");
        }

        public DeviceLicense Start()
        {
            DeviceLicense device = ValidateLicense(false);
            if (device == null) return null;

            //DeviceLicense device = new DeviceLicense();

            _timer.Interval = TimerInterval;
            _timer.Start();

            _log.Write("License Controler started.");
            return device;
        }
        public void Stop()
        {
            _timer.Stop();
            _log.Write("License Controler stopped.");
        }

        private DeviceLicense ValidateLicense(bool retry)
        {
            DeviceLicense device = null;
            _log.Write("==== Validate License Begin ====");

            GWLicenseAgent agent = new GWLicenseAgent(retry);
            GWLicense license = agent.LoginGetLicenseLogout(_log);
            if (license == null) goto ExitValidation;

            device = license.FindDevice(_name, _type, _direction);
            if (device == null)
            {
                _log.Write(LogType.Warning, "Cannot find license information for this device.");
                goto ExitValidation;
            }

            if (device.MaxInterfaceCount == 0)
            {
                device = null;
                _log.Write(LogType.Warning, "License of this device was disabled. ");
                goto ExitValidation;
            }

            if (device.IsExpired(license.Header.CreateDate))
            {
                device = null;
                _log.Write(LogType.Warning, "License of this device was expired. License create date: " + license.Header.CreateDate.ToShortDateString());
                goto ExitValidation;
            }

        ExitValidation:

            _log.Write("==== Validate License End ====");

            if (device == null)
            {
                _log.Write(LogType.Warning, "Validate license failed, stop NT service.");
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
