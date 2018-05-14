using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Server.ReportDAO
{
    /// <summary>
    /// DAO Layer interface
    /// </summary>
    public interface IReportDAO
    {
        /// <summary>
        /// Execute method
        /// </summary>
        /// <param name="param">Filters collection</param>
        /// <returns>Returns</returns>
        object Execute(object param);
       
    }

    public interface IReportNewDAO
    {
        /// <summary>
        /// Execute method
        /// </summary>
        /// <param name="param">Filters collection</param>
        /// <returns>Returns</returns>       
        object Execute(object param, ref string strObjectGuids);
    }

    /// <summary>
    /// interface that gets user-defined grid columns
    /// </summary>
    public interface IReportGridColumn
    {
        /// <summary>
        /// Get User-Defined Grid Columns
        /// </summary>
        /// <param name="gridName"></param>
        /// <param name="userGuid"></param>
        /// <returns>DataSet contains columns' info</returns>
        DataSet GetGridCol(string gridName, string userGuid);
    }
}
