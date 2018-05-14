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
using System.Data;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    public class BaseDataSetModel : BaseModel
    {
        private DataSet dataSet = null;

        public DataSet DataSetParameter 
        {
            get
            {
                return dataSet;
            }
            set
            {
                dataSet = value;
            }
        }
    }
}
