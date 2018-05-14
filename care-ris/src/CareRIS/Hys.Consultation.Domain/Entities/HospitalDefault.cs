using Hys.Platform.Domain;
using System;

namespace Hys.Consultation.Domain.Entities
{
    public partial class HospitalDefault : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }

        public int RequestType { get; set; }
        public string RequestID { get; set; }
        public int ResponseType { get; set; }
        public string ResponseID { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Owner { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
