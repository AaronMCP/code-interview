using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;
using Server.ClientFramework.Common.Data.Profile ;
using Common.ActionResult.Framework;


namespace Server.ClientFrameworkAction.Action
{
    /// <summary>
    /// Load  ConfigDic information in DataSet
    /// </summary>
    public class LoadDsConfigDicAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();

            ConfigDic obj = new ConfigDic();
            DsConfigDic ds = obj.Load(1);
            result.Result = true;
            result.DataSetData = ds;
            return result;
        }
    }
}
