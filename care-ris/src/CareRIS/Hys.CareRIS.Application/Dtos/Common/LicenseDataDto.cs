namespace Hys.CareRIS.Application.Dtos
{
    public class LicenseDataDto
    {
        public string ErrorMessage { get; set; }
        public int MaxOnlineUserCount { get; set; }
        public bool RisEnabled { get; set; }
        public bool ConsultationEnabled { get; set; }
        public bool IsSuccessed { get; set; }
        public bool IsExpired { get; set; }
    }
}
