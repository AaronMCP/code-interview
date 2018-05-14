using System;
using System.Collections.Generic;
using System.Text;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class OrderModel : OrderBaseModel
    {
        private string accNo;
        private string bedNo;
        private string localName;
        private string status;
        private string orderGuid;        
        private string applyDept;
        private string applyDoctor;
        private string modalityType;
        private string modality;
        private string procedureCode;
        private string checkingItem;
        private string bodyPart;
        private string bodyCategory;
        private string clinicDiagnos;

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

        public string BEDNO
        {
            set { bedNo = value; }
            get { return bedNo; }
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

    }
}
