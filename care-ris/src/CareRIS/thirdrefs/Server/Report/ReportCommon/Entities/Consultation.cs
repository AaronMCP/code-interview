using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCommon
{
    public class Consultation
    {
        public string cstGuid { get; set; }
        public string cstUserGuid { get; set; }
        public string cstSite { get; set; }
        public string cstOrderGuid { get; set; }
        public string cstStatus { get; set; }
        public string cstConsultHospital { get; set; }
        public string cstType { get; set; }
        public DateTime cstApplyTime { get; set; }
        public DateTime cstStartTime { get; set; }
        public DateTime cstEndTime { get; set; }
        public string cstExpert { get; set; }
    }

    public enum ConsultationRequestStatus
    {
        //[Description("预提交")]
        PreSubmitted = -1,
        //[Description("已提交")]
        Submitted = 0,
        //[Description("已取消")]
        Canceled = 1,
        //[Description("已受理")]
        Accepted = 2,
        //[Description("被拒绝")]
        Rejected = 3,
        //[Description("已完成")]
        Completed = 4,
        //[Description("已转发")]
        Forwarded = 5,
        //[Description("重新申请")]
        ReSubmit = 6,
    }
}
