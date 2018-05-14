using System;
using System.Collections.Generic;
using System.Text;
using Server.ClientFramework.Common.Data.Panels;
using Server.DAO;
using Server.DAO.ClientFramework;

namespace Server.Business.ClientFramework
{
    public class ModulesAndPanels : MarshalByRefObject
    {

        public ModulesAndPanels()
        {
        }
        /// <summary>
        /// Load saved data 
        /// </summary>
        /// <param name="ModuleID"></param>
        /// <param name="RoleID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DsPanelInfo Load()
        {
            //Server.DAO.ClientFramework.ModulesAndPanels objDAO;
            
            //Need a factory here:
            return DaoInstanceFactory.GetInstance().LoadDsPanelInfo();
            //objDAO = new Server.DAO.ClientFramework.ModulesAndPanelsSql();

            //return objDAO.Load();
        }
    }
}
