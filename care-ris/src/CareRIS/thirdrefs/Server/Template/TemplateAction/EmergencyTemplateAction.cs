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
/*                        Author : Bruce Deng
/****************************************************************************/


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


namespace Server.TemplatesActions.Action
{
    public class EmergencyTemplateAction : BaseAction
    {
        private IEmergencyTemplateService eyTemplateService = BusinessFactory.Instance.GetEmergencyTemplateService();
        public override BaseActionResult Execute(Context context)
        {
            DataSetActionResult dsAr = new DataSetActionResult();
            dsAr.DataSetData = new DataSet();
            try
            {
                string strError = "";
                BaseDataSetModel bdsm = context.Model as BaseDataSetModel;
                switch (context.MessageName.Trim())
                {
                    case "QueryEYTemplate":
                        {
                            string strTemplateType = UC.Utilities.GetParameter("TemplateType", context.Parameters);
                            dsAr.Result = eyTemplateService.QueryEYTemplate(dsAr.DataSetData, strTemplateType,ref strError);
                            dsAr.ReturnMessage = strError;
                        }
                        break;

                    case "SaveEYTemplate":
                        {

                            dsAr.Result = eyTemplateService.SaveEYTemplate(bdsm.DataSetParameter, ref  strError);
                            dsAr.ReturnMessage = strError;
                        }
                        break;
                    case "DelEYTemplate":
                        {

                            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", context.Parameters);
                            strTemplateGuid = strTemplateGuid.Trim();
                            if (strTemplateGuid.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }
                            dsAr.Result = eyTemplateService.DelEYTemplate(strTemplateGuid, ref  strError);
                            dsAr.ReturnMessage = strError;
                        }
                        break;
                    case "UpdateEYTemplate":
                        {

                            dsAr.Result = eyTemplateService.UpdateEYTemplate(bdsm.DataSetParameter, ref  strError);
                            dsAr.ReturnMessage = strError;
                        }
                        break;
                    case "LockEYTemplate":
                        {
                            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", context.Parameters);
                            string strOwner = UC.Utilities.GetParameter("Owner", context.Parameters);
                            string strOwnerIP = UC.Utilities.GetParameter("OwnerIP", context.Parameters);

                            string strLockInfo = "";
                            strTemplateGuid = strTemplateGuid.Trim();
                            if (strTemplateGuid.Length == 0)
                            {
                                throw new Exception("Param is invalid");
                            }
                            dsAr.Result = eyTemplateService.LockEYTemplate(strTemplateGuid, strOwner, strOwnerIP, ref strLockInfo, ref  strError);
                            dsAr.ReturnMessage = strError;
                            dsAr.ReturnString = strLockInfo;
                        }
                        break;

                    case "UnLockEYTemplate":
                        {

                            string strTemplateGuid = UC.Utilities.GetParameter("TemplateGuid", context.Parameters);
                            string strOwner = UC.Utilities.GetParameter("Owner", context.Parameters);
                            strTemplateGuid = strTemplateGuid.Trim();
                           
                            dsAr.Result = eyTemplateService.UnLockEYTemplate(strTemplateGuid, strOwner, ref  strError);
                            dsAr.ReturnMessage = strError;
                        }
                        break;

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
                dsAr.ReturnMessage = e.Message;
                dsAr.Result = false;
                return dsAr;

            }

            return dsAr;
        }
     
    }
}
