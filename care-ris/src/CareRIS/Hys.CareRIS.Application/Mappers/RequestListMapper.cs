using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Hys.CareRIS.Application.Mappers
{
    public class RequestListMapper : Profile 
    {
        public RequestListMapper()
        {
            Configure();
        }

        public override string ProfileName
        {
            get { return this.GetType().Name; }
        }
        /// <summary>
        /// Convert LIST to XML string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        private string ConverListToXml<T>(List<T> list)
        {
            string xmlResult = string.Empty;
            if (list != null)
            {
                StringWriter stringWriter = new StringWriter();

                XmlDocument xmlDoc = new XmlDocument();

                XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);

                XmlSerializer serializer = new XmlSerializer(list.GetType());

                serializer.Serialize(xmlWriter, list);

                xmlResult = stringWriter.ToString();
            }
            return xmlResult;
        }

        private void Configure()
        {
            CreateMap<RequestList, RequestListDto>();
            CreateMap<RequestDto, RequestList>()  
                .ForMember(src => src.RequestItem, opt =>
                    opt.MapFrom(dto => (dto.RequestItems==null||dto.RequestItems.Count == 0) ? null : ConverListToXml<RequestItemDto>(dto.RequestItems)))
                .ForMember(src => src.RequestCharge, opt =>
                    opt.MapFrom(dto => (dto.ChargeItems == null || dto.ChargeItems.Count == 0) ? null : ConverListToXml<RequestChargeDto>(dto.ChargeItems)));
        }
    }
}
