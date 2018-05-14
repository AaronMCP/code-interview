using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common.Action;
using Common.ActionResult;
using CommonGlobalSettings;
using Server.Business.ClientFramework;

namespace Server.ClientFrameworkAction.Action
{
    public class LoadOnlienClientsAction : BaseAction
    {
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dar = new DataSetActionResult();
            dar.Result = true;
            Login login = new Login();
            System.Data.DataSet ds = login.GetOnlineClients();
            dar.DataSetData = ds;
            return dar;
        }
    }
}
