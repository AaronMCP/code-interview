using HYS.Common.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYS.Common.Dicom.Net
{
    public class SCPConfig : XObject, IConfig
    {
        public SCPConfig()
        {
        }

        //private string _implementationClassUID = "1.2.3.10080";
        private string _implementationClassUID = "1.2.840.113564.3.1.64";
        public string ImplementationClassUID
        {
            get { return _implementationClassUID; }
            set { _implementationClassUID = value; }
        }

        private string _implementationVersion = "2.0";
        public string ImplementationVersion
        {
            get { return _implementationVersion; }
            set { _implementationVersion = value; }
        }

        private int _maxPduLength = 128;    //kb
        public int MaxPduLength
        {
            get { return _maxPduLength; }
            set { _maxPduLength = value; }
        }

        private int _maxAssociation = -1;
        public int MaxAssociation
        {
            get { return _maxAssociation; }
            set { _maxAssociation = value; }
        }

        private int _associationTimeOut = 30000;        //ms    30s    0.5min
        public int AssociationTimeOut
        {
            get { return _associationTimeOut; }
            set { _associationTimeOut = value; }
        }

        private int _sessionTimeOut = 60000;          //ms    60s    1min
        public int SessionTimeOut
        {
            get { return _sessionTimeOut; }
            set { _sessionTimeOut = value; }
        }

        private string _aeTitle = "EKC_MWL_SERVER";
        public string AETitle
        {
            get { return _aeTitle; }
            set { _aeTitle = value; }
        }

        private int _port = 5678;
        public int Port
        {
            get { return _port; }
            set { _port = value; }
        }

        private XCollection<Modality> _knownModalities = new XCollection<Modality>();
        public XCollection<Modality> KnownModalities
        {
            get { return _knownModalities; }
            set { _knownModalities = value; }
        }

        public string FindAETitleByCallingIP(string callingIP)
        {
            if (string.IsNullOrEmpty(callingIP)) return "";
            foreach (Modality m in _knownModalities)
            {
                if (m.IPAddress == callingIP) return m.AETitle;
            }
            return "";
        }
        public string FindDescriptionByCallingIP(string callingIP)
        {
            if (string.IsNullOrEmpty(callingIP)) return "";
            foreach (Modality m in _knownModalities)
            {
                if (m.IPAddress == callingIP) return m.Description;
            }
            return "";
        }

        private bool _enableAETitleChecking = false;
        public bool EnableAETitleChecking
        {
            get { return _enableAETitleChecking; }
            set { _enableAETitleChecking = value; }
        }

        private bool _enableModalityChecking = false;
        public bool EnableModalityChecking
        {
            get { return _enableModalityChecking; }
            set { _enableModalityChecking = value; }
        }

        public bool CheckAETitle(string calledAETitle)
        {
            if (!EnableAETitleChecking) return true;
            if (calledAETitle == null) return false;
            return calledAETitle.Trim() == AETitle.Trim();
        }
        public bool CheckModality(string callingAETitle, string callingIP)
        {
            if (!EnableModalityChecking) return true;
            if (callingAETitle == null) return false;
            if (callingIP == null) return false;
            foreach (Modality mod in KnownModalities)
            {
                if (callingIP.Trim() == mod.IPAddress.Trim() &&
                    callingAETitle.Trim() == mod.AETitle.Trim()) return true;
            }
            return false;
        }

        public string GetSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("AETitle:").Append(AETitle).Append("; ");
            sb.Append("Port:").Append(Port).Append("; ");
            return sb.ToString();
        }
    }
}
