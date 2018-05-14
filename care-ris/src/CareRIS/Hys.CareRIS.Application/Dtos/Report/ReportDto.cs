using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class ReportDto
    {
        public string UniqueID { get; set; }
        public string ReportName { get; set; }
        public string WYS { get; set; }
        public string WYG { get; set; }
        public byte[] AppendInfo { get; set; }
        public string ReportText { get; set; }
        public string DoctorAdvice { get; set; }
        public int? IsPositive { get; set; }
        public string AcrCode { get; set; }
        public string AcrAnatomic { get; set; }
        public string AcrPathologic { get; set; }
        public string Creater { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Submitter { get; set; }
        public DateTime? SubmitTime { get; set; }
        public string FirstApprover { get; set; }
        public DateTime? FirstApproveTime { get; set; }
        public string SecondApprover { get; set; }
        public DateTime? SecondApproveTime { get; set; }
        public bool? IsDiagnosisRight { get; set; }
        public string KeyWord { get; set; }
        public string ReportQuality { get; set; }
        public string RejectToObject { get; set; }
        public string Rejecter { get; set; }
        public DateTime? RejectTime { get; set; }
        public int? Status { get; set; }
        public string Comments { get; set; }
        public bool? DeleteMark { get; set; }
        public string Deleter { get; set; }
        public DateTime? DeleteTime { get; set; }
        //public string Recuperator { get; set; }
        //public DateTime? ReconvertDt { get; set; }
        public string Mender { get; set; }
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsPrint { get; set; }
        public string CheckItemName { get; set; }
        public string Optional1 { get; set; }
        public string Optional2 { get; set; }
        public string Optional3 { get; set; }
        public bool? IsLeaveWord { get; set; }
        public string WYSText { get; set; }
        public string WYGText { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsDraw { get; set; }
        public byte[] DrawerSign { get; set; }
        public DateTime? DrawTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsLeaveSound { get; set; }
        public string TakeFilmDept { get; set; }
        public string TakeFilmRegion { get; set; }
        public string TakeFilmComment { get; set; }
        public int PrintCopies { get; set; }
        public string PrintTemplateID { get; set; }
        public string Domain { get; set; }
        public int? ReadOnly { get; set; }
        public string SubmitDomain { get; set; }
        public string RejectDomain { get; set; }
        public string FirstApproveDomain { get; set; }
        public string SecondApproveDomain { get; set; }
        //public byte[] ReportTextApprovedSign { get; set; }
        //public byte[] ReportTextSubmittedSign { get; set; }
        //public byte[] CombinedForCertification { get; set; }
        //public byte[] SignCombinedForCertification { get; set; }
        public string RejectSite { get; set; }
        public string SubmitSite { get; set; }
        public string FirstApproveSite { get; set; }
        public string SecondApproveSite { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? RebuildMark { get; set; }
        public string ReportQuality2 { get; set; }
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? Uploaded { get; set; }
        public string SubmitterName { get; set; }
        public string FirstApproverName { get; set; }
        public string SecondApproverName { get; set; }
        public string ReportQualityComments { get; set; }
        public string CreaterName { get; set; }
        public string MenderName { get; set; }
        public string TechInfo { get; set; }
        public int? TerminalReportPrintNumber { get; set; }
        public string ScoringVersion { get; set; }
        public string AccordRate { get; set; }
        //public string SubmitterSign { get; set; }
        //public string FirstApproverSign { get; set; }
        //public string SecondApproverSign { get; set; }
        //public string SubmitterSignTimeStamp { get; set; }
        //public string FirstApproverSignTimeStamp { get; set; }
        //public string SecondApproverSignTimeStamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsModified { get; set; }

        //procedureids
        public List<string> ProcedureIDs { get; set; }
    }
}
