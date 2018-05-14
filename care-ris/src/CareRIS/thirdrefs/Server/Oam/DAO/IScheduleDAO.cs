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

namespace Server.DAO.Oam
{
    public interface IScheduleDAO
    {
        DataSet QueryWorkTimeList();
        int GetStepLength();
        DataSet QueryScheduledModalityList(DateTime beginTime, DateTime endTime);
        /// <summary>
        /// Add a work time item.
        /// </summary>
        /// <param name="model">The model contains the work time.</param>
        /// <returns>
        /// 0: success.
        /// 1: There is overlapped time. 
        /// 2: Other errors.
        /// 3: The Work Time has been exist.
        /// </returns>
        int AddWorkTime(WorkTimeModel model);
        /// <summary>
        /// Modify an existed work time item.
        /// </summary>
        /// <param name="model">The model contains the work time.</param>
        /// <returns>
        /// 0: success
        /// 1: There is overlapped time.
        /// 2: Other errors.
        /// </returns>
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

        #region WorkingCalendar
        DataSet GetWrokingCalendarSpecialDays(string modality, string site);
        DataSet GetWrokingCalendarAllSpecialDays();
        void SaveCalendarSpeicalDays(SpecialDayCollection sDayCol);
        void DeleteCalendarSpecialDays(SpecialDayCollection sDayCol);
        #endregion

        #region PeopleScheduler
        DataTable GetPeopleScheduleTemplate(string site,string template, string beginTime);
        bool SavePeopleScheduleTemplate(DataTable dt);
        bool SavePeopleSchedule(DataTable dt);
        DataTable GetPeoplesWeekSchedule(DataTable   dtCondition);
        #endregion
    }
}
