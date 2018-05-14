using AutoMapper;
using C1.C1Report;
using Hys.CareRIS.Application.Dtos;
using Hys.CareRIS.Application.Dtos.Report;
using Hys.CareRIS.Application.Dtos.UserManagement;
using Hys.CareRIS.Application.Mappers;
using Hys.CareRIS.EnterpriseLib;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.Application;
using Hys.Platform.Domain.Ris;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.Domain.Interface;
using Hys.CareRIS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web.Security;
using System.Xml;
using Hys.Platform.CrossCutting.LogContract;

namespace Hys.CareRIS.Application.Services.ServiceImpl
{
    public class ReportTemplateService : DisposableServiceBase, IReportTemplateService
    {
        private IReportTemplateRepository _ReportTemplateRepository;
        private IReportTemplateDirecRepository _ReportTemplateDirecRepository;
        private IRisProContext _dbContext;

        public ReportTemplateService(
            IReportTemplateRepository reportTemplateRespository,
            IReportTemplateDirecRepository reportTemplateDirecRepository,
            IRisProContext dbContext)
        {
            _ReportTemplateRepository = reportTemplateRespository;
            _ReportTemplateDirecRepository = reportTemplateDirecRepository;
            _dbContext = dbContext;
            AddDisposableObject(reportTemplateRespository);
            AddDisposableObject(reportTemplateDirecRepository);
            AddDisposableObject(dbContext);
        }
        /// <summary>
        /// Get report template detail info by uniqueID
        /// </summary>
        /// <param name="uniqueID"></param>
        /// <returns></returns>
        public ReportTemplateDirecDto GetReportTemplateDirecByID(string uniqueID)
        {
            var reportTemplateDirec = _ReportTemplateDirecRepository.Get(
                p => p.UniqueID.Equals(uniqueID, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (reportTemplateDirec != null)
            {
                var reportTemplateDirecDto = Mapper.Map<ReportTemplateDirec, ReportTemplateDirecDto>(reportTemplateDirec);
                var reportTemplate = _ReportTemplateRepository.Get(p => p.UniqueID == reportTemplateDirec.TemplateID).FirstOrDefault();
                if (reportTemplate != null)
                {
                    var reportTemplateDto = Mapper.Map<ReportTemplate, ReportTemplateDto>(reportTemplate);
                    reportTemplateDirecDto.ReportTemplateDto = reportTemplateDto;
                    reportTemplateDirecDto.ReportTemplateDto.UserID = reportTemplateDirec.UserID;
                    reportTemplateDirecDto.ReportTemplateDto.Type = reportTemplateDirec.Type == null?1: (int)reportTemplateDirec.Type;
                }
                return reportTemplateDirecDto;
            }
            return null;
        }

        /// <summary>
        /// Get report template tree nodes by parentID and userID
        /// </summary>
        /// <param name="parentID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public IEnumerable<ReportTemplateDirecDto> GetReportTemplateNodes(string parentID, string userID, string site)
        {
            if (string.IsNullOrEmpty(parentID) || parentID == "undefined")
            {
                string siteName = "";
                if (!string.IsNullOrEmpty(site))
                {
                    var siteDto = _dbContext.Set<Site>().Where(p => p.SiteName.Equals(site, StringComparison.OrdinalIgnoreCase))
                        .ToList().Select(d => Mapper.Map<Site, SiteDto>(d)).FirstOrDefault();
                    if (siteDto != null)
                    {
                        siteName = siteDto.Alias;
                    }
                }

                var templateDirecList = new List<ReportTemplateDirecDto>();
                templateDirecList.Add(new ReportTemplateDirecDto
                {
                    UniqueID = "GlobalTemplate",
                    ItemName = "公有模版",
                    Type = 0
                });

                templateDirecList.Add(new ReportTemplateDirecDto
                {
                    UniqueID = site,
                    ItemName = siteName,
                    Type = 2
                });

                templateDirecList.Add(new ReportTemplateDirecDto
                {
                    UniqueID = "UserTemplate",
                    ItemName = "私有模版",
                    Type = 1
                });

                return templateDirecList;
            }
            else
            {

                //string sql = string.Format("select * from tbReportTemplateDirec where ParentID = '{0}' and (TYPE=0 OR TYPE=2 or (TYPE=1 AND UserGuid = '{1}'))  and [DirectoryType]='report' order by ItemOrder", strItemGuid, strUserGuid);
                var reportTemplateDirecs = _ReportTemplateDirecRepository.Get(
                    p => p.ParentID == parentID &&
                    (p.Type == 0 || p.Type == 2 || (p.Type == 1 && p.UserID == userID))
                    && p.DirectoryType == "report"
                    ).OrderBy(p => p.ItemOrder);
                if (reportTemplateDirecs.Count() != 0)
                {
                    var reportTemplateDirecDtos = new List<ReportTemplateDirecDto>();
                    foreach (var r in reportTemplateDirecs)
                    {
                        var reportTemplateDirecDto = Mapper.Map<ReportTemplateDirec, ReportTemplateDirecDto>(r);

                        reportTemplateDirecDtos.Add(reportTemplateDirecDto);
                    }
                    return reportTemplateDirecDtos;
                }

                return null;
            }
        }

        public ReportTemplateDto GetReportTemplate(string templateID)
        {
            var reportTemplate = _ReportTemplateRepository.Get(p => p.UniqueID == templateID).FirstOrDefault();
            if (reportTemplate != null)
            {
                var reportTemplateDto = Mapper.Map<ReportTemplate, ReportTemplateDto>(reportTemplate);
                return reportTemplateDto;
            }

            return null;
        }


        public ReportTemplateDto CreateReportTemplate(ReportTemplateDto reportTemplateDto, string userID)
        {
            reportTemplateDto.UniqueID = Guid.NewGuid().ToString();
            if (reportTemplateDto.IsPositive == null)
                reportTemplateDto.IsPositive = 0;

            ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UserID == userID && p.ItemName == reportTemplateDto.TemplateName).FirstOrDefault();
            if (reportTemplateDirec == null)
            {
                ReportTemplateDirec reportTemplateDirecNew = new ReportTemplateDirec();
                reportTemplateDirecNew.UniqueID = Guid.NewGuid().ToString();
                reportTemplateDirecNew.ParentID = "UserTemplate";
                reportTemplateDirecNew.TemplateID = reportTemplateDto.UniqueID;
                reportTemplateDirecNew.UserID = userID;
                reportTemplateDirecNew.ItemName = reportTemplateDto.TemplateName;
                reportTemplateDirecNew.Leaf = 1;
                reportTemplateDirecNew.DirectoryType = "report";
                reportTemplateDirecNew.Domain = reportTemplateDto.Domain;
                reportTemplateDirecNew.Type = 1;
                reportTemplateDirecNew.Depth = -1;
                reportTemplateDirecNew.ItemOrder = 0;

                List<ReportTemplateDirec> reportTemplateDirecList = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UserID == userID).OrderByDescending(p => p.ItemOrder).ToList();
                if (reportTemplateDirecList.Count > 0)
                {
                    if (reportTemplateDirecList[0].ItemOrder.HasValue)
                    {
                        reportTemplateDirecNew.ItemOrder = reportTemplateDirecList[0].ItemOrder + 1;
                    }
                }

                _dbContext.Set<ReportTemplate>().Add(Mapper.Map<ReportTemplateDto, ReportTemplate>(reportTemplateDto));
                _dbContext.Set<ReportTemplateDirec>().Add(reportTemplateDirecNew);
                _dbContext.SaveChanges();
            }
            else
            {
                return null;
            }

            ReportTemplate reportTemplate = _dbContext.Set<ReportTemplate>().Where(p => p.UniqueID == reportTemplateDto.UniqueID).FirstOrDefault();
            if (reportTemplate != null)
            {
                return Mapper.Map<ReportTemplate, ReportTemplateDto>(reportTemplate);
            }

            return null;


        }


