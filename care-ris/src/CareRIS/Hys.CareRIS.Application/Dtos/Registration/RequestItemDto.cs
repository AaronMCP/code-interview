using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class RequestItemDto
    {
        public string UniqueID { get; set; }

        public string RequestID { get; set; }

        public string RequestItemUID { get; set; }

        public string ModalityType { get; set; }

        public string Modality { get; set; }

        public string ProcedureCode { get; set; }

        public string RPDesc { get; set; }

        public string ExamSystem { get; set; }

        public string ScheduleTime { get; set; }

        public string Comment { get; set; }

        public string TeethName { get; set; }

        public string TeethCode { get; set; }

        public int TeethCount { get; set; }

        public string AccNo { get; set; }

        public string Status { get; set; }

        public string RemoteRPID { get; set; }

        public bool? IsCharge { get; set; }

        public string ContrastName { get; set; }

        public string ContrastDose { get; set; }

        public string Optional1 { get; set; }

        public string Optional2 { get; set; }

        public string Optional3 { get; set; }

        public List<RequestChargeDto> ChargeItems { get; set; }
    }
}
