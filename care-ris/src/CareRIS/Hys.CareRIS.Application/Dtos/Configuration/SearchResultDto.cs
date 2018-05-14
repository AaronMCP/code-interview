using System;
using AutoMapper;
using Hys.CareRIS.Domain.Entities;
using System.Collections.Generic;

namespace Hys.CareRIS.Application.Dtos
{
   
    public class SearchResultDto
    {
        public IEnumerable<ICDTenDto> Codes{ get; set; }
        public PaginationDto Pagination { get; set; }
    }
}