using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Services
{
    public interface ICommonService :IDisposable
    {
        IEnumerable<ProcedureCodeDto> GetProcedureCodes();
        IEnumerable<string> GetModalityTypes();
        IEnumerable<string> GetBodyParts();
        void WriteBroker(string dataID, PatientDto patient, OrderDto order, ProcedureDto procedure,
            ReportDto report, int eventType, int examStatus, int reportStatus, bool isDataSigned = false, string loginUserID = "");
        void WriteBroker(PatientDto patient, OrderDto order, ProcedureDto procedure,
            ReportDto report, string patientEventType, string examStatus, int actionCode, string reportStatus, bool isPatientBroker = false, bool isDataSigned = false, string loginUserID = "");
    }
}
