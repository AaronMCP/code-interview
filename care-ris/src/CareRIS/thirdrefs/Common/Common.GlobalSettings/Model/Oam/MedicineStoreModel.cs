using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Action;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class MedicineStoreModel : OamBaseModel
    {
        public string MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public int Counts { get; set; }
        public DateTime CreateDt { get; set; }

    }

    [Serializable()]
    public class MedicineStoreLogModel : OamBaseModel
    {
        public string MedicineGuid { get; set; }
        public string MedicineCode { get; set; }
        public string MedicineName { get; set; }
        public string Spec { get; set; }
        public string Batch { get; set; }
        public string Manufacturer { get; set; }
        public DateTime  ManufaturingDt { get; set; }
        public string Signature { get; set; }
        public string Remark { get; set; }
        public int Counts { get; set; }
        public int Type { get; set; }
        public string PatientID{ get; set; }
        public string AccNo { get; set; }
        public string OperatorID { get; set; }
        public string OperatorName { get; set; }
        public DateTime CreateDt { get; set; }
    }

}
