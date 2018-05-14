using System;
using AutoMapper;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Dtos
{
   
    public class SearchCriteriaDto
    {
        public string Code { get; set; }
        public PaginationDto Pagination { get; set; }
    }
}