using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;
using Server.ClientFramework.Common.Data.Profile;
using Common.ActionResult.Framework;

using System.Data;

namespace Server.ClientFrameworkAction.Action
{
    /// <summary>
    /// Load  System information in DataSet
    /// </summary>
    public class LoadDsSystemProfileAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();

            SystemProfile systemProfile = new SystemProfile();
            DsProfile ds = systemProfile.Load((context.Model as DsSystemProfileModel ).ModuleID);
            result.Result = true;
            result.DataSetData = ds;
            return result;
        }
    }

    public class LoadAllProfileAction : BaseAction
    {
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();

            AllProfile allProfile = new AllProfile();

            DataSet ds = allProfile.LoadAllProfile();

            result.Result = true;
            result.DataSetData = ds;

            return result;
        }
    }
}
