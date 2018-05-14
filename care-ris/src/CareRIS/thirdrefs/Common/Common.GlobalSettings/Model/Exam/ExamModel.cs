using System;
using System.Collections.Generic;
using System.Text;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class ExamModel : ExamBaseModel
    {
        private string accNo;
        private string patientID;
        private string localName;
        private string status;
        private string orderGuid;
        private string procedureGuid;
        private string applyDept;
        private string applyDoctor;
        private string registrarGuid;
        private string techDoctorGuid;
        private string modalityType;
        private string modality;
        private string technicianGuid;
        private string birthday;
        private string procedureCode;
        private string procedureCodeDesc;
        private string technician;
        private string gender;

        public string Technician
        {
            set { technician = value; }
            get { return technician; }
        }

        public string ProcedureCodeDesc
        {
            set { procedureCodeDesc = value; }
            get { return procedureCodeDesc; }
        }

        public string ProcedureCode
        {
            set { procedureCode = value; }
            get { return procedureCode; }
        }


        public string AccNo
        {
            set { accNo = value; }
            get { return accNo; }
        }

        public string LocalName
        {
            set { localName = value; }
            get { return localName; }
        }

        public string PatientID
        {
            set { patientID = value; }
            get { return patientID; }
        }

        public string Status
        {
            set { status = value; }
            get { return status; }
        }

        public string OrderGuid
        {
            set { orderGuid = value; }
            get { return orderGuid; }
        }

        public string ProcedureGuid
        {
            set { procedureGuid = value; }
            get { return procedureGuid; }
        }

        public string ApplyDept
        {
            set { applyDept = value; }
            get { return applyDept; }
        }

        public string ApplyDoctor
        {
            set { applyDoctor = value; }
            get { return applyDoctor; }
        }

        public string RegistrarGuid
        {
            set { registrarGuid = value; }
            get { return registrarGuid; }
        }

        public string TechDoctorGuid
        {
            set { techDoctorGuid = value; }
            get { return techDoctorGuid; }
        }

        public string ModalityType
        {
            set { modalityType = value; }
            get { return modalityType; }
        }

        public string Modality
        {
            set { modality = value; }
            get { return modality; }
        }

        public string TechnicianGuid
        {
            set { technicianGuid = value; }
            get { return technicianGuid; }
        }

        public string Birthday
        {
            set { birthday = value; }
            get { return birthday; }
        }

        public string Gender
        {
            set { gender = value; }
            get { return gender; }
        }
    }
}
