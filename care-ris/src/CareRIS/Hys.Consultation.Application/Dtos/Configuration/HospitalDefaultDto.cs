using System;

namespace Hys.Consultation.Application.Dtos
{
    public class HospitalDefaultDto
    {
        public string UniqueID { get; set; }

        public HospitalDefaultType RequestType { get; set; }
        public string RequestID { get; set; }
        public string RequestName { get; set; }
        public HospitalDefaultType ResponseType { get; set; }
        public string ResponseID { get; set; }
        public string ResponseName { get; set; }
        public int Order { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Owner { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
