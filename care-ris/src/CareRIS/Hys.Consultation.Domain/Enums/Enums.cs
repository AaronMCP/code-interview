using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hys.Consultation.Domain.Enums
{
    public enum DictionaryType
    {
        ConsultationStauts = 1,
        TimeRange = 2,
        RejectReason = 3,
        CancaleReason = 4,
        ApplyReconsiderReason = 5,
        TerminateReason = 6,
        ApplyCancelReason = 7
    }

    public enum ReceiverType
    {
        Center = 0,
        Expert = 1
    }
}
