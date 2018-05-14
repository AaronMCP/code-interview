using Hys.Consultation.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hys.Consultation.EntityFramework
{
    public interface IInitialDataService
    {
        void InitialData(ConsultationContext context, bool isV1Initialed);
    }
}
