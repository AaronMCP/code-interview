using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class ACRCodeSubAnatomicalDto
    {

        public string UniqueID { get; set; }
        public string AnId { get; set; }
        public string SuId { get; set; }
        public int IsUserAdd { get; set; }
        public string Description { get; set; }
        public string DescriptionEn { get; set; }
        public string Domain { get; set; }
    }
}
