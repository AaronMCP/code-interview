using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class Module : Entity
    {
        public override object UniqueId
        {
            get
            {
                return ModuleID;
            }
        }

        public string ModuleID { get; set; }
        public string Title { get; set; }
        public int? Parameter { get; set; }
        public int ImageIndex { get; set; }
        public int? OrderNo { get; set; }
        public string Domain { get; set; }
    }
}
