using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ProcedureCodeDto
    {
        public string UniqueId { get; set; }
        public string ProcedureCode { get; set; }
        public string ModalityType { get; set; }
        public string BodyPart { get; set; }
        public string CheckingItem { get; set; }

        public string Modality { get; set; }
        public string BodyCategory { get; set; }

        public string FilmSpec { get; set; }
        public int? FilmCount { get; set; }

        public string ContrastName { get; set; }
        public string ContrastDose { get; set; }
        public int? ImageCount { get; set; }
        public int? ExposalCount { get; set; }
        public decimal? Charge { get; set; }
        public string BookingNotice { get; set; }
        public string DefaultModality { get; set; }
        public string Description { get; set; }

        #region 检查代码配置
        /// <summary>
        /// 检查时间
        /// </summary>
        public int? Duration { get; set; }
        /// <summary>
        /// 是否增强
        /// </summary>
        public int? Enhance { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int? Effective { get; set; }
        public int TechnicianWeight { get; set; }
        public int? CheckingItemFrequency { get; set; }
        public int RadiologistWeight { get; set; }
        public int ApprovedRadiologistWeight { get; set; }
        public string ShortcutCode { get; set; }
        public string Preparation { get; set; }
        public string EnglishDescription { get; set; }
        public int? Frequency { get; set; }
        public int Puncture { get; set; }
        public int Radiography { get; set; }

        public string Domain { get; set; }
        public int? BodypartFrequency { get; set; }
        public string Site { get; set; }
        public string ExamSystem { get; set; }


        #endregion



    }

    public class PorcedureCodeSiteDto
    {
        public List<string> ModalityTypes{get;set;}
        public List<ProcedureCodeDto> procedureCodes { get; set; }
    }
}
