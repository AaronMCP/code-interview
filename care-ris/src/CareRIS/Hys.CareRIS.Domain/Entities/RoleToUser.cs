using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class RoleToUser : Entity
    {
        public override object UniqueId
        {
            get
            {
                return RoleName + UserID + Domain;
            }
        }
        public string RoleName { get; set; }
        public string UserID { get; set; }
        public string Domain { get; set; }
    }
}
