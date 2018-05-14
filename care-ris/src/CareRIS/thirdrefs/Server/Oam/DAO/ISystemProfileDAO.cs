using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;

namespace Server.DAO.Oam
{
    public interface ISystemProfileDAO
    {
        DataSet GetSystemProfileDataSet(string strDomain);
        bool EditSystemProfile(SystemModel model, string strDomain);
        //GCRIS 2.0 CPE Part Number: 7H1634
        DataSet GetAllWarningTimeSet();
        DataSet GetWarningTimeSelectConditionSet();
        bool AddNewWarningTime(SystemModel model);
        bool DeleteWarningTime(SystemModel model);
        bool UpdateWarningTime(SystemModel model);
        bool IsExistWarningTime(SystemModel model);
        DataSet GetAllGridColumnOptionListNames();
        DataSet GetAllGridColumnOptionRows(string strListName);
        bool UpdateGridColumnOptionTable(SystemModel model,string strListName);
        bool IsExistGridColumnOption(SystemModel model);
    }
}
