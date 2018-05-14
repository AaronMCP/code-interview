using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class RoleDir : Entity
    {
        public override object UniqueId
        {
            get
            {
                return UniqueID;
            }
        }

        public string UniqueID { get; set; }
        public string Name { get; set; }
        public string ParentID { get; set; }
        public string RoleID { get; set; }

        public int? Leaf { get; set; }
        public int? OrderID { get; set; }
        public string Domain { get; set; }
    }
}
