using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class SyncDto
    {
        public string OrderID { get; set; }
        public int SyncType { get; set; }
        public string Owner { get; set; }
        public string OwnerIP { get; set; }
        public DateTime? CreateTime { get; set; }
        public string ModuleID { get; set; }

        public string PatientID { get; set; }
        public string PatientName { get; set; }
        public string AccNo { get; set; }
        public int? Counter { get; set; }
        public string ProcedureIDs { get; set; }
        public string Domain { get; set; }
        //
        public string OwnerName { get; set; }
        public string LoginName { get; set; }
        public string ModuleTitle { get; set; }
    }
}
