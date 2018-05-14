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

namespace Server.Utilities.Oam
{
    public class MessageName
    {
        //DictionaryManager
        public const string DictionaryManager = "Oam.DictionaryManager";

        //ResourceManager
        public const string ResourceManager = "Oam.ResourceManager";

        //ProcedureCodeManager
        public const string ProcedureCodeManager = "Oam.ProcedureCodeManager";

        //ScheduleManager
        public const string ScheduleManager = "Oam.ScheduleManager";

        //RoleManager
        public const string RoleManager = "Oam.RoleManager";

        //User Manager
        public const string UserManager = "Oam.UserManager";

        //System Profile  Manager
        public const string SystemProfileManager = "Oam.SystemProfileManager";

        //Client Config Manager
        public const string ClientConfigManager = "Oam.ClientConfigManager";

        //Bulletin Manager
        public const string BulletinManager = "OAM.BulletinPublish";

        //ConditionCol Manager
        public const string ConiditioncolManager = "OAM.ConditionColManager";

        //Assign Report Manager
        public const string AssignReportManager = "Assignment";
    }

    //public enum MessageName
    //{
    //    //RoleManager
    //    AddRole,
    //    EditRole,
    //    DeleteRole,
    //    CopyRole,

    //    //UserManager
    //    AddUser,
    //    EditUser,
    //    DeleteUser,
    //    ShowUserOnlineStatus,
    //    SetUserOffline,

    //    //RIS Config
    //    GetRISConfig,
    //    SetRISConfig,

    //    //ResourceManager
    //    LocationManager,
    //    InstrumentManager,

    //    //SystemSettings
    //    ExportSystemSettings,
    //    ImportSystemSettings,

    //    //LogFileManager
    //    DeleteLogFile,
    //    QueryLogFile,
    //    SetLogParameters,

    //    //ScheduleManager
    //    AddEmployeeSchedule,
    //    EditEmployeeSchedule,
    //    DeleteEmoloyeeSchedule,
    //    CopyEmployeeSchedule,
    //    AddDeviceSchedule,
    //    EditDeviceSchedule,
    //    DeleteDeviceSchedule,

    //    //ACRCodeManager
    //    AddACRCode,
    //    EditACRCode,
    //    DeleteACRCode,
    //    QueryACRCode,

    //    //DictionaryManager
    //    DictionaryManager,

    //    //ProcedureCodeManager
    //    ProcedureCodeManager,
    //}
}
