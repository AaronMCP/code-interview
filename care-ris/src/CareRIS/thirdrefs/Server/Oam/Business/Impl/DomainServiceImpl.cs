using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.DAO.Oam;
using Server.DAO.Oam.Impl;
using System.Text.RegularExpressions;
using CommonGlobalSettings;
using Common.ActionResult;
using System.Web;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class DomainServiceImpl : IDomainService
    {

        IDomainDao Domain = DataBasePool.Instance.GetDBProvider();
  
        public int DomainList(BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {

                if (!Domain.DomainList(dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }



            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;



            }

            return dsar.recode;
        }

        public int SyncDomainSiteList(BaseActionResult bar)
        {
            string strError = "";
            DataSetActionResult dsar = bar as DataSetActionResult;
            dsar.recode = 0;
            dsar.Result = true;
            try
            {

                if (!Domain.SyncDomainSiteList(dsar.DataSetData, ref strError))
                {
                    throw new Exception(strError);
                }



            }
            catch (Exception ex)
            {
                dsar.recode = -1;
                dsar.ReturnMessage = ex.Message;
                dsar.Result = false;



            }

            return dsar.recode;
        }

        public int AddDomain(DataTable dtDomain, BaseActionResult bar)
        {
            string strError = "";

            bar.recode = 0;
            bar.Result = true;
            try
            {

                if (!Domain.AddDomain(dtDomain, ref strError))
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
        public int ModifyDomain(DataTable dtDomain, BaseActionResult bar)
        {
            string strError = "";

            bar.recode = 0;
            bar.Result = true;
            try
            {

                if (!Domain.ModifyDomain(dtDomain, ref strError))
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
        public int DelDomain(string strDomain, BaseActionResult bar)
        {
            string strError = "";

            bar.recode = 0;
            bar.Result = true;
            try
            {

                if (!Domain.DelDomain(strDomain, ref strError))
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
        public int AddSite(DataTable dtSite, BaseActionResult bar)
        {
            string strError = "";

            bar.recode = 0;
            bar.Result = true;
            try
            {

                if (!Domain.AddSite(dtSite, ref strError))
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

        public int ModifySite(DataTable dtSite, BaseActionResult bar)
        {
            string strError = "";

            bar.recode = 0;
            bar.Result = true;
            try
            {

                if (!Domain.ModifySite(dtSite, ref strError))
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

        public DataSet GetSiteProfileDataSet(string domainName, string siteName)
        {
            try
            {
                return Domain.GetSiteProfileDataSet(domainName, siteName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditSiteProfile(SystemModel model, string domainName, string siteName)
        {
            try
            {
                return Domain.EditSiteProfile(model, domainName, siteName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddSiteProfile(string domainName, string siteName, string fieldName, string moduleID)
        {
            try
            {
                return Domain.AddSiteProfile(domainName, siteName, fieldName, moduleID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteSiteProfile(string domainName, string siteName, string fieldName, string moduleID)
        {
            try
            {
                return Domain.DeleteSiteProfile(domainName, siteName, fieldName, moduleID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
