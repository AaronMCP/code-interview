using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;
using Server.ClientFramework.Common.Data.Profile;

using System.Data;

namespace Server.ClientFrameworkAction.Action
{
    /// <summary>
    /// Load Site information in DataSet
    /// </summary>
    public class LoadDsSiteProfileAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();

            SiteProfile systemProfile = new SiteProfile();
            DsSiteProfileModel model = context.Model as DsSiteProfileModel;
            DataSet ds = null;
            if (!string.IsNullOrWhiteSpace(model.SiteName))
            {
                ds = systemProfile.Load(model.ModuleID, model.SiteName);
            }
            else
            {
                ds = systemProfile.Load(model.ModuleID);
            }

            result.Result = true;
            result.DataSetData = ds;
            return result;
        }
    }
}
