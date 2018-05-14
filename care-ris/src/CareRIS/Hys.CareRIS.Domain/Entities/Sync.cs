namespace Hys.CareRIS.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Sync : Entity
    {
        public override object UniqueId
        {
            get
            {
                return OrderID + SyncType;
            }
        }

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
    }
}
