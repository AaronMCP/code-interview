using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;
namespace Server.DAO.Oam
{
    public interface IConditionColDao
    {
        DataSet GetAllConditionItems();
        bool SaveColChange(BaseDataSetModel model);
        DataSet GetAllExclusionConditions();
        DataSet GetOperatorMap();
        DataSet GetDataSource(string sql);
        bool SaveExclusionCondition(DataTable dt, string conditionName, string domain, string site);
        DataSet GetExclusionConditionSqlByPanelTitle(string title);
        DataSet GetAllHotKeys();
        bool SaveHotKeys(BaseDataSetModel model);
        DataSet GetAllMessageConfigs();
        bool SaveMessageConfigs(BaseDataSetModel model);
    }
}
