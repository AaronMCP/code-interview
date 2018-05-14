using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using Hys.Consultation.EntityFramework.Repositories;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Application.Dtos.PatientCase;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.CrossCutting.Common.Utils;
using Hys.Platform.Application;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;

namespace Hys.Consultation.Application.Services.ServiceImpl
{
    public class ConsultationPatientCaseService : DisposableServiceBase, IConsultationPatientCaseService
    {
        private readonly IConsultationContext _dbContext;
        private readonly IRisProContext _risProContext;
        private readonly IPatientCaseRepository _PatientCaseRepository;
        private readonly IConsultationConfigurationService _ConsultationConfigurationService;
        private readonly ILoginUserService _LoginUserService;


        public ConsultationPatientCaseService(IConsultationContext consultationContext, IRisProContext risProContext, IPatientCaseRepository patientCaseRepository,
            IConsultationConfigurationService consultationConfigurationService, ILoginUserService loginUserService)
        {
            _dbContext = consultationContext;
            _risProContext = risProContext;
            _PatientCaseRepository = patientCaseRepository;
            _ConsultationConfigurationService = consultationConfigurationService;
            _LoginUserService = loginUserService;

            AddDisposableObject(consultationContext);
            AddDisposableObject(risProContext);
            AddDisposableObject(patientCaseRepository);
            AddDisposableObject(consultationConfigurationService);
        }

        public PatientCaseInfoDto CreatePatientCase(PatientCaseInfoDto patientCaseDto, IEnumerable<ExamModuleDto> defaultModules,string userID,string localName )
        {
            string patientCaseID = Guid.NewGuid().ToString();
            List<RegEMRItemFileDto> regEMRItemFiles = new List<RegEMRItemFileDto>();
            List<EMRItem> emrItems = new List<EMRItem>();
            List<EMRItemDetail> emrItemDetails = new List<EMRItemDetail>();

            HospitalProfileDto hospitalProfileDto = _ConsultationConfigurationService.GetHospital(userID);
            if (patientCaseDto.newEMRItems != null && patientCaseDto.newEMRItems.Count > 0)
            {
                this.BuildEMRItem(emrItems, emrItemDetails, regEMRItemFiles, hospitalProfileDto, patientCaseDto.newEMRItems, patientCaseID, userID);
            }

            var patientCase = InitialPersonPatientCase(patientCaseDto, patientCaseID, hospitalProfileDto.UniqueID, userID,localName);
            var person = InitialPerson(patientCase.PatientNo);
            var personPatientCase = InitialPersonPatientCase(person.UniqueID, patientCase.UniqueID, userID);

            var newPatientCaseDto = Mapper.Map<PatientCase, PatientCaseInfoDto>(patientCase);
            var newModules = new List<ExamModuleDto>();
            if (patientCaseDto.Modules != null)
            {
                foreach (var examModuleDto in patientCaseDto.Modules.Where(examModuleDto => examModuleDto != null))
                {
                    examModuleDto.Owner = patientCaseID;
                    newModules.Add(examModuleDto);
                }
            }

            if (patientCaseDto.IsMobile)
            {
                var damInfo = _ConsultationConfigurationService.GetDam();

                var risRegEmrItemFiles = regEMRItemFiles.Where(r => r.IsFromRis != null && r.IsFromRis == true).ToList();
                if (risRegEmrItemFiles.Count > 0)
                {
                    using (var client = new HttpClient())
                    {
                        var parameters = "/api/v1/registration/newitemlist";
                        var response = client.PostAsync(damInfo.WebApiUrl + parameters, risRegEmrItemFiles, new JsonMediaTypeFormatter()).Result;
                    }
                }

                patientCase.Progress = 100;

                foreach (var emrItemDetail in emrItemDetails)
                {
                    emrItemDetail.DamID = damInfo.UniqueID;
                }

                SaveNewPateintCase(patientCase, person, personPatientCase, emrItems, emrItemDetails);

                if (patientCaseDto.Modules != null)
                {
                    _ConsultationConfigurationService.AddExamModules(newModules, userID);
                }
                return newPatientCaseDto;
            }

            if (regEMRItemFiles.Count > 0)
            {
                var damInfo = _ConsultationConfigurationService.GetDam();
                using (var client = new HttpClient())
                {
                    var parameters = "/api/v1/registration/newitemlist";
                    var response = client.PostAsync(damInfo.WebApiUrl + parameters, regEMRItemFiles, new JsonMediaTypeFormatter()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        foreach (var emrItemDetail in emrItemDetails)
                        {
                            emrItemDetail.DamID = damInfo.UniqueID;
                        }
                        SaveNewPateintCase(patientCase, person, personPatientCase, emrItems, emrItemDetails);
                        if (patientCaseDto.Modules != null)
                        {
                            _ConsultationConfigurationService.AddExamModules(newModules, userID);
                        }
                        return newPatientCaseDto;
                    }
                }
            }
            else
            {
                patientCase.Progress = 100;
                SaveNewPateintCase(patientCase, person, personPatientCase, emrItems, emrItemDetails);
                if (newModules.Count == 0)
                {
                    foreach (ExamModuleDto examModuleDto in defaultModules)
                    {
                        ExamModuleDto newModule = new ExamModuleDto();
                        newModule.ID = examModuleDto.ID;
                        newModule.LastEditTime = examModuleDto.LastEditTime;
                        newModule.LastEditUser = examModuleDto.LastEditUser;
                        newModule.Position = examModuleDto.Position;
                        newModule.Title = examModuleDto.Title;
                        newModule.Type = examModuleDto.Type;
                        newModule.Visible = examModuleDto.Visible;
                        newModule.Owner = patientCase.UniqueID;
                        newModules.Add(newModule);
                    }

                }
                _ConsultationConfigurationService.AddExamModules(newModules, userID);
                return newPatientCaseDto;
            }

            return null;
        }

        private void SaveNewPateintCase(PatientCase patientCase,
            Person person,
            PersonPatientCase personPatientCase,
            List<EMRItem> emrItems,
            List<EMRItemDetail> emrItemDetails)
        {
            _dbContext.Set<PatientCase>().Add(patientCase);
            _dbContext.Set<Person>().Add(person);
            _dbContext.Set<PersonPatientCase>().Add(personPatientCase);
            _dbContext.Set<EMRItem>().AddRange(emrItems);
            _dbContext.Set<EMRItemDetail>().AddRange(emrItemDetails);

            _dbContext.SaveChanges();
        }

