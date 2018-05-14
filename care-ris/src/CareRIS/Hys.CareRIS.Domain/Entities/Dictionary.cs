using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class Dictionary : Entity
    {
        public override object UniqueId { get { return Tag; } }

        public int Tag { get; set; }
        public string Name { get; set; }
        public int? IsHidden { get; set; }
        public string Description { get; set; }
        public string Domain { get; set; }
    }
}
