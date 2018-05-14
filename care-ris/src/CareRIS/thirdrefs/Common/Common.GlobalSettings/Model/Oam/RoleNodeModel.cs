using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonGlobalSettings
{
    public class RoleNodeModel : OamBaseModel
    {
        public string UniqueId
        {
            get;
            set;
        }

        public string ParentId
        {
            get;
            set;
        }

        public bool Leaf { get; set; }

        public int OrderId { get; set; }

        public string RoleID { get; set; }

        public string Domain { get; set; }

        public string Name { get; set; }

    }
}
