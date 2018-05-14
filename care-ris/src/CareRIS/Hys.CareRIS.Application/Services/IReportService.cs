using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Hys.CareRIS.Application.Services
{
    public interface IReportService : IDisposable
    {
        ReportDto GetReport(string reportID);
        ReportDto GetReportByProcedureID(string procedureID);
        void AddReport(ReportDto reportDto);
        void UpdateReport(ReportDto reportDto);
        void UpdateComments(ReportDto reportDto);
        void DeleteReport(string reportID);
        ReportDto CreateReport(ReportDto reportDto, string userName, string userID, string domain, string site);
        void ModifyReport(ReportDto reportDto, string userName, string userID, string domain, string site);
        IEnumerable<ReportFileDto> GetReportFilesByReportID(string reportID);
        IEnumerable<ReportListDto> GetReportListByReportID(string reportID);
        void DeleteReport(ReportDto reportDto);
        IEnumerable<ProcedureDto> GetProcedureByReportID(string reportID);
        IEnumerable<ProcedureDto> GetProcedureByOrderID(string orderID);

        List<RoleDto> GetAllRoleForReport();
        List<UserDto> GetUserForReportByRoleID(string roleID);
        string GetBaseInfoDescByProcedure(ProcedureDto procedure);

        List<ReportDto> GetOtherReportsByReportID(string id);
        List<ReportDto> GetOtherReportsByProcedureID(string id);

        string GetPacsUrl(string procedureID, string loginUserID);
        string GetPacsUrlDX(string procedureID, string loginUserID);

        List<string> GetProcedureIDsByReportID(string id);
        List<string> GetProcedureIDsByOrderID(string id);
        List<string> GetExamedProcedureIDsByOrderID(string id);

        string GetBaseInfoDescByProcedureID( string id);
        string GetBaseInfoDescByOrderID(string id);
        ProcedureDto GetIntegerProcedureByReportID(string id);

        OrderLiteDto GetOrderByProcedureID(string id);
        bool GetImageStatusByOrderId(string orderId);
    }
}
