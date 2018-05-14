using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.DAO.Oam;
using Server.DAO.Oam.Impl;
using CommonGlobalSettings;
namespace Server.Business.Oam.Impl
{
    public class ConditionColServiceImpl : IConditionColService
    {
        private IConditionColDao ConditionColDao = DataBasePool.Instance.GetDBProvider();

        public virtual DataSet GetAllConditionItems()
        {
            return ConditionColDao.GetAllConditionItems();
        }

        public virtual bool SaveColChange(BaseDataSetModel model)
        {
            return ConditionColDao.SaveColChange(model);
        }

        public virtual DataSet GetAllExclusionConditions()
        {
            return ConditionColDao.GetAllExclusionConditions();
        }

        public virtual DataSet GetOperatorMap()
        {
            return ConditionColDao.GetOperatorMap();
        }

        public virtual DataSet GetDataSource(string sql)
        {
            return ConditionColDao.GetDataSource(sql);
        }

        public virtual bool SaveExclusionCondition(DataTable dt, string conditionName, string domain, string site)
        {
            return ConditionColDao.SaveExclusionCondition(dt, conditionName, domain, site);
        }

        public virtual DataSet GetExclusionConditionSqlByPanelTitle(string title)
        {
            return ConditionColDao.GetExclusionConditionSqlByPanelTitle(title);
        }

        public virtual DataSet GetAllHotKeys()
        {
            return ConditionColDao.GetAllHotKeys();
        }

        public virtual bool SaveHotKeys(BaseDataSetModel model)
        {
            return ConditionColDao.SaveHotKeys(model);
        }

        public virtual DataSet GetAllMessageConfigs()
        {
            return ConditionColDao.GetAllMessageConfigs();
        }

        public virtual bool SaveMessageConfigs(BaseDataSetModel model)
        {
            return ConditionColDao.SaveMessageConfigs(model);
        }
    }
}
