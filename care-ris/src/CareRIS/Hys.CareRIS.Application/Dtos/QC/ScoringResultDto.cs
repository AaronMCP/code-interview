using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    /// <summary>
    /// 评分结果
    /// </summary>
    public class ScoringResultDto
    {


        public string UniqueID { get; set; }
        public string ObjectGuid { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? Type { get; set; }
        public string Result { get; set; }
        public string AccordRate { get; set; }
        public string Appraiser { get; set; }
        public string Comment { get; set; }
        public string Domain { get; set; }
        public string Result2 { get; set; }
        public int? IsFinalVersion { get; set; }
        public string UserName { get; set; }



    }
}
