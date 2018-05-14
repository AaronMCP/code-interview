using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class PatientEditDto
    {
        public PatientDto Patient { get; set; }
        public string  OrderID { get; set; }
        public string CurrentAge { get; set; }
    }
}
