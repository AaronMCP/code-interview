using System;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class SyncSearchCriteriaDto
    {

        public string OwnerName  { get; set; }
        public DateTime? CreateStartTime { get; set; }
        public DateTime? CreateEndTime { get; set; }

        public bool? HaveTime { get; set; }
        public PaginationDto Pagination { get; set; }
        
    }
    
}
