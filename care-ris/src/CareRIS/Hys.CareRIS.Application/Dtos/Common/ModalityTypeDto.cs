using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class ModalityTypeDto
    {
        public Guid UniqueId { get; set; }
        public string ModalityType { get; set; }
        public string SOPClass { get; set; }
        public string Domain { get; set; }
        public string Site { get; set; }
        //
        public bool HasChildren { get; set; }
        public List<ModalityDto> Childrens { get; set; }
        

    }
}
