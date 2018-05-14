using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Domain.Entities;

namespace Hys.CareRIS.Application.Services
{
    public interface IStructuredReportService :IDisposable
    {
        IEnumerable<ProcedureCodeDto> GetProcedureCodes();
        IEnumerable<string> GetModalityTypes();
        IEnumerable<string> GetBodyParts();

        void AddToRisTemplate(ReportTemplateDto templatejson);
        void UpdateToRisTemplate(ReportTemplateDto templatejson);
        void DeleteToRisTemplate(string templatename);

        string GetRisReportTemplateId(string templatename);
    }
}
