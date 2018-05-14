using System;
using System.Data;
using AutoMapper;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Interface;
using Hys.Consultation.EntityFramework;
using Hys.Consultation.Application.Dtos;
using Hys.Platform.Application;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hys.CrossCutting.Common.Utils;
using Hys.CareRIS.Domain.Entities;
using Hys.CareRIS.EntityFramework;
using Role = Hys.Consultation.Domain.Entities.Role;
using Hys.Consultation.Domain.Enums;
using System.Transactions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Hys.Consultation.Application.Services.ServiceImpl
{
    public class ConsultationConfigurationService : DisposableServiceBase, IConsultationConfigurationService
    {
        private readonly IConsultationDictionaryRepository _ConsultationDictionaryRepository;
        private readonly IExamModuleRepository _ExamModuleRepository;
        private readonly IConsultationContext _DBContext;
        private readonly IRisProContext _RisProContext;
        private readonly ILoginUserService _LoginUserService;
        private const string DefaultLang = "en-us";

        public ConsultationConfigurationService(IConsultationDictionaryRepository consultationDictionaryRepository, IExamModuleRepository examModuleRepository,
            IConsultationContext consultationContext, IRisProContext risProContext, ILoginUserService loginUserService)
        {
            _ConsultationDictionaryRepository = consultationDictionaryRepository;
            _ExamModuleRepository = examModuleRepository;
            _DBContext = consultationContext;
            _RisProContext = risProContext;
            _LoginUserService = loginUserService;

            AddDisposableObject(consultationDictionaryRepository);
            AddDisposableObject(examModuleRepository);
            AddDisposableObject(consultationContext);
        }

        public IEnumerable<ConsultationDictionaryDto> GetAllDictionaries(string lang)
        {
            var result = new List<ConsultationDictionaryDto>();
            using (var sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" + lang + "\\consultationDic.json"))
            {
                var objects = JsonConvert.DeserializeObject<List<ConsultationDictionary>>(sr.ReadToEnd());

                result.AddRange(objects.Select(Mapper.Map<ConsultationDictionary, ConsultationDictionaryDto>));
            }

            return result;
        }

        public IEnumerable<ExamModuleDto> GetDefaultExamModule(string lang)
        {
            IEnumerable<ExamModuleDto> modules;
            using (var sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" +
   lang + "\\examModule.json"))
            {
                var objects = JsonConvert.DeserializeObject<List<ExamModule>>(sr.ReadToEnd());
                modules = objects.Select(Mapper.Map<ExamModule, ExamModuleDto>);
            }

            return modules;
        }

        public List<ServiceTypeDto> GetServiceType(string lang)
        {
            using (var sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" + lang + "\\serviceType.json"))
            {
                var objects = JsonConvert.DeserializeObject<List<ServiceType>>(sr.ReadToEnd());
                return objects.Select(Mapper.Map<ServiceType, ServiceTypeDto>).ToList();
            }
        }

        [Obsolete("This method read data from database")]
        public IEnumerable<ConsultationDictionaryDto> GetDictionaryByTypeFromDb(int type)
        {
            var dictionaries = _ConsultationDictionaryRepository.Get().Where(w => w.Type == (DictionaryType)type).OrderBy(d => d.Value)
                .Select(Mapper.Map<ConsultationDictionary, ConsultationDictionaryDto>);

            return dictionaries;
        }

        public IEnumerable<ConsultationDictionaryDto> GetDictionaryByType(int type, string language)
        {
            using (var sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" +
                (!string.IsNullOrEmpty(language) ? language : DefaultLang) + "\\consultationDic.json"))
            {
                var objects = JsonConvert.DeserializeObject<List<ConsultationDictionary>>(sr.ReadToEnd());

                var dictionaries = objects.Where(w => w.Type == (DictionaryType)type).OrderBy(d => d.Value)
                    .Select(Mapper.Map<ConsultationDictionary, ConsultationDictionaryDto>);

                return dictionaries;
            }
        }

        [Obsolete("This method read data from database")]
        public IEnumerable<ConsultationDictionaryDtos> GetDictionaryByTypesFromDb(int[] types)
        {
            var result = new List<ConsultationDictionaryDtos>();
            if (types != null)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    var dictionaries = _ConsultationDictionaryRepository.Get().Where(d => d.Type == (DictionaryType)types[i])
                        .Select(Mapper.Map<ConsultationDictionary, ConsultationDictionaryDto>).ToList();
                    result.Add(new ConsultationDictionaryDtos
                    {
                        Type = types[i],
                        Dictionaries = dictionaries
                    });
                }

            }
            return result;
        }

        public IEnumerable<ConsultationDictionaryDtos> GetDictionaryByTypes(int[] types, string language)
        {
            var result = new List<ConsultationDictionaryDtos>();

            if (types != null)
            {
                using (var sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" +
                    (!string.IsNullOrEmpty(language) ? language : DefaultLang) + "\\consultationDic.json"))
                {
                    var objects = JsonConvert.DeserializeObject<List<ConsultationDictionary>>(sr.ReadToEnd());
                    result.AddRange(from t in types
                                    let dictionaries = objects.Where(d => d.Type == (DictionaryType)t).
                                    Select(Mapper.Map<ConsultationDictionary, ConsultationDictionaryDto>).ToList()
                                    select new ConsultationDictionaryDtos
                                    {
                                        Type = t,
                                        Dictionaries = dictionaries
                                    });
                }
            }

            return result;
        }

        [Obsolete("This method read the default data from database")]
        public IEnumerable<ExamModuleDto> GetUserExamModuleFromDb(string userID)
        {
            var user = userID;
            var modules = _ExamModuleRepository.Get(g => g.Owner == user).Select(s => Mapper.Map<ExamModule, ExamModuleDto>(s));

            if (modules == null || modules.Count() < 1)
            {
                modules = _ExamModuleRepository.Get(g => g.Owner == "").Select(s => Mapper.Map<ExamModule, ExamModuleDto>(s));
                foreach (var module in modules)
                {
                    ExamModule tmpModule = new ExamModule
                    {
                        LastEditTime = DateTime.Now,
                        LastEditUser = user,
                        Owner = user,
                        Position = module.Position,
                        Title = module.Title,
                        Type = module.Type,
                        Visible = module.Visible
                    };
                    _ExamModuleRepository.Add(tmpModule);
                }
                _ExamModuleRepository.SaveChanges();
            }

            return modules;
        }


        public IEnumerable<ExamModuleDto> GetUserExamModule(string userID, string language)
        {
            var user = userID;
            var modules = _ExamModuleRepository.Get(g => g.Owner == user).Select(s => Mapper.Map<ExamModule, ExamModuleDto>(s));

            if (modules == null || modules.Count() < 1)
            {
                using (var sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" +
                    (!string.IsNullOrEmpty(language) ? language : DefaultLang) + "\\examModule.json"))
                {
                    var objects = JsonConvert.DeserializeObject<List<ExamModule>>(sr.ReadToEnd());
                    modules = objects.Select(Mapper.Map<ExamModule, ExamModuleDto>);
                }

                foreach (var module in modules)
                {
                    ExamModule tmpModule = new ExamModule
                    {
                        LastEditTime = DateTime.Now,
                        LastEditUser = user,
                        Owner = user,
                        Position = module.Position,
                        Title = module.Title,
                        Type = module.Type,
                        Visible = module.Visible
                    };
                    _ExamModuleRepository.Add(tmpModule);
                }
                _ExamModuleRepository.SaveChanges();
            }

            return modules;
        }

        [Obsolete("This method read the default data from database")]
        public IEnumerable<ExamModuleDto> GetExamModuleFromDb(string owner, out bool isNew)
        {
            var validOwner = !string.IsNullOrEmpty(owner);
            IEnumerable<ExamModuleDto> modules = null;
            isNew = false;
            if (validOwner)
            {
                modules = _ExamModuleRepository.Get(g => g.Owner == owner).Select(s => Mapper.Map<ExamModule, ExamModuleDto>(s));
            }

            if (modules == null || modules.Count() < 1)
            {
                modules = _ExamModuleRepository.Get(g => g.Owner == "").Select(s => Mapper.Map<ExamModule, ExamModuleDto>(s));
                isNew = true;
            }

            return modules;
        }

        public IEnumerable<ExamModuleDto> GetExamModule(string owner)
        {
            if (!string.IsNullOrEmpty(owner))
            {
               return _ExamModuleRepository.Get(g => g.Owner == owner).Select(Mapper.Map<ExamModule, ExamModuleDto>);
            }

            return new List<ExamModuleDto>();
        }


        public bool AddExamModules(IEnumerable<ExamModuleDto> modules,string userID)
        {
            if (modules == null || modules.Count() < 1) return true;

            var user = userID;

            foreach (var module in modules)
            {
                if (string.IsNullOrEmpty(module.Owner))
                    throw new Exception("Module must have an owner.");

                var tmpModule = new ExamModule();
                Mapper.Map(module, tmpModule);
                tmpModule.LastEditUser = user;
                tmpModule.LastEditTime = DateTime.Now;

                _ExamModuleRepository.Add(tmpModule);
            }
            _ExamModuleRepository.SaveChanges();
            return true;
        }
        public bool UpdateExamModule(ExamModuleDto module,string userID)
        {
            var user = userID;
            var dbModule = _ExamModuleRepository.Get(m => m.ID == module.ID).FirstOrDefault();
            if (dbModule == null) return false;
            Mapper.Map(module, dbModule);

            dbModule.LastEditTime = DateTime.Now;
            dbModule.LastEditUser = user;

            _ExamModuleRepository.SaveChanges();
            return true;
        }
        public bool UpdateExamModules(IEnumerable<ExamModuleDto> modules,string userID)
        {
            if (modules == null || modules.Count() < 1) return true;
            var user = userID;
            foreach (var module in modules)
            {
                var dbModule = _ExamModuleRepository.Get(m => m.ID == module.ID).FirstOrDefault();
                if (dbModule == null) continue;
                Mapper.Map(module, dbModule);

                dbModule.LastEditTime = DateTime.Now;
                dbModule.LastEditUser = user;
            }

            _ExamModuleRepository.SaveChanges();
            return true;
        }

        public HospitalProfileDto GetHospital(string userID)
        {
            //get hos info by userid
            HospitalProfile hospitalProfile = (from h in _DBContext.Set<HospitalProfile>()
                                               join o in _DBContext.Set<UserExtention>() on h.UniqueID equals o.HospitalID
                                               where o.UniqueID == userID
                                               select h).FirstOrDefault();

            if (hospitalProfile != null)
            {
                return Mapper.Map<HospitalProfile, HospitalProfileDto>(hospitalProfile);
            }

            return new HospitalProfileDto { UniqueID = "1" };
        }

        public async Task<HospitalProfileDto> GetHospitalAsync(string userID)
        {
            var hospitalProfile = await (from h in _DBContext.Set<HospitalProfile>()
                                         join o in _DBContext.Set<UserExtention>() on h.UniqueID equals o.HospitalID
                                         where o.UniqueID == userID
                                         select h).FirstOrDefaultAsync();

            return hospitalProfile != null ? Mapper.Map<HospitalProfile, HospitalProfileDto>(hospitalProfile) : new HospitalProfileDto { UniqueID = "1" };
        }

        public DAMInfoDto GetDam()
        {
            DAMInfo damInfo = _DBContext.Set<DAMInfo>().FirstOrDefault();
            return damInfo != null ? Mapper.Map<DAMInfo, DAMInfoDto>(damInfo) : new DAMInfoDto { UniqueID = "1" };
        }

        public async Task<string> GetDamIdAsync()
        {
            var damInfo = await _DBContext.Set<DAMInfo>().FirstOrDefaultAsync();
            return damInfo != null ? damInfo.UniqueID : "1";
        }

        public List<DAMInfoDto> GetDams()
        {
            return _DBContext.Set<DAMInfo>().ToList().Select(Mapper.Map<DAMInfo, DAMInfoDto>).ToList();
        }

        public DAMInfoDto GetDamByID(string id)
        {
            DAMInfo damInfo = _DBContext.Set<DAMInfo>().FirstOrDefault(d => d.UniqueID == id);
            return damInfo != null ? Mapper.Map<DAMInfo, DAMInfoDto>(damInfo) : null;
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            var roles = _DBContext.Set<Role>().Where(r => !r.IsDeleted).OrderByDescending(r => r.LastEditTime).ToList();
            return roles.Select(Mapper.Map<Role, RoleDto>);
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await _DBContext.Set<Role>().Where(r => !r.IsDeleted).OrderByDescending(r => r.LastEditTime).AsNoTracking().ToListAsync();
            return roles.Select(Mapper.Map<Role, RoleDto>);
        }

        public bool SaveRole(RoleDto role)
        {
            //distinc permissions
            if (!String.IsNullOrEmpty(role.Permissions))
            {
                role.Permissions = String.Join(",", role.Permissions.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct());
            }

            var roles = _DBContext.Set<Role>();
            var existingRole = roles.FirstOrDefault(r => r.UniqueID == role.UniqueID);
            if (existingRole == null)
            {
                roles.Add(Mapper.Map<RoleDto, Role>(role));
            }
            else
            {
                Mapper.Map(role, existingRole);
            }
            _DBContext.SaveChanges();
            return true;
        }

        public IEnumerable<DepartmentDto> GetDepartments()
        {
            return _DBContext.Set<Department>().ToList().Select(Mapper.Map<Department, DepartmentDto>);
        }

        [Obsolete("This method read data from database")]
        public List<ServiceTypeDto> GetServiceTypeFromDb()
        {
            return _DBContext.Set<ServiceType>().ToList().Select(Mapper.Map<ServiceType, ServiceTypeDto>).ToList();
        }

        public IEnumerable<HospitalDefaultDto> GetRecipientConfigs()
        {
            var configs = _DBContext.Set<HospitalDefault>().Where(h => !h.IsDeleted);
            if (!_LoginUserService.IsSystemAdmin)
            {
                configs = configs.Where(c => c.Owner == _LoginUserService.DefaultSiteID);
            }
            return configs.ToList().Select(item =>
            {
                var config = Mapper.Map<HospitalDefault, HospitalDefaultDto>(item);
                config.RequestName = GetHospitalDefaultData(config.RequestType, config.RequestID);
                config.ResponseName = GetHospitalDefaultData(config.ResponseType, config.ResponseID);
                return config;
            });
        }

        public IEnumerable<HospitalDefaultDto> GetRecipientConfigsForReceiver(string userID)
        {
            var configs = _DBContext.Set<HospitalDefault>().Where(h => !h.IsDeleted);

            var configFilterUser = configs.Where(c => c.RequestType == 1 && c.RequestID == userID);
            if (configFilterUser.Count() > 0)
            {
                return configFilterUser.ToList().Select(item =>
                {
                    var config = Mapper.Map<HospitalDefault, HospitalDefaultDto>(item);
                    config.RequestName = GetHospitalDefaultData(config.RequestType, config.RequestID);
                    config.ResponseName = GetHospitalDefaultData(config.ResponseType, config.ResponseID);
                    return config;
                });
            }
            else
            {
                var configFilterHospital = configs.Where(c => c.RequestType == 0 && c.RequestID == _LoginUserService.DefaultSiteID);

                return configFilterHospital.ToList().Select(item =>
                {
                    var config = Mapper.Map<HospitalDefault, HospitalDefaultDto>(item);
                    config.RequestName = GetHospitalDefaultData(config.RequestType, config.RequestID);
                    config.ResponseName = GetHospitalDefaultData(config.ResponseType, config.ResponseID);
                    return config;
                });

            }
        }

        private string GetHospitalDefaultData(HospitalDefaultType type, string id)
        {
            switch (type)
            {
                case HospitalDefaultType.Hospital:
                    return _DBContext.Set<HospitalProfile>().Where(h => h.UniqueID == id).Select(h => h.HospitalName).FirstOrDefault();
                case HospitalDefaultType.Expert:
                    return _RisProContext.Set<User>().Where(h => h.UniqueID == id).Select(h => h.LocalName).FirstOrDefault();
            }
            return String.Empty;
        }

        public bool SaveRecipientConfigs(HospitalDefaultDto hospitalDefaultDto,string userID)
        {
            hospitalDefaultDto.LastEditTime = DateTime.Now;
            hospitalDefaultDto.LastEditUser = userID;
            hospitalDefaultDto.Owner = _LoginUserService.DefaultSiteID;

            var config = _DBContext.Set<HospitalDefault>().FirstOrDefault(c => c.UniqueID == hospitalDefaultDto.UniqueID);
            if (config == null)
            {
                config = Mapper.Map<HospitalDefaultDto, HospitalDefault>(hospitalDefaultDto);
                _DBContext.Set<HospitalDefault>().Add(config);
            }
            else
            {
                Mapper.Map(hospitalDefaultDto, config);
            }
            _DBContext.SaveChanges();
            return true;
        }

        public List<HospitalDefaultDto> GetHospitalDefaultForHospital()
        {
            return _DBContext.Set<HospitalDefault>().Where(h => !h.IsDeleted && h.ResponseType == (int)HospitalDefaultType.Hospital).ToList().Select(Mapper.Map<HospitalDefault, HospitalDefaultDto>).ToList();
        }

        public List<HospitalDefaultDto> GetHospitalDefaultForExpert()
        {
            return _DBContext.Set<HospitalDefault>().Where(h => !h.IsDeleted && h.ResponseType == (int)HospitalDefaultType.Expert).ToList().Select(Mapper.Map<HospitalDefault, HospitalDefaultDto>).ToList();
        }

        public bool ValidateRoleName(string roleID, string roleName)
        {
            if (_DBContext.Set<Role>().Any(r => !r.IsDeleted && r.UniqueID != roleID && r.RoleName == roleName))
            {
                throw new DuplicateNameException("duplicate role name");
            }
            return true;
        }

        public string GeneratePatientNo(string userID)
        {
            string result = "";
            HospitalProfileDto hospitalProfileDto = GetHospital(userID);
            var hospitalID = hospitalProfileDto.UniqueID;
            string prefix = "";
            int maxLength = 0, currentValue = 0;
            ConsultationPatientNo consultationPatientNo = _DBContext.Set<ConsultationPatientNo>().FirstOrDefault(c => c.HospitalID == hospitalID);
            ConsultationContext consultationContext = (ConsultationContext)_DBContext;
            if (consultationPatientNo != null)
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                 new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    consultationPatientNo = consultationContext.Set<ConsultationPatientNo>().FirstOrDefault(c => c.HospitalID == hospitalID);
                    consultationPatientNo.CurrentValue++;
                    consultationContext.SaveChanges();

                    ts.Complete();
                }
                prefix = consultationPatientNo.Prefix;
                maxLength = consultationPatientNo.MaxLength;
                currentValue = consultationPatientNo.CurrentValue;
            }
            else
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required,
                 new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
                {
                    consultationPatientNo = consultationContext.Set<ConsultationPatientNo>().FirstOrDefault(c => c.HospitalID == null || c.HospitalID == "");
                    consultationPatientNo.CurrentValue++;
                    consultationContext.SaveChanges();

                    ts.Complete();
                }
                prefix = consultationPatientNo.Prefix;
                maxLength = consultationPatientNo.MaxLength;
                currentValue = consultationPatientNo.CurrentValue;
            }

            if (maxLength > 0 && currentValue > 0)
            {
                result = !string.IsNullOrEmpty(prefix)
                    ? prefix + currentValue.ToString().PadLeft(maxLength, '0')
                    : currentValue.ToString().PadLeft(maxLength, '0');
            }

            return result;
        }


        public async Task<string> GeneratePatientNoAsync(string userID)
        {
            var hospitalId = (await GetHospitalAsync(userID)).UniqueID;
            var consultationPatientNo = await _DBContext.Set<ConsultationPatientNo>().FirstOrDefaultAsync(c => c.HospitalID == hospitalId);

            using (var ts = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions() { IsolationLevel = System.Transactions.IsolationLevel.Serializable }))
            {
                var consultationPatientNoChanged =
                          _DBContext.Set<ConsultationPatientNo>()
                          .First(c => consultationPatientNo == null ? c.HospitalID == null || c.HospitalID == "" : c.HospitalID == hospitalId);
                consultationPatientNoChanged.CurrentValue++;
                _DBContext.SaveChanges();
                consultationPatientNo = consultationPatientNoChanged;
                ts.Complete();
            }

            if (consultationPatientNo.MaxLength > 0 && consultationPatientNo.CurrentValue > 0)
            {
                return !string.IsNullOrEmpty(consultationPatientNo.Prefix)
                      ? consultationPatientNo.Prefix + consultationPatientNo.CurrentValue.ToString().PadLeft(consultationPatientNo.MaxLength, '0')
                      : consultationPatientNo.CurrentValue.ToString().PadLeft(consultationPatientNo.MaxLength, '0');
            }

            return string.Empty;
        }
    }
}
