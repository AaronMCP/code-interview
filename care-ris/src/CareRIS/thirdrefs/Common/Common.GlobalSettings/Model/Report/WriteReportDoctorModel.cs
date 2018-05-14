using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Kodak.GCRIS.Common.Model.Report
{
    [Serializable()]
    public class WriteReportDoctorModel : ReportBaseModel
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _userGuid;

        public string UserGuid
        {
            get { return _userGuid; }
            set { _userGuid = value; }
        }
        private string _preferredModalityType = string.Empty;

        public string PreferredModalityType
        {
            get { return _preferredModalityType; }
            set { _preferredModalityType = value; }
        }
        private string _preferredPhysiologicalSystem = string.Empty;

        public string PreferredPhysiologicalSystem
        {
            get { return _preferredPhysiologicalSystem; }
            set { _preferredPhysiologicalSystem = value; }
        }
        private string _preferredPatientType = string.Empty;

        public string PreferredPatientType
        {
            get { return _preferredPatientType; }
            set { _preferredPatientType = value; }
        }
        private string _imStatus;

        public string IMStatus
        {
            get { return _imStatus; }
            set { _imStatus = value; }
        }


        private bool _canReceiveReport;

        public bool CanReceiveReport
        {
            get { return _canReceiveReport; }
            set { _canReceiveReport = value; }
        }

        private string _domain;

        public string Domain
        {
            get { return _domain; }
            set { _domain = value; }
        }
        private bool _isOnline;

        public bool IsOnline
        {
            get { return _isOnline; }
            set { _isOnline = value; }
        }

        private int _assignedUnwrittenReportCount;

        public int AssignedUnwrittenReportCount
        {
            get { return _assignedUnwrittenReportCount; }
            set { _assignedUnwrittenReportCount = value; }
        }

        public override string ToString()
        {
            string preferredTypeString = _preferredModalityType + "/" + _preferredPhysiologicalSystem + "/" + _preferredPatientType;
            preferredTypeString = preferredTypeString.Trim("/".ToCharArray());
            if (!preferredTypeString.Equals("/"))
            {
                return string.Format("{0} ({1})", _name, preferredTypeString);
            }
            else
            {
                return _name;
            }
        }
    }
}
