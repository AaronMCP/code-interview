using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.DAO.Oam;
using Server.DAO.Oam.Impl;
using System.Text.RegularExpressions;
using Common.ActionResult;

namespace Server.Business.Oam.Impl
{
    public class AdministratorToolServiceImpl : IAdministratorToolService
    {
        private IAdministratorToolDAO atDAO = DataBasePool.Instance.GetDBProvider();
        public int QueryLock(string stOwner, string stBeginTime, string stEndTime, BaseActionResult bar)
        {
            
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            try
            {
                dsar.Result = atDAO.QueryLock(stOwner, stBeginTime, stEndTime, dsar.DataSetData, ref strError);
                dsar.ReturnMessage = strError;
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
            }
            return dsar.recode;
        }

        public int UnLock(int strObjectType, int nSyncType, string strObjectGuid, string strOwner, BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                if (!atDAO.UnLock(strObjectType, nSyncType, strObjectGuid, strOwner, ref strError))
                {
                    throw new Exception(strError);
                }

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;           
            }

            return bar.recode;
        }


        public int QueryOnline(BaseActionResult bar)
        {
            
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            try
            {

                dsar.Result = atDAO.QueryOnline(dsar.DataSetData, ref strError);
                dsar.ReturnMessage = strError;
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
            }

            return dsar.recode;
        }

        public int SetOffline(string strUserGuid, BaseActionResult bar)
        {

            string strError = "";
            bar.recode = 0;
            bar.Result = true;
            try
            {
                if (!atDAO.SetOffline(strUserGuid,ref strError))
                {
                    throw new Exception(strError);
                }

            }
            catch (Exception ex)
            {
                bar.recode = -1;
                bar.ReturnMessage = ex.Message;
                bar.Result = false;
            }

            return bar.recode;
        }
    }
}