        public ReportTemplateDto UpdateReportTemplate(ReportTemplateDto reportTemplateDto, string userID)
        {
            ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.TemplateID == reportTemplateDto.UniqueID).FirstOrDefault();
            if (reportTemplateDto.IsPositive == null)
                reportTemplateDto.IsPositive = 0;
            if (reportTemplateDirec != null)
            {
                reportTemplateDirec.ItemName = reportTemplateDto.TemplateName;
                ReportTemplateDirec reportTemplateDirecDup = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UserID == reportTemplateDirec.UserID && p.ItemName == reportTemplateDto.TemplateName).FirstOrDefault();
                if (reportTemplateDirecDup != null)
                {
                    if (reportTemplateDirecDup.UniqueID != reportTemplateDirec.UniqueID)
                    {
                        return null;
                    }
                }

                ReportTemplate reportTemplateOld = _dbContext.Set<ReportTemplate>().Where(p => p.UniqueID == reportTemplateDto.UniqueID).FirstOrDefault();
                if (reportTemplateOld != null)
                {
                    //reportTemplateOld.TemplateName = reportTemplateDto.TemplateName;
                    //reportTemplateOld.ModalityType = reportTemplateDto.ModalityType;
                    //reportTemplateOld.BodyPart = reportTemplateDto.BodyPart;
                    //reportTemplateOld.Gender = reportTemplateDto.Gender;
                    //reportTemplateOld.WYS = ReportMapper.GetBytes(reportTemplateDto.WYSText);
                    //reportTemplateOld.WYG = ReportMapper.GetBytes(reportTemplateDto.WYGText);
                   
                    reportTemplateOld.TemplateName = reportTemplateDto.TemplateName;         
                    reportTemplateOld.ModalityType = reportTemplateDto.ModalityType;
                    reportTemplateOld.BodyPart = reportTemplateDto.BodyPart;
                    reportTemplateOld.Gender = reportTemplateDto.Gender;
                    reportTemplateOld.WYS = ReportMapper.GetBytes(reportTemplateDto.WYSText == null ? "" : reportTemplateDto.WYSText);
                    reportTemplateOld.WYG = ReportMapper.GetBytes(reportTemplateDto.WYGText == null ? "" : reportTemplateDto.WYSText);

                    reportTemplateOld.ShortcutCode = reportTemplateDto.ShortcutCode == null ? reportTemplateOld.ShortcutCode : reportTemplateDto.ShortcutCode;
                    reportTemplateOld.ACRCode = reportTemplateDto.ACRCode == null ? reportTemplateOld.ACRCode : reportTemplateDto.ACRCode;
                    reportTemplateOld.DoctorAdvice = reportTemplateDto.DoctorAdvice == null ? reportTemplateOld.DoctorAdvice : reportTemplateDto.DoctorAdvice;
                    reportTemplateOld.BodyCategory = reportTemplateDto.BodyCategory == null ? reportTemplateOld.BodyCategory : reportTemplateDto.BodyCategory;
                    reportTemplateOld.IsPositive = reportTemplateDto.IsPositive == null ? reportTemplateOld.IsPositive : reportTemplateDto.IsPositive;
                    reportTemplateOld.CheckItemName = string.IsNullOrEmpty(reportTemplateDto.CheckItemName)? reportTemplateOld.CheckItemName: reportTemplateDto.CheckItemName;
                   
                    _dbContext.SaveChanges();
                }

            }

