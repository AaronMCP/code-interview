using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class FtpModel
    {
        private string ftpServer = null;
        private string ftpPort = null;
        private string ftpRootFolder = null;
        private string ftpUserID = null;
        private string ftpPassword = null;

        public string FtpServer
        {
            get
            {
                return ftpServer;
            }
            set
            {
                ftpServer = value;
            }
        }

        public string FtpPort
        {
            get
            {
                return ftpPort;
            }
            set
            {
                ftpPort = value;
            }
        }

        public string FtpRootFolder
        {
            get
            {
                return ftpRootFolder;
            }
            set
            {
                ftpRootFolder = value;
            }
        }

        public string FtpUserID
        {
            get
            {
                return ftpUserID;
            }
            set
            {
                ftpUserID = value;
            }
        }

        public string FtpPassword
        {
            get
            {
                return ftpPassword;
            }
            set
            {
                ftpPassword = value;
            }
        }
    }
}
