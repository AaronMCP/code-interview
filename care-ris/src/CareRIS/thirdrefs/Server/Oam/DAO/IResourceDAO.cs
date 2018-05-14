using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;

namespace Server.DAO.Oam
{
    public interface IResourceDAO
    {
        DataSet GetResourceDataSet(string strSite);
        ResourceModel QueryResource(string modalityName);
        bool AddResource(ResourceModel model);
        bool DeleteResource(ResourceModel model);
        bool UpdateResource(ResourceModel model);
        bool AddModalityType(ResourceModel model);
        bool DeleteModalityType(ResourceModel model);
    }
}
