using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class SearchCriteriaShortcutDto
    {
        public string UniqueID { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        // user id, if empty, means global
        public string Owner { get; set; }
        public string Domain { get; set; }

        public WorklistSearchCriteriaDto criteria { get; set; }
        public bool IgnoreNameDuplicated { get; set; }
    }
}
