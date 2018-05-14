using System;
using AutoMapper;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Dtos
{
   
    public class ICDTenDto
    {
        public string UniqueId { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string PY { get; set; }
        public string WB { get; set; }
        public string TJM { get; set; }
        public string BZLB { get; set; }
        public string ZLBM { get; set; }
        public string JLZT { get; set; }
        public string Memo { get; set; }
        public string Domain { get; set; }
    }
}