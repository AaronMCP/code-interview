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
using CommonGlobalSettings;
using UC=CommonGlobalSettings;
using Server.Utilities.LogFacility;

namespace Server.TemplatesActions.Action
{
    public class PhraseTemplateAction:BaseAction
    {
         private IPhraseTemplateService phraseTemplateService = BusinessFactory.Instance.GetPhraseTemplateService();
        LogManagerForServer logger = new LogManagerForServer("TemplateServerLoglevel", "0C00");
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dsAr = new DataSetActionResult();
            dsAr.DataSetData = new DataSet();
            try
            {
                switch (context.MessageName.Trim())
                {
                    case "GetTemplateModalityType":
                        return GetModalityType();
                    case "LoadPhraseTemplatesByModality":
                        return LoadPhraseTemplatesByModality(context.Parameters);
                    case "AddPhraseTemplate":
                        return AddPhraseTemplate(context.Model as PhraseTemplateModel);
                    case "EditPhraseTemplate":
                        return EditPhraseTemplate(context.Model as PhraseTemplateModel);
                    case "DeletePhraseTemplate":
                        return DeletePhraseTemplate(context.Parameters);
                    case "LoadPhraseTemplateByGuid":
                        return LoadPhraseTemplateByGuid(context.Parameters);
                    case "LoadPhraseTemplateByShortcut":
                        return LoadPhraseTemplateByShortcut(context.Parameters);
                    case "ExistPhraseShortcut":
                        return ExistShortcut(context.Parameters);
                    case "ExistPhraseTemplateName":
                        return ExsitPhraseTemplateName(context.Parameters);
                    case "LoadChildren":
                        return LoadChildren(context.Parameters);
                    case "UpDownNode":
                        return UpDownNode(context.Parameters);
                    case "MovePhraseLeafNode":
                        return MovePhraseLeafNode(context.Parameters, context.UserID);
                    case "CopyPhraseLeafNode":
                        return CopyPhraseLeafNode(context.Parameters, context.UserID);
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
                logger.Error((long)ModuleEnum.Templates_WS, ModuleInstanceName.Templates, 1, e.Message, Application.StartupPath.ToString(),
                   (new System.Diagnostics.StackFrame(true)).GetFileName(),
                   Convert.ToInt32(new System.Diagnostics.StackFrame(true).GetFileLineNumber().ToString()));
                dsAr.ReturnMessage = null;
                dsAr.Result = false;
                return dsAr;

            }
        }
        private BaseActionResult ExsitPhraseTemplateName(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strTemplateName = UC.Utilities.GetParameter("TemplateName", parameters);
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            //Global: type=0; User:type=1
            int type = Convert.ToInt32(UC.Utilities.GetParameter("Type", parameters));
            if (phraseTemplateService.ExistPhaseTemplateName(strTemplateName,strUserGuid,type))
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
            //Global: type=0; User:type=1
            int type = Convert.ToInt32( UC.Utilities.GetParameter("Type", parameters));
            string templateGuid = UC.Utilities.GetParameter("templateGuid", parameters);
            if (phraseTemplateService.ExistShortcut(strShortcut, strUserGuid, type, templateGuid))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }
        private DataSetActionResult LoadPhraseTemplateByShortcut(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strShortcut = UC.Utilities.GetParameter("Shortcut", parameters);
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            DataSet myDataSet = phraseTemplateService.LoadPhraseTemplateByShortcut(strShortcut,strUserGuid);
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
        private DataSetActionResult LoadPhraseTemplateByGuid(string parameters)
        { 
             DataSetActionResult result = new DataSetActionResult();
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid",parameters);
            DataSet myDataSet = phraseTemplateService.LoadPhraseTemplateByGuid(strTemplateGuid);
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
        
        private DataSetActionResult GetModalityType()
        {
            DataSetActionResult result = new DataSetActionResult();
            DataSet myDataSet = phraseTemplateService.GetModalityType();
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
        private DataSetActionResult LoadPhraseTemplatesByModality(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strModalityType = UC.Utilities.GetParameter("ModalityType", parameters);
            int type = Convert.ToInt32(UC.Utilities.GetParameter("Type", parameters));
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            DataSet myDataSet = phraseTemplateService.LoadPhraseTemplatesByModality(strModalityType,type,strUserGuid);
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

        private DataSetActionResult LoadChildren(string parameters)
        {
            DataSetActionResult result = new DataSetActionResult();
            string strParentGuid = UC.Utilities.GetParameter("ParentGuid", parameters);
            int type = Convert.ToInt32(UC.Utilities.GetParameter("Type", parameters));
            string strUserGuid = UC.Utilities.GetParameter("UserGuid", parameters);
            DataSet myDataSet = phraseTemplateService.LoadChildren(strParentGuid, type, strUserGuid);
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

        private BaseActionResult AddPhraseTemplate(PhraseTemplateModel model)
        {
            BaseActionResult result = new BaseActionResult();
           
            if (phraseTemplateService.AddPhraseTemplate(model))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult EditPhraseTemplate(PhraseTemplateModel model)
        {
            BaseActionResult result = new BaseActionResult();

            if (phraseTemplateService.EditPhraseTemplate(model))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult DeletePhraseTemplate(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strItemGuid = UC.Utilities.GetParameter("ItemGuid", parameters);
            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", parameters);
            if (phraseTemplateService.DeletePhraseTemplate(strTemplateGuid,strItemGuid))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult UpDownNode(string parameters)
        {
            BaseActionResult result = new BaseActionResult();
            string strItemGuid1 = UC.Utilities.GetParameter("ItemGuid1", parameters);
            string strItemGuid2 = UC.Utilities.GetParameter("ItemGuid2", parameters);
            if (phraseTemplateService.UpDownNode(strItemGuid1, strItemGuid2))
            {
                result.Result = true;
            }
            else
            {
                result.Result = false;
            }

            return result;
        }

        private BaseActionResult CopyPhraseLeafNode(string parameters, string userguid)
        {
            string strItemID, strParentItemID, strUserUguid, strNewTemplateName, strNewItemID,strNewTemplateID, strOrderIndex;
            strItemID = strParentItemID = strNewTemplateName = strNewItemID = strNewTemplateID = strOrderIndex = "";
            BaseActionResult result = new BaseActionResult();
            strItemID = UC.Utilities.GetParameter("ItemGuid", parameters);
            strParentItemID = UC.Utilities.GetParameter("ParentItemGuid", parameters);
            result.Result = phraseTemplateService.CopyPhraseLeafNode(strItemID, strParentItemID, userguid, ref strNewTemplateName, ref strNewItemID, ref strNewTemplateID, ref strOrderIndex);
            result.ReturnString = strNewTemplateName + "|" + strNewItemID + "|" + strNewTemplateID + "|" + strOrderIndex;
            return result;
        }

        private BaseActionResult MovePhraseLeafNode(string parameters, string userguid)
        {
            string strItemID, strParentItemID, strUserUguid, strNewTemplateName, strOrderIndex;
            int orderIndex = 0;
            strItemID = strParentItemID = strNewTemplateName = strOrderIndex = "";
            BaseActionResult result = new BaseActionResult();
            strItemID = UC.Utilities.GetParameter("ItemGuid", parameters);
            strParentItemID = UC.Utilities.GetParameter("ParentItemGuid", parameters);
            result.Result = phraseTemplateService.MovePhraseLeafNode(strItemID, strParentItemID, userguid, ref strNewTemplateName, ref strOrderIndex);
            result.ReturnString = strNewTemplateName + "|" + strOrderIndex;
            return result;
        }

    

    }
}
