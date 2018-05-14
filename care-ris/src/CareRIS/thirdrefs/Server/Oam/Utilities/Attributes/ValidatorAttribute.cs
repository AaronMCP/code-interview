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
using System.Collections.Generic;
using System.Text;

namespace Server.Utilities.Oam.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public abstract class ValidatorAttribute : Attribute
    {
        /// <summary>
        /// Validates the specified value.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <exception cref="OamException">The validation fails.</exception>
        public abstract void Validate(object value);
    }
}
