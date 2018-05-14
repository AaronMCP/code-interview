using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class ReportListDto
    {
        public string UniqueID { get; set; }
        public string ReportID { get; set; }
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
        public bool DeleteMark { get; set; }
        //public string Deleter { get; set; }
        //public DateTime? DeleteTime { get; set; }
        //public string Recuperator { get; set; }
        //public DateTime? ReconvertDt { get; set; }
        public string Mender { get; set; }
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? IsPrint { get; set; }
        public DateTime? OperationTime { get; set; }

       
        public string WYSText { get; set; }
        public string WYGText { get; set; }
        
        public string Domain { get; set; }
       
        public string SubmitterName { get; set; }
      
        public string CreaterName { get; set; }
        public string MenderName { get; set; }
        public string TechInfo { get; set; }
       
    }
}
