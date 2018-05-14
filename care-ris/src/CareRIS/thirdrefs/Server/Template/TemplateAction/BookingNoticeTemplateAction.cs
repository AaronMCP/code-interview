/****************************************************************************/
/*                                                                          */
/*                          Copyright 2006                                  */
/*                       CareStreamHealth                                   */
/*                        All Rights Reserved.                              */
/*                                                                          */
/*     This software contains proprietary and confidential information      */
/*     belonging to CareStreamHealth , and may not be decompiled,           */
/*     disassembled, disclosed, reproduced or copied without the prior      */
/*     written consent of CareStreamHealth .                                */
/*                                                                          */
/*                        Author : Jame Wei
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
using UC = CommonGlobalSettings;
using CommonGlobalSettings;

namespace Server.TemplatesActions.Action
{
    public class BookingNoticeTemplateAction:BaseAction
    {
        private IBookingNoticeTemplateService bookingNoticeTemplateService = BusinessFactory.Instance.GetBookingNoticeTemplateService();
        private IPhraseTemplateService phraseTemplateService = BusinessFactory.Instance.GetPhraseTemplateService();
        public override BaseActionResult Execute(Context context)
        {
            string actionName = CommonGlobalSettings.Utilities.GetParameter("actionName", context.Parameters);

            DataSetActionResult dsAr = new DataSetActionResult();
            dsAr.DataSetData = new DataSet();
            try
            {
                string strError = "";
                BookingNoticeModel model = context.Model as BookingNoticeModel;
                switch (actionName)
                {
                    case "AddBookingNoticeTemplate":
                        {

                            dsAr.Result = bookingNoticeTemplateService.AddBookingNoticeTemplate(model);
                            dsAr.ReturnMessage = strError;
                        }
                        break;

                    case "ModifyBookingNoticeTemplate":
                        {

                            dsAr.Result = bookingNoticeTemplateService.ModifyBookingNoticeTemplate(model);
                            dsAr.ReturnMessage = strError;
                        }
                        break;
                    case "DeleteBookingNoticeTemplate":
                        {
                            dsAr.Result = bookingNoticeTemplateService.DeleteBookingNoticeTemplate(model);
                            dsAr.ReturnMessage = strError;
                        }
                        break;
                    case "GetAllBookingNoticeTemplate":
                        {

                            dsAr.DataSetData = bookingNoticeTemplateService.GetAllBookingNoticeTemplate();
                            dsAr.ReturnMessage = strError;
                        }
                        break;
                    case "GetOneBookingNoticeTemplate":
                        {

                            dsAr.DataSetData = bookingNoticeTemplateService.GetOneBookingNoticeTemplate(model);
                            dsAr.ReturnMessage = strError;
                        }
                        break;
                    case "GetModalityType":
                        {

                            dsAr.DataSetData= phraseTemplateService.GetModalityType();
                            dsAr.ReturnMessage = strError;
                        }
                        break;
                    case "GetBookingNoticeTemplates":
                        {

                            dsAr.DataSetData = bookingNoticeTemplateService.GetBookingNoticeTemplates(model);
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
