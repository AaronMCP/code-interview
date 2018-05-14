using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.Business.Oam
{
    public interface IModalityTimeSliceService
    {
        /// <summary>
        /// AddModalityTimeSlice
        /// </summary>
        /// <param name="strModalityType"></param>
        /// <param name="strModality"></param>
        /// <param name="strStartTime"></param>
        /// <param name="strEndTime"></param>
        /// <param name="strDescription"></param>
        /// <param name="strMaxMan"></param>
        /// <param name="strDomain"></param>
        /// <param name="strDateTypes"></param>
        /// <returns>return one newly added TimeSlice Guid</returns>
        string AddModalityTimeSliceEx(string strModalityType, string strModality, string strStartTime, string strEndTime, string strDescription, string strMaxMan, string strDomain, string strDateTypes, string strAvailableDt);
        /// <summary>
        /// Name:ModifyModalityTimeSlice
        /// Function:bool ModifyModalityTimeSlice(string strGuid, string strStartTime, string strEndTime, string strDescription, string strMaxMan)
        /// </summary>
        /// <returns>return true if modify successfully, else false</returns>
        bool ModifyModalityTimeSlice(string strGuid, string strStartTime, string strEndTime, string strDescription, string strMaxMan, string strDateType);
        /// <summary>
        /// Name:DeleteModalityTimeSlice
        /// Function:bool DeleteModalityTimeSlice(string strGuid)
        /// </summary>
        /// <returns>return true if delete successfully, else false</returns>
        bool DeleteModalityTimeSlice(string strGuid);
        /// <summary>
        /// Name:IsModalityTimeSliceOverLap
        /// Function:bool IsModalityTimeSliceOverLap(string strGuid,string strModalityType, string strModality, string strStartTime, string strEndTime,string isModify)
        /// </summary>
        /// <returns>return true if over lap , else false</returns>
        bool IsModalityTimeSliceOverLap(string strGuid, string strModalityType, string strModality, string strStartTime, string strEndTime, string isModify, string strDateType, string strAvailableDate);
        /// <summary>
        /// Name:GetModalityTimeSlice
        /// Function:DataSet GetModalityTimeSlice(string Modality)
        /// </summary>
        /// <returns>return ModalityTimeSlice by modality</returns>
        DataSet GetModalityTimeSlice(string strModality);
        /// <summary>
        /// Name:GetModalityTimeSliceOverLapGuids
        /// Function:bool GetModalityTimeSliceOverLapGuids(string strGuid,string strModalityType, string strModality, string strStartTime, string strEndTime,string isModify)
        /// </summary>
        /// <returns>return over laped guids</returns>
        DataSet GetModalityTimeSliceOverLapGuids(string strGuid, string strModalityType, string strModality, string strStartTime, string strEndTime, string isModify, string strDateType, string strAvailableDt);
        /// <summary>
        /// Add timeslice in bulk
        /// </summary>
        /// <param name="strModalityType"></param>
        /// <param name="strModality"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="strMaxNumber"></param>
        /// <param name="strDomain"></param>
        /// <param name="interval"></param>
        /// <param name="strDateTypes"></param>
        void BulkAddModalityTimeSlice(string strModalityType, string strModality, string startTime, string endTime, string strMaxNumber, string strDomain, string interval, string strDateTypes, string strAvailableDt);

        void UpdateAvailableTime(string strModalityType, string strModality, string availabeDate, string exAvailableDate, string datetype);
        DataSet GetShareSettings(string timeSliceGuid);
        bool SaveModalityShare(string timeSliceGuids, DataSet model);
        bool LockModalityQuota(string parameters, out string lockGuid);
    }
}
