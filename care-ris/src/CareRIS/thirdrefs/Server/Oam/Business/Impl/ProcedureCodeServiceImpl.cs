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
using Server.DAO.Oam;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class ProcedureCodeServiceImpl : IProcedureCodeService
    {
        private IProcedureCodeDAO procedureCodeDAO = DataBasePool.Instance.GetDBProvider();


        public DataSet GetProceTimeSliceDuration(string timeSliceDur)
        {
            try
            {
                return procedureCodeDAO.GetProceTimeSliceDuration(timeSliceDur);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetProcedureCodeList(string strDomain, string strSite)
        {
            try
            {
                return procedureCodeDAO.GetProcedureCodeList(strDomain,strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddProcedureCode(ProcedureCodeModel model)
        {
            try
            {
                return procedureCodeDAO.AddProcedureCode(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeleteProcedureCode(string procedureCode, string site)
        {
            try
            {
                return procedureCodeDAO.DeleteProcedureCode(procedureCode,site);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ModifyProcedureCode(ProcedureCodeModel model)
        {
            try
            {
                return procedureCodeDAO.ModifyProcedureCode(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QueryExamSystem(string modalityType, string bodyPart)
        {
            try
            {
                return procedureCodeDAO.QueryExamSystem(modalityType, bodyPart);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QueryAllExamSystem()
        {
            try
            {
                return procedureCodeDAO.QueryAllExamSystem();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool QueryBodyCategory(string categoryName, string description, string shortcutCode)
        {
            try
            {
                return procedureCodeDAO.QueryBodyCategory(categoryName, description, shortcutCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddBodyCategory(string tag, string categoryName, string description, string shortcutCode)
        {
            try
            {
                return procedureCodeDAO.AddBodyCategory(tag, categoryName, description, shortcutCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsBodyPartExist(string modalityType, string bodyPart, string examSystem)
        {
            try
            {
                return procedureCodeDAO.IsBodyPartExist(modalityType, bodyPart, examSystem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddBodyPart(string modalityType, string bodyPart, string examSystem, string domain)
        {
            try
            {
                return procedureCodeDAO.AddBodyPart(modalityType, bodyPart, examSystem, domain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet QueryBodyPart(string modalityType, string site)
        {
            try
            {
                return procedureCodeDAO.QueryBodyPart(modalityType, site);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataSet QueryCheckingItem(string modalityType)
        {
            try
            {
                return procedureCodeDAO.QueryCheckingItem(modalityType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetSiteProcedureCode(string site)
        {
            try
            {
                return procedureCodeDAO.GetSiteProcedureCode(site);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    
        public DataSet QueryChargeTypeFee(string procedureCode)
        {
            try
            {
                return procedureCodeDAO.QueryChargeTypeFee(procedureCode);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ModifyProcedureCodeFrequency(ProcedureCodeModel model)
        {
            try
            {
                return procedureCodeDAO.ModifyProcedureCodeFrequency(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Copy2Site(string site, string domain)
        {
            try
            {
                return procedureCodeDAO.Copy2Site(site, domain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Delall4Site(string site, string domain)
        {
            try
            {
                return procedureCodeDAO.Delall4Site(site, domain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
