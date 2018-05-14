#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Server.Business.Oam.Impl;

namespace Server.Business.Oam
{
    public sealed class BusinessFactory
    {
        private static BusinessFactory instance = new BusinessFactory();
        private Hashtable flyWeightPool = new Hashtable();

        private BusinessFactory()
        {

        }

        public static BusinessFactory Instance
        {
            get
            {
                return instance;
            }
        }

        public IUserService GetUserService()
        {
            IUserService userService = flyWeightPool["UserService"] as IUserService;

            if (userService == null)
            {
                userService = new UserServiceImpl();
                flyWeightPool.Add("UserService", userService);
            }

            return userService;
        }

        public IRoleService GetRoleService()
        {
            IRoleService roleService = flyWeightPool["RoleService"] as IRoleService;

            if (roleService == null)
            {
                roleService = new RoleServiceImpl();
                flyWeightPool.Add("RoleService", roleService);
            }

            return roleService;
        }

        public ISystemProfileService GetSystemProfileService()
        {
            ISystemProfileService systemProfileService = flyWeightPool["SystemProfileService"] as ISystemProfileService;

            if (systemProfileService == null)
            {
                systemProfileService = new SystemProfileServiceImpl();
                flyWeightPool.Add("SystemProfileService", systemProfileService);
            }

            return systemProfileService;
        }

        public IClientConfigService GetClientConfigService()
        {
            IClientConfigService clientConfigService = flyWeightPool["ClientConfigService"] as IClientConfigService;

            if (clientConfigService == null)
            {
                clientConfigService = new ClientConfigServiceImpl();
                flyWeightPool.Add("ClientConfigService", clientConfigService);
            }

            return clientConfigService;
        }

        public IDictionaryService GetDictionaryService()
        {
            IDictionaryService dictionaryService = flyWeightPool["DictionaryService"] as IDictionaryService;

            if (dictionaryService == null)
            {
                dictionaryService = new DictionaryServiceImpl();
                flyWeightPool.Add("DictionaryService", dictionaryService);
            }

            return dictionaryService;
        }

        public IResourceService GetResourceService()
        {
            IResourceService resourceService = flyWeightPool["ResourceService"] as IResourceService;

            if (resourceService == null)
            {
                resourceService = new ResourceServcieImpl();
                flyWeightPool.Add("ResourceService", resourceService);
            }

            return resourceService;
        }

        public IProcedureCodeService GetProcedureCodeService()
        {
            IProcedureCodeService procedureCodeService = flyWeightPool["ProcedureCodeService"] as IProcedureCodeService;

            if (procedureCodeService == null)
            {
                procedureCodeService = new ProcedureCodeServiceImpl();
                flyWeightPool.Add("ProcedureCodeService", procedureCodeService);
            }

            return procedureCodeService;
        }

        public IScheduleService GetScheduleService()
        {
            IScheduleService scheduleService = flyWeightPool["ScheduleService"] as IScheduleService;

            if (scheduleService == null)
            {
                scheduleService = new ScheduleServiceImpl();
                flyWeightPool.Add("ScheduleService", scheduleService);
            }

            return scheduleService;
        }

        public IAnatomyService GetAnatomyService()
        {
            IAnatomyService anatomyService = flyWeightPool["AnatomyService"] as IAnatomyService;

            if (anatomyService == null)
            {
                anatomyService = new AnatomyServiceImpl();
                flyWeightPool.Add("AnatomyService", anatomyService);
            }

            return anatomyService;
        }

        public IPathologyService GetPathologyService()
        {
            IPathologyService pathologyService = flyWeightPool["PathologyService"] as IPathologyService;

            if (pathologyService == null)
            {
                pathologyService = new PathologyServiceImpl();
                flyWeightPool.Add("PathologyService", pathologyService);
            }

            return pathologyService;
        }

        public IACRCodeService GetACRCodeService()
        {
            IACRCodeService acrCodeService = flyWeightPool["ACRCodeService"] as IACRCodeService;

            if (acrCodeService == null)
            {
                acrCodeService = new ACRCodeServiceImpl();
                flyWeightPool.Add("ACRCodeService", acrCodeService);
            }

            return acrCodeService;
        }
        public ITemplateIEService GetTemplateIEService()
        {
            ITemplateIEService templateIEService = flyWeightPool["TemplateIEService"] as ITemplateIEService;
            if (templateIEService == null)
            {
                templateIEService = new TemplateIEServiceImpl();
                flyWeightPool.Add("TemplateIEService", templateIEService);
            }
            return templateIEService;
        }

