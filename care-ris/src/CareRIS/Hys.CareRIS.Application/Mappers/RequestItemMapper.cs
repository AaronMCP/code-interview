﻿using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;

namespace Hys.CareRIS.Application.Mappers
{
    public class RequestItemMapper : Profile 
    {
        public RequestItemMapper()
        {
            Configure();
        }
        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }

        private void Configure()
        {
            CreateMap<RequestItem, RequestItemDto>();
            
            CreateMap<RequestItemDto, RequestItem>();
            
        }
    }
}
