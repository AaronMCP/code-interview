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
using System.Data;
using System.Collections.Generic;
using System.Text;
using Server.Business.Oam;
using Server.DAO.Oam;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class ScheduleServiceImpl : IScheduleService
    {
        private IScheduleDAO scheduleDAO = DataBasePool.Instance.GetDBProvider();
        public DataSet QueryWorkTimeList()
        {
            try
            {
                return scheduleDAO.QueryWorkTimeList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int GetStepLength()
        {
            try
            {
                return scheduleDAO.GetStepLength();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QueryScheduledModalityList(DateTime beginTime, DateTime endTime)
        {
            try
            {
                return scheduleDAO.QueryScheduledModalityList(beginTime, endTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int AddWorkTime(WorkTimeModel model)
        {
            try
            {
                return scheduleDAO.AddWorkTime(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int ModifyWorkTime(WorkTimeModel model)
        {
            try
            {
                return scheduleDAO.ModifyWorkTime(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteWorkTime(WorkTimeModel model)
        {
            try
            {
                return scheduleDAO.DeleteWorkTime(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QueryEmployeeList(string type)
        {
            try
            {
                return scheduleDAO.QueryEmployeeList(type);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QueryScheduledEmployeeList(DateTime beginTime, DateTime endTime)
        {
            try
            {
                return scheduleDAO.QueryScheduledEmployeeList(beginTime, endTime);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddEmployeeSchedule(EmployeeScheduleModel model)
        {
            try
            {
                return scheduleDAO.AddEmployeeSchedule(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool CopyEmployeeSchedule(CopyScheduleModel model)
        {
            try
            {
                return scheduleDAO.CopyEmployeeSchedule(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ModifySchedule(CopyScheduleModel model)
        {
            try
            {
                return scheduleDAO.ModifySchedule(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QuerySchedule(DateTime beginTime, DateTime endTime, string employee, bool isTemplate)
        {
            try
            {
                return scheduleDAO.QuerySchedule(beginTime, endTime, employee, isTemplate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QueryModalitySchedule(DateTime beginTime, DateTime endTime, string modality)
        {
            try
            {
                return scheduleDAO.QueryModalitySchedule(beginTime, endTime, modality);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteSchedule(DateTime beginTime, DateTime endTime, string name, bool isTemplate, string type)
        {
            try
            {
                return scheduleDAO.DeleteSchedule(beginTime, endTime, name, isTemplate, type);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QueryAvailableEmployeeList(ModalityScheduleModel model)
        {
            try
            {
                return scheduleDAO.QueryAvailableEmployeeList(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddModalitySchedule(ModalityScheduleModel model)
        {
            try
            {
                return scheduleDAO.AddModalitySchedule(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetWrokingCalendarSpecialDays(string modality, string site)
        {
            return scheduleDAO.GetWrokingCalendarSpecialDays(modality, site);
        }

        public DataSet GetWrokingCalendarAllSpecialDays()
        {
            return scheduleDAO.GetWrokingCalendarAllSpecialDays();
        }        

        public void SaveCalendarSpeicalDays(SpecialDayCollection sDayCol)
        {
            scheduleDAO.SaveCalendarSpeicalDays(sDayCol);
        }

        public void DeleteCalendarSpecialDays(SpecialDayCollection sDayCol)
        {
            scheduleDAO.DeleteCalendarSpecialDays(sDayCol);
        }

        public DataTable GetPeopleScheduleTemplate(string site,string templateType, string beginTime)
        {
            return scheduleDAO.GetPeopleScheduleTemplate(site, templateType, beginTime);
        }

        public bool SavePeopleScheduleTemplate(DataTable dt)
        {
            return scheduleDAO.SavePeopleScheduleTemplate(dt);
        }

        public bool SavePeopleSchedule(DataTable dt)
        {
            return scheduleDAO.SavePeopleSchedule(dt);
        }

        public DataTable GetPeoplesWeekSchedule(DataTable dtCondition)
        {
            return scheduleDAO.GetPeoplesWeekSchedule(dtCondition);
        }
    }
}
