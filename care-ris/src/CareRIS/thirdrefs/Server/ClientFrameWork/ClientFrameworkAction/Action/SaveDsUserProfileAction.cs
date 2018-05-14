using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;
using Server.ClientFramework.Common.Data.Profile;
using Common.ActionResult.Framework;

using Server.ClientFramework.Common;

namespace Server.ClientFrameworkAction.Action
{
    /// <summary>
    /// Load  user profile in DataSet
    /// </summary>
    public class SaveDsUserProfileAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            BaseActionResult result = new BaseActionResult();

            UserProfile profile = new UserProfile();
//            profile.Save(Functions.ConvertToTypedDataSet((context.Model as SaveUserProfileModel).DataSet, typeof(DsProfile).FullName) as DsProfile ,
            profile.Save((context.Model as SaveUserProfileModel).DataSet, 
                         (context.Model as SaveUserProfileModel).RoleName,
                         (context.Model as SaveUserProfileModel).UserGUID 
                        );
            result.Result = true;
            return result;
        }
    }
}
