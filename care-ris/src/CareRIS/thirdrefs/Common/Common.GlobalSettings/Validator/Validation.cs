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
/*   Author : Andy Bu                                                       */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

namespace CommonGlobalSettings
{
    #region abstract class  for validation
    /// <summary>
    ///  abstract class definition for Validation.
    ///  In the Program, we always want to validate some input.
    /// this interface just defines the Validate method for inheritor
    /// </summary>
    public abstract class Validation
    {
        public virtual bool Validate(){return false;}
    }

    #endregion
}
