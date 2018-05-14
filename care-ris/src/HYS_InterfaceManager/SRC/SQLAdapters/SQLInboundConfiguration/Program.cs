using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HYS.SQLInboundAdapterObjects;
using HYS.Common.Xml;
using HYS.Common.Objects.Rule;
using HYS.Common.DataAccess;
using HYS.Common.Objects.Device;
using HYS.SQLInboundAdapterConfiguration.Forms;

namespace HYS.SQLInboundAdapterConfiguration
{
    static class Program
    {
        public static string GWDataDBConnection;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PreLoading();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SQLInboundConfiguration());
        }

        public static DeviceDirManager DeviceMgt;
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
            string FileName = Application.StartupPath + "\\" + SQLInAdapterConfigMgt._FileName;
            if (!SQLInAdapterConfigMgt.Load(FileName))
            {
                if (SQLInAdapterConfigMgt.LastException != null)
                {
                    if (MessageBox.Show("Cannot load configuration file. Do you want to create an empty configuration file?",
                             "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        SQLInAdapterConfigMgt.Save(SQLInAdapterConfigMgt._FileName);
                    }
                }
            }
        }
        
             
        //internal static void PreLoading()
        //{
        //    DeviceMgt = new DeviceDirManager();
        //    DeviceMgt.FileName = Application.StartupPath + "\\" + DeviceDirManager.IndexFileName;
        //    if (!DeviceMgt.LoadDeviceDir())
        //    {
        //        MessageBox.Show("Cannot load DeviceDir file.");
        //    }

        //    LoadConfigXMLFile();
        //}

        //internal static void LoadConfigXMLFile()
        //{
        //    string FileName = Application.StartupPath + "\\" + SQLInAdapterConfigMgt._FileName;

        //    if (!SQLInAdapterConfigMgt.Load(FileName))
        //    {
        //        if (SQLInAdapterConfigMgt.LastException != null)
        //        {
        //            if (MessageBox.Show("Cannot load configuration file. Do you want to create an empty configuration file?",
        //                     "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
        //            {
        //                SQLInAdapterConfigMgt.Save(SQLInAdapterConfigMgt._FileName);
        //            }
        //        }
        //    }
        //}
    }
}