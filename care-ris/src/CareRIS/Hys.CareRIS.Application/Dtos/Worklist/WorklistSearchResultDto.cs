using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
    public class WorklistSearchResultDto
    {
        public PaginationDto Pagination { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
    }
}
