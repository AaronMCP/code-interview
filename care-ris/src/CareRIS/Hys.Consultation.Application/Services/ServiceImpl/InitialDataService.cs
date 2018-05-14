using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Hys.Consultation.Application.Dtos;
using Hys.Consultation.Domain.Entities;
using Hys.Consultation.Domain.Enums;
using Hys.Consultation.EntityFramework;

namespace Hys.Consultation.Application.Services.ServiceImpl
{
    /// <summary>
    /// Initialize data for release 1.0.1.0
    /// Notice: 
    /// 1.The date time type must be a const 
    /// 2.The last edit user should be SupperAdminUserId.
    /// 3.Do not use new guid for any initial column.
    /// 4.Each release should have each seed method. The method name should have release version name like Seed_x_x_x_x
    /// </summary>
    public class InitialDataService : IInitialDataService
    {
        private const string SupperAdminUserId = "CAE0FCF9-EA87-4989-A76B-EFD95AEC2516";

        public void InitialData(ConsultationContext context, bool isV1Initialed)
        {
            Seed_1_0_0_1(context, isV1Initialed);

            Seed_1_0_1_0(context);
        }

        private static void Seed_1_0_0_1(ConsultationContext context, bool isV1Initialed)
        {
            var dateTime = new DateTime(2015, 9, 12);

            if (!isV1Initialed)
            {
                var version = context.InitialDataHistory.FirstOrDefault(i => i.UniqueID.Equals(Version.V1001.VersionId));
                if (version == null || !version.IsUpdated)
                {
                    #region DictionaryType.TimeRange

                    context.ConsultationDictionary.AddOrUpdate(
                        new ConsultationDictionary
                        {
                            DictionaryID = "240986EF-4B13-4040-9C4F-2F65338AF2F3",
                            Type = DictionaryType.TimeRange,
                            Name = "晚上 17:00-23:00",
                            Value = "3",
                            Description = "17:00|23:00",
                            LastEditUser = SupperAdminUserId,
                            LastEditTime = dateTime
                        }, new ConsultationDictionary
                        {
                            DictionaryID = "717CF241-C15B-48CE-A3C3-E53144F4D1D9",
                            Type = DictionaryType.TimeRange,
                            Name = "上午 9:00-12:00",
                            Value = "1",
                            Description = "9:00|12:00",
                            LastEditUser = SupperAdminUserId,
                            LastEditTime = new DateTime(2015, 9, 18)
                        }, new ConsultationDictionary
                        {
                            DictionaryID = "C3D4BE07-C4E4-4C6B-BD08-8285F1C211E1",
                            Type = DictionaryType.TimeRange,
                            Name = "下午 13:00-17:00",
                            Value = "2",
                            Description = "13:00|17:00",
                            LastEditUser = SupperAdminUserId,
                            LastEditTime = dateTime
                        });

                    #endregion

                    #region DictionaryType.RejectReason

                    context.ConsultationDictionary.AddOrUpdate(new ConsultationDictionary
                    {
                        DictionaryID = "4CDCEA79-D1CA-4C82-87DA-16BDA78B5404",
                        Type = DictionaryType.RejectReason,
                        Name = "资料不完整",
                        Value = "1",
                        Description = "拒绝原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "BD7CA588-864C-45DA-9B10-0E7893B1EB42",
                        Type = DictionaryType.RejectReason,
                        Name = "不在受理范围",
                        Value = "2",
                        Description = "拒绝原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "51E6AB3B-2AB9-4F78-9E54-D88C9DDE50CF",
                        Type = DictionaryType.RejectReason,
                        Name = "不能在允许时间内安排",
                        Value = "3",
                        Description = "拒绝原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "D9236F5C-F678-42EF-9FD5-FB11F7AB1451",
                        Type = DictionaryType.RejectReason,
                        Name = "其他原因",
                        Value = "4",
                        Description = "拒绝原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    #region DictionaryType.CancaleReason

                    context.ConsultationDictionary.AddOrUpdate(new ConsultationDictionary
                    {
                        DictionaryID = "8BD133FC-271A-4F81-83A6-D8962A2CCBB9",
                        Type = DictionaryType.CancaleReason,
                        Name = "病人要求",
                        Value = "1",
                        Description = "取消原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "9634562A-3126-460B-B109-6029BB85FB28",
                        Type = DictionaryType.CancaleReason,
                        Name = "通过其他途径完成诊断",
                        Value = "2",
                        Description = "取消原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "CAA809ED-E8D6-4F49-86F9-662EAC6FCB94",
                        Type = DictionaryType.CancaleReason,
                        Name = "其他原因",
                        Value = "3",
                        Description = "取消原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    #region DictionaryType.ApplyReconsiderReason

                    context.ConsultationDictionary.AddOrUpdate(new ConsultationDictionary
                    {
                        DictionaryID = "BD0DBB32-8E35-43AB-9B12-0825083178CD",
                        Type = DictionaryType.ApplyReconsiderReason,
                        Name = "不达要求",
                        Value = "1",
                        Description = "申请复议原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "FB981478-F0FB-4607-A434-877F5678D878",
                        Type = DictionaryType.ApplyReconsiderReason,
                        Name = "不合格",
                        Value = "2",
                        Description = "申请复议原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "73D49DC2-C0CB-4146-BB5E-3BCC13459BE2",
                        Type = DictionaryType.ApplyReconsiderReason,
                        Name = "不能在允许时间内安排",
                        Value = "3",
                        Description = "病人要求",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "BA691829-ADE0-47D9-A971-4C8807D0B63A",
                        Type = DictionaryType.ApplyReconsiderReason,
                        Name = "其他原因",
                        Value = "4",
                        Description = "申请复议原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    #region DictionaryType.TerminateReason

                    context.ConsultationDictionary.AddOrUpdate(new ConsultationDictionary
                    {
                        DictionaryID = "67DE2AA3-1026-4F2E-A90F-3795FD24AD60",
                        Type = DictionaryType.TerminateReason,
                        Name = "资料不完整",
                        Value = "1",
                        Description = "强制结束原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "B2BF2B54-AA5D-4DF1-84D1-54D681C338EE",
                        Type = DictionaryType.TerminateReason,
                        Name = "不能在允许时间内安排",
                        Value = "2",
                        Description = "强制结束原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "90F5811B-21D9-4CBA-A2EA-0F5F71EB25D7",
                        Type = DictionaryType.TerminateReason,
                        Name = "不在受理范围",
                        Value = "3",
                        Description = "强制结束原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "1094F218-F79D-403F-B986-95A87F45917C",
                        Type = DictionaryType.TerminateReason,
                        Name = "其他原因",
                        Value = "4",
                        Description = "强制结束原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ConsultationDictionary
                    {
                        DictionaryID = "3A827BBE-B8F2-43C0-A14C-6863EF03D2A0",
                        Type = DictionaryType.TerminateReason,
                        Name = "其他原因-强制结束原因",
                        Value = "5",
                        Description = "强制结束原因",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    #region Add default exam modules data

                    context.ExamModule.AddOrUpdate(new ExamModule
                    {
                        Owner = "",
                        Type = "teleradiology",
                        Title = "放射",
                        Position = "0,0,4,3,1",
                        Visible = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ExamModule
                    {
                        Owner = "",
                        Type = "test",
                        Title = "检验",
                        Position = "0,4,4,3,1",
                        Visible = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ExamModule
                    {
                        Owner = "",
                        Type = "ecg",
                        Title = "心电",
                        Position = "0,8,4,3,1",
                        Visible = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ExamModule
                    {
                        Owner = "",
                        Type = "ultrasound",
                        Title = "超声",
                        Position = "3,0,4,3,1",
                        Visible = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ExamModule
                    {
                        Owner = "",
                        Type = "pathology",
                        Title = "病理",
                        Position = "3,4,4,3,1",
                        Visible = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ExamModule
                    {
                        Owner = "",
                        Type = "other",
                        Title = "其他",
                        Position = "3,8,4,3,1",
                        Visible = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    #region service type

                    context.ServiceType.AddOrUpdate(new ServiceType
                    {
                        UniqueID = "1",
                        Name = "普通会诊",
                        Description = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new ServiceType
                    {
                        UniqueID = "2",
                        Name = "急会诊",
                        Description = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    #region role

                    context.Role.AddOrUpdate(new Role
                    {
                        UniqueID = "2ee2fd0c-100d-b934-d0c2-f24ff16039e9",
                        RoleName = "超级管理员",
                        Description = "超级管理员",
                        Status = true,
                        Permissions = "",
                        LastEditTime = dateTime,
                        IsDeleted = false,
                        IsSystem = true
                    }, new Role
                    {
                        UniqueID = "4d1fa440-28b7-b5d2-976f-9db7f84b5d9d",
                        RoleName = "远程专家",
                        Description = "远程专家",
                        Status = true,
                        Permissions =
                            "004ViewCase,010SearchCase,002ConsultationCenterViewInfo,015ViewConsultation,018PrintResult,004RefuseRequest,005StartMeeting,001ExpertViewInfo,002ExpertStartMeeting,003ExpertRefuseRequest",
                        LastEditTime = dateTime,
                        IsDeleted = false,
                        IsSystem = true
                    }, new Role
                    {
                        UniqueID = "4dc3dbb2-27e0-9eb1-a106-003cae158b16",
                        RoleName = "远程申请医生",
                        Description = "远程申请医生",
                        Status = true,
                        Permissions =
                            "001CreateNewCase,002CombineCase,003EditProcedure,004ViewCase,005EditCaseExceptCompleted,006EditPatientExceptCompleted,007EditHistoryAndDiagnosisExceptCompleted,008EditRequestAndRequirmentExceptCompleted,009EditReceiverNotAccepted,010SearchCase,011RequestConsultation,012SubmitRequest,013CancelRequest,014RemoteRequestForceEnd,015ViewConsultation,016RequestCancelConsultation,017RequestReconsideration,018PrintResult,002ConsultationCenterViewInfo,005StartMeeting",
                        LastEditTime = dateTime,
                        IsDeleted = false,
                        IsSystem = true
                    }, new Role
                    {
                        UniqueID = "93321a30-891f-6c86-6b4d-46f17f13dfae",
                        RoleName = "会诊管理员",
                        Description = "会诊管理员",
                        Status = true,
                        Permissions =
                            "003EditProcedure,004ViewCase,005EditCaseExceptCompleted,009EditReceiverNotAccepted,001EditReceiverAccept,010SearchCase,014RemoteRequestForceEnd,015ViewConsultation,018PrintResult,002ConsultationCenterViewInfo,004RefuseRequest,003AcceptRequest,005StartMeeting,001ExpertViewInfo,002ExpertStartMeeting,003ExpertRefuseRequest",
                        LastEditTime = dateTime,
                        IsDeleted = false,
                        IsSystem = true
                    }, new Role
                    {
                        UniqueID = "d6e52828-6c4f-2efe-c2f0-700de9375e75",
                        RoleName = "Site管理员",
                        Description = "Site管理员",
                        Status = true,
                        Permissions = "",
                        LastEditTime = dateTime,
                        IsDeleted = false,
                        IsSystem = true
                    });

                    context.SaveChanges();

                    #endregion

                    #region user extention and user link

                    var roles =
                        context.Role.Where(f => f.UniqueID.Equals("2ee2fd0c-100d-b934-d0c2-f24ff16039e9")).ToList();
                    context.UserExtention.AddOrUpdate(new UserExtention
                    {
                        UniqueID = "cae0fcf9-ea87-4989-a76b-efd95aec2516",
                        DefaultRoleID = "2ee2fd0c-100d-b934-d0c2-f24ff16039e9",
                        Roles = roles
                    }, new UserExtention
                    {
                        UniqueID = "ea436d1d-d494-44f2-9c9c-0c1d5542ede8",
                        DefaultRoleID = "2ee2fd0c-100d-b934-d0c2-f24ff16039e9",
                        Roles = roles
                    });

                    #endregion

                    #region NotificationConfig

                    context.NotificationConfig.AddOrUpdate(new NotificationConfig
                    {
                        UniqueID = "401122f3-05c9-49bb-b747-192bbd9ea80a",
                        Template = "{会诊管理员所在医院}结束了病人{病人姓名}的会诊申请,请及时查看!",
                        SiteID = "",
                        Event = 4,
                        Variables = "会诊管理员所在医院,病人姓名",
                        IsEnable = true,
                        IsDefault = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new NotificationConfig
                    {
                        UniqueID = "59734a0e-81e7-4901-8235-4f6c3924e3bc",
                        Template = "{会诊管理员所在医院}受理了病人{病人姓名}的会诊申请,会诊日期为{会诊日期},请及时查看!",
                        SiteID = "",
                        Event = 2,
                        Variables = "会诊管理员所在医院,会诊日期,病人姓名",
                        IsEnable = true,
                        IsDefault = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new NotificationConfig
                    {
                        UniqueID = "609f1ce9-ba9d-4905-a806-6b4fda00b18c",
                        Template = "{会诊管理员所在医院}拒绝了病人{病人姓名}的会诊申请,请及时查看!",
                        SiteID = "",
                        Event = 3,
                        Variables = "会诊管理员所在医院,病人姓名",
                        IsEnable = true,
                        IsDefault = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new NotificationConfig
                    {
                        UniqueID = "89aec546-a41d-4c91-a131-818331502c2f",
                        Template = "{会诊管理员所在医院}更新了病人{病人姓名}的会诊建议,请检查会诊结果!",
                        SiteID = "",
                        Event = 6,
                        Variables = "会诊管理员所在医院,病人姓名",
                        IsEnable = true,
                        IsDefault = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new NotificationConfig
                    {
                        UniqueID = "ba2d3a6a-2733-4684-821f-56748c28bd5d",
                        Template = "{会诊管理员姓名}邀请您参加来自{医生所在医院}的会诊，会诊类型为{会诊类型}，会诊日期为{会诊日期},请及时查看!",
                        SiteID = "",
                        Event = 1,
                        Variables = "会诊管理员姓名,医生所在医院,会诊类型,会诊日期",
                        IsEnable = true,
                        IsDefault = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new NotificationConfig
                    {
                        UniqueID = "baa8cc6b-d17a-4819-a965-bc4ed1d5bb4b",
                        Template = "来自{医生所在医院}的{医生姓名}取消了病人{病人姓名}的会诊申请,请及时查看!",
                        SiteID = "",
                        Event = 5,
                        Variables = "医生所在医院,医生姓名,病人姓名",
                        IsEnable = true,
                        IsDefault = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new NotificationConfig
                    {
                        UniqueID = "d2585189-11c6-4a44-a431-2a214902c796",
                        Template = "来自{医生所在医院}的{医生姓名}向您发送了一份新的申请，会诊类型为{会诊类型}，期望会诊日期为{会诊日期},请及时查看!",
                        SiteID = "",
                        Event = 0,
                        Variables = "医生所在医院,医生姓名,会诊类型,会诊日期",
                        IsEnable = true,
                        IsDefault = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    #region ConsultationPatientNo

                    context.ConsultationPatientNo.AddOrUpdate(new ConsultationPatientNo
                    {
                        UniqueID = "1",
                        Prefix = "P",
                        MaxLength = 8,
                        CurrentValue = 0,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    #region system config

                    context.SysConfig.AddOrUpdate(new SysConfig
                    {
                        UniqueID = "1",
                        Module = 1,
                        GroupName = "Meeting",
                        ConfigKey = "IPAddress",
                        ConfigValue = "10.112.20.100",
                        ConfigDescription = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new SysConfig
                    {
                        UniqueID = "2",
                        Module = 1,
                        GroupName = "Meeting",
                        ConfigKey = "User",
                        ConfigValue = "admin",
                        ConfigDescription = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new SysConfig
                    {
                        UniqueID = "3",
                        Module = 1,
                        GroupName = "Meeting",
                        ConfigKey = "Password",
                        ConfigValue = "admin",
                        ConfigDescription = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new SysConfig
                    {
                        UniqueID = "4",
                        Module = 1,
                        GroupName = "Meeting",
                        ConfigKey = "Version",
                        ConfigValue = "50",
                        ConfigDescription = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new SysConfig
                    {
                        UniqueID = "5",
                        Module = 1,
                        GroupName = "Meeting",
                        ConfigKey = "MeetingPassword",
                        ConfigValue = "1",
                        ConfigDescription = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new SysConfig
                    {
                        UniqueID = "6",
                        Module = 1,
                        GroupName = "Meeting",
                        ConfigKey = "Site",
                        ConfigValue = "box",
                        ConfigDescription = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    }, new SysConfig
                    {
                        UniqueID = "7",
                        Module = 2,
                        GroupName = "ShareDesktop",
                        ConfigKey = "Url",
                        ConfigValue = "Http://10.112.20.140:5800",
                        ConfigDescription = "",
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });

                    #endregion

                    context.InitialDataHistory.AddOrUpdate(new InitialDataHistory
                    {
                        UniqueID = Version.V1001.VersionId,
                        Version = Version.V1001.VersionDecsription,
                        IsUpdated = true,
                        LastEditUser = SupperAdminUserId,
                        LastEditTime = dateTime
                    });
                }
            }
            else
            {
                context.InitialDataHistory.AddOrUpdate(new InitialDataHistory
                {
                    UniqueID = Version.V1001.VersionId,
                    Version = Version.V1001.VersionDecsription,
                    IsUpdated = true,
                    LastEditUser = SupperAdminUserId,
                    LastEditTime = dateTime
                });
            }

            context.SaveChanges();
        }

        private static void Seed_1_0_1_0(ConsultationContext context)
        {
            var version = context.InitialDataHistory.FirstOrDefault(i => i.UniqueID.Equals(Version.V1010.VersionId));

            if (version == null || !version.IsUpdated)
            {
                var dateTime = new DateTime(2015, 9, 30);

                context.ConsultationDictionary.AddOrUpdate(new ConsultationDictionary
                {
                    DictionaryID = "2DF82B4A-6B18-4D41-A8AF-DFA2B3A0D9A9",
                    Type = DictionaryType.ApplyCancelReason,
                    Name = "病人要求",
                    Value = "1",
                    Description = "申请取消理由",
                    LastEditUser = SupperAdminUserId,
                    LastEditTime = dateTime
                }, new ConsultationDictionary
                {
                    DictionaryID = "15A1E6B8-E1ED-4F5B-9741-4ED772687BDD",
                    Type = DictionaryType.ApplyCancelReason,
                    Name = "通过其他途径完成诊断",
                    Value = "2",
                    Description = "申请取消理由",
                    LastEditUser = SupperAdminUserId,
                    LastEditTime = dateTime
                }, new ConsultationDictionary
                {
                    DictionaryID = "1AEEEC19-B602-4CDD-87B8-3CC5F4B3CB15",
                    Type = DictionaryType.ApplyCancelReason,
                    Name = "其他原因",
                    Value = "3",
                    Description = "申请取消理由",
                    LastEditUser = SupperAdminUserId,
                    LastEditTime = dateTime
                }, new ConsultationDictionary
                {
                    DictionaryID = "240986EF-4B13-4040-9C4F-2F65338AF2F3",
                    Type = DictionaryType.TimeRange,
                    Name = "晚上 17:00-23:00",
                    Value = "3",
                    Description = "17:00|24:00",
                    LastEditUser = SupperAdminUserId,
                    LastEditTime = dateTime
                }, new ConsultationDictionary
                {
                    DictionaryID = "717CF241-C15B-48CE-A3C3-E53144F4D1D9",
                    Type = DictionaryType.TimeRange,
                    Name = "上午 9:00-12:00",
                    Value = "1",
                    Description = "9:00|12:00",
                    LastEditUser = SupperAdminUserId,
                    LastEditTime = new DateTime(2015, 9, 18)
                }, new ConsultationDictionary
                {
                    DictionaryID = "C3D4BE07-C4E4-4C6B-BD08-8285F1C211E1",
                    Type = DictionaryType.TimeRange,
                    Name = "下午 13:00-17:00",
                    Value = "2",
                    Description = "13:00|17:00",
                    LastEditUser = SupperAdminUserId,
                    LastEditTime = dateTime
                });

                context.InitialDataHistory.AddOrUpdate(new InitialDataHistory
                {
                    UniqueID = Version.V1010.VersionId,
                    Version = Version.V1010.VersionDecsription,
                    IsUpdated = true,
                    LastEditUser = SupperAdminUserId,
                    LastEditTime = dateTime
                });

                context.SaveChanges();
            }
        }
    }

    public class Version
    {
        public static readonly Version V1001 = new Version("E8BD70BA-BD74-47CB-A3DA-5B87178BF1EC", "1.0.0.1");
        public static readonly Version V1010 = new Version("BBF4C7B0-92AB-4665-80A3-B79B71E1E088", "1.0.1.0");

        public string VersionDecsription { get; set; }
        public string VersionId { get; set; }

        private Version(string vid, string vDecsription)
        {
            VersionDecsription = vDecsription;
            VersionId = vid;
        }
    }
}
