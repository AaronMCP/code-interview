using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonGlobalSettings
{
    public class UnwrittenReport
    {
        private string _accNumber;

        public string AccNumber
        {
            get { return _accNumber; }
            set { _accNumber = value; }
        }

        private string _currentSite;

        public string CurrentSite
        {
            get { return _currentSite; }
            set { _currentSite = value; }
        }

        private string _procedureCode;

        public string ProcedureCode
        {
            get { return _procedureCode; }
            set { _procedureCode = value; }
        }
        private string _modalityType;

        public string ModalityType
        {
            get { return _modalityType; }
            set { _modalityType = value; }
        }
        private string _physiologicalSystem;

        public string PhysiologicalSystem
        {
            get { return _physiologicalSystem; }
            set { _physiologicalSystem = value; }
        }
        private string _bodyCategory;

        public string BodyCategory
        {
            get { return _bodyCategory; }
            set { _bodyCategory = value; }
        }
        private DateTime _createTime;

        public DateTime CreateTime
        {
            get { return _createTime; }
            set { _createTime = value; }
        }
        private string _previousOwner;

        public string PreviousOwner
        {
            get { return _previousOwner; }
            set { _previousOwner = value; }
        }
        private string _currentOwner;

        public string CurrentOwner
        {
            get { return _currentOwner; }
            set { _currentOwner = value; }
        }
        private string _unapprovedOwner;

        public string UnapprovedOwner
        {
            get { return _unapprovedOwner; }
            set { _unapprovedOwner = value; }
        }
        private string _unwrittenOwner;

        public string UnwrittenOwner
        {
            get { return _unwrittenOwner; }
            set { _unwrittenOwner = value; }
        }
        private string _remark;

        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        private string _patientID;

        public string PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }
        private string _patientName;

        public string PatientName
        {
            get { return _patientName; }
            set { _patientName = value; }
        }
        private string _gender;

        public string Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        private string _exam;

        public string Exam
        {
            get { return _exam; }
            set { _exam = value; }
        }
        private DateTime _examDate;

        public DateTime ExamDate
        {
            get { return _examDate; }
            set { _examDate = value; }
        }
        private string _patientType;

        public string PatientType
        {
            get { return _patientType; }
            set { _patientType = value; }
        }
        private string _department;

        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        private string _creator;

        public string Creator
        {
            get { return _creator; }
            set { _creator = value; }
        }
        private string _procedureGuid;

        public string ProcedureGuid
        {
            get { return _procedureGuid; }
            set { _procedureGuid = value; }
        }
        private string _reportGuid;

        public string ReportGuid
        {
            get { return _reportGuid; }
            set { _reportGuid = value; }
        }
        private string _domain;

        public string Domain
        {
            get { return _domain; }
            set { _domain = value; }
        }
        private string assignmentType;

        public string AssignmentType
        {
            get { return assignmentType; }
            set { assignmentType = value; }
        }
        private decimal _unwrittenWeight;

        public decimal UnwrittenWeight
        {
            get { return _unwrittenWeight; }
            set { _unwrittenWeight = value; }
        }
        private decimal _unapprovedWeight;

        public decimal UnapprovedWeight
        {
            get { return _unapprovedWeight; }
            set { _unapprovedWeight = value; }
        }

        public string CurrentOwnerName { get; set; }

        public string PreviousOwnerName { get; set; }

        public string Submitter_Top_RoleLevel { get; set; }

        public Int32 AvailableDoctorCount { get; set; }
    }
}
