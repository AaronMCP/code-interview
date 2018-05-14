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
/****************************************************************************/
#endregion

using System;
using System.Data;
using System.Configuration;

namespace Kodak.GCRIS.Oam.Common.Utilities.ActionModel
{
    /// <summary>
    /// The Model for the operation about Role
    /// </summary>
    public class RoleModel : BaseModel
    {
        private string roleName = null;
        private string description = null;

        public string RoleName 
        {
            get
            {
                return roleName;
            }
            set
            {
                roleName = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
    }
}
