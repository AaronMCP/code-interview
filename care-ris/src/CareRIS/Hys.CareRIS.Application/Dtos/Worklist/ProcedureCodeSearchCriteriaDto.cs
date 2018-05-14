using System;
using Hys.CrossCutting.Common.Interfaces;

namespace Hys.CareRIS.Application.Dtos
{
    public class ProcedureCodeSearchCriteriaDto
    {
        ///设备类型
        public string ModalityType { get; set; }
        /// <summary>
        /// 检查部位
        /// </summary>
        public string BodyPart { get; set; }
        /// <summary>
        /// 检查名
        /// </summary>
        public string CheckingItem { get; set; }
        /// <summary>
        /// 部位分类
        /// </summary>
        public string BodyCategory { get; set; }

        public PaginationDto PaginationDto { get; set; }
    }
}
