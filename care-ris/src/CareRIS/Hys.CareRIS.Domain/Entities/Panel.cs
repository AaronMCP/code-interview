using Hys.Platform.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Domain.Entities
{
    public partial class Panel : Entity
    {
        public override object UniqueId
        {
            get
            {
                return PanelID;
            }
        }
        public string PanelID { get; set; }
        public string ModuleID { get; set; }
        public string Title { get; set; }
        public string AssemblyQualifiedName { get; set; }
        public string Flag { get; set; }
        public int? Parameter { get; set; }
        public int? ImageIndex { get; set; }
        public int? OrderNo { get; set; }
        public int PanelKey { get; set; }
        public string Domain { get; set; }
    }
}
