using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class ModalityType : Entity
    {
        public string UniqueID { get; set; }
        public string Modalitytype { get; set; }
        public string Sopclass { get; set; }
        public string Domain { get; set; }
        public string Site { get; set; }
    }
}
