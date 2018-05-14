#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2007                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/*   Author : Andy Bu                                                       */
/*   Created : 2007Äê3ÔÂ9ÈÕ                                                       */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(GeneralStatisticModel))]
    public abstract class StatisticBaseModel : BaseModel
    {
    }
}