        public List<PatientCaseInfoDto> GetCombinePatientCaseList(string patientId, string identityCard)
        {
            PatientCase patientCase = _dbContext.Set<PatientCase>().FirstOrDefault(p => p.UniqueID == patientId);
            if (patientCase != null)
            {
                Person person = (from p in _dbContext.Set<Person>()
                                 join pr in _dbContext.Set<PersonPatientCase>()
                                     on p.UniqueID equals pr.PersonID
                                 where pr.PatientCaseID == patientId
                                 select p).FirstOrDefault();

                var query = (from p in _dbContext.Set<Person>()
                             join pp in _dbContext.Set<PersonPatientCase>()
                                 on p.UniqueID equals pp.PersonID
                             join pc in _dbContext.Set<PatientCase>()
                                 on pp.PatientCaseID equals pc.UniqueID
                             where pc.IdentityCard == identityCard && pc.UniqueID != patientId && p.PatientNo != person.PatientNo
                             && pc.HospitalId == patientCase.HospitalId && pc.IsDeleted == 0
                             orderby p.PatientNo, pc.PatientName
                             select new PatientCaseInfoDto
                             {
                                 UniqueID = pc.UniqueID,
                                 PatientNo = p.PatientNo,
                                 PatientName = pc.PatientName,
                                 Gender = pc.Gender,
                                 Address = pc.Address,
                                 Birthday = pc.Birthday,
                                 InsuranceNumber = pc.InsuranceNumber,
                                 Age = pc.Age,
                                 LastEditTime = pc.LastEditTime,
                                 CreateTime = pc.CreateTime
                             });
                return query.ToList();
            }
            return new List<PatientCaseInfoDto>();
        }

        public async Task<List<PatientCaseInfoDto>> GetCombinePatientCaseListAsync(CombinePatientCaseDto combinePatientCase)
        {
            var query = (from p in _dbContext.Set<Person>()
                         join pp in _dbContext.Set<PersonPatientCase>()
                             on p.UniqueID equals pp.PersonID
                         join pc in _dbContext.Set<PatientCase>()
                             on pp.PatientCaseID equals pc.UniqueID
                         where pc.IdentityCard.Equals(combinePatientCase.IdentityCard)
                         && pc.UniqueID != combinePatientCase.PatientId
                         && p.PatientNo != combinePatientCase.PatientNo
                         && pc.HospitalId == combinePatientCase.HospitalId
                         && pc.IsDeleted == 0
                         orderby p.PatientNo
                         select new PatientCaseInfoDto
                         {
                             UniqueID = pc.UniqueID,
                             PatientNo = p.PatientNo,
                             PatientName = pc.PatientName,
                             Gender = pc.Gender,
                             Address = pc.Address,
                             Birthday = pc.Birthday,
                             InsuranceNumber = pc.InsuranceNumber,
                             Age = pc.Age,
                             LastEditTime = pc.LastEditTime,
                             CreateTime = pc.CreateTime
                         });
            return await query.ToListAsync();
        }

