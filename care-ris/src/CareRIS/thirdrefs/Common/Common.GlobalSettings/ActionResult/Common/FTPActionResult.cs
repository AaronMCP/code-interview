using System;
using System.Collections.Generic;
using System.Text;
using CommonGlobalSettings;

namespace Common.ActionResult
{
    public class FTPActionResult : CommonBaseActionResult
    {
        private FtpModel model = null;

        public FtpModel Model 
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }
    }
}
