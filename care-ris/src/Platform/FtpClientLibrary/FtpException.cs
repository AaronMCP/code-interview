using System;
using System.Collections.Generic;
using System.Text;

namespace Hys.Common
{
    public class FtpException : Exception
    {
        private FtpResult result = 0;

        public FtpException(string message) : base(message) 
        { 

        }

        public FtpException(string message, Exception innerException) : base(message, innerException) 
        { 

        }

        public FtpResult FtpResult 
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
            }
        }
    }
}
