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
using System.Xml;


namespace CommonGlobalSettings
{
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(DictionaryModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ResourceModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ProcedureCodeModel))]
    [System.Xml.Serialization.XmlInclude(typeof(WorkTimeModel))]
    [System.Xml.Serialization.XmlInclude(typeof(EmployeeScheduleModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ModalityScheduleModel))]
    [System.Xml.Serialization.XmlInclude(typeof(CopyScheduleModel))]
    [System.Xml.Serialization.XmlInclude(typeof(RoleModel))]
    [System.Xml.Serialization.XmlInclude(typeof(UserModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SystemModel))]
    [System.Xml.Serialization.XmlInclude(typeof(HippaModel))]
    [System.Xml.Serialization.XmlInclude(typeof(BulletinBoardModel))]
    [System.Xml.Serialization.XmlInclude(typeof(BookingNoticeModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ChargeCodeModel))]
    [System.Xml.Serialization.XmlInclude(typeof(ChargeModel))]
    [System.Xml.Serialization.XmlInclude(typeof(SpecialDay))]
    [System.Xml.Serialization.XmlInclude(typeof(SpecialDayCollection))]
    [System.Xml.Serialization.XmlInclude(typeof(RoleNodeModel))]
    [System.Xml.Serialization.XmlInclude(typeof(UserCertModel))]
    [System.Xml.Serialization.XmlInclude(typeof(FilmStoreModule))]
    [System.Xml.Serialization.XmlInclude(typeof(FilmReservedModule))]
    [System.Xml.Serialization.XmlInclude(typeof(PathologyTrackModel))]
    [System.Xml.Serialization.XmlInclude(typeof(MedicineStoreModel))]
    [System.Xml.Serialization.XmlInclude(typeof(MedicineStoreLogModel))]    
    public abstract class OamBaseModel : BaseModel
    {
    }
}
