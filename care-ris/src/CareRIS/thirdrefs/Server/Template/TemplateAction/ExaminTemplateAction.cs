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
/*   Author : Terrence Jiang                                                                       */
/*   Create Date: July.2006
/****************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using Common.Action;
using Common.ActionResult;
using Server.Business.Templates;
using System.Data;
using LogServer;
using CommonGlobalSettings;
using System.Windows.Forms;
using CommonGlobalSettings;
using Server.Utilities.LogFacility;
using CommonGlobalSettings;
using UC=CommonGlobalSettings;

namespace Server.TemplatesActions.Action
{
    public class ExaminTemplateAction : BaseAction
    {
        private IExaminTemplateService ExaminTemplateService = BusinessFactory.Instance.GetExaminTemplateService();
        LogManagerForServer lm = new LogManagerForServer("TemplateServerLoglevel", "0C00");
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dsAr = new DataSetActionResult();
            dsAr.DataSetData = new DataSet();
            try
            {
                switch (context.MessageName.Trim())
                {
                    
                    case "LoadExaminTemplatesByModality":
                        return LoadExaminTemplatesByModality(context.Parameters);
                    case "AddExaminTemplate":
                        return AddExaminTemplate(context.Model as ExaminTemplateModel);
                    case "EditExaminTemplate":
                        return EditExaminTemplate(context.Model as ExaminTemplateModel);
                    case "DeleteExaminTemplate":
                        return DeleteExaminTemplate(context.Parameters);
                    case "LoadExaminTemplateByGuid":
                        return LoadExaminTemplateByGuid(context.Parameters);
                    case "LoadExaminTemplateByShortcut":
                        return LoadExaminTemplateByShortcut(context.Parameters);
                    case "ExistExaminShortcut":
                        return ExistShortcut(context.Parameters);
                    case "ExistExaminTemplateName":
                        return ExistExaminTemplateName(context.Parameters);
                    default:
                        {
                            dsAr.ReturnMessage = null;
                            dsAr.Result = false;
                            return dsAr;
                        }

                }



            }
            catch (Exception e)
            {
                lm.Error((long)ModuleEnum.Templates_WS, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dsAr.ReturnMessage = null;
                dsAr.Result = false;
                return dsAr;

            }
        }
        private BaseActionResult ExistExaminTemplateName(string parameters)
        {
              BaseActionResult result = new BaseActionResult();
            string strTemplateName = UC.Utilities.GetParameter("TemplateName", parameters);
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            //Global: type=0; User:type=1
            int type = Convert.ToInt32(UC.Utilities.GetParameter("Type", parameters));
            if (ExaminTemplateService.ExistExaminTemplateName(strTemplateName,strUserGuid,type))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        //The follow functions have same comments as commented in DAO
        private BaseActionResult ExistShortcut(string parameters)
        {

            BaseActionResult result = new BaseActionResult();
            string strShortcut = UC.Utilities.GetParameter("Shortcut", parameters);
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            string site = UC.Utilities.GetParameter("Site", parameters);
            //Global: type=0; User:type=1
            int type = Convert.ToInt32(UC.Utilities.GetParameter("Type", parameters));
            if (ExaminTemplateService.ExistExaminShortcut(strShortcut, strUserGuid, type, site))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private DataSetActionResult LoadExaminTemplateByShortcut(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strShortcut = UC.Utilities.GetParameter("Shortcut", parameters);
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            string site = UC.Utilities.GetParameter("Site", parameters);
            DataSet myDataSet = ExaminTemplateService.LoadExaminTemplateByShortcut(strShortcut, strUserGuid, site);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private DataSetActionResult LoadExaminTemplateByGuid(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            DataSet myDataSet = ExaminTemplateService.LoadExaminTemplateByGuid(strTemplateGuid);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

       
        private DataSetActionResult LoadExaminTemplatesByModality(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strModalityType = UC.Utilities.GetParameter("ModalityType", parameters);
            int type = Convert.ToInt32(UC.Utilities.GetParameter("Type", parameters));
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            string site = UC.Utilities.GetParameter("Site", parameters);
            DataSet myDataSet = ExaminTemplateService.LoadExaminTemplatesByModality(strModalityType, type, strUserGuid, site);
            if (myDataSet.Tables[0].Rows.Count > 0)
            {
                result.DataSetData = myDataSet;
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult AddExaminTemplate(ExaminTemplateModel model)
        {
            BaseActionResult result = new BaseActionResult();

            if (ExaminTemplateService.AddExaminTemplate(model))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult EditExaminTemplate(ExaminTemplateModel model)
        {
            BaseActionResult result = new BaseActionResult();

            if (ExaminTemplateService.EditExaminTemplate(model))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult DeleteExaminTemplate(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            if (ExaminTemplateService.DeleteExaminTemplate(strTemplateGuid))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }


    }
}
