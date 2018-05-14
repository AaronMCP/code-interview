using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;
using Server.ClientFramework.Common.Data.Login;
using Common.ActionResult.Framework;


namespace Server.ClientFrameworkAction.Action
{
    /// <summary>
    /// Load  Role information in DataSet
    /// </summary>
    public class LoadDsRoleAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();

            Role objRole = new Role();
            DsRole ds = context.Parameters == string.Empty ? objRole.Load() : objRole.Load4Login();
            result.Result = true;
            result.DataSetData = ds;
            return result;
        }
    }
}
