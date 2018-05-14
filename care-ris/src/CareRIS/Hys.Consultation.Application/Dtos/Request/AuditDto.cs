using System;

namespace Hys.Consultation.Application.Dtos
{
    public class AuditDto
    {
        public string LastEditUser { get; set; }
        public DateTime LastEditTime { get; set; }
    }
}
