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
using System.Data;


namespace CommonGlobalSettings
{
    [Serializable()]
    public class GeneralStatisticModel : StatisticBaseModel
    {
        string _szStoreProcedureName;
        string _queryid;

        public string QueryID
        {
            get { return _queryid; }
            set { _queryid = value; }
        }

        public string StoreProcedureName
        {
            get { return _szStoreProcedureName; }
            set { _szStoreProcedureName = value; }
        }

        public DataSet queryconsoledata;
        public DataSet queryedata;
        public DataSet dsUsers;
    }
}
