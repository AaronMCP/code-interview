using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class AssignmentLog : SearchCriteriaBaseModel
    {
        private OperationType _operationType;

        public OperationType OperationType
        {
            get { return _operationType; }
            set { _operationType = value; }
        }
        private string _assigner;

        public string Assigner
        {
            get { return _assigner; }
            set { _assigner = value; }
        }
        private string _assignee;

        public string Assignee
        {
            get { return _assignee; }
            set { _assignee = value; }
        }
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

        private string _modalityType;

        public string ModalityType
        {
            get { return _modalityType; }
            set { _modalityType = value; }
        }

        private string _examSystem;

        public string ExamSystem
        {
            get { return _examSystem; }
            set { _examSystem = value; }
        }
        private string _patientType;

        public string PatientType
        {
            get { return _patientType; }
            set { _patientType = value; }
        }
        private string _procedureCode;

        public string ProcedureCode
        {
            get { return _procedureCode; }
            set { _procedureCode = value; }
        }

        private DateTime _operationDate;

        public DateTime OperationDate
        {
            get { return _operationDate; }
            set { _operationDate = value; }
        }
        private AssignmentType assignmentType;

        public AssignmentType AssignmentType
        {
            get { return assignmentType; }
            set { assignmentType = value; }
        }
        private string _operator;

        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
        private string _site;

        public string Site
        {
            get { return _site; }
            set { _site = value; }
        }

        

    }
}
