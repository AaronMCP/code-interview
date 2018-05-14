namespace Hys.CareRIS.Application.Dtos
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// for request module
    /// </summary>
    public class RequestInfoDto
    {
        public bool IsHasNoRequestItem { get; set; }
        public PatientDto Patient { get; set; }
        public IList<RequestDto> Requests { get; set; }
    }
}
