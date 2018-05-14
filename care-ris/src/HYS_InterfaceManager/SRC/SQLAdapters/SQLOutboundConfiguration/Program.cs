using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.SQLOutboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Device;
using HYS.SQLOutboundAdapterConfiguration.Forms;

namespace HYS.SQLOutboundAdapterConfiguration
{
    static class Program
    {
        public static string GWDataDBConnection;

        public static DeviceDirManager DeviceMgt;
        //public static SQLOutboundConfiguration form;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PreLoading();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SQLOutboundConfiguration());
        }

        internal static void PreLoading()
        {
            //Load DeviceDir
            DeviceMgt = new DeviceDirManager();
            DeviceMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
            if (!DeviceMgt.LoadDeviceDir())
            {
                MessageBox.Show("Cannot load DeviceDir file.");
            }

            //Load XML file
            string FileName = Application.StartupPath + "\\" + SQLOutAdapterConfigMgt._FileName;
            if (!SQLOutAdapterConfigMgt.Load(FileName))
            {
                if (SQLOutAdapterConfigMgt.LastException != null)
                {
                    if (MessageBox.Show("Cannot load configuration file. Do you want to create an empty configuration file?",
                             "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        SQLOutAdapterConfigMgt.Save(SQLOutAdapterConfigMgt._FileName);
                    }
                }
            }
        }
    }
}