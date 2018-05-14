using System;

namespace Hys.CareRIS.Application.Dtos
{
    public class PaginationDto
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool HasNextPage { get; set; }
        public int? TotalCount { get; set; }
        public int? TotalPages { get; set; }

        // always false, only special case set this property as true. e.g: bulk print.
        public bool NeedNoPagination { get; set; }
    }
}
