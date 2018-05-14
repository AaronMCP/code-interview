using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace Hys.CareRIS.Application.Services
{
    public interface IReportTemplateService : IDisposable
    {
        ReportTemplateDirecDto GetReportTemplateDirecByID(string uniqueID);
        IEnumerable<ReportTemplateDirecDto> GetReportTemplateNodes(string parentID, string userID, string site);
        ReportTemplateDto GetReportTemplate(string templateID);
        ReportTemplateDto CreateReportTemplate(ReportTemplateDto reportTemplateDto, string userID);
        ReportTemplateDto UpdateReportTemplate(ReportTemplateDto reportTemplateDto, string userID);
        bool DeleteTemplateByID(string id);
        bool IsDuplicatedTemplateName(ReportTemplateDto reportTemplateDto, string userID);

        /// <summary>
        /// 共有模板新增
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        ReportTemplateDto CreatePublicReportTemplate(ReportTemplateDto reportTemplateDto, string userID);
        /// <summary>
        /// 共有模板修改
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        ReportTemplateDto UpdatePublicReportTemplate(ReportTemplateDto reportTemplateDto, string userID);
        /// <summary>
        /// 新增目录
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        ReportTemplateDirecDto CreateReportTemplateDirec(ReportTemplateDirecDto reportTemplateDto, string userID);

        /// <summary>
        /// 修改目录
        /// </summary>
        /// <param name="templateDirecDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        ReportTemplateDirecDto UpdateReportTemplateDirec(ReportTemplateDirecDto templateDirecDto, string userID);
        /// <summary>
        /// 目录上移
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool NodeItemOrderUp(string id);
        /// <summary>
        /// 目录下移
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool NodeItemOrderDown(string id);

        /// <summary>
        /// 验证模板
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        bool CreateReportTemplateExist(ReportTemplateDirecDto reportTemplateDto, string userID);

    }
}
