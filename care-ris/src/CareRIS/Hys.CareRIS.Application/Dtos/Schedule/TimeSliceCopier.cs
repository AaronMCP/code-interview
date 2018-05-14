using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.CareRIS.Application.Dtos
{
    public class TimeSliceCopier
    {
        public string[] SliceIds
        {
            get;
            set;
        }

        public DateTime AvailableDate
        {
            get;
            set;
        }

        public string Modality
        {
            get;
            set;
        }

        public int DateType
        {
            get;
            set;
        }
    }
}
