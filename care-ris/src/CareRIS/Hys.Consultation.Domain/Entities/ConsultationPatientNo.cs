using Hys.Platform.Domain;
using System;


namespace Hys.Consultation.Domain.Entities
{
    public partial class ConsultationPatientNo : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }

        public string HospitalID { get; set; }
        public string Prefix { get; set; }
        public int MaxLength { get; set; }
        public int CurrentValue { get; set; }
        
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
