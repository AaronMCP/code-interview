using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using HYS.Common.Objects.Logging;

namespace HYS.Common.Objects.License
{
    [Obsolete("Please use classes in HYS.Common.Objects.License2 namespace instead.", true)]
    public class DeviceLicenseListControler
    {
        private Label _lblDate;
        private ListView _lstLicense;

        public DeviceLicenseListControler(ListView lstLicense, Label lblDate)
        {
            _lblDate = lblDate;
            _lstLicense = lstLicense;
        }

        public void RefreshList(GWLicense license)
        {
            if (license == null) return;
            this._lblDate.Text = "Date Modified: " + license.Header.CreateDate.ToShortDateString();

            int index = 1;
            this._lstLicense.Items.Clear();
            foreach (DeviceLicense lic in license.Devices)
            {
                DeviceLicenseLevel level = license.FindLicenseLevel(lic.LevelID);
                string maxInterface = (level == null) ? "" : level.GetMaxInstanceCount();
                string maxDay = (level == null) ? "" : level.GetMaxDayCount();

                ListViewItem item = new ListViewItem((index++).ToString());
                item.SubItems.Add(lic.Name.ToString());
                item.SubItems.Add(lic.Type.ToString());
                item.SubItems.Add(lic.Direction.ToString());
                item.SubItems.Add(maxInterface);
                item.SubItems.Add(maxDay);
                item.SubItems.Add("Ox" + lic.FunctionControl.ToString("X"));
                item.Tag = lic;
                this._lstLicense.Items.Add(item);
            }
        }

        public DeviceLicense SelectedItem
        {
            get
            {
                if (this._lstLicense.SelectedItems.Count < 1) return null;
                return this._lstLicense.SelectedItems[0].Tag as DeviceLicense;
            }
        }

        public GWLicense Default()
        {
            GWLicense license = GWLicenseHelper.GetDefaultLicense();
            GWLicenseHelper.SetTimeStamp(license);
            RefreshList(license);
            return license;
        }

        public GWLicense Reset()
        {
            return Reset(null);
        }

        public GWLicense Reset(ILogging log)
        {
            Form frm = this._lstLicense.FindForm();
            GWLicenseAgent a = new GWLicenseAgent();
            GWLicense license = a.LoginGetLicenseLogout(frm, log);
            RefreshList(license);
            return license;
        }
    }
}
