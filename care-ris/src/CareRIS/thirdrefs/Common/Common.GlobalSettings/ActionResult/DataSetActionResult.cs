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
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Common.ActionResult
{
    /// <summary>
    /// Summary description for DataSetActionResult
    /// </summary>
    [Serializable()]
    public class DataSetActionResult : BaseActionResult
    {
        private DataSet dataSet = null;

        public DataSetActionResult()
        {

        }

        public DataSetActionResult(bool result) : base(result)
        {

        }

        public DataSetActionResult(bool result, string returnMessage) : 
            base(result, returnMessage)
        {

        }

        public DataSet DataSetData
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
