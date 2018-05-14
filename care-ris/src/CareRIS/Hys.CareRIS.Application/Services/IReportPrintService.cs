using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Hys.CareRIS.Application.Services
{
    public interface IReportPrintService : IDisposable
    {
        bool UpdateReportPrintTemplate(string reportID, string printTemplateID);
        PrintTemplateDto GetPrintTemplate(string printTemplateID);
        List<PrintTemplateDto> GetPrintTemplateByCriteria(PrintTemplateDto criteria, string domain, string site);
        PrintTemplateFieldsDto GetPrintTemplateFields(string uniqueID);
        string GetReportPrintTemplateID(string reportID, string loginDomain, string loginSite);
        string GetReportPrintUrl(string genPDFServiceURL, string reportID);
        bool UpdateReportPrintStatusByProcedureID(string procedureID, string printer, string site, string domain);
        PrintDataDto GetOtherReportPrintData(string accno, string modalityType, string templateType, string site);
        string GetOtherReportPrintID(string accno, string modalityType, string templateType, string site);

        DataTable GetBaseInfoByProcedure(ProcedureDto procedure);
        string GetBaseInfoHtmlByProcedure(ProcedureDto procedure, string domain, string site);
        string GetBaseInfoByOrderID(string id, string domain, string site);
        string GetBaseInfoByProcedureID(ProcedureDto procedureDto, string id, string domain, string site);
        string GetReportViewer(string reportID, string loginDomain, string loginSite);
        ShowHtmlDataDto GetReportViewer2(string reportID, string loginDomain, string loginSite, string printTemplateID);

    }
}
