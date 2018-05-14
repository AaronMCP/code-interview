using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    public class WriteReportDoctor : IComparable<WriteReportDoctor>
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private string _site;

        public string Site
        {
            get { return _site; }
            set { _site = value; }
        }
        private string _userGuid;

        public string UserGuid
        {
            get { return _userGuid; }
            set { _userGuid = value; }
        }
        private string _preferredModalityType;

        public string PreferredModalityType
        {
            get { return _preferredModalityType; }
            set { _preferredModalityType = value; }
        }
        private string _preferredPhysiologicalSystem;

        public string PreferredPhysiologicalSystem
        {
            get { return _preferredPhysiologicalSystem; }
            set { _preferredPhysiologicalSystem = value; }
        }
        private string _preferredPatientType;

        public string PreferredPatientType
        {
            get { return _preferredPatientType; }
            set { _preferredPatientType = value; }
        }

        private string _preferredSite;

        public string PreferredSite
        {
            get { return _preferredSite; }
            set { _preferredSite = value; }
        }

        private string _preferredBodypartCategory;
        public string PreferredBodypartCategory
        {
            get { return _preferredBodypartCategory; }
            set { _preferredBodypartCategory = value; }
        }

        private string _reportDoctorGroup;
        public string ReportDoctorGroup
        {
            get { return _reportDoctorGroup; }
            set { _reportDoctorGroup = value; }
        }

        private DoctorStatus _status;

        public DoctorStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        private List<UnwrittenReport> _assignedUnwrittenReportList;

        public List<UnwrittenReport> AssignUnwrittenReportList
        {
            get { return _assignedUnwrittenReportList; }
            set { _assignedUnwrittenReportList = value; }
        }
        private int _assignedReportCountOfToday;

        public int AssignedReportCountOfToday
        {
            get { return _assignedReportCountOfToday; }
            set { _assignedReportCountOfToday = value; }
        }

        private int _assignedUnwrittenReportCount;

        public int AssignedUnwrittenReportCount
        {
            get { return _assignedUnwrittenReportCount; }
            set { _assignedUnwrittenReportCount = value; }
        }

        private int _assignedReportCountThisTime = 0;

        public int AssignedReportCountThisTime
        {
            get { return _assignedReportCountThisTime; }
            set { _assignedReportCountThisTime = value; }
        }

        private decimal _estimatedUnAssignedReportCountThisTime = 0;

        public decimal EstimatedUnAssignedReportCountThisTime
        {
            get { return _estimatedUnAssignedReportCountThisTime; }
            set { _estimatedUnAssignedReportCountThisTime = value; }
        }

        private int _originalAssignedUnwrittenReportCount;

        public int OriginalAssignedUnwrittenReportCount
        {
            get { return _originalAssignedUnwrittenReportCount; }
            set { _originalAssignedUnwrittenReportCount = value; }
        }
        private int _maxHoldUnwrittenReportCount;

        public int MaxHoldUnwrittenReportCount
        {
            get { return _maxHoldUnwrittenReportCount; }
            set { _maxHoldUnwrittenReportCount = value; }
        }
        private int _maxAssignedReportCountOfToday;

        public int MaxAssignedReportCountOfToday
        {
            get { return _maxAssignedReportCountOfToday; }
            set { _maxAssignedReportCountOfToday = value; }
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
        private string _imStatus;

        public string IMStatus
        {
            get { return _imStatus; }
            set { _imStatus = value; }
        }
        private string _supervisor;

        public string Supervisor
        {
            get { return _supervisor; }
            set { _supervisor = value; }
        }
        private AssignmentType assignmentType;

        public AssignmentType AssignmentType
        {
            get { return assignmentType; }
            set { assignmentType = value; }
        }
        private decimal _currentDayAssignedWeight = 0;

        public decimal CurrentDayAssignedWeight
        {
            get { return _currentDayAssignedWeight; }
            set { _currentDayAssignedWeight = value; }
        }
        private decimal _averageWeight = 0;

        public decimal AverageWeight
        {
            get { return _averageWeight; }
            set { _averageWeight = value; }
        }
        private decimal _maxAssignWeightPercentage;

        public decimal MaxAssignWeightPercentage
        {
            get { return _maxAssignWeightPercentage; }
            set { _maxAssignWeightPercentage = value; }
        }
        private decimal _maxAssignedWeightToday = 0;

        public decimal MaxAssignedWeightToday
        {
            get { return _maxAssignedWeightToday; }
            set { _maxAssignedWeightToday = value; }
        }

        private DateTime _beginDateTime;

        public DateTime BeginDateTime
        {
            get { return _beginDateTime; }
            set { _beginDateTime = value; }
        }
        private DateTime _endDateTime;

        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set { _endDateTime = value; }
        }

        private string _topRoleLevel;

        public string TopRoleLevel
        {
            get { return _topRoleLevel; }
            set { _topRoleLevel = value; }
        }

        private decimal _totalPunishWeight;
        public decimal TotalPunishWeight
        {
            get { return _totalPunishWeight; }
            set { _totalPunishWeight = value; }
        }

        private decimal _punishedWeight;
        public decimal PunishedWeight
        {
            get { return _punishedWeight; }
            set { _punishedWeight = value; }
        }

        private decimal _unPunishWeight;
        public decimal UnPunishWeight
        {
            get { return _unPunishWeight; }
            set { _unPunishWeight = value; }
        }


        public List<string> PreferredModalityTypeList
        {
            get
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrWhiteSpace(_preferredModalityType))
                {
                    string[] modalityTypes = _preferredModalityType.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (modalityTypes != null && modalityTypes.Length > 0)
                    {
                        list.AddRange(modalityTypes);
                    }
                }
                return list;
            }
        }

        public List<string> PreferredPhysiologicalSystemList
        {
            get
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrWhiteSpace(_preferredPhysiologicalSystem))
                {
                    string[] physiologicalSystem = _preferredPhysiologicalSystem.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (physiologicalSystem != null && physiologicalSystem.Length > 0)
                    {
                        list.AddRange(physiologicalSystem);
                    }
                }
                return list;
            }
        }

        public List<string> PreferredPatientTypeList
        {
            get
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrWhiteSpace(_preferredPatientType))
                {
                    string[] patientType = _preferredPatientType.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (patientType != null && patientType.Length > 0)
                    {
                        list.AddRange(patientType);
                    }
                }
                return list;
            }
        }

        public List<string> PreferredBodypartCategoryList
        {
            get
            {
                List<string> list = new List<string>();
                if (!string.IsNullOrWhiteSpace(_preferredBodypartCategory))
                {
                    string[] category = _preferredBodypartCategory.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (category != null && category.Length > 0)
                    {
                        list.AddRange(category);
                    }
                }
                return list;
            }
        }

        private bool _isAssigned;
        /// <summary>
        /// only used by DealingCardsPolicy temporarily
        /// </summary>
        public bool IsAssigned
        {
            get { return _isAssigned; }
            set { _isAssigned = value; }
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

        public string ToString2()
        {
            string preferredTypeString = _preferredModalityType + "/" + _preferredPhysiologicalSystem + "/" + _preferredPatientType;
            preferredTypeString = preferredTypeString.Trim("/".ToCharArray());
            string statString = _assignedUnwrittenReportCount.ToString() + "/" + _assignedReportCountOfToday.ToString();//unfinishedCount/totalCount            

            if (!preferredTypeString.Equals("/"))
            {
                return string.Format("{0} ({1})({2})", _name, preferredTypeString, statString);
            }
            else
            {
                return _name;
            }
        }


        #region IComparable<WriteReportDoctor> Members

        public int CompareTo(WriteReportDoctor other)
        {
            return this.AssignedUnwrittenReportCount.CompareTo(other.AssignedUnwrittenReportCount);
        }

        #endregion
    }
}
