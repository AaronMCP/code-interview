using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;
using Server.ClientFramework.Common.Data.Profile;
using Common.ActionResult.Framework;


namespace Server.ClientFrameworkAction.Action
{
    /// <summary>
    /// Load  role profile information in DataSet
    /// </summary>
    public class LoadDsRoleProfileAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();

            RoleProfile profile = new RoleProfile();
            DsProfile ds = profile.Load((context.Model as DsRoleProfileModel).ModuleID,
                                        (context.Model as DsRoleProfileModel).RoleID);
            result.Result = true;
            result.DataSetData = ds;
            return result;
        }
    }
}
