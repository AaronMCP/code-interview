using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class TimesliceSharer
    {
        public string GroupId { get; set; }

        /// <summary>
        /// Site name for sharing
        /// </summary>
        public string ShareTarget { get; set; }

        public int TargetType { get; set; }
        public int MaxCount { get; set; }
    }
}
