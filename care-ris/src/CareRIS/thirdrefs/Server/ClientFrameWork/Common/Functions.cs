using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.ClientFramework.Common
{
    public  class Functions
    {
        public static DataSet ConvertToTypedDataSet(DataSet ds, string czTypeName)
        {
            DataSet Ds1;
            try
            {
                Ds1 = Activator.CreateInstance(Type.GetType(czTypeName)) as DataSet;
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        DataRow newRow = Ds1.Tables[i].NewRow();
                        for (int k = 0; k < ds.Tables[i].Rows[j].ItemArray.Length; k++)
                        {
                            newRow[k] = ds.Tables[i].Rows[j].ItemArray[k];
                        }
                        Ds1.Tables[i].Rows.Add(newRow);
                    }
                }
                Ds1.AcceptChanges();
            }
            catch { Ds1 = null; }

            return Ds1;
        }

        public static String ToModuleIdString(int iValue)
        {
            //Add toUpper() to resolve case senstive in oracle
            return System.Convert.ToString(iValue, 16).PadLeft(4, '0').ToUpper();
           
        }
    }
}
