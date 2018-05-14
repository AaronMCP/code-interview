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
using System.Text.RegularExpressions;
namespace CommonGlobalSettings
{
    [Serializable()]
    public class TeachingModel:TeachingBaseModel
    {
        private string teachingGuid;
        private string reportGuid;
        private string fileType;
        private string acrCode;

        private string submitter;
        private DateTime submitDateTime;
        private string aCodeDetail;
        private string pCodeDetail;
        private string accordRate;
        private string optional1;
        private string optional2;
        private string optional3;
        private string optional4;
        private string optional5;
        private string optional6;
        private string userGuid;
        private int type;//private = 0,public  = 1
        private int replaceSubmitterAndDate;// 1 to replace, 0 not
        private string _domain;


        public string UserGuid
        {
            get
            {
                    return userGuid;
             }
            set
            {
                userGuid = value;
            }
         }
        public string TeachingGuid
        {
            get 
            {
                return teachingGuid;
            }
            set
            {
                teachingGuid = value;
            }
        }
        public string ReportGuid
        {
            get
            {
                return reportGuid;
            }
            set
            {
                reportGuid = value;
            }
        }
        public string FileType
        {
            get 
            {
                return fileType;
            }
            set
            {
                fileType = value;
            }
        }
        public string ACRCode
        {
            get
            {
                return acrCode;
            }
            set
            {
                acrCode = value;
            }
        }
       
        public string Submitter
        {
            get
            {
                return submitter;
            }
            set
            {
                submitter = value;
            }
        }
        public DateTime SubmitDateTime
        {
            get
            {
                return submitDateTime;
            }
            set
            {
                submitDateTime = value;
            }
        }
        public string ACodeDetail
        {
            get
            {
                return aCodeDetail;
            }
            set
            {
                aCodeDetail = value;
            }
        }
        public string PCodeDetail
        {
            get
            {
                return pCodeDetail;
            }
            set
            {
                pCodeDetail = value;
            }
        }
        public string AccordRate
        {
            get
            {
                return accordRate;
            }
            set
            {
                accordRate = value;
            }
        }
        public string Optional1
        {
            get
            {
                return optional1;
            }
            set
            {
                optional1 = value;
            }
        }
        public string Optional2
        {
            get
            {
                return optional2;
            }
            set
            {
                optional2 = value;
            }
        }
        public string Optional3
        {
            get
            {
                return optional3;
            }
            set
            {
                optional3 = value;
            }
        }
        public string Optional4
        {
            get
            {
                return optional4;
            }
            set
            {
                optional4 = value;
            }
        }
        public string Optional5
        {
            get
            {
                return optional5;
            }
            set
            {
                optional5 = value;
            }
        }
        public string Optional6
        {
            get
            {
                return optional6;
            }
            set
            {
                optional6 = value;
            }
        }
        public int Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public string Domain
        {
            get
            {
                return _domain;
            }
            set
            {
                _domain = value;
            }
        }

        public int ReplaceSubmitterAndDate
        {
            get
            {
                return replaceSubmitterAndDate;
            }
            set
            {
                replaceSubmitterAndDate = value;
            }
        }

        #region Modified by Blue for RC569, 4/22/2014
        private string _evaluation;

        public string Evaluation
        {
            get { return _evaluation; }
            set { _evaluation = value; }
        }

        #endregion  

        public override ActionMessage Validator()
        {
            if (teachingGuid==null||teachingGuid=="")
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "TeachingGuid must not be empty!";
                return actionMessage;
            }
            if (reportGuid == null || reportGuid == "")
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "ReportGuid must not be empty!";
                return actionMessage;
            }

            //{Bruce Deng 20090105
            /*
            if (fileType<0||fileType>4)
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "File type is illegal!";
                return actionMessage;
            }*/
            //}
            //if (acrCode== null || acrCode == "")
            //{
            //    ActionMessage actionMessage = new ActionMessage();
            //    actionMessage.Message = "Anatomical code must not be empty!";
            //    return actionMessage;
            //}
       
            if (submitter == null || submitter == "")
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "Submitter must not be empty!";
                return actionMessage;
            }
        
            //if (aCodeDetail== null || aCodeDetail == "")
            //{
            //    ActionMessage actionMessage = new ActionMessage();
            //    actionMessage.Message = "Anatomical description must not be empty!";
            //    return actionMessage;
            //}
            //if (pCodeDetail == null || pCodeDetail == "")
            //{
            //    ActionMessage actionMessage = new ActionMessage();
            //    actionMessage.Message = "Pathological description must not be empty!";
            //    return actionMessage;
            //}
             if (userGuid == null || userGuid == "")
            {
                ActionMessage actionMessage = new ActionMessage();
                actionMessage.Message = "UserGuid description must not be empty!";
                return actionMessage;
            }
            return null;
        } 

    }
}
