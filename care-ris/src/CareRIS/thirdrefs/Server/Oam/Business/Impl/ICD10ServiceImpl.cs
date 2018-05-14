using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.DAO.Oam;
using Server.DAO.Oam.Impl;
using CommonGlobalSettings;
namespace Server.Business.Oam.Impl
{
    public class ICD10ServiceImpl:IICD10Service
    {
        private IICD10DAO ICD10Dao = DataBasePool.Instance.GetDBProvider();

        public virtual DataSet SearchICD10(string condition)
        {
            return ICD10Dao.SearchICD10(condition);
        }

        public virtual DataSet GetAllICD10()
        {
            return ICD10Dao.GetAllICD10();
        }
        public virtual bool ModifyICD10(BaseDataSetModel model)
        {
            return ICD10Dao.ModifyICD10(model);
        }

        public virtual bool AddICD10(BaseDataSetModel model)
        {
            return ICD10Dao.AddICD10(model);
        }

        public virtual bool DeleteICD10(string parameters)
        {
            return ICD10Dao.DeleteICD10(parameters);
        }

        public virtual bool ImportICD10(DataSet icd10DataSet, bool isClear)
        {
            return ICD10Dao.ImportICD10(icd10DataSet, isClear);
        }
    }
}
