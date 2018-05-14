namespace Hys.CareRIS.Domain.Entities
{
    using Hys.Platform.Domain;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Order : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }

        public string PatientID { get; set; }

        public string VisitID { get; set; }

        public string AccNo { get; set; }

        public string ApplyDept { get; set; }

        public string ApplyDoctor { get; set; }

        public DateTime? CreateTime { get; set; }

        public int? IsScan { get; set; }

        public string Comments { get; set; }

        /// <summary>
        /// For intergation wite other sys
        /// </summary>
        public string RemoteAccNo { get; set; }

        /// <summary>
        /// total fee
        /// </summary>
        public decimal? TotalFee { get; set; }

        public string Optional1 { get; set; }

        public string Optional2 { get; set; }

        public string Optional3 { get; set; }

        /// <summary>
        /// PACS 
        /// </summary>
        public string StudyInstanceUID { get; set; }

        /// <summary>
        /// In HIS(maybe more  number)
        /// </summary>
        public string HisID { get; set; }

        public string CardNo { get; set; }

        public string InhospitalNo { get; set; }

        public string ClinicNo { get; set; }

        public string PatientType { get; set; }

        public string Observation { get; set; }

        public string HealthHistory { get; set; }

        public string InhospitalRegion { get; set; }

        public int? IsEmergency { get; set; }

        public string BedNo { get; set; }

        public string CurrentAge { get; set; }

        public int? AgeInDays { get; set; }

        public string VisitComment { get; set; }

        public string ChargeType { get; set; }

        public string ErethismType { get; set; }

        public string ErethismCode { get; set; }

        public string ErethismGrade { get; set; }

        public string Domain { get; set; }

        public string ReferralID { get; set; }

        public int? IsReferral { get; set; }

        public string ExamAccNo { get; set; }

        public string ExamDomain { get; set; }

        //public string MedicalAlert { get; set; }

        //public string EXAMALERT1 { get; set; }

        //public string EXAMALERT2 { get; set; }

       // public string LMP { get; set; }

        public string InitialDomain { get; set; }

        public string ERequisition { get; set; }

        public string CurPatientName { get; set; }

        public string CurGender { get; set; }

        public int? Priority { get; set; }

        public int? IsCharge { get; set; }

        /// <summary>
        /// validate wheather it has equipment on the bedside or not
        /// </summary>
        public int? IsBedside { get; set; }

        public int? IsFilmSent { get; set; }

        public string FilmSentOperator { get; set; }

        public DateTime? FilmSentTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderMessage { get; set; }

        public string BookingSite { get; set; }

        public string RegSite { get; set; }

        public string ExamSite { get; set; }

        public decimal? BodyWeight { get; set; }

        /// <summary>
        /// film fee
        /// </summary>
        public int? FilmFee { get; set; }

        /// <summary>
        /// validate  wheather it is 3D rebuild or not
        /// </summary>
        public int? IsThreeDRebuild { get; set; }

        public string CurrentSite { get; set; }

        public DateTime? AssignTime { get; set; }

        public string Assign2Site { get; set; }

        public string StudyID { get; set; }

        public string PathologicalFindings { get; set; }

        public string InternalOptional1 { get; set; }

        public string InternalOptional2 { get; set; }

        public string ExternalOptional1 { get; set; }

        public string ExternalOptional2 { get; set; }

        public string ExternalOptional3 { get; set; }

        //public DateTime? UpdateTime { get; set; }

        //public int? Uploaded { get; set; }

        public DateTime? TakeReportDate { get; set; }

        // nuclear medicine
        public string InjectDose { get; set; }

        public string InjectTime { get; set; }

        public string BodyHeight { get; set; }

        public string BloodSugar { get; set; }

        public string Insulin { get; set; }

        public string GoOnGoTime { get; set; }

        public string InjectorRemnant { get; set; }

        public string SubmitHospital { get; set; }

        public string SubmitDepartment { get; set; }

        public string SubmitDoctor { get; set; }
        //

        public int? EFilmNumber { get; set; }

        public int? TerminalCheckinPrintNumber { get; set; }

        public byte[] FilmDrawerSign { get; set; }

        public string FilmDrawDepartment { get; set; }

        public string FilmDrawRegion { get; set; }

        public string FilmDrawComment { get; set; }

        // HK
        public string LotNumber { get; set; }

        public string PhysicalCompany { get; set; }

        public bool? HeartDisease { get; set; }

        public bool? Hypertension { get; set; }

        public bool? Scoliosis { get; set; }

        public string ImagingExamSheets { get; set; }
    }
}
