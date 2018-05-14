using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    /// <summary>
    /// 质控合并检查
    /// </summary>
    public class MergeObjectDto
    {
        //合并病人

        /// <summary>
        /// 目标病人
        /// </summary>
        public string TargetPatientID { get; set; }
        /// <summary>
        /// 被合并病人
        /// </summary>
        public string SrcPatientID { get; set; }
        /// <summary>
        /// 多个病人合并到一个
        /// </summary>
        public Dictionary<string, string> SrcPatientIDs{ get; set; }
        /// <summary>
        /// 合并后是否删除
        /// </summary>
        public bool AfterDelPatient { get; set; }

        //移动检验
        //TargetPatientID
        //SrcOrderID

        //合并检验
        /// <summary>
        /// 被移动检查的id
        /// </summary>
        public string SrcOrderID { get; set; }
        /// <summary>
        /// 目标申请单
        /// </summary>
        public string TargetOrderID { get; set; }

        //移动检查项目
        public string SrcProcedureID { get; set; }
        /// <summary>
        /// 目标申请单
        /// </summary>
        public string TargetProcedureID { get; set; }

        /// <summary>
        /// 是否合并申请单
        /// </summary>
        public bool IsMergeRequisition { get; set; }
        /// <summary>
        /// 是否合并费用
        /// </summary>
        public bool IsMergeOrderCharge { get; set; }



    }
}
