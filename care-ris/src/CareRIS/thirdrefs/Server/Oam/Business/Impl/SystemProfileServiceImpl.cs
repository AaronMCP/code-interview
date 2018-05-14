using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Business.Oam;
using Server.DAO.Oam;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class SystemProfileServiceImpl : ISystemProfileService
    {
        private ISystemProfileDAO systemProfileDAO = DataBasePool.Instance.GetDBProvider();

        public DataSet GetSystemProfileDataSet(string strDomain)
        {
            try
            {
                return systemProfileDAO.GetSystemProfileDataSet(strDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool EditSystemProfile(SystemModel model, string strDomain)
        {
            try
            {
                return systemProfileDAO.EditSystemProfile(model, strDomain);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region GCRIS 2.0 CPE Part Number: 7H1634
        public DataSet GetAllWarningTimeSet()
        {
            try
            {
                return systemProfileDAO.GetAllWarningTimeSet();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataSet GetWarningTimeSelectConditionSet()
        {
            try
            {
                return systemProfileDAO.GetWarningTimeSelectConditionSet();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool AddNewWarningTime(SystemModel model)
        {
            try
            {
                if (!systemProfileDAO.IsExistWarningTime(model))
                {
                    return systemProfileDAO.AddNewWarningTime(model);
                }
                else
                {
                    throw new Exception("Exist same Warning Time Setting");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteWarningTime(SystemModel model)
        {
            try
            {
                if (systemProfileDAO.IsExistWarningTime(model))
                {
                    return systemProfileDAO.DeleteWarningTime(model);
                }
                else
                {
                    throw new Exception("Not Exists this Warning Time Setting");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateWarningTime(SystemModel model)
        {
            try
            {
                if (systemProfileDAO.IsExistWarningTime(model))
                {
                    return systemProfileDAO.UpdateWarningTime(model);
                }
                else
                {
                    throw new Exception("Not Exists this Warning Time Setting");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataSet GetAllGridColumnOptionListNames()
        {
            try
            {
                return systemProfileDAO.GetAllGridColumnOptionListNames();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataSet GetAllGridColumnOptionRows(string strListName)
        {
            try
            {
                return systemProfileDAO.GetAllGridColumnOptionRows(strListName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateGridColumnOptionTable(SystemModel model, string strListName)
        {
            try
            {
                //if (systemProfileDAO.IsExistGridColumnOption(model))
                //{
                return systemProfileDAO.UpdateGridColumnOptionTable(model, strListName);
                //}
                //else
                //{
                  //  throw new Exception("Not Exists This Record");
               // }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}
