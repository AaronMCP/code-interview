using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class SearchCriteria : SearchCriteriaBaseModel
    {
        public SearchCriteria()
        {
            _graphicArrivalFlagList = new List<GraphicArrivalFlag>();
            _timePeriodList = new List<TimePeriod>();
            _maxAssignCountList = new List<ModalityTypeMaxAssignCount>();
        }

        [XmlArray()]
        private List<GraphicArrivalFlag> _graphicArrivalFlagList;

        public List<GraphicArrivalFlag> GraphicArrivalFlagList
        {
            get { return _graphicArrivalFlagList; }
            set { _graphicArrivalFlagList = value; }
        }

        private List<TimePeriod> _timePeriodList;

        [XmlArray()]
        public List<TimePeriod> TimePeriodList
        {
            get { return _timePeriodList; }
            set { _timePeriodList = value; }
        }

        private List<ModalityTypeMaxAssignCount> _maxAssignCountList;

        [XmlArray()]
        public List<ModalityTypeMaxAssignCount> MaxAssignCountList
        {
            get { return _maxAssignCountList; }
            set { _maxAssignCountList = value; }
        }
        private SpecialCriteria _specialCriteria;

        public SpecialCriteria SpecialCriteria
        {
            get { return _specialCriteria; }
            set { _specialCriteria = value; }
        }
        private int _reportStatus;

        public int ReportStatus
        {
            get { return _reportStatus; }
            set { _reportStatus = value; }
        }
        private string _searchUserGuid;

        public string SearchUserGuid
        {
            get { return _searchUserGuid; }
            set { _searchUserGuid = value; }
        }
        private bool _isGetSpecialReport;

        public bool IsGetSpecialReport
        {
            get { return _isGetSpecialReport; }
            set { _isGetSpecialReport = value; }
        }

        private string _currentSite;

        public string CurrentSite
        {
            get { return _currentSite; }
            set { _currentSite = value; }
        }

        private string[] _paraArray;

        [XmlArray()]
        public string[] ParaArray
        {
            get { return _paraArray; }
            set { _paraArray = value; }
        }

    }

    [Serializable()]
    public class GraphicArrivalFlag : SearchCriteriaBaseModel
    {
        public GraphicArrivalFlag()
        { }

        public GraphicArrivalFlag(string modalityType, int needGraphicArrived)
        {
            _modalityType = modalityType;
            _needGraphicArrived = needGraphicArrived;
        }

        private string _modalityType;

        public string ModalityType
        {
            get { return _modalityType; }
            set { _modalityType = value; }
        }
        private int _needGraphicArrived;

        public int NeedGraphicArrived
        {
            get { return _needGraphicArrived; }
            set { _needGraphicArrived = value; }
        }

    }

    [Serializable()]
    public class TimePeriod : SearchCriteriaBaseModel
    {
        private DateTime _beginTime;

        public DateTime BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }
        private DateTime _endTime;

        public DateTime EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }
        private string _modalityType;

        public string ModalityType
        {
            get { return _modalityType; }
            set { _modalityType = value; }
        }
        private string _patientType;

        public string PatientType
        {
            get { return _patientType; }
            set { _patientType = value; }
        }

        private int _canAutoSetApprover;

        public int CanAutoSetApprover
        {
            get { return _canAutoSetApprover; }
            set { _canAutoSetApprover = value; }
        }

        private int _selectApproverFrom;

        public int SelectApproverFrom
        {
            get { return _selectApproverFrom; }
            set { _selectApproverFrom = value; }
        }
    }

    [Serializable()]
    public class ModalityTypeMaxAssignCount : SearchCriteriaBaseModel
    {
        public ModalityTypeMaxAssignCount()
        { }

        public ModalityTypeMaxAssignCount(string modalityType, int maxAssignCount)
        {
            _modalityType = modalityType;
            _maxAssignCount = maxAssignCount;
        }

        private string _modalityType;

        public string ModalityType
        {
            get { return _modalityType; }
            set { _modalityType = value; }
        }
        private int _maxAssignCount;

        public int MaxAssignCount
        {
            get { return _maxAssignCount; }
            set { _maxAssignCount = value; }
        }

    }

    [Serializable()]
    public class SpecialCriteria : SearchCriteriaBaseModel
    {
        private string _accNo;

        public string AccNo
        {
            get { return _accNo; }
            set { _accNo = value; }
        }
        private string _patientName;

        public string PatientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }
        private string _patientID;

        public string PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

    }

    [Serializable()]
    public class UnlockReport : SearchCriteriaBaseModel
    {
        private string _beginTime;

        public string BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        private string _endTime;

        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        private string _modalityType;

        public string ModalityType
        {
            get { return _modalityType; }
            set { _modalityType = value; }
        }
        private string _patientType;

        public string PatientType
        {
            get { return _patientType; }
            set { _patientType = value; }
        }

        private string _currentSite;

        public string CurrentSite
        {
            get { return _currentSite; }
            set { _currentSite = value; }
        }

        private string[] _paraArray;

        [XmlArray()]
        public string[] ParaArray
        {
            get { return _paraArray; }
            set { _paraArray = value; }
        }
    }
}
