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
using Server.Business.Oam;
using Server.DAO.Oam;
using CommonGlobalSettings;

namespace Server.Business.Oam.Impl
{
    public class ResourceServcieImpl : IResourceService
    {
        private IResourceDAO resourceDAO = DataBasePool.Instance.GetDBProvider();

        public DataSet GetResourceDataSet(string strSite)
        {
            try
            {
                return resourceDAO.GetResourceDataSet(strSite);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ResourceModel QueryResource(string modalityName)
        {
            try
            {
                return resourceDAO.QueryResource(modalityName);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddResource(ResourceModel model)
        {
            try
            {
                return resourceDAO.AddResource(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteResource(ResourceModel model)
        {
            try
            {
                return resourceDAO.DeleteResource(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateResource(ResourceModel model)
        {
            try
            {
                return resourceDAO.UpdateResource(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AddModalityType(ResourceModel model)
        {
            try
            {
                return resourceDAO.AddModalityType(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteModalityType(ResourceModel model)
        {
            try
            {
                return resourceDAO.DeleteModalityType(model);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
