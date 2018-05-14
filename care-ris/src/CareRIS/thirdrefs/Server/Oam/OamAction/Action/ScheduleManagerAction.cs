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
using Common.Action;
using Common.ActionResult;
using Server.Business.Oam;
using CommonGlobalSettings;
using CommonGlobalSettings;
using CommonGlobalSettings;

namespace Server.OamAction.Action
{
    public class ScheduleManagerAction : BaseAction
    {
        private IScheduleService scheduleServcie = BusinessFactory.Instance.GetScheduleService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);

            switch (actionName)
            {
                case "queryWorkTimeList":
                    return QueryWorkTimeList();
                case "queryScheduledModalityList":
                    return QueryScheduledModalityList(context.Parameters);
                case "addWorkTime":
                    return AddWorkTime(context.Model as WorkTimeModel);
                case "modifyWorkTime":
                    return ModifyWorkTime(context.Model as WorkTimeModel);
                case "deleteWorkTime":
                    return DeleteWorkTime(context.Model as WorkTimeModel);
                case "queryEmployeeList":
                    return QueryEmployeeList(context.Parameters);
                case "queryScheduledEmployeeList":
                    return QueryScheduledEmployeeList(context.Parameters);
                case "addEmployeeSchedule":
                    return AddEmployeeSchedule(context.Model as EmployeeScheduleModel);
                case "copyEmployeeSchedule":
                    return CopyEmployeeSchedule(context.Model as CopyScheduleModel);
                case "querySchedule":
                    return QuerySchedule(context.Parameters);
                case "queryModalitySchedule":
                    return QueryModalitySchedule(context.Parameters);
                case "deleteSchedule":
                    return DeleteSchedule(context.Parameters);
                case "modifySchedule":
                    return ModifySchedule(context.Model as CopyScheduleModel);
                case "queryAvailableEmployeeList":
                    return QueryAvailableEmployeeList(context.Model as ModalityScheduleModel);
                case "addModalitySchedule":
                    return AddModalitySchedule(context.Model as ModalityScheduleModel);
                case "GetCalendarSpecialDays":
                    return GetWorkingCalendarSpecialDays(context.Parameters);
                case "SaveCalendarSpeicalDays":
                    return SaveCalendarSpeicalDays(context.Model as SpecialDayCollection);
                case "DeleteCalendarSpecialDays":
                    return DeleteCalendarSpecialDays(context.Model as SpecialDayCollection);
                case "GetPeopleScheduleTemplate":
                    return GetPeopleScheduleTemplate(context);
                case "SavePeopleScheduleTemplate":
                    return SavePeopleScheduleTemplate(context);
                case "SavePeopleSchedule":
                    return SavePeopleSchedule(context);
                case "GetPeoplesWeekSchedule":
                    return GetPeoplesWeekSchedule(context);
                case "GetAllCalendarSpecialDays":
                    return GetWrokingCalendarAllSpecialDays(context.Parameters);                
            }

