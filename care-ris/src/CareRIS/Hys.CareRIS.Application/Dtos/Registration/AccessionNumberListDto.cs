using System;
namespace Hys.CareRIS.Application.Dtos
{
    public  class AccessionNumberListDto
    {
        public string UniqueID { get; set; }
        public string AccNo { get; set; }
        public string OrderID { get; set; }
        public string PatientID { get; set; }
        public string HisID { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
