using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class QualityScoreResultDto
    {
        public PaginationDto PaginationDto { get; set; }

        public IEnumerable<QualityScoreDto> QualityScoreInfo { get; set; }
    }
}