            ReportTemplate reportTemplate = _dbContext.Set<ReportTemplate>().Where(p => p.UniqueID == reportTemplateDto.UniqueID).FirstOrDefault();
            if (reportTemplate != null)
            {
                return Mapper.Map<ReportTemplate, ReportTemplateDto>(reportTemplate);
            }

            return null;
        }

        private List<ReportTemplateDirec> descendantNode(string ancestorId)
        {
            var resultList = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == ancestorId).ToList();
            if (resultList.Count > 0)
            {
                var childrenNodes = new List<ReportTemplateDirec>();
                foreach (var node in resultList)
                {
                    var tmpChildren = descendantNode(node.UniqueID);
                    if (tmpChildren.Count > 0) childrenNodes.AddRange(tmpChildren);
                }
                resultList.AddRange(childrenNodes);
            }
            return resultList;
        }

        /// <summary>
        /// delete template
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteTemplateByID(string id)
        {
            ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UniqueID == id).FirstOrDefault();
            if (reportTemplateDirec == null) {
                reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.TemplateID == id).FirstOrDefault(); 
            }
            if (reportTemplateDirec != null)
            {
                var dirList = new List<ReportTemplateDirec>();
                var tplList = new List<ReportTemplate>();
                dirList.Add(reportTemplateDirec);

                var descendants = descendantNode(id);
                if (descendants.Count > 0)
                {
                    dirList.AddRange(descendants);
                }
                foreach (var node in dirList)
                {
                    var tpl = _dbContext.Set<ReportTemplate>().Where(p => p.UniqueID == node.TemplateID).FirstOrDefault();
                    if (tpl != null)
                    {
                        tplList.Add(tpl);
                    }
                }

                _dbContext.Set<ReportTemplateDirec>().RemoveRange(dirList);
                _dbContext.Set<ReportTemplate>().RemoveRange(tplList);
            }
            _dbContext.SaveChanges();
            return true;
        }

        public bool IsDuplicatedTemplateName(ReportTemplateDto reportTemplateDto, string userID)
        {
            ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UserID == userID && p.ItemName == reportTemplateDto.TemplateName).FirstOrDefault();

            if (reportTemplateDto.UniqueID == null)
            {
                reportTemplateDto.UniqueID = "";
            }
            if (reportTemplateDirec != null && reportTemplateDirec.TemplateID.ToLower() != reportTemplateDto.UniqueID.ToLower())
            {
                return true;
            }

            return false;
        }

        #region  模板管理

        /// <summary>
        /// 创建全局模板
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ReportTemplateDto CreatePublicReportTemplate(ReportTemplateDto reportTemplateDto, string userID)
        {
            reportTemplateDto.UniqueID = Guid.NewGuid().ToString();
            if (reportTemplateDto.IsPositive == null)
                reportTemplateDto.IsPositive = 0;

            ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UserID == userID && p.ItemName == reportTemplateDto.TemplateName).FirstOrDefault();
            if (reportTemplateDirec == null)
            {
                ReportTemplateDirec reportTemplateDirecNew = new ReportTemplateDirec();
                reportTemplateDirecNew.UniqueID = Guid.NewGuid().ToString();
                reportTemplateDirecNew.ParentID = reportTemplateDto.ParentID;
                reportTemplateDirecNew.TemplateID = reportTemplateDto.UniqueID;
                reportTemplateDirecNew.UserID = "";
                reportTemplateDirecNew.ItemName = reportTemplateDto.TemplateName;
                reportTemplateDirecNew.Leaf = 1;
                reportTemplateDirecNew.DirectoryType = "report";
                reportTemplateDirecNew.Domain = reportTemplateDto.Domain;
                reportTemplateDirecNew.Type = reportTemplateDto.Type;//0
                reportTemplateDirecNew.Depth = -1;
                reportTemplateDirecNew.ItemOrder = 0;

                List<ReportTemplateDirec> reportTemplateDirecList = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == reportTemplateDto.ParentID).OrderByDescending(p => p.ItemOrder).ToList();
                if (reportTemplateDirecList.Count > 0)
                {
                    if (reportTemplateDirecList[0].ItemOrder.HasValue)
                    {
                        reportTemplateDirecNew.ItemOrder = reportTemplateDirecList[0].ItemOrder + 1;
                    }
                }
                var report = Mapper.Map<ReportTemplateDto, ReportTemplate>(reportTemplateDto);
                _dbContext.Set<ReportTemplate>().Add(report);
                _dbContext.Set<ReportTemplateDirec>().Add(reportTemplateDirecNew);
                _dbContext.SaveChanges();
            }
            else
            {
                return null;
            }

            ReportTemplate reportTemplate = _dbContext.Set<ReportTemplate>().Where(p => p.UniqueID == reportTemplateDto.UniqueID).FirstOrDefault();
            if (reportTemplate != null)
            {
                return Mapper.Map<ReportTemplate, ReportTemplateDto>(reportTemplate);
            }

            return null;
        }

        /// <summary>
        /// 修改全局模板
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ReportTemplateDto UpdatePublicReportTemplate(ReportTemplateDto reportTemplateDto, string userID)
        {
            ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.TemplateID == reportTemplateDto.UniqueID).FirstOrDefault();
            if (reportTemplateDto.IsPositive == null)
                reportTemplateDto.IsPositive = 0;
            if (reportTemplateDirec != null)
            {
                reportTemplateDirec.ItemName = reportTemplateDto.TemplateName;
                ReportTemplateDirec reportTemplateDirecDup = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == reportTemplateDto.ParentID && p.ItemName == reportTemplateDto.TemplateName).FirstOrDefault();
                if (reportTemplateDirecDup != null)
                {
                    if (reportTemplateDirecDup.UniqueID != reportTemplateDirec.UniqueID)
                    {
                        return null;
                    }
                }
                //修改模板详细
                ReportTemplate reportTemplateOld = _dbContext.Set<ReportTemplate>().Where(p => p.UniqueID == reportTemplateDto.UniqueID).FirstOrDefault();
                if (reportTemplateOld != null)
                {
                    reportTemplateOld.TemplateName = reportTemplateDto.TemplateName;
                    reportTemplateOld.CheckItemName = reportTemplateDto.CheckItemName;
                    reportTemplateOld.ModalityType = reportTemplateDto.ModalityType;
                    reportTemplateOld.BodyPart = reportTemplateDto.BodyPart;
                    reportTemplateOld.ShortcutCode = reportTemplateDto.ShortcutCode;
                    reportTemplateOld.ACRCode = reportTemplateDto.ACRCode;
                    reportTemplateOld.DoctorAdvice = reportTemplateDto.DoctorAdvice;
                    reportTemplateOld.BodyCategory = reportTemplateDto.BodyCategory;
                    reportTemplateOld.IsPositive = reportTemplateDto.IsPositive;
                    reportTemplateOld.Gender = reportTemplateDto.Gender;
                    reportTemplateOld.WYS = ReportMapper.GetBytes(reportTemplateDto.WYSText == null ? "" : reportTemplateDto.WYSText);
                    reportTemplateOld.WYG = ReportMapper.GetBytes(reportTemplateDto.WYGText == null ? "" : reportTemplateDto.WYSText);

                    _dbContext.SaveChanges();
                }

            }

            ReportTemplate reportTemplate = _dbContext.Set<ReportTemplate>().Where(p => p.UniqueID == reportTemplateDto.UniqueID).FirstOrDefault();
            if (reportTemplate != null)
            {
                return Mapper.Map<ReportTemplate, ReportTemplateDto>(reportTemplate);
            }

            return null;
        }


        /// <summary>
        /// 添加模板目录
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ReportTemplateDirecDto CreateReportTemplateDirec(ReportTemplateDirecDto reportTemplateDto, string userID)
        {

            ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UserID == userID && p.ItemName == reportTemplateDto.ItemName).FirstOrDefault();
            if (reportTemplateDirec == null)
            {
                ReportTemplateDirec reportTemplateDirecNew = new ReportTemplateDirec();
                reportTemplateDirecNew.UniqueID = Guid.NewGuid().ToString();
                reportTemplateDirecNew.ParentID = reportTemplateDto.ParentID;
                reportTemplateDirecNew.TemplateID = "";
                reportTemplateDirecNew.UserID = userID;
                reportTemplateDirecNew.ItemName = reportTemplateDto.ItemName;
                reportTemplateDirecNew.Leaf = 0;
                reportTemplateDirecNew.DirectoryType = "report";
                reportTemplateDirecNew.Domain = reportTemplateDto.Domain;
                reportTemplateDirecNew.Type = reportTemplateDto.Type;//0:公共、1:用户、2:站点
                reportTemplateDirecNew.Depth = -1;
                reportTemplateDirecNew.ItemOrder = 0;

                List<ReportTemplateDirec> reportTemplateDirecList = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == reportTemplateDto.ParentID).OrderByDescending(p => p.ItemOrder).ToList();
                if (reportTemplateDirecList.Count > 0)
                {
                    if (reportTemplateDirecList[0].ItemOrder.HasValue)
                    {
                        reportTemplateDirecNew.ItemOrder = reportTemplateDirecList[0].ItemOrder + 1;
                    }
                }
                _dbContext.Set<ReportTemplateDirec>().Add(reportTemplateDirecNew);
                _dbContext.SaveChanges();
                reportTemplateDto.UniqueID = reportTemplateDirecNew.UniqueID;
            }
            else
            {
                return null;
            }

            ReportTemplateDirec reportTemplate = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UniqueID == reportTemplateDto.UniqueID).FirstOrDefault();
            if (reportTemplate != null)
            {
                return Mapper.Map<ReportTemplateDirec, ReportTemplateDirecDto>(reportTemplate);
            }

            return null;
        }

        /// <summary>
        /// 模板目录修改
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public ReportTemplateDirecDto UpdateReportTemplateDirec(ReportTemplateDirecDto templateDirecDto, string userID)
        {
            ReportTemplateDirec reportTemplateDirec = null;
            if (templateDirecDto.Type == 1)//用户目录修改自己的
            {
                reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UniqueID == templateDirecDto.UniqueID && p.UserID == userID).FirstOrDefault();
            }
            else
            {
                reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UniqueID == templateDirecDto.UniqueID).FirstOrDefault();
            }
            if (reportTemplateDirec != null)
            {
                reportTemplateDirec.ItemName = templateDirecDto.ItemName;
                ReportTemplateDirec reportTemplateDirecDup = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == templateDirecDto.ParentID && p.ItemName == templateDirecDto.ItemName).FirstOrDefault();
                if (reportTemplateDirecDup != null)
                {
                    if (reportTemplateDirecDup.UniqueID != reportTemplateDirec.UniqueID)
                    {
                        return null;
                    }
                }
                _dbContext.SaveChanges();

                return Mapper.Map<ReportTemplateDirec, ReportTemplateDirecDto>(reportTemplateDirec);
            }
            return null;
        }
        /// <summary>
        /// 目录上移
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool NodeItemOrderUp(string id)
        {
            ReportTemplateDirec reportTemplateDirecU = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UniqueID == id).FirstOrDefault();
            if (reportTemplateDirecU != null)
            {
                if (!string.IsNullOrEmpty(reportTemplateDirecU.ParentID) && reportTemplateDirecU.ItemOrder >= 1)
                {
                    var indexs = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == reportTemplateDirecU.ParentID && p.ItemOrder < reportTemplateDirecU.ItemOrder).Select(p => p.ItemOrder).ToList();
                    if (indexs == null || indexs.Count < 1)
                    {
                        return false;
                    }
                    int index = (int)indexs.Max();

                    ReportTemplateDirec reportTemplateDirecD = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == reportTemplateDirecU.ParentID && p.ItemOrder == index).FirstOrDefault();
                    if (reportTemplateDirecD != null)
                    {
                        int distance = (int)reportTemplateDirecU.ItemOrder - index;
                        reportTemplateDirecU.ItemOrder = reportTemplateDirecU.ItemOrder - distance;
                        reportTemplateDirecD.ItemOrder = reportTemplateDirecD.ItemOrder + distance;
                    }
                }
            }
            _dbContext.SaveChanges();
            return true;
        }
        /// <summary>
        /// 目录下移
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool NodeItemOrderDown(string id)
        {
            ReportTemplateDirec reportTemplateDirecD = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UniqueID == id).FirstOrDefault();
            if (reportTemplateDirecD != null)
            {
                if (!string.IsNullOrEmpty(reportTemplateDirecD.ParentID))
                {

                    var indexs = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == reportTemplateDirecD.ParentID && p.ItemOrder > reportTemplateDirecD.ItemOrder).Select(p => p.ItemOrder).ToList();
                    if (indexs == null || indexs.Count < 1)
                    {
                        return false;
                    }
                    int index = (int)indexs.Min();
                    ReportTemplateDirec reportTemplateDirecU = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ParentID == reportTemplateDirecD.ParentID && p.ItemOrder == index).FirstOrDefault();
                    if (reportTemplateDirecU != null)
                    {
                        int distance = index - (int)reportTemplateDirecD.ItemOrder;
                        reportTemplateDirecD.ItemOrder = reportTemplateDirecD.ItemOrder + distance;
                        reportTemplateDirecU.ItemOrder = reportTemplateDirecU.ItemOrder - distance;
                    }
                }
            }
            _dbContext.SaveChanges();
            return true;
        }
        /// <summary>
        /// 创建模板验证
        /// </summary>
        /// <param name="reportTemplateDto"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public bool CreateReportTemplateExist(ReportTemplateDirecDto reportTemplateDto, string userID)
        {
            if (reportTemplateDto.Type == 0 || reportTemplateDto.Type == 2)
            {
                if (string.IsNullOrEmpty(reportTemplateDto.UniqueID))
                {
                    ReportTemplateDirec reportTemplateDirec1 = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ItemName == reportTemplateDto.ItemName).FirstOrDefault();
                    if (reportTemplateDirec1 != null)
                    {
                        return false;
                    }
                }
                else
                {
                    ReportTemplateDirec reportTemplateDirec1 = _dbContext.Set<ReportTemplateDirec>().Where(p => p.ItemName == reportTemplateDto.ItemName && p.UniqueID != reportTemplateDto.UniqueID).FirstOrDefault();
                    if (reportTemplateDirec1 != null)
                    {
                        return false;
                    }

                }
              
            }
            if (string.IsNullOrEmpty(reportTemplateDto.UniqueID))
            {
                ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UserID == userID && p.ItemName == reportTemplateDto.ItemName).FirstOrDefault();
                if (reportTemplateDirec != null)
                {
                        return false;
                }
            }
            else
            {
                ReportTemplateDirec reportTemplateDirec = _dbContext.Set<ReportTemplateDirec>().Where(p => p.UserID == userID && p.ItemName == reportTemplateDto.ItemName&&p.UniqueID!= reportTemplateDto.UniqueID).FirstOrDefault();
                if (reportTemplateDirec != null)
                {
                    return false;
                }
            }
            
            return true;
        }

        //所有部位
        public IEnumerable<ACRCodeAnatomicalDto> GetAllAnatomical()
        {
            var query = _dbContext.Set<ACRCodeAnatomical>().OrderBy(a => a.AID);
            var result = query.Select(p => Mapper.Map<ACRCodeAnatomical, ACRCodeAnatomicalDto>(p)).ToList();
            return result;
        }
        //详细部位
        public IEnumerable<ACRCodeSubAnatomicalDto> GetSubAnatomicalsByaid(string aid)
        {
            if (string.IsNullOrEmpty(aid))
            {
                return null;
            }
            var query = _dbContext.Set<ACRCodeSubAnatomical>().Where(a => a.AID == aid).OrderBy(a => a.SID);
            var result = query.Select(p => Mapper.Map<ACRCodeSubAnatomical, ACRCodeSubAnatomicalDto>(p)).ToList();
            return result;
        }
        //病理
        public IEnumerable<ACRCodePathologicalDto> GetPathologicalsByaid(string aid)
        {
            if (string.IsNullOrEmpty(aid))
            {
                return null;
            }
            var query = _dbContext.Set<ACRCodePathological>().Where(a => a.AID == aid).OrderBy(a => a.PID);
            var result = query.Select(p => Mapper.Map<ACRCodePathological, ACRCodePathologicalDto>(p)).ToList();
            return result;
        }

        //详细病理
        public IEnumerable<ACRCodeSubPathologicalDto> GetPathologicalsByaid(string aid, string pid)
        {
            if (string.IsNullOrEmpty(aid))
            {
                return null;
            }
            var query = _dbContext.Set<ACRCodeSubPathological>().Where(a => a.AID == aid && a.PID == pid).OrderBy(a => a.PID);
            var result = query.Select(p => Mapper.Map<ACRCodeSubPathological, ACRCodeSubPathologicalDto>(p)).ToList();
            return result;
        }
        #endregion
    }
}
