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
    /// Load  user information in DataSet
    /// </summary>
    public class LoadDsUserProfileAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();

            UserProfile profile = new UserProfile();
            DsProfile ds = profile.Load((context.Model as DsUserProfileModel).ModuleID,
                                        (context.Model as DsUserProfileModel).RoleID,
                                        (context.Model as DsUserProfileModel).UserID 
                                        );
            result.Result = true;
            result.DataSetData = ds;
            return result;
        }
    }
}
