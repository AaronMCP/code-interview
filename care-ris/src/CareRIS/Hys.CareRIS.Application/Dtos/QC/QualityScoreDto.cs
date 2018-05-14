using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    /// <summary>
    /// 质控评分
    /// </summary>
    public class QualityScoreDto
    {

        //tbQualityScoring
        public string UniqueID { get; set; }
        /// <summary>
        /// type:2 报告质量的xml 例子：<Items><Item>描述合适;</Item><Item>无错别字或符号;</Item><Item>报告完整;</Item><Item>结论准确;</Item></Items>
        /// type:1 报告质量的xml 例子：<Items><Item>描述合适;</Item><Item>无错别字或符号;</Item><Item>报告完整;</Item><Item>结论准确;</Item></Items>
        /// </summary>
        public string ResultItem { get; set; }
        /// <summary>
        /// type:1 合格
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// type:1 
        /// </summary>
        public string Result2 { get; set; }
        /// <summary>
        ///  Indexs
        /// </summary>
        public string Result3 { get; set; }
        /// <summary>
        /// type:1 ProcedureGuid
        /// </summary>
        public string AppraiseObject { get; set; }
        /// <summary>
        /// type:1 备注
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 1 for image,2 for report
        /// </summary>
        public int ScoringType { get; set; }
        //Order
        public string OrderID { get; set; }
        public string AccNo { get; set; }
        public string ApplyDept { get; set; }
        public string PatientType { get; set; }
        public DateTime? AssignTime { get; set; }
        //patient
        public string PatientID { get; set; }
        public string LocalName { get; set; }
        public string Gender { get; set; }
        public DateTime? Birthday { get; set; }

        //Procedure
        public string ProcedureID { get; set; }

        public int? Status { get; set; }
        public string ExamSystem { get; set; }

        public string Modality { get; set; }

        public string ModalityType { get; set; }
        public string RPDesc { get; set; }
        /// <summary>
        /// 状态名
        /// </summary>
        public string RPStatus { get; set; }
        /// <summary>
        /// user的LocalName
        /// </summary>
        public string Technician { get; set; }
        public string TechnicianID { get; set; }
        public string BodyPart { get; set; }
        /// <summary>
        /// ExamineDt
        /// </summary>
        public DateTime? ExamineTime { get; set; }
        public string CheckingItem { get; set; }

        //report
        public string ReportID { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? SubmitTime { get; set; }
        public DateTime? FirstApproveTime { get; set; }
        public string SubmitSite { get; set; }
        public string ScoringVersion { get; set; }
        /// <summary>
        /// type:2  评分结果
        /// </summary>
        public string ReportQuality { get; set; }
        /// <summary>
        /// type:2  报告质量 Indexs
        /// </summary>
        public string ReportQuality2 { get; set; }
        /// <summary>
        /// type:2  备注
        /// </summary>
        public string ReportQualityComments { get; set; }
        /// <summary>
        /// type:2 例:符合..
        /// </summary>
        public string AccordRate { get; set; }
        /// <summary>
        /// type是1时使用
        /// </summary>
        public List<string> ProcedIDs { get; set; }
        /// <summary>
        /// type是2时使用
        /// </summary>
        public List<string> ReportIDs { get; set; }
        /// <summary>
        ///user.Domain
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// ?评分人
        /// </summary>
        public string Appraiser { get; set; }
        public string PrintTemplateID { get; set; }
        public string StudyInstanceUID { get; set; }
    }
}
