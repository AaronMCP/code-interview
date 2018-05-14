using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class GWReportDto
    {
        public string UniqueID { get; set; }
        public DateTime? DataTime { get; set; }
        public string ReportNo { get; set; }
        public string AccessionNumber { get; set; }
        public string PatientID { get; set; }
        public string ReportStatus { get; set; }
        public string Modality { get; set; }
        public int ReportType { get; set; }
        public string ReportFile { get; set; }
        public string Diagnose { get; set; }
        public string Comments { get; set; }
        public string ReportWriter { get; set; }
        public string ReportIntepreter { get; set; }
        public string ReportApprover { get; set; }
        public string ReportTime { get; set; }
        public string ObservationMethod { get; set; }
        public string Customer1 { get; set; }
        public string Customer2 { get; set; }
        public string Customer3 { get; set; }
        public string Customer4 { get; set; }
    }
}
