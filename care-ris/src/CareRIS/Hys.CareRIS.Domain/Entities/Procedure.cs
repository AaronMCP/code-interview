namespace Hys.CareRIS.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Procedure : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }

        public string OrderID { get; set; }

        public string ReportID { get; set; }

        /// <summary>
        /// external system ID
        /// </summary>
        public string RemoteRPID { get; set; }

        public string ProcedureCode { get; set; }

        public string ExamSystem { get; set; }

        public int WarningTime { get; set; }

        public string FilmSpec { get; set; }

        public int? FilmCount { get; set; }

        public string ContrastName { get; set; }

        public string ContrastDose { get; set; }

        public int? ImageCount { get; set; }

        public int? ExposalCount { get; set; }

        public decimal? Deposit { get; set; }

        public decimal? Charge { get; set; }

        public string ModalityType { get; set; }

        public string Modality { get; set; }

        /// <summary>
        /// RegistrarID
        /// </summary>
        public string Registrar { get; set; }

        public DateTime? RegisterTime { get; set; }

        //public int? Priority { get; set; }

        /// <summary>
        /// TechnicianID
        /// </summary>
        public string Technician { get; set; }

        public string TechDoctor { get; set; }

        public string TechNurse { get; set; }

        public string OperationStep { get; set; }

        public DateTime? ExamineTime { get; set; }

        public string Mender { get; set; }

        public DateTime? ModifyTime { get; set; }

        public int? IsPost { get; set; }

        public int? IsExistImage { get; set; }

        public int Status { get; set; }

        public string Comments { get; set; }

        public DateTime? BookingBeginTime { get; set; }

        public DateTime? BookingEndTime { get; set; }

        /// <summary>
        /// BookerID
        /// </summary>
        public string Booker { get; set; }

        public int? IsCharge { get; set; }

        public string Optional1 { get; set; }

        public string Optional2 { get; set; }

        public string Optional3 { get; set; }

        public string QueueNo { get; set; }

        public byte[] BookingNotice { get; set; }

        public string BookingTimeAlias { get; set; }

        public DateTime? CreateTime { get; set; }

        //public string MedicineUsage { get; set; }

        //public string Posture { get; set; }

        public string Technician1 { get; set; }

        public string Technician2 { get; set; }

        public string Technician3 { get; set; }

        public string Technician4 { get; set; }

        public string Domain { get; set; }

        //public string UnwrittenPreviousOwner { get; set; }

        //public string UnwrittenCurrentOwner { get; set; }

        //public DateTime? UnwrittenAssignDate { get; set; }

        //public string UnapprovedCurrentOwner { get; set; }

        //public string UnapprovedPreviousOwner { get; set; }

        //public DateTime? UnapprovedAssignDate { get; set; }

        public int? PreStatus { get; set; }

        public DateTime? UpdateTime { get; set; }

        //public int? Uploaded { get; set; }

        public string BookerName { get; set; }

        public string RegistrarName { get; set; }

        public string TechnicianName { get; set; }

        // table=>procedurecode
        public string BodyCategory { get; set; }

        public string BodyPart { get; set; }

        public string CheckingItem { get; set; }

        public string RPDesc { get; set; }

        public string ScanDelayTime { get; set; }

        public string CheckItemName { get; set; }
        //ÌåÎ»
        public string Posture { get; set; }
        public string MedicineUsage { get; set; }

    }
}
