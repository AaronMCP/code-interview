using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;
using Server.ClientFramework.Common.Data.Panels;
using Common.ActionResult.Framework;


namespace Server.ClientFrameworkAction.Action
{
    /// <summary>
    /// Load  panel information in DataSet
    /// </summary>
    public class LoadDsPanelInfoAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            DsPanelInfoActionResult result = new DsPanelInfoActionResult();

            ModulesAndPanels m = new ModulesAndPanels();
            DsPanelInfo info = m.Load();

            
            result.Result = true;
            result.DataSetData = info;
            return result;
        }
    }
}
