using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class GWOrderDto
    {
        public string UniqueID { get; set; }
        public DateTime? DataTime { get; set; }
        public string OrderNo { get; set; }
        public string PlacerNo { get; set; }
        public string FillerNo { get; set; }
        public string SeriesNo { get; set; }
        public string PatientID { get; set; }
        public string ExamStatus { get; set; }
        public string PlacerDepartment { get; set; }
        public string Placer { get; set; }
        public string PlacerContact { get; set; }
        public string FillerDepartment { get; set; }
        public string Filler { get; set; }
        public string FillerContact { get; set; }
        public string RefOrganization { get; set; }
        public string RefPhysician { get; set; }
        public string RefContact { get; set; }
        public string RequestReason { get; set; }
        public string ReuqestComments { get; set; }
        public string ExamRequirement { get; set; }
        public string ScheduledTime { get; set; }
        public string Modality { get; set; }
        public string StationName { get; set; }
        public string StationAETitle { get; set; }
        public string ExamLocation { get; set; }
        public string ExamVolume { get; set; }
        public string ExamTime { get; set; }
        public string Duration { get; set; }
        public string TransportArrange { get; set; }
        public string Technician { get; set; }
        public string BodyPart { get; set; }
        public string ProcedureName { get; set; }
        public string ProcedureCode { get; set; }
        public string ProcedureDesc { get; set; }
        public string StudyInstanceUID { get; set; }
        public string StudyID { get; set; }
        public string RefClassUID { get; set; }
        public string ExamComment { get; set; }
        public string CNTAgent { get; set; }
        public string ChargeStatus { get; set; }
        public string ChargeAmount { get; set; }
        public string Customer1 { get; set; }
        public string Customer2 { get; set; }
        public string Customer3 { get; set; }
        public string Customer4 { get; set; }
    }
}
