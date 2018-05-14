using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;
namespace Server.DAO.Oam
{
    public interface IICD10DAO
    {
        DataSet SearchICD10(string condition);
        DataSet GetAllICD10();
        bool ModifyICD10(BaseDataSetModel model);
        bool AddICD10(BaseDataSetModel model);
        bool DeleteICD10(string parameters);
        bool ImportICD10(DataSet icd10DataSet, bool isClear);
    }
}