        public bool CombinePatientCase(PatientCaseCombineDto patientCaseCombineDto)
        {
            Person person = _dbContext.Set<Person>().Where(p => p.PatientNo == patientCaseCombineDto.PatientNo).FirstOrDefault();
            PatientCase patientCase = (from p in _dbContext.Set<Person>()
                                       join pp in _dbContext.Set<PersonPatientCase>()
                                           on p.UniqueID equals pp.PersonID
                                       join pa in _dbContext.Set<PatientCase>()
                                       on pp.PatientCaseID equals pa.UniqueID
                                       where p.PatientNo == patientCaseCombineDto.PatientNo
                                       select pa
                                ).FirstOrDefault();

            if (person != null)
            {
                foreach (string combineNo in patientCaseCombineDto.PatientCombineNo)
                {
                    if (combineNo == patientCaseCombineDto.PatientNo)
                    {
                        continue;
                    }
                    var query = (from p in _dbContext.Set<Person>()
                                 join pp in _dbContext.Set<PersonPatientCase>()
                                     on p.UniqueID equals pp.PersonID
                                 join pa in _dbContext.Set<PatientCase>()
                                   on pp.PatientCaseID equals pa.UniqueID
                                 where p.PatientNo == combineNo && pa.IdentityCard == patientCase.IdentityCard
                                 select pp
                                );
                    List<PersonPatientCase> list = query.ToList();
                    foreach (PersonPatientCase personPatientCase in list)
                    {
                        personPatientCase.PersonID = person.UniqueID;
                    }

                    var query2 = (from p in _dbContext.Set<Person>()
                                  join pp in _dbContext.Set<PersonPatientCase>()
                                      on p.UniqueID equals pp.PersonID
                                  where p.PatientNo == combineNo
                                  select pp
                               );

                    if (query.Count() == query2.Count())
                    {
                        Person delPerson = _dbContext.Set<Person>().Where(p => p.PatientNo == combineNo).FirstOrDefault();
                        if (delPerson != null)
                        {
                            _dbContext.Set<Person>().Remove(delPerson);
                        }
                    }
                }

                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public PatientCaseInfoDto GetPatientCaseNoItems(string id)
        {
            PatientCase patientCase = _PatientCaseRepository.Get(p => p.UniqueID == id).FirstOrDefault();
            if (patientCase != null)
            {
                PatientCaseInfoDto patientCaseDto = Mapper.Map<PatientCase, PatientCaseInfoDto>(patientCase);
                Person person = (from p in _dbContext.Set<Person>()
                                 join pr in _dbContext.Set<PersonPatientCase>()
                                     on p.UniqueID equals pr.PersonID
                                 where pr.PatientCaseID == id
                                 select p).FirstOrDefault();
                patientCaseDto.PatientNo = person.PatientNo;

                if (_LoginUserService.ServiceContext.IsPublicAccount)
                {
                    patientCaseDto.DeleteUserName = patientCase.DeletePublicAccountName;
                }
                else
                {
                    var deleteUserName = _risProContext.Set<User>().Where(u => u.UniqueID.Equals(patientCaseDto.DeleteUser)).Select(u => u.LocalName).FirstOrDefault();
                    if (!string.IsNullOrEmpty(deleteUserName))
                    {
                        patientCaseDto.DeleteUserName = deleteUserName;
                    }
                }

                return patientCaseDto;
            }

            return null;
        }

        public bool EditPatientCase(PatientCaseEditInfoDto patientCaseDto,string userID)
        {
            List<RegEMRItemFileDto> regNewEMRItemFiles = new List<RegEMRItemFileDto>();
            List<EMRItem> emrNewItems = new List<EMRItem>();
            List<EMRItemDetail> emrNewItemDetails = new List<EMRItemDetail>();

            List<RegEMRItemFileDto> regEditEMRItemFiles = new List<RegEMRItemFileDto>();
            List<EMRItem> emrEditItems = new List<EMRItem>();

            HospitalProfileDto hospitalProfileDto = _ConsultationConfigurationService.GetHospital(userID);
            string dicomPrefix = hospitalProfileDto.DicomPrefix;
            if (string.IsNullOrEmpty(dicomPrefix))
            {
                dicomPrefix = hospitalProfileDto.UniqueID.Substring(0, 1) + "-";
            }

            //register to DAM
            if (patientCaseDto.newEMRItems != null && patientCaseDto.newEMRItems.Count > 0)
            {
                EditPatientCaseNewItems(patientCaseDto, regNewEMRItemFiles, emrNewItems, emrNewItemDetails, regEditEMRItemFiles, emrEditItems, dicomPrefix, userID);
            }

            patientCaseDto.LastEditUser = userID;
            patientCaseDto.LastEditTime = DateTime.Now;
            patientCaseDto.PatientNamePy = PinyinUtil.ToPinyin(patientCaseDto.PatientName, false, 0, "");
            patientCaseDto.Progress = 0;

            //delete item
            if (patientCaseDto.DeletedItemData != null)
            {
                EditPatientCaseDelItems(patientCaseDto);
            }
            List<ExamModuleDto> examModuleList = new List<ExamModuleDto>();
            if (patientCaseDto.Modules != null)
            {
                foreach (ExamModuleDto examModuleDto in patientCaseDto.Modules)
                {
                    if (examModuleDto != null)
                    {
                        examModuleDto.Owner = patientCaseDto.UniqueID;
                        examModuleList.Add(examModuleDto);
                    }
                }
            }

            if (regNewEMRItemFiles.Count > 0 || regEditEMRItemFiles.Count > 0 || (patientCaseDto.DeletedFileData != null && patientCaseDto.DeletedFileData.Count > 0))
            {
                var damInfo = _ConsultationConfigurationService.GetDam();
                var editEMRItemFileDto = new EditEMRItemFileDto
                {
                    NewItems = regNewEMRItemFiles,
                    EditItems = regEditEMRItemFiles,
                    DeleteItems = patientCaseDto.DeletedFileData
                };

                SaveEditPateintCaseToDAM(patientCaseDto, emrNewItems, emrEditItems, damInfo, editEMRItemFileDto, emrNewItemDetails, examModuleList, userID);
                return true;
            }

            patientCaseDto.Progress = 100;
            SaveEditPateintCase(patientCaseDto, emrNewItems, emrEditItems, emrNewItemDetails, patientCaseDto.DeletedItemData, null, null);
            if (patientCaseDto.ModuleIsNew)
            {
                _ConsultationConfigurationService.AddExamModules(examModuleList, userID);
            }
            else
            {
                _ConsultationConfigurationService.UpdateExamModules(examModuleList, userID);
            }

            return true;
        }

        private void EditPatientCaseDelItems(PatientCaseEditInfoDto patientCaseDto)
        {
            List<EMRItemDetail> emrItemDetails = _dbContext.Set<EMRItemDetail>().Where(e => patientCaseDto.DeletedItemData.Contains(e.EMRItemID)).ToList();

            if (emrItemDetails.Count > 0 && patientCaseDto.DeletedFileData == null)
            {
                patientCaseDto.DeletedFileData = new List<string>();
            }

            foreach (EMRItemDetail emrItemDetail in emrItemDetails)
            {
                if (!patientCaseDto.DeletedFileData.Contains(emrItemDetail.DetailID))
                {
                    patientCaseDto.DeletedFileData.Add(emrItemDetail.DetailID);
                }
            }
        }

        private static void EditPatientCaseNewItems(PatientCaseEditInfoDto patientCaseDto,
            List<RegEMRItemFileDto> regNewEMRItemFiles,
            List<EMRItem> emrNewItems,
            List<EMRItemDetail> emrNewItemDetails,
            List<RegEMRItemFileDto> regEditEMRItemFiles,
            List<EMRItem> emrEditItems,
            string dicomPrefix,string userID)
        {
            foreach (NewEMRItemDto newEMRItemDto in patientCaseDto.newEMRItems)
            {
                EMRItem emrItem = Mapper.Map<NewEMRItemDto, EMRItem>(newEMRItemDto);

                emrItem.PatientCaseID = patientCaseDto.UniqueID;
                emrItem.LastEditUser = userID;
                emrItem.LastEditTime = DateTime.Now;

                if (string.IsNullOrEmpty(emrItem.UniqueID))
                {
                    emrItem.UniqueID = Guid.NewGuid().ToString();
                    emrNewItems.Add(emrItem);
                }
                else
                {
                    emrEditItems.Add(emrItem);
                }

                if (newEMRItemDto.ItemFiles != null && newEMRItemDto.ItemFiles.Any())
                {
                    foreach (NewEMRItemFileDto newEMRItemFileDto in newEMRItemDto.ItemFiles)
                    {
                        var isNew = true;

                        if (string.IsNullOrEmpty(newEMRItemFileDto.UniqueID))
                        {
                            newEMRItemFileDto.UniqueID = Guid.NewGuid().ToString();
                        }
                        else
                        {
                            isNew = false;
                        }

                        RegEMRItemFileDto regEMRItemFile = new RegEMRItemFileDto
                        {
                            UniqueID = newEMRItemFileDto.UniqueID,
                            ItemType = newEMRItemFileDto.FileType == "folder" ? 1 : 0,
                            FileName = newEMRItemFileDto.FileName,
                            SrcFilePath = newEMRItemFileDto.Path,
                            SrcInfo = newEMRItemFileDto.SrcInfo,
                            DicomPrefix = dicomPrefix,
                            CreatorID = userID
                        };

                        if (isNew)
                        {
                            regNewEMRItemFiles.Add(regEMRItemFile);

                            emrNewItemDetails.Add(new EMRItemDetail
                            {
                                UniqueID = Guid.NewGuid().ToString(),
                                EMRItemID = emrItem.UniqueID,
                                DetailID = string.IsNullOrEmpty(newEMRItemFileDto.DetailedId) ? newEMRItemFileDto.UniqueID : newEMRItemFileDto.DetailedId,
                                LastEditUser = userID,
                                LastEditTime = DateTime.Now
                            });
                        }
                        else
                        {
                            regEditEMRItemFiles.Add(regEMRItemFile);
                        }
                    }
                }
            }
        }

        private void SaveEditPateintCaseToDAM(PatientCaseEditInfoDto patientCaseDto,
            List<EMRItem> emrNewItems,
            List<EMRItem> emrEditItems,
            DAMInfoDto damInfo,
            EditEMRItemFileDto editEMRItemFileDto,
            List<EMRItemDetail> emrNewItemDetails,
            List<ExamModuleDto> examModuleList,string userID)
        {

            var parameters = "/api/v1/registration/edititemlist";

            if (patientCaseDto.IsMobile)
            {
                parameters = "/api/v1/registration/edititemlistmobile";
            }

            using (var client = new HttpClient())
            {
                HttpResponseMessage response =
                    client.PostAsync(damInfo.WebApiUrl + parameters, editEMRItemFileDto, new JsonMediaTypeFormatter())
                        .Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<List<string>>().Result;

                    foreach (EMRItemDetail emrItemDetail in emrNewItemDetails)
                    {
                        emrItemDetail.DamID = damInfo.UniqueID;
                    }

                    SaveEditPateintCase(patientCaseDto, emrNewItems, emrEditItems, emrNewItemDetails,
                        patientCaseDto.DeletedItemData, patientCaseDto.DeletedFileData, result);

                    if (patientCaseDto.ModuleIsNew)
                    {
                        _ConsultationConfigurationService.AddExamModules(examModuleList, userID);
                    }
                    else
                    {
                        _ConsultationConfigurationService.UpdateExamModules(examModuleList, userID);
                    }
                }
            }
        }

        private void SaveEditPateintCase(PatientCaseEditInfoDto patientCase,
            List<EMRItem> emrNewItems,
            List<EMRItem> emrEditItems,
            List<EMRItemDetail> emrItemDetails,
            List<string> deletedItems,
            List<string> deletedFiles,
            List<string> parents)
        {
            //update
            PatientCase oldPatientCase = _dbContext.Set<PatientCase>().FirstOrDefault(p => p.UniqueID == patientCase.UniqueID);
            if (oldPatientCase != null)
            {
                patientCase.CreateTime = oldPatientCase.CreateTime;
                patientCase.PatientNo = oldPatientCase.PatientNo;
                Mapper.Map(patientCase, oldPatientCase);
            }

            foreach (EMRItem emrItem in emrNewItems)
            {
                _dbContext.Set<EMRItem>().Add(emrItem);
            }

            foreach (EMRItemDetail emrItemDetail in emrItemDetails)
            {
                _dbContext.Set<EMRItemDetail>().Add(emrItemDetail);
            }

            foreach (EMRItem emrItem in emrEditItems)
            {
                EMRItem oldEMRItem = _dbContext.Set<EMRItem>().FirstOrDefault(p => p.UniqueID == emrItem.UniqueID);
                if (oldEMRItem != null)
                {
                    EMRItemDto emrItemDto = Mapper.Map<EMRItem, EMRItemDto>(emrItem);
                    Mapper.Map(emrItemDto, oldEMRItem);
                }
            }

            if (deletedItems != null)
            {
                foreach (string id in deletedItems)
                {
                    EMRItem oldEMRItem = _dbContext.Set<EMRItem>().FirstOrDefault(p => p.UniqueID == id);
                    if (oldEMRItem != null)
                    {
                        List<EMRItemDetail> delEMRItemDetails = _dbContext.Set<EMRItemDetail>().Where(e => e.EMRItemID == id).ToList();
                        foreach (EMRItemDetail emrItemDetail in delEMRItemDetails)
                        {
                            _dbContext.Set<EMRItemDetail>().Remove(emrItemDetail);
                        }
                        _dbContext.Set<EMRItem>().Remove(oldEMRItem);
                    }
                }
            }

            if (deletedFiles != null)
            {
                foreach (string id in deletedFiles)
                {
                    EMRItemDetail oldEMRItemDetail = _dbContext.Set<EMRItemDetail>().FirstOrDefault(p => p.DetailID == id);
                    if (oldEMRItemDetail != null)
                    {
                        _dbContext.Set<EMRItemDetail>().Remove(oldEMRItemDetail);
                    }
                }

                if (parents != null && parents.Count > 0)
                {
                    foreach (string id in parents)
                    {
                        EMRItemDetail emrItemDetail = _dbContext.Set<EMRItemDetail>().FirstOrDefault(e => e.DetailID == id);
                        if (emrItemDetail != null)
                        {
                            _dbContext.Set<EMRItemDetail>().Remove(emrItemDetail);
                        }
                    }
                }
            }

            _dbContext.SaveChanges();
        }

        public bool ExamInfoDeleteFile(string patientCaseID, string fileID)
        {
            DAMInfoDto damInfo = _ConsultationConfigurationService.GetDam();

            using (var client = new HttpClient())
            {
                var parameters = "/api/v1/registration/delitem/" + fileID;
                HttpResponseMessage response = client.GetAsync(damInfo.WebApiUrl + parameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<List<string>>().Result;
                    List<string> fileIDs = fileID.Split(',').ToList();
                    List<EMRItemDetail> emrItemDetails = _dbContext.Set<EMRItemDetail>().Where(e => fileIDs.Contains(e.DetailID) || result.Contains(e.DetailID)).ToList();
                    foreach (EMRItemDetail emrItemDetail in emrItemDetails)
                    {
                        _dbContext.Set<EMRItemDetail>().Remove(emrItemDetail);
                    }

                    _dbContext.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public bool ExamInfoDeleteItem(string patientCaseID, string itemID)
        {
            List<string> ids = new List<string>();

            EMRItem item = _dbContext.Set<EMRItem>().Where(e => e.UniqueID == itemID).FirstOrDefault();
            if (item != null)
            {
                _dbContext.Set<EMRItem>().Remove(item);
            }

            List<EMRItemDetail> emrItemDetails = _dbContext.Set<EMRItemDetail>().Where(e => e.EMRItemID == itemID).ToList();
            foreach (EMRItemDetail emrItemDetail in emrItemDetails)
            {
                ids.Add(emrItemDetail.DetailID);
                _dbContext.Set<EMRItemDetail>().Remove(emrItemDetail);
            }

            if (ids.Count > 0)
            {
                DAMInfoDto damInfo = _ConsultationConfigurationService.GetDam();

                using (var client = new HttpClient())
                {
                    var parameters = "/api/v1/registration/delitems";
                    HttpResponseMessage response = client.PostAsync<List<string>>(damInfo.WebApiUrl + parameters, ids, new JsonMediaTypeFormatter()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        _dbContext.SaveChanges();
                        return true;
                    }
                }
            }
            else
            {
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        public bool ExamInfoFileNameChanged(string patientCaseID, string fileID, string fileName,string userID)
        {
            DAMInfoDto damInfo = _ConsultationConfigurationService.GetDam();
            RegEMRItemFileDto regEMRItemFileDto = new RegEMRItemFileDto();
            regEMRItemFileDto.UniqueID = fileID;
            regEMRItemFileDto.FileName = fileName;
            regEMRItemFileDto.CreatorID = userID;
            using (var client = new HttpClient())
            {
                var parameters = "/api/v1/registration/updateditem";
                HttpResponseMessage response = client.PostAsync<RegEMRItemFileDto>(damInfo.WebApiUrl + parameters, regEMRItemFileDto, new JsonMediaTypeFormatter()).Result;
                if (response.IsSuccessStatusCode)
                {
                    _dbContext.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public PatientCaseInfoDto ExamInfoItemAdded(PatientCaseInfoDto patientCaseInfoDto,string userID)
        {
            if (patientCaseInfoDto.newEMRItems == null || patientCaseInfoDto.newEMRItems.Count == 0)
            {
                return null;
            }
            List<RegEMRItemFileDto> regEMRItemFiles = new List<RegEMRItemFileDto>();
            List<EMRItemDetail> emrItemDetails = new List<EMRItemDetail>();

            HospitalProfileDto hospitalProfileDto = _ConsultationConfigurationService.GetHospital(userID);
            string dicomPrefix = hospitalProfileDto.DicomPrefix;
            if (string.IsNullOrEmpty(dicomPrefix))
            {
                dicomPrefix = hospitalProfileDto.UniqueID.Substring(0, 1) + "-";
            }

            EMRItem emrItem = Mapper.Map<NewEMRItemDto, EMRItem>(patientCaseInfoDto.newEMRItems[0]);
            emrItem.UniqueID = Guid.NewGuid().ToString();
            emrItem.PatientCaseID = patientCaseInfoDto.UniqueID;
            emrItem.LastEditUser = userID;
            emrItem.LastEditTime = DateTime.Now;

            patientCaseInfoDto.newEMRItems[0].UniqueID = emrItem.UniqueID;

            if (patientCaseInfoDto.newEMRItems[0].ItemFiles != null && patientCaseInfoDto.newEMRItems[0].ItemFiles.Count() > 0)
            {
                foreach (NewEMRItemFileDto newEMRItemFileDto in patientCaseInfoDto.newEMRItems[0].ItemFiles)
                {
                    string detailID = string.IsNullOrEmpty(newEMRItemFileDto.DetailedId) ? Guid.NewGuid().ToString() : newEMRItemFileDto.DetailedId;
                    newEMRItemFileDto.UniqueID = detailID;
                    RegEMRItemFileDto regEMRItemFile = new RegEMRItemFileDto
                    {
                        UniqueID = detailID,
                        ItemType = newEMRItemFileDto.FileType == "folder" ? 1 : 0,
                        FileName = newEMRItemFileDto.FileName,
                        SrcFilePath = newEMRItemFileDto.Path,
                        SrcInfo = newEMRItemFileDto.SrcInfo,
                        DicomPrefix = dicomPrefix,
                        CreatorID = userID
                    };
                    regEMRItemFiles.Add(regEMRItemFile);
                    EMRItemDetail emrItemDetail = new EMRItemDetail
                    {
                        UniqueID = Guid.NewGuid().ToString(),
                        EMRItemID = emrItem.UniqueID,
                        DetailID = detailID,
                        LastEditUser = userID,
                        LastEditTime = DateTime.Now
                    };

                    emrItemDetails.Add(emrItemDetail);
                }
            }

            if (patientCaseInfoDto.IsMobile)
            {
                patientCaseInfoDto.Progress = 100;
                DAMInfoDto damInfo = _ConsultationConfigurationService.GetDam();
                foreach (EMRItemDetail emrItemDetail in emrItemDetails)
                {
                    emrItemDetail.DamID = damInfo.UniqueID;
                    _dbContext.Set<EMRItemDetail>().Add(emrItemDetail);
                }

                _dbContext.Set<EMRItem>().Add(emrItem);
                _dbContext.SaveChanges();
                return patientCaseInfoDto;
            }

            if (regEMRItemFiles.Count > 0)
            {
                DAMInfoDto damInfo = _ConsultationConfigurationService.GetDam();
                using (var client = new HttpClient())
                {
                    var parameters = "/api/v1/registration/newitemlist";
                    HttpResponseMessage response = client.PostAsync<List<RegEMRItemFileDto>>(damInfo.WebApiUrl + parameters, regEMRItemFiles, new JsonMediaTypeFormatter()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        foreach (EMRItemDetail emrItemDetail in emrItemDetails)
                        {
                            emrItemDetail.DamID = damInfo.UniqueID;
                            _dbContext.Set<EMRItemDetail>().Add(emrItemDetail);
                        }

                        _dbContext.Set<EMRItem>().Add(emrItem);
                        _dbContext.SaveChanges();
                        return patientCaseInfoDto;
                    }
                }
            }
            else
            {
                foreach (EMRItemDetail emrItemDetail in emrItemDetails)
                {
                    _dbContext.Set<EMRItemDetail>().Add(emrItemDetail);
                }

                _dbContext.Set<EMRItem>().Add(emrItem);
                _dbContext.SaveChanges();
                return patientCaseInfoDto;
            }

            return null;
        }

        public PatientCaseInfoDto ExamInfoItemEdited(PatientCaseInfoDto patientCaseInfoDto,string userID)
        {
            if (patientCaseInfoDto.newEMRItems == null || patientCaseInfoDto.newEMRItems.Count == 0)
            {
                return null;
            }

            List<RegEMRItemFileDto> regEMRItemFiles = new List<RegEMRItemFileDto>();
            List<EMRItemDetail> emrItemDetails = new List<EMRItemDetail>();

            HospitalProfileDto hospitalProfileDto = _ConsultationConfigurationService.GetHospital(userID);
            string dicomPrefix = hospitalProfileDto.DicomPrefix;
            if (string.IsNullOrEmpty(dicomPrefix))
            {
                dicomPrefix = hospitalProfileDto.UniqueID.Substring(0, 1) + "-";
            }

            string itemUID = patientCaseInfoDto.newEMRItems[0].UniqueID;
            EMRItem emrItem = _dbContext.Set<EMRItem>().FirstOrDefault(e => e.UniqueID == itemUID);
            if (emrItem != null)
            {
                Mapper.Map(patientCaseInfoDto.newEMRItems[0], emrItem);
                emrItem.PatientCaseID = patientCaseInfoDto.UniqueID;
                emrItem.LastEditUser = userID;
                emrItem.LastEditTime = DateTime.Now;

                if (patientCaseInfoDto.newEMRItems[0].ItemFiles != null && patientCaseInfoDto.newEMRItems[0].ItemFiles.Any())
                {
                    foreach (NewEMRItemFileDto newEMRItemFileDto in patientCaseInfoDto.newEMRItems[0].ItemFiles)
                    {
                        if (newEMRItemFileDto.UniqueID != null)
                        {
                            continue;
                        }
                        string detailID = string.IsNullOrEmpty(newEMRItemFileDto.DetailedId) ? Guid.NewGuid().ToString() : newEMRItemFileDto.DetailedId;

                        newEMRItemFileDto.UniqueID = detailID;
                        RegEMRItemFileDto regEMRItemFile = new RegEMRItemFileDto
                        {
                            UniqueID = detailID,
                            ItemType = newEMRItemFileDto.FileType == "folder" ? 1 : 0,
                            FileName = newEMRItemFileDto.FileName,
                            SrcFilePath = newEMRItemFileDto.Path,
                            SrcInfo = newEMRItemFileDto.SrcInfo,
                            DicomPrefix = dicomPrefix,
                            CreatorID = userID
                        };
                        regEMRItemFiles.Add(regEMRItemFile);

                        EMRItemDetail emrItemDetail = new EMRItemDetail
                        {
                            UniqueID = Guid.NewGuid().ToString(),
                            EMRItemID = emrItem.UniqueID,
                            DetailID = detailID,
                            LastEditUser = userID,
                            LastEditTime = DateTime.Now
                        };

                        emrItemDetails.Add(emrItemDetail);
                    }
                }

                if (patientCaseInfoDto.IsMobile)
                {
                    patientCaseInfoDto.Progress = 100;
                    DAMInfoDto damInfo = _ConsultationConfigurationService.GetDam();
                    foreach (EMRItemDetail emrItemDetail in emrItemDetails)
                    {
                        emrItemDetail.DamID = damInfo.UniqueID;
                        _dbContext.Set<EMRItemDetail>().Add(emrItemDetail);
                    }
                    _dbContext.SaveChanges();
                    return patientCaseInfoDto;
                }

                if (regEMRItemFiles.Count > 0)
                {
                    DAMInfoDto damInfo = _ConsultationConfigurationService.GetDam();
                    using (var client = new HttpClient())
                    {
                        var parameters = "/api/v1/registration/newitemlist";
                        HttpResponseMessage response = client.PostAsync<List<RegEMRItemFileDto>>(damInfo.WebApiUrl + parameters, regEMRItemFiles, new JsonMediaTypeFormatter()).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            foreach (EMRItemDetail emrItemDetail in emrItemDetails)
                            {
                                emrItemDetail.DamID = damInfo.UniqueID;
                                _dbContext.Set<EMRItemDetail>().Add(emrItemDetail);
                            }
                            _dbContext.SaveChanges();
                            return patientCaseInfoDto;
                        }
                    }
                }
                else
                {
                    foreach (EMRItemDetail emrItemDetail in emrItemDetails)
                    {
                        _dbContext.Set<EMRItemDetail>().Add(emrItemDetail);
                    }

                    _dbContext.SaveChanges();
                    return patientCaseInfoDto;
                }


            }
            return null;
        }

        public bool ReUploadPatientCase(string id)
        {
            var query = (from p in _dbContext.Set<PatientCase>()
                         join e in _dbContext.Set<EMRItem>()
                             on p.UniqueID equals e.PatientCaseID
                         join d in _dbContext.Set<EMRItemDetail>()
                             on e.UniqueID equals d.EMRItemID
                         where p.UniqueID == id
                         select d.DetailID);
            List<string> ids = query.ToList();


            return ReUploadProcess(ids);
        }

        public bool ReUploadExamItem(string id)
        {
            var query = (from e in _dbContext.Set<EMRItem>()
                         join d in _dbContext.Set<EMRItemDetail>()
                             on e.UniqueID equals d.EMRItemID
                         where e.UniqueID == id
                         select d.DetailID);
            List<string> ids = query.ToList();


            return ReUploadProcess(ids);
        }

        public bool ReUploadFileItem(string id)
        {
            return ReUploadProcess(new List<string> { id });
        }

        private bool ReUploadProcess(List<string> ids)
        {
            if (ids.Count > 0)
            {
                DAMInfoDto damInfo = _ConsultationConfigurationService.GetDam();
                using (var client = new HttpClient())
                {
                    var parameters = "/api/v1/upload/reupload";
                    HttpResponseMessage response = client.PostAsync<List<string>>(damInfo.WebApiUrl + parameters, ids, new JsonMediaTypeFormatter()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Dictionary<string, bool> DICOMPatientCaseRelations(List<string> dicoms, string userID)
        {
            Dictionary<string, bool> retInfo = new Dictionary<string, bool>();
            DAMInfo damInfo = GetDAMInfoByUserID(userID);
            if (dicoms != null && dicoms.Count > 0 && damInfo != null)
            {
                using (var client = new HttpClient())
                {
                    var parameters = "/api/v1/registration/regitemidsfordicom";
                    HttpResponseMessage response = client.PostAsync<List<string>>(damInfo.WebApiUrl + parameters, dicoms, new JsonMediaTypeFormatter()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var result = response.Content.ReadAsAsync<Dictionary<string, List<string>>>().Result;
                        foreach (string key in result.Keys)
                        {
                            bool hasRelation = false;

                            if (result[key] != null && result[key].Count > 0)
                            {
                                List<string> detailIDs = result[key];
                                int count = (from p in _dbContext.Set<PatientCase>()
                                             join e in _dbContext.Set<EMRItem>() on p.UniqueID equals e.PatientCaseID
                                             join i in _dbContext.Set<EMRItemDetail>() on e.UniqueID equals i.EMRItemID
                                             where detailIDs.Contains(i.DetailID) && p.IsDeleted != 1
                                             select p).Count();
                                if (count > 0)
                                {
                                    hasRelation = true;
                                }
                            }

                            if (!retInfo.ContainsKey(key))
                            {
                                retInfo.Add(key, hasRelation);
                            }
                        }

                    }
                }
            }

            return retInfo;
        }

        private DAMInfo GetDAMInfoByUserID(string userID)
        {
            DAMInfo damInfo = null;
            UserExtention userExtention = _dbContext.Set<UserExtention>().FirstOrDefault(u => u.UniqueID == userID);
            if (userExtention != null && !string.IsNullOrEmpty(userExtention.HospitalID))
            {
                HospitalProfile hospitalProfile = _dbContext.Set<HospitalProfile>().FirstOrDefault(h => h.UniqueID == userExtention.HospitalID);
                if (!string.IsNullOrEmpty(hospitalProfile.Dam1ID))
                {
                    damInfo = _dbContext.Set<DAMInfo>().FirstOrDefault(h => h.UniqueID == hospitalProfile.Dam1ID);
                }
                else
                {
                    damInfo = _dbContext.Set<DAMInfo>().FirstOrDefault();
                }
            }

            return damInfo;
        }

        /// <summary>
        /// Get case info by id,maybe the id is procedureId or orderId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic GetCaseInfoFromRis(string id)
        {
            //determine wheatehr the id is orderId or procedureId
            var orderId = id;
            var deleted = 0;
            var order = _risProContext.Set<Order>().FirstOrDefault(p => p.UniqueID.Equals(id));
            var exitProcedure = _risProContext.Set<Procedure>().FirstOrDefault(p => p.UniqueID.Equals(id));
            if (order != null)
            {
                orderId = order.UniqueID;
            }
            else if (exitProcedure != null)
            {
                orderId = exitProcedure.OrderID;
            }
            var existCase = _dbContext.Set<PatientCase>().FirstOrDefault(p => p.OrderID.Equals(orderId) && p.IsDeleted == deleted);
            if (existCase != null)
            {
                var patientCaseInfo = GetPatientCaseNoItems(existCase.UniqueID);
                return new
                {
                    CaseInfo = patientCaseInfo,
                    IsExistPatientCase = true
                };
            }
            var procedures = from p in _risProContext.Set<Procedure>()
                             where p.OrderID.Equals(orderId)
                             orderby p.ExamineTime
                             select new
                             {
                                 p.ExamineTime,
                                 p.BodyPart,
                                 p.RPDesc
                             };

            var caseInfo = (from o in _risProContext.Set<Order>()
                            join p in _risProContext.Set<Patient>() on o.PatientID equals p.UniqueID
                            where o.UniqueID == orderId
                            select new
                            {
                                o.AccNo,
                                o.HealthHistory,
                                o.Observation,
                                p.LocalName,
                                p.ReferenceNo,
                                p.SocialSecurityNo,
                                p.MedicareNo,
                                p.Gender,
                                p.Birthday,
                                p.Telephone,
                                p.Address,
                                p.PatientNo,
                                Procedures = procedures
                            }).Take(1).FirstOrDefault();
            var insurance = string.Empty;
            if (!string.IsNullOrEmpty(caseInfo.MedicareNo) && !string.IsNullOrEmpty(caseInfo.SocialSecurityNo))
            {
                insurance = caseInfo.MedicareNo + "/" + caseInfo.SocialSecurityNo;
            }
            else
            {
                insurance = !string.IsNullOrEmpty(caseInfo.MedicareNo) ? caseInfo.MedicareNo : caseInfo.SocialSecurityNo;
            }
            if (caseInfo != null)
            {
                var patientCase = new
                {
                    patientName = caseInfo.LocalName,
                    gender = caseInfo.Gender,
                    birthday = caseInfo.Birthday,
                    identityCard = caseInfo.ReferenceNo,
                    insuranceNumber = insurance,
                    telephone = caseInfo.Telephone,
                    address = caseInfo.Address,
                    history = caseInfo.HealthHistory,
                    clinicalDiagnosis = caseInfo.Observation,
                    orderID = orderId
                };
                var procedure = caseInfo.Procedures.ToList().FirstOrDefault();
                var modulesInitValue = new
                {
                    patientNo = caseInfo.PatientNo,
                    accessionNo = caseInfo.AccNo,
                    examDate = procedure != null && procedure.ExamineTime.HasValue ? procedure.ExamineTime.Value.ToString("yyyy/MM/dd") : null,
                    examDescription = string.Join(",", procedures.Select(p => p.RPDesc).Distinct().ToArray()),
                    bodyPart = string.Join(",", procedures.Select(p => p.BodyPart).Distinct().ToArray())
                };
                return new
                {
                    CaseInfo = new { patientCase = patientCase, modulesInitValue = modulesInitValue },
                    IsExistPatientCase = false
                };
            }
            return null;
        }

        public List<ConsultationResultDto> GetConsultationResult(string orderId)
        {
            var patientCase = _PatientCaseRepository.Get(p => p.OrderID.Equals(orderId)).FirstOrDefault();
            if (patientCase == null)
            {
                return null;
            }
            var person = (from p in _dbContext.Set<Person>()
                          join pr in _dbContext.Set<PersonPatientCase>()
                              on p.UniqueID equals pr.PersonID
                          where pr.PatientCaseID.Equals(patientCase.UniqueID)
                          select p).FirstOrDefault();

            var patientNo = person.PatientNo;
            var result = from c in _dbContext.Set<ConsultationRequest>()
                         join p in _dbContext.Set<ConsultationReport>() on c.UniqueID equals p.RequestID into reports
                         from p in reports.DefaultIfEmpty()
                         join h in _dbContext.Set<HospitalProfile>() on c.ReceiveHospitalID equals h.UniqueID into hospitals
                         from h in hospitals.DefaultIfEmpty()
                         where patientCase.UniqueID.Equals(c.PatientCaseID)
                         orderby c.RequestCreateDate
                         select new ConsultationResultDto
                            {
                                UniqueID = c.UniqueID,
                                ConsultationNo = patientNo,
                                Status = c.Status,
                                IsDeleted = c.IsDeleted,
                                RequestDate = c.RequestCreateDate,
                                RequestHospital = h.HospitalName,
                                ConsultationDate = c.ConsultationDate.Value,
                                Advice = p.Advice,
                                Description = p.Description
                            };
            return result.ToList();
        }

        private static Person InitialPerson(string patientNo)
        {
            return new Person
            {
                UniqueID = Guid.NewGuid().ToString(),
                PatientNo = patientNo
            };
        }

        private static PersonPatientCase InitialPersonPatientCase(string personId, string patienCaseId,string userID)
        {
            return new PersonPatientCase
            {
                UniqueID = Guid.NewGuid().ToString(),
                PersonID = personId,
                PatientCaseID = patienCaseId,
                LastEditUser = userID,
                LastEditTime = DateTime.Now
            };
        }

        private static PatientCase InitialPersonPatientCase(PatientCaseInfoDto patientCaseDto, string patientCaseId, string hospitalId, string userID, string localName)
        {
            var patientCase = Mapper.Map<PatientCaseInfoDto, PatientCase>(patientCaseDto);
            patientCase.UniqueID = patientCaseId;
            patientCase.Creator = userID;
            patientCase.CreatorName = localName;
            patientCase.CreateTime = DateTime.Now;
            patientCase.LastEditUser = userID;
            patientCase.LastEditTime = DateTime.Now;
            patientCase.PatientNamePy = PinyinUtil.ToPinyin(patientCase.PatientName, false, 0, "");
            patientCase.HospitalId = hospitalId;
            patientCase.Progress = 0;
            patientCase.Status = (int)PatientCaseStatus.NotApply;

            return patientCase;
        }

        private void BuildEMRItem(List<EMRItem> emrItems,
            List<EMRItemDetail> emrItemDetails,
            List<RegEMRItemFileDto> regEMRItemFiles,
            HospitalProfileDto hospitalProfileDto,
            List<NewEMRItemDto> newEmrItemDtos,
            string patientCaseID,
            string userID)
        {
            foreach (NewEMRItemDto newEMRItemDto in newEmrItemDtos)
            {
                EMRItem emrItem = Mapper.Map<NewEMRItemDto, EMRItem>(newEMRItemDto);
                emrItem.UniqueID = Guid.NewGuid().ToString();
                emrItem.PatientCaseID = patientCaseID;
                emrItem.LastEditUser = userID;
                emrItem.LastEditTime = DateTime.Now;
                emrItems.Add(emrItem);

                if (newEMRItemDto.ItemFiles != null && newEMRItemDto.ItemFiles.Any())
                {
                    foreach (var newEMRItemFileDto in newEMRItemDto.ItemFiles)
                    {
                        var detailId = string.IsNullOrEmpty(newEMRItemFileDto.DetailedId) ? Guid.NewGuid().ToString() : newEMRItemFileDto.DetailedId;
                        var regEMRItemFile = new RegEMRItemFileDto
                        {
                            UniqueID = detailId,
                            ItemType = newEMRItemFileDto.FileType == "folder" ? 1 : 0,
                            FileName = newEMRItemFileDto.FileName,
                            SrcFilePath = newEMRItemFileDto.Path,
                            SrcInfo = newEMRItemFileDto.SrcInfo,
                            DicomPrefix = string.IsNullOrEmpty(hospitalProfileDto.DicomPrefix) ? hospitalProfileDto.UniqueID.Substring(0, 1) + "-" : hospitalProfileDto.DicomPrefix,
                            CreatorID = userID,
                        };
                        if (newEMRItemFileDto.IsFromRis != null && newEMRItemFileDto.IsFromRis.Value)
                        {
                            var study = new StudyDto()
                            {
                                AccessionNo = newEMRItemFileDto.PacsAccessionNo,
                                PatientID = newEMRItemFileDto.PacsPatientId
                            };
                            regEMRItemFile.IsFromRis = newEMRItemFileDto.IsFromRis;
                            regEMRItemFile.Study = study;
                        }

                        regEMRItemFiles.Add(regEMRItemFile);

                        var emrItemDetail = new EMRItemDetail
                        {
                            UniqueID = Guid.NewGuid().ToString(),
                            EMRItemID = emrItem.UniqueID,
                            DetailID = detailId,
                            LastEditUser = userID,
                            LastEditTime = DateTime.Now
                        };
                        emrItemDetails.Add(emrItemDetail);
                    }
                }
            }
        }
    }
}
