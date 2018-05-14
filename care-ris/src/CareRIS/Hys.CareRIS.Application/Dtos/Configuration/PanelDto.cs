using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class PanelDto : SystemProfileDto
    {
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
