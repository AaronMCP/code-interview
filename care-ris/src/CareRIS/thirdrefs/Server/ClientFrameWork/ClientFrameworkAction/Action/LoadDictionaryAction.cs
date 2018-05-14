using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;
using Common.ActionResult.Framework;



namespace Server.ClientFrameworkAction.Action
{
 
    /// <summary>
    /// Load  System information in DataSet
    /// </summary>
    public class LoadDictionaryAction : BaseAction
    {
        /// <summary>
        ///  override method of BaseAction.<br></br>
        /// </summary>
        /// <param name="context">input parameter</param>
        /// <returns>if successful,return the result.</returns>
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult result = new DataSetActionResult();

            DictionaryBusiness dic = new DictionaryBusiness();
            result.DataSetData=dic.Load();
            result.Result = true;
           
            return result;
        }
    }
}
