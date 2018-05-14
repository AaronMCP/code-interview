using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class IDMaxValue : Entity
    {
        public override object UniqueId { get { return Tag.ToString() + "," + Value.ToString() + Domain + Site; } }

        public int Tag { get; set; }
        public int Value { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Domain { get; set; }
        public string Site { get; set; }
        public string ModalityType { get; set; }
        public string LocationAccNoPrefix { get; set; }
    }
}
