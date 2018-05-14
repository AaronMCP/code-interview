using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class OrderItemDto
    {
        public OrderItemDto()
        {
            Procedures = new List<ProcedureItemDto>();
        }

        // patient info
        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientNo { get; set; }
        public DateTime? Birthday { get; set; }

        // order info
        public string OrderID { get; set; }
        public string AccNo { get; set; }
        public string PatientType { get; set; }
        public string CurrentSite { get; set; }
        public string ExamSite { get; set; }
        public DateTime CreatedTime { get; set; }
        public string CurrentAge { get; set; }
        public int? AgeInDays { get; set; }
        public bool? IsScan { get; set; }
        public string ReferralID { get; set; }
        // procedure info
        public IEnumerable<ProcedureItemDto> Procedures { get; set; }
        public DateTime? ExamineTime { get; set; }
        public string StudyInstanceUID { get; set; }

    }

    public class ProcedureItemDto
    {
        // procedure info
        public string ProcedureID { get; set; }
        public int Status { get; set; }
        public string ModalityType { get; set; }
        public string RPDesc { get; set; }
        public string Modality { get; set; }
        public string ReportID { get; set; }
        public DateTime? ExamineTime { get; set; }
        public int? IsPrint { get; set; }
        public int? IsExistImage { get; set; }
        public string OrderId { get; set; }
        public string ExamSystem { get; set; }
    }
}
