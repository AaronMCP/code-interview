using System;
using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class MeetingInfoDto
    {
        public string IPAddress { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Version { get; set; }
        public string MeetingPassword { get; set; }
        public string Site { get; set; }
    }
}
