using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class Role : Entity
    {
        public override object UniqueId
        {
            get
            {
                return RoleName;
            }
        }

        public string UniqueID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int? IsSystem { get; set; }
        public string Domain { get; set; }
    }
}
