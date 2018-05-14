#region FileBanner
/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       EASTMAN KODAK COMPANY                              */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to EASTMAN KODAK COMPANY, and may not be decompiled,       */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of EASTMAN KODAK COMPANY.                            */
/*                                                                          */
/****************************************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using CommonGlobalSettings;
using Common.Action;
using Common.ActionResult;
using Server.Business.Oam;
using CommonGlobalSettings;
using Common.ActionResult.Oam;

namespace Server.OamAction.Action
{
    public class ResourceManagerAction : BaseAction
    {
        private IResourceService resourceService = BusinessFactory.Instance.GetResourceService();

        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);

            if (actionName == null || actionName.Equals(""))
            {
                actionName = "list";
            }

            switch(actionName)
            {
                case "list":
                    return List(context.Parameters);
                case "query":
                    return Query(context.Parameters);
                case "add":
                    return Add(context.Model as ResourceModel);
                case "delete":
                    return Delete(context.Model as ResourceModel);
                case "modify":
                    return Modify(context.Model as ResourceModel);
            }

            //Default
            return List(context.Parameters);
        }

        private BaseActionResult List(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            try
            {
                string site = CommonGlobalSettings.Utilities.GetParameter("site", parameters);
                DataSet dataSet = resourceService.GetResourceDataSet(site);
                if (dataSet.Tables.Count > 0)
                {
                    result.Result = true;
                    result.DataSetData = dataSet;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch(Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private ResourceActionResult Query(string parameters)
        {
            string modalityName = CommonGlobalSettings.Utilities.GetParameter("modalityName", parameters);
            ResourceActionResult result = new ResourceActionResult();
            try
            {
                ResourceModel model = resourceService.QueryResource(modalityName);
                if (model != null)
                {
                    result.Result = true;
                    result.Model = model;
                }
                else
                {
                    result.Result = false;
                    result.ReturnMessage = "This item has been deleted by other client!";
                }
            }
            catch(Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult Add(ResourceModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (!model.AddModalityType)//add modality
                {
                    if (resourceService.AddResource(model))
                    {
                        result.Result = true;
                    }
                    else
                    {
                        result.Result = false;
                    }
                }
                else
                {
                    if (resourceService.AddModalityType(model))
                    {
                        result.Result = true;
                    }
                    else
                    {
                        result.Result = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult Delete(ResourceModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (!model.DeleteModalityType)//delete modality
                {
                    if (resourceService.DeleteResource(model))
                    {
                        result.Result = true;
                    }
                    else
                    {
                        result.Result = false;
                    }
                }
                else//delete modality type
                {
                    if (resourceService.DeleteModalityType(model))
                    {
                        result.Result = true;
                    }
                    else
                    {
                        result.Result = false;
                    }
                }
            }
            catch(Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }

        private BaseActionResult Modify(ResourceModel model)
        {
            BaseActionResult result = new BaseActionResult();
            try
            {
                if (resourceService.UpdateResource(model))
                {
                    result.Result = true;
                }
                else
                {
                    result.Result = false;
                }
            }
            catch(Exception ex)
            {
                result.Result = false;
                result.ReturnMessage = ex.Message;
            }

            return result;
        }
    }
}
