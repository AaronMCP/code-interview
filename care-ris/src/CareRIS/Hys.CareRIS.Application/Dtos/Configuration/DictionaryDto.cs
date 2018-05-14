using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class DictionaryDto
    {
        public DictionaryDto()
        {
            Values = new List<DictionaryValueDto>();
        }

        public int Tag { get; set; }
        public string Name { get; set; }
        public bool? IsHidden { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }

        public IEnumerable<DictionaryValueDto> Values { get; set; }
        public String value { get; set; }
    }
}
