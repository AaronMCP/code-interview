using Dicom.Log;
using HYS.Adapter.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HYS.Common.Dicom
{
    public class LogMgt
    {
        private static Logging Log = new Logging(Application.StartupPath + "\\Dicom.log");
        public static Logging Logger
        {
            get
            {
                return Log;
            }
        }
    }
}
