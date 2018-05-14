using System;

namespace Hys.Consultation.Application.Dtos
{
    public class DAMInfoDto
    {
        public string UniqueID { get; set; }
        public string Name { get; set; }
        public string IpAddress { get; set; }
        public string WebApiUrl { get; set; }
        public string Description { get; set; }
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}