using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;

namespace Server.Business.Oam
{
    public interface ISystemProfileService
    {
        DataSet GetSystemProfileDataSet(string strDomain);
        bool EditSystemProfile(SystemModel model, string strDomain);
        //GCRIS 2.0 CPE Part Number: 7H1634
        DataSet GetAllWarningTimeSet();
        DataSet GetWarningTimeSelectConditionSet();
        bool AddNewWarningTime(SystemModel model);
        bool DeleteWarningTime(SystemModel model);
        bool UpdateWarningTime(SystemModel model);
        DataSet GetAllGridColumnOptionListNames();
        DataSet GetAllGridColumnOptionRows(string strListName);
        bool UpdateGridColumnOptionTable(SystemModel model,string strListName);

    }
}
