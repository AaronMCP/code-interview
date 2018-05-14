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
using System.Data;
using CommonGlobalSettings;

namespace Server.Business.Oam
{
    public interface IScheduleService
    {
        DataSet QueryWorkTimeList();
        int GetStepLength();
        DataSet QueryScheduledModalityList(DateTime beginTime, DateTime endTime);
        int AddWorkTime(WorkTimeModel model);
        int ModifyWorkTime(WorkTimeModel model);
        bool DeleteWorkTime(WorkTimeModel model);
        DataSet QueryEmployeeList(string type);
        DataSet QueryScheduledEmployeeList(DateTime beginTime, DateTime endTime);
        bool AddEmployeeSchedule(EmployeeScheduleModel model);
        bool CopyEmployeeSchedule(CopyScheduleModel model);
        bool ModifySchedule(CopyScheduleModel model);
        DataSet QuerySchedule(DateTime beginTime, DateTime endTime, string name, bool isTemplate);
        DataSet QueryModalitySchedule(DateTime beginTime, DateTime endTime, string modality);
        bool DeleteSchedule(DateTime beginTime, DateTime endTime, string name, bool isTemplate, string type);
        DataSet QueryAvailableEmployeeList(ModalityScheduleModel model);
        bool AddModalitySchedule(ModalityScheduleModel model);
        DataSet GetWrokingCalendarSpecialDays(string modality, string site);
        DataSet GetWrokingCalendarAllSpecialDays();
        void SaveCalendarSpeicalDays(SpecialDayCollection sDayCol);
        void DeleteCalendarSpecialDays(SpecialDayCollection sDayCol);
        DataTable GetPeopleScheduleTemplate(string site,string templateType,string beginTime);
        bool SavePeopleScheduleTemplate(DataTable dt);
        bool SavePeopleSchedule(DataTable dt);
        DataTable GetPeoplesWeekSchedule(DataTable dtCondition);
    }
}
