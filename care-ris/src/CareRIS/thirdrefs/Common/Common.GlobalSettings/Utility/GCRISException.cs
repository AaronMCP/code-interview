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
/*     Author : Caron Zhao                                                  */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace CommonGlobalSettings
{
    /// <summary>
    /// Exception thrown when there is an error processing the
    /// configuration information for an application.
    /// </summary>
    public class GCRISException : System.Exception
    {
        public GCRISException() : base()
        {

        }

        public GCRISException(string s) : base(s)
        {

        }

        protected GCRISException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        public GCRISException(string s, System.Exception e) : base(s, e)
        {

        }
    }
}
