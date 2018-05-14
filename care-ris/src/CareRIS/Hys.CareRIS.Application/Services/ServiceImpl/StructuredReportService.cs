using AutoMapper;
using Hys.CareRIS.Application.Dtos;
using Hys.Platform.Application;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class StructuredReportService : DisposableServiceBase, IStructuredReportService
    {
        private IProcedureCodeRepository _ProcedureCodeRepository;
        private IModalityTypeRepository _ModalityTypeRepository;
        private IRisProContext _dbContext;

        public StructuredReportService(IProcedureCodeRepository procedurecodeRepository,
            IModalityTypeRepository modalityTypeRepository, IRisProContext dbContext)
        {
            _ProcedureCodeRepository = procedurecodeRepository;

            _ModalityTypeRepository = modalityTypeRepository;
            _dbContext = dbContext;

            AddDisposableObject(procedurecodeRepository);
            AddDisposableObject(modalityTypeRepository);
            AddDisposableObject(dbContext);
        }

        public IEnumerable<ProcedureCodeDto> GetProcedureCodes()
        {
            var procedurecodes = _ProcedureCodeRepository.Get().Select(p => Mapper.Map<Procedurecode, ProcedureCodeDto>(p)).ToList();
            return procedurecodes;
        }

        public IEnumerable<string> GetModalityTypes()
        {
            var modalityTypes = _ModalityTypeRepository.Get().Select(p => p.Modalitytype).Distinct().ToList();
            return modalityTypes;
        }

        public IEnumerable<string> GetBodyParts()
        {
            var bodyParts = _ProcedureCodeRepository.Get().Select(p => p.BodyPart).Distinct().ToList();
            CultureInfo PronoCi = new CultureInfo(2052);

            Thread.CurrentThread.CurrentCulture = PronoCi;

            bodyParts.Sort();
            return bodyParts;
        }

        /// <summary>
        /// Get Ris ReportTemplate Id
        /// </summary>
        /// <param name="templatename"></param>
        /// <returns></returns>
        public string GetRisReportTemplateId(string templatename)
        {
            string templateid = string.Empty;
            try
            {
                var template = _dbContext.Set<ReportTemplate>().FirstOrDefault(t => t.TemplateName == templatename);
                if (template != null)
                {
                    templateid = template.UniqueID;
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return templateid;
        }

        /// <summary>
        /// Get Ris ReportTemplatedirc ParentId
        /// </summary>
        /// <param name="modalitytype"></param>
        /// <param name="bodypart"></param>
        /// <returns></returns>
        private ReportTemplateDirec GetRisReportTemplateDircParentId(string modalitytype, string bodypart)
        {
            string modalitytypeid = string.Empty;
            string parentid = string.Empty;
            try
            {
                var reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().FirstOrDefault(t => t.ItemName == modalitytype && t.ParentID == "GlobalTemplate");

                var domain= _dbContext.Set<ReportTemplateDirec>().FirstOrDefault(t => t.ParentID == "GlobalTemplate");

                if (reportTemplateDirec != null)
                {
                    modalitytypeid = reportTemplateDirec.UniqueID;
                }
                else
                {
                    var tmpdirc = new ReportTemplateDirec();

                    _dbContext.Set<ReportTemplateDirec>().Add(tmpdirc);
                    tmpdirc.ItemName = modalitytype;
                    tmpdirc.UniqueID = Guid.NewGuid().ToString();
                    tmpdirc.ParentID = "GlobalTemplate";
                    tmpdirc.Depth = 0;
                    tmpdirc.ItemOrder = 0;
                    tmpdirc.Leaf = 0;
                    tmpdirc.TemplateID = "";
                    tmpdirc.DirectoryType = "report";
                    if (domain != null) tmpdirc.Domain = domain.Domain;

                    _dbContext.SaveChanges();

                    modalitytypeid = tmpdirc.UniqueID;
                }

                var reportTemplateDirec2 = _dbContext.Set<ReportTemplateDirec>().FirstOrDefault(t => t.ItemName == bodypart && t.ParentID == modalitytypeid);

                if (reportTemplateDirec2 != null)
                {
                    return reportTemplateDirec2;
                }
                else
                {
                    var temp = _dbContext.Set<ReportTemplateDirec>().FirstOrDefault(t => t.ItemName == modalitytype && t.ParentID == "GlobalTemplate");

                    var tmpdirc = new ReportTemplateDirec();

                    _dbContext.Set<ReportTemplateDirec>().Add(tmpdirc);
                    tmpdirc.ItemName = bodypart;
                    tmpdirc.UniqueID = Guid.NewGuid().ToString();
                    tmpdirc.ParentID = temp.UniqueID;
                    tmpdirc.Depth = 0;
                    tmpdirc.ItemOrder = 0;
                    tmpdirc.Leaf = 0;
                    tmpdirc.TemplateID = "";
                    tmpdirc.DirectoryType = "report";
                    if (domain != null) tmpdirc.Domain = domain.Domain;

                    _dbContext.SaveChanges();

                    return _dbContext.Set<ReportTemplateDirec>().FirstOrDefault(t => t.ItemName == bodypart && t.ParentID == modalitytypeid);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private int? GetRisReportTemplateDirecMaxOrderNumber(string parentid)
        {
            int? maxnumber = 0;

            var tmp = _dbContext.Set<ReportTemplateDirec>()
                .Where(t => t.ParentID == parentid)
                .OrderByDescending(t => t.ItemOrder)
                .FirstOrDefault();

            if (tmp != null) maxnumber = tmp.ItemOrder ?? 0;

            return maxnumber;
        }

        /// <summary>
        ///  Save SR report template into ris report tempalte
        /// </summary>
        /// <param name="template"></param>
        public void AddToRisTemplate(ReportTemplateDto template)
        {
            try
            {
                var tmpdirc = new ReportTemplateDirec();
                var tmp = new ReportTemplate();
                var dirc = GetRisReportTemplateDircParentId(template.ModalityType, template.BodyPart);

                if (dirc != null)
                {
                    _dbContext.Set<ReportTemplateDirec>().Add(tmpdirc);
                    tmpdirc.ItemName = template.TemplateName;
                    tmpdirc.UniqueID = Guid.NewGuid().ToString();
                    tmpdirc.ParentID = dirc.UniqueID;
                    tmpdirc.Depth = 0;
                    tmpdirc.ItemOrder = GetRisReportTemplateDirecMaxOrderNumber(dirc.UniqueID) + 1;
                    tmpdirc.Leaf = 1;
                    tmpdirc.TemplateID = Guid.NewGuid().ToString();
                    tmpdirc.DirectoryType = "report";
                    tmpdirc.Domain = dirc.Domain;
                    tmpdirc.Type = 0;

                    _dbContext.Set<ReportTemplate>().Add(tmp);
                    tmp.UniqueID = tmpdirc.TemplateID;
                    tmp.TemplateName = template.TemplateName;
                    tmp.ModalityType = template.ModalityType;
                    tmp.BodyPart = template.BodyPart;
                    tmp.WYG = new byte[0];
                    tmp.WYS = new byte[0];
                    tmp.AppendInfo = new byte[0];
                    tmp.TechInfo = new byte[0];
                    tmp.CheckItemName = "";
                    tmp.BodyCategory = "";
                    tmp.Domain = tmpdirc.Domain;
                    tmp.Gender = "";
                    tmp.IsPositive = 0;

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        public void UpdateToRisTemplate(ReportTemplateDto template)
        {
            try
            {
                var tmpdirc = new ReportTemplateDirec();
                var tmp = new ReportTemplate();
                var dirc = GetRisReportTemplateDircParentId(template.ModalityType, template.BodyPart);

                tmpdirc = _dbContext.Set<ReportTemplateDirec>().FirstOrDefault(t => t.TemplateID == template.UniqueID);
                if (tmpdirc == null)
                {
                    AddToRisTemplate(template);
                }
                else
                {
                    tmpdirc.ItemName = template.TemplateName;
                    tmpdirc.ParentID = dirc.UniqueID;

                    tmp = _dbContext.Set<ReportTemplate>().FirstOrDefault(t => t.UniqueID == tmpdirc.TemplateID);
                    tmp.TemplateName = template.TemplateName;
                    tmp.BodyPart = template.BodyPart;
                    tmp.ModalityType = template.ModalityType;

                    _dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                // ignored
            }
        }

        /// <summary>
        /// Delete reporttemplate when sr report deleted
        /// </summary>
        /// <param name="templatename"></param>
        public void DeleteToRisTemplate(string templatename)
        {
            try
            {
                var rpt = _dbContext.Set<ReportTemplate>().FirstOrDefault(p => p.TemplateName == templatename);

                _dbContext.Set<ReportTemplate>().Remove(rpt);

                var rptd = _dbContext.Set<ReportTemplateDirec>().FirstOrDefault(p => p.ItemName == templatename);

                _dbContext.Set<ReportTemplateDirec>().Remove(rptd);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}