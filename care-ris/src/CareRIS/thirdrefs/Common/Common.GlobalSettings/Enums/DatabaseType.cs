using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    /// <summary>
    ///  Enumeration for Database types
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// Sybase database
        /// </summary>
        Sybase = 0,
        /// <summary>
        /// MS SQL Server database
        /// </summary>
        SQLSERVER = 1,
        /// <summary>
        /// Oracel database
        /// </summary>
        ORACLE = 2,
        /// <summary>
        /// unknown database
        /// </summary>
        UNKNOWN
    }
}
