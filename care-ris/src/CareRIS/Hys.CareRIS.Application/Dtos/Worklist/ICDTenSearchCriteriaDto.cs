using System;
using Hys.CrossCutting.Common.Interfaces;

namespace Hys.CareRIS.Application.Dtos
{
    public class ICDTenSearchCriteriaDto
    {
        public string ID { get; set; }
        public string NAME { get; set; }
        public string PY { get; set; }
        public string WB { get; set; }

        public string MEMO { get; set; }

        public PaginationDto PaginationDto { get; set; }
    }
}
