using System;
using System.Collections.Generic;
using System.Text;
using Server.DAO.Oam;
using Common.ActionResult;
using System.Data;

namespace Server.Business.Oam.Impl
{
    public class QualityScoringServiceImpl:IQualityScoringService
    {
        private IQualityScoringDAO qualityScoringDAO = DataBasePool.Instance.GetDBProvider();

        public bool QueryQualityScoringList(string strParam, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                string strWhere = "";
                char[] sep1 ={ '&' };
                char[] sep2 ={ '=' };
                char[] sep3 ={ ',' };
                string[] arrItems = strParam.Split(sep1);
                foreach (string str in arrItems)
                {
                    string[] arr = str.Split(sep2);
                    dic.Add(arr[0], arr[1]);
                }

                string strValue = "";
                dic.TryGetValue("PageIndex", out strValue);
                if(strValue==null||strValue.Length==0)
                {
                    throw new Exception("Param is invalid");
                }                
   
                int nPageIndex=Convert.ToInt32(strValue);

                dic.TryGetValue("PageSize", out strValue);
                if (strValue == null || strValue.Length == 0)
                {
                    throw new Exception("Param is invalid");
                }
                int nPageSize = Convert.ToInt32(strValue);

                dic.TryGetValue("RandomNum", out strValue);
                int nRandomNum = Convert.ToInt32(strValue);

                dic.TryGetValue("QueryObject", out strValue);
                int nQueryObject = Convert.ToInt32(strValue);

                dic.TryGetValue("IncludingAppraised", out strValue);
                string IncludingAppraised = strValue.Trim();

                int nTotalCount=0;
                if (!qualityScoringDAO.QueryQualityScoringList(strParam, nPageIndex, nPageSize, ref nTotalCount, dsar.DataSetData, ref strError, nRandomNum, nQueryObject, IncludingAppraised))
                {
                    throw new Exception(strError);
                }
                dsar.recode = nTotalCount;
                return true;
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
                throw new Exception(ex.Message);
                return false;
            }
      
        }

        public bool QueryScoringHistoryList(string strParam, BaseActionResult bar)
        {
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {
                dsar.Result = qualityScoringDAO.QueryScoringHistoryList(strParam, dsar.DataSetData);
                return true;
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                throw new Exception(ex.Message);
            }
        }

        public bool SaveAppraise(DataSet ds, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = false;
            try
            {
                if (qualityScoringDAO.SaveAppraise(ds,ref strError))
                {
                    dsar.recode = 1;
                    dsar.ReturnMessage = strError;
                    dsar.Result = true;
                    return true;
                }
                else
                {
                    dsar.recode = 0;
                    dsar.ReturnMessage = strError;
                    dsar.Result = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
                return false;
            }
        }

        public bool GetAppraise(string RPGuid,BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = false;
            try
            {
                if (qualityScoringDAO.GetAppraise(RPGuid, dsar.DataSetData, ref strError))
                {
                    dsar.recode = 1;
                    dsar.ReturnMessage = strError;
                    dsar.Result = true;
                    return true;
                }
                else
                {
                    dsar.recode = 0;
                    dsar.ReturnMessage = strError;
                    dsar.Result = false;
                    return false;
                }

            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
                throw new Exception(ex.Message);
            }
        }

        public string GetSettings(string strParam, ref string version)
        {
            return qualityScoringDAO.GetSettings(strParam, ref version);
        }

        public bool SaveSettings(string strParam, DataSet ds)
        {
            return qualityScoringDAO.SaveSettings(strParam, ds);
        }

        public bool GetAppraiseNew(string RPGuid, string type, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = false;
            try
            {
                if (qualityScoringDAO.GetAppraiseNew(RPGuid,type, dsar.DataSetData, ref strError))
                {
                    dsar.recode = 1;
                    dsar.ReturnMessage = strError;
                    dsar.Result = true;
                    return true;
                }
                else
                {
                    dsar.recode = 0;
                    dsar.ReturnMessage = strError;
                    dsar.Result = false;
                    return false;
                }

            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
                throw new Exception(ex.Message);
            }
        }

        public bool SaveAppraiseNew(DataSet ds, BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = false;
            try
            {
                if (qualityScoringDAO.SaveAppraiseNew(ds, ref strError))
                {
                    dsar.recode = 1;
                    dsar.ReturnMessage = strError;
                    dsar.Result = true;
                    return true;
                }
                else
                {
                    dsar.recode = 0;
                    dsar.ReturnMessage = strError;
                    dsar.Result = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;
                return false;
            }
        }
    }
}
