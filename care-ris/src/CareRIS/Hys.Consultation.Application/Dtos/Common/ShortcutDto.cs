using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Application.Dtos
{
    public class ShortcutDto
    {
        public string UniqueId { get; set; }
        public ShortcutCategory Category { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        // user id, if empty, means global
        public string Owner { get; set; }
        public bool IgnoreDuplicatedName { get; set; }
    }
}
