#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*   Author : Fred Li                                                       */
/****************************************************************************/
#endregion

namespace Server.ClientFramework.Common.Data.Panels
{

    /// <summary>
    /// It's Dataset, storage 2 tables: Module and Panel
    /// </summary>
    partial class DsPanelInfo
    {
        partial class PanelDataTable
        {
        }
        /// <summary>
        /// (Parameter & 2) == 0 (IsFunctionPanel)  means will be displayed 
        /// </summary>
        public partial class PanelRow : System.Data.DataRow
        {
            public bool IsListedPanel
            {
                get { return (Parameter & 2) == 0; }
            }
        }
        /// <summary>
        /// (Parameter & 2) == 0 (IsFunctionPanel)  means will be displayed 
        /// </summary>
        public partial class ModuleRow : System.Data.DataRow
        {
            public bool IsListedModule
            {
                get { return (Parameter & 2) == 0; }
            }
        }
    }
}
