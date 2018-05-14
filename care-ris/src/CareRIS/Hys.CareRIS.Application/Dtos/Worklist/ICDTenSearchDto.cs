using System.Collections.Generic;
namespace Hys.CareRIS.Application.Dtos
{
    public class ICDTenSearchDto 
    {
        public List<ICDTenDto> Cases { get; set; }
        public PaginationDto PaginationDto { get; set; }
    }
}
