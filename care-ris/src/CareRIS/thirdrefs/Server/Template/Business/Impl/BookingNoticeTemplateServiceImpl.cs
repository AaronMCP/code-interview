using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Server.Business.Templates;
using Server.DAO.Templates;
using CommonGlobalSettings;

namespace Server.Business.Templates.Impl
{
    class BookingNoticeTemplateServiceImpl:IBookingNoticeTemplateService
    {
        private IDBProvider dbProvider = DataBasePool.Instance.GetDBProvider();

        public virtual bool AddBookingNoticeTemplate(BookingNoticeModel model)
        {
            return dbProvider.AddBookingNoticeTemplate(model);
        }
        public virtual bool DeleteBookingNoticeTemplate(BookingNoticeModel model)
        {
            return dbProvider.DeleteBookingNoticeTemplate(model);
        }
        public virtual bool ModifyBookingNoticeTemplate(BookingNoticeModel model)
        {
            return dbProvider.ModifyBookingNoticeTemplate(model);
        }
        public virtual  DataSet GetAllBookingNoticeTemplate()
        {
            return dbProvider.GetAllBookingNoticeTemplate();
        }
        public virtual DataSet GetOneBookingNoticeTemplate(BookingNoticeModel model)
        {
            return dbProvider.GetOneBookingNoticeTemplate(model);
        }
        public virtual DataSet GetBookingNoticeTemplates(BookingNoticeModel model)
        {
            return dbProvider.GetBookingNoticeTemplates(model);
        }
   }
}
