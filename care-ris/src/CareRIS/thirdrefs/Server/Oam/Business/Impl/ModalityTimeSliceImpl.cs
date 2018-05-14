using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.DAO.Oam;
using Common.ActionResult;

namespace Server.Business.Oam
{
    public class ModalityTimeSliceImpl : IModalityTimeSliceService
    {
        private IModalityTimeSliceSettingDAO modalityTimeSliceDAO = DataBasePool.Instance.GetDBProvider();

        public string AddModalityTimeSliceEx(string strModalityType, string strModality, string strStartTime, string strEndTime, string strDescription, string strMaxMan, string strDomain, string strDateTypes, string strAvailableDt)
        {
            return modalityTimeSliceDAO.AddModalityTimeSliceEx(strModalityType, strModality, strStartTime, strEndTime, strDescription, strMaxMan, strDomain, strDateTypes, strAvailableDt);
        }

        public bool ModifyModalityTimeSlice(string strGuid, string strStartTime, string strEndTime, string strDescription, string strMaxMan, string strDateType)
        {
            try
            {
                return modalityTimeSliceDAO.ModifyModalityTimeSlice(strGuid, strStartTime, strEndTime, strDescription, strMaxMan, strDateType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }

        public bool DeleteModalityTimeSlice(string strGuid)
        {
            try
            {
                return modalityTimeSliceDAO.DeleteModalityTimeSlice(strGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }

        public bool IsModalityTimeSliceOverLap(string strGuid, string strModalityType, string strModality, string strStartTime, string strEndTime, string isModify, string strDateType, string strAvailableDate)
        {
            try
            {
                return modalityTimeSliceDAO.IsModalityTimeSliceOverLap(strGuid, strModalityType, strModality, strStartTime, strEndTime, isModify, strDateType, strAvailableDate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }

        public DataSet GetModalityTimeSlice(string strModality)
        {
            try
            {
                return modalityTimeSliceDAO.GetModalityTimeSlice(strModality);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetModalityTimeSliceOverLapGuids(string strGuid, string strModalityType, string strModality, string strStartTime, string strEndTime, string isModify, string strDateType, string strAvailableDt)
        {
            try
            {
                return modalityTimeSliceDAO.GetModalityTimeSliceOverLapGuids(strGuid, strModalityType, strModality, strStartTime, strEndTime, isModify, strDateType, strAvailableDt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void BulkAddModalityTimeSlice(string strModalityType, string strModality, string startTime, string endTime,
            string strMaxNumber, string strDomain, string interval, string strDateTypes, string strAvailableDt)
        {
            try
            {
                modalityTimeSliceDAO.BulkAddModalityTimeSlice(strModalityType, strModality, startTime, endTime,
                    strMaxNumber, strDomain, interval, strDateTypes, strAvailableDt);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void UpdateAvailableTime(string strModalityType, string strModality, string availabeDate, string exAvailableDate, string datetype)
        {
            modalityTimeSliceDAO.UpdateAvailableTime(strModalityType, strModality, availabeDate, exAvailableDate, datetype);
        }

        public DataSet GetShareSettings(string timeSliceGuid)
        {
            try
            {
                return modalityTimeSliceDAO.GetShareSettings(timeSliceGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool SaveModalityShare(string timeSliceGuids, DataSet model)
        {
            try
            {
                return modalityTimeSliceDAO.SaveModalityShare(timeSliceGuids, model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }

        public bool LockModalityQuota(string parameters, out string lockGuid)
        {
            try
            {
                return modalityTimeSliceDAO.LockModalityQuota(parameters, out lockGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
        }

    }
}
