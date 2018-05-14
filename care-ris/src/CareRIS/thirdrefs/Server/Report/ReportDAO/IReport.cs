using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.ActionResult;

namespace Server.ReportDAO
{
    public interface IConsultation
    {
        bool save(ReportCommon.Consultation consultation);
    }

    public interface IReferral
    {
        DataSet getMemo(string patientId, string accNo);
    }
}
