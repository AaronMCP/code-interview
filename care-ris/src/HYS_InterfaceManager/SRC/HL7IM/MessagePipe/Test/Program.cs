using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HYS.Messaging.Base.Config;

namespace HYS.MessageDevices.MessagePipe.Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TestGetRelativePath();
            return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void TestGetRelativePath()
        {
            string path = @"C:\XDSGW1.1\RHIS_PIX\Tools\HL7RCV_MSGPIPE_FEEDnQUERY_Config\..\..\Entities\HL7RCV_MSGPIPE_FEEDnQUERY\";
            //string path = @"C:\XDSGW1.1\RHIS_PIX\Tools\HL7RCV_MSGPIPE_FEEDnQUERY_Config\..\..\Entities\HL7RCV_MSGPIPE_FEEDnQUERY";
            //string path = @"C:\XDSGW1.1\RHIS_PIX\Tools\HL7RCV_MSGPIPE_FEEDnQUERY_Config\..\..\Entities\HL7RCV_MSGPIPE_FEEDnQUERY\..";
            //string path = @"C:\XDSGW1.1\RHIS_PIX\Tools\HL7RCV_MSGPIPE_FEEDnQUERY_Config\..\..\Entities\HL7RCV_MSGPIPE_FEEDnQUERY\..\";
            //string path = @"C:\XDSGW1.1\RHIS_PIX\Tools\HL7RCV_MSGPIPE_FEEDnQUERY_Config\..\..\..\..\..\..\Entities\HL7RCV_MSGPIPE_FEEDnQUERY";
            //string path = @"C:\XDSGW1.1\RHIS_PIX\Tools\HL7RCV_MSGPIPE_FEEDnQUERY_Config\..\..\..\..\..\..\Entities\HL7RCV_MSGPIPE_FEEDnQUERY\..\..\";
            MessageBox.Show(ConfigHelper.DismissDotDotInThePath(path));

            string rootedFrom = @"C:\XDSGW1.1\RHIS_PIX\Tools\HL7RCV_MSGPIPE_FEEDnQUERY_Config\..\..\Entities\HL7RCV_MSGPIPE_FEEDnQUERY\";
            //string rootedFrom = @"C:\XDSGW1.1\RHIS_PIX\Entities\HL7RCV_MSGPIPE_FEEDnQUERY\";
            //string rootedFrom = @"C:\XDSGW1.1\RHIS_PIX\Entities\HL7RCV_MSGPIPE_FEEDnQUERY";
            string rootedPath = @"C:\XDSGW1.1\RHIS_PIX\Entities\HL7RCV_MSGPIPE_FEEDnQUERY\MessagePipe.xml";

            string relativePath = ConfigHelper.GetRelativePath(rootedFrom, rootedPath);
            MessageBox.Show(relativePath);
        }
    }
}