            //default
            return null;
        }

        #region WorkTime Management Section
        private DataSetActionResult QueryWorkTimeList()
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = scheduleServcie.QueryWorkTimeList();
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                    result.ReturnString = scheduleServcie.GetStepLength().ToString();
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddWorkTime(WorkTimeModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                int returnCode = scheduleServcie.AddWorkTime(model);
                if (returnCode == 0)
                {
                    result.Result = true;
                }
                else if (returnCode == 1)
                {
                    result.Result = false;
                    result.ReturnMessage = "There is time overlap,Please input other time range.";
                }
                else if (returnCode == 3)
                {
                    result.Result = false;
                    result.ReturnMessage = "The work time name has been exist!";
                }
                else
                {
                    result.Result = false;
                    result.ReturnMessage = "Unknown Errors.";
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult ModifyWorkTime(WorkTimeModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                int returnCode = scheduleServcie.ModifyWorkTime(model);
                if (returnCode == 0)
                {
                    result.Result = true;
                }
                else if (returnCode == 1)
                {
                    result.Result = false;
                    result.ReturnMessage = "There is time overlap,Please input other time range.";
                }
                else if (returnCode == 3)
                {
                    result.Result = false;
                    result.ReturnMessage = "This item has been deleted in other client.";
                }
                else
                {
                    result.Result = false;
                    result.ReturnMessage = "Unknown Errors.";
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteWorkTime(WorkTimeModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (scheduleServcie.DeleteWorkTime(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }
        #endregion

        private DataSetActionResult QueryScheduledModalityList(string parameters)
        {
            DateTime beginTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("beginTime", parameters));
            DateTime endTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("endTime", parameters));
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = scheduleServcie.QueryScheduledModalityList(beginTime, endTime);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult QueryEmployeeList(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string type = CommonGlobalSettings.Utilities.GetParameter("type", parameters);
            try
            {
                DataSet dataSet = scheduleServcie.QueryEmployeeList(type);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult QueryScheduledEmployeeList(string parameters)
        {
            DateTime beginTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("beginTime", parameters));
            DateTime endTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("endTime", parameters));
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = scheduleServcie.QueryScheduledEmployeeList(beginTime, endTime);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddEmployeeSchedule(EmployeeScheduleModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (scheduleServcie.AddEmployeeSchedule(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult CopyEmployeeSchedule(CopyScheduleModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (scheduleServcie.CopyEmployeeSchedule(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult ModifySchedule(CopyScheduleModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (scheduleServcie.ModifySchedule(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult QuerySchedule(string parameters)
        {
            DateTime beginTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("beginTime", parameters));
            DateTime endTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("endTime", parameters));
            string name = CommonGlobalSettings.Utilities.GetParameter("name", parameters);
            bool isTemplate = Convert.ToBoolean(CommonGlobalSettings.Utilities.GetParameter("isTemplate", parameters));
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = scheduleServcie.QuerySchedule(beginTime, endTime, name, isTemplate);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult QueryModalitySchedule(string parameters)
        {
            DateTime beginTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("beginTime", parameters));
            DateTime endTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("endTime", parameters));
            string modality = CommonGlobalSettings.Utilities.GetParameter("modality", parameters);
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = scheduleServcie.QueryModalitySchedule(beginTime, endTime, modality);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult DeleteSchedule(string parameters)
        {
            DateTime beginTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("beginTime", parameters));
            DateTime endTime = Convert.ToDateTime(CommonGlobalSettings.Utilities.GetParameter("endTime", parameters));
            string name = CommonGlobalSettings.Utilities.GetParameter("name", parameters);
            bool isTemplate = Convert.ToBoolean(CommonGlobalSettings.Utilities.GetParameter("isTemplate", parameters));
            string type = CommonGlobalSettings.Utilities.GetParameter("type", parameters);
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (scheduleServcie.DeleteSchedule(beginTime, endTime, name, isTemplate, type))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult QueryAvailableEmployeeList(ModalityScheduleModel model)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                DataSet dataSet = scheduleServcie.QueryAvailableEmployeeList(model);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult AddModalitySchedule(ModalityScheduleModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (scheduleServcie.AddModalitySchedule(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private DataSetActionResult GetWorkingCalendarSpecialDays(string parameters)
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                string modality = CommonGlobalSettings.Utilities.GetParameter("Modality", parameters);
                string site = CommonGlobalSettings.Utilities.GetParameter("Site", parameters);
                DataSet ds = scheduleServcie.GetWrokingCalendarSpecialDays(modality, site);
                dsar.DataSetData = ds;
                dsar.Result = true;
                return dsar;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }

            return dsar;
        }

        private DataSetActionResult GetWrokingCalendarAllSpecialDays(string parameters)
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = scheduleServcie.GetWrokingCalendarAllSpecialDays();
                dsar.DataSetData = ds;
                dsar.Result = true;
                return dsar;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }

            return dsar;
        }        

        private BaseActionResult SaveCalendarSpeicalDays(SpecialDayCollection sDayCol)
        {
            BaseActionResult bsar = new BaseActionResult();
            try
            {
                scheduleServcie.SaveCalendarSpeicalDays(sDayCol);
                bsar.Result = true;
                return bsar;
            }
            catch (Exception ex)
            {
                bsar.Result = false;
                bsar.ReturnMessage = ex.Message;
            }
            return bsar;
        }

        private BaseActionResult DeleteCalendarSpecialDays(SpecialDayCollection sDayCol)
        {
            BaseActionResult bsar = new BaseActionResult();
            try
            {
                scheduleServcie.DeleteCalendarSpecialDays(sDayCol);
                bsar.Result = true;
                return bsar;
            }
            catch (Exception ex)
            {
                bsar.Result = false;
                bsar.ReturnMessage = ex.Message;
            }
            return bsar;
        }

        private DataSetActionResult GetPeopleScheduleTemplate(Context ctx)
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = new DataSet();
                string site = CommonGlobalSettings.Utilities.GetParameter("Site", ctx.Parameters);
                string templateType = CommonGlobalSettings.Utilities.GetParameter("TemplateType", ctx.Parameters);
                string beginTime = CommonGlobalSettings.Utilities.GetParameter("BeginTime", ctx.Parameters);
                DataTable dt = scheduleServcie.GetPeopleScheduleTemplate(site, templateType, beginTime);
                if (dt != null)
                {
                    ds.Tables.Add(dt);
                }
                dsar.DataSetData = ds;
                dsar.Result = true;
                return dsar;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }

        private BaseActionResult SavePeopleScheduleTemplate(Context ctx)
        {
            BaseActionResult bar = new BaseActionResult();
            try
            {
                BaseDataSetModel bdsm = (ctx.Model as BaseDataSetModel);
                scheduleServcie.SavePeopleScheduleTemplate(bdsm.DataSetParameter.Tables[0]);
                bar.Result = true;
            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }

        private BaseActionResult SavePeopleSchedule(Context ctx)
        {
            BaseActionResult bar = new BaseActionResult();
            try
            {
                BaseDataSetModel bdsm = (ctx.Model as BaseDataSetModel);
                scheduleServcie.SavePeopleSchedule(bdsm.DataSetParameter.Tables[0]);
                bar.Result = true;
            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }

        private DataSetActionResult GetPeoplesWeekSchedule(Context ctx)
        {
            DataSetActionResult dsar = new DataSetActionResult();
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = scheduleServcie.GetPeoplesWeekSchedule((ctx.Model as BaseDataSetModel).DataSetParameter.Tables[0]);
                if (dt != null)
                {
                    ds.Tables.Add(dt);
                }
                dsar.DataSetData = ds;
                dsar.Result = true;
                return dsar;
            }
            catch (Exception ex)
            {
                dsar.Result = false;
                dsar.ReturnMessage = ex.Message;
            }
            return dsar;
        }
    }
}