        public IHippaQueryService GetHippaQueryService()
        {
            IHippaQueryService hippaQueryService = flyWeightPool["HippaQueryService"] as IHippaQueryService;
            if (hippaQueryService == null)
            {
                hippaQueryService = new HippaQueryServiceImpl();
                flyWeightPool.Add("HippaQueryService", hippaQueryService);
            }
            return hippaQueryService;
        }

        public IBulletinBoardService GetBulletinBoardService()
        {
            IBulletinBoardService bulletinBoardService = flyWeightPool["BulletinBoardService"] as IBulletinBoardService;
            if (bulletinBoardService == null)
            {
                bulletinBoardService = new BulletinBoardServiceImpl();
                flyWeightPool.Add("BulletinBoardService", bulletinBoardService);
            }
            return bulletinBoardService;
        }

        public IICD10Service GetICD10Service()
        {
            IICD10Service icd10Service = flyWeightPool["ICD10Service"] as IICD10Service;
            if (icd10Service == null)
            {
                icd10Service = new ICD10ServiceImpl();
                flyWeightPool.Add("ICD10Service", icd10Service);
            }
            return icd10Service;
        }

        public IQualityScoringService GetQualityScoringService()
        {
            IQualityScoringService qualityScoringService = flyWeightPool["QualityScoringService"] as IQualityScoringService;
            if (qualityScoringService == null)
            {
                qualityScoringService = new QualityScoringServiceImpl();
                flyWeightPool.Add("QualityScoringService", qualityScoringService);
            }
            return qualityScoringService;
        }

        public IKMSService GetKMSService()
        {
            IKMSService kmsService = flyWeightPool["KMSService"] as IKMSService;
            if (kmsService == null)
            {
                kmsService = new KMSServiceImpl();
                flyWeightPool.Add("KMSService", kmsService);
            }
            return kmsService;
        }

        public IConditionColService GetConditionColService()
        {
            IConditionColService conditionColService = flyWeightPool["ConditionColService"] as IConditionColService;
            if (conditionColService == null)
            {
                conditionColService = new ConditionColServiceImpl();
                flyWeightPool.Add("ConditionColService", conditionColService);
            }
            return conditionColService;
        }

        public IModalityTimeSliceService GetModalityTimeSliceService()
        {
            IModalityTimeSliceService modalityTimeSliceService = flyWeightPool["ModalityTimeSliceService"] as IModalityTimeSliceService;
            if (modalityTimeSliceService == null)
            {
                modalityTimeSliceService = new ModalityTimeSliceImpl();
                flyWeightPool.Add("ModalityTimeSliceService", modalityTimeSliceService);
            }
            return modalityTimeSliceService;
        }
        public IAdministratorToolService GetAdministratorToolService()
        {
            IAdministratorToolService atService = flyWeightPool["AdministratorToolService"] as IAdministratorToolService;

            if (atService == null)
            {
                atService = new AdministratorToolServiceImpl();
                flyWeightPool.Add("AdministratorToolService", atService);
            }

            return atService;
        }

        public IDomainService GetDomainService()
        {
            IDomainService atService = flyWeightPool["DomainService"] as IDomainService;

            if (atService == null)
            {
                atService = new DomainServiceImpl();
                flyWeightPool.Add("DomainService", atService);
            }

            return atService;
        }

        public IChargeCodeService GetChargeCodeService()
        {
            IChargeCodeService codeService = flyWeightPool["ChargeCodeService"] as IChargeCodeService;

            if (codeService == null)
            {
                codeService = new ChargeCodeServiceImpl();
                flyWeightPool.Add("ChargeCodeService", codeService);
            }

            return codeService;
        }

        public ILoginSettings GetLoginSettings()
        {
            ILoginSettings codeService = flyWeightPool["LoginSettings"] as ILoginSettings;

            if (codeService == null)
            {
                codeService = new LoginSettingsServiceImpl();
                flyWeightPool.Add("LoginSettings", codeService);
            }

            return codeService;
        }

        public IKeyPerformanceRatingService GetKeyPerformanceRating()
        {
            IKeyPerformanceRatingService codeService = flyWeightPool["GetKeyPerformanceRating"] as IKeyPerformanceRatingService;

            if (codeService == null)
            {
                codeService = new KeyPerformanceRatingServiceImpl();
                flyWeightPool.Add("GetKeyPerformanceRating", codeService);
            }

            return codeService;
        }

        public IRandomInspectionService GetRandomInspection()
        {
            IRandomInspectionService codeService = flyWeightPool["GetRandomInspection"] as IRandomInspectionService;

            if (codeService == null)
            {
                codeService = new RandomInspectionServiceImpl();
                flyWeightPool.Add("GetRandomInspection", codeService);
            }

            return codeService;
        }
    }
}
