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
using Common.Action;
using CommonGlobalSettings;

namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(LoginSettingsModel))]
    [System.Xml.Serialization.XmlInclude(typeof(BaseDataSetModel))]
    [System.Xml.Serialization.XmlInclude(typeof(OamBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(FrameworkBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ReportTemplateBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(TeachingBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(CommonBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(StatisticBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ExamBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(PatientBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(OrderBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ReportBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ReferralBaseModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SearchCriteriaBaseModel))]
    public abstract class BaseModel
    {
        //protected MultiLanManager languageManager = ClientFrameworkBuilder.Instance.MultiLanManager;

        public virtual ActionMessage Validator()
        {
            return null;
        }

        public virtual void Reset()
        {

        }
    }
}
