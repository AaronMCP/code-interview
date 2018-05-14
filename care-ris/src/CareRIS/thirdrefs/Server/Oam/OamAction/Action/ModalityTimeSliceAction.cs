using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using CommonGlobalSettings;
using Server.Business.Oam;
using CommonGlobalSettings;
using Server.Utilities.Oam;

namespace Server.OamAction.Action
{
    public class ModalityTimeSliceAction : BaseAction
    {
        IModalityTimeSliceService modalityTimeSliceService = BusinessFactory.Instance.GetModalityTimeSliceService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);
            string strGuid = CommonGlobalSettings.Utilities.GetParameter("Guid", context.Parameters);
            string strModalityType = CommonGlobalSettings.Utilities.GetParameter("ModalityType", context.Parameters);
            string strModality = CommonGlobalSettings.Utilities.GetParameter("Modality", context.Parameters);
            string strStartTime = CommonGlobalSettings.Utilities.GetParameter("StartTime", context.Parameters);
            string strEndTime = CommonGlobalSettings.Utilities.GetParameter("EndTime", context.Parameters);
            string strIsModify = CommonGlobalSettings.Utilities.GetParameter("IsModify", context.Parameters);
            string strDescription = CommonGlobalSettings.Utilities.GetParameter("Description", context.Parameters);
            string strMaxMan = CommonGlobalSettings.Utilities.GetParameter("MaxMan", context.Parameters);
            //string strMaxWeekendMan = CommonGlobalSettings.Utilities.GetParameter("MaxWeekendNumber", context.Parameters);
            string strDomain = CommonGlobalSettings.Utilities.GetParameter("Domain", context.Parameters);
            //string strMaxHolidayNum = CommonGlobalSettings.Utilities.GetParameter("MaxHolidayNumber", context.Parameters);
            string strDateType = CommonGlobalSettings.Utilities.GetParameter("DateType", context.Parameters);
            string strInterval = CommonGlobalSettings.Utilities.GetParameter("Interval", context.Parameters);
            BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
            if (actionName == null || actionName.Equals(""))
            {
                actionName = "GetModalityTimeSlice";
            }
            switch (actionName)
            {
                case "AddModalityTimeSlice":
                    {
                        string strAvailableDate = CommonGlobalSettings.Utilities.GetParameter("AvailableDate", context.Parameters);
                        return AddModalityTimeSlice(strGuid, strModalityType, strModality, strStartTime,
                            strEndTime, strDescription, strMaxMan, strDomain, strDateType, strAvailableDate);
                    }
                case "ModifyModalityTimeSlice":
                    {
                        return ModifyModalityTimeSlice(strGuid, strStartTime, strEndTime, strDescription, strMaxMan, strDateType);
                    }
                case "DeleteModalityTimeSlice":
                    {
                        return DeleteModalityTimeSlice(strGuid);
                    }
                case "IsModalityTimeSliceOverLap":
                    {
                        string strAvailableDate = CommonGlobalSettings.Utilities.GetParameter("AvailableDate", context.Parameters);
                        return IsModalityTimeSliceOverLap(strGuid, strModalityType, strModality, strStartTime, strEndTime, strIsModify, strDateType, strAvailableDate);
                    }
                case "GetModalityTimeSlice":
                    {
                        return GetModalityTimeSlice(strModality);
                    }
                case "GetModalityTimeSliceOverLapGuids":
                    {
                        string strAvailableDate = CommonGlobalSettings.Utilities.GetParameter("AvailableDate", context.Parameters);
                        return GetModalityTimeSliceOverLapGuids(strGuid, strModalityType, strModality, strStartTime, strEndTime, strIsModify, strDateType, strAvailableDate);
                    }
                case "BulkAddModalityTimeSlice":
                    {
                        string strAvailableDate = CommonGlobalSettings.Utilities.GetParameter("AvailableDate", context.Parameters);
                        return BulkAddModalityTimeSlice(strModalityType, strModality, strStartTime, strEndTime,
                            strMaxMan, strDomain, strInterval, strDateType, strAvailableDate);
                    }
                case "UpdateAvailableDate":
                    {
                        string strAvailableDate = CommonGlobalSettings.Utilities.GetParameter("AvailableDate", context.Parameters);
                        string strExAvailableDate = CommonGlobalSettings.Utilities.GetParameter("ExAvailableDate", context.Parameters);

                        return UpdateAvailableTime(strModalityType, strModality, strAvailableDate, strExAvailableDate, strDateType);
                    }
                case "GetShareSettings":
                    {
                        string timeSliceGuid = CommonGlobalSettings.Utilities.GetParameter("Guid", context.Parameters);
                        return GetShareSettings(timeSliceGuid);
                    }
                case "SaveModalityShare":
                    {
                        string timeSliceGuids = CommonGlobalSettings.Utilities.GetParameter("Guids", context.Parameters);
                        return SaveModalityShare(timeSliceGuids, (context.Model as BaseDataSetModel).DataSetParameter);
                    }
                case "LockModalityQuota":
                    {
                        return LockModalityQuota(context.Parameters);
                    }
                default:
                    return null;
            }
        }

        private BaseActionResult AddModalityTimeSlice(string strGuid, string strModalityType, string strModality,
                            string strStartTime, string strEndTime, string strDescription, string strMaxMan,
                            string strDomain, string strDateType, string strAvailableDt)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.ReturnString = modalityTimeSliceService.AddModalityTimeSliceEx(strModalityType, strModality,
                    strStartTime, strEndTime, strDescription, strMaxMan, strDomain, strDateType, strAvailableDt);
                result.Result = true;
            }
            catch (Exception ex)
            {
                if (ex is DuplicateDescrpException)
                {
                    result.recode = 1;//Duplicate Exception; Error code
                }
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult ModifyModalityTimeSlice(string strGuid, string strStartTime, string strEndTime, string strDescription, string strMaxMan, string strDateType)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.Result = modalityTimeSliceService.ModifyModalityTimeSlice(strGuid, strStartTime, strEndTime, strDescription, strMaxMan, strDateType);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult DeleteModalityTimeSlice(string strGuid)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.Result = modalityTimeSliceService.DeleteModalityTimeSlice(strGuid);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult IsModalityTimeSliceOverLap(string strGuid, string strModalityType, string strModality,
            string strStartTime, string strEndTime, string IsModify, string strDateType, string strAvailableDate)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.Result = modalityTimeSliceService.IsModalityTimeSliceOverLap(strGuid, strModalityType,
                    strModality, strStartTime, strEndTime, IsModify, strDateType, strAvailableDate);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult GetModalityTimeSlice(string strModality)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.DataSetData = modalityTimeSliceService.GetModalityTimeSlice(strModality);
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }
        
        private BaseActionResult GetModalityTimeSliceOverLapGuids(string strGuid, string strModalityType, string strModality,
            string strStartTime, string strEndTime, string IsModify, string strDateType, string strAvailableDt)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.DataSetData = modalityTimeSliceService.GetModalityTimeSliceOverLapGuids(strGuid, strModalityType, strModality, strStartTime, strEndTime, IsModify, strDateType, strAvailableDt);
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }


        private BaseActionResult BulkAddModalityTimeSlice(string strModalityType, string strModality, string startTime, string endTime,
            string strMaxNumber, string strDomain, string interval, string strDateTypes, string strAvailableDt)
        {
            BaseActionResult bar = new BaseActionResult();
            bar.Result = true;
            try
            {
                modalityTimeSliceService.BulkAddModalityTimeSlice(strModalityType, strModality, startTime, endTime,
                    strMaxNumber, strDomain, interval, strDateTypes, strAvailableDt);
            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }

        private BaseActionResult UpdateAvailableTime(string strModalityType, string strModality, string availabeDate, string exAvailableDate, string datetype)
        {
            BaseActionResult bar = new BaseActionResult();
            bar.Result = true;
            try
            {
                modalityTimeSliceService.UpdateAvailableTime(strModalityType, strModality, availabeDate, exAvailableDate, datetype);
            }
            catch (Exception ex)
            {
                bar.Result = false;
                bar.ReturnMessage = ex.Message;
            }
            return bar;
        }

        private BaseActionResult GetShareSettings(string timeSliceGuid)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.DataSetData = modalityTimeSliceService.GetShareSettings(timeSliceGuid);
                result.Result = true;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private BaseActionResult SaveModalityShare(string timeSliceGuids, DataSet model)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                result.Result = modalityTimeSliceService.SaveModalityShare(timeSliceGuids, model);
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }

        private DataSetActionResult LockModalityQuota(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet ds = new DataSet();
            result.DataSetData = ds;
            try
            {
                string lockGuid = "";
                result.Result = modalityTimeSliceService.LockModalityQuota(parameters, out lockGuid);
                result.ReturnString = lockGuid;
            }
            catch (Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }
            return result;
        }
    }
}
