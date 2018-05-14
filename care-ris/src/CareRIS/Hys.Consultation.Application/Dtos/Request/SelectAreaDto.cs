using Hys.CrossCutting.Common.Interfaces;
using System.Collections.Generic;

namespace Hys.Consultation.Application.Dtos
{
    public class SelectAreaDto
    {
        public string ProvinceName { get; set; }
        public List<string> CityNames { get; set; }
    }

}
