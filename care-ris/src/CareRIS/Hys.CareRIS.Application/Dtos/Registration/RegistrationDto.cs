namespace Hys.CareRIS.Application.Dtos
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// for registration module
    /// </summary>
    public class RegistrationDto
    {
        public List<OrderDto> Orders { get; set; }
        public PatientDto Patient { get; set; }
        public List<ProcedureDto> Procedures { get; set; }
        //public RequestDto Request { get; set; }
        public List<RequestDto> Requests { get; set; }
        public ReuisitionFilesDto RequisitionFiles { get; set; }
    }

    public class RegistrationLiteDto
    {
        public PatientDto Patient { get; set; }
        public OrderLiteDto Order { get; set; }
        public List<ProcedureLiteDto> Procedures { get; set; }
    }

    public class RegistrationViewDto
    {
        public RegistrationLiteDto Registration { get; set; }
        public OrderItemDto OrderItem { get; set; }
    }

    public class ImageData
    {
        public string Base64Str { get; set; }
        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public string RequisitionID { get; set; }
        public long ImageQualityLevel { get; set; }
        public bool IsUpdate { get; set; }
    }

    public class ReuisitionFilesDto
    {
        public IList<ImageData> RequisitionFiles { get; set; }
        public string ErNo { get; set; }
        public string ImageQualityLevel { get; set; }
    }
}
