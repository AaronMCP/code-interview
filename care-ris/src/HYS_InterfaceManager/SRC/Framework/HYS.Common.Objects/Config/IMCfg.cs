using System;
using System.Collections.Generic;
using HYS.Common.Xml;

namespace HYS.Common.Objects.Config
{
    public class IMCfg : ConfigBase
    {
        private string _deviceFolder = ConfigHelper.IMDeviceFolder;
        public string DeviceFolder
        {
            get { return _deviceFolder; }
            set { _deviceFolder = value; }
        }

        private string _interfaceFolder = ConfigHelper.IMInterfaceFolder;
        public string InterfaceFolder
        {
            get { return _interfaceFolder; }
            set { _interfaceFolder = value; }
        }

        private string _dataDBConnection = "";
        [XCData(true)]
        public string DataDBConnection
        {
            get { return _dataDBConnection; }
            set { _dataDBConnection = value; }
        }

        private string _configDBConnection = "";
        [XCData(true)]
        public string ConfigDBConnection
        {
            get { return _configDBConnection; }
            set { _configDBConnection = value; }
        }

        private string _helpFileName = "..\\Manual\\Gateway_Service_Manual.pdf";
        [XCData(true)]
        public string HelpFileName
        {
            get { return _helpFileName; }
            set { _helpFileName = value; }
        }

        private string _appCaption = ConfigHelper.IMDefaultCaption;
        [XCData(true)]
        public string AppCaption
        {
            get { return _appCaption; }
            set { _appCaption = value; }
        }

        private bool _showDeviceViewWhenStartup = true;
        public bool ShowDeviceViewWhenStartup
        {
            get { return _showDeviceViewWhenStartup; }
            set { _showDeviceViewWhenStartup = value; }
        }

        private bool _showConfigWhenInterfaceInstall = true;
        public bool ShowConfigWhenInterfaceInstall
        {
            get { return _showConfigWhenInterfaceInstall; }
            set { _showConfigWhenInterfaceInstall = value; }
        }

        private bool _showConfigAfterImportConfig = true;
        public bool ShowConfigAfterImportConfig
        {
            get { return _showConfigAfterImportConfig; }
            set { _showConfigAfterImportConfig = value; }
        }

        private string _osqlFileName = "C:\\Program Files\\Microsoft SQL Server\\90\\Tools\\Binn\\osql.exe";
        [XCData(true)]
        public string OSqlFileName
        {
            get { return _osqlFileName; }
            set { _osqlFileName = value; }
        }

        private string _osqlParameter = "-S (local) -E";
        [XCData(true)]
        public string OSqlParameter
        {
            get { return _osqlParameter; }
            set { _osqlParameter = value; }
        }

        private string _recentDeviceSelectionFolder = "";
        public string RecentDeviceSelectionFolder
        {
            get { return _recentDeviceSelectionFolder; }
            set { _recentDeviceSelectionFolder = value; }
        }

        private string _loginUser = "service";
        [XCData(true)]
        public string LoginUser
        {
            get { return _loginUser; }
            set { _loginUser = value; }
        }

        private string _loginPassword = "service";
        [XCData(true)]
        public string LoginPassword
        {
            get { return _loginPassword; }
            set { _loginPassword = value; }
        }

        private bool _isPasswordSecret = true;
        [XNode(false)]
        public bool IsPasswordSecret
        {
            get 
            {
                return _isPasswordSecret;
            }
            set 
            {
                _isPasswordSecret = value;
            }
        }

        private XCollection<RegularExpressionItem> _regularExpressions = new XCollection<RegularExpressionItem>();
        public XCollection<RegularExpressionItem> RegularExpressions
        {
            get { return _regularExpressions; }
            set { _regularExpressions = value; }
        }
    }
}
