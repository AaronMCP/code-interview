using System;
using System.Collections.Generic;
using System.Text;
using Server.DAO;
using Server.DAO.ClientFramework;


namespace Server.Business.ClientFramework
{
   
    public class DictionaryBusiness : MarshalByRefObject
    {
        public DictionaryBusiness() { }
        /// <summary>
      
        /// <summary>
        /// Load saved data 
        /// </summary>
        /// <param name="ModuleID">ID of module for load</param>
        /// <returns></returns>
        public System.Data.DataSet Load()
        {
           
            return DaoInstanceFactory.GetInstance().LoadDictionary ();
        }

       
    }
}
